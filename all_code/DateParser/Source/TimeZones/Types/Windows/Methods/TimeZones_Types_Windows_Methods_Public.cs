using System;

namespace FlexibleParser
{
    public partial class TimeZoneWindows
    {
        private static Type MainType = typeof(TimeZoneWindows);

        public static TimeZoneWindows FromOfficial(TimeZoneOfficial official)
        {
            return TimeZones.FromOfficialCommon(official, MainType);
        }

        public static TimeZoneWindowsEnum FromOfficialOnlyEnum(TimeZoneOfficial official)
        {
            return TimeZones.FromOfficialOnlyEnumCommon(official, MainType);
        }

        public static TimeZoneWindows FromIANA(TimeZoneIANA iana)
        {
            return TimeZones.FromIANACommon(iana, MainType);
        }

        public static TimeZoneWindowsEnum FromIANAOnlyEnum(TimeZoneIANA iana)
        {
            return TimeZones.FromIANAOnlyEnumCommon(iana, MainType);
        }

        public static TimeZoneWindows FromConventional(TimeZoneConventional conventional)
        {
            return TimeZones.FromConventionalCommon(conventional, MainType);
        }

        public static TimeZoneWindowsEnum FromConventionalOnlyEnum(TimeZoneConventional conventional)
        {
            return TimeZones.FromConventionalOnlyEnumCommon(conventional, MainType);
        }

        public static TimeZoneWindows FromUTC(TimeZoneUTC utc)
        {
            return TimeZones.FromUTCCommon(utc, MainType);
        }

        public static TimeZoneWindowsEnum FromUTCOnlyEnum(TimeZoneUTC utc)
        {
            return TimeZones.FromUTCOnlyEnumCommon(utc, MainType);
        }

        public static TimeZoneWindows FromMilitary(TimeZoneMilitary military)
        {
            return TimeZones.FromMilitaryCommon(military, MainType);
        }

        public static TimeZoneWindowsEnum FromMilitaryOnlyEnum(TimeZoneMilitary military)
        {
            return TimeZones.FromMilitaryOnlyEnumCommon(military, MainType);
        }
    }
}
