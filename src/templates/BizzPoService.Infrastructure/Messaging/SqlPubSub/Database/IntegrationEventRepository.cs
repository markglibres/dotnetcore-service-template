using BizzPo.Core.Infrastructure.Repository.EntityFramework;

namespace BizzPoService.Infrastructure.Messaging.SqlPubSub.Database
{
    public class IntegrationEventRepository : SqlRepository<PubSubEvent>
    {
        public IntegrationEventRepository(IntegrationEventDbContext dbContext) 
            : base(dbContext)
        {
        }
    }
}