using BizzPo.Core.Infrastructure.Repository.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BizzPo.Infrastructure.SqlPubSub.Database
{
    public class EventConfiguration : IEntityTypeConfiguration<PubSubEvent>
    {
        public void Configure(EntityTypeBuilder<PubSubEvent> builder)
        {
            builder
                .Property(t => t.Message)
                .HasConversion(new DynamicPropertyValueConverter());
            builder.Property(t => t.DateCreated)
                .HasConversion(new DateTimeValueConverter());
        }
    }
}