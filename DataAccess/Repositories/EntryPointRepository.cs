using System.Data;
using DataAccess.DbAccess;
using DataAccess.Helpers;
using DataAccess.Models.Entity;

namespace DataAccess.Repositories;
public class EntryPointRepository : IEntryPointRepository
{
    private readonly IRDBMDataAccess _db;
    private const string _spName = "[dbo].[Sp_Crud_EntryPoint]";
    public EntryPointRepository(IRDBMDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<EntryPoint>> Get() =>
        _db.LoadData<EntryPoint, dynamic>(_spName, CRUD_TYPE.GET_ALL,  new { });

    public async Task<EntryPoint?> Get(int id)
    {
        var results = await _db.LoadData<EntryPoint, dynamic>(
            _spName,
            CRUD_TYPE.GET_BY_ID,
            new { Id = id });
        return results.FirstOrDefault();
    }

    public Task Insert(EntryPoint model) =>
        _db.SaveData(_spName, CRUD_TYPE.INSERT, model);

    public Task Update(EntryPoint model) =>
        _db.SaveData(_spName, CRUD_TYPE.UPDATE, model);

    public Task Delete(int id) =>
        _db.SaveData(_spName, CRUD_TYPE.DELETE, new { Id = id });

    #region "TRANSACTIONAL QUERIES"

    public async Task<EntryPoint?> GetByName_T(IDbConnection connection, IDbTransaction transaction, string name)
    {
        var results = await _db.LoadDataTransact<EntryPoint, dynamic>(
            connection,
            transaction,
            _spName,
            CRUD_TYPE.GET_BY_NAME,
            new { Name = name });
        return results.FirstOrDefault();
    }

    public Task Insert_T(IDbConnection connection, IDbTransaction transaction, EntryPoint model)
    {
        return _db.SaveDataTransact(connection,
            transaction, _spName, CRUD_TYPE.INSERT, model);
    }
    #endregion
}
