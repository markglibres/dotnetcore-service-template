using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using BizzPo.Domain.Contacts.Seedwork;
using BizzPo.Domain.Extensions;
using BizzPo.Domain.Seedwork;
using Microsoft.Extensions.Logging;

namespace BizzPo.Domain.Contacts
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IDomainEventsService _domainEventsService;
        private readonly ILogger<ContactService> _logger;

        public ContactService(
            ILogger<ContactService> logger,
            IDomainEventsService domainEventsService,
            IContactRepository contactRepository)
        {
            _logger = logger;
            _domainEventsService = domainEventsService;
            _contactRepository = contactRepository;
        }

        public async Task<Contact> Create(string email, string firstname, string lastname)
        {
            Guard.Against.Empty<ContactsException>(email, "email");
            Guard.Against.Empty<ContactsException>(firstname, "firstname");
            Guard.Against.Empty<ContactsException>(lastname, "lastname");

            var contact = new Contact(email, firstname, lastname);

            await _contactRepository.InsertAsync(contact);
            await _domainEventsService.Publish(contact.Events, CancellationToken.None);

            return contact;
        }

        public async Task<Contact> GetById(Guid id)
        {
            Guard.Against.Empty<ContactsException>(id, "id");

            var contact = await _contactRepository.GetAsync(id);
            return contact;
        }

        public async Task<Contact> UpdateEmail(Guid id, string email)
        {
            Guard.Against.Empty<ContactsException>(id, "id");
            Guard.Against.Empty<ContactsException>(email, "email");

            var contact = await _contactRepository.GetAsync(id);

            if (contact == null) throw new ContactsException($"Cannot find contact {id}");

            contact.SetEmail(email);

            await _contactRepository.SaveAsync(contact);
            await _domainEventsService.Publish(contact.Events, CancellationToken.None);

            return contact;
        }
    }
}