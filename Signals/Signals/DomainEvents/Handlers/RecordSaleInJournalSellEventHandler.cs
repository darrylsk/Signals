using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Signals.CoreLayer.Abstract;
using Signals.CoreLayer.Entities;
using Signals.DomainEvents.Events;

namespace Signals.DomainEvents.Handlers;

public class RecordSaleInJournalSellEventHandler(ITradingJournalRepository repository) 
    : INotificationHandler<HoldingSold>
{
    private ITradingJournalRepository Repository { get; } = repository;

    /// <summary>
    /// Record the sale in the trading journal.
    /// </summary>
    /// <param name="notification"></param>
    /// <param name="cancellationToken"></param>
    public async Task Handle(HoldingSold notification, CancellationToken cancellationToken)
    {
        // Console.WriteLine(Resources.Resources.SellEventHandler_Handle_Sold_units);
        var journalEntry = new TradingJournal(notification.Holding.Symbol, notification.TimeOfEvent, 
            notification.UnitsSold, TransactionTypes.Sale, notification.SalePrice);
        await Repository.AddAsync(journalEntry);
    }
}