using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Signals.CoreLayer.Abstract;
using Signals.CoreLayer.Entities;
using Signals.DomainEvents.Events;

namespace Signals.DomainEvents.Handlers;

public class RecordSaleInJournalSellEventHandler(ITradingJournalRepository repository) 
    : INotificationHandler<SellEvent>
{
    private ITradingJournalRepository Repository { get; } = repository;

    /// <summary>
    /// Record the sale in the trading journal.
    /// </summary>
    /// <param name="notification"></param>
    /// <param name="cancellationToken"></param>
    public async Task Handle(SellEvent notification, CancellationToken cancellationToken)
    {
        // Console.WriteLine(Resources.Resources.SellEventHandler_Handle_Sold_units);
        var journalEntry = new TradingJournal(notification.Symbol, notification.WhenSold,
            notification.Quantity, TransactionTypes.Sale, notification.SalePrice);
        await Repository.AddAsync(journalEntry);
    }
}