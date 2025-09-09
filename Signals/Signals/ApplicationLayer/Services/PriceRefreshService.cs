using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Signals.ApplicationLayer.Abstract;
using Signals.CoreLayer.Entities;
using Signals.InfrastructureLayer.Abstract;

namespace Signals.ApplicationLayer.Services;

public class PriceRefreshService : IPriceRefreshService
{
    public IQuotationServiceAdapter QuotationService { get; }
    public IWatchlistService WatchlistService { get; }
    public IHoldingService HoldingService { get; }
    public IIndexItemService IndexService { get; }

    public PriceRefreshService(
        IQuotationServiceAdapter quotationService,
        IWatchlistService watchlistService,
        IHoldingService holdingService,
        IIndexItemService indexService)
    {
        QuotationService = quotationService;
        WatchlistService = watchlistService;
        HoldingService = holdingService;
        IndexService = indexService;
    }

    public async Task<int> UpdateWatchlistPrices()
    {
        var itemsUpdated = 0;

        try
        {
            var watchlist = (await WatchlistService.GetAll())
                .OrderBy(x => x.WhenLatestQuoteReceived);

            foreach (var watchlistItem in watchlist)
            {
                var quote = await QuotationService.GetQuoteAsync(watchlistItem.Symbol);
                watchlistItem.LatestQuotedPrice = quote?.LatestQuotedPrice ?? 0;
                watchlistItem.WhenLatestQuoteReceived = quote.WhenLatestQuoteReceived;
                await WatchlistService.Update(watchlistItem);
                itemsUpdated++;
                // Wait for 1.05 seconds to avoid hitting the API too quickly.
                Thread.Sleep(1050);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return itemsUpdated;
    }

    public async Task<int> UpdateHoldingPrices()
    {
        var itemsUpdated = 0;
        try
        {
            var holdings = (await HoldingService.GetAll())
                .OrderBy(x => x.WhenLatestQuoteReceived);

            foreach (var holding in holdings)
            {
                var quote = await QuotationService.GetQuoteAsync(holding.Symbol);
                holding.LatestQuotedPrice = quote?.LatestQuotedPrice ?? 0;
                holding.WhenLatestQuoteReceived = quote.WhenLatestQuoteReceived;
                await HoldingService.Update(holding);
                itemsUpdated++;
                // Wait for 1.05 seconds to avoid hitting the API too quickly.
                Thread.Sleep(1050);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return itemsUpdated;
    }

    public async Task<int> UpdateIndexPrices()
    {
        var itemsUpdated = 0;

        try
        {
            var indexes = (await IndexService.GetAll())
                .OrderBy(x => x.WhenLatestQuoteReceived);

            foreach (var index in indexes)
            {
                var quote = await QuotationService.GetQuoteAsync(index.Symbol);
                index.LatestQuotedPrice = quote?.LatestQuotedPrice ?? 0;
                index.WhenLatestQuoteReceived = quote?.WhenLatestQuoteReceived;
                index.CurrentDayOpeningPrice = quote?.CurrentDayOpeningPrice ?? 0;
                index.CurrentDayHighPrice = quote?.CurrentDayHighPrice ?? 0;
                index.CurrentDayLowPrice = quote?.CurrentDayLowPrice ?? 0;
                index.PreviousDayClosingPrice = quote?.PreviousDayClosingPrice ?? 0;
                await IndexService.Update(index);

                // index.LatestQuotedPrice = 100 + itemsUpdated * 10;
                // index.WhenLatestQuoteReceived = DateTime.Now;
                // index.Symbol = "TEST";
                // index.Name = "Testing Periodic Worker";
                // await IndexService.Update(index);
                
                itemsUpdated++;

                // Wait for 1.05 seconds to avoid hitting the API too quickly.
                Thread.Sleep(1050);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return itemsUpdated;
    }
}