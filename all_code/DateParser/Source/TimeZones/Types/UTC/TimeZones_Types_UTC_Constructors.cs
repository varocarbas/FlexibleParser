using System;

namespace FlexibleParser
{
    public partial class TimeZoneUTC : TimeZoneType
    {
        private static bool Populated = TimeZonesInternal.StartTimezones();

        public TimeZoneUTC(TimeZoneInfo timeZoneInfo) : base(timeZoneInfo, typeof(TimeZoneUTCEnum)) { }

        public TimeZoneUTC(string input) : base(input, typeof(TimeZoneUTCEnum)) { }

        public TimeZoneUTC(TimeZoneUTCEnum utc) : base
        (
            TimeZonesInternal.AllNames[utc], TimeZonesInternal.GetEnumItemAbbreviation
            (
                utc, typeof(TimeZoneUTCEnum)
            ),
            TimeZonesInternal.GetEnumItemOffset
            (
                utc, typeof(TimeZoneUTCEnum)
            ), 
            utc
        )
        { }
    }
}
