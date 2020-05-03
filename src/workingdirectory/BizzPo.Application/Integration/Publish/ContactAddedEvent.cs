﻿using BizzPo.Core.Application;

namespace BizzPo.Application.Integration.Publish
{
    public class ContactAddedEvent : IIntegrationEvent
    {
        public string ContactId { get; set; }
        public string Id { get; set; }
    }
}