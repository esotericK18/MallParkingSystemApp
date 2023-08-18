
using System.Data;
using DataAccess.Helpers;

namespace DataAccess.DbAccess;

public interface IRDBMDataAccess
{
    Task<IEnumerable<T>> LoadComplexDataTransact<T, U>(IDbConnection db, IDbTransaction transaction, string storedProcedure, U parameters);
    Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, CRUD_TYPE crudType, U parameters, string connectionId = "Default");
    Task<IEnumerable<T>> LoadDataTransact<T, U>(IDbConnection db, IDbTransaction transaction, string storedProcedure, CRUD_TYPE crudType, U parameters);
    Task SaveData<T>(string storedProcedure, CRUD_TYPE crudType, T parameters, string connectionId = "Default");
    Task SaveDataTransact<T>(IDbConnection db, IDbTransaction transaction, string storedProcedure, CRUD_TYPE crudType, T parameters);
}
