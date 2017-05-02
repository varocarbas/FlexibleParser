using System;

namespace FlexibleParser
{
    public partial class TimeZoneWindows
    {
        private static Type MainType = typeof(TimeZoneWindows);

        ///<summary><para>Returns the TimeZoneWindows variable associated with the input official timezone.</para></summary>
        ///<param name="official">TimeZoneOfficial variable to be considered.</param>
        public static TimeZoneWindows FromOfficial(TimeZoneOfficial official)
        {
            return TimeZones.FromOfficialCommon(official, MainType);
        }

        ///<summary><para>Returns the TimeZoneWindowsEnum variable associated with the input official timezone.</para></summary>
        ///<param name="official">TimeZoneOfficial variable to be considered.</param>
        public static TimeZoneWindowsEnum FromOfficialOnlyEnum(TimeZoneOfficial official)
        {
            return TimeZones.FromOfficialOnlyEnumCommon(official, MainType);
        }

        ///<summary><para>Returns the TimeZoneWindows variable associated with the input IANA timezone.</para></summary>
        ///<param name="iana">TimeZoneIANA variable to be considered.</param>
        public static TimeZoneWindows FromIANA(TimeZoneIANA iana)
        {
            return TimeZones.FromIANACommon(iana, MainType);
        }

        ///<summary><para>Returns the TimeZoneWindowsEnum variable associated with the input IANA timezone.</para></summary>
        ///<param name="iana">TimeZoneIANA variable to be considered.</param>
        public static TimeZoneWindowsEnum FromIANAOnlyEnum(TimeZoneIANA iana)
        {
            return TimeZones.FromIANAOnlyEnumCommon(iana, MainType);
        }

        ///<summary><para>Returns the TimeZoneWindows variable associated with the input conventional timezone.</para></summary>
        ///<param name="conventional">TimeZoneConventional variable to be considered.</param>
        public static TimeZoneWindows FromConventional(TimeZoneConventional conventional)
        {
            return TimeZones.FromConventionalCommon(conventional, MainType);
        }

        ///<summary><para>Returns the TimeZoneWindowsEnum variable associated with the input conventional timezone.</para></summary>
        ///<param name="conventional">TimeZoneConventional variable to be considered.</param>
        public static TimeZoneWindowsEnum FromConventionalOnlyEnum(TimeZoneConventional conventional)
        {
            return TimeZones.FromConventionalOnlyEnumCommon(conventional, MainType);
        }

        ///<summary><para>Returns the TimeZoneWindows variable associated with the input UTC timezone.</para></summary>
        ///<param name="utc">TimeZoneUTC variable to be considered.</param>
        public static TimeZoneWindows FromUTC(TimeZoneUTC utc)
        {
            return TimeZones.FromUTCCommon(utc, MainType);
        }

        ///<summary><para>Returns the TimeZoneWindowsEnum variable associated with the input UTC timezone.</para></summary>
        ///<param name="utc">TimeZoneUTC variable to be considered.</param>
        public static TimeZoneWindowsEnum FromUTCOnlyEnum(TimeZoneUTC utc)
        {
            return TimeZones.FromUTCOnlyEnumCommon(utc, MainType);
        }

        ///<summary><para>Returns the TimeZoneWindows variable associated with the input military timezone.</para></summary>
        ///<param name="military">TimeZoneMilitary variable to be considered.</param>
        public static TimeZoneWindows FromMilitary(TimeZoneMilitary military)
        {
            return TimeZones.FromMilitaryCommon(military, MainType);
        }

        ///<summary><para>Returns the TimeZoneWindowsEnum variable associated with the input military timezone.</para></summary>
        ///<param name="military">TimeZoneMilitary variable to be considered.</param>
        public static TimeZoneWindowsEnum FromMilitaryOnlyEnum(TimeZoneMilitary military)
        {
            return TimeZones.FromMilitaryOnlyEnumCommon(military, MainType);
        }
    }
}
