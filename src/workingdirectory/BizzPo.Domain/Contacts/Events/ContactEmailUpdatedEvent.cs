using System;
using BizzPo.Domain.Seedwork;
using MediatR;
using Newtonsoft.Json;

namespace BizzPo.Domain.Contacts.Events
{
    public class ContactEmailUpdatedEvent : IEvent, INotification
    {
        public Guid ContactId { get; private set; }
        public string Email { get; private set; }
        public string Id { get; private set; }

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
    }
}
