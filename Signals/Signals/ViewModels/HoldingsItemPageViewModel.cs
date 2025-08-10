using System;
using System.Threading.Tasks;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Signals.ApplicationLayer.Abstract;
using Signals.CoreLayer.Entities;
using Signals.Factories;

namespace Signals.ViewModels;

public partial class HoldingsItemPageViewModel : PageViewModel
{
    public PageFactory PageFactory { get; }
    private IHoldingService HoldingService { get; }
    public IMapper Mapper { get; }
    [ObservableProperty] private Holding? _holdingItem;

    // Todo: decide whether these need to be observables (they probably do).
    public string Symbol { get; set; }
    public string Name { get; set; }
    
    public decimal QuantityHeld { get; set; }
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
        IHoldingService holdingService,
        IMapper mapper
        )
        : base("Holdings Item Detail", "Holdings Item Detail")
    {
        PageFactory = pageFactory;
        HoldingService = holdingService;
        Mapper = mapper;
    }

    public async Task LoadData(string symbol)
    {
        HoldingItem = await HoldingService.GetBySymbol(symbol);
        
        // Would like to be able to do this to populate the view model, but this navigation mechanism
        // does not provide any easily discoverable way to do it.
        // var vm = Mapper.Map<HoldingsItemPageViewModel>(HoldingItem);
        // Alternative solution: 
        // Mapper.Map(HoldingItem, this);
        // This is what we'd do if we didn't have AutoMapper
        // foreach (var propertyInfo  in GetType().GetProperties())
        // {
        //     if (!propertyInfo.CanWrite || propertyInfo.Name == "HoldingItem") continue;
        //     
        //     Console.WriteLine($"Property: {propertyInfo.Name}  Value: {propertyInfo.GetValue(vm)}");
        //     var retrievedValue  = propertyInfo.GetValue(vm);
        //     propertyInfo.SetValue(this, retrievedValue);
        // }
        // for example, the following doesn't work for obvious reasons, and there is no work-around
        // this = vm;
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
