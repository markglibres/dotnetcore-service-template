using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BizzPo.Application.Subscribe.AccountCreated
{
    public class AccountCreatedEventHandler : INotificationHandler<AccountCreatedEvent>
    {
        private readonly ILogger<AccountCreatedEventHandler> _logger;

        public AccountCreatedEventHandler(
            ILogger<AccountCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(AccountCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("do something here with the subscribed event");
            return Task.CompletedTask;
        }
    }
}