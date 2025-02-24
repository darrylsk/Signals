using CommunityToolkit.Mvvm.ComponentModel;

namespace Signals.ViewModels;

public abstract partial class ViewModelBase : ObservableObject
{
    [ObservableProperty]
    private PageViewModel _currentPage;

    public ViewModelBase MainMenu { get; set; }
}