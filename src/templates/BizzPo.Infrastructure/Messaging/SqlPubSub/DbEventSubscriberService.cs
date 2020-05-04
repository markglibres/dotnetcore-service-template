using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BizzPo.Core.Application;
using BizzPo.Core.Domain;
using BizzPo.Infrastructure.SqlPubSub.Database;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BizzPo.Infrastructure.SqlPubSub
{
    public class DbEventSubscriberService<T> : IIntegrationEventSubscriberService<T>
        where T : IIntegrationEvent
    {
        private readonly IDomainEventsService _domainEventsService;
        private readonly ILogger<DbEventSubscriberService<T>> _logger;
        private readonly IRepository<PubSubEvent> _repository;
        private readonly string _topic;
        private readonly int _maxConcurrentCalls;

        public DbEventSubscriberService(
            ILogger<DbEventSubscriberService<T>> logger,
            IRepository<PubSubEvent> repository,
            IDomainEventsService domainEventsService,
            string topic,
            int maxConcurrentCalls)
        {
            _logger = logger;
            _repository = repository;
            _domainEventsService = domainEventsService;
            _topic = topic;
            _maxConcurrentCalls = maxConcurrentCalls;
        }

        public async Task SubscribeAndExecute(CancellationToken stoppingToken)
        {
            try
            {
                var messageType = typeof(T).Name;
                var messages = (await _repository.GetByAsync(e => e
                        .Where(r =>
                            r.MessageType.ToLower() == messageType.ToLower()
                            && r.Topic.ToLower() == _topic.ToLower())
                        .OrderByDescending(r => r.DateCreated)
                        .Take(_maxConcurrentCalls)))
                    .ToList();

                var tasks = new List<Task>();

                messages
                    .ForEach(message =>
                    {
                        var messageBody = JsonConvert.SerializeObject(message.Message);
                        tasks.Add(
                            _domainEventsService
                                .Publish(JsonConvert.DeserializeObject<T>(messageBody), stoppingToken));
                    });

                await Task.WhenAll(tasks);

                tasks.Clear();
                messages.ForEach(message => { tasks.Add(_repository.DeleteAsync(message)); });

                await Task.WhenAll(tasks);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error subscribing to events: {e.Message}");
                throw;
            }
        }

        public Task CloseAsync()
        {
            return Task.CompletedTask;
        }
    }
}