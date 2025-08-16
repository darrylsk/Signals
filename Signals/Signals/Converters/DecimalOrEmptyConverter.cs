using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace Signals.Converters;

// Empty string <-> null; valid digits <-> int
public sealed class DecimalOrEmptyConverter : IValueConverter
{
    public static readonly DecimalOrEmptyConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is decimal i)
            return i.ToString(culture);

        // When source is null (decimal?), show empty
        return string.Empty;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var s = value as string;

        if (string.IsNullOrWhiteSpace(s))
            return null; // empty -> null (safe for decimal?)

        if (decimal.TryParse(s, NumberStyles.AllowDecimalPoint, culture, out var i))
            return i;

        // Keep the previous source value if parse fails
        return BindingOperations.DoNothing;
    }
}