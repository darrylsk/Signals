using CommunityToolkit.Mvvm.ComponentModel;
using Signals.ViewModels.Abstract;

namespace Signals.ViewModels;

public partial class PageViewModel : ViewModelBase
{
    public PageViewModel(string pageTitle, string pageSubtitle)
    {
        _pageTitle = pageTitle;
        _pageSubtitle = pageSubtitle;
    }

    [ObservableProperty] private string _pageTitle;
    [ObservableProperty] private string _pageSubtitle;

}