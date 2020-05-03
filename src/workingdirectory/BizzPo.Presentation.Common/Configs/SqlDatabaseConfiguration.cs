using BizzPo.Core.Domain;
using BizzPo.Core.Infrastructure.Repository.EntityFramework;
using BizzPo.Domain.Contacts;
using BizzPo.Infrastructure.Repositories;
using BizzPo.Infrastructure.Repositories.Ef;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BizzPo.Presentation.Common.Configs
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