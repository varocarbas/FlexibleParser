using System;
using System.Collections.ObjectModel;

namespace FlexibleParser
{
    public partial class TimeZoneIANA
    {
        private static Type MainType = typeof(TimeZoneIANA);

        ///<summary><para>Returns a list with all the TimeZoneIANA variables associated with the input official timezone.</para></summary>
        ///<param name="official">TimeZoneOfficial variable to be considered.</param>
        public static ReadOnlyCollection<TimeZoneIANA> FromOfficial(TimeZoneOfficial official)
        {
            return TimeZones.FromOfficialCommon(official, MainType);
        }

        ///<summary><para>Returns a list with all the TimeZoneIANAEnum variables associated with the input official timezone.</para></summary>
        ///<param name="official">TimeZoneOfficial variable to be considered.</param>
        public static ReadOnlyCollection<TimeZoneIANAEnum> FromOfficialOnlyEnum(TimeZoneOfficial official)
        {
            return TimeZones.FromOfficialOnlyEnumCommon(official, MainType);
        }

        ///<summary><para>Returns a list with all the TimeZoneIANA variables associated with the input conventional timezone.</para></summary>
        ///<param name="conventional">TimeZoneConventional variable to be considered.</param>
        public static ReadOnlyCollection<TimeZoneIANA> FromConventional(TimeZoneConventional conventional)
        {
            return TimeZones.FromConventionalCommon(conventional, MainType);
        }

        ///<summary><para>Returns a list with all the TimeZoneIANAEnum variables associated with the input conventional timezone.</para></summary>
        ///<param name="conventional">TimeZoneConventional variable to be considered.</param>
        public static ReadOnlyCollection<TimeZoneIANAEnum> FromConventionalOnlyEnum(TimeZoneConventional conventional)
        {
            return TimeZones.FromConventionalOnlyEnumCommon(conventional, MainType);
        }

        ///<summary><para>Returns a list with all the TimeZoneIANA variables associated with the input UTC timezone.</para></summary>
        ///<param name="utc">TimeZoneUTC variable to be considered.</param>
        public static ReadOnlyCollection<TimeZoneIANA> FromUTC(TimeZoneUTC utc)
        {
            return TimeZones.FromUTCCommon(utc, MainType);
        }

        ///<summary><para>Returns a list with all the TimeZoneIANAEnum variables associated with the input UTC timezone.</para></summary>
        ///<param name="utc">TimeZoneUTC variable to be considered.</param>
        public static ReadOnlyCollection<TimeZoneIANAEnum> FromUTCOnlyEnum(TimeZoneUTC utc)
        {
            return TimeZones.FromUTCOnlyEnumCommon(utc, MainType);
        }

        ///<summary><para>Returns a list with all the TimeZoneIANA variables associated with the input Windows timezone.</para></summary>
        ///<param name="windows">TimeZoneWindows variable to be considered.</param>
        public static ReadOnlyCollection<TimeZoneIANA> FromWindows(TimeZoneWindows windows)
        {
            return TimeZones.FromWindowsCommon(windows, MainType);
        }

        ///<summary><para>Returns a list with all the TimeZoneIANAEnum variables associated with the input Windows timezone.</para></summary>
        ///<param name="windows">TimeZoneWindows variable to be considered.</param>
        public static ReadOnlyCollection<TimeZoneIANAEnum> FromWindowsOnlyEnum(TimeZoneWindows windows)
        {
            return TimeZones.FromWindowsOnlyEnumCommon(windows, MainType);
        }

        ///<summary><para>Returns a list with all the TimeZoneIANA variables associated with the input military timezone.</para></summary>
        ///<param name="military">TimeZoneMilitar variable to be considered.</param>
        public static ReadOnlyCollection<TimeZoneIANA> FromMilitary(TimeZoneMilitary military)
        {
            return TimeZones.FromMilitaryCommon(military, MainType);
        }

        ///<summary><para>Returns a list with all the TimeZoneIANAEnum variables associated with the input military timezone.</para></summary>
        ///<param name="military">TimeZoneMilitar variable to be considered.</param>
        public static ReadOnlyCollection<TimeZoneIANAEnum> FromMilitaryOnlyEnum(TimeZoneMilitary military)
        {
            return TimeZones.FromMilitaryOnlyEnumCommon(military, MainType);
        }
    }
}
