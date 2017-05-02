using System;

namespace FlexibleParser
{
    ///<summary><para>Class dealing with UTC timezones.</para></summary>
    public partial class TimeZoneUTC : TimeZoneType
    {
        internal static bool Populated = TimeZonesInternal.StartTimezones();

        ///<summary><para>Initialises a new TimeZoneUTC instance.</para></summary>
        ///<param name="utc">TimeZoneUTC variable whose information will be used.</param>
        public TimeZoneUTC(TimeZoneUTC utc) : this
        (
            (TimeZoneUTCEnum)
            (
                utc == null || utc.Error != ErrorTimeZoneEnum.None ? 
                TimeZoneUTCEnum.None : utc.Value
            )
        ) 
        { }

        ///<summary><para>Initialises a new TimeZoneUTC instance.</para></summary>
        ///<param name="timeZoneInfo">TimeZoneInfo variable associated with the current instance.</param>
        public TimeZoneUTC(TimeZoneInfo timeZoneInfo) : base(timeZoneInfo, typeof(TimeZoneUTCEnum)) { }

        ///<summary><para>Initialises a new TimeZoneUTC instance.</para></summary>
        ///<param name="input">UTC timezone information to be parsed.</param>
        public TimeZoneUTC(string input) : base(input, typeof(TimeZoneUTCEnum)) { }

        ///<summary><para>Initialises a new TimeZoneUTC instance.</para></summary>
        ///<param name="utcEnum">TimeZoneUTCEnum variable to be used.</param>
        public TimeZoneUTC(TimeZoneUTCEnum utcEnum) : base
        (
            TimeZonesInternal.AllNames[utcEnum], TimeZonesInternal.GetEnumItemAbbreviation
            (
                utcEnum, typeof(TimeZoneUTCEnum)
            ),
            TimeZonesInternal.GetEnumItemOffset
            (
                utcEnum, typeof(TimeZoneUTCEnum)
            ),
            utcEnum
        )
        { }
    }
}
