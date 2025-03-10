using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Signals.ApplicationLayer.Abstract;

public interface IRetrieveService<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<T> GetById(Guid id);
}