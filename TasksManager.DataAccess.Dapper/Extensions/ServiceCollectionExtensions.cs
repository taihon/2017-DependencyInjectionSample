using Microsoft.Extensions.DependencyInjection;
using TasksManager.DataAccess.Dapper.Projects;
using TasksManager.DataAccess.Projects;

namespace TasksManager.DataAccess.Dapper.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDapperDataAccess(this IServiceCollection services, string connectionString)
        {
            return
                services
                    .AddScoped<IConnectionFactory>(factory => new SqlConnectionFactory(connectionString))
                    .AddScoped<IProjectQuery, ProjectQuery>();
        }
    }
}
