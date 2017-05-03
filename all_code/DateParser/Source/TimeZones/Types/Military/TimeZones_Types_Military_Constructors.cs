using System;

namespace FlexibleParser
{
    ///<summary><para>Class dealing with military timezones.</para></summary>
    public partial class TimeZoneMilitary : TimeZoneType
    {
        internal static bool Populated = TimeZonesInternal.StartTimezones();

        ///<summary><para>Initialises a new TimeZoneMilitary instance.</para></summary>
        ///<param name="military">TimeZoneMilitary variable associated with the current instance.</param>
        public TimeZoneMilitary(TimeZoneMilitary military) : this
        (
            (TimeZoneMilitaryEnum)
            (
                military == null || military.Error != ErrorTimeZoneEnum.None ? 
                TimeZoneMilitaryEnum.None : military.Value
            )
        ) 
        { }

        ///<summary><para>Initialises a new TimeZoneMilitary instance.</para></summary>
        ///<param name="timeZoneInfo">TimeZoneInfo variable associated with the current instance.</param>
        public TimeZoneMilitary(TimeZoneInfo timeZoneInfo) : base(timeZoneInfo, typeof(TimeZoneMilitaryEnum)) { }

        ///<summary><para>Initialises a new TimeZoneMilitary instance.</para></summary>
        ///<param name="input">Military timezone information to be parsed.</param>
        public TimeZoneMilitary(string input) : base(input, typeof(TimeZoneMilitaryEnum)) { }

        ///<summary><para>Initialises a new TimeZoneMilitary instance.</para></summary>
        ///<param name="militaryEnum">TimeZoneMilitaryEnum variable to be used.</param>
        public TimeZoneMilitary(TimeZoneMilitaryEnum militaryEnum) : base
        (
            militaryEnum, typeof(TimeZoneMilitaryEnum)
        )
        { } 
    }
}
