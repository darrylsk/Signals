using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Signals.ApplicationLayer.Abstract;
using Signals.CoreLayer.Entities;

namespace Signals.ViewModels;

public partial class HoldingsItemPageViewModel : PageViewModel
{
    public IHoldingService HoldingService { get; }
    [ObservableProperty] private Holding? _holdingItem;
    
    public HoldingsItemPageViewModel()
        : base("Holdings Item Detail", "Holdings Item Detail")
    { }

    public HoldingsItemPageViewModel(IHoldingService holdingService)
        : base("Holdings Item Detail", "Holdings Item Detail")
    {
        HoldingService = holdingService;
    }

    public async Task LoadData(string symbol)
    {
        HoldingItem = await HoldingService.GetBySymbol(symbol);
    }
}