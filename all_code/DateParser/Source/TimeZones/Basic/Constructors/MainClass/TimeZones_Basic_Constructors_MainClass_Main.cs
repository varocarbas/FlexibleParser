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

    public partial class TimeZones
    {
        public readonly TimeZoneInfo TimeZoneInfo;
        public readonly Offset Offset;
        public ReadOnlyCollection<TimeZoneOfficial> Official;
        public ReadOnlyCollection<TimeZoneIANA> IANA;
        public ReadOnlyCollection<TimeZoneConventional> Conventional;
        public readonly TimeZoneWindows Windows;
        public readonly TimeZoneUTC UTC;
        public readonly TimeZoneMilitary Military;
        public readonly ErrorTimeZoneEnum Error;

        public TimeZones(Offset offset) : this(TimeZoneUTCInternal.GetTimeZoneUTCFromOffset(offset)) { }
        public TimeZones(HourMinute hourMinute) : this(new Offset(hourMinute)) { }

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

        public TimeZones(TimeZoneInfo timeZoneInfo) : this(new TimeZoneWindows(timeZoneInfo), typeof(TimeZoneWindows)) { }

        //Note that the public arguments are classes (e.g., TimeZoneOfficial rather than TimeZoneOfficialItems) 
        //because of being the most user-friendly format (i.e., enums implicitly convertible to them). For internal 
        //calculations, it is always better to rely on the lighter enums and that's why the Type arguments of the 
        //corresponding base constructors are enums.
        public TimeZones(TimeZoneOfficial official) : this(official, typeof(TimeZoneOfficialEnum)) { }
        public TimeZones(TimeZoneIANA iana) : this(iana, typeof(TimeZoneIANAEnum)) { }
        public TimeZones(TimeZoneConventional conventional) : this(conventional, typeof(TimeZoneConventionalEnum)) { }
        public TimeZones(TimeZoneWindows windows) : this(windows, typeof(TimeZoneWindowsEnum)) { }
        public TimeZones(TimeZoneUTC utc) : this(utc, typeof(TimeZoneUTCEnum)) { }
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
