using BizzPo.Core.Domain;
using BizzPoService.Domain.Contacts;
using BizzPoService.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BizzPoService.Presentation.Common.Configs
{
    public static class InMemoryDatabaseConfiguration
    {
        public static void AddInMemoryDatabase(this IServiceCollection services)
        {
            services.AddTransient<IRepository<Contact>, InMemoryContactRepository>();
        }
    }
}