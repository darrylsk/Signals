using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Signals.CoreLayer.Abstract;
using Signals.CoreLayer.Entities.Base;
using Signals.CoreLayer.Specifications.Base;

namespace Signals.InfrastructureLayer.Repository.Base;

public abstract class Repository<T>(ISignalsDbContext dbContext) : IRepository<T>
    where T : Entity
{
    protected ISignalsDbContext Context { get; } = dbContext;

    public abstract Task<IReadOnlyList<T>> GetAllAsync();

    public Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public abstract Task<T> GetByIdAsync(Guid id);

    public virtual async Task<int> AddAsync(T entity)
    {
        return await Context.Connection.InsertAsync(entity);
    }

    public virtual async Task<int> UpdateAsync(T entity)
    {
        return await Context.Connection.UpdateAsync(entity);
    }

    public virtual async Task<int> DeleteAsync(T entity)
    {
        return await Context.Connection.DeleteAsync(entity);
    }
}