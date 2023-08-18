using DataAccess.DbAccess;
using DataAccess.Helpers;
using DataAccess.Models.Entity;

namespace DataAccess.Repositories;
public class PSEPDistanceRepository : IPSEPDistanceRepository
{
    private readonly IRDBMDataAccess _db;
    private const string _spName = "[dbo].[Sp_Crud_ParkingSlotEntryPointDistance]";
    public PSEPDistanceRepository(IRDBMDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<ParkingSlotEntryPointDistance>> Get() =>
        _db.LoadData<ParkingSlotEntryPointDistance, dynamic>(_spName, CRUD_TYPE.GET_ALL, new { });

    public async Task<ParkingSlotEntryPointDistance?> Get(int id)
    {
        var results = await _db.LoadData<ParkingSlotEntryPointDistance, dynamic>(
            _spName,
            CRUD_TYPE.GET_BY_ID,
            new { Id = id });
        return results.FirstOrDefault();
    }

    public Task Insert(ParkingSlotEntryPointDistance model) =>
        _db.SaveData(_spName, CRUD_TYPE.INSERT, model);

    public Task Update(ParkingSlotEntryPointDistance model) =>
        _db.SaveData(_spName, CRUD_TYPE.UPDATE, model);

    public Task Delete(int id) =>
        _db.SaveData(_spName, CRUD_TYPE.DELETE, new { Id = id });
}
