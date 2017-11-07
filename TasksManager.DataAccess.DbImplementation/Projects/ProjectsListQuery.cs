using System;
using System.Threading.Tasks;
using TasksManager.DataAccess.Projects;
using TasksManager.ViewModel;
using TasksManager.ViewModel.Projects;

namespace TasksManager.DataAccess.DbImplementation.Projects
{
    internal class ProjectsListQuery : IProjectsListQuery
    {
        public Task<ListResponse<ProjectResponse>> RunAsync(ProjectFilter filter, ListOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
