using BizzPo.Application.Integration.Publish;
using BizzPo.Application.Integration.Subscribe.AccountCreated;
using BizzPo.Core.Application;
using BizzPo.Core.Domain;
using BizzPo.Core.Infrastructure.Messaging.AzureServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BizzPo.Presentation.Common.Configs
{
    public static class AzurePubSubConfiguration
    {
        public static void AddAzurePublishEvents(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionStringSection = configuration.GetSection("ConnectionStrings");
            var azureServiceBusConnectionString = connectionStringSection["AzureServiceBus"];
            var contactEventTopic = configuration.GetValue<string>("Events:Publish:ContactAddedEvent:Topic");

            services.AddPublishEvent<ContactAddedEvent>(azureServiceBusConnectionString, contactEventTopic);

        }

        private static void AddPublishEvent<T>(
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

        public static void AddAzureSubscribeEvents(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionStringSection = configuration.GetSection("ConnectionStrings");
            var azureServiceBusConnectionString = connectionStringSection["AzureServiceBus"];
            var accountCreatedEventSection = configuration.GetSection("Events:Subscribe:AccountCreatedEvent");

            services.AddSubscribeEvent<AccountCreatedEvent>(azureServiceBusConnectionString,
                accountCreatedEventSection["Topic"],
                accountCreatedEventSection["Subscription"]);
        }

        private static void AddSubscribeEvent<T>(
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