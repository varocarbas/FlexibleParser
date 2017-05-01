using System;

namespace FlexibleParser
{
    public partial class TimeZoneWindows : TimeZoneType
    {
        private static bool Populated = TimeZonesInternal.StartTimezones();

        public TimeZoneWindows(TimeZoneInfo timeZoneInfo) : base(timeZoneInfo, typeof(TimeZoneUTCEnum)) { }

        public TimeZoneWindows(string input) : base(input, typeof(TimeZoneWindowsEnum)) { }

        public TimeZoneWindows(TimeZoneWindowsEnum windows) : base
        (
            TimeZonesInternal.AllNames[windows], TimeZonesInternal.GetEnumItemAbbreviation
            (
                windows, typeof(TimeZoneWindowsEnum)
            ),
            TimeZonesInternal.GetEnumItemOffset
            (
                windows, typeof(TimeZoneWindowsEnum)
            ), 
            windows
        )
        { }
    }
}
