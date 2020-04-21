using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BizzPo.Domain.Contacts.Seedwork
{
    public interface IContactRepository
    {
        Task InsertAsync(Contact contact);
        Task SaveAsync(Contact contact);
        Task DeleteAsync(Contact contact);
        Task<Contact> GetAsync(Guid id);
        Task<Contact> GetByAsync(Expression<Func<Contact, bool>> filter);
        Task<bool> IsAnyAsync(Expression<Func<Contact, bool>> filter);
    }
}