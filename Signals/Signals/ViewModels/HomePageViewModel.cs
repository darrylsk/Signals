using CommunityToolkit.Mvvm.ComponentModel;
using Signals.CoreLayer.Entities;
using Signals.InfrastructureLayer.Abstract;

namespace Signals.ViewModels;

public partial class HomePageViewModel : PageViewModel
{

    public HomePageViewModel(IFileService fileService) : base("Home",
        "Main entry page (not sure if this will ever be useful - might remove)")
    {
        FileService = fileService;
        PopulateFolderNames();
    }

    [ObservableProperty] private string _applicationDataFolderName;
    [ObservableProperty] private string _applicationLocalDataFolderName;
    [ObservableProperty] private string _applicationCommonDataFolderName;

    private void PopulateFolderNames()
    {
        ApplicationDataFolderName = FileService.GetRoamingAppDataFolder();
        ApplicationLocalDataFolderName = FileService.GetLocalAppDataFolder();
        ApplicationCommonDataFolderName = FileService.GetCommonDataFolder();
    }

    public IFileService FileService { get; }

}