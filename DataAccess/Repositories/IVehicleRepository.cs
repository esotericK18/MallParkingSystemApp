using System.Data;
using DataAccess.Models.Entity;

namespace DataAccess.Repositories;

public interface IVehicleRepository
{
    Task Delete(int id);
    Task<IEnumerable<Vehicle>> Get();
    Task<Vehicle?> Get(int id);
    Task<Vehicle?> GetByPlateNumber(string plateNumber);
    Task<Vehicle?> GetByPlateNumber_T(IDbConnection connection, IDbTransaction transaction, string plateNumber);
    Task Insert(Vehicle model);
    Task Insert_T(IDbConnection connection, IDbTransaction transaction, Vehicle model);
    Task Update(Vehicle model);
}
