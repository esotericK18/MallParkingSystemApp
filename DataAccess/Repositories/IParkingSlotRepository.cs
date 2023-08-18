using System.Data;
using DataAccess.Models.Entity;

namespace DataAccess.Repositories;

public interface IParkingSlotRepository
{
    Task Delete(int id);
    Task<IEnumerable<ParkingSlot>> Get();
    Task<ParkingSlot?> Get(int id);
    Task<ParkingSlot?> GetById_T(IDbConnection connection, IDbTransaction transaction, int id);
    Task Insert(ParkingSlot model);
    Task Insert_T(IDbConnection connection, IDbTransaction transaction, ParkingSlot model);
    Task Update(ParkingSlot model);
    Task Update_T(IDbConnection connection, IDbTransaction transaction, ParkingSlot model);
}