using System.Threading;
using System.Threading.Tasks;
using BizzPo.Application.Seedwork;
using Microsoft.Extensions.Logging;

namespace BizzPo.Infrastructure.EventBus
{
    public class AzureServiceBusPublisherService : IIntegrationEventPublisherService
    {
        private readonly ILogger<AzureServiceBusPublisherService> _logger;

        public AzureServiceBusPublisherService(ILogger<AzureServiceBusPublisherService> logger)
        {
            _logger = logger;
        }

        public Task Publish(IIntegrationEvent @event, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Integration events published to Azure Service Bus");
            _logger.LogInformation("Do your stuff here");
            return Task.CompletedTask;
        }
    }
}