using System;

namespace FlexibleParser
{
    internal partial class TimeZoneUTCInternal
    {
        public static TimeZoneUTCEnum GetTimeZoneUTCFromOffset(Offset offset)
        {
            if (offset == null || offset.Error != ErrorTimeZoneEnum.None)
            {
                return TimeZoneUTCEnum.None;
            }

            return GetTimeZoneUTCFromDecimalOffset(offset.DecimalOffset);
        }

        public static TimeZoneUTCEnum GetTimeZoneUTCFromDecimalOffset(decimal decimalOffset)
        {
            if (!Common.DecimalOffsetIsValid(decimalOffset))
            {
                return TimeZoneUTCEnum.None;
            }

            return GetTimeZoneUTCFromString
            (
                new HourMinute(decimalOffset).ToString()
            );
        }

        private static TimeZoneUTCEnum GetTimeZoneUTCFromString(string input)
        {
            input = input.Replace(":", "_").Replace("_00", "");

            if (input.Contains("-"))
            {
                input = input.Replace("-", "Minus_");
                input = input.Replace("Minus_0", "Minus_");
            }
            else if (input.Contains("+"))
            {
                input = input.Replace("+", "Plus_");
                input = input.Replace("Plus_0", "Plus_");
            }

            if (input == "Plus_0" || input == "Minus_0")
            {
                input = "UTC";
            }

            TimeZoneUTCEnum timezoneUTC = TimeZoneUTCEnum.None;
            Enum.TryParse(input, out timezoneUTC);

            return timezoneUTC;
        }
    }
}
