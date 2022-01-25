using System;

namespace RoboKiwi.Functions;

static class DateTimeExtensions
{
    public static string ToISO8601(this DateTime dateTime)
    {
        return dateTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
    }
}