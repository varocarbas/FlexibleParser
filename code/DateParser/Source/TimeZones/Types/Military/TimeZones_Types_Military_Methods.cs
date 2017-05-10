using System;

namespace FlexibleParser
{
    public partial class TimeZoneMilitary
    {
        private static Type MainType = typeof(TimeZoneMilitary);

        ///<summary><para>Returns the TimeZoneMilitary variable associated with the input official timezone.</para></summary>
        ///<param name="official">TimeZoneOfficial variable to be considered.</param>
        public static TimeZoneMilitary FromOfficial(TimeZoneOfficial official)
        {
            return TimeZones.FromOfficialCommon(official, MainType);
        }

        ///<summary><para>Returns the TimeZoneMilitaryEnum variable associated with the input official timezone.</para></summary>
        ///<param name="official">TimeZoneOfficial variable to be considered.</param>
        public static TimeZoneMilitaryEnum FromOfficialOnlyEnum(TimeZoneOfficial official)
        {
            return TimeZones.FromOfficialOnlyEnumCommon(official, MainType);
        }

        ///<summary><para>Returns the TimeZoneMilitary variable associated with the input conventional timezone.</para></summary>
        ///<param name="conventional">TimeZoneConventional variable to be considered.</param>
        public static TimeZoneMilitary FromConventional(TimeZoneConventional conventional)
        {
            return TimeZones.FromConventionalCommon(conventional, MainType);
        }

        ///<summary><para>Returns the TimeZoneMilitaryEnum variable associated with the input conventional timezone.</para></summary>
        ///<param name="conventional">TimeZoneConventional variable to be considered.</param>
        public static TimeZoneMilitaryEnum FromConventionalOnlyEnum(TimeZoneConventional conventional)
        {
            return TimeZones.FromConventionalOnlyEnumCommon(conventional, MainType);
        }

        ///<summary><para>Returns the TimeZoneMilitary variable associated with the input UTC timezone.</para></summary>
        ///<param name="utc">TimeZoneUTC variable to be considered.</param>
        public static TimeZoneMilitary FromUTC(TimeZoneUTC utc)
        {
            return TimeZones.FromUTCCommon(utc, MainType);
        }

        ///<summary><para>Returns the TimeZoneMilitaryEnum variable associated with the input UTC timezone.</para></summary>
        ///<param name="utc">TimeZoneUTC variable to be considered.</param>
        public static TimeZoneMilitaryEnum FromUTCOnlyEnum(TimeZoneUTC utc)
        {
            return TimeZones.FromUTCOnlyEnumCommon(utc, MainType);
        }

        ///<summary><para>Returns the TimeZoneMilitary variable associated with the input Windows timezone.</para></summary>
        ///<param name="windows">TimeZoneWindows variable to be considered.</param>
        public static TimeZoneMilitary FromWindows(TimeZoneWindows windows)
        {
            return TimeZones.FromWindowsCommon(windows, MainType);
        }

        ///<summary><para>Returns the TimeZoneMilitaryEnum variable associated with the input Windows timezone.</para></summary>
        ///<param name="windows">TimeZoneWindows variable to be considered.</param>
        public static TimeZoneMilitaryEnum FromWindowsOnlyEnum(TimeZoneWindows windows)
        {
            return TimeZones.FromWindowsOnlyEnumCommon(windows, MainType);
        }
    }
}
