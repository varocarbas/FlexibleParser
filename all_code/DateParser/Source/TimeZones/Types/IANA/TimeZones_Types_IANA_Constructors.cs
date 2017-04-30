using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    public partial class TimeZoneIANA : TimeZoneType
    {
        private static bool Populated = TimeZonesInternal.StartTimezones();

        public TimeZoneIANA(TimeZoneInfo timeZoneInfo) : base(timeZoneInfo, typeof(TimeZoneIANAEnum)) { }

        public TimeZoneIANA(string input) : base(input, typeof(TimeZoneIANAEnum)) { }

        public TimeZoneIANA(TimeZoneIANAEnum iana) : base
        (
            TimeZonesInternal.AllNames[iana], TimeZonesInternal.GetEnumItemAbbreviation
            (
                iana, typeof(TimeZoneIANAEnum)
            ),
            TimeZonesInternal.GetEnumItemOffset
            (
                iana, typeof(TimeZoneIANAEnum)
            ), 
            iana
        )
        { }
    }
}
