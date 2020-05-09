using System;
using System.Threading;
using System.Threading.Tasks;
using BizzPo.Core.Application;
using BizzPo.Core.Domain;
using BizzPoService.Infrastructure.Messaging.SqlPubSub.Database;
using Microsoft.Extensions.Logging;

namespace BizzPoService.Infrastructure.Messaging.SqlPubSub
{
    public class DbEventPublisherService<T> : IIntegrationEventPublisherService<T>
        where T : IIntegrationEvent

    {
        private readonly ILogger<DbEventPublisherService<T>> _logger;
        private readonly IRepository<PubSubEvent> _repository;
        private readonly string _topic;

        public DbEventPublisherService(
            ILogger<DbEventPublisherService<T>> logger,
            IRepository<PubSubEvent> repository,
            string topic)
        {
            _logger = logger;
            _repository = repository;
            _topic = topic;
        }

        public async Task Publish(T @event, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Publishing integration event to database");
            var message = new PubSubEvent
            {
                DateCreated = DateTime.Now,
                Id = Guid.NewGuid(),
                MessageType = @event.GetType().Name,
                Message = @event,
                Topic = _topic
            };

            await _repository.InsertAsync(message);
            _logger.LogInformation($"Integration event saved to database");
        }
    }
}