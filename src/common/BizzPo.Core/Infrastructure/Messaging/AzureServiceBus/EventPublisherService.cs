using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BizzPo.Core.Application;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BizzPo.Core.Infrastructure.Messaging.AzureServiceBus
{
    public class EventPublisherService<T>
        : IIntegrationEventPublisherService<T>
        where T : IIntegrationEvent

    {
        protected readonly ILogger<T> Logger;

        public EventPublisherService(
            ILogger<T> logger,
            string connectionString,
            string topic)
        {
            ConnectionString = connectionString;
            Topic = topic;
            Logger = logger;
        }

        protected string ConnectionString { get; }
        public string Topic { get; }

        public virtual async Task Publish(T @event, CancellationToken cancellationToken)
        {
            var messageBody = JsonConvert.SerializeObject(@event);
            var topic = new TopicClient(ConnectionString, Topic);
            var message = new Message(Encoding.UTF8.GetBytes(messageBody))
            {
                MessageId = @event.Id
            };

            SetProperties(message, @event);

            await topic.SendAsync(message);
            Logger.LogInformation($"Event {@event.Id} has been published to Azure Service Bus");
        }

        protected virtual void SetProperties(Message message, T @event)
        {
            message.UserProperties.Add("messageType", @event.GetType().Name);
        }
    }
}