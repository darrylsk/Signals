using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MediatR;
using Signals.CoreLayer.Abstract;
using Signals.CoreLayer.Entities;
using Signals.InfrastructureLayer.Repository.Base;

namespace Signals.InfrastructureLayer.Repository;

public class IndexItemRepository(ISignalsDbContext dbContext, IMediator mediator)
    : Repository<IndexItem>(dbContext, mediator), IIndexItemRepository
{
    public override async Task<IReadOnlyList<IndexItem>> GetAllAsync()
    {
        return await Context.Connection.Table<IndexItem>().ToListAsync();
    }

    public override async Task<IReadOnlyList<IndexItem>> GetAsync(Expression<Func<IndexItem, bool>> predicate)
    {
        return await Context.Connection.Table<IndexItem>().Where(predicate).ToListAsync();
    }

    public override async Task<IndexItem> GetByIdAsync(Guid id)
    {
        return await Context.Connection.Table<IndexItem>().FirstOrDefaultAsync(x => x.Id == id);
    }
}