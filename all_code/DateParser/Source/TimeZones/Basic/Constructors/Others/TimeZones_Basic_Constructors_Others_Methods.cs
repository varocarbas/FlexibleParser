using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    internal class TimeZoneTypeInternal
    {
        public static TimeZoneInfo GetTimeZoneInfo(dynamic value)
        {
            if (TimeZonesInternal.EnumIsNothing(value))
            {
                return null;
            }

            dynamic windows = value;
            Type type = value.GetType();

            if (type != typeof(TimeZoneWindowsEnum))
            {
                windows = TimeZonesInternal.GetAssociatedMapItem(value, type).WindowsTimeZone;
            }

            return TimeZoneWindowsInternal.GetTimeZoneInfoFromEnum(windows);
        }
    }

    internal partial class OffsetInternal
    {
        //Both arguments are expected to be valid: a non-null instance of a timezone type class or TimeZoneInfo;
        //and a non-null type referring to that other input (by bearing in mind that the timezone type class is
        //associated with the corresponding enum type).
        public static Offset GetOffsetFromTimeZone(dynamic input, Type type)
        {
            if (type == typeof(TimeZoneInfo))
            {
                return new Offset
                (
                    new HourMinute
                    (
                        input.BaseUtcOffset.Hours, input.BaseUtcOffset.Minutes
                    )
                );
            }
            else if (!TimeZonesInternal.EnumIsNothing(input.Value))
            {
                return input.Offset;
            }

            return null;
        }
    }

    internal partial class HourMinuteInternal
    {
        public static int GetMinuteFromDecimal(decimal input, int hour)
        {
            input = Math.Abs(input);
            hour = Math.Abs(hour);

            return
            (
                input == hour ? 0 : Convert.ToInt32
                (
                    (input - hour) * 60m
                )
            );
        }
    }
}
