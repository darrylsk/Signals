using System;
using System.Collections.Generic;
using MediatR;
using SQLite;

namespace Signals.CoreLayer.Entities.Base;

public class Entity : EntityBase<Guid>
{
    protected Entity()
    {
        if (IsTransient())
        {
            Id = Guid.NewGuid();
            WhenCreated = DateTime.UtcNow;
        }
    }

    [PrimaryKey]
    public sealed override Guid Id { get; set; }

    [Ignore]
    public List<INotification> Events { get; set; } = new();

}