using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using Signals.CoreLayer.Entities;

namespace Signals.ViewModels;

public partial class HoldingsPageViewModel : PageViewModel
{
    [ObservableProperty] private List<Holding> _holdings;

    public HoldingsPageViewModel() : base("Holdings",
        "Items that I own in my portfolio")
    {
        
        _holdings = 
        [
            new("NVDA","NYSE",  "Nvidia", "USD"),
            new("AAPL","NYSE",  "Apple", "USD"),
            new("MSFT","NYSE",  "Microsoft", "USD"),
            new("RUM", "NYSE", "Rumble", "USD"),
            new("REGN","NYSE",  "Regeneron Pharmaceuticals Inc", "USD"),
        ];
    }

}