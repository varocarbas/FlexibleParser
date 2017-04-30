using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FlexibleParser
{
    public partial class TimeZoneOfficial
    {
        private static Type MainType = typeof(TimeZoneOfficial);

        public static ReadOnlyCollection<TimeZoneOfficial> FromIANA(TimeZoneIANA iana)
        {
            return TimeZones.FromIANACommon(iana, MainType);
        }

        public static ReadOnlyCollection<TimeZoneOfficialEnum> FromIANAOnlyEnum(TimeZoneIANA iana)
        {
            return TimeZones.FromIANAOnlyEnumCommon(iana, MainType);
        }

        public static ReadOnlyCollection<TimeZoneOfficial> FromConventional(TimeZoneConventional conventional)
        {
            return TimeZones.FromConventionalCommon(conventional, MainType);
        }

        public static ReadOnlyCollection<TimeZoneOfficialEnum> FromConventionalOnlyEnum(TimeZoneConventional conventional)
        {
            return TimeZones.FromConventionalOnlyEnumCommon(conventional, MainType);
        }

        public static ReadOnlyCollection<TimeZoneOfficial> FromUTC(TimeZoneUTC utc)
        {
            return TimeZones.FromUTCCommon(utc, MainType);
        }

        public static ReadOnlyCollection<TimeZoneOfficialEnum> FromUTCOnlyEnum(TimeZoneUTC utc)
        {
            return TimeZones.FromUTCOnlyEnumCommon(utc, MainType);
        }

        public static ReadOnlyCollection<TimeZoneOfficial> FromWindows(TimeZoneWindows windows)
        {
            return TimeZones.FromWindowsCommon(windows, MainType);
        }

        public static ReadOnlyCollection<TimeZoneOfficialEnum> FromWindowsOnlyEnum(TimeZoneWindows windows)
        {
            return TimeZones.FromWindowsOnlyEnumCommon(windows, MainType);
        }

        public static ReadOnlyCollection<TimeZoneOfficial> FromMilitary(TimeZoneMilitary military)
        {
            return TimeZones.FromMilitaryCommon(military, MainType);
        }

        public static ReadOnlyCollection<TimeZoneOfficialEnum> FromMilitaryOnlyEnum(TimeZoneMilitary military)
        {
            return TimeZones.FromMilitaryOnlyEnumCommon(military, MainType);
        }
    }
}
