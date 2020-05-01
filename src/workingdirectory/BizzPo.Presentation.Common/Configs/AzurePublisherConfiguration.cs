using BizzPo.Application.Seedwork;
using BizzPo.Infrastructure.AzurePubSub;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BizzPo.Presentation.Common.Configs
{
    public static class AzurePublisherConfiguration
    {
        public static void AddPublishEvent<T>(
            this IServiceCollection services,
            string connectionString,
            string topic)
            where T : IIntegrationEvent
        {
            services.AddTransient<
                IIntegrationEventPublisherService<T>,
                AzureServiceBusPublisherService<T>>(provider =>
            {
                var logger = provider.GetService<ILogger<T>>();
                return new AzureServiceBusPublisherService<T>(
                    logger,
                    connectionString,
                    topic);
            });
        }
    }
}