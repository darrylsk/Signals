using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Signals.Views;

public partial class AboutPageView : UserControl
{
    public AboutPageView()
    {
        InitializeComponent();
    }

    private void BtnGetKey_OnClick(object? sender, RoutedEventArgs e)
    {
        // Launch a browser and navigate to the Quotation service web site.
        var uri = new Uri("https://finnhub.io/");
        var launcher = TopLevel.GetTopLevel(BtnGetKey)?.Launcher;
        launcher.LaunchUriAsync(uri);
    }
}