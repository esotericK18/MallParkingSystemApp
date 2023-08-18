using Dapper;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using DataAccess.Helpers;

namespace DataAccess.DbAccess;

public class RDBMDataAccess : IRDBMDataAccess
{
    private readonly IConfiguration _config;

    public RDBMDataAccess(IConfiguration config)
    {
        _config = config;
    }

    public async Task<IEnumerable<T>> LoadData<T, U>(
        string storedProcedure, 
        CRUD_TYPE crudType,
        U parameters,
        string connectionId = "Default")
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

        var SpParams = new DynamicParameters();
        SpParams.AddDynamicParams(parameters);
        SpParams.Add("@init_flag", crudType);

        return await connection.QueryAsync<T>(storedProcedure, SpParams,
            commandType: CommandType.StoredProcedure);
    }

    public async Task SaveData<T>(
        string storedProcedure, 
        CRUD_TYPE crudType,
        T parameters,
        string connectionId = "Default")
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

        var SpParams = new DynamicParameters();
        SpParams.AddDynamicParams(parameters);
        SpParams.Add("@init_flag", crudType);

        await connection.ExecuteAsync(storedProcedure, SpParams,
            commandType: CommandType.StoredProcedure);
    }


    public async Task<IEnumerable<T>> LoadDataTransact<T, U>(
        IDbConnection db, 
        IDbTransaction transaction,
        string storedProcedure,
        CRUD_TYPE crudType,
        U parameters)
    {

        var SpParams = new DynamicParameters();
        SpParams.AddDynamicParams(parameters);
        SpParams.Add("@init_flag", crudType);

        return await db.QueryAsync<T>(storedProcedure, 
            SpParams,
            transaction,
            commandType: CommandType.StoredProcedure);
    }

    public async Task SaveDataTransact<T>(
        IDbConnection db,
        IDbTransaction transaction,
        string storedProcedure,
        CRUD_TYPE crudType,
        T parameters)
    {

        var SpParams = new DynamicParameters();
        SpParams.AddDynamicParams(parameters);
        SpParams.Add("@init_flag", crudType);

        await db.ExecuteAsync(storedProcedure, 
            SpParams,
            transaction,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<T>> LoadComplexDataTransact<T, U>(
        IDbConnection db,
        IDbTransaction transaction,
        string storedProcedure,
        U parameters)
    {

        var SpParams = new DynamicParameters();
        SpParams.AddDynamicParams(parameters);

        return await db.QueryAsync<T>(storedProcedure,
            SpParams,
            transaction,
            commandType: CommandType.StoredProcedure);
    }
}
