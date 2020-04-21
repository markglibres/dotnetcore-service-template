using System.Threading;
using System.Threading.Tasks;
using BizzPo.Domain.Contacts.Seedwork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BizzPo.Application.GetContact
{
    public class GetContactQueryHandler : IRequestHandler<GetContactQuery, GetContactQueryResponse>
    {
        private readonly IContactService _contactService;
        private readonly ILogger<GetContactQueryHandler> _logger;

        public GetContactQueryHandler(
            ILogger<GetContactQueryHandler> logger,
            IContactService contactService)
        {
            _logger = logger;
            _contactService = contactService;
        }

        public async Task<GetContactQueryResponse> Handle(GetContactQuery request, CancellationToken cancellationToken)
        {
            var contact = await _contactService.GetById(request.ContactId);

            return new GetContactQueryResponse
            {
                ContactId = contact.Id,
                Firstname = contact.Firstname,
                Lastname = contact.Lastname
            };
        }
    }
}