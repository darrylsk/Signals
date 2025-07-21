using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Signals.CoreLayer.Abstract;
using Signals.CoreLayer.Abstract.Base;
using Signals.CoreLayer.Entities;
using Signals.InfrastructureLayer.Repository.Base;

namespace Signals.InfrastructureLayer.Repository;

public class HoldingRepository(ISignalsDbContext dbContext)
    : Repository<Holding>(dbContext), IHoldingRepository
{
    public override async Task<IReadOnlyList<Holding>> GetAllAsync()
    {
        return await Context.Connection.Table<Holding>().ToListAsync();
    }

    public override async Task<IReadOnlyList<Holding>> GetAsync(Expression<Func<Holding, bool>> predicate)
    {
        return await Context.Connection.Table<Holding>().Where(predicate).ToListAsync();
    }

    public override async Task<Holding> GetByIdAsync(Guid id)
    {
        return await Context.Connection.Table<Holding>().FirstOrDefaultAsync(x => x.Id == id);
    }
}
public class TradingJournalRepository(ISignalsDbContext dbContext) : Repository<TradingJournal>(dbContext), ITradingJournalRepository
{
    public override async Task<IReadOnlyList<TradingJournal>> GetAllAsync()
    {
        return await Context.Connection.Table<TradingJournal>().ToListAsync();
    }

    public override async Task<IReadOnlyList<TradingJournal>> GetAsync(Expression<Func<TradingJournal, bool>> predicate)
    {
        return await Context.Connection.Table<TradingJournal>().Where(predicate).ToListAsync();
    }

    public override async Task<TradingJournal> GetByIdAsync(Guid id)
    {
        return await Context.Connection.Table<TradingJournal>().FirstOrDefaultAsync(x => x.Id == id);
    }
}