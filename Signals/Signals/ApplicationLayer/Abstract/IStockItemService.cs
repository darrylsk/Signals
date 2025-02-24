using System.Threading.Tasks;

namespace Signals.ApplicationLayer.Abstract;

public interface IStockItemService<T> : IBusinessService<T>
{
    Task<T?> GetBySymbol(string symbol);

}