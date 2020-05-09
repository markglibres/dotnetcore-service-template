using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using BizzPo.Core.Domain;
using BizzPoService.Domain.Contacts.Seedwork;
using BizzPoService.Domain.Extensions;
using Microsoft.Extensions.Logging;

namespace BizzPoService.Domain.Contacts
{
    public class ContactService : IContactService
    {
        private readonly IDomainEventsService _domainEventsService;
        private readonly ILogger<ContactService> _logger;
        private readonly IRepository<Contact> _repository;

        public ContactService(
            ILogger<ContactService> logger,
            IDomainEventsService domainEventsService,
            IRepository<Contact> repository)
        {
            _logger = logger;
            _domainEventsService = domainEventsService;
            _repository = repository;
        }

        public async Task<Contact> Create(string email, string firstname, string lastname)
        {
            Guard.Against.Empty<DomainException>(email, "email");
            Guard.Against.Empty<DomainException>(firstname, "firstname");
            Guard.Against.Empty<DomainException>(lastname, "lastname");

            var contact = new Contact(email, firstname, lastname);

            await _repository.InsertAsync(contact);
            await _domainEventsService.Publish(contact.Events, CancellationToken.None);

            return contact;
        }

        public async Task<Contact> GetById(Guid id)
        {
            Guard.Against.Empty<DomainException>(id, "id");

            var contact = await _repository.GetSingleAsync(c => c.Id.Equals(id));
            return contact;
        }

        public async Task<Contact> UpdateEmail(Guid id, string email)
        {
            Guard.Against.Empty<DomainException>(id, "id");
            Guard.Against.Empty<DomainException>(email, "email");

            var contact = await _repository.GetSingleAsync(c => c.Id.Equals(id));

            if (contact == null) throw new DomainException($"Cannot find contact {id}");

            contact.SetEmail(email);

            await _repository.SaveAsync(contact);
            await _domainEventsService.Publish(contact.Events, CancellationToken.None);

            return contact;
        }
    }
}