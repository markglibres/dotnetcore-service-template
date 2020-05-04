using Microsoft.EntityFrameworkCore;

namespace BizzPo.Infrastructure.SqlPubSub.Database
{
    public class IntegrationEventDbContext : DbContext
    {
        public DbSet<PubSubEvent> PubSubEvents { get; set; }
        public IntegrationEventDbContext(DbContextOptions<IntegrationEventDbContext> contextOptions)
            : base(contextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //apply table configurations, i.e. ContactsConfiguration.cs
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}