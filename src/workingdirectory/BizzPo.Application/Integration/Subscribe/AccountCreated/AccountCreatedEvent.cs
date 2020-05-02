using BizzPo.Application.Integration.Seedwork;
using MediatR;

namespace BizzPo.Application.Integration.Subscribe.AccountCreated
{
    public class AccountCreatedEvent : IIntegrationEvent, INotification
    {
        public string Id { get; set; }
        public string AccountId { get; set; }
    }
}