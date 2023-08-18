using System.Data;
using System.Data.SqlClient;
using System.Net;
using DataAccess.DbAccess;
using DataAccess.Helpers;
using DataAccess.Models.DTO;
using DataAccess.Models.Entity;
using DataAccess.Repositories;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Services;
public class ParkingService : IParkingService
{
    private readonly IRDBMDataAccess _db;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IConfiguration _config;
    private readonly IEntryPointRepository _entryPointRepository;
    private readonly IParkingSlotRepository _parkingSlotRepository;
    private readonly IVehicleParkingSlotRepository _vehicleParkingSlotRepository;
    private readonly IParkingSlotService _slotAssignmentService;
    private readonly IFeeComputationService _feeComputationService;
    public ParkingService(IRDBMDataAccess db,
        IVehicleRepository vehicleRepository,
        IConfiguration config,
        IEntryPointRepository entryPointRepository,
        IParkingSlotRepository parkingSlotRepository,
        IVehicleParkingSlotRepository vehicleParkingSlotRepository,
        IParkingSlotService slotAssignmentService, 
        IFeeComputationService feeComputationService)
    {
        _db = db;
        _vehicleRepository = vehicleRepository;
        _config = config;
        _entryPointRepository = entryPointRepository;
        _parkingSlotRepository = parkingSlotRepository;
        _vehicleParkingSlotRepository = vehicleParkingSlotRepository;
        _slotAssignmentService = slotAssignmentService;
        _feeComputationService = feeComputationService;
    }

    /* Algo for parking
     * 1. Check if vehicle is in the DB
        * true: get id
            * check if already parked   
        * false: save first to db
     *  2. Validate entrypoint endhere if has any error
     * 3. Get assigned Slot from Sp_GetParkingSlot usind ids from 1 and 2
     * 4.1 Update ParkingSLot status(IsOccupied) and 
        * Using id from 3 
     * 4.2 create new VehicleParkingSlot record
        * using id from 3 and 1
     * 5. Return info
    */

    public async Task<GenericResult> Park(ParkRequestDto model)
    {
        try
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("Default"));
            connection.Open();

            using var transact = connection.BeginTransaction();

            //1. check vehicle existence
            var vehicle = await _vehicleRepository.GetByPlateNumber_T(connection, transact, model.PlateNumber);
            if (vehicle == null)
            {
                //non existent so create it first
                await _vehicleRepository.Insert_T(connection, transact, new Vehicle { PlateNumber = model.PlateNumber, Size = model.Size });
                vehicle = await _vehicleRepository.GetByPlateNumber_T(connection, transact, model.PlateNumber);
            }
            else
            {
                //check if already parked
                var vpsInfo = await _vehicleParkingSlotRepository.GetExtendedInfo_T(connection, transact, vehicle.PlateNumber);
                if (vpsInfo != null)
                {
                    if(vpsInfo.Where(x => x.ExitDateTime == null).Count() > 0)
                    {
                        transact.Rollback();
                        return new GenericResult(false, "Vehicle Already Parked", response: (int)HttpStatusCode.BadRequest);
                    }
                }
            }


            //2. validate entrypoint
            var entryPoint = await _entryPointRepository.GetByName_T(connection, transact, model.EntryPoint);
            if (entryPoint == null)
            {
                transact.Rollback();
                return new GenericResult(false, "Invalid Entrypoint Name", response: (int)HttpStatusCode.BadRequest);
            }

            //3. Get slot
            var slot = await _slotAssignmentService.GetAvailableSlot(connection, transact, entryPoint.Id, vehicle.Id);

            if (slot.responseData == null)
            {
                transact.Rollback();
                return slot;
            }

            var availableSlot = (AvailableSlotDto)slot.responseData;

            //4.1 Update ParkingSlot
            await _parkingSlotRepository.Update_T(connection, transact, new ParkingSlot { Id = availableSlot.ParkingSlotId, Size = availableSlot.SlotSize, IsOccupied = true });

            //4.2 Create VehicleParkingSlot
            await _vehicleParkingSlotRepository.Insert_T(connection, transact, new VehicleParkingSlot { ParkingSlotId = availableSlot.ParkingSlotId, VehicleId = vehicle.Id, EntryDateTime = model.EntryDateTime });


            //all is well
            transact.Commit();

            //return vehicle parking slot info
            return new GenericResult(true, "Success", availableSlot);
        }
        catch (Exception ex)
        {
            return new GenericResult(false, ex.Message, response: (int)HttpStatusCode.InternalServerError);
        }
    }


    /* Algo for parking
     * 1. Check if vehicle is parked
        * true: unpark: Update PakingSlot IsOccupied to false and VehicleParkingSlot ExitDateTime
        * false: return error
    *  2. Compute fee
     * 3. Return info
    */
    public async Task<GenericResult> Unpark(UnparkRequestDto model)
    {
        try
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("Default"));
            connection.Open();

            using var transact = connection.BeginTransaction();

            //1. Check if vehicle is parked
            var vpsInfo = await _vehicleParkingSlotRepository.GetExtendedInfo_T(connection, transact, model.PlateNumber);
            var toUnpark = vpsInfo.Where(x => x.ExitDateTime == null && x.IsOccupied == true).FirstOrDefault();
            if (vpsInfo == null || toUnpark == null)
            {
                transact.Rollback();
                return new GenericResult(false, "Vehicle not found", response: (int)HttpStatusCode.BadRequest);
            }

            //Update PakingSlot IsOccupied
            await _parkingSlotRepository.Update_T(connection, transact, new ParkingSlot { Id = toUnpark.ParkingSlotId, IsOccupied = false, Size = toUnpark.SlotSize });

            //update VehicleParkingSlot
            await _vehicleParkingSlotRepository.Update_T(
                connection,
                transact,
                new VehicleParkingSlot { 
                    Id = toUnpark.Id, 
                    EntryDateTime = toUnpark.EntryDateTime, 
                    ExitDateTime = model.ExitDateTime, 
                    ParkingSlotId = toUnpark.ParkingSlotId, 
                    VehicleId = toUnpark.VehicleId 
                }
            );

            //all is well
            transact.Commit();

            var totalFee = await _feeComputationService.GetParkingFee(toUnpark.PlateNumber);

            //return vehicle parking slot info
            return new GenericResult(true, "Success", totalFee);
        }
        catch (Exception ex)
        {
            return new GenericResult(false, ex.Message, response: (int)HttpStatusCode.InternalServerError);
        }
    }
}
