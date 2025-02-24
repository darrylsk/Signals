using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using Signals.ApplicationLayer.Abstract;
using Signals.CoreLayer.Entities;
using Signals.Factories;

namespace Signals.ViewModels;

public partial class WatchlistPageViewModel : PageViewModel
{
    public IWatchlistService WatchlistService { get; }
    public IMapper WatchlistMapper { get; }
    public IMapper Mapper { get; }
    public PageFactory PageFactory { get; }

    [ObservableProperty] private IEnumerable<WatchlistItem> _watchlist;
    [ObservableProperty] private PageViewModel _currentPage;

    
    /// <summary>
    /// Design constructor to satisfy the previewer
    /// </summary>
    public WatchlistPageViewModel():base(PageNames.Watchlist, "Watchlist",
        "Tracking only - not part of portfolio")
    { }
    
    /// <inheritdoc/>
    public WatchlistPageViewModel(
        IWatchlistService watchlistService,
        IMapper watchlistMapper,
        PageFactory pageFactory) 
        : base(PageNames.Watchlist,
        "Watchlist",
        "Tracking only - not part of portfolio")
    {
        WatchlistService = watchlistService;
        WatchlistMapper = watchlistMapper;
        PageFactory = pageFactory;
        LoadData();
    }

    public async Task LoadData()
    {
        Watchlist = await WatchlistService.GetAll();
    }
    
    [RelayCommand]
    private async Task GoToWatchlistDetail(string symbol)
    {
        Console.WriteLine(symbol);
        var watchlistItem = await WatchlistService.GetBySymbol(symbol);
        // var watchlistItem = await WatchlistService.GetById(new Guid());
        
        // var viewModel = WatchlistMapper.Map<WatchlistItem,WatchlistItemPageViewModel>(watchlistItem);
        // CurrentPage = _pageFactory.GetPageViewModel(PageNames.WatchlistItemDetail);
        CurrentPage = PageFactory.GetPageViewModel<WatchlistItemPageViewModel>(
               async vm => await vm.LoadData(symbol));
    }
}