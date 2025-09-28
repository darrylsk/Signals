using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Signals.Factories;
using Signals.ViewModels.Abstract;

namespace Signals.ViewModels;

public partial class MainViewModel : Abstract.ViewModelBase, IDialogProvider
{
    private readonly PageFactory _pageFactory;

    [ObservableProperty] private string _appTitle = Resources.Resources.ApplicationTitle;
    [ObservableProperty] private string _tagLine = Resources.Resources.ApplicationTagLine;
    [ObservableProperty] private string _pageTitle;
    
    [ObservableProperty] private DialogViewModel _dialog;
    
    /// <summary>
    /// Design-time only constructor
    /// </summary>
    public MainViewModel()
    {
        _pageFactory = new PageFactory(type => new PageViewModel( Resources.Resources.Main,
            Resources.Resources.MainPageSubtitle));
        CurrentPage = _pageFactory.GetPageViewModel<HomePageViewModel>();
        PageTitle = Resources.Resources.Main;
    }

    public MainViewModel(PageFactory pageFactory)
    {
        _pageFactory = pageFactory;
        CurrentPage = pageFactory.GetPageViewModel<HomePageViewModel>();
        PageTitle = Resources.Resources.Main;
        GoToHome();
    }

    [RelayCommand]
    private void GoToHome()
    {
        CurrentPage = _pageFactory.GetPageViewModel<HomePageViewModel>(
            vm =>
            {
                vm.PageSubtitle = Resources.Resources.HoldingsPageSubtitle;
            });
        PageTitle = Resources.Resources.Home;
    }

    [RelayCommand]
    private void GoToWatchlist()
    {
        CurrentPage = _pageFactory.GetPageViewModel<WatchlistPageViewModel>();
        PageTitle = Resources.Resources.Watchlist;
    }

    [RelayCommand]
    private void GoToHoldings()
    {
        CurrentPage =  _pageFactory.GetPageViewModel<HoldingsPageViewModel>();
        PageTitle = Resources.Resources.Holdings;
    }

    [RelayCommand]
    private void GoToSettings()
    {
        CurrentPage = _pageFactory.GetPageViewModel<SettingsPageViewModel>(async vm 
            => await vm.LoadSettings());
        PageTitle = Resources.Resources.Settings;
    } 

    [RelayCommand]
    private void GoToWatchlistDetail(string symbol)
    {
        CurrentPage = _pageFactory.GetPageViewModel<WatchlistItemPageViewModel>(async vm 
            => await vm.LoadData(symbol));
        CurrentPage.BackLink = this;
        this.PageTitle = Resources.Resources.WatchlistItem;
    }

    [RelayCommand]
    private void GoToHoldingDetail(string symbol)
    {
        CurrentPage = _pageFactory.GetPageViewModel<HoldingsItemPageViewModel>(async vm 
            => await vm.LoadData(symbol));
        CurrentPage.BackLink = this;
        PageTitle = Resources.Resources.HoldingsItemDetail;
    }

    [RelayCommand]
    private void GoToAddItem()
    {
        CurrentPage = _pageFactory.GetPageViewModel<AddItemPageViewModel>();
        CurrentPage.BackLink = this;
        PageTitle=Resources.Resources.AddItem;
    }

    [RelayCommand]
    private void GotoAbout()
    {
        CurrentPage = _pageFactory.GetPageViewModel<AboutPageViewModel>();
        CurrentPage.BackLink = this;
        PageTitle = Resources.Resources.About;
    }

    [RelayCommand]
    private void GotoUpdates()
    {
        CurrentPage = _pageFactory.GetPageViewModel<UpdatesPageViewModel>();
        CurrentPage.BackLink = this;
        PageTitle = Resources.Resources.Updates;
    }

    [RelayCommand]
    private void GotoQuoteLog()
    {
        CurrentPage = _pageFactory.GetPageViewModel<QuoteLogPageViewModel>();
        CurrentPage.BackLink = this;
        PageTitle = Resources.Resources.Log;
    }
}