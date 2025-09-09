using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Signals.Converters;

public class LocalTimeConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var result = ((DateTime)(value ?? DateTime.Today)).ToLocalTime();
        return result;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var result = ((DateTime)(value ?? DateTime.Today)).ToUniversalTime();
        return result;
    }
}