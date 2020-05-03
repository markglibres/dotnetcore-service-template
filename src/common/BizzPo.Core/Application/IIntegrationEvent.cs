using BizzPo.Core.Domain;

namespace BizzPo.Core.Application
{
    public interface IIntegrationEvent : IEvent
    {
        new string Id { get; set; }
    }
}