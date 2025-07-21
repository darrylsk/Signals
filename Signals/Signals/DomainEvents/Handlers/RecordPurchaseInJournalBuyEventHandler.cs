using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Signals.CoreLayer.Abstract;
using Signals.CoreLayer.Entities;
using Signals.DomainEvents.Events;

namespace Signals.DomainEvents.Handlers;

public class RecordPurchaseInJournalBuyEventHandler(ITradingJournalRepository repository)
    : INotificationHandler<BuyEvent>
{
    private ITradingJournalRepository Repository { get; } = repository;

    /// <summary>
    /// Record the purchase in the trading journal.
    /// </summary>
    /// <param name="notification"></param>
    /// <param name="cancellationToken"></param>
    public async Task Handle(BuyEvent notification, CancellationToken cancellationToken)
    {
        // Console.WriteLine(Resources.Resources.BuyEventHandler_Handle_purchased_units);
        var journalEntry = new TradingJournal(notification.Symbol, notification.WhenPurchased,
            notification.Quantity, TransactionTypes.Purchase, notification.PurchasePrice);
        await Repository.AddAsync(journalEntry);
    }
}