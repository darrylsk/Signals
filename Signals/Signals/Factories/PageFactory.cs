using System;
using Avalonia.Media;
using Signals.Data;
using PageViewModel = Signals.ViewModels.PageViewModel;

namespace Signals.Factories;

public class PageFactory
{
    private readonly Func<PageNames, PageViewModel> _pageFactory;
    private readonly Func<PageNames, string, PageViewModel> _pageFactory2;

    public PageFactory(Func<PageNames, PageViewModel> pageFactory)
    {
        _pageFactory = pageFactory;
    }

    public PageViewModel GetPageViewModel(PageNames pageName) => _pageFactory.Invoke(pageName);

    public PageFactory(Func<PageNames, string, PageViewModel> pageFactory)
    {
        _pageFactory2 = pageFactory;
    }
    public PageViewModel GetPageViewModel(PageNames pageName, string id) => _pageFactory2.Invoke(pageName, "null");

}
