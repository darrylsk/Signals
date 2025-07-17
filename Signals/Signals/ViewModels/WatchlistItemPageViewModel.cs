using System.Threading.Tasks;
using AutoMapper;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using Signals.ApplicationLayer.Abstract;
using Signals.CoreLayer.Entities;
using Signals.Factories;

namespace Signals.ViewModels;

public partial class WatchlistItemPageViewModel : PageViewModel
{
    public PageFactory PageFactory { get; }
    private IWatchlistService WatchlistService { get; }
    public IMapper Mapper { get; }
    
    [ObservableProperty] private WatchlistItem? _watchlistItem;

    public WatchlistItemPageViewModel()
        : base("Watchlist Item Detail", "Watchlist Item Detail")
    { }

    /// <inheritdoc/>
    public WatchlistItemPageViewModel(
        PageFactory pageFactory,
        IWatchlistService watchlistService,
        IMapper mapper) : base("Watchlist Item Detail", "Watchlist Item Detail")
    {
        WatchlistService = watchlistService;
        Mapper = mapper;
        PageFactory = pageFactory;
    }

    public async Task LoadData(string symbol)
    {
        WatchlistItem = await WatchlistService.GetBySymbol(symbol);
    }

    [RelayCommand]
    public async Task DeleteWatchlistItem()
    {
        var box = MessageBoxManager
            .GetMessageBoxStandard("Delete", "Are you sure you would like to delete this watchlist item?",
                ButtonEnum.YesNo, 
                Icon.Question,
                WindowStartupLocation.CenterOwner);

        var result = await box.ShowAsync();
        if (result == ButtonResult.Yes)
        {
            // delete the current item
        }
    }
    
}