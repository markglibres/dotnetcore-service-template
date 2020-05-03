using BizzPo.Core.Application;
using BizzPo.Core.Infrastructure.Messaging.AzureServiceBus;
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
                EventPublisherService<T>>(provider =>
            {
                var logger = provider.GetService<ILogger<T>>();
                return new EventPublisherService<T>(
                    logger,
                    connectionString,
                    topic);
            });
        }
    }
}