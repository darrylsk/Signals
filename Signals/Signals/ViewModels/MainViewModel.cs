using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Signals.Factories;
using Signals.ViewModels.Abstract;

namespace Signals.ViewModels;

public partial class MainViewModel : Abstract.ViewModelBase, IDialogProvider
{
    private readonly PageFactory _pageFactory;

    [ObservableProperty] private string _appTitle = $"$ignal$";
    [ObservableProperty] private string _tagLine = $"Watch what happens";
    [ObservableProperty] private string _pageTitle;
    
    [ObservableProperty] private DialogViewModel _dialog;
    
    /// <summary>
    /// Design-time only constructor
    /// </summary>
    public MainViewModel()
    {
        _pageFactory = new PageFactory(type => new PageViewModel("Main", "main page"));
        CurrentPage = _pageFactory.GetPageViewModel<HomePageViewModel>();
        PageTitle = "Main";
    }

    public MainViewModel(PageFactory pageFactory)
    {
        _pageFactory = pageFactory;
        CurrentPage = pageFactory.GetPageViewModel<HomePageViewModel>();
        PageTitle = "Main";
        GoToHome();
    }

    [RelayCommand]
    private void GoToHome()
    {
        CurrentPage = _pageFactory.GetPageViewModel<HomePageViewModel>(
            vm =>
            {
                vm.PageSubtitle = "Home page";
            });
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
        CurrentPage.BackLink = this;
        this.PageTitle = "Watchlist Item";
    }

    [RelayCommand]
    private void GoToHoldingDetail(string symbol)
    {
        CurrentPage = _pageFactory.GetPageViewModel<HoldingsItemPageViewModel>(async vm 
            => await vm.LoadData(symbol));
        CurrentPage.BackLink = this;
        PageTitle = "Holding";
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