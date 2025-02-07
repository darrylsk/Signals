using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Signals.ApplicationLayer.Abstract;
using Signals.Data;

namespace Signals.ApplicationLayer.Services;

public class HoldingService : IHoldingService
{
    public HoldingService()
    {
        
    }
    public Task<IEnumerable<Holding>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Holding> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<int> Add(Holding model)
    {
        throw new NotImplementedException();
    }

    public Task<int> Update(Holding model)
    {
        throw new NotImplementedException();
    }

    public Task<int> Delete(Holding model)
    {
        throw new NotImplementedException();
    }
}