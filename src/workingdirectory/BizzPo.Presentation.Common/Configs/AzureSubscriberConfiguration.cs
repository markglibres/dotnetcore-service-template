using BizzPo.Application.Seedwork;
using BizzPo.Domain.Seedwork;
using BizzPo.Infrastructure.AzurePubSub;
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
                AzureServiceBusSubscriberService<T>>(provider =>
            {
                var logger = provider.GetService<ILogger<T>>();
                var domainEventsService = provider.GetService<IDomainEventsService>();

                return new AzureServiceBusSubscriberService<T>(
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