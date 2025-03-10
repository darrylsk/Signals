using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using Signals.Factories;


namespace Signals.ViewModels;

public partial class AboutPageViewModel : PageViewModel
{
    public AboutPageViewModel(): base( "About",
        "About") { }

    public AboutPageViewModel(PageFactory pageFactory) : base( "About",
        "About")
    { }

    [RelayCommand]
    public async Task GetKey(Button getKeyButton)
    {
        // Get key from Quotation Service and apply to the application configuration.
        var uri = new Uri("https://finnhub.io/");
        var launcher = TopLevel.GetTopLevel(getKeyButton)?.Launcher;
        if (launcher == null) return;
        var success = await launcher.LaunchUriAsync(uri);
    }

    [RelayCommand]
    public void CheckForNewVersion()
    {
        // Check to see if the current installed version is the latest.
        
    }
 }
 