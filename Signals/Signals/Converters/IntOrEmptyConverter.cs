using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace Signals.Converters;

// Empty string <-> null; valid digits <-> int
public sealed class IntOrEmptyConverter : IValueConverter
{
    public static readonly IntOrEmptyConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int i)
            return i.ToString(culture);

        // When source is null (int?), show empty
        return string.Empty;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var s = value as string;

        if (string.IsNullOrWhiteSpace(s))
            return null; // empty -> null (safe for int?)

        if (int.TryParse(s, NumberStyles.Integer, culture, out var i))
            return i;

        // Keep the previous source value if parse fails
        return BindingOperations.DoNothing;
    }
}