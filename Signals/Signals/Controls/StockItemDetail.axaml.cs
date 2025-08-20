using System;
using Avalonia;
using Avalonia.Controls.Primitives;

namespace Signals.Controls;

public class StockItemDetail : TemplatedControl
{
    public static readonly StyledProperty<string> SymbolProperty 
        = AvaloniaProperty.Register<StockItemDetail, string>(
        nameof(Symbol));

    public string Symbol
    {
        get => GetValue(SymbolProperty);
        set => SetValue(SymbolProperty, value);
    }

    public static readonly StyledProperty<string> ColourByTrendProperty = AvaloniaProperty.Register<StockItemDetail, string>(
        nameof(ColourByTrend));

    public string ColourByTrend
    {
        get => GetValue(ColourByTrendProperty);
        set => SetValue(ColourByTrendProperty, value);
    }

    public new static readonly StyledProperty<string> NameProperty = AvaloniaProperty.Register<StockItemDetail, string>(
        nameof(Name));

    // public string Name
    // {
    //     get => GetValue(NameProperty);
    //     set => SetValue(NameProperty, value);
    // }

    public static readonly StyledProperty<string> ExchangeNameProperty = AvaloniaProperty.Register<StockItemDetail, string>(
        nameof(ExchangeName));

    public string ExchangeName
    {
        get => GetValue(ExchangeNameProperty);
        set => SetValue(ExchangeNameProperty, value);
    }

    public static readonly StyledProperty<string> CurrencyCodeProperty = AvaloniaProperty.Register<StockItemDetail, string>(
        nameof(CurrencyCode));

    public string CurrencyCode
    {
        get => GetValue(CurrencyCodeProperty);
        set => SetValue(CurrencyCodeProperty, value);
    }
    
    public static readonly StyledProperty<decimal?> LatestQuotedPriceProperty = AvaloniaProperty.Register<StockItemDetail, decimal?>(
        nameof(LatestQuotedPrice));

    public decimal? LatestQuotedPrice
    {
        get => GetValue(LatestQuotedPriceProperty);
        set => SetValue(LatestQuotedPriceProperty, value);
    }

    public static readonly StyledProperty<DateTime?> WhenLatestQuoteReceivedProperty = AvaloniaProperty.Register<StockItemDetail, DateTime?>(
        nameof(WhenLatestQuoteReceived));

    public DateTime? WhenLatestQuoteReceived
    {
        get => GetValue(WhenLatestQuoteReceivedProperty);
        set => SetValue(WhenLatestQuoteReceivedProperty, value);
    }

    public static readonly StyledProperty<decimal?> PreviousDayClosingPriceProperty = AvaloniaProperty.Register<StockItemDetail, decimal?>(
        nameof(PreviousDayClosingPrice));

    public decimal? PreviousDayClosingPrice
    {
        get => GetValue(PreviousDayClosingPriceProperty);
        set => SetValue(PreviousDayClosingPriceProperty, value);
    }
    
    public static readonly StyledProperty<decimal?> CurrentDayOpeningPriceProperty = AvaloniaProperty.Register<StockItemDetail, decimal?>(
        nameof(CurrentDayOpeningPrice));
    public decimal? CurrentDayOpeningPrice
    {
        get => GetValue(CurrentDayOpeningPriceProperty);
        set => SetValue(CurrentDayOpeningPriceProperty, value);
    }

    public static readonly StyledProperty<decimal?> CurrentDayPercentChangeProperty = AvaloniaProperty.Register<StockItemDetail, decimal?>(
        nameof(CurrentDayPercentChange));

    public decimal? CurrentDayPercentChange
    {
        get => GetValue(CurrentDayPercentChangeProperty);
        set => SetValue(CurrentDayPercentChangeProperty, value);
    }

    public static readonly StyledProperty<decimal?> CurrentDayHighPriceProperty = AvaloniaProperty.Register<StockItemDetail, decimal?>(
        nameof(CurrentDayHighPrice));

    public decimal? CurrentDayHighPrice
    {
        get => GetValue(CurrentDayHighPriceProperty);
        set => SetValue(CurrentDayHighPriceProperty, value);
    }

    public static readonly StyledProperty<decimal?> CurrentDayLowPriceProperty = AvaloniaProperty.Register<StockItemDetail, decimal?>(
        nameof(CurrentDayLowPrice));
    public decimal? CurrentDayLowPrice
    {
        get => GetValue(CurrentDayLowPriceProperty);
        set => SetValue(CurrentDayLowPriceProperty, value);
    }
}