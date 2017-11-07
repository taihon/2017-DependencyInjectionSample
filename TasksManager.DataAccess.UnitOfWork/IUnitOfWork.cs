using System.Threading.Tasks;
using TasksManager.Entities;

namespace TasksManager.DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        void Migrate();

        IRepository<Project> ProjectsRepository { get; }

        #region Sync

        int Commit();

        #endregion

        #region Async

        Task<int> CommitAsync();

        #endregion
    }
}
