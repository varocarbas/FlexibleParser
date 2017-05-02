using System;

namespace FlexibleParser
{
    public partial class TimeZoneWindows : TimeZoneType
    {
        private static bool Populated = TimeZonesInternal.StartTimezones();

        ///<summary><para>Initialises a new TimeZoneWindows instance.</para></summary>
        ///<param name="windows">TimeZoneWindows variable whose information will be used.</param>
        public TimeZoneWindows(TimeZoneWindows windows) : this
        (
            (TimeZoneWindowsEnum)
            (
                windows == null || windows.Error != ErrorTimeZoneEnum.None ?
                TimeZoneWindowsEnum.None : windows.Value
            )
        ) 
        { }

        ///<summary><para>Initialises a new TimeZoneWindows instance.</para></summary>
        ///<param name="timeZoneInfo">TimeZoneInfo variable associated with the current instance.</param>
        public TimeZoneWindows(TimeZoneInfo timeZoneInfo) : base(timeZoneInfo, typeof(TimeZoneUTCEnum)) { }

        ///<summary><para>Initialises a new TimeZoneWindows instance.</para></summary>
        ///<param name="input">Windows timezone information to be parsed.</param>
        public TimeZoneWindows(string input) : base(input, typeof(TimeZoneWindowsEnum)) { }

        ///<summary><para>Initialises a new TimeZoneWindows instance.</para></summary>
        ///<param name="windowsEnum">TimeZoneWindowsEnum variable to be used.</param>
        public TimeZoneWindows(TimeZoneWindowsEnum windowsEnum) : base
        (
            TimeZonesInternal.AllNames[windowsEnum], TimeZonesInternal.GetEnumItemAbbreviation
            (
                windowsEnum, typeof(TimeZoneWindowsEnum)
            ),
            TimeZonesInternal.GetEnumItemOffset
            (
                windowsEnum, typeof(TimeZoneWindowsEnum)
            ),
            windowsEnum
        )
        { }
    }
}
