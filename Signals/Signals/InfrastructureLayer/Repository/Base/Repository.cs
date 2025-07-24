using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MediatR;
using Signals.CoreLayer.Abstract.Base;
using Signals.CoreLayer.Entities.Base;

namespace Signals.InfrastructureLayer.Repository.Base;

public abstract class Repository<T>(ISignalsDbContext dbContext, IMediator mediator) : SegregatedPartialRepository<T>(dbContext), 
    IRepository<T> where T : Entity
{
    public IMediator Mediator { get; } = mediator;

    public virtual async Task<int> AddAsync(T entity)
    {
        var ct = await Context.Connection.InsertAsync(entity);
        
        var eventsCopy = entity.Events.ToArray();
        foreach (var domainEvent in eventsCopy)
            await Mediator.Publish(domainEvent).ConfigureAwait(false);
        
        entity.Events.Clear();

        return ct;
    }

    public virtual async Task<int> DeleteAsync(T entity)
    {
        return await Context.Connection.DeleteAsync(entity);
    }
}