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
public class ParkingSlotService : IParkingSlotService
{
    private readonly IRDBMDataAccess _db;
    public ParkingSlotService(IRDBMDataAccess db)
    {
        _db = db;
    }

    public async Task<GenericResult> GetAvailableSlot(IDbConnection connection, IDbTransaction transaction, int entryPointId, int vehicleId)
    {
        var results = await _db.LoadComplexDataTransact<AvailableSlotDto, dynamic>(
            connection,
            transaction,
            "Sp_GetParkingSlot",
            new { EntryPoint = entryPointId, VehicleId = vehicleId });

        if (results.FirstOrDefault() == null)
        {
            return new GenericResult(false, "No Available Slot", response: (int)HttpStatusCode.NotFound);
        }

        return new GenericResult(true, "Success", results.FirstOrDefault(), response: (int)HttpStatusCode.OK);
    }

}
