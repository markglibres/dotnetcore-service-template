﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BizzPo.Core.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BizzPo.Core.Infrastructure.Messaging.MediatR
{
    public class MediatrDomainEventsService : IDomainEventsService
    {
        private readonly ILogger<MediatrDomainEventsService> _logger;
        private readonly IMediator _mediator;

        public MediatrDomainEventsService(
            ILogger<MediatrDomainEventsService> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task Publish(IEvent @event, CancellationToken cancellationToken)
        {
            await _mediator.Publish(@event, cancellationToken);
            _logger.LogInformation($"Event published by Mediatr: {JsonConvert.SerializeObject(@event)}");
        }

        public async Task Publish(IEnumerable<IEvent> domainEvents, CancellationToken cancellationToken)
        {
            var eventsToPublish = new List<Task>();

            domainEvents.ToList().ForEach(@event => eventsToPublish.Add(Publish(@event, cancellationToken)));

            await Task.WhenAll(eventsToPublish);
        }
    }
}