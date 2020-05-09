using System;
using System.Threading;
using System.Threading.Tasks;
using BizzPo.Core.Application;
using BizzPo.Core.Domain;
using Microsoft.Extensions.Logging;

namespace BizzPoService.Infrastructure.Messaging.InMemoryPubSub
{
    public class InMemorySubscriberService<T> : IIntegrationEventSubscriberService<T>
        where T : IIntegrationEvent
    {
        private readonly IDomainEventsService _domainEventsService;
        private readonly ILogger<InMemorySubscriberService<T>> _logger;

        public InMemorySubscriberService(
            ILogger<InMemorySubscriberService<T>> logger,
            IDomainEventsService domainEventsService)
        {
            _logger = logger;
            _domainEventsService = domainEventsService;
        }

        public async Task SubscribeAndExecute(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Subscribing random events");
            var message = Activator.CreateInstance<T>();
            message.Id = Guid.NewGuid().ToString();

            await _domainEventsService.Publish(message, stoppingToken);
            _logger.LogInformation("Subscribing random events completed");

            await Task.Delay(5000, stoppingToken);
        }

        public Task CloseAsync()
        {
            _logger.LogInformation("Closing event subscription");
            return Task.CompletedTask;
        }
    }
}