using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BizzPo.Core.Domain;

namespace BizzPo.Core.Infrastructure.Repository.InMemory
{
    public class InMemoryRepository<T> : IRepository<T>
        where T : class, IEntity
    {
        private IQueryable<T> _inMemoryRecords;

        public InMemoryRepository()
        {
            _inMemoryRecords = new EnumerableQuery<T>(new List<T>());
        }

        public Task InsertAsync(T entity)
        {
            var records = _inMemoryRecords.ToList();
            records.Add(entity);

            _inMemoryRecords = records.AsQueryable();
            return Task.CompletedTask;
        }

        public Task SaveAsync(T entity)
        {
            var word = _inMemoryRecords.FirstOrDefault(w => w.Id.Equals(entity.Id));
            if (word == null) return Task.CompletedTask;

            var records = _inMemoryRecords.ToList();
            records.Remove(word);
            records.Add(entity);

            return Task.CompletedTask;
        }

        public Task<T> GetSingleAsync(Expression<Func<T, bool>> filter)
        {
            var response = _inMemoryRecords
                .FirstOrDefault(filter);
            return Task.FromResult(response);
        }

        public IQueryable<T> GetAll()
        {
            return _inMemoryRecords;
        }

        public Task<IEnumerable<T>> GetByAsync(Func<IQueryable<T>, IQueryable<T>> filter)
        {
            var response = filter(_inMemoryRecords);
            return Task.FromResult(response.AsEnumerable());
        }

        public Task DeleteAsync(T entity)
        {
            var records = _inMemoryRecords.ToList();
            records.Remove(entity);

            return Task.CompletedTask;
        }
    }
}