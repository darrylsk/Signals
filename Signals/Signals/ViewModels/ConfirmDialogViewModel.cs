using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Signals.ViewModels.Abstract;

namespace Signals.ViewModels;

public partial class ConfirmDialogViewModel : DialogViewModel
{
    [ObservableProperty] private string _title = "Confirm";
    [ObservableProperty] private string _message = "Are you sure?";
    [ObservableProperty] private string _confirmText = "Yes";
    [ObservableProperty] private string _cancelText = "No";
    [ObservableProperty] private string _iconText = "\xE4E0";
    
    [ObservableProperty] private bool _isConfirmed;
    
    [RelayCommand]
    public void Confirm()
    {
        IsConfirmed = true;
        CloseDialog();
    }

    [RelayCommand]
    public void Cancel()
    {
        IsConfirmed = false;
        CloseDialog();
    }
}