using BizzPo.Core.Application;
using MediatR;

namespace BizzPo.Application.Integration.Subscribe.AccountCreated
{
    public class AccountCreatedEvent : IIntegrationEvent, INotification
    {
        public string AccountId { get; set; }
        public string Id { get; set; }
    }
}