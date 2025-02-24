using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Signals.CoreLayer.Abstract.Base;
using Signals.CoreLayer.Entities.Base;

namespace Signals.InfrastructureLayer.Repository.Base;

public abstract class Repository<T>(ISignalsDbContext dbContext) : SegregatedPartialRepository<T>(dbContext), 
    IRepository<T> where T : Entity
{
    public virtual async Task<int> AddAsync(T entity)
    {
        return await Context.Connection.InsertAsync(entity);
    }

    public virtual async Task<int> DeleteAsync(T entity)
    {
        return await Context.Connection.DeleteAsync(entity);
    }
}