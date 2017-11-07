using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TasksManager.DataAccess.Projects;
using TasksManager.DataAccess.UnitOfWork;
using TasksManager.ViewModel.Projects;

namespace TasksManager.DataAccess.DbImplementation.Projects
{
    internal class ProjectQuery : IProjectQuery
    {
        private IUnitOfWork Uow { get; }

        public ProjectQuery(IUnitOfWork uow)
        {
            Uow = uow;
        }

        public async Task<ProjectResponse> RunAsync(int projectId)
        {
            ProjectResponse response = await Uow.ProjectsRepository.Query()
                .Select(p => new ProjectResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    OpenTasksCount = p.Tasks.Count(t => t.Status != Entities.TaskStatus.Completed)
                })
                .FirstOrDefaultAsync(pr => pr.Id == projectId);

            return response;
        }
    }
}
