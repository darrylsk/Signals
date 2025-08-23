using System;

namespace Signals.Android.Helpers;

public static class TimeHelper
{
    public static TimeSpan TimeDifferenceFromUtc(DateTime date)
    {
        return DateTime.UtcNow - DateTime.Now;
    }
}