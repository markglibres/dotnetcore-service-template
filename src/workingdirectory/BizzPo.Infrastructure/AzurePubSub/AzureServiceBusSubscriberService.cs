using System.Threading;
using System.Threading.Tasks;
using BizzPo.Core.Application;
using BizzPo.Core.Domain;
using BizzPo.Core.Infrastructure.Messaging.AzureServiceBus;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;

namespace BizzPo.Infrastructure.AzurePubSub
{
    //do your own implementation of IIntegrationEventSubscriberService<T>
    //or you can inherit / use from
    //BizzPo.Core.Infrastructure.Messaging.AzureServiceBus.EventSubscriberService
    public class AzureServiceBusSubscriberService<T> : EventSubscriberService<T>
        where T : IIntegrationEvent
    {
        public AzureServiceBusSubscriberService(
            ILogger<T> logger,
            IDomainEventsService domainEventsService,
            string connectionStrings,
            string topic,
            string subscription,
            int maxConcurrentCalls = 100)
            : base(logger, domainEventsService, connectionStrings,
                topic, subscription, maxConcurrentCalls)
        {
        }
    }
}