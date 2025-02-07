using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using Signals.ApplicationLayer.Abstract;
using Signals.Data;
using Signals.Factories;

namespace Signals.ViewModels;

public partial class WatchlistPageViewModel : PageViewModel
{
    public IWatchlistService WatchlistService { get; }
    public PageFactory PageFactory { get; }

    [ObservableProperty] private IEnumerable<WatchlistItem> _watchlist;

    /// <inheritdoc/>
    public WatchlistPageViewModel(IWatchlistService watchlistService, PageFactory pageFactory) 
        : base(PageNames.Watchlist,
        "Watchlist",
        "Tracking only - not part of portfolio")
    {
        WatchlistService = watchlistService;
        PageFactory = pageFactory;
        LoadData();
    }

    [RelayCommand]
    private async void LoadData()
    {
        Watchlist = await WatchlistService.GetAll();
    }
    
    [RelayCommand]
    private void GoToWatchlistDetail()
    {
        //CurrentPage = _pageFactory.GetPageViewModel(PageNames.WatchlistItemDetail);
        PageFactory.GetPageViewModel(PageNames.WatchlistItemDetail);
    }
}