using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Signals.ApplicationLayer.Abstract;
using Signals.CoreLayer.Entities;
using Signals.Factories;

namespace Signals.ViewModels;

public partial class HoldingsItemPageViewModel : PageViewModel
{
    public PageFactory PageFactory { get; }
    public IHoldingService HoldingService { get; }
    [ObservableProperty] private Holding? _holdingItem;

    public decimal? GainOrLoss => HoldingItem?.PeakPriceSincePurchase - HoldingItem?.LatestQuotedPrice; 
    
    public HoldingsItemPageViewModel()
        : base("Holdings Item Detail", "Holdings Item Detail")
    { }

    public HoldingsItemPageViewModel(
        PageFactory pageFactory,
        IHoldingService holdingService
        )
        : base("Holdings Item Detail", "Holdings Item Detail")
    {
        PageFactory = pageFactory;
        HoldingService = holdingService;
    }

    public async Task LoadData(string symbol)
    {
        HoldingItem = await HoldingService.GetBySymbol(symbol);
    }

    [RelayCommand]
    public async Task Buy(Holding entity)
    {
        await HoldingService.Buy(entity);
    }

    [RelayCommand]
    public async Task Sell(Holding entity)
    {
        await HoldingService.Sell(entity, 10.00M, 5.0M);
    }

}