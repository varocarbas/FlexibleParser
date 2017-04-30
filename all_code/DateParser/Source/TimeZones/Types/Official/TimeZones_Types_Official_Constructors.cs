using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FlexibleParser
{
    public partial class TimeZoneOfficial : TimeZoneType
    {
        private static bool Populated = TimeZonesInternal.StartTimezones();

        public TimeZoneOfficial(TimeZoneOfficial official) : this
        (
            (TimeZoneOfficialEnum)
            (
                official == null || official.Error != ErrorTimeZoneEnum.None ? TimeZoneOfficialEnum.None : official.Value
            )
        ) 
        { }
        public TimeZoneOfficial(TimeZoneInfo timeZoneInfo) : base(timeZoneInfo, typeof(TimeZoneOfficialEnum)) { }

        public TimeZoneOfficial(string input) : base(input, typeof(TimeZoneOfficialEnum)) { }

        public TimeZoneOfficial(TimeZoneOfficialEnum officialEnum) : base
        (
            TimeZonesInternal.AllNames[officialEnum], TimeZonesInternal.GetEnumItemAbbreviation
            (
                officialEnum, typeof(TimeZoneOfficialEnum)
            ),
            TimeZonesInternal.GetEnumItemOffset
            (
                officialEnum, typeof(TimeZoneOfficialEnum)
            ), 
            officialEnum
        )
        { }
    }
}
