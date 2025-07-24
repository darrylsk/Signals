using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Signals.ApplicationLayer.Abstract;
using Signals.ApplicationLayer.Services.Base;
using Signals.CoreLayer.Abstract;
using Signals.CoreLayer.Abstract.Base;
using Signals.CoreLayer.Entities;
using Signals.InfrastructureLayer.QuotationService.FinnhubQuotationService;
using Signals.InfrastructureLayer.Repository.Base;

namespace Signals.ApplicationLayer.Services;

public class WatchlistService : BusinessService<WatchlistItem>, IWatchlistService
{
    // public IQuotationServiceAdapter QuotationService { get; }
    public IWatchlistItemRepository Repository { get; }

    public WatchlistService(IWatchlistItemRepository repository): base(repository)
    {
        // QuotationService = quotationService;
        Repository = repository;
    }
    
    /*
    public async Task<IEnumerable<WatchlistItem>> GetAll()
    {
        return await Repository.GetAllAsync();
        
        /*
        return await Task.FromResult(new List<WatchlistItem>()
            {
                new("AAPL", "NYSE", "Apple", "USD"),
                new("NVDA", "NYSE", "Nvidia", "USD"),
                new("MSFT", "NYSE", "Microsoft", "USD"),
                new("AMZN", "NYSE", "Amazon", "USD"),
                new("META", "NYSE", "Meta", "USD"),
                new("BRK-B", "NYSE", "Birkshire Hatheway", "USD"),
                new("TSM", "NYSE", "Taiwan Semiconductor", "USD"),
                new("AVGO", "NYSE", "Broadcom", "USD"),
                new("TSLA", "NYSE", "Tesla", "USD"),
                new("LLY", "NYSE", "Eli Lilly", "USD"),
                new("WMT", "NYSE", "Walmart", "USD"),
                new("JPM", "NYSE", "JP Morgan", "USD"),
                new("V", "NYSE", "Visa", "USD"),
                new("TCEHY", "NYSE", "Tencent", "USD"),
                new("MA", "NYSE", "Mastercard", "USD"),
                new("RUM", "NYSE", "Rumble", "USD"),
                new("REGN", "NYSE", "Regeneron Pharmaceuticals Inc", "USD"),
            });
    #1#
    }
    */

    public async Task<WatchlistItem?> GetBySymbol(string symbol)
    {
        var watchlistItem = (await Repository.GetAsync(x => x.Symbol == symbol)).FirstOrDefault();
        return watchlistItem;
    }

    public async Task<int> Add(WatchlistItem model)
    {
        return await Repository.AddAsync(model); 
    }

    public async Task<int> Delete(WatchlistItem model)
    {
        return await Repository.DeleteAsync(model);
    }
    
}