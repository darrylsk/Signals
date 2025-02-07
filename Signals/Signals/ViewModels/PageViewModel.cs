using CommunityToolkit.Mvvm.ComponentModel;
using Signals.Data;

namespace Signals.ViewModels;

public partial class PageViewModel : ViewModelBase
{
    public PageViewModel(PageNames pageName, string pageTitle, string pageSubtitle)
    {
        _pageName = pageName;
        _pageTitle = pageTitle;
        PageSubtitle = pageSubtitle;
    }
    [ObservableProperty] private PageNames _pageName;
    [ObservableProperty] private string _pageTitle;
    [ObservableProperty] private string _pageSubtitle;

}