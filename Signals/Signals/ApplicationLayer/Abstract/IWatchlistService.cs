using System.Collections.Generic;
using System.Threading.Tasks;
using Signals.CoreLayer.Entities;

namespace Signals.ApplicationLayer.Abstract;

public interface IWatchlistService : IStockItemService<WatchlistItem>
{
    Task<int> Add(WatchlistItem model);
    Task<int> Delete(WatchlistItem model);
}