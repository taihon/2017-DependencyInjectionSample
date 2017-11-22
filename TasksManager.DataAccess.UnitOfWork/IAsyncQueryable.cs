using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TasksManager.DataAccess.UnitOfWork
{
    public interface IAsyncQueryable<TEntity>
    {
        Task<TEntity> FirstOrDefaultAsync();
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    }
}