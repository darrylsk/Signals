using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Signals.CoreLayer.Abstract;
using Signals.CoreLayer.Entities;
using Signals.CoreLayer.Enums;
using Signals.DomainEvents.Events;

namespace Signals.DomainEvents.Handlers;

public class RecordPurchaseInJournalBuyEventHandler(ITradingJournalRepository repository)
    : INotificationHandler<HoldingPurchased>
{
    private ITradingJournalRepository Repository { get; } = repository;

    /// <summary>
    /// Record the purchase in the trading journal.
    /// </summary>
    /// <param name="notification"></param>
    /// <param name="cancellationToken"></param>
    public async Task Handle(HoldingPurchased notification, CancellationToken cancellationToken)
    {
        // Console.WriteLine(Resources.Resources.BuyEventHandler_Handle_purchased_units);
        var journalEntry = new TradingJournal(notification.Holding.Symbol, notification.TimeOfEvent,
            notification.Holding.QuantityHeld, TransactionTypes.Purchase, notification.Holding.LatestQuotedPrice);
        await Repository.AddAsync(journalEntry);
    }
}