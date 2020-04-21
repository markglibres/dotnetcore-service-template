using System.Threading;
using System.Threading.Tasks;
using BizzPo.Domain.Contacts.Seedwork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BizzPo.Application.CreateContact
{
    public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, CreateContactCommandResponse>
    {
        private readonly IContactService _contactService;
        private readonly ILogger<CreateContactCommandHandler> _logger;

        public CreateContactCommandHandler(
            ILogger<CreateContactCommandHandler> logger,
            IContactService contactService)
        {
            _logger = logger;
            _contactService = contactService;
        }

        public async Task<CreateContactCommandResponse> Handle(
            CreateContactCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(CreateContactCommand)} has been handled my mediatr");
            _logger.LogInformation("Do your stuff here...");

            var contact = await _contactService.Create(request.Email, request.Firstname, request.Lastname);

            return new CreateContactCommandResponse
            {
                ContactId = contact.Id
            };
        }
    }
}