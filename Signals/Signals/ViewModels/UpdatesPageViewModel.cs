using Signals.Factories;

namespace Signals.ViewModels;

public class UpdatesPageViewModel : PageViewModel
{
    public UpdatesPageViewModel(PageFactory pageFactory): base(Resources.Resources.Updates, 
        Resources.Resources.UpdatesPageSubtitle)
    { }
    
}