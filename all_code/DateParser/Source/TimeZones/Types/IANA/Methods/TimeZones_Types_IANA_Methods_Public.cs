using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FlexibleParser
{
    public partial class TimeZoneIANA
    {
        private static Type MainType = typeof(TimeZoneIANA);

        public static ReadOnlyCollection<TimeZoneIANA> FromOfficial(TimeZoneOfficial official)
        {
            return TimeZones.FromOfficialCommon(official, MainType);
        }

        public static ReadOnlyCollection<TimeZoneIANAEnum> FromOfficialOnlyEnum(TimeZoneOfficial official)
        {
            return TimeZones.FromOfficialOnlyEnumCommon(official, MainType);
        }

        public static ReadOnlyCollection<TimeZoneIANA> FromConventional(TimeZoneConventional conventional)
        {
            return TimeZones.FromConventionalCommon(conventional, MainType);
        }

        public static ReadOnlyCollection<TimeZoneIANAEnum> FromConventionalOnlyEnum(TimeZoneConventional conventional)
        {
            return TimeZones.FromConventionalOnlyEnumCommon(conventional, MainType);
        }

        public static ReadOnlyCollection<TimeZoneIANA> FromUTC(TimeZoneUTC utc)
        {
            return TimeZones.FromUTCCommon(utc, MainType);
        }

        public static ReadOnlyCollection<TimeZoneIANAEnum> FromUTCOnlyEnum(TimeZoneUTC utc)
        {
            return TimeZones.FromUTCOnlyEnumCommon(utc, MainType);
        }

        public static ReadOnlyCollection<TimeZoneIANA> FromWindows(TimeZoneWindows windows)
        {
            return TimeZones.FromWindowsCommon(windows, MainType);
        }

        public static ReadOnlyCollection<TimeZoneIANAEnum> FromWindowsOnlyEnum(TimeZoneWindows windows)
        {
            return TimeZones.FromWindowsOnlyEnumCommon(windows, MainType);
        }

        public static ReadOnlyCollection<TimeZoneIANA> FromMilitary(TimeZoneMilitary military)
        {
            return TimeZones.FromMilitaryCommon(military, MainType);
        }

        public static ReadOnlyCollection<TimeZoneIANAEnum> FromMilitaryOnlyEnum(TimeZoneMilitary military)
        {
            return TimeZones.FromMilitaryOnlyEnumCommon(military, MainType);
        }
    }
}
