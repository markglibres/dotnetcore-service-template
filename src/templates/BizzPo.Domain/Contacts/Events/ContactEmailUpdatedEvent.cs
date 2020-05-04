using System;
using BizzPo.Core.Domain;
using MediatR;
using Newtonsoft.Json;

namespace BizzPo.Domain.Contacts.Events
{
    public class ContactEmailUpdatedEvent : IEvent, INotification
    {
        [JsonConstructor]
        private ContactEmailUpdatedEvent()
        {
        }

        public ContactEmailUpdatedEvent(Guid contactId, string email)
        {
            ContactId = contactId;
            Email = email;
            Id = Guid.NewGuid().ToString();
        }

        public Guid ContactId { get; }
        public string Email { get; }
        public string Id { get; }
    }
}