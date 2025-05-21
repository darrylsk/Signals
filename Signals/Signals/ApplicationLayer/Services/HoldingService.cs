using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Signals.ApplicationLayer.Abstract;
using Signals.ApplicationLayer.Services.Base;
using Signals.CoreLayer.Abstract;
using Signals.CoreLayer.Entities;

namespace Signals.ApplicationLayer.Services;

public class HoldingService : BusinessService<Holding>, IHoldingService
{
    public IHoldingRepository Repository { get; }
    
    public HoldingService(IHoldingRepository repository) : base(repository)
    { 
        Repository = repository;
        
    }
    // public async Task<IEnumerable<Holding>> GetAll()
    // {
    //     return await Repository.GetAllAsync();
    // }

    // public Task<Holding> GetById(Guid id)
    // {
    //     throw new NotImplementedException();
    // }

    public async Task<Holding?> GetBySymbol(string symbol)
    {
        var holding = (await Repository.GetAsync(x => x.Symbol == symbol)).FirstOrDefault();
        return holding;
    }

    public new async Task<int> Add(Holding model)
    {
        return await Repository.AddAsync(model);
    }

}