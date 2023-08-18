using System.Data;
using DataAccess.Models.DTO;
using DataAccess.Models.Entity;

namespace DataAccess.Repositories;

public interface IVehicleParkingSlotRepository
{
    Task Delete(int id);
    Task<IEnumerable<VehicleParkingSlot>> Get();
    Task<VehicleParkingSlot?> Get(int id);
    Task<VehicleParkingSlot?> GetById_T(IDbConnection connection, IDbTransaction transaction, int id);
    Task<IEnumerable<ExtendedParkingInfoDto>> GetExtendedInfo_T(IDbConnection connection, IDbTransaction transaction, string PlateNumber);
    Task Insert(VehicleParkingSlot model);
    Task Insert_T(IDbConnection connection, IDbTransaction transaction, VehicleParkingSlot model);
    Task Update(VehicleParkingSlot model);
    Task Update_T(IDbConnection connection, IDbTransaction transaction, VehicleParkingSlot model);
}
