using System;
using System.Threading;
using System.Threading.Tasks;
using BizzPo.Core.Application;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BizzPo.Presentation.Subscriber
{
    public class SubscriberService<T> : BackgroundService
        where T : IIntegrationEvent
    {
        private readonly ILogger<T> _logger;
        private readonly IServiceProvider _services;

        public SubscriberService(
            ILogger<T> logger,
            IServiceProvider services)
        {
            _logger = logger;
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _services.CreateScope())
            {
                var subscriber = scope.ServiceProvider
                    .GetRequiredService<IIntegrationEventSubscriberService<T>>();

                while (!stoppingToken.IsCancellationRequested)
                {
                    await subscriber.SubscribeAndExecute(stoppingToken);
                    await Task.Delay(1000, stoppingToken);
                }

                await subscriber.CloseAsync();
            }
        }
    }
}