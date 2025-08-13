using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Input;
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
            // If the item is already in holdings, then update the quantity held and
            // recalculate the average price.
            if (model.AveragePurchasePrice > currentHolding.PeakPriceSincePurchase)
            {
                currentHolding.PeakPriceSincePurchase = model.AveragePurchasePrice;
            }

            var originalValue = currentHolding.QuantityHeld * currentHolding.AveragePurchasePrice;
            var valueAdded = model.QuantityHeld * model.AveragePurchasePrice;
            var totalQuantity = currentHolding.QuantityHeld + model.QuantityHeld;
            currentHolding.QuantityHeld = totalQuantity;
            currentHolding.AveragePurchasePrice = (originalValue + valueAdded) / totalQuantity;
            ct = await Repository.UpdateAsync(currentHolding);
        }
        else
        {
            model.PeakPriceSincePurchase = model.AveragePurchasePrice;
            ct = await Add(model);
        }

        return ct;
    }

    public async Task<int> Sell(Holding model)
    {
        var currentHolding = await GetBySymbol(model.Symbol);

        if (currentHolding! == null!)
            throw new ApplicationException(
                $"Cannot sell {model.Symbol}: {model.Name} because there are no units in the portfolio.");

        if (currentHolding.QuantityHeld < model.QuantityHeld)
            throw new ApplicationException(
                $"Sale of {model.QuantityHeld} units would result in negative remainder");

        currentHolding.QuantityHeld -= model.QuantityHeld;

        model.Events.Add(new HoldingSold(model, model.AveragePurchasePrice, model.QuantityHeld));
        if (currentHolding.QuantityHeld == 0)
            return await Delete(model);
        
        return await Update(currentHolding);
    }
}
