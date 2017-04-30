using System;
using System.Collections.Generic;

namespace FlexibleParser
{
    public partial class TimeZoneMilitary
    {
        private static Type MainType = typeof(TimeZoneMilitary);

        public static TimeZoneMilitary FromOfficial(TimeZoneOfficial official)
        {
            return TimeZones.FromOfficialCommon(official, MainType);
        }

        public static TimeZoneMilitaryEnum FromOfficialOnlyEnum(TimeZoneOfficial official)
        {
            return TimeZones.FromOfficialOnlyEnumCommon(official, MainType);
        }

        public static TimeZoneMilitary FromConventional(TimeZoneConventional conventional)
        {
            return TimeZones.FromConventionalCommon(conventional, MainType);
        }

        public static TimeZoneMilitaryEnum FromConventionalOnlyEnum(TimeZoneConventional conventional)
        {
            return TimeZones.FromConventionalOnlyEnumCommon(conventional, MainType);
        }

        public static TimeZoneMilitary FromUTC(TimeZoneUTC utc)
        {
            return TimeZones.FromUTCCommon(utc, MainType);
        }

        public static TimeZoneMilitaryEnum FromUTCOnlyEnum(TimeZoneUTC utc)
        {
            return TimeZones.FromUTCOnlyEnumCommon(utc, MainType);
        }

        public static TimeZoneMilitary FromWindows(TimeZoneWindows windows)
        {
            return TimeZones.FromWindowsCommon(windows, MainType);
        }

        public static TimeZoneMilitaryEnum FromWindowsOnlyEnum(TimeZoneWindows windows)
        {
            return TimeZones.FromWindowsOnlyEnumCommon(windows, MainType);
        }

        public static TimeZoneMilitary FromMilitary(TimeZoneMilitary military)
        {
            return TimeZones.FromMilitaryCommon(military, MainType);
        }

        public static TimeZoneMilitaryEnum FromMilitaryOnlyEnum(TimeZoneMilitary military)
        {
            return TimeZones.FromMilitaryOnlyEnumCommon(military, MainType);
        }
    }
}
