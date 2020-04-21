using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BizzPo.Domain.Seedwork
{
    public interface IDomainEventsService
    {
        Task Publish(IEvent @event, CancellationToken cancellationToken);
        Task Publish(IEnumerable<IEvent> domainEvents, CancellationToken cancellationToken);
    }
}