namespace Api.Endpoints;

public static class ParkingSlotApi
{
    public static void ConfigureParkingSlotApi(this WebApplication app)
    {
        app.MapGet("/ParkingSlots", GetParkingSlots);
        app.MapGet("/ParkingSlots/{id}", GetParkingSlot);
        app.MapPost("/ParkingSlots", InsertParkingSlot);
        app.MapPut("/ParkingSlots", UpdateParkingSlot);
        app.MapDelete("/ParkingSlots", DeleteParkingSlot);
    }
    private static async Task<IResult> GetParkingSlots(IParkingSlotRepository repo)
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

    private static async Task<IResult> GetParkingSlot(int id, IParkingSlotRepository repo)
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

    private static async Task<IResult> InsertParkingSlot(ParkingSlot model, IParkingSlotRepository repo)
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

    private static async Task<IResult> UpdateParkingSlot(ParkingSlot model, IParkingSlotRepository repo)
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

    private static async Task<IResult> DeleteParkingSlot(int id, IParkingSlotRepository repo)
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
