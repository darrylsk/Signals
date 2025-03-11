using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Signals.ApplicationLayer.Abstract;
using Signals.CoreLayer.Entities;

namespace Signals.ViewModels;

public partial class WatchlistItemPageViewModel : PageViewModel
{
    private IWatchlistService WatchlistService { get; }
    [ObservableProperty] private WatchlistItem? _watchlistItem; // = new("symbol", "NYSE", "Nvidia", "USD") { };

    public WatchlistItemPageViewModel():base(
        "Watchlist Item Detail", "Watchlist Item Detail")
    {
    }

    /// <inheritdoc/>
    public WatchlistItemPageViewModel(IWatchlistService watchlistService) 
        : base("Watchlist Item Detail", "Watchlist Item Detail")
    {
        WatchlistService = watchlistService;
    }
    
    public async Task LoadData(string symbol)
    {
       WatchlistItem = await WatchlistService.GetBySymbol(symbol);
    }

    // public void LoadData(string symbol)
    // {
    //    WatchlistItem = (WatchlistService.GetBySymbol(symbol)).Result;
    // }

}