using System;
using System.Threading.Tasks;
using AutoMapper;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using Signals.ApplicationLayer.Abstract;
using Signals.ApplicationLayer.Services;
using Signals.CoreLayer.Entities;
using Signals.Factories;
using Signals.ViewModels.Abstract;

namespace Signals.ViewModels;

public partial class WatchlistItemPageViewModel : PageViewModel
{
    public PageFactory PageFactory { get; }
    public MainViewModel MainViewModel { get; }
    public DialogService DialogService { get; }
    private IWatchlistService WatchlistService { get; }
    public IMapper Mapper { get; }
    
    [ObservableProperty] private WatchlistItem? _watchlistItem;
    // [ObservableProperty] private DialogViewModel _currentDialog = new ConfirmDialogViewModel()
    // {
    //     IsDialogOpen = true
    // };

    public WatchlistItemPageViewModel()
        : base("Watchlist Item Detail", "Watchlist Item Detail")
    {
    }

    /// <inheritdoc/>
    public WatchlistItemPageViewModel(
        PageFactory pageFactory,
        MainViewModel mainViewModel,
        DialogService dialogService,
        IWatchlistService watchlistService,
        IMapper mapper) : base("Watchlist Item Detail", "Watchlist Item Detail")
    {
        MainViewModel = mainViewModel;
        DialogService = dialogService;
        WatchlistService = watchlistService;
        Mapper = mapper;
        PageFactory = pageFactory;
    }

    public async Task<WatchlistItemPageViewModel> LoadData(string symbol)
    {
        WatchlistItem = await WatchlistService.GetBySymbol(symbol);
        if (WatchlistItem! == null!) return null!;
        
        var vm = Mapper.Map<WatchlistItem, WatchlistItemPageViewModel>(WatchlistItem);
        return vm;
    }

    [RelayCommand]
    private async Task DeleteWatchlistItem(WatchlistItem watchlistItem)
    {
        var confirmDialog = new ConfirmDialogViewModel()
        {
            Message = $"Are you sure you want to delete {watchlistItem.Name}?"
        };

        await DialogService.ShowDialog(MainViewModel, confirmDialog);
        
        if (confirmDialog.IsConfirmed == false) return;
        
        // Todo: Convert to logging
        // Console.WriteLine(Resources.Resources.WatchlistItemPageViewModel_DeleteWatchlistItem_Deleted__0_,
        // watchlistItem?.Name);

        // delete the current item
        if (watchlistItem! != null!) await WatchlistService.Delete(watchlistItem);
        
        // navigate back to the list
        MainViewModel.GoToWatchlistCommand.Execute(null);
    }
    
}