using Signals.CoreLayer.Entities;
using Signals.Factories;

namespace Signals.ViewModels;

public class AboutPageViewModel : PageViewModel
{
    public AboutPageViewModel(): base( "About",
        "About") { }

    public AboutPageViewModel(PageFactory pageFactory) : base( "About",
        "About")
    {
        
    }
 }