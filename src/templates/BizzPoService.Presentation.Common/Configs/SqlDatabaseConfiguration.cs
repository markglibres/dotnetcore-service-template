using BizzPo.Core.Domain;
using BizzPoService.Domain.Contacts;
using BizzPoService.Infrastructure.Repositories;
using BizzPoService.Infrastructure.Repositories.EfSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BizzPoService.Presentation.Common.Configs
{
    public static class SqlDatabaseConfiguration
    {
        public static void AddSqlDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ContactsDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IRepository<Contact>, ContactsRepository>();
        }
    }
}