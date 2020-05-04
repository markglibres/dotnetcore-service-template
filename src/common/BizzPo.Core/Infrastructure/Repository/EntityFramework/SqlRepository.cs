using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BizzPo.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace BizzPo.Core.Infrastructure.Repository.EntityFramework
{
    public class SqlRepository<T> : IRepository<T>
        where T : class, IEntity
    {
        protected readonly DbContext DbContext;

        public SqlRepository(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual IQueryable<T> GetAll()
        {
            return DbContext.Set<T>().AsQueryable();
        }

        public virtual async Task<IEnumerable<T>> GetByAsync(Func<IQueryable<T>, IQueryable<T>> filter)
        {
            var filtered = filter(DbContext.Set<T>());
            return await filtered.ToListAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            DbContext.Set<T>().Remove(entity);
            await DbContext.SaveChangesAsync();
        }

        public virtual async Task<T> GetSingleAsync(Expression<Func<T, bool>> filter)
        {
            return await DbContext.Set<T>().FirstOrDefaultAsync(filter);
        }

        public virtual async Task InsertAsync(T entity)
        {
            await DbContext.Set<T>().AddAsync(entity);
            await DbContext.SaveChangesAsync();
        }

        public virtual async Task SaveAsync(T entity)
        {
            DbContext.Set<T>().Update(entity);
            await DbContext.SaveChangesAsync();
        }
    }
}