using System;
using System.Threading.Tasks;

namespace BizzPo.Domain.Contacts.Seedwork
{
    public interface IContactService
    {
        Task<Contact> Create(string email, string firstname, string lastname);
        Task<Contact> GetById(Guid id);
        Task<Contact> UpdateEmail(Guid id, string email);
    }
}