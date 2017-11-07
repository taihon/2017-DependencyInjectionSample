using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TasksManager.Db;
using TasksManager.Entities;

namespace TasksManager.DataAccess.UnitOfWork.EFCore
{
    internal class EFCoreUnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly TasksContext _context;

        private IRepository<Project> _projectRepository;

        public EFCoreUnitOfWork(TasksContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #region Sync
        public int Commit()
        {
            return _context.SaveChanges();
        }
        #endregion

        #region Async
        public Task<int> CommitAsync()
        {
            return _context.SaveChangesAsync();
        }
        #endregion

        public void Migrate()
        {
            _context.Database.Migrate();
        }

        public IRepository<Project> ProjectsRepository => _projectRepository ??
            (_projectRepository = new EfCoreRepository<Project>(_context.Projects));

        #region Disposable implementation

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                _context.Dispose();
            }

            disposed = true;
        }
        #endregion
    }
}
