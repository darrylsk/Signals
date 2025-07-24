using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MediatR;
using Signals.CoreLayer.Abstract;
using Signals.CoreLayer.Entities;
using Signals.InfrastructureLayer.Repository.Base;

namespace Signals.InfrastructureLayer.Repository;

public class TradingJournalRepository(ISignalsDbContext dbContext, IMediator mediator) : 
    Repository<TradingJournal>(dbContext, mediator), ITradingJournalRepository
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