using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BizzPo.Application.Seedwork;
using BizzPo.Domain.Seedwork;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BizzPo.Infrastructure.AzurePubSub
{
    public class AzureServiceBusSubscriberService<T> : IIntegrationEventSubscriberService<T>
        where T : IIntegrationEvent
    {
        public string Topic { get; }
        public string Subscription { get; }
        public int MaxConcurrentCalls { get; }
        protected readonly string ConnectionString;
        protected readonly IDomainEventsService DomainEventsService;
        protected readonly ILogger<T> Logger;
        protected SubscriptionClient SubscriptionClient;

        public AzureServiceBusSubscriberService(
            ILogger<T> logger,
            IDomainEventsService domainEventsService,
            string connectionStrings,
            string topic,
            string subscription,
            int maxConcurrentCalls = 100)
        {
            Topic = topic;
            Subscription = subscription;
            MaxConcurrentCalls = maxConcurrentCalls;
            Logger = logger;
            DomainEventsService = domainEventsService;
            ConnectionString = connectionStrings;
        }

        public virtual async Task CloseAsync()
        {
            await SubscriptionClient.CloseAsync();
        }

        public virtual Task SubscribeAndExecute(CancellationToken stoppingToken)
        {
            if (SubscriptionClient != null) return Task.CompletedTask;

            SubscriptionClient = new SubscriptionClient(
                ConnectionString,
                Topic,
                Subscription);

            Logger.LogInformation("Listening to events .....");
            SubscriptionClient.RegisterMessageHandler(ProcessMessageAsync, MessageHandlerOptions());

            return Task.CompletedTask;
        }

        protected virtual async Task ProcessMessageAsync(Message message, CancellationToken cancellationToken)
        {
            var messageBody = Encoding.UTF8.GetString(message.Body);
            Logger.LogInformation(
                $"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{messageBody}");

            await DomainEventsService.Publish(JsonConvert.DeserializeObject<T>(messageBody), cancellationToken);

            if (!cancellationToken.IsCancellationRequested)
                await SubscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        protected virtual MessageHandlerOptions MessageHandlerOptions()
        {
            return new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = MaxConcurrentCalls,
                AutoComplete = false
            };
        }

        protected virtual Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Logger.LogError($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            Logger.LogError("Exception context for troubleshooting:");
            Logger.LogError($"- Endpoint: {context.Endpoint}");
            Logger.LogError($"- Entity Path: {context.EntityPath}");
            Logger.LogError($"- Executing Action: {context.Action}");
            return Task.CompletedTask;
        }
    }
}