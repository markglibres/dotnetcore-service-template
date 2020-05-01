using BizzPo.Application.Seedwork;

namespace BizzPo.Application.Publish
{
    public class ContactAddedEvent : IIntegrationEvent
    {
        public string ContactId { get; set; }
        public string Id { get; set; }
    }
}