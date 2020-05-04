using System;
using System.Threading;
using System.Threading.Tasks;
using BizzPo.Application.Integration.Publish;
using BizzPo.Core.Application;
using BizzPo.Domain.Contacts.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BizzPo.Application.DomainHandlers
{
    public class ContactCreatedEventHandler : INotificationHandler<ContactCreatedEvent>
    {
        private readonly ILogger<ContactCreatedEventHandler> _logger;
        private readonly IIntegrationEventPublisherService<ContactAddedEvent> _integrationEventPublisherService;

        public ContactCreatedEventHandler(
            ILogger<ContactCreatedEventHandler> logger,
            IIntegrationEventPublisherService<ContactAddedEvent> integrationEventPublisherService)
        {
            _logger = logger;
            _integrationEventPublisherService = integrationEventPublisherService;
        }

        public async Task Handle(ContactCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(ContactCreatedEventHandler)} has been handled by mediatr");
            _logger.LogInformation("Do your stuff here");

            var contactAddedEvents = new ContactAddedEvent
            {
                ContactId = notification.Id,
                Id = Guid.NewGuid().ToString()
            };

            await _integrationEventPublisherService.Publish(contactAddedEvents, cancellationToken);
        }
    }
}