using System;

namespace FlexibleParser
{
    ///<summary><para>Class dealing with official timezones.</para></summary>
    public partial class TimeZoneOfficial : TimeZoneType
    {
        private static bool Populated = TimeZonesInternal.StartTimezones();

        ///<summary><para>Initialises a new TimeZoneOfficial instance.</para></summary>
        ///<param name="official">TimeZoneOfficial variable whose information will be used.</param>
        public TimeZoneOfficial(TimeZoneOfficial official) : this
        (
            (TimeZoneOfficialEnum)
            (
                official == null || official.Error != ErrorTimeZoneEnum.None ? TimeZoneOfficialEnum.None : official.Value
            )
        ) 
        { }

        ///<summary><para>Initialises a new TimeZoneOfficial instance.</para></summary>
        ///<param name="timeZoneInfo">TimeZoneInfo variable associated with the current instance.</param>
        public TimeZoneOfficial(TimeZoneInfo timeZoneInfo) : base(timeZoneInfo, typeof(TimeZoneOfficialEnum)) { }

        ///<summary><para>Initialises a new TimeZoneOfficial instance.</para></summary>
        ///<param name="input">Official timezone information to be parsed.</param>
        public TimeZoneOfficial(string input) : base(input, typeof(TimeZoneOfficialEnum)) { }

        ///<summary><para>Initialises a new TimeZoneOfficial instance.</para></summary>
        ///<param name="officialEnum">TimeZoneOfficialEnum variable to be used.</param>
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
