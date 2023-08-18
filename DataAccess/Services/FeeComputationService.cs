using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DbAccess;
using DataAccess.Helpers;
using DataAccess.Models.DTO;
using DataAccess.Repositories;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Services;
public class FeeComputationService : IFeeComputationService
{
    private readonly IRDBMDataAccess _db;
    private readonly IConfiguration _config;
    private readonly IVehicleParkingSlotRepository _vehicleParkingSlotRepository;

    public FeeComputationService(IRDBMDataAccess db, IConfiguration config, IVehicleParkingSlotRepository vehicleParkingSlotRepository)
    {
        _db = db;
        _config = config;
        _vehicleParkingSlotRepository = vehicleParkingSlotRepository;
    }

    public async Task<GenericResult> GetParkingFee(string PlateNumber)
    {
        try
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("Default"));
            connection.Open();

            using var transact = connection.BeginTransaction();

            var vpsInfo = await _vehicleParkingSlotRepository.GetExtendedInfo_T(connection, transact, PlateNumber);

            //computation scenarios
            /*
             * 1. Simple - flatrate
             * 2. Simple - flatrate + slotSize rate
             * 3. Complex - continuous rate 
             */
            double totalminutes = 0.0;
            var parkingLogs = vpsInfo.ToList();
            int totalFee = 0;


            var ContinuousRateRecords = new List<ExtendedParkingInfoDto>();
            //get the latest parking log record
            ContinuousRateRecords.Add(parkingLogs[0]);

            //get only valid continuous log
            for (int i = 1; i < parkingLogs.Count(); i++)
            {
                if((ContinuousRateRecords[i - 1].EntryDateTime - parkingLogs[i].ExitDateTime.Value).TotalMinutes <= 60)
                {
                    ContinuousRateRecords.Add(parkingLogs[i]);
                }
            }

            totalminutes = (ContinuousRateRecords[0].ExitDateTime.Value - ContinuousRateRecords[ContinuousRateRecords.Count() -1].EntryDateTime).TotalMinutes;

            //if (parkingLogs.Count() == 1)
            //{
            //    totalminutes += (parkingLogs[0].ExitDateTime.Value - parkingLogs[0].EntryDateTime).TotalMinutes;
            //}

            ////create a new collection with records that qualifies for for continuous rate
            //for (int i = 0; i < parkingLogs.Count(); i++)
            //{
            //    if ((i + 1) < vpsInfo.Count())
            //    {
            //        if((parkingLogs[i + 1].ExitDateTime.Value - parkingLogs[i].EntryDateTime).TotalMinutes < 60)
            //        {
            //            totalminutes += (parkingLogs[i].ExitDateTime.Value - parkingLogs[i + 1].EntryDateTime).TotalMinutes;
            //        }

            //    }
            //}

            var hoursStayed = (int)Math.Ceiling(totalminutes/60);
            var unchangedhoursStayed = hoursStayed;
            //exceed 24-hour
            if (hoursStayed > 24)
            {
                int chuckof24 = (int)hoursStayed / 24;
                totalFee += chuckof24 * 5000;
                hoursStayed -= chuckof24 * 24;

                //all excess hours will be computed on rule b
                totalFee += getExcessHoursFee(hoursStayed, parkingLogs[0].SlotSize);
            }
            else{

                //exceed 3 hours
                if (hoursStayed > 3)
                {
                    //compute for the the hours after the flatrate rule
                    var excessHours = (hoursStayed - 3);
                    totalFee += getExcessHoursFee(excessHours, parkingLogs[0].SlotSize);
                }

                //flatrate
                totalFee += 40;
            }

            return new GenericResult(true, "Success", new { ParkingHistoryComputed = ContinuousRateRecords, HoursStayed = unchangedhoursStayed, TotalFee = totalFee });

        }
        catch (Exception ex)
        {
            return new GenericResult(false, ex.Message, response: (int)HttpStatusCode.InternalServerError);
        }
    }

    private int getExcessHoursFee(int hoursStayed, int slotSize)
    {
        int totalFee = 0;
        switch (slotSize)
        {
            case 0:
                totalFee += hoursStayed * 20;
                break;
            case 1:
                totalFee += hoursStayed * 60;
                break;
            case 2:
                totalFee += hoursStayed * 100;
                break;
            default:
                break;
        }

        return totalFee;
    }
}
