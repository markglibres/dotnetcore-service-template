using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BizzPo.Application.Seedwork;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BizzPo.Infrastructure.AzurePubSub
{
    public class AzureServiceBusPublisherService<T>
        : IIntegrationEventPublisherService<T>
        where T : IIntegrationEvent

    {
        protected readonly ILogger<T> Logger;
        protected string ConnectionString { get; }
        public string Topic { get; }

        public AzureServiceBusPublisherService(
            ILogger<T> logger,
            string connectionString,
            string topic)
        {
            ConnectionString = connectionString;
            Topic = topic;
            Logger = logger;
        }

        public virtual async Task Publish(T @event, CancellationToken cancellationToken)
        {
            var messageBody = JsonConvert.SerializeObject(@event);
            var topic = new TopicClient(ConnectionString, Topic);
            var message = new Message(Encoding.UTF8.GetBytes(messageBody))
            {
                MessageId = @event.Id
            };

            message.UserProperties.Add("messageType", @event.GetType().Name);

            await topic.SendAsync(message);
            Logger.LogInformation($"Event {@event.Id} has been published to Azure Service Bus");
        }
    }
}