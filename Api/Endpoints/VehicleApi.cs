namespace Api.Endpoints;

public static class VehicleApi
{
    public static void ConfigureVehicleApi(this WebApplication app)
    {
        app.MapGet("/Vehicles", GetVehicles);
        app.MapGet("/Vehicle/{id:int}", GetVehicle);
        app.MapGet("/Vehicle/ByPlate/{plateNumber}", GetVehicleByPlate);
        app.MapPost("/Vehicles", InsertVehicle);
        app.MapPut("/Vehicles", UpdateVehicle);
        app.MapDelete("/Vehicles", DeleteVehicle);
    }

    private static async Task<IResult> GetVehicles(IVehicleRepository repo)
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

    private static async Task<IResult> GetVehicle(int id, IVehicleRepository repo)
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

    private static async Task<IResult> GetVehicleByPlate(string plateNumber, IVehicleRepository repo)
    {
        try
        {
            var results = await repo.GetByPlateNumber(plateNumber);
            if (results == null) return Results.NotFound();
            return Results.Ok(results);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> InsertVehicle(Vehicle model, IVehicleRepository repo)
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

    private static async Task<IResult> UpdateVehicle(Vehicle model, IVehicleRepository repo)
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

    private static async Task<IResult> DeleteVehicle(int id, IVehicleRepository repo)
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
