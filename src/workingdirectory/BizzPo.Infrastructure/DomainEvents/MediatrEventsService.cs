using BizzPo.Core.Infrastructure.Messaging.MediatR;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BizzPo.Infrastructure.DomainEvents
{
    //implement your own service here
    public class MediatrEventsService : MediatrDomainEventsService
    {
        public MediatrEventsService(
            ILogger<MediatrDomainEventsService> logger,
            IMediator mediator)
            : base(logger, mediator)
        {
        }
    }
}