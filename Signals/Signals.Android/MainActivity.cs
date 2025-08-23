using Android.App;
using Android.Content.PM;
using Avalonia;
using Avalonia.Android;
using Signals.Android.Scheduling;

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
        Scheduler.ConfigureWorkManager();

        return base.CustomizeAppBuilder(builder)
            .WithInterFont();
    }
}