using CommunityToolkit.Mvvm.ComponentModel;

namespace Signals.ViewModels;

public partial class AddBuyOrSellDialogViewModel : BuyOrSellDialogViewModel
{
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(SaveIsEnabled))] private bool _alreadyInWatchlist;
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(SaveIsEnabled))] private bool _alreadyInHoldings;
}