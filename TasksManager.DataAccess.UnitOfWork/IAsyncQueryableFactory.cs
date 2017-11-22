using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TasksManager.DataAccess.UnitOfWork
{
    public interface IAsyncQueryableFactory
    {
        IAsyncQueryable<TEntity> GetAsyncQueryable<TEntity>(IQueryable<TEntity> query);
    }
}
