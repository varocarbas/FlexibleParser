using System;
using System.Collections.ObjectModel;

namespace FlexibleParser
{
    public partial class TimeZoneConventional
    {
        private static Type MainType = typeof(TimeZoneConventional);

        public static ReadOnlyCollection<TimeZoneConventional> FromOfficial(TimeZoneOfficial official)
        {
            return TimeZones.FromOfficialCommon(official, MainType);
        }

        public static ReadOnlyCollection<TimeZoneConventionalEnum> FromOfficialOnlyEnum(TimeZoneOfficial official)
        {
            return TimeZones.FromOfficialOnlyEnumCommon(official, MainType);
        }

        public static ReadOnlyCollection<TimeZoneConventional> FromIANA(TimeZoneIANA iana)
        {
            return TimeZones.FromIANACommon(iana, MainType);
        }

        public static ReadOnlyCollection<TimeZoneConventionalEnum> FromIANAOnlyEnum(TimeZoneIANA iana)
        {
            return TimeZones.FromIANAOnlyEnumCommon(iana, MainType);
        }

        public static ReadOnlyCollection<TimeZoneConventional> FromUTC(TimeZoneUTC utc)
        {
            return TimeZones.FromUTCCommon(utc, MainType);
        }

        public static ReadOnlyCollection<TimeZoneConventionalEnum> FromUTCOnlyEnum(TimeZoneUTC utc)
        {
            return TimeZones.FromUTCOnlyEnumCommon(utc, MainType);
        }

        public static ReadOnlyCollection<TimeZoneConventional> FromWindows(TimeZoneWindows windows)
        {
            return TimeZones.FromWindowsCommon(windows, MainType);
        }

        public static ReadOnlyCollection<TimeZoneConventionalEnum> FromWindowsOnlyEnum(TimeZoneWindows windows)
        {
            return TimeZones.FromWindowsOnlyEnumCommon(windows, MainType);
        }

        public static ReadOnlyCollection<TimeZoneConventional> FromMilitary(TimeZoneMilitary military)
        {
            return TimeZones.FromMilitaryCommon(military, MainType);
        }

        public static ReadOnlyCollection<TimeZoneConventionalEnum> FromMilitaryOnlyEnum(TimeZoneMilitary military)
        {
            return TimeZones.FromMilitaryOnlyEnumCommon(military, MainType);
        }
    }
}