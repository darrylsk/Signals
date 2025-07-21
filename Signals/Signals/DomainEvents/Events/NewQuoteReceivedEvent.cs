using System;
using INotification = MediatR.INotification;

namespace Signals.DomainEvents.Events;

public class NewQuoteReceivedEvent(string symbol, decimal previousPrice, decimal newPrice, DateTime whenReceived) : INotification
{
    public string Symbol { get; } = symbol;
    public decimal PreviousPrice { get; } = previousPrice;
    public decimal NewPrice { get; } = newPrice;
    public DateTime WhenReceived { get; } = whenReceived;
}