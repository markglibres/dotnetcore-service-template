using BizzPo.Core.Infrastructure.Messaging.MediatR;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BizzPoService.Infrastructure.DomainEvents
{
    //do your own implementation of IDomainEventsService
    //or you can inherit / use from
    //BizzPo.Core.Infrastructure.Messaging.MediatR.MediatrDomainEventsService
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