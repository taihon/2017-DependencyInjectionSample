using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TasksManager.Db;

namespace TasksManager.DataAccess.UnitOfWork.EFCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterUnitOfWorkEFCore(this IServiceCollection services, string connectionString)
        {
            return
                services
                    .AddDbContext<TasksContext>(options => options.UseSqlServer(connectionString))
                    .AddScoped<IUnitOfWork, EFCoreUnitOfWork>();
        }
    }
}
