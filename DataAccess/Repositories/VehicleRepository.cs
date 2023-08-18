using System.Data;
using System.Data.SqlClient;
using DataAccess.DbAccess;
using DataAccess.Helpers;
using DataAccess.Models.Entity;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Repositories;
public class VehicleRepository : IVehicleRepository
{
    private readonly IRDBMDataAccess _db;
    private const string _spName = "[dbo].[Sp_Crud_Vehicle]";

    public VehicleRepository(IRDBMDataAccess db, IConfiguration config)
    {
        _db = db;
    }

    public Task<IEnumerable<Vehicle>> Get() =>
        _db.LoadData<Vehicle, dynamic>(_spName, CRUD_TYPE.GET_ALL, new { });

    public async Task<Vehicle?> Get(int id)
    {
        var results = await _db.LoadData<Vehicle, dynamic>(
            _spName,
            CRUD_TYPE.GET_BY_ID,
            new { Id = id });
        return results.FirstOrDefault();
    }

    public Task Insert(Vehicle model) =>
        _db.SaveData(_spName, CRUD_TYPE.INSERT, model);

    public Task Update(Vehicle model) =>
        _db.SaveData(_spName, CRUD_TYPE.UPDATE, model);

    public Task Delete(int id) =>
        _db.SaveData(_spName, CRUD_TYPE.DELETE, new { Id = id });

    public async Task<Vehicle?> GetByPlateNumber(string plateNumber)
    {
        var results = await _db.LoadData<Vehicle, dynamic>(
            _spName,
            CRUD_TYPE.GET_BY_NAME,
            new { PlateNumber = plateNumber });
        return results.FirstOrDefault();
    }

    #region "TRANSACTIONAL QUERIES"

    public async Task<Vehicle?> GetByPlateNumber_T(IDbConnection connection, IDbTransaction transaction, string plateNumber)
    {
        //using IDbConnection connection = new SqlConnection(_config.GetConnectionString("Default"));
        //using var transact = connection.BeginTransaction();
        var results = await _db.LoadDataTransact<Vehicle, dynamic>(
            connection,
            transaction,
            _spName,
            CRUD_TYPE.GET_BY_NAME,
            new { PlateNumber = plateNumber });
        return results.FirstOrDefault();
    }

    public Task Insert_T(IDbConnection connection, IDbTransaction transaction, Vehicle model){
        //using IDbConnection connection = new SqlConnection(_config.GetConnectionString("Default"));
        //using var transact = connection.BeginTransaction();
        return _db.SaveDataTransact(connection,
            transaction, _spName, CRUD_TYPE.INSERT, model);
    }
    #endregion
}
