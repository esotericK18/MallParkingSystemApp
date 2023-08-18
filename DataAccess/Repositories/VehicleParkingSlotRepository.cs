using System.Data;
using DataAccess.DbAccess;
using DataAccess.Helpers;
using DataAccess.Models.DTO;
using DataAccess.Models.Entity;

namespace DataAccess.Repositories;
public class VehicleParkingSlotRepository : IVehicleParkingSlotRepository
{
    private readonly IRDBMDataAccess _db;
    private const string _spName = "[dbo].[Sp_Crud_VehicleParkingSlot]";
    public VehicleParkingSlotRepository(IRDBMDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<VehicleParkingSlot>> Get() =>
        _db.LoadData<VehicleParkingSlot, dynamic>(_spName, CRUD_TYPE.GET_ALL, new { });

    public async Task<VehicleParkingSlot?> Get(int id)
    {
        var results = await _db.LoadData<VehicleParkingSlot, dynamic>(
            _spName,
            CRUD_TYPE.GET_BY_ID,
            new { Id = id });
        return results.FirstOrDefault();
    }

    public Task Insert(VehicleParkingSlot model) =>
        _db.SaveData(_spName, CRUD_TYPE.INSERT, model);

    public Task Update(VehicleParkingSlot model) =>
        _db.SaveData(_spName, CRUD_TYPE.UPDATE, model);

    public Task Delete(int id) =>
        _db.SaveData(_spName, CRUD_TYPE.DELETE, new { Id = id });

    #region "TRANSACTIONAL QUERIES"

    public async Task<VehicleParkingSlot?> GetById_T(IDbConnection connection, IDbTransaction transaction, int id)
    {
        var results = await _db.LoadDataTransact<VehicleParkingSlot, dynamic>(
            connection,
            transaction,
            _spName,
            CRUD_TYPE.GET_BY_ID,
            new { Id = id });
        return results.FirstOrDefault();
    }

    public Task Insert_T(IDbConnection connection, IDbTransaction transaction, VehicleParkingSlot model)
    {
        return _db.SaveDataTransact(connection,
            transaction, _spName, CRUD_TYPE.INSERT, model);
    }

    public Task Update_T(IDbConnection connection, IDbTransaction transaction, VehicleParkingSlot model)
    {
        return _db.SaveDataTransact(connection,
            transaction, _spName, CRUD_TYPE.UPDATE, model);

    }

    public async Task<IEnumerable<ExtendedParkingInfoDto>> GetExtendedInfo_T(IDbConnection connection, IDbTransaction transaction, string PlateNumber)
    {
        var results = await _db.LoadComplexDataTransact<ExtendedParkingInfoDto, dynamic>(
            connection,
            transaction,
            "Sp_GetExtendedParkingInfo",
            new { PlateNumber });
        return results;
    }

    #endregion
}
