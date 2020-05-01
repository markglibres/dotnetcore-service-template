using System.Threading;
using System.Threading.Tasks;

namespace BizzPo.Application.Seedwork
{
    public interface IIntegrationEventPublisherService<T>
        where T : IIntegrationEvent
    {
        Task Publish(T @event, CancellationToken cancellationToken);
    }
}