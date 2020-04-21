using System;
using System.Collections.Generic;
using System.Linq;

namespace BizzPo.Domain.Seedwork
{
    public abstract class Entity : IEntity, IHasEvents
    {
        public Guid Id { get; private set; }
        public IList<IEvent> Events { get; }

        protected Entity()
        {
            Events = new List<IEvent>();
            Id = Guid.NewGuid();
        }

        protected void Emit(IEvent @event)
        {
            if (Events.Any(e => e.Id == @event.Id)) return;

            Events.Add(@event);
        }

        
    }
}
