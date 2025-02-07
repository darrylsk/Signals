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
}

public class PageFactory<T>
{
    private readonly Func<PageNames, Action<T>, PageViewModel> _pageFactory;

    public PageFactory(Func<PageNames, Action<T>, PageViewModel> pageFactory)
    {
        _pageFactory = pageFactory;
    }
    
    public PageViewModel GetPageViewModel(PageNames pageName, Action<T> action) =>
        _pageFactory.Invoke(pageName, action);
}