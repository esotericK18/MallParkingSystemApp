using DataAccess.Models.Entity;

namespace DataAccess.Repositories;

public interface IPSEPDistanceRepository
{
    Task Delete(int id);
    Task<IEnumerable<ParkingSlotEntryPointDistance>> Get();
    Task<ParkingSlotEntryPointDistance?> Get(int id);
    Task Insert(ParkingSlotEntryPointDistance model);
    Task Update(ParkingSlotEntryPointDistance model);
}