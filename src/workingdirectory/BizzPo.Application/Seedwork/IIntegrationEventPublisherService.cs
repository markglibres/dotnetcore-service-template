using System.Threading;
using System.Threading.Tasks;

namespace BizzPo.Application.Seedwork
{
    public interface IIntegrationEventPublisherService
    {
        Task Publish(IIntegrationEvent @event, CancellationToken cancellationToken);
    }
}