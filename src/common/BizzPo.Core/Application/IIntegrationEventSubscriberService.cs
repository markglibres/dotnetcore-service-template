using System.Threading;
using System.Threading.Tasks;

namespace BizzPo.Core.Application
{
    public interface IIntegrationEventSubscriberService<T>
        where T : IIntegrationEvent
    {
        Task SubscribeAndExecute(CancellationToken stoppingToken);

        Task CloseAsync();
    }
}