namespace FlexibleParser
{
    public partial class DateP
    {
        ///<summary><para>Modifies the time/date of the associated DateTime variable to match the input offset.</para></summary>
        ///<param name="offset">Offset variable whose information will be used.</param>
        public DateP AdaptTimeToOffset(Offset offset)
        {
            if (offset == null) return this;

            this.TimeZoneOffset = offset;

            return this;
        }

        ///<summary><para>Modifies the time/date of the associated DateTime variable to match the input timezone.</para></summary>
        ///<param name="official">TimeZoneOfficial variable whose information will be used.</param>
        public DateP AdaptTimeToTimezone(TimeZoneOfficial official)
        {
            return DatesInternal.AdaptTimeToTimezoneTypes(official, this);
        }

        ///<summary><para>Modifies the time/date of the associated DateTime variable to match the input timezone.</para></summary>
        ///<param name="officialEnum">TimeZoneOfficialEnum variable whose information will be used.</param>
        public DateP AdaptTimeToTimezone(TimeZoneOfficialEnum officialEnum)
        {
            return DatesInternal.AdaptTimeToTimezoneTypes(officialEnum, this, true);
        }

        ///<summary><para>Modifies the time/date of the associated DateTime variable to match the input timezone.</para></summary>
        ///<param name="iana">TimeZoneIANA variable whose information will be used.</param>
        public DateP AdaptTimeToTimezone(TimeZoneIANA iana)
        {
            return DatesInternal.AdaptTimeToTimezoneTypes(iana, this);
        }

        ///<summary><para>Modifies the time/date of the associated DateTime variable to match the input timezone.</para></summary>
        ///<param name="ianaEnum">TimeZoneIANAEnum variable whose information will be used.</param>
        public DateP AdaptTimeToTimezone(TimeZoneIANAEnum ianaEnum)
        {
            return DatesInternal.AdaptTimeToTimezoneTypes(ianaEnum, this, true);
        }

        ///<summary><para>Modifies the time/date of the associated DateTime variable to match the input timezone.</para></summary>
        ///<param name="conventional">TimeZoneConventional variable whose information will be used.</param>
        public DateP AdaptTimeToTimezone(TimeZoneConventional conventional)
        {
            return DatesInternal.AdaptTimeToTimezoneTypes(conventional, this);
        }

        ///<summary><para>Modifies the time/date of the associated DateTime variable to match the input timezone.</para></summary>
        ///<param name="conventionalEnum">TimeZoneConventionalEnum variable whose information will be used.</param>
        public DateP AdaptTimeToTimezone(TimeZoneConventionalEnum conventionalEnum)
        {
            return DatesInternal.AdaptTimeToTimezoneTypes(conventionalEnum, this, true);
        }

        ///<summary><para>Modifies the time/date of the associated DateTime variable to match the input timezone.</para></summary>
        ///<param name="utc">TimeZoneUTC variable whose information will be used.</param>
        public DateP AdaptTimeToTimezone(TimeZoneUTC utc)
        {
            return DatesInternal.AdaptTimeToTimezoneTypes(utc, this);
        }

        ///<summary><para>Modifies the time/date of the associated DateTime variable to match the input timezone.</para></summary>
        ///<param name="utcEnum">TimeZoneUTCEnum variable whose information will be used.</param>
        public DateP AdaptTimeToTimezone(TimeZoneUTCEnum utcEnum)
        {
            return DatesInternal.AdaptTimeToTimezoneTypes(utcEnum, this, true);
        }

        ///<summary><para>Modifies the time/date of the associated DateTime variable to match the input timezone.</para></summary>
        ///<param name="windows">TimeZoneWindows variable whose information will be used.</param>
        public DateP AdaptTimeToTimezone(TimeZoneWindows windows)
        {
            return DatesInternal.AdaptTimeToTimezoneTypes(windows, this);
        }

        ///<summary><para>Modifies the time/date of the associated DateTime variable to match the input timezone.</para></summary>
        ///<param name="windowsEnum">TimeZoneWindowsEnum variable whose information will be used.</param>
        public DateP AdaptTimeToTimezone(TimeZoneWindowsEnum windowsEnum)
        {
            return DatesInternal.AdaptTimeToTimezoneTypes(windowsEnum, this, true);
        }

        ///<summary><para>Modifies the time/date of the associated DateTime variable to match the input timezone.</para></summary>
        ///<param name="military">TimeZoneMilitary variable whose information will be used.</param>
        public DateP AdaptTimeToTimezone(TimeZoneMilitary military)
        {
            return DatesInternal.AdaptTimeToTimezoneTypes(military, this);
        }

        ///<summary><para>Modifies the time/date of the associated DateTime variable to match the input timezone.</para></summary>
        ///<param name="militaryEnum">TimeZoneMilitaryEnum variable whose information will be used.</param>
        public DateP AdaptTimeToTimezone(TimeZoneMilitaryEnum militaryEnum)
        {
            return DatesInternal.AdaptTimeToTimezoneTypes(militaryEnum, this, true);
        }

        ///<summary><para>Modifies the time/date of the associated DateTime variable to match the input timezone.</para></summary>
        ///<param name="timezones">TimeZones variable whose information will be used.</param>
        public DateP AdaptTimeToTimezone(TimeZones timezones)
        {
            return DatesInternal.AdaptTimeToTimezoneTypes(timezones, this);
        }
    }
}
