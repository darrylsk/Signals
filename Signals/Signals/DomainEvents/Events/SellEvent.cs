using System;
using MediatR;

namespace Signals.DomainEvents.Events;

public class SellEvent(string symbol, decimal salePrice, decimal quantity, DateTime whenSold) : INotification
{
    public string Symbol { get; } = symbol;
    public decimal SalePrice { get; } = salePrice;
    public decimal Quantity { get; } = quantity;
    public DateTime WhenSold { get; } = whenSold;
}