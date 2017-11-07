using System.Threading.Tasks;
using TasksManager.DataAccess.Projects;
using TasksManager.DataAccess.UnitOfWork;
using TasksManager.Entities;
using TasksManager.ViewModel.Projects;

namespace TasksManager.DataAccess.DbImplementation.Projects
{
    internal class CreateProjectCommand : ICreateProjectCommand
    {
        private IUnitOfWork Uow { get; }

        public CreateProjectCommand(IUnitOfWork uow)
        {
            Uow = uow;
        }

        public async Task<ProjectResponse> ExecuteAsync(CreateProjectRequest request)
        {
            var project = new Project
            {
                Name = request.Name,
                Description = request.Description
            };

            Uow.ProjectsRepository.Add(project);
            await Uow.CommitAsync();

            return new ProjectResponse
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                OpenTasksCount = 0
            };
        }
    }
}
