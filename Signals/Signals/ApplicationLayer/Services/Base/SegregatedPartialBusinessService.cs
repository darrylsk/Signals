using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Signals.ApplicationLayer.Abstract;
using Signals.CoreLayer.Abstract.Base;
using Signals.CoreLayer.Entities.Base;

namespace Signals.ApplicationLayer.Services.Base;

public abstract class SegregatedPartialBusinessService<T>(ISegregatedPartialRepository<T> repository)
    : ISegregatedPartialBusinessService<T> where T : Entity
{
    private ISegregatedPartialRepository<T> Repository { get; } = repository;

    public async Task<IEnumerable<T>> GetAll()
    {
        return await Repository.GetAllAsync();
    }

    public async Task<T> GetById(Guid id)
    {
        return await Repository.GetByIdAsync(id);
    }

    public async Task<int> Update(T model)
    {
        return await Repository.UpdateAsync(model);
    }
}