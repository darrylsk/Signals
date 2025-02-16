using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Threading.Tasks;
using Signals.CoreLayer.Abstract.Base;
using Signals.CoreLayer.Entities.Base;

namespace Signals.InfrastructureLayer.Repository.Base;

/// <summary>
/// A single record repository represents a table that always has a cardinality of 1.  The
/// corresponding table will always have exactly one record with no additions nor deletions allowed.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="dbContext"></param>
public abstract class SegregatedPartialRepository<T>(ISignalsDbContext dbContext) : ISegregatedPartialRepository<T>
    where T : Entity
{

    protected ISignalsDbContext Context { get; } = dbContext;

    public abstract Task<IReadOnlyList<T>> GetAllAsync();

    public abstract Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);

    public abstract Task<T> GetByIdAsync(Guid id);

    public virtual async Task<int> UpdateAsync(T entity)
    {
        return await Context.Connection.UpdateAsync(entity);
    }
}
