using System.Threading.Tasks;

namespace Signals.ApplicationLayer.Abstract;

public interface IPriceRefreshService
{
    Task<int> UpdateWatchlistPrices();
    Task<int> UpdateHoldingPrices();
    Task<int> UpdateIndexPrices();
}