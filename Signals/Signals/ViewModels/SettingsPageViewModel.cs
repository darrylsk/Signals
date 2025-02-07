using Signals.Data;

namespace Signals.ViewModels;

public partial class SettingsPageViewModel : PageViewModel
{
    public SettingsPageViewModel() : base(PageNames.Settings, "Settings",
        "Configure settings and preferences")
    {
        PageName = PageNames.Settings;
    }
        
}