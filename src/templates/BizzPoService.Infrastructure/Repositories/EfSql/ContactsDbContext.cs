using BizzPoService.Domain.Contacts;
using Microsoft.EntityFrameworkCore;

namespace BizzPoService.Infrastructure.Repositories.EfSql
{
    public class ContactsDbContext : DbContext
    {
        public ContactsDbContext(DbContextOptions<ContactsDbContext> contextOptions)
            : base(contextOptions)
        {
        }

        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //apply table configurations, i.e. ContactsConfiguration.cs
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}