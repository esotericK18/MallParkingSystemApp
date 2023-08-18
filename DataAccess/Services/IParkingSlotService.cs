using System.Data;
using DataAccess.Helpers;
using DataAccess.Models.DTO;

namespace DataAccess.Services;

public interface IParkingSlotService
{
    Task<GenericResult> GetAvailableSlot(IDbConnection connection, IDbTransaction transaction, int entryPointId, int vehicleId);
}
