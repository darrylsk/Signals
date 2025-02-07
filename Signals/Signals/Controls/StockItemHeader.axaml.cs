using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace Signals.Controls;


public class StockItemHeader : Button
{
    public static readonly StyledProperty<string> SymbolProperty = AvaloniaProperty.Register<StockItemHeader, string>(
        nameof(Symbol));

    public string Symbol
    {
        get => GetValue(SymbolProperty);
        set => SetValue(SymbolProperty, value);
    }

    public static readonly StyledProperty<string> ColourByTrendProperty = AvaloniaProperty.Register<StockItemHeader, string>(
        nameof(ColourByTrend));

    public string ColourByTrend
    {
        get => GetValue(ColourByTrendProperty);
        set => SetValue(ColourByTrendProperty, value);
    }

    public new static readonly StyledProperty<string> NameProperty = AvaloniaProperty.Register<StockItemHeader, string>(
        nameof(Name));

    public new string Name
    {
        get => GetValue(NameProperty);
        set => SetValue(NameProperty, value);
    }

    public static readonly StyledProperty<decimal?> LatestQuotedPriceProperty = AvaloniaProperty.Register<StockItemHeader, decimal?>(
        nameof(LatestQuotedPrice));

    public decimal? LatestQuotedPrice
    {
        get => GetValue(LatestQuotedPriceProperty);
        set => SetValue(LatestQuotedPriceProperty, value);
    }
    
    public static readonly StyledProperty<DateTime?> WhenLatestQuoteReceivedProperty = AvaloniaProperty.Register<StockItemHeader, DateTime?>(
        nameof(WhenLatestQuoteReceived));

    public DateTime? WhenLatestQuoteReceived
    {
        get => GetValue(WhenLatestQuoteReceivedProperty);
        set => SetValue(WhenLatestQuoteReceivedProperty, value);
    }

    public static readonly StyledProperty<decimal?> CurrentDayPercentChangeProperty = AvaloniaProperty.Register<StockItemHeader, decimal?>(
        nameof(CurrentDayPercentChange));

    public decimal? CurrentDayPercentChange
    {
        get => GetValue(CurrentDayPercentChangeProperty);
        set => SetValue(CurrentDayPercentChangeProperty, value);
    }
}