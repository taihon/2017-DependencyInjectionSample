using Dapper;
using System.Threading.Tasks;
using TasksManager.DataAccess.Projects;
using TasksManager.ViewModel.Projects;

namespace TasksManager.DataAccess.Dapper.Projects
{
    internal class ProjectQuery : IProjectQuery
    {
        private IConnectionFactory ConnectionFactory { get; }

        public ProjectQuery(IConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory;
        }

        public async Task<ProjectResponse> RunAsync(int projectId)
        {
            var sql = @"select p.Id, p.Name, p.Description, (
                        select count(*)
                        from tasks as t
                        where t.Status <> @Completed and p.Id = t.ProjectId
                    )
                    from projects AS p
                    where p.Id = @ProjectId";
            using (var connection = ConnectionFactory.GetOpenedConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<ProjectResponse>(sql, 
                    new { ProjectId = projectId, Completed = Entities.TaskStatus.Completed });
            }
        }
    }
}
