using BizzPo.Domain.Contacts;
using Microsoft.EntityFrameworkCore;

namespace BizzPo.Infrastructure.Repositories.Ef
{
    public class ContactsDbContext : DbContext
    {
        public ContactsDbContext(DbContextOptions contextOptions)
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