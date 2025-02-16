using CommunityToolkit.Mvvm.ComponentModel;
using Signals.ApplicationLayer.Abstract;
using Signals.CoreLayer.Entities;

namespace Signals.ViewModels;

public partial class WatchlistItemDetailViewModel(IWatchlistService watchlistService) : PageViewModel(PageNames.WatchlistItemDetail,
    "Watchlist Item Detail", "Watchlist Item Detail")
{
    [ObservableProperty] private WatchlistItem _watchlistItem = new("symbol", "NYSE", "Nvidia", "USD") { };
}