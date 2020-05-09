using BizzPo.Core.Application;
using BizzPoService.Application.Integration.Publish;
using BizzPoService.Application.Integration.Subscribe.AccountCreated;
using BizzPoService.Infrastructure.Messaging.InMemoryPubSub;
using Microsoft.Extensions.DependencyInjection;

namespace BizzPoService.Presentation.Common.Configs
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