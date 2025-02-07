using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Signals.ApplicationLayer.Abstract;

public interface IBusinessService<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<T> GetById(Guid id);
    Task<int> Add(T model);
    Task<int> Update(T model);
    Task<int> Delete(T model);
}