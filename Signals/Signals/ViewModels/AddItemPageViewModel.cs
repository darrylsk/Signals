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
    public AddItemPageViewModel(): base("AddItem", "Add item")
    { }
    
    public IQuotationServiceAdapter QuotationService { get; }
    public ICompanyProfileService CompanyProfileService { get; }
    public IWatchlistService WatchlistService { get; }
    public IHoldingService HoldingService { get; }
    public IMapper Mapper { get; }
    public PageFactory PageFactory { get; }

    /// <summary>
    /// Design constructor
    /// </summary>
    // public AddItemPageViewModel() : base("AddItem", "Add item")
    // {
    // }
    public AddItemPageViewModel(
        IQuotationServiceAdapter quotationService,
        ICompanyProfileService companyProfileService,
        IWatchlistService watchlistService,
        IHoldingService holdingService,
        IMapper mapper,
        PageFactory pageFactory)
        : base("AddItem", "Add item")
    {
        QuotationService = quotationService;
        CompanyProfileService = companyProfileService;
        WatchlistService = watchlistService;
        HoldingService = holdingService;
        Mapper = mapper;
        PageFactory = pageFactory;
    }


    [ObservableProperty] [NotifyPropertyChangedFor(nameof(SaveIsEnabled))]
    private string _symbol;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(SaveIsEnabled))]
    private bool _addToWatchlist;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(SaveIsEnabled))]
    private bool _addToHoldings;

    public bool SaveIsEnabled
        => !string.IsNullOrEmpty(Symbol) && (AddToWatchlist || AddToHoldings);

    /// <summary>
    /// Add a new symbol to the list of tracked stock items.
    /// Add to the watchlist and/or holdings list in accordance with selections.
    /// </summary>
    [RelayCommand]
    private async Task AddSymbol(PageViewModel viewModel)
    {
        try
        {
            // Call on the profile service to retrieve the company info.
            var profile = await QuotationService.GetProfileAsync(Symbol);

            if (profile! == null!)
                throw new NullReferenceException("The symbol was not recognized by the quotation service");

            // Call the company profile service to add the new info if not already present.
            var existingProfile = await CompanyProfileService.GetBySymbol(Symbol);
            if (existingProfile! == null!) await CompanyProfileService.Add(profile);

            // Call the quotation service to retrieve the latest quote for the given symbol
            var quotation = await QuotationService.GetQuoteAsync(Symbol);
            // If watchlist is selected and item is already in the watchlist, it will not be updated; the original record
            // will remain intact.  If the holding does not already exist, the profile and latest quote information
            // will be added to their respective tables.
            // Call on the holdings service to add the symbol to the holdings list.
            // The service will handle any necessary logic.

            if (AddToWatchlist && quotation! != null!)
            {
                // Call on the watchlist service to add the symbol to the watch list if not already there.
                var existing = await WatchlistService.GetBySymbol(Symbol);
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
                }
            }

            if (AddToHoldings && quotation! != null!)
            {
                // If holding is selected and the item is already in the holdings list, it will not be updated; the original
                // holding will remain intact.  Units can be added to holdings or removed only from the holdings detail page
                // using the Buy() and Sell() functions.
                var existing = await HoldingService.GetBySymbol(Symbol);
                if (existing! != null!) return;

                // Pop up a new dialog to allow the user to enter the number of units bought, adjust the staring price and/or
                // date and time, if the new item is a holding.

                // Then create the holding record and return to the holdings edit page.
                var holding = Mapper.Map<Holding>(quotation);

                // Grab values from the company profile.
                // Todo: Create an association to the company profile instead of copying these values. 
                holding.Symbol = profile.Symbol;
                holding.Name = profile.Name;
                holding.ExchangeName = profile.Exchange;
                holding.CurrencyCode = profile.Currency;
                
                // Save to holdings list
                await HoldingService.Buy(holding);

                BackLink.CurrentPage = PageFactory.GetPageViewModel<HoldingsPageViewModel>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }

        // Navigate back to the list.
        BackLink.CurrentPage = viewModel ?? PageFactory.GetPageViewModel<WatchlistPageViewModel>();
    }

    [RelayCommand]
    private void Cancel(PageViewModel viewModel)
    {
        // Navigate back to the list.
        BackLink.CurrentPage = viewModel ?? PageFactory.GetPageViewModel<WatchlistPageViewModel>();
    }
}