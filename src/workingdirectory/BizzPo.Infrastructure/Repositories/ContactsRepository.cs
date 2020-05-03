using BizzPo.Core.Infrastructure.Repository.EntityFramework;
using BizzPo.Domain.Contacts;
using BizzPo.Infrastructure.Repositories.Ef;
using Microsoft.EntityFrameworkCore;

namespace BizzPo.Infrastructure.Repositories
{
    //do your own implementation of IRepository<T>
    //or you can inherit / use from
    //BizzPo.Core.Infrastructure.Repository.EntityFramework.SqlRepository
    public class ContactsRepository : SqlRepository<Contact>
    {
        public ContactsRepository(ContactsDbContext dbContext) : base(dbContext)
        {
        }
    }
}