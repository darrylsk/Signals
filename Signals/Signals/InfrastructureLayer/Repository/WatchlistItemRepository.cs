using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Signals.CoreLayer.Abstract;
using Signals.Data;
using Signals.InfrastructureLayer.Repository.Base;

namespace Signals.InfrastructureLayer.Repository;

public class WatchlistItemRepository(ISignalsDbContext dbContext)
    : Repository<WatchlistItem>(dbContext), IRepository<WatchlistItem>
{
    public override async Task<IReadOnlyList<WatchlistItem>> GetAllAsync()
    {
        return await Context.Connection.Table<WatchlistItem>().ToListAsync();
    }

    public override async Task<WatchlistItem> GetByIdAsync(Guid id)
    {
        return await Context.Connection.Table<WatchlistItem>().FirstOrDefaultAsync(x => x.Id == id);
    }
}