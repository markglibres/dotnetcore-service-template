using System;
using System.Collections.Generic;
using System.Linq;

namespace BizzPo.Core.Domain
{
    public abstract class Entity : IEntity, IHasEvents
    {
        protected Entity()
        {
            Events = new List<IEvent>();
            Id = Guid.NewGuid();
            DateCreated = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public DateTime DateCreated { get; private set; }
        public IList<IEvent> Events { get; }

        public void ClearEvents()
        {
            Events.Clear();
        }

        protected void Emit(IEvent @event)
        {
            if (Events.Any(e => e.Id == @event.Id)) return;

            Events.Add(@event);
        }
    }
}