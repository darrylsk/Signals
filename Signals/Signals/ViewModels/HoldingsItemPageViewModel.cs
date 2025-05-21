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
    
}