using System.Data;
using System.Data.SqlClient;

namespace TasksManager.DataAccess.Dapper
{
    internal class SqlConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString;
        public SqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection GetOpenedConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
