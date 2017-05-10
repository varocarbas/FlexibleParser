using System;

namespace FlexibleParser
{
    public partial class TimeZoneUTC
    {
        private static Type MainType = typeof(TimeZoneUTC);

        ///<summary><para>Returns the TimeZoneUTC variable associated with the input official timezone.</para></summary>
        ///<param name="official">TimeZoneOfficial variable to be considered.</param>
        public static TimeZoneUTC FromOfficial(TimeZoneOfficial official)
        {
            return TimeZones.FromOfficialCommon(official, MainType);
        }

        ///<summary><para>Returns the TimeZoneUTCEnum variable associated with the input official timezone.</para></summary>
        ///<param name="official">TimeZoneOfficial variable to be considered.</param>
        public static TimeZoneUTCEnum FromOfficialOnlyEnum(TimeZoneOfficial official)
        {
            return TimeZones.FromOfficialOnlyEnumCommon(official, MainType);
        }

        ///<summary><para>Returns the TimeZoneUTC variable associated with the input IANA timezone.</para></summary>
        ///<param name="iana">TimeZoneIANA variable to be considered.</param>
        public static TimeZoneUTC FromIANA(TimeZoneIANA iana)
        {
            return TimeZones.FromIANACommon(iana, MainType);
        }

        ///<summary><para>Returns the TimeZoneUTCEnum variable associated with the input IANA timezone.</para></summary>
        ///<param name="iana">TimeZoneIANA variable to be considered.</param>
        public static TimeZoneUTCEnum FromIANAOnlyEnum(TimeZoneIANA iana)
        {
            return TimeZones.FromIANAOnlyEnumCommon(iana, MainType);
        }

        ///<summary><para>Returns the TimeZoneUTC variable associated with the input conventional timezone.</para></summary>
        ///<param name="conventional">TimeZoneConventional variable to be considered.</param>
        public static TimeZoneUTC FromConventional(TimeZoneConventional conventional)
        {
            return TimeZones.FromConventionalCommon(conventional, MainType);
        }

        ///<summary><para>Returns the TimeZoneUTCEnum variable associated with the input conventional timezone.</para></summary>
        ///<param name="conventional">TimeZoneConventional variable to be considered.</param>
        public static TimeZoneUTCEnum FromConventionalOnlyEnum(TimeZoneConventional conventional)
        {
            return TimeZones.FromConventionalOnlyEnumCommon(conventional, MainType);
        }

        ///<summary><para>Returns the TimeZoneUTC variable associated with the input Windows timezone.</para></summary>
        ///<param name="windows">TimeZoneWindows variable to be considered.</param>
        public static TimeZoneUTC FromWindows(TimeZoneWindows windows)
        {
            return TimeZones.FromWindowsCommon(windows, MainType);
        }

        ///<summary><para>Returns the TimeZoneUTCEnum variable associated with the input Windows timezone.</para></summary>
        ///<param name="windows">TimeZoneWindows variable to be considered.</param>
        public static TimeZoneUTCEnum FromWindowsOnlyEnum(TimeZoneWindows windows)
        {
            return TimeZones.FromWindowsOnlyEnumCommon(windows, MainType);
        }

        ///<summary><para>Returns the TimeZoneUTC variable associated with the input military timezone.</para></summary>
        ///<param name="military">TimeZoneMilitary variable to be considered.</param>
        public static TimeZoneUTC FromMilitary(TimeZoneMilitary military)
        {
            return TimeZones.FromMilitaryCommon(military, MainType);
        }

        ///<summary><para>Returns the TimeZoneUTCEnum variable associated with the input military timezone.</para></summary>
        ///<param name="military">TimeZoneMilitary variable to be considered.</param>
        public static TimeZoneUTCEnum FromMilitaryOnlyEnum(TimeZoneMilitary military)
        {
            return TimeZones.FromMilitaryOnlyEnumCommon(military, MainType);
        }
    }
}
