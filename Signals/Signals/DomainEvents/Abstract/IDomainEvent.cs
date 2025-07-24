using System;
using MediatR;

namespace Signals.DomainEvents.Abstract;

public interface IDomainEvent : INotification
{
    public DateTime TimeOfEvent { get; }
}