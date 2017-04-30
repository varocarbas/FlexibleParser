using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    internal partial class TimeZoneWindowsInternal
    {
        private static Type Type = typeof(TimeZoneWindowsEnum);

        internal static TimeZoneWindowsEnum GetEnumFromTimeZoneInfo(TimeZoneInfo timeZoneInfo)
        {
            if (timeZoneInfo == null || timeZoneInfo.Id == null)
            {
                return TimeZoneWindowsEnum.None;
            }

            var temp = TimeZonesInternal.AnalyseEnumNames
            (
                timeZoneInfo.Id.ToLower(), typeof(TimeZoneWindowsEnum)
            );

            return 
            (
                temp == null ? TimeZoneWindowsEnum.None : temp
            );
        }

        internal static TimeZoneInfo GetTimeZoneInfoFromEnum(TimeZoneWindowsEnum windows)
        {
            if (windows != TimeZoneWindowsEnum.None)
            {
                try
                {
                    return TimeZoneInfo.FindSystemTimeZoneById
                    (
                        TimeZonesInternal.CorrectEnumString(windows.ToString(), Type)
                    );
                }
                catch { }
            }

            return null;
        }
    }
}
