using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BizzPo.Domain.Contacts;
using BizzPo.Domain.Contacts.Seedwork;
using Microsoft.Extensions.Logging;

namespace BizzPo.Infrastructure.Repositories
{
    public class InMemoryContactRepository : IContactRepository
    {
        private readonly ILogger<InMemoryContactRepository> _logger;

        public InMemoryContactRepository(ILogger<InMemoryContactRepository> logger)
        {
            _logger = logger;
        }

        public Task InsertAsync(Contact contact)
        {
            _logger.LogInformation($"Contact {contact.Id} has been inserted to database");
            return Task.CompletedTask;
        }

        public Task SaveAsync(Contact contact)
        {
            _logger.LogInformation($"Contact {contact.Id} has been saved to database");
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Contact contact)
        {
            _logger.LogInformation($"Contact {contact.Id} has been deleted from database");
            return Task.CompletedTask;
        }

        public Task<Contact> GetAsync(Guid id)
        {
            _logger.LogInformation($"Contact {id} has been retrieved from database");
            return Task.FromResult(new Contact(
                $"{id}@example.com",
                $"{id}-firstname",
                $"{id}-lastname"));
        }

        public Task<Contact> GetByAsync(Expression<Func<Contact, bool>> filter)
        {
            _logger.LogInformation("Contact has been retrieved from database with custom filter");

            var id = Guid.NewGuid();
            return Task.FromResult(new Contact(
                $"{id}@example.com",
                $"{id}-firstname",
                $"{id}-lastname"));
        }

        public Task<bool> IsAnyAsync(Expression<Func<Contact, bool>> filter)
        {
            _logger.LogInformation("Contact is found from database with custom filter");
            return Task.FromResult(true);
        }
    }
}