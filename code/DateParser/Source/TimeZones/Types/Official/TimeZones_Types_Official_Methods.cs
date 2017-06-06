using System;
using System.Collections.ObjectModel;

namespace FlexibleParser
{
    public partial class TimeZoneOfficial
    {
        private static Type MainType = typeof(TimeZoneOfficial);

        ///<summary><para>Returns a list with all the TimeZoneOfficial variables associated with the input IANA timezone.</para></summary>
        ///<param name="iana">TimeZoneIANA variable to be considered.</param>
        public static ReadOnlyCollection<TimeZoneOfficial> FromIANA(TimeZoneIANA iana)
        {
            return TimeZones.FromIANACommon(iana, MainType);
        }

        ///<summary><para>Returns a list with all the TimeZoneOfficialEnum variables associated with the input IANA timezone.</para></summary>
        ///<param name="iana">TimeZoneIANA variable to be considered.</param>
        public static ReadOnlyCollection<TimeZoneOfficialEnum> FromIANAOnlyEnum(TimeZoneIANA iana)
        {
            return TimeZones.FromIANAOnlyEnumCommon(iana, MainType);
        }

        ///<summary><para>Returns a list with all the TimeZoneOfficial variables associated with the input conventional timezone.</para></summary>
        ///<param name="conventional">TimeZoneConventional variable to be considered.</param>
        public static ReadOnlyCollection<TimeZoneOfficial> FromConventional(TimeZoneConventional conventional)
        {
            return TimeZones.FromConventionalCommon(conventional, MainType);
        }

        ///<summary><para>Returns a list with all the TimeZoneOfficialEnum variables associated with the input conventional timezone.</para></summary>
        ///<param name="conventional">TimeZoneConventional variable to be considered.</param>
        public static ReadOnlyCollection<TimeZoneOfficialEnum> FromConventionalOnlyEnum(TimeZoneConventional conventional)
        {
            return TimeZones.FromConventionalOnlyEnumCommon(conventional, MainType);
        }

        ///<summary><para>Returns a list with all the TimeZoneOfficial variables associated with the input UTC timezone.</para></summary>
        ///<param name="utc">TimeZoneUTC variable to be considered.</param>
        public static ReadOnlyCollection<TimeZoneOfficial> FromUTC(TimeZoneUTC utc)
        {
            return TimeZones.FromUTCCommon(utc, MainType);
        }

        ///<summary><para>Returns a list with all the TimeZoneOfficialEnum variables associated with the input UTC timezone.</para></summary>
        ///<param name="utc">TimeZoneUTC variable to be considered.</param>
        public static ReadOnlyCollection<TimeZoneOfficialEnum> FromUTCOnlyEnum(TimeZoneUTC utc)
        {
            return TimeZones.FromUTCOnlyEnumCommon(utc, MainType);
        }

        ///<summary><para>Returns a list with all the TimeZoneOfficial variables associated with the input Windows timezone.</para></summary>
        ///<param name="windows">TimeZoneWindows variable to be considered.</param>
        public static ReadOnlyCollection<TimeZoneOfficial> FromWindows(TimeZoneWindows windows)
        {
            return TimeZones.FromWindowsCommon(windows, MainType);
        }

        ///<summary><para>Returns a list with all the TimeZoneOfficialEnum variables associated with the input Windows timezone.</para></summary>
        ///<param name="windows">TimeZoneWindows variable to be considered.</param>
        public static ReadOnlyCollection<TimeZoneOfficialEnum> FromWindowsOnlyEnum(TimeZoneWindows windows)
        {
            return TimeZones.FromWindowsOnlyEnumCommon(windows, MainType);
        }

        ///<summary><para>Returns a list with all the TimeZoneOfficial variables associated with the input military timezone.</para></summary>
        ///<param name="military">TimeZoneMilitary variable to be considered.</param>
        public static ReadOnlyCollection<TimeZoneOfficial> FromMilitary(TimeZoneMilitary military)
        {
            return TimeZones.FromMilitaryCommon(military, MainType);
        }

        ///<summary><para>Returns a list with all the TimeZoneOfficialEnum variables associated with the input military timezone.</para></summary>
        ///<param name="military">TimeZoneMilitary variable to be considered.</param>
        public static ReadOnlyCollection<TimeZoneOfficialEnum> FromMilitaryOnlyEnum(TimeZoneMilitary military)
        {
            return TimeZones.FromMilitaryOnlyEnumCommon(military, MainType);
        }
    }
}
