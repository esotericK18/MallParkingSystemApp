namespace Api.Endpoints;

public static class ParkingSlotEntryPointDistanceApi
{
    public static void ConfigureParkingSlotEntryPointDistanceApi(this WebApplication app)
    {
        app.MapGet("/ParkingSlotEntryPointDistances", GetParkingSlotEntryPointDistances);
        app.MapGet("/ParkingSlotEntryPointDistances/{id}", GetParkingSlotEntryPointDistance);
        app.MapPost("/ParkingSlotEntryPointDistances", InsertParkingSlotEntryPointDistance);
        app.MapPut("/ParkingSlotEntryPointDistances", UpdateParkingSlotEntryPointDistance);
        app.MapDelete("/ParkingSlotEntryPointDistances", DeleteParkingSlotEntryPointDistance);
    }

    private static async Task<IResult> GetParkingSlotEntryPointDistances(IPSEPDistanceRepository repo)
    {
        try
        {
            return Results.Ok(await repo.Get());
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> GetParkingSlotEntryPointDistance(int id, IPSEPDistanceRepository repo)
    {
        try
        {
            var results = await repo.Get(id);
            if (results == null) return Results.NotFound();
            return Results.Ok(results);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> InsertParkingSlotEntryPointDistance(ParkingSlotEntryPointDistance model, IPSEPDistanceRepository repo)
    {
        try
        {
            await repo.Insert(model);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> UpdateParkingSlotEntryPointDistance(ParkingSlotEntryPointDistance model, IPSEPDistanceRepository repo)
    {
        try
        {
            await repo.Update(model);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> DeleteParkingSlotEntryPointDistance(int id, IPSEPDistanceRepository repo)
    {
        try
        {
            await repo.Delete(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}
