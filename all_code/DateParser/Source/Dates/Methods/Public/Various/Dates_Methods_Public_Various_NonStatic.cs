using System;
using System.Globalization;
using System.Linq;

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
            return AdaptTimeToTimezoneTypes(official);
        }

        public DateP AdaptTimeToTimezone(TimeZoneOfficialEnum officialEnum)
        {
            return AdaptTimeToTimezoneTypes(officialEnum, true);
        }

        public DateP AdaptTimeToTimezone(TimeZoneIANA iana)
        {
            return AdaptTimeToTimezoneTypes(iana);
        }

        public DateP AdaptTimeToTimezone(TimeZoneIANAEnum ianaEnum)
        {
            return AdaptTimeToTimezoneTypes(ianaEnum, true);
        }

        public DateP AdaptTimeToTimezone(TimeZoneConventional conventional)
        {
            return AdaptTimeToTimezoneTypes(conventional);
        }

        public DateP AdaptTimeToTimezone(TimeZoneConventionalEnum conventionalEnum)
        {
            return AdaptTimeToTimezoneTypes(conventionalEnum, true);
        }

        public DateP AdaptTimeToTimezone(TimeZoneUTC utc)
        {
            return AdaptTimeToTimezoneTypes(utc);
        }

        public DateP AdaptTimeToTimezone(TimeZoneUTCEnum utcEnum)
        {
            return AdaptTimeToTimezoneTypes(utcEnum, true);
        }

        public DateP AdaptTimeToTimezone(TimeZoneWindows windows)
        {
            return AdaptTimeToTimezoneTypes(windows);
        }

        public DateP AdaptTimeToTimezone(TimeZoneWindowsEnum windowsEnum)
        {
            return AdaptTimeToTimezoneTypes(windowsEnum, true);
        }

        public DateP AdaptTimeToTimezone(TimeZoneMilitary military)
        {
            return AdaptTimeToTimezoneTypes(military);
        }

        public DateP AdaptTimeToTimezone(TimeZoneMilitaryEnum militaryEnum)
        {
            return AdaptTimeToTimezoneTypes(militaryEnum, true);
        }

        public DateP AdaptTimeToTimezone(TimeZones timezones)
        {
            return AdaptTimeToTimezoneTypes(timezones);
        }

        private DateP AdaptTimeToTimezoneTypes(dynamic input, bool isEnum = false)
        {
            if 
            (
                (isEnum && TimeZonesInternal.EnumIsNothing(input)) || 
                (!isEnum && (input == null || input.Offset == null))
            )
            { return this; }

            dynamic input2 =
            (
                !isEnum ? input : TimeZonesInternal.GetTimeZoneClassFromEnum
                (
                    input, input.GetType()   
                )
            );

            this.TimeZoneOffset = input2.Offset;

            return this;
        }
    }
}
