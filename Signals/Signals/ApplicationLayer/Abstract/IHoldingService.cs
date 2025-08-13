using System.Threading.Tasks;
using Signals.CoreLayer.Entities;

namespace Signals.ApplicationLayer.Abstract;

public interface IHoldingService : IStockItemService<Holding>
{
    Task<int> Buy(Holding holding);
    Task<int> Sell(Holding model);
}