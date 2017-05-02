using System;
using System.Collections.ObjectModel;

namespace FlexibleParser
{
    ///<summary><para>Errors triggered by timezone-related classes.</para></summary>
    public enum ErrorTimeZoneEnum
    {
        ///<summary><para>None.</para></summary>
        None = 0,
        ///<summary><para>Invalid offset.</para></summary>
        InvalidOffset,
        ///<summary><para>Invalid timezone.</para></summary>
        InvalidTimeZone
    }

    ///<summary><para>Class dealing with all the timezone types.</para></summary>
    public partial class TimeZones
    {
        ///<summary><para>TimeZoneInfo variable associated with the current instance.</para></summary>
        public readonly TimeZoneInfo TimeZoneInfo;
        ///<summary><para>Offset variable associated with the current instance.</para></summary>
        public readonly Offset Offset;
        ///<summary><para>Collection of TimeZoneOfficial variables associated with the current instance.</para></summary>
        public ReadOnlyCollection<TimeZoneOfficial> Official;
        ///<summary><para>Collection of TimeZoneIANA variables associated with the current instance.</para></summary>
        public ReadOnlyCollection<TimeZoneIANA> IANA;
        ///<summary><para>Collection of TimeZoneConventional variables associated with the current instance.</para></summary>
        public ReadOnlyCollection<TimeZoneConventional> Conventional;
        ///<summary><para>Collection of TimeZoneWindows variables associated with the current instance.</para></summary>
        public readonly TimeZoneWindows Windows;
        ///<summary><para>Collection of TimeZoneUTC variables associated with the current instance.</para></summary>
        public readonly TimeZoneUTC UTC;
        ///<summary><para>Collection of TimeZoneMilitary variables associated with the current instance.</para></summary>
        public readonly TimeZoneMilitary Military;
        ///<summary><para>Error associated with the current instance.</para></summary>
        public readonly ErrorTimeZoneEnum Error;

        ///<summary><para>Initialises a new TimeZones instance.</para></summary>
        ///<param name="offset">Offset variable whose information will be used.</param>
        public TimeZones(Offset offset) : this(TimeZoneUTCInternal.GetTimeZoneUTCFromOffset(offset)) { }

        ///<summary><para>Initialises a new TimeZones instance.</para></summary>
        ///<param name="hourMinute">HourMinute variable whose information will be used.</param>
        public TimeZones(HourMinute hourMinute) : this(new Offset(hourMinute)) { }

        ///<summary><para>Initialises a new TimeZones instance.</para></summary>
        ///<param name="input">Timezone information to be parsed.</param>
        public TimeZones(string input) 
        {
            TemporaryVariables temp = TimeZonesInternal.GetGlobalValuesFromString(input);
            if (temp == null)
            {
                Error = ErrorTimeZoneEnum.InvalidTimeZone;
                return;
            }
            
            int i = 0;
            Official = temp.Vars[i];
            i++;
            IANA = temp.Vars[i];
            i++;
            Conventional = temp.Vars[i];
            i++;
            Windows = temp.Vars[i];
            i++;
            UTC = temp.Vars[i];
            i++;
            Military = temp.Vars[i];

            Offset = new Offset(UTC.Value);
            TimeZoneInfo = TimeZoneWindowsInternal.GetTimeZoneInfoFromEnum(Windows.Value);

            if (Offset == null)
            {
                Error = ErrorTimeZoneEnum.InvalidTimeZone;
            }
        }

        ///<summary><para>Initialises a new TimeZones instance.</para></summary>
        ///<param name="timeZoneInfo">TimeZoneInfo whose information will be used.</param>
        public TimeZones(TimeZoneInfo timeZoneInfo) : this(new TimeZoneWindows(timeZoneInfo), typeof(TimeZoneWindows)) { }

        //Note that the public arguments are classes (e.g., TimeZoneOfficial rather than TimeZoneOfficialItems) 
        //because of being the most user-friendly format (i.e., enums implicitly convertible to them). For internal 
        //calculations, it is always better to rely on the lighter enums and that's why the Type arguments of the 
        //corresponding base constructors are enums.

        ///<summary><para>Initialises a new TimeZones instance.</para></summary>
        ///<param name="official">TimeZoneOfficial variable whose information will be used.</param>
        public TimeZones(TimeZoneOfficial official) : this(official, typeof(TimeZoneOfficialEnum)) { }
        ///<param name="iana">TimeZoneIANA variable whose information will be used.</param>
        public TimeZones(TimeZoneIANA iana) : this(iana, typeof(TimeZoneIANAEnum)) { }
        ///<param name="conventional">TimeZoneConventional variable whose information will be used.</param>
        public TimeZones(TimeZoneConventional conventional) : this(conventional, typeof(TimeZoneConventionalEnum)) { }
        ///<param name="windows">TimeZoneWindows variable whose information will be used.</param>
        public TimeZones(TimeZoneWindows windows) : this(windows, typeof(TimeZoneWindowsEnum)) { }
        ///<param name="utc">TimeZoneUTC variable whose information will be used.</param>
        public TimeZones(TimeZoneUTC utc) : this(utc, typeof(TimeZoneUTCEnum)) { }
        ///<param name="military">TimeZoneMilitary variable whose information will be used.</param>
        public TimeZones(TimeZoneMilitary military) : this(military, typeof(TimeZoneMilitaryEnum)) { }

        private TimeZones(dynamic input, Type type)
        {
            if (input == null || input.Error != ErrorTimeZoneEnum.None)
            {
                Error = ErrorTimeZoneEnum.InvalidTimeZone;
                return;
            }
            var temp = TimeZonesInternal.GetGlobalValuesInternal(input.Value, type);
            if (temp == null)
            {
                Error = ErrorTimeZoneEnum.InvalidTimeZone;
                return;
            }

            CountryEnum[] targets = TimeZonesInternal.GetAssociatedCountries(input.Value, type);

            int i = 0;
            Official =
            (
                targets == null ? temp.Vars[i] : TimeZonesInternal.RemoveOtherCountryItems
                (
                    temp.Vars[i], typeof(TimeZoneOfficialEnum), targets
                )
            );
            
            i++;
            IANA =
            (
                targets == null ? temp.Vars[i] : TimeZonesInternal.RemoveOtherCountryItems
                (
                    temp.Vars[i], typeof(TimeZoneIANAEnum), targets
                )
            );
            

            i++;
            Conventional = temp.Vars[i];
            i++;
            Windows = temp.Vars[i];
            i++;
            UTC = temp.Vars[i];
            i++;
            Military = temp.Vars[i];

            Offset = new Offset(UTC.Value);
            TimeZoneInfo = TimeZoneWindowsInternal.GetTimeZoneInfoFromEnum(Windows.Value);

            if (Offset == null)
            {
                Error = ErrorTimeZoneEnum.InvalidTimeZone;
            }
        }
    }
}
