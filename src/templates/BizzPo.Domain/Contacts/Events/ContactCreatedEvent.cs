using System;
using BizzPo.Core.Domain;
using MediatR;
using Newtonsoft.Json;

namespace BizzPo.Domain.Contacts.Events
{
    public class ContactCreatedEvent : IEvent, INotification
    {
        [JsonConstructor]
        private ContactCreatedEvent()
        {
        }

        public ContactCreatedEvent(Guid contactId)
        {
            ContactId = contactId;
            Id = Guid.NewGuid().ToString();
        }

        public Guid ContactId { get; }
        public string Id { get; }
    }
}