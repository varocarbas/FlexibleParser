namespace FlexibleParser
{
    public partial class DateP
    {
        public DateP AdaptTimeToOffset(Offset offset)
        {
            if (offset == null) return this;

            this.TimeZoneOffset = offset;

            return this;
        }

        public DateP AdaptTimeToTimezone(TimeZoneOfficial official)
        {
            return DatesInternal.AdaptTimeToTimezoneTypes(official, this);
        }

        public DateP AdaptTimeToTimezone(TimeZoneOfficialEnum officialEnum)
        {
            return DatesInternal.AdaptTimeToTimezoneTypes(officialEnum, this, true);
        }

        public DateP AdaptTimeToTimezone(TimeZoneIANA iana)
        {
            return DatesInternal.AdaptTimeToTimezoneTypes(iana, this);
        }

        public DateP AdaptTimeToTimezone(TimeZoneIANAEnum ianaEnum)
        {
            return DatesInternal.AdaptTimeToTimezoneTypes(ianaEnum, this, true);
        }

        public DateP AdaptTimeToTimezone(TimeZoneConventional conventional)
        {
            return DatesInternal.AdaptTimeToTimezoneTypes(conventional, this);
        }

        public DateP AdaptTimeToTimezone(TimeZoneConventionalEnum conventionalEnum)
        {
            return DatesInternal.AdaptTimeToTimezoneTypes(conventionalEnum, this, true);
        }

        public DateP AdaptTimeToTimezone(TimeZoneUTC utc)
        {
            return DatesInternal.AdaptTimeToTimezoneTypes(utc, this);
        }

        public DateP AdaptTimeToTimezone(TimeZoneUTCEnum utcEnum)
        {
            return DatesInternal.AdaptTimeToTimezoneTypes(utcEnum, this, true);
        }

        public DateP AdaptTimeToTimezone(TimeZoneWindows windows)
        {
            return DatesInternal.AdaptTimeToTimezoneTypes(windows, this);
        }

        public DateP AdaptTimeToTimezone(TimeZoneWindowsEnum windowsEnum)
        {
            return DatesInternal.AdaptTimeToTimezoneTypes(windowsEnum, this, true);
        }

        public DateP AdaptTimeToTimezone(TimeZoneMilitary military)
        {
            return DatesInternal.AdaptTimeToTimezoneTypes(military, this);
        }

        public DateP AdaptTimeToTimezone(TimeZoneMilitaryEnum militaryEnum)
        {
            return DatesInternal.AdaptTimeToTimezoneTypes(militaryEnum, this, true);
        }

        public DateP AdaptTimeToTimezone(TimeZones timezones)
        {
            return DatesInternal.AdaptTimeToTimezoneTypes(timezones, this);
        }
    }
}
