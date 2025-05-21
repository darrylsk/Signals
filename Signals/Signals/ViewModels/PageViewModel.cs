using CommunityToolkit.Mvvm.ComponentModel;
using Signals.CoreLayer.Entities;

namespace Signals.ViewModels;

public partial class PageViewModel : ViewModelBase
{
    public PageViewModel(string pageTitle, string pageSubtitle)
    {
        _pageTitle = pageTitle;
        _pageSubtitle = pageSubtitle;
    }
    
    // public PageViewModel(PageNames pageName, string pageTitle, string pageSubtitle)
    // {
    //     _pageName = pageName;
    //     _pageTitle = pageTitle;
    //     _pageSubtitle = pageSubtitle;
    // }
    // [ObservableProperty] private PageNames _pageName;
    
    [ObservableProperty] private string _pageTitle;
    [ObservableProperty] private string _pageSubtitle;

}