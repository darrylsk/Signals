using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Signals.ApplicationLayer.Abstract;
using Signals.ApplicationLayer.Services;
using Signals.Data;
using Signals.Factories;
using Signals.ViewModels;
using Signals.Views;
using PageViewModel = Signals.ViewModels.PageViewModel;

namespace Signals;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var services = new ServiceCollection();
        services.AddSingleton<MainViewModel>();
        services.AddTransient<HomePageViewModel>();
        services.AddTransient<WatchlistPageViewModel>();
        services.AddTransient<HoldingsPageViewModel>();
        services.AddTransient<SettingsPageViewModel>();
        services.AddTransient<WatchlistItemDetailViewModel>();
        services.AddTransient<IWatchlistService, WatchlistService>();

        services.AddSingleton<Func<PageNames, PageViewModel>>( x => name
            => name switch
        {
            PageNames.Home => x.GetRequiredService<HomePageViewModel>(),
            PageNames.Watchlist => x.GetRequiredService<WatchlistPageViewModel>(),
            PageNames.Holdings => x.GetRequiredService<HoldingsPageViewModel>(),
            PageNames.Settings => x.GetRequiredService<SettingsPageViewModel>(),
            PageNames.WatchlistItemDetail => x.GetRequiredService<WatchlistItemDetailViewModel>(),
            _ => throw new NotImplementedException(),
        });

        // services.AddSingleton<Func<PageNames, string, PageViewModel>>(x => (names, s) 
        //     => names switch
        // {
        //     PageNames.WatchlistItemDetail => x.GetRequiredService<WatchlistItemDetailViewModel>()
        // });
        
        services.AddSingleton<PageFactory>();
        services.AddAutoMapper(typeof(MappingProfile));
        
        var provider = services.BuildServiceProvider();
        
        // var viewLocator = new ViewLocator();
        // viewLocator.RegisterViewModelFactory(typeof(WatchlistItemDetailViewModel), vm => 
        //     new WatchlistItemDetailViewModel(provider.GetRequiredService<IBusinessService<WatchlistItem>>()));
        // this.DataTemplates.Add(viewLocator);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Line below is needed to remove Avalonia data validation.
            // Without this line you will get duplicate validations from both Avalonia and CT
            BindingPlugins.DataValidators.RemoveAt(0);
            desktop.MainWindow = new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}