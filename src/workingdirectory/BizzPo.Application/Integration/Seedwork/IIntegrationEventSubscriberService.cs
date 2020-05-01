using System.Threading;
using System.Threading.Tasks;

namespace BizzPo.Application.Seedwork
{
    public interface IIntegrationEventSubscriberService<T>
        where T : IIntegrationEvent
    {
        Task SubscribeAndExecute(CancellationToken stoppingToken);
           
        Task CloseAsync();
    }
}