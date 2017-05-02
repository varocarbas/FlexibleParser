using System;

namespace FlexibleParser
{
    ///<summary><para>Class dealing with IANA timezones.</para></summary>
    public partial class TimeZoneIANA : TimeZoneType
    {
        private static bool Populated = TimeZonesInternal.StartTimezones();
        
        ///<summary><para>Initialises a new TimeZoneIANA instance.</para></summary>
        ///<param name="iana">TimeZoneIANA variable associated with the current instance.</param>
        public TimeZoneIANA(TimeZoneIANA iana) : this
        (
            (TimeZoneIANAEnum)
            (
                iana == null || iana.Error != ErrorTimeZoneEnum.None ? TimeZoneIANAEnum.None : iana.Value
            )
        ) 
        { }

        ///<summary><para>Initialises a new TimeZoneIANA instance.</para></summary>
        ///<param name="timeZoneInfo">TimeZoneInfo variable associated with the current instance.</param>
        public TimeZoneIANA(TimeZoneInfo timeZoneInfo) : base(timeZoneInfo, typeof(TimeZoneIANAEnum)) { }

        ///<summary><para>Initialises a new TimeZoneIANA instance.</para></summary>
        ///<param name="input">IANA timezone information to be parsed.</param>
        public TimeZoneIANA(string input) : base(input, typeof(TimeZoneIANAEnum)) { }

        ///<summary><para>Initialises a new TimeZoneConventional instance.</para></summary>
        ///<param name="ianaEnum">TimeZoneIANAEnum variable to be used.</param>
        public TimeZoneIANA(TimeZoneIANAEnum ianaEnum) : base
        (
            TimeZonesInternal.AllNames[ianaEnum], TimeZonesInternal.GetEnumItemAbbreviation
            (
                ianaEnum, typeof(TimeZoneIANAEnum)
            ),
            TimeZonesInternal.GetEnumItemOffset
            (
                ianaEnum, typeof(TimeZoneIANAEnum)
            ),
            ianaEnum
        )
        { }
    }
}
