using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Signals.ApplicationLayer.Abstract;
using Signals.ApplicationLayer.Services.Base;
using Signals.CoreLayer.Abstract;
using Signals.CoreLayer.Entities;
using Signals.DomainEvents.Events;

namespace Signals.ApplicationLayer.Services;

public class HoldingService : BusinessService<Holding>, IHoldingService
{
    public IHoldingRepository Repository { get; }
    public IMediator Mediator { get; }

    public HoldingService(IHoldingRepository repository, IMediator mediator) : base(repository)
    {
        Repository = repository;
        Mediator = mediator;
    }

    public async Task<Holding?> GetBySymbol(string symbol)
    {
        var holding = (await Repository.GetAsync(x => x.Symbol == symbol)).FirstOrDefault();
        return holding;
    }

    /// <summary>
    /// Add a new holding.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    private async Task<int> Add(Holding model)
    {
        var ct = await Repository.AddAsync(model);
        return ct;
    }

    /// <summary>
    /// Delete a holding.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    private async Task<int> Delete(Holding model)
    {
        return await Repository.DeleteAsync(model);
    }


    public async Task<int> Buy(Holding model)
    {
        model.Events.Add(new HoldingPurchased(model));
        int ct;
        var currentHolding = await GetBySymbol(model.Symbol);
        if (currentHolding! != null!)
        {
            currentHolding.QuantityHeld += model.QuantityHeld;
            ct = await Repository.UpdateAsync(currentHolding);
        }
        else
        {
            ct = await Add(model);
        }

        return ct;
    }

    public async Task<int> Sell(Holding model, decimal salePrice, decimal unitsSold)
    {
        int ct;
        var currentHolding = await GetBySymbol(model.Symbol);
        if (currentHolding != null && currentHolding.QuantityHeld > 0)
        {
            currentHolding.QuantityHeld -= model.QuantityHeld;
            if (currentHolding.QuantityHeld < 0)
                throw new ApplicationException(
                    $"Sale of {model.QuantityHeld} units would result in negative remainder");
            else if (currentHolding.QuantityHeld == 0)
                ct = await Delete(model);
            model.Events.Add(new HoldingSold(model, salePrice, unitsSold));
        }

        throw new ApplicationException(
            $"Cannot sell {model.Symbol}: {model.Name} because there are no units in the portfolio.");
    }
}