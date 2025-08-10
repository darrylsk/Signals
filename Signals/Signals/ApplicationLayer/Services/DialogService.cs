using System.Threading.Tasks;
using Signals.ViewModels.Abstract;

namespace Signals.ApplicationLayer.Services;

public class DialogService
{
    public async Task ShowDialog<THost, T>(THost host, T dialogViewModel)
        where T : DialogViewModel
        where THost : IDialogProvider
    {
        host.Dialog = dialogViewModel;
        dialogViewModel.ShowDialog();
        
        // Hold up the calling process until the Wait method has been called (and returns).
        await dialogViewModel.WaitAsync();
    }
}