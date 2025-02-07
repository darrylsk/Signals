using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Signals.Data;
using Signals.Factories;

namespace Signals.ViewModels; 

public partial class MainViewModel : ViewModelBase //PageViewModel
{
    private readonly PageFactory _pageFactory;

    [ObservableProperty]
    private string _title = $"$ignal$";
    [ObservableProperty]
    private string _subtitle = $"Watch what happens";
    [ObservableProperty] private string _pageTitle;
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HomePageIsActive))]
    [NotifyPropertyChangedFor(nameof(WatchlistPageIsActive))]
    [NotifyPropertyChangedFor(nameof(HoldingsPageIsActive))]
    [NotifyPropertyChangedFor(nameof(SettingsPageIsActive))]
    private PageViewModel _currentPage;

    public bool HomePageIsActive => CurrentPage.PageName == PageNames.Home;
    public bool WatchlistPageIsActive => CurrentPage.PageName == PageNames.Watchlist;
    public bool HoldingsPageIsActive => CurrentPage.PageName == PageNames.Holdings;
    public bool SettingsPageIsActive => CurrentPage.PageName == PageNames.Settings;

    /// <summary>
    /// Design-time only constructor
    /// </summary>
    public MainViewModel()
    {
        CurrentPage = new HomePageViewModel();
    }
    public MainViewModel(PageFactory pageFactory) : base()
    {
        _pageFactory = pageFactory;
        _currentPage = pageFactory.GetPageViewModel(PageNames.Home);
        GoToWatchlist();
    }

    [RelayCommand]
    private void GoToHome()
    {
        CurrentPage = _pageFactory.GetPageViewModel(PageNames.Home);
        PageTitle = "Home";
    }

    [RelayCommand]
    private void GoToWatchlist()
    {
        CurrentPage = _pageFactory.GetPageViewModel(PageNames.Watchlist);
        PageTitle = "Watchlist";
    }

    [RelayCommand]
    private void GoToHoldings()
    {
        CurrentPage =  _pageFactory.GetPageViewModel(PageNames.Holdings);
        PageTitle = "Holdings";
    }

    [RelayCommand]
    private void GoToSettings()
    {
        CurrentPage = _pageFactory.GetPageViewModel(PageNames.Settings);
        PageTitle = "Settings";
    }

    [RelayCommand]
    private void GoToWatchlistDetail()
    {
        CurrentPage = _pageFactory.GetPageViewModel(PageNames.WatchlistItemDetail);
    }
}