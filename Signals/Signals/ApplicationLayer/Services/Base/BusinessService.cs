using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Signals.ApplicationLayer.Abstract;
using Signals.CoreLayer.Abstract;
using Signals.CoreLayer.Entities.Base;

namespace Signals.ApplicationLayer.Services.Base;

public abstract class BusinessService<T>(IRepository<T> repository) : IBusinessService<T>
    where T : Entity
{
    public IRepository<T> Repository { get; } = repository;

    public async Task<IEnumerable<T>> GetAll()
    {
        return await Repository.GetAllAsync();
    }

    public async Task<T> GetById(Guid id)
    {
        return await Repository.GetByIdAsync(id);
    }

    public async Task<int> Add(T model)
    {
        return await Repository.AddAsync(model); 
    }

    public async Task<int> Update(T model)
    {
        return await Repository.UpdateAsync(model);
    }

    public async Task<int> Delete(T model)
    {
        return await Repository.DeleteAsync(model);
    }
}