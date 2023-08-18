using DataAccess.Helpers;
using DataAccess.Models.DTO;

namespace DataAccess.Services;

public interface IParkingService
{
    Task<GenericResult> Park(ParkRequestDto model);
    Task<GenericResult> Unpark(UnparkRequestDto model);
}