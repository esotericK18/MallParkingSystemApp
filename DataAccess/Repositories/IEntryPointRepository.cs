using System.Data;
using DataAccess.Models.Entity;

namespace DataAccess.Repositories;

public interface IEntryPointRepository
{
    Task Delete(int id);
    Task<IEnumerable<EntryPoint>> Get();
    Task<EntryPoint?> Get(int id);
    Task<EntryPoint?> GetByName_T(IDbConnection connection, IDbTransaction transaction, string name);
    Task Insert(EntryPoint model);
    Task Insert_T(IDbConnection connection, IDbTransaction transaction, EntryPoint model);
    Task Update(EntryPoint model);
}
