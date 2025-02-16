using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Threading.Tasks;
using Signals.CoreLayer.Entities.Base;

namespace Signals.CoreLayer.Abstract.Base;

public interface ISegregatedPartialRepository<T> where T : Entity
{
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
    Task<T> GetByIdAsync(Guid id);
    Task<int> UpdateAsync(T entity);
}
