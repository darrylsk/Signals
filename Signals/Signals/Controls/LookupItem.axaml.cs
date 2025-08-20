using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace Signals.Controls;


public class LookupItem : Button
{
    public static readonly StyledProperty<string> SymbolProperty = AvaloniaProperty.Register<LookupItem, string>(
        nameof(Symbol));
    public string Symbol
    {
        get => GetValue(SymbolProperty);
        set => SetValue(SymbolProperty, value);
    }
    
    public new static readonly StyledProperty<string> NameProperty = AvaloniaProperty.Register<LookupItem, string>(
        nameof(Name));
    public new string Name
    {
        get => GetValue(NameProperty);
        set => SetValue(NameProperty, value);
    }
    
    public static readonly StyledProperty<decimal?> CurrentDayOpeningPriceProperty = AvaloniaProperty.Register<LookupItem, decimal?>(
        nameof(CurrentDayOpeningPrice));
    public decimal? CurrentDayOpeningPrice
    {
        get => GetValue(CurrentDayOpeningPriceProperty);
        set => SetValue(CurrentDayOpeningPriceProperty, value);
    }
    
    public static readonly StyledProperty<decimal?> CurrentDayLowPriceProperty = AvaloniaProperty.Register<LookupItem, decimal?>(
        nameof(CurrentDayLowPrice));
    public decimal? CurrentDayLowPrice
    {
        get => GetValue(CurrentDayLowPriceProperty);
        set => SetValue(CurrentDayLowPriceProperty, value);
    }

    public static readonly StyledProperty<decimal?> CurrentDayHighPriceProperty = AvaloniaProperty.Register<LookupItem, decimal?>(
        nameof(CurrentDayHighPrice));

    public decimal? CurrentDayHighPrice
    {
        get => GetValue(CurrentDayHighPriceProperty);
        set => SetValue(CurrentDayHighPriceProperty, value);
    }
    
    public static readonly StyledProperty<decimal?> LatestQuotedPriceProperty = AvaloniaProperty.Register<LookupItem, decimal?>(
        nameof(LatestQuotedPrice));
    public decimal? LatestQuotedPrice
    {
        get => GetValue(LatestQuotedPriceProperty);
        set => SetValue(LatestQuotedPriceProperty, value);
    }
    
    public static readonly StyledProperty<DateTime?> WhenLatestQuoteReceivedProperty = AvaloniaProperty.Register<LookupItem, DateTime?>(
        nameof(WhenLatestQuoteReceived));
    public DateTime? WhenLatestQuoteReceived
    {
        get => GetValue(WhenLatestQuoteReceivedProperty);
        set => SetValue(WhenLatestQuoteReceivedProperty, value);
    }
}