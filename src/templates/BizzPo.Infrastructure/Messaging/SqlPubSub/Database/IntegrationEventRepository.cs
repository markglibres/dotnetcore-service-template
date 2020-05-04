using BizzPo.Core.Infrastructure.Repository.EntityFramework;

namespace BizzPo.Infrastructure.SqlPubSub.Database
{
    public class IntegrationEventRepository : SqlRepository<PubSubEvent>
    {
        public IntegrationEventRepository(IntegrationEventDbContext dbContext) 
            : base(dbContext)
        {
        }
    }
}