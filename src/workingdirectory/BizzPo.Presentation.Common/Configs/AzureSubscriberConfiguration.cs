using BizzPo.Core.Application;
using BizzPo.Core.Domain;
using BizzPo.Core.Infrastructure.Messaging.AzureServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BizzPo.Presentation.Common.Configs
{
    public static class AzureSubscriberConfiguration
    {
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