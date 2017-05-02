using System.Collections.Generic;

namespace FlexibleParser
{
    internal enum DateTimeInternalParts
    {
        None = 0,
        Year, MonthName, Month, DayWeek, Day, Hour,
        Minute, Second, Millisecond
    };

    internal partial class DatesInternal
    {
        //When dealing with common-substring elements, the longer elements should be
        //located first (e.g., millisecond before second).
        public static Dictionary<string, DateTimeInternalParts> KeywordsDateTimeParts =
        new Dictionary<string, DateTimeInternalParts>()
        {
            { "year", DateTimeInternalParts.Year }, { "month", DateTimeInternalParts.Month },
            { "monthname", DateTimeInternalParts.MonthName }, { "namemonth", DateTimeInternalParts.MonthName },
            { "week", DateTimeInternalParts.DayWeek }, { "day", DateTimeInternalParts.Day },
            { "hour", DateTimeInternalParts.Hour }, { "minute", DateTimeInternalParts.Minute },
            { "millisecond", DateTimeInternalParts.Millisecond }, { "second", DateTimeInternalParts.Second }
        };
    }
}
