using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TasksManager.DataAccess.UnitOfWork.EFCore
{
    public class AsyncQueryable<T> : IAsyncQueryable<T>
    {
        public async Task<T> FirstOrDefaultAsync()
        {
            return await _query.FirstOrDefaultAsync();
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _query.FirstOrDefaultAsync(predicate);
        }
        private IQueryable<T> _query;
        public AsyncQueryable(IQueryable<T> query)
        {
            _query = query;
        }
    }
}
