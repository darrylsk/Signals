using System;
using MediatR;
using Signals.CoreLayer.Entities;
using Signals.DomainEvents.Abstract;

namespace Signals.DomainEvents.Events;

public class HoldingSold(Holding holding, decimal? salePrice, int? unitsSold) : IDomainEvent
{
    public Holding Holding { get; set; } = holding;
    public decimal? SalePrice { get; } = salePrice;
    public int? UnitsSold { get; } = unitsSold;

    public DateTime TimeOfEvent { get; } = DateTime.UtcNow;
}