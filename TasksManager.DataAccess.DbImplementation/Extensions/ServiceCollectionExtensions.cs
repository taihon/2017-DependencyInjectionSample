using Microsoft.Extensions.DependencyInjection;
using TasksManager.DataAccess.DbImplementation.Projects;
using TasksManager.DataAccess.Projects;

namespace TasksManager.DataAccess.DbImplementation.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterUnitOfWorkDataAccess(this IServiceCollection services)
        {
            return 
                services
                    .AddScoped<IProjectsListQuery, ProjectsListQuery>()
                    .AddScoped<IProjectQuery, ProjectQuery>()
                    .AddScoped<ICreateProjectCommand, CreateProjectCommand>();
        }
    }
}
