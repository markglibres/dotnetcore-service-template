using System;
using System.Threading;
using System.Threading.Tasks;
using BizzPo.Core.Application;

namespace BizzPo.Infrastructure.AzurePubSub
{
    //do your own implementation..
    //or you can inherit / user from
    //BizzPo.Core.Infrastructure.Messaging.AzureServiceBus.EventPublisherService
    public class EventSubscriberService<T> : IIntegrationEventSubscriberService<T>
        where T : IIntegrationEvent
    {
        public Task SubscribeAndExecute(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }

        public Task CloseAsync()
        {
            throw new NotImplementedException();
        }
    }
}