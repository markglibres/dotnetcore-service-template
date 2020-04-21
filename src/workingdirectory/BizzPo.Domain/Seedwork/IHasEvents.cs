using System.Collections.Generic;

namespace BizzPo.Domain.Seedwork
{
    public interface IHasEvents
    {
        IList<IEvent> Events { get; }
    }
}