using System.Data;
using DataAccess.DbAccess;
using DataAccess.Helpers;
using DataAccess.Models.Entity;

namespace DataAccess.Repositories;
public class ParkingSlotRepository : IParkingSlotRepository
{
    private readonly IRDBMDataAccess _db;
    private const string _spName = "[dbo].[Sp_Crud_ParkingSlot]";
    public ParkingSlotRepository(IRDBMDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<ParkingSlot>> Get() =>
        _db.LoadData<ParkingSlot, dynamic>(_spName, CRUD_TYPE.GET_ALL, new { });

    public async Task<ParkingSlot?> Get(int id)
    {
        var results = await _db.LoadData<ParkingSlot, dynamic>(
            _spName,
            CRUD_TYPE.GET_BY_ID,
            new { Id = id });
        return results.FirstOrDefault();
    }

    public Task Insert(ParkingSlot model) =>
        _db.SaveData(_spName, CRUD_TYPE.INSERT, model);

    public Task Update(ParkingSlot model) =>
        _db.SaveData(_spName, CRUD_TYPE.UPDATE, model);

    public Task Delete(int id) =>
        _db.SaveData(_spName, CRUD_TYPE.DELETE, new { Id = id });

    #region "TRANSACTIONAL QUERIES"

    public async Task<ParkingSlot?> GetById_T(IDbConnection connection, IDbTransaction transaction, int id)
    {
        var results = await _db.LoadDataTransact<ParkingSlot, dynamic>(
            connection,
            transaction,
            _spName,
            CRUD_TYPE.GET_BY_ID,
            new { Id = id });
        return results.FirstOrDefault();
    }

    public Task Insert_T(IDbConnection connection, IDbTransaction transaction, ParkingSlot model)
    {
        return _db.SaveDataTransact(connection,
            transaction, _spName, CRUD_TYPE.INSERT, model);
    }

    public Task Update_T(IDbConnection connection, IDbTransaction transaction, ParkingSlot model) 
    { 
        return _db.SaveDataTransact(connection,
            transaction, _spName, CRUD_TYPE.UPDATE, model);

    }

    #endregion
}
