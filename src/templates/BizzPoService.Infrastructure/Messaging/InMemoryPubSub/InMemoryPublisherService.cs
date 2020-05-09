using System.Threading;
using System.Threading.Tasks;
using BizzPo.Core.Application;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BizzPoService.Infrastructure.Messaging.InMemoryPubSub
{
    public class InMemoryPublisherService<T> : IIntegrationEventPublisherService<T>
        where T : IIntegrationEvent
    {
        private readonly ILogger<InMemoryPublisherService<T>> _logger;

        public InMemoryPublisherService(
            ILogger<InMemoryPublisherService<T>> logger)
        {
            _logger = logger;
        }

        public Task Publish(T @event, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Integration event has been received. {JsonConvert.SerializeObject(@event)}");
            return Task.CompletedTask;
        }
    }
}