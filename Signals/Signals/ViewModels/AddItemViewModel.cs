using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Signals.ApplicationLayer.Abstract;
using Signals.CoreLayer.Entities;
using Signals.Factories;

namespace Signals.ViewModels;

public partial class AddItemViewModel : PageViewModel
{
    public IWatchlistService WatchlistService { get; }
    public PageFactory PageFactory { get; }

    /// <summary>
    /// Design constructor
    /// </summary>
    public AddItemViewModel() : base(PageNames.AddItem, "AddItem",
        "Add item")
    { }

    public AddItemViewModel(
        IWatchlistService watchlistService, 
        PageFactory pageFactory)
        : base(PageNames.AddItem, "AddItem", "Add item")
    {
        WatchlistService = watchlistService;
        PageFactory = pageFactory;
    }
    
    
    [ObservableProperty] 
    [NotifyPropertyChangedFor(nameof(SaveIsEnabled))]
    private string _symbol;

    public bool SaveIsEnabled => string.IsNullOrEmpty(Symbol) == false;

    /// <summary>
    /// Add a new symbol to the list of tracked stock items.
    /// </summary>
    [RelayCommand]
    public async Task AddSymbol()
    {
        await WatchlistService.AddSymbol(Symbol);
        MainMenu.CurrentPage = PageFactory.GetPageViewModel<WatchlistPageViewModel>();
    }
}