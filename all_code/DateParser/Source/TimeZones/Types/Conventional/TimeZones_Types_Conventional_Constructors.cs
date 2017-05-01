using System;

namespace FlexibleParser
{
    public partial class TimeZoneConventional : TimeZoneType
    {
        private static bool Populated = TimeZonesInternal.StartTimezones();

        public TimeZoneConventional(TimeZoneInfo timeZoneInfo) : base(timeZoneInfo, typeof(TimeZoneConventional)) { }

        public TimeZoneConventional(string input) : base(input, typeof(TimeZoneConventionalEnum)) { }

        public TimeZoneConventional(TimeZoneConventionalEnum conventional) : base
        (
            TimeZonesInternal.AllNames[conventional], TimeZonesInternal.GetEnumItemAbbreviation
            (
                conventional, typeof(TimeZoneConventionalEnum)
            ),
            TimeZonesInternal.GetEnumItemOffset
            (
                conventional, typeof(TimeZoneConventionalEnum)
            ), 
            conventional
        )
        { }
    }
}