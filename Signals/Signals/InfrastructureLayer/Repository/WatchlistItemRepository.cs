using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Signals.CoreLayer.Abstract;
using Signals.CoreLayer.Abstract.Base;
using Signals.CoreLayer.Entities;
using Signals.InfrastructureLayer.Repository.Base;

namespace Signals.InfrastructureLayer.Repository;

public class WatchlistItemRepository(ISignalsDbContext dbContext)
    : Repository<WatchlistItem>(dbContext), IWatchlistItemRepository
{
    public override async Task<IReadOnlyList<WatchlistItem>> GetAllAsync()
    {
        return await Context.Connection.Table<WatchlistItem>().ToListAsync();
    }

    public override async Task<IReadOnlyList<WatchlistItem>> GetAsync(Expression<Func<WatchlistItem, bool>> predicate)
    {
        return await Context.Connection.Table<WatchlistItem>().Where(predicate).ToListAsync();
    }

    public override async Task<WatchlistItem> GetByIdAsync(Guid id)
    {
        return await Context.Connection.Table<WatchlistItem>().FirstOrDefaultAsync(x => x.Id == id);
    }
}