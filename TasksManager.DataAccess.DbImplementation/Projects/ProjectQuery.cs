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
        private IAsyncQueryableFactory Factory { get; }

        public ProjectQuery(IUnitOfWork uow, IAsyncQueryableFactory factory)
        {
            Uow = uow;
            Factory = factory;
        }

        public async Task<ProjectResponse> RunAsync(int projectId)
        {
            var response = Uow.ProjectsRepository.Query()
                .Select(p => new ProjectResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    OpenTasksCount = p.Tasks.Count(t => t.Status != Entities.TaskStatus.Completed)
                })
            ;

            return await Factory.GetAsyncQueryable(response).FirstOrDefaultAsync(p => p.Id == projectId);
        }
    }
}
