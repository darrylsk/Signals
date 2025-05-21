using System;

namespace Signals.CoreLayer.Entities.Base;

public class Entity : EntityBase<Guid>
{
    protected Entity()
    {
        if (IsTransient()) Id = Guid.NewGuid();
    }

    public sealed override Guid Id { get; set; }
}