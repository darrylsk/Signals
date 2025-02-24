using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Signals.ApplicationLayer.Abstract;
using Signals.ApplicationLayer.Services.Base;
using Signals.CoreLayer.Abstract;
using Signals.CoreLayer.Entities;
using Signals.InfrastructureLayer.Abstract;
using Signals.InfrastructureLayer.QuotationService.FinnhubQuotationService;

namespace Signals.ApplicationLayer.Services;

public class WatchlistService : BusinessService<WatchlistItem>, IWatchlistService
{
    public IQuotationServiceAdapter QuotationService { get; }
    public IWatchlistItemRepository Repository { get; }

    public WatchlistService(
        IQuotationServiceAdapter quotationService,
        IWatchlistItemRepository repository): base(repository)
    {
        QuotationService = quotationService;
        Repository = repository;
    }
    public async Task<IEnumerable<WatchlistItem>> GetAll()
    {
        // return await Repository.GetAllAsync();
        
        return await Task.FromResult(new List<WatchlistItem>()
            {
                new("NVDA", "NYSE", "Nvidia", "USD"),
                new("AAPL", "NYSE", "Apple", "USD"),
                new("MSFT", "NYSE", "Microsoft", "USD"),
                new("RUM", "NYSE", "Rumble", "USD"),
                new("REGN", "NYSE", "Regeneron Pharmaceuticals Inc", "USD"),
            });
    }

    public Task<WatchlistItem?> GetById(Guid id)
    {
        return Repository.GetByIdAsync(id);
    }

    public async Task<WatchlistItem?> GetBySymbol(string symbol)
    {
        var watchlistItem = (await Repository.GetAsync(x => x.Symbol == symbol)).FirstOrDefault();
        return watchlistItem;
    }

    public async Task<int> Add(WatchlistItem model)
    {
        //return await Repository.AddAsync(model);
        return await Task.FromResult(1);
    }

    /// <summary>
    /// Add a new profile to the app given its ticker symbol.
    /// </summary>
    /// <param name="symbol"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public override async Task<int> AddSymbol(string symbol)
    {
        // Get profile from web service using symbol, then add it to the database.
        var profile = await QuotationService.GetProfileAsync(symbol);
        Console.WriteLine($"{profile?.Name}, with ticker symbol {profile?.Symbol} has been added.");
        return 1;
    }

    public Task<int> Update(WatchlistItem model)
    {
        throw new NotImplementedException();
    }

    public Task<int> Delete(WatchlistItem model)
    {
        throw new NotImplementedException();
    }
}