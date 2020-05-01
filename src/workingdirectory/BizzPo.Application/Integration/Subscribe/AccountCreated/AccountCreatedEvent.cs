using BizzPo.Application.Seedwork;
using MediatR;

namespace BizzPo.Application.Subscribe.AccountCreated
{
    public class AccountCreatedEvent : IIntegrationEvent, INotification
    {
        public string Id { get; set; }
        public string AccountId { get; set; }
    }
}