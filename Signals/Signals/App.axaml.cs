using System;
using System.Globalization;
using System.Threading;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Signals.ApplicationLayer.Abstract;
using Signals.ApplicationLayer.Services;
using Signals.CoreLayer.Abstract;
using Signals.CoreLayer.Entities;
using Signals.Factories;
using Signals.InfrastructureLayer.Abstract;
using Signals.InfrastructureLayer.FileService;
using Signals.InfrastructureLayer.QuotationService;
using Signals.InfrastructureLayer.QuotationService.FinnhubQuotationService;
using Signals.InfrastructureLayer.QuotationService.TiingoQuotationService;
using Signals.InfrastructureLayer.Repository;
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
        var culture = new CultureInfo("fr-CA");
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;
        // Lang.Resources.Culture = new CultureInfo("fil-PH");

        var services = new ServiceCollection();
        services.AddSingleton<MainViewModel>();
        
        services.AddTransient<HomePageViewModel>();
        services.AddTransient<HoldingsPageViewModel>();
        services.AddTransient<SettingsPageViewModel>();
        services.AddTransient<WatchlistPageViewModel>();
        services.AddTransient<WatchlistItemPageViewModel>();
        services.AddTransient<AddItemPageViewModel>();
        services.AddTransient<AboutPageViewModel>();
        services.AddTransient<UpdatesPageViewModel>();
        services.AddTransient<QuoteLogPageViewModel>();

        services.AddTransient<IWatchlistService, WatchlistService>();
        services.AddTransient<IHoldingService, HoldingService>();
        services.AddTransient<IQuotationServiceAdapter, QuotationServiceAdapter>();
        services.AddTransient<IFinnhubQuotationService, FinnhubQuotationService>();
        services.AddTransient<ITiingoQuotationService, TiingoQuotationService>();
        services.AddTransient<IConfigurationService, ConfigurationService>();
        services.AddTransient<IFileService, FileService>();
        services.AddTransient<IWatchlistItemRepository, WatchlistItemRepository>();
        services.AddTransient<IHoldingRepository, HoldingRepository>();
        services.AddTransient<ISettingsRepository, SettingsRepository>();

        services.AddSingleton<ISignalsDbContext, SignalsContext>();

        services.AddSingleton<Func<Type, PageViewModel>>( x => type
            => type switch
        {
            //_ when type == typeof(MainViewModel)  => x.GetRequiredService<MainViewModel>(),
            _ when type == typeof(HomePageViewModel)  => x.GetRequiredService<HomePageViewModel>(),
            _ when type == typeof(WatchlistPageViewModel) => x.GetRequiredService<WatchlistPageViewModel>(),
            _ when type == typeof(HoldingsPageViewModel) => x.GetRequiredService<HoldingsPageViewModel>(),
            _ when type == typeof(SettingsPageViewModel)  => x.GetRequiredService<SettingsPageViewModel>(),
            _ when type == typeof(WatchlistItemPageViewModel)  => x.GetRequiredService<WatchlistItemPageViewModel>(),
            _ when type == typeof(AddItemPageViewModel)  => x.GetRequiredService<AddItemPageViewModel>(),
            _ when type == typeof(AboutPageViewModel)  => x.GetRequiredService<AboutPageViewModel>(),
            _ when type == typeof(UpdatesPageViewModel)  => x.GetRequiredService<UpdatesPageViewModel>(),
            _ when type == typeof(QuoteLogPageViewModel)  => x.GetRequiredService<QuoteLogPageViewModel>(),
            _ => throw new NotImplementedException(),
        });

        // services.AddSingleton<Func<PageNames, string, PageViewModel>>(x => (names, s) 
        //     => names switch
        // {
        //     PageNames.WatchlistItemDetail => x.GetRequiredService<WatchlistItemPageViewModel>()
        // });
        
        services.AddSingleton<PageFactory>();
        services.AddAutoMapper(typeof(MappingProfile));
        
        var provider = services.BuildServiceProvider();
        
        // var viewLocator = new ViewLocator();
        // viewLocator.RegisterViewModelFactory(typeof(WatchlistItemPageViewModel), vm => 
        //     new WatchlistItemPageViewModel(provider.GetRequiredService<IBusinessService<WatchlistItem>>()));
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