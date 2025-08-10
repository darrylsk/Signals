using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Signals.ViewModels.Abstract;

public partial class DialogViewModel : ViewModelBase
{
    [ObservableProperty] private bool _isDialogOpen;
    [ObservableProperty] private double _dialogWidth = double.NaN; // indicates that width has not been set. 

    protected TaskCompletionSource CloseTask = new();
    
    public async Task WaitAsync()
    {
        if (CloseTask.Task.IsCompleted) CloseTask = new();
        
        await CloseTask.Task;
    }
    public void ShowDialog()
    {
        IsDialogOpen = true;
    }

    public void CloseDialog()
    {
        IsDialogOpen = false;
        CloseTask.TrySetResult();
    }
}