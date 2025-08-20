using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Signals.Factories;
using Signals.InfrastructureLayer.Abstract;
using Signals.InfrastructureLayer.FileService;


namespace Signals.ViewModels;

public partial class AboutPageViewModel : PageViewModel
{
    private IFileService FileService { get; }
    [ObservableProperty] private string _applicationDataFolderName;
    [ObservableProperty] private string _applicationLocalDataFolderName;
    [ObservableProperty] private string _applicationCommonDataFolderName;

    public AboutPageViewModel(): base( "About",
        "About") { }

    public AboutPageViewModel(
        PageFactory pageFactory,
        IFileService fileService) : base("About",
        "About")
    {
        FileService = fileService;
        PopulateFolderNames();
    }

    private void PopulateFolderNames()
    {
        ApplicationDataFolderName = $@"{FileService.GetRoamingAppDataFolder()}\Signals";
        ApplicationLocalDataFolderName = $@"{FileService.GetLocalAppDataFolder()}\Signals";
        ApplicationCommonDataFolderName = $@"{FileService.GetCommonDataFolder()}\Signals";
    }

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
 