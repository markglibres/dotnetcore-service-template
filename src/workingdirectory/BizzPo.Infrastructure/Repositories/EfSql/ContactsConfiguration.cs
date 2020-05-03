using BizzPo.Core.Infrastructure.Repository.EntityFramework;
using BizzPo.Domain.Contacts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BizzPo.Infrastructure.Repositories.Ef
{
    public class ContactsConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder
                .Property(t => t.Profile)
                .HasConversion(new DynamicPropertyValueConverter());
            builder.Property(t => t.DateCreated)
                .HasConversion(new DateTimeValueConverter());
            builder.Property(t => t.ContactType)
                .HasConversion(new EnumToStringConverter<ContactTypes>())
                .HasColumnType("varchar(25)");
            builder.Property(t => t.Salary)
                .HasColumnType("decimal(18,4)");
            builder.Ignore(t => t.Events);
        }
    }
}