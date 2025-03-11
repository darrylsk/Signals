using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Signals.Factories;

namespace Signals.ViewModels;

public partial class MainViewModel : ViewModelBase 
{
    private readonly PageFactory _pageFactory;

    [ObservableProperty]
    private string _appTitle = $"$ignal$";
    [ObservableProperty]
    private string _tagLine = $"Watch what happens";
    [ObservableProperty] private string _pageTitle;

    // [ObservableProperty]
    // [NotifyPropertyChangedFor(nameof(HomePageIsActive))]
    // [NotifyPropertyChangedFor(nameof(WatchlistPageIsActive))]
    // [NotifyPropertyChangedFor(nameof(HoldingsPageIsActive))]
    // [NotifyPropertyChangedFor(nameof(SettingsPageIsActive))]
    // private PageViewModel _currentPage;

    // public bool HomePageIsActive => CurrentPage.PageName == PageNames.Home;
    // public bool WatchlistPageIsActive => CurrentPage.PageName == PageNames.Watchlist;
    // public bool HoldingsPageIsActive => CurrentPage.PageName == PageNames.Holdings;
    // public bool SettingsPageIsActive => CurrentPage.PageName == PageNames.Settings;

    /// <summary>
    /// Design-time only constructor
    /// </summary>
    public MainViewModel()
    {
        CurrentPage = _pageFactory.GetPageViewModel<HomePageViewModel>();
    }

    public MainViewModel(PageFactory pageFactory)
    {
        _pageFactory = pageFactory;
        CurrentPage = pageFactory.GetPageViewModel<HomePageViewModel>();
        GoToWatchlist();
    }

    [RelayCommand]
    private void GoToHome()
    {
        CurrentPage = _pageFactory.GetPageViewModel<HomePageViewModel>(
            afterCreation => afterCreation.PageSubtitle = "After-creation executed.");
        PageTitle = "Home";
    }

    [RelayCommand]
    private void GoToWatchlist()
    {
        CurrentPage = _pageFactory.GetPageViewModel<WatchlistPageViewModel>();
        PageTitle = "Watchlist";
    }

    [RelayCommand]
    private void GoToHoldings()
    {
        CurrentPage =  _pageFactory.GetPageViewModel<HoldingsPageViewModel>();
        PageTitle = "Holdings";
    }

    [RelayCommand]
    private void GoToSettings()
    {
        CurrentPage = _pageFactory.GetPageViewModel<SettingsPageViewModel>(async vm 
            => await vm.LoadSettings());
        PageTitle = "Settings";
    }

    [RelayCommand]
    private void GoToWatchlistDetail(string symbol)
    {
        CurrentPage = _pageFactory.GetPageViewModel<WatchlistItemPageViewModel>(async vm 
            => await vm.LoadData(symbol));
        PageTitle = "Watchlist Item";
    }

    [RelayCommand]
    private void GoToAddItem()
    {
        CurrentPage = _pageFactory.GetPageViewModel<AddItemPageViewModel>();
        CurrentPage.BackLink = this;
        PageTitle="Add Item";
    }

    [RelayCommand]
    private void GotoAbout()
    {
        CurrentPage = _pageFactory.GetPageViewModel<AboutPageViewModel>();
        CurrentPage.BackLink = this;
        PageTitle = "About";
    }

    [RelayCommand]
    private void GotoUpdates()
    {
        CurrentPage = _pageFactory.GetPageViewModel<UpdatesPageViewModel>();
        CurrentPage.BackLink = this;
        PageTitle = "Updates";
    }

    [RelayCommand]
    private void GotoQuoteLog()
    {
        CurrentPage = _pageFactory.GetPageViewModel<QuoteLogPageViewModel>();
        CurrentPage.BackLink = this;
        PageTitle = "Log";
    }
}