using DataAccess.Helpers;

namespace DataAccess.Services;

public interface IFeeComputationService
{
    Task<GenericResult> GetParkingFee(string PlateNumber);
}
