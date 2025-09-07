using Android.App;
using Android.Content.PM;
using Android.OS;
using Avalonia;
using Avalonia.Android;
using Microsoft.Extensions.DependencyInjection;
using Signals.Android.Scheduling;
using Signals.ApplicationLayer.Abstract;
using Signals.InfrastructureLayer.Abstract;

namespace Signals.Android;

[Activity(
    Label = "Signals.Android",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity<App>
{
    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {

        return base.CustomizeAppBuilder(builder)
            .WithInterFont();
    }

    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        Xamarin.Essentials.Platform.Init(this, savedInstanceState); // Ensure Platform.AppContext is initialized
        // var services = new ServiceCollection();
        // var provider = App.ConfigureServices(services);
        // var dc = provider.GetRequiredService<IPriceRefreshService>();
        
        // dc.UpdateWatchlistPrices();
        // dc.UpdateHoldingPrices();
        //dc.UpdateIndexPrices();
            
         Scheduler.ConfigureWorkManager();
    }
}