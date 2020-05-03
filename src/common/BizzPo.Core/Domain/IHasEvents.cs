using System.Collections.Generic;

namespace BizzPo.Core.Domain
{
    public interface IHasEvents
    {
        IList<IEvent> Events { get; }
        void ClearEvents();
    }
}