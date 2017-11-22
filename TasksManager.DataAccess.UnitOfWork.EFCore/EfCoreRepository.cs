using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using TasksManager.Entities;
using System.Threading.Tasks;

namespace TasksManager.DataAccess.UnitOfWork.EFCore
{
    internal class EfCoreRepository<TEntity> : IRepository<TEntity>
        where TEntity : DomainObject
    {
        protected DbSet<TEntity> DbSet { get; }

        public EfCoreRepository(DbSet<TEntity> dbset)
        {
            DbSet = dbset ?? throw new ArgumentNullException(nameof(dbset));
        }

        public IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includes)
        {
            return ApplyIncludes(includes, DbSet);
        }

        private static IQueryable<TEntity> ApplyIncludes(Expression<Func<TEntity, object>>[] includes, IQueryable<TEntity> query)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query;
        }

        public IQueryable<TEntity> NoTrackingQuery(params Expression<Func<TEntity, object>>[] includes)
        {
            return ApplyIncludes(includes, DbSet.AsNoTracking());
        }

        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public Task<TEntity> FirstOrDefaultAsync(IQueryable<TEntity> query, Expression<Func<TEntity, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
