using System;
using System.Threading.Tasks;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Signals.ApplicationLayer.Abstract;
using Signals.ApplicationLayer.Services;
using Signals.CoreLayer.Entities;
using Signals.CoreLayer.Enums;
using Signals.Factories;
using Signals.InfrastructureLayer.Abstract;

namespace Signals.ViewModels;

public partial class HoldingsItemPageViewModel : PageViewModel
{
    public PageFactory PageFactory { get; }
    public MainViewModel MainViewModel { get; }
    public DialogService DialogService { get; }
    public IQuotationServiceAdapter QuotationService { get; }
    private IHoldingService HoldingService { get; }
    public IMapper Mapper { get; }
    [ObservableProperty] private Holding? _holdingItem;
    [ObservableProperty] private decimal _quantityHeld;
    
    // Todo: decide whether these need to be observables (they probably do).
    public string Symbol { get; set; }
    public string Name { get; set; }
    
    public DateTime WhenPurchased { get; set; }
    public decimal? PeakPriceSincePurchase { get; set; }
    public decimal? HighTargetPrice { get; set; }
    public decimal? LowTargetPrice { get; set; }
    public double TrailingStop { get; set; }

    public bool UseTrailingStop { get; set; }    
    public decimal? GainOrLoss => HoldingItem?.PeakPriceSincePurchase - HoldingItem?.LatestQuotedPrice; 
    
    public HoldingsItemPageViewModel()
        : base("Holdings Item Detail", "Holdings Item Detail")
    { }

    public HoldingsItemPageViewModel(
        PageFactory pageFactory,
        MainViewModel mainViewModel,
        DialogService dialogService,
        IQuotationServiceAdapter quotationService,
        IHoldingService holdingService,
        IMapper mapper
        )
        : base("Holdings Item Detail", "Holdings Item Detail")
    {
        PageFactory = pageFactory;
        MainViewModel = mainViewModel;
        DialogService = dialogService;
        QuotationService = quotationService;
        HoldingService = holdingService;
        Mapper = mapper;
    }

    public async Task LoadData(string symbol)
    {
        HoldingItem = await HoldingService.GetBySymbol(symbol);
        if (HoldingItem == null) 
            MainViewModel.GoToHoldingsCommand.Execute(null);
    }

    [RelayCommand]
    public async Task Buy(Holding holding)
    {
        var quote = await QuotationService.GetQuoteAsync(holding.Symbol);
        
        var buyOrSellViewDialogModel = new BuyOrSellDialogViewModel()
        {
            Title = $"Buy {holding.Name}",
            Action = TransactionTypes.Purchase,
            Symbol = holding.Symbol,
            Price = (quote! == null!) ? 0 : quote.LatestQuotedPrice
        };
        
        await DialogService.ShowDialog(MainViewModel, buyOrSellViewDialogModel);
        
        if (buyOrSellViewDialogModel.IsConfirmed == false) return;
        
        holding.QuantityHeld = buyOrSellViewDialogModel.Units.Value;
        holding.AveragePurchasePrice = buyOrSellViewDialogModel.Price.Value;

        await HoldingService.Buy(holding);

        MainViewModel.GoToHoldingDetailCommand.Execute(holding.Symbol);
    }

    [RelayCommand]
    public async Task Sell(Holding holding)
    {
        var quote = await QuotationService.GetQuoteAsync(holding.Symbol);
        
        var buyOrSellViewDialogModel = new BuyOrSellDialogViewModel()
        {
            Title = $"Sell {holding.Name}",
            Action = TransactionTypes.Sale,
            Symbol = holding.Symbol,
            Price = (quote! == null!) ? 0 : quote.LatestQuotedPrice
        };
        
        await DialogService.ShowDialog(MainViewModel, buyOrSellViewDialogModel);
        
        if (buyOrSellViewDialogModel.IsConfirmed == false) return;

        holding.QuantityHeld = buyOrSellViewDialogModel.Units.Value;
        holding.AveragePurchasePrice = buyOrSellViewDialogModel.Price.Value;
        
        await HoldingService.Sell(holding);
        
        MainViewModel.GoToHoldingDetailCommand.Execute(holding.Symbol);
    }

}
