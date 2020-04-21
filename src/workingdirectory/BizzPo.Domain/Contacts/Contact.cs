using System;
using BizzPo.Domain.Contacts.Events;
using BizzPo.Domain.Seedwork;
using Newtonsoft.Json;

namespace BizzPo.Domain.Contacts
{
    public class Contact : Entity
    {
        public string Email { get; private set; }
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }

        [JsonConstructor]
        private Contact()
        {
        }

        public Contact(
            string email, 
            string firstname,
            string lastname) 
        {
            Email = email;
            Firstname = firstname;
            Lastname = lastname;

            Emit(new ContactCreatedEvent(Id));
        }

        public void SetEmail(string email)
        {
            Email = email;
            Emit(new ContactEmailUpdatedEvent(Id, email));
        }

    }
}