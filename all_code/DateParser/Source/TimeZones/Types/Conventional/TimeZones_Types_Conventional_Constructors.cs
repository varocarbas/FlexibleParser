using System;

namespace FlexibleParser
{
    ///<summary><para>Class dealing with conventional timezones.</para></summary>
    public partial class TimeZoneConventional : TimeZoneType
    {
        internal static bool Populated = TimeZonesInternal.StartTimezones();

        ///<summary><para>Initialises a new TimeZoneConventional instance.</para></summary>
        ///<param name="conventional">TimeZoneConventional variable associated with the current instance.</param>
        public TimeZoneConventional(TimeZoneConventional conventional) : this
        (
            (TimeZoneConventionalEnum)
            (
                conventional == null || conventional.Error != ErrorTimeZoneEnum.None ? 
                TimeZoneConventionalEnum.None : conventional.Value
            )
        ) 
        { }

        ///<summary><para>Initialises a new TimeZoneConventional instance.</para></summary>
        ///<param name="timeZoneInfo">TimeZoneInfo variable associated with the current instance.</param>
        public TimeZoneConventional(TimeZoneInfo timeZoneInfo) : base(timeZoneInfo, typeof(TimeZoneConventional)) { }

        ///<summary><para>Initialises a new TimeZoneConventional instance.</para></summary>
        ///<param name="input">Conventional timezone information to be parsed.</param>
        public TimeZoneConventional(string input) : base(input, typeof(TimeZoneConventionalEnum)) { }

        ///<summary><para>Initialises a new TimeZoneConventional instance.</para></summary>
        ///<param name="conventionalEnum">TimeZoneConventionalEnum variable to be used.</param>
        public TimeZoneConventional(TimeZoneConventionalEnum conventionalEnum) : base
        (
            conventionalEnum, typeof(TimeZoneConventionalEnum)
        )
        { }
    }
}