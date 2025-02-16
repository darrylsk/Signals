using System;

namespace Signals.CoreLayer.Entities.Base;

public class Entity : EntityBase<Guid>
{
    public Entity()
    {
        if (IsTransient()) Id = Guid.NewGuid();
    }
}