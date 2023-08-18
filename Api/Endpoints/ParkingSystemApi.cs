using System.Net;
using DataAccess.Models.DTO;

namespace Api.Endpoints;

public static class ParkingSystemApi
{
    public static void ConfigureParkingSystemApi(this WebApplication app)
    {
        app.MapPut("/Park", ParkVehicle);
        app.MapPut("/UnPark", UnParkVehicle);
        app.MapPut("/ComputeFee", ComputeFee);
    }

    private static async Task<IResult> ParkVehicle(ParkRequestDto model, IParkingService repo)
    {
        try
        {
            var results = await repo.Park(model);
            if (results.responseData == null)
            {
                Results.Problem(results.message);
            }

            return Results.Ok(results);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> UnParkVehicle(UnparkRequestDto model, IParkingService repo)
    {
        try
        {
            var results = await repo.Unpark(model);
            if (!results.isSuccess)
            {
                Results.Problem(results.message);
            }

            return Results.Ok(results);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> ComputeFee(string PlateNumber, IFeeComputationService repo)
    {
        try
        {
            var results = await repo.GetParkingFee(PlateNumber);
            if (!results.isSuccess)
            {
                Results.Problem(results.message);
            }

            return Results.Ok(results);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}
