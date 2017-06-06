using System;
using System.Collections.ObjectModel;

namespace FlexibleParser
{
    public partial class TimeZoneConventional
    {
        private static Type MainType = typeof(TimeZoneConventional);

        ///<summary><para>Returns a list with all the TimeZoneConventional variables associated with the input official timezone.</para></summary>
        ///<param name="official">TimeZoneOfficial variable to be considered.</param>
        public static ReadOnlyCollection<TimeZoneConventional> FromOfficial(TimeZoneOfficial official)
        {
            return TimeZones.FromOfficialCommon(official, MainType);
        }

        ///<summary><para>Returns a list with all the TimeZoneConventionalEnum variables associated with the input official timezone.</para></summary>
        ///<param name="official">TimeZoneOfficial variable to be considered.</param>
        public static ReadOnlyCollection<TimeZoneConventionalEnum> FromOfficialOnlyEnum(TimeZoneOfficial official)
        {
            return TimeZones.FromOfficialOnlyEnumCommon(official, MainType);
        }

        ///<summary><para>Returns a list with all the TimeZoneConventional variables associated with the input IANA timezone.</para></summary>
        ///<param name="iana">TimeZoneIANA variable to be considered.</param>
        public static ReadOnlyCollection<TimeZoneConventional> FromIANA(TimeZoneIANA iana)
        {
            return TimeZones.FromIANACommon(iana, MainType);
        }

        ///<summary><para>Returns a list with all the TimeZoneConventionalEnum variables associated with the input IANA timezone.</para></summary>
        ///<param name="iana">TimeZoneIANA variable to be considered.</param>
        public static ReadOnlyCollection<TimeZoneConventionalEnum> FromIANAOnlyEnum(TimeZoneIANA iana)
        {
            return TimeZones.FromIANAOnlyEnumCommon(iana, MainType);
        }

        ///<summary><para>Returns a list with all the TimeZoneConventional variables associated with the input UTC timezone.</para></summary>
        ///<param name="utc">TimeZoneUTC variable to be considered.</param>
        public static ReadOnlyCollection<TimeZoneConventional> FromUTC(TimeZoneUTC utc)
        {
            return TimeZones.FromUTCCommon(utc, MainType);
        }

        ///<summary><para>Returns a list with all the TimeZoneConventionalEnum variables associated with the input UTC timezone.</para></summary>
        ///<param name="utc">TimeZoneUTC variable to be considered.</param>
        public static ReadOnlyCollection<TimeZoneConventionalEnum> FromUTCOnlyEnum(TimeZoneUTC utc)
        {
            return TimeZones.FromUTCOnlyEnumCommon(utc, MainType);
        }

        ///<summary><para>Returns a list with all the TimeZoneConventional variables associated with the input Windows timezone.</para></summary>
        ///<param name="windows">TimeZoneWindows variable to be considered.</param>
        public static ReadOnlyCollection<TimeZoneConventional> FromWindows(TimeZoneWindows windows)
        {
            return TimeZones.FromWindowsCommon(windows, MainType);
        }

        ///<summary><para>Returns a list with all the TimeZoneConventionalEnum variables associated with the input Windows timezone.</para></summary>
        ///<param name="windows">TimeZoneWindows variable to be considered.</param>
        public static ReadOnlyCollection<TimeZoneConventionalEnum> FromWindowsOnlyEnum(TimeZoneWindows windows)
        {
            return TimeZones.FromWindowsOnlyEnumCommon(windows, MainType);
        }

        ///<summary><para>Returns a list with all the TimeZoneConventional variables associated with the input military timezone.</para></summary>
        ///<param name="military">TimeZoneMilitary variable to be considered.</param>
        public static ReadOnlyCollection<TimeZoneConventional> FromMilitary(TimeZoneMilitary military)
        {
            return TimeZones.FromMilitaryCommon(military, MainType);
        }

        ///<summary><para>Returns a list with all the TimeZoneConventionalEnum variables associated with the input military timezone.</para></summary>
        ///<param name="military">TimeZoneMilitary variable to be considered.</param>
        public static ReadOnlyCollection<TimeZoneConventionalEnum> FromMilitaryOnlyEnum(TimeZoneMilitary military)
        {
            return TimeZones.FromMilitaryOnlyEnumCommon(military, MainType);
        }
    }
}