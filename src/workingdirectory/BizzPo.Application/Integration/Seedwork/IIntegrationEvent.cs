using BizzPo.Domain.Seedwork;

namespace BizzPo.Application.Seedwork
{
    public interface IIntegrationEvent : IEvent
    {
        new string Id { get; set; }
    }
}