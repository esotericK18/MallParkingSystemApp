namespace Api.Endpoints;

public static class VehicleParkingSlotApi
{
    public static void ConfigureVehicleParkingSlotApi(this WebApplication app)
    {
        app.MapGet("/VehicleParkingSlots", GetVehicleParkingSlots);
        app.MapGet("/VehicleParkingSlots/{id}", GetVehicleParkingSlot);
        app.MapPost("/VehicleParkingSlots", InsertVehicleParkingSlot);
        app.MapPut("/VehicleParkingSlots", UpdateVehicleParkingSlot);
        app.MapDelete("/VehicleParkingSlots", DeleteVehicleParkingSlot);
    }

    private static async Task<IResult> GetVehicleParkingSlots(IVehicleParkingSlotRepository repo)
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

    private static async Task<IResult> GetVehicleParkingSlot(int id, IVehicleParkingSlotRepository repo)
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

    private static async Task<IResult> InsertVehicleParkingSlot(VehicleParkingSlot model, IVehicleParkingSlotRepository repo)
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

    private static async Task<IResult> UpdateVehicleParkingSlot(VehicleParkingSlot model, IVehicleParkingSlotRepository repo)
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

    private static async Task<IResult> DeleteVehicleParkingSlot(int id, IVehicleParkingSlotRepository repo)
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
