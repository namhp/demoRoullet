using System;
public static class DateTimeExtensions
{
    // find date of this week by day of week.
    public static DateTime DateByDayOfWeek(this DateTime dt, DayOfWeek dayOfWeek)
    {
        int diff = dt.DayOfWeek - dayOfWeek;
        DateTime n = dt.AddDays(-diff);
        return n;
    }
}

