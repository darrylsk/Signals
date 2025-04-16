using System.Threading.Tasks;
using Signals.ApplicationLayer.Abstract;
using Signals.CoreLayer.Abstract.Base;
using Signals.CoreLayer.Entities.Base;

namespace Signals.ApplicationLayer.Services.Base;

public abstract class BusinessService<T>(IRepository<T> repository) 
    : SegregatedPartialBusinessService<T>(repository), IBusinessService<T>
    where T : Entity
{
    private IRepository<T> Repository { get; } = repository;

    public async Task<int> Add(T model)
    {
        return await Repository.AddAsync(model); 
    }

    public async Task<int> Delete(T model)
    {
        return await Repository.DeleteAsync(model);
    }
}