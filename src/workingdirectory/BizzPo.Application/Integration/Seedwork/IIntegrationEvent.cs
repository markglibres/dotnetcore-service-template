using BizzPo.Domain.Seedwork;

namespace BizzPo.Application.Integration.Seedwork
{
    public interface IIntegrationEvent : IEvent
    {
        new string Id { get; set; }
    }
}