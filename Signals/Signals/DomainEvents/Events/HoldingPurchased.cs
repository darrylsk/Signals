using System;
using Signals.CoreLayer.Entities;
using Signals.DomainEvents.Abstract;

namespace Signals.DomainEvents.Events;

public class HoldingPurchased(Holding holding) : IDomainEvent
{
    public Holding Holding { get; set; } = holding;
    public DateTime TimeOfEvent { get; } = DateTime.UtcNow;
}