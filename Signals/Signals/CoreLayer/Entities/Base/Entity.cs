using System;
using SQLite;

namespace Signals.CoreLayer.Entities.Base;

public class Entity : EntityBase<Guid>
{
    protected Entity()
    {
        if (IsTransient()) Id = Guid.NewGuid();
    }

    [PrimaryKey]
    public sealed override Guid Id { get; set; }
}