using System;
using BizzPo.Core.Domain;

namespace BizzPoService.Infrastructure.Messaging.SqlPubSub.Database
{
    public class PubSubEvent : IEntity
    {
        public string MessageType { get; set; }
        public dynamic Message { get; set; }
        public string Topic { get; set; }
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
    }
}