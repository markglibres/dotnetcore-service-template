using System;
using MediatR;

namespace BizzPoService.Application.Queries.GetContact
{
    public class GetContactQuery : IRequest<GetContactQueryResponse>
    {
        public Guid ContactId { get; set; }
    }
}