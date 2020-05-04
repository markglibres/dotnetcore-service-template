using BizzPo.Core.Domain;
using BizzPo.Domain.Contacts;
using BizzPo.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BizzPo.Presentation.Common.Configs
{
    public static class InMemoryDatabaseConfiguration
    {
        public static void AddInMemoryDatabase(this IServiceCollection services)
        {
            services.AddTransient<IRepository<Contact>, InMemoryContactRepository>();
        }
    }
}