using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BizzPo.Core.Domain
{
    public interface IRepository<T>
        where T : IEntity
    {
        Task InsertAsync(T entity);
        Task SaveAsync(T entity);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> filter);
        IQueryable<T> GetAll();
        Task<IEnumerable<T>> GetByAsync(Func<IQueryable<T>, IQueryable<T>> filter);
        Task DeleteAsync(T entity);
    }
}