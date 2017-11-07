using System.Data;

namespace TasksManager.DataAccess.Dapper
{
    internal interface IConnectionFactory
    {
        IDbConnection GetOpenedConnection();
    }
}
