﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Signals.ApplicationLayer.Abstract;
using Signals.Data;

namespace Signals.ApplicationLayer.Services;

public class WatchlistService : IWatchlistService
{
    public async Task<IEnumerable<WatchlistItem>> GetAll()
    {
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
        throw new NotImplementedException();
    }

    public Task<int> Add(WatchlistItem model)
    {
        throw new NotImplementedException();
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