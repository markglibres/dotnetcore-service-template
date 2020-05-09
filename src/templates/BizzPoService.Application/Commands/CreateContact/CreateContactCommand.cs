using MediatR;

namespace BizzPoService.Application.Commands.CreateContact
{
    public class CreateContactCommand : IRequest<CreateContactCommandResponse>
    {
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}