using System;
using System.Threading.Tasks;
using BizzPo.Application.CreateContact;
using BizzPo.Application.GetContact;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BizzPo.Presentation.Api.Contacts
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactRequest request)
        {
            var command = new CreateContactCommand
            {
                Email = request.Email,
                Firstname = request.Firstname,
                Lastname = request.Lastname
            };

            var response = await _mediator.Send(command);

            var httpResponse = new CreateContactResponse
            {
                Id = response.ContactId.ToString(),
                IsSuccess = true
            };

            return Ok(httpResponse);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var query = new GetContactQuery
            {
                ContactId = id
            };

            var response = await _mediator.Send(query);

            var httpResponse = new ContactHttpResponse
            {
                ContactId = response.ContactId,
                Firstname = response.Firstname,
                Lastname = response.Lastname
            };

            return Ok(httpResponse);
        }
    }
}