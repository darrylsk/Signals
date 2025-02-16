using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Signals.ApplicationLayer.Abstract;
using Signals.ApplicationLayer.Services.Base;
using Signals.CoreLayer.Abstract;
using Signals.CoreLayer.Entities;

namespace Signals.ApplicationLayer.Services;

public class WatchlistService : BusinessService<WatchlistItem>, IWatchlistService
{
    public IWatchlistItemRepository Repository { get; }

    public WatchlistService(IWatchlistItemRepository repository): base(repository)
    {
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

    public Task<WatchlistItem> GetById(Guid id)
    {
        return Repository.GetByIdAsync(id);
    }

    public Task<int> Add(WatchlistItem model)
    {
        return Repository.AddAsync(model);
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