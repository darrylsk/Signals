using System;
using System.Threading.Tasks;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Signals.ApplicationLayer.Abstract;
using Signals.Controls;
using Signals.CoreLayer.Entities;
using Signals.Factories;
using Signals.InfrastructureLayer.Abstract;

namespace Signals.ViewModels;

public partial class AddItemPageViewModel : PageViewModel
{
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(TransactionDateTime))]
    private DateTimeOffset _transactionDate = DateTime.UtcNow.Date;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(TransactionDateTime))]
    private TimeSpan _transactionTime = DateTime.UtcNow.TimeOfDay;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SaveIsEnabled))]
    [NotifyPropertyChangedFor(nameof(EditIsEnabled))]
    private string _symbol;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(SaveIsEnabled))]
    private bool _addToWatchlist;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(SaveIsEnabled))]
    private bool _addToHoldings;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(SaveIsEnabled))]
    private decimal? _purchasePrice;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(SaveIsEnabled))]
    private int? _unitsPurchased;

    public IQuotationServiceAdapter QuotationService { get; }
    public ICompanyProfileService CompanyProfileService { get; }
    public IWatchlistService WatchlistService { get; }
    public IHoldingService HoldingService { get; }
    public MainViewModel MainViewModel { get; }
    public IMapper Mapper { get; }
    public PageFactory PageFactory { get; }
    public DateTime TransactionDateTime => TransactionDate.DateTime + TransactionTime;

    /// <summary>
    /// Design constructor
    /// </summary>
    public AddItemPageViewModel() : base("AddItem", "Add item")
    {
    }

    public AddItemPageViewModel(
        IQuotationServiceAdapter quotationService,
        ICompanyProfileService companyProfileService,
        IWatchlistService watchlistService,
        IHoldingService holdingService,
        MainViewModel mainViewModel,
        IMapper mapper,
        PageFactory pageFactory)
        : base("AddItem", "Add item")
    {
        QuotationService = quotationService;
        CompanyProfileService = companyProfileService;
        WatchlistService = watchlistService;
        HoldingService = holdingService;
        MainViewModel = mainViewModel;
        Mapper = mapper;
        PageFactory = pageFactory;
    }


    public bool EditIsEnabled => !string.IsNullOrEmpty(Symbol);

    public bool SaveIsEnabled
        => !string.IsNullOrEmpty(Symbol) && (AddToWatchlist && !AddToHoldings ||
                                             AddToHoldings && UnitsPurchased is > 0 && PurchasePrice is > 0);

    /// <summary>
    /// Add a new symbol to the list of tracked stock items.
    /// Add to the watchlist and/or holdings list in accordance with selections.
    /// </summary>
    [RelayCommand]
    private async Task AddSymbol(PageViewModel viewModel)
    {
        PageViewModel navigateToPage = PageFactory.GetPageViewModel<HomePageViewModel>();
        var navigateToPageTitle = "Home";

        try
        {
            // Call on the profile service to retrieve the company info.
            var profile = await QuotationService.GetProfileAsync(Symbol);
            if (profile! == null!)
                throw new NullReferenceException("The symbol was not recognized by the quotation service");

            // Call the quotation service to retrieve the latest quote for the given symbol
            var quotation = await QuotationService.GetQuoteAsync(Symbol);

            // Call the company profile service to add the new info if not already present.
            var existingProfile = await CompanyProfileService.GetBySymbol(profile.Symbol);
            if (existingProfile! == null!) await CompanyProfileService.Add(profile);

            // If watchlist is selected and item is already in the watchlist, it will not be updated; the original record
            // will remain intact.  If the holding does not already exist, the profile and latest quote information
            // will be added to their respective tables.
            // Call on the holdings service to add the symbol to the holdings list.
            // The service will handle any necessary logic.

            if (AddToWatchlist && quotation! != null!)
            {
                // Call on the watchlist service to add the symbol to the watch list if not already there.
                var existing = await WatchlistService.GetBySymbol(profile.Symbol);
                if (existing! == null!)
                {
                    var watchlistItem = Mapper.Map<WatchlistItem>(quotation);

                    // Grab values from the company profile.
                    // Todo: Create an association to the company profile instead of copying these values. 
                    watchlistItem.Symbol = profile.Symbol;
                    watchlistItem.Name = profile.Name;
                    watchlistItem.ExchangeName = profile.Exchange;
                    watchlistItem.CurrencyCode = profile.Currency;

                    // Save to watchlist
                    await WatchlistService.Add(watchlistItem);

                    navigateToPage = PageFactory.GetPageViewModel<WatchlistPageViewModel>();
                    navigateToPageTitle = "Watchlist";
                }
            }

            if (AddToHoldings && quotation! != null!)
            {
                var holding = Mapper.Map<Holding>(quotation);

                // Grab values from the company profile.
                // Todo: Create an association to the company profile instead of copying these values. 
                holding.Symbol = profile.Symbol;
                holding.Name = profile.Name;
                holding.ExchangeName = profile.Exchange;
                holding.CurrencyCode = profile.Currency;

                // Grab the values entered on the view 
                holding.AveragePurchasePrice = PurchasePrice;
                holding.PeakPriceSincePurchase = PurchasePrice;
                holding.QuantityHeld = (holding.QuantityHeld ?? 0) + (UnitsPurchased ?? 0);
                holding.WhenLastPurchased = TransactionDateTime; 

                // Save holding, recalculate the average price, and add to the holding list.
                await HoldingService.Buy(holding);

                navigateToPage = PageFactory.GetPageViewModel<HoldingsPageViewModel>();
                navigateToPageTitle = "Holdings";
            }
        }
        catch (Exception ex)
        {
            // Log the exception to console.  Todo: Add proper logging.
            Console.WriteLine(ex);
        }

        MainViewModel.CurrentPage = navigateToPage ?? PageFactory.GetPageViewModel<WatchlistPageViewModel>();
        MainViewModel.PageTitle = navigateToPageTitle;
    }

    [RelayCommand]
    private void Cancel(PageViewModel viewModel)
    {
        // Navigate back to the list.
        BackLink.CurrentPage = viewModel ?? PageFactory.GetPageViewModel<WatchlistPageViewModel>();
    }

    [RelayCommand]
    private async Task LookupSymbol()
    {
        var profile = await QuotationService.GetProfileAsync(Symbol);
        if (profile! == null!) return;
        var quote = await QuotationService.GetQuoteAsync(Symbol);
        if (quote! == null!) return;

        var item = Mapper.Map<WatchlistItem>(quote);
        item.Name = profile?.Name;
        item.Symbol = profile?.Symbol;
        if (Symbol != profile.Symbol) Symbol = profile?.Symbol;

        PurchasePrice = quote?.LatestQuotedPrice;
    }
}