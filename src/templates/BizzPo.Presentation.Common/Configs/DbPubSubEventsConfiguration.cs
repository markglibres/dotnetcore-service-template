using System;
using BizzPo.Application.Integration.Publish;
using BizzPo.Application.Integration.Subscribe.AccountCreated;
using BizzPo.Core.Application;
using BizzPo.Core.Domain;
using BizzPo.Infrastructure.SqlPubSub;
using BizzPo.Infrastructure.SqlPubSub.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace BizzPo.Presentation.Common.Configs
{
    public static class DbPubSubEventsConfiguration
    {
        public static void AddDbPublishEvents(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionStringSection = configuration.GetSection("ConnectionStrings");
            var contactEventTopic = configuration.GetValue<string>("Events:Publish:ContactAddedEvent:Topic");

            services.AddDbContext<IntegrationEventDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IntegrationEventsDb")));
            services.AddTransient<IRepository<PubSubEvent>, IntegrationEventRepository>();

            services.AddDbPublishEvent<ContactAddedEvent>(contactEventTopic);
        }

        private static void AddDbPublishEvent<T>(
            this IServiceCollection services,
            string topic)
            where T : IIntegrationEvent
        {
            services.AddTransient<
                IIntegrationEventPublisherService<T>,
                DbEventPublisherService<T>>(provider =>
            {
                var logger = provider.GetService<ILogger<DbEventPublisherService<T>>>();
                var repo = provider.GetService<IRepository<PubSubEvent>>();

                return new DbEventPublisherService<T>(
                    logger,
                    repo,
                    topic);
            });
        }

        public static void AddDbSubscribeEvents(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionStringSection = configuration.GetSection("ConnectionStrings");
            var azureServiceBusConnectionString = connectionStringSection["AzureServiceBus"];
            var accountCreatedEventSection = configuration.GetSection("Events:Subscribe:AccountCreatedEvent");

            services.AddSubscribeEvent<AccountCreatedEvent>(
                accountCreatedEventSection["Topic"],
                Convert.ToInt32(accountCreatedEventSection["MaxConcurrentCalls"]));
        }

        private static void AddSubscribeEvent<T>(
            this IServiceCollection services,
            string topic,
            int maxConcurrentCalls = 100)
            where T : IIntegrationEvent
        {
            services.AddTransient<
                IIntegrationEventSubscriberService<T>,
                DbEventSubscriberService<T>>(provider =>
            {
                var logger = provider.GetService<ILogger<DbEventSubscriberService<T>>>();
                var repo = provider.GetService<IRepository<PubSubEvent>>();
                var domainEventsService = provider.GetService<IDomainEventsService>();

                return new DbEventSubscriberService<T>(
                    logger,
                    repo,
                    domainEventsService,
                    topic,
                    maxConcurrentCalls);
            });
        }
    }
}