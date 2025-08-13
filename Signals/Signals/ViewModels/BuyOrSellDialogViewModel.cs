using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Signals.CoreLayer.Enums;
using Signals.ViewModels.Abstract;

namespace Signals.ViewModels;

public partial class BuyOrSellDialogViewModel : DialogViewModel
{
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(SaveIsEnabled))] private string _symbol = String.Empty;
    [ObservableProperty] private string _title = String.Empty;
    [ObservableProperty] private DateTime _transactionTime = DateTime.UtcNow;
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(SaveIsEnabled))] private int _units;
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(SaveIsEnabled))] private decimal _price;

    [ObservableProperty] private string _confirmText = "Confirm";
    [ObservableProperty] private string _cancelText = "Cancel";
    [ObservableProperty] private bool _isConfirmed;

    // Profile data items
    public string Country { get; set; }
    public string Currency { get; set; }
    public string Exchange { get; set; }
    public string Name { get; set; }
    public DateTime IpoDate { get; set; }
    public decimal MarketCapitalization { get; set; }
    public decimal SharesOutstanding { get; set; }
    public string Logo { get; set; }
    public string Phone { get; set; }
    public string WebUrl { get; set; }
    public string Industry { get; set; }
    
    // Quotation data items
    public string ExchangeName { get; set; }
    public string CurrencyCode { get; set; }
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
    
    public bool SaveIsEnabled
        => !string.IsNullOrEmpty(Symbol) && Units > 0 && Price > 0;

    public TransactionTypes Action { get; set; }

    #endregion

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