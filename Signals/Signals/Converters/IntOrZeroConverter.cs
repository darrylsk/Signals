using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace Signals.Converters;

// Empty string -> 0; valid digits -> int
public sealed class IntOrZeroConverter : IValueConverter
{
    public static readonly IntOrZeroConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int i)
            return i.ToString(culture);

        return "0";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var s = value as string;

        if (string.IsNullOrWhiteSpace(s))
            return 0; // empty -> 0

        if (int.TryParse(s, NumberStyles.Integer, culture, out var i))
            return i;

        return BindingOperations.DoNothing;
    }
}
