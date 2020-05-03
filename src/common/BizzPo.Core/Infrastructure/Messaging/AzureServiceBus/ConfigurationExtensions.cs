using BizzPo.Core.Application;
using BizzPo.Core.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BizzPo.Core.Infrastructure.Messaging.AzureServiceBus
{
    public static class ConfigurationExtensions
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

        public static void AddSubscribeEvent<T>(
            this IServiceCollection services,
            string connectionString,
            string topic,
            string subscription,
            int maxConcurrentCalls = 100)
            where T : IIntegrationEvent
        {
            services.AddTransient<
                IIntegrationEventSubscriberService<T>,
                EventSubscriberService<T>>(provider =>
            {
                var logger = provider.GetService<ILogger<T>>();
                var domainEventsService = provider.GetService<IDomainEventsService>();

                return new EventSubscriberService<T>(
                    logger,
                    domainEventsService,
                    connectionString,
                    topic,
                    subscription,
                    maxConcurrentCalls);
            });
        }
    }
}