using System;
using MediatR;

namespace BizzPo.Application.Queries.GetContact
{
    public class GetContactQuery : IRequest<GetContactQueryResponse>
    {
        public Guid ContactId { get; set; }
    }
}