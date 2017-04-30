using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    public partial class TimeZoneMilitary : TimeZoneType
    {
        private static bool Populated = TimeZonesInternal.StartTimezones();

        public TimeZoneMilitary(TimeZoneInfo timeZoneInfo) : base(timeZoneInfo, typeof(TimeZoneMilitaryEnum)) { }

        public TimeZoneMilitary(string input) : base(input, typeof(TimeZoneMilitaryEnum)) { }

        public TimeZoneMilitary(TimeZoneMilitaryEnum military) : base
        (
            TimeZonesInternal.AllNames[military], TimeZonesInternal.GetEnumItemAbbreviation
            (
                military, typeof(TimeZoneMilitaryEnum)
            ),
            TimeZonesInternal.GetEnumItemOffset
            (
                military, typeof(TimeZoneMilitaryEnum)
            ), 
            military
        )
        { } 
    }
}
