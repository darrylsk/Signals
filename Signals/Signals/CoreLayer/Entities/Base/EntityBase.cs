﻿using System;
using System.Diagnostics;
using SQLite;

namespace Signals.CoreLayer.Entities.Base;

public abstract class EntityBase<TId> : IEntityBase<TId>
{
    [PrimaryKey]
    public abstract TId Id { get; set; }
    
    public DateTime WhenCreated { get; set; }

    int? _requestedHashCode;

    protected bool IsTransient()
    {
        return Id.Equals(default(TId));
    }

    public override bool Equals(object? obj)
    {
        if (obj! == null! || !(obj is EntityBase<TId>))
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (GetType() != obj.GetType())
            return false;

        var item = (EntityBase<TId>)obj;

        if (item.IsTransient() || IsTransient())
            return false;
        else
            return item == this;
    }

    public override int GetHashCode()
    {
        if (!IsTransient())
        {
            if (!_requestedHashCode.HasValue)
                _requestedHashCode = Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

            return _requestedHashCode.Value;
        }
        else
            return base.GetHashCode();
    }

    public static bool operator ==(EntityBase<TId> left, EntityBase<TId> right)
    {
        if (Equals(left, null))
            return Equals(right, null) ? true : false;
        else
            return left.Equals(right);
    }

    public static bool operator !=(EntityBase<TId> left, EntityBase<TId> right)
    {
        return !(left == right);
    }
}
