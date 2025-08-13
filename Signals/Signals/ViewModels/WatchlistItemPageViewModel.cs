using System;
using System.Threading.Tasks;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Signals.ApplicationLayer.Abstract;
using Signals.ApplicationLayer.Services;
using Signals.CoreLayer.Abstract.Base;
using Signals.CoreLayer.Entities;
using Signals.CoreLayer.Enums;
using Signals.Factories;
using Signals.InfrastructureLayer.Abstract;

namespace Signals.ViewModels;

public partial class WatchlistItemPageViewModel : PageViewModel
{
    public PageFactory PageFactory { get; }
    public MainViewModel MainViewModel { get; }
    public DialogService DialogService { get; }
    public IQuotationServiceAdapter QuotationService { get; }
    private IWatchlistService WatchlistService { get; }
    public IHoldingService HoldingService { get; }
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
        IQuotationServiceAdapter quotationService,
        IWatchlistService watchlistService,
        IHoldingService holdingService,
        IMapper mapper) : base("Watchlist Item Detail", "Watchlist Item Detail")
    {
        MainViewModel = mainViewModel;
        DialogService = dialogService;
        QuotationService = quotationService;
        WatchlistService = watchlistService;
        HoldingService = holdingService;
        Mapper = mapper;
        PageFactory = pageFactory;
    }

    public async Task LoadData(string symbol)
    {
        WatchlistItem = await WatchlistService.GetBySymbol(symbol);
        
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

    [RelayCommand]
    private async Task Buy(WatchlistItem watchlistItem)
    {
        var quote = await QuotationService.GetQuoteAsync(watchlistItem.Symbol);
        
        var buyOrSellDialog = new BuyOrSellDialogViewModel()
        {
            Title = $"Buy {watchlistItem.Name}",
            Action = TransactionTypes.Purchase,
            Symbol = watchlistItem.Symbol,
            Price = (quote! == null!) ? 0 : quote.LatestQuotedPrice
        };

        Mapper.Map(watchlistItem, buyOrSellDialog);
        //Mapper.Map(quote, buyOrSellDialog);
        
        await DialogService.ShowDialog(MainViewModel, buyOrSellDialog);
        
        if (buyOrSellDialog.IsConfirmed == false) return;

        // Perform the Buy operation.
        
        var holding = Mapper.Map<Holding>(watchlistItem);
        holding.Symbol = buyOrSellDialog.Symbol;
        holding.QuantityHeld = buyOrSellDialog.Units;
        holding.AveragePurchasePrice = buyOrSellDialog.Price;
        await HoldingService.Buy(holding);
        
        MainViewModel.GoToHoldingDetailCommand.Execute(holding.Symbol);

        // Todo: Convert to logging
        Console.WriteLine(Resources.Resources.WatchlistItemPageViewModel_Buy_Purchased__0__units_of__1__for__2_C__each_on__3_MMMM_dd_yyyy__at__4_h_mm_ss_tt_zz_, buyOrSellDialog.Units, buyOrSellDialog.Symbol, buyOrSellDialog.Price, buyOrSellDialog.TransactionTime, buyOrSellDialog.TransactionTime);
    }
    
}