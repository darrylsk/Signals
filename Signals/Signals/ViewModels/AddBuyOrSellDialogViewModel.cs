using System;
using CommunityToolkit.Mvvm.ComponentModel;
using Signals.CoreLayer.Enums;

namespace Signals.ViewModels;

public partial class AddBuyOrSellDialogViewModel : ConfirmDialogViewModel
{
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(SaveIsEnabled))] private bool _alreadyInWatchlist;
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(SaveIsEnabled))] private bool _alreadyInHoldings;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(SaveIsEnabled))] private string _symbol = String.Empty;
    [ObservableProperty] private string _title = String.Empty;
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(TransactionDateTime))] private DateTimeOffset _transactionDate = DateTime.UtcNow.Date;
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(TransactionDateTime))] private TimeSpan _transactionTime = DateTime.UtcNow.TimeOfDay; 
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(SaveIsEnabled))] private int? _units;
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(SaveIsEnabled))] private decimal? _price;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(SaveIsEnabled))] private bool _addToWatchlist;
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(SaveIsEnabled))] private bool _addToHoldings;
    [ObservableProperty] private string _confirmText = "Confirm";
    [ObservableProperty] private string _cancelText = "Cancel";
    [ObservableProperty] private bool _isConfirmed;

    // Profile data items
    public DateTime TransactionDateTime => TransactionDate.DateTime + TransactionTime;
    public string Country { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public string Exchange { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime IpoDate { get; set; }
    public decimal MarketCapitalization { get; set; }
    public decimal SharesOutstanding { get; set; }
    public string Logo { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string WebUrl { get; set; } = string.Empty;
    public string Industry { get; set; } = string.Empty;
    
    // Quotation data items
    public string ExchangeName { get; set; } = string.Empty;
    public string CurrencyCode { get; set; } = string.Empty;
    public decimal? PreviousDayClosingPrice { get; set; }
    public decimal CurrentDayOpeningPrice { get; set; }
    public decimal? CurrentDayHighPrice { get; set; }
    public decimal? CurrentDayLowPrice { get; set; }
    public decimal LatestQuotedPrice { get; set; }
    public DateTime? WhenLatestQuoteReceived { get; set; }

    // Additional data items only found in holdings
    public decimal AveragePurchasePrice { get; set; }
    public DateTime WhenLastPurchased { get; set; }
    
    #region Computed and non data members
    
    // Bug: The value converter for the units is necessary to avoid an invalid cast exception and an error message
    // in the NumericUpDown if its text box is cleared, but it interferes with the notification system, and there's
    // no way to fix it from my code.
    public bool SaveIsEnabled
        => !string.IsNullOrEmpty(Symbol) && Units is > 0 && Price is > 0;

    public TransactionTypes Action { get; set; }

    #endregion

    /*
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
    */

}