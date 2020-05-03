using System.Threading;
using System.Threading.Tasks;
using BizzPo.Core.Application;
using BizzPo.Core.Infrastructure.Messaging.AzureServiceBus;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;

namespace BizzPo.Infrastructure.AzurePubSub
{
    //do your own implementation of IIntegrationEventPublisherService<T>
    //or you can inherit / use from
    //BizzPo.Core.Infrastructure.Messaging.AzureServiceBus.EventPublisherService
    public class AzureServiceBusPublisherService<T> : EventPublisherService<T>
        where T : IIntegrationEvent
    {
        public AzureServiceBusPublisherService(
            ILogger<T> logger,
            string connectionString,
            string topic) : base(logger, connectionString, topic)
        {
        }

    }
}