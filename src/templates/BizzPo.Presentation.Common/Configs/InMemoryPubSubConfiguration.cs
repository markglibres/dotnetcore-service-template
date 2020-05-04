using BizzPo.Application.Integration.Publish;
using BizzPo.Application.Integration.Subscribe.AccountCreated;
using BizzPo.Core.Application;
using BizzPo.Infrastructure.Messaging.InMemoryPubSub;
using BizzPo.Infrastructure.SqlPubSub;
using Microsoft.Extensions.DependencyInjection;

namespace BizzPo.Presentation.Common.Configs
{
    public static class InMemoryPubSubConfiguration
    {
        public static void AddInMemoryPublishEvents(this IServiceCollection services)
        {
            services
                .AddTransient<IIntegrationEventPublisherService<ContactAddedEvent>,
                    InMemoryPublisherService<ContactAddedEvent>>();
        }

        public static void AddInMemorySubscribeEvents(this IServiceCollection services)
        {
            services.AddTransient<IIntegrationEventSubscriberService<AccountCreatedEvent>,
                InMemorySubscriberService<AccountCreatedEvent>>();
        }
    }
}