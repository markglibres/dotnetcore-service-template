using System;

namespace BizzPo.Core.Domain
{
    public interface IEntity
    {
        Guid Id { get; }
        DateTime DateCreated { get; }
    }
}
