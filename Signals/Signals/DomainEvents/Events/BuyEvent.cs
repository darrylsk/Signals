using System;
using INotification = MediatR.INotification;

namespace Signals.DomainEvents.Events;

public class BuyEvent(string symbol, decimal purchasePrice, decimal quantity, DateTime whenPurchased) : INotification
{
    public string Symbol { get; set; } = symbol;
    public decimal PurchasePrice { get; set; } = purchasePrice;
    public decimal Quantity { get; } = quantity;
    public DateTime WhenPurchased { get; set; } = whenPurchased;
}