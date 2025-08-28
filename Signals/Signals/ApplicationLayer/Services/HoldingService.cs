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
        var holding = (await Repository.GetAsync(x => x.Symbol == symbol.ToUpper())).FirstOrDefault();
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
        try
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

                var originalValue = (currentHolding.QuantityHeld ?? 0) * (currentHolding.AveragePurchasePrice ?? 0);
                var valueAdded = model.QuantityHeld * model.AveragePurchasePrice;
                var totalQuantity = (currentHolding.QuantityHeld ?? 0) + (model.QuantityHeld ?? 0);
                currentHolding.QuantityHeld = totalQuantity;
                currentHolding.AveragePurchasePrice = (originalValue + (valueAdded ?? 0)) / totalQuantity;
                ct = await Repository.UpdateAsync(currentHolding);
            }
            else
            {
                model.PeakPriceSincePurchase = model.AveragePurchasePrice;
                ct = await Add(model);
            }

            return ct;
        }
        catch (Exception e)
        {
            // Log any exceptions to the console.  Todo: Add proper logging.
            Console.WriteLine(e);
            return 0;
        }
    }

    public async Task<int> Sell(Holding model)
    {
        try
        {
            var currentHolding = await GetBySymbol(model.Symbol);

            if (currentHolding! == null!)
                throw new ApplicationException(
                    $"Cannot sell {model.Symbol}: {model.Name} because there are no units in the portfolio.");

            if (currentHolding.QuantityHeld < model.QuantityHeld)
                throw new ApplicationException(
                    $"Sale of {model.QuantityHeld} units would result in negative remainder");

            var originalValue = (currentHolding.QuantityHeld ?? 0) * (currentHolding.AveragePurchasePrice ?? 0);
            var valueSold = model.QuantityHeld * model.AveragePurchasePrice;
            var newQuantityHeld = (currentHolding.QuantityHeld ?? 0) - (model.QuantityHeld ?? 0);

            // If all items are sold, then delete the holding.
            if (newQuantityHeld <= 0)
                return await Delete(model);

            currentHolding.QuantityHeld = newQuantityHeld;
            var newAveragePrice = (originalValue - valueSold) / newQuantityHeld;
            currentHolding.AveragePurchasePrice = newAveragePrice;
            model.Events.Add(new HoldingSold(model, model.AveragePurchasePrice ?? 0, model.QuantityHeld ?? 0));
            return await Update(currentHolding);
        }
        catch (Exception e)
        {
            // Log any exceptions to the console.  Todo: Add proper logging.
            Console.WriteLine(e);
            return 0;
        }
    }
}