using BizzPo.Core.Infrastructure.Repository.EntityFramework;
using BizzPo.Domain.Contacts;
using Microsoft.EntityFrameworkCore;

namespace BizzPo.Infrastructure.Repositories
{
    //do your own implementation of IRepository<T>
    //or you can inherit / use from
    //BizzPo.Core.Infrastructure.Repository.EntityFramework.SqlRepository
    public class EfCoreSqlRepository : SqlRepository<Contact>
    {
        public EfCoreSqlRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}