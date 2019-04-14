using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Checkout.Orders.Infrastructure
{
    public interface IDatabase
    {
        Task<T> ExecuteOnConnection<T>(Func<IDbConnection, Task<T>> operation);
    }

    public class SqlDatabase : IDatabase
    {
        private readonly string _connectionString;

        public SqlDatabase(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<T> ExecuteOnConnection<T>(Func<IDbConnection, Task<T>> operation)
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                return await operation.Invoke(dbConnection);
            }
        }
    }
}