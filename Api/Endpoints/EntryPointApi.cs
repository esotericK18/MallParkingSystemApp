namespace Api.Endpoints;

public static class EntryPointApi
{
    public static void ConfigureEntryPointApi(this WebApplication app)
    {
        app.MapGet("/EntryPoints", GetEntryPoints);
        app.MapGet("/EntryPoints/{id}", GetEntryPoint);
        app.MapPost("/EntryPoints", InsertEntryPoint);
        app.MapPut("/EntryPoints", UpdateEntryPoint);
        app.MapDelete("/EntryPoints", DeleteEntryPoint);
    }

    /// <summary>
    /// Gets all the Entrypoint records
    /// </summary>
    /// <param name="repo">Injected Repo</param>
    /// <returns>List of Entrypoints</returns>
    private static async Task<IResult> GetEntryPoints(IEntryPointRepository repo)
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

    private static async Task<IResult> GetEntryPoint(int id, IEntryPointRepository repo)
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

    private static async Task<IResult> InsertEntryPoint(EntryPoint model, IEntryPointRepository repo)
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

    private static async Task<IResult> UpdateEntryPoint(EntryPoint model, IEntryPointRepository repo)
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

    private static async Task<IResult> DeleteEntryPoint(int id, IEntryPointRepository repo)
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
