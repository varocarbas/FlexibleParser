using System;
using System.Collections.Generic;

namespace FlexibleParser
{
    internal partial class OffsetInternal
    {
        public static string OffsetToString(Offset offset, bool isDateP = false)
        {
            if (offset == null || offset.Error != ErrorTimeZoneEnum.None)
            {
                return (isDateP ? "" : "Invalid offset");
            }

            return HourMinuteInternal.HourMinuteToString(offset.HourMinute, isDateP);
        }
    }

    internal partial class HourMinuteInternal
    {
        public static decimal GetDecimalFromHourMinute(HourMinute hourMinute)
        {
            return
            (
                hourMinute == null ? 0m : hourMinute.Hour + hourMinute.Minute / 60m
            );
        }

        public static string HourMinuteToString(HourMinute hourMinute, bool isDateP = false)
        {
            if (hourMinute == null || hourMinute.Error != ErrorTimeZoneEnum.None)
            {
                return (isDateP ? "" : "Invalid offset");
            }

            string outString = (hourMinute.Hour < 0m ? "-" : "+");

            return
            (
                outString + Math.Abs(hourMinute.Hour).ToString("00") + 
                (isDateP ? "" : ":") + hourMinute.Minute.ToString("00")
            );
        }
    }
}
