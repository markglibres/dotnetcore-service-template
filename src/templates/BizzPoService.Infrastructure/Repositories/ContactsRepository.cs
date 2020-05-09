using BizzPo.Core.Infrastructure.Repository.EntityFramework;
using BizzPoService.Domain.Contacts;
using BizzPoService.Infrastructure.Repositories.EfSql;

namespace BizzPoService.Infrastructure.Repositories
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