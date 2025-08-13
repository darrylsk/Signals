using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Signals.ApplicationLayer.Abstract;
using Signals.CoreLayer.Entities;
using Signals.Factories;

namespace Signals.ViewModels;

public partial class HoldingsPageViewModel : PageViewModel
{
    public IHoldingService HoldingService { get; }
    public IMapper HoldingsMapper { get; }
    public PageFactory PageFactory { get; }
    
    [ObservableProperty] private IEnumerable<Holding> _holdings;
    [ObservableProperty] private PageViewModel _currentPage;

    public HoldingsPageViewModel() : base("Holdings",
        "Items that I own in my portfolio")
    {
        
        // _holdings = 
        // [
        //     new("NVDA","NYSE",  "Nvidia", "USD"),
        //     new("AAPL","NYSE",  "Apple", "USD"),
        //     new("MSFT","NYSE",  "Microsoft", "USD"),
        //     new("RUM", "NYSE", "Rumble", "USD"),
        //     new("REGN","NYSE",  "Regeneron Pharmaceuticals Inc", "USD"),
        // ];
    }
    
    /// <inheritdoc/>
    public HoldingsPageViewModel(
        IHoldingService holdingService,
        IMapper holdingsMapper,
        PageFactory pageFactory
        ): base("Holdings", "Items that I own in my portfolio")
    {
        PageFactory = pageFactory;
        HoldingService = holdingService;
        HoldingsMapper = holdingsMapper;
        LoadData();
    }

    private async Task LoadData()
    {
        Holdings = await HoldingService.GetAll();
        Console.WriteLine(Holdings.Count());
    }
}