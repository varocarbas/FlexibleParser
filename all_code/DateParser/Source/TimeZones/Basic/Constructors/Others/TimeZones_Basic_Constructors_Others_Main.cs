using System;

namespace FlexibleParser
{
    public partial class Offset
    {
        public readonly decimal DecimalOffset;
        public readonly HourMinute HourMinute;
        public readonly ErrorTimeZoneEnum Error;

        public Offset(decimal supportedDecimalOffset) : this(supportedDecimalOffset, true) { }

        public Offset(HourMinute hourMinute)
        {
            if (hourMinute == null || hourMinute.Error != ErrorTimeZoneEnum.None)
            {
                Error = ErrorTimeZoneEnum.InvalidOffset;
                return;
            }

            HourMinute = new HourMinute(hourMinute);
            DecimalOffset = HourMinute.Hour + HourMinute.Minute / 60m;
        }

        public Offset(Offset offset)
        {
            if (offset == null || offset.Error != ErrorTimeZoneEnum.None)
            {
                Error = ErrorTimeZoneEnum.InvalidOffset;
                return;
            }

            DecimalOffset = offset.DecimalOffset;
            HourMinute = new HourMinute(offset.HourMinute);
        }

        public Offset(TimeZoneOfficial official) : this
        (
            official, (official == null ? null : typeof(TimeZoneOfficialEnum))
        ) 
        { }

        public Offset(TimeZoneIANA iana) : this
        (
            iana, (iana == null ? null : typeof(TimeZoneIANAEnum))
        )
        { }
        
        public Offset(TimeZoneConventional conventional) : this
        (
            conventional, (conventional == null ? null : typeof(TimeZoneConventionalEnum))
        )
        { }

        public Offset(TimeZoneUTC utc) : this
        (
            utc, (utc == null ? null : typeof(TimeZoneUTCEnum))
        )
        { }

        public Offset(TimeZoneWindows windows) : this
        (
            windows, (windows == null ? null : typeof(TimeZoneWindowsEnum))
        )
        { }

        public Offset(TimeZoneMilitary military) : this
        (
            military, (military == null ? null : typeof(TimeZoneMilitaryEnum))
        )
        { }

        public Offset(TimeZoneInfo timeZoneInfo) : this
        (
            timeZoneInfo, (timeZoneInfo == null ? null : typeof(TimeZoneInfo))
        )
        { }

        //Apparently-redundant constructor which avoids potential infinite recursions when instantiating a
        //timezone-type class (+ the associated offset via this UTC alternative). 
        internal Offset(TimeZoneUTCEnum utc) : this
        (
            TimeZoneUTCInternal.GetDecimalOffsetFromEnum(utc), false
        ) 
        { }

        private Offset(decimal decimalOffset, bool checkValidity)
        {
            if (!checkValidity || Common.DecimalOffsetIsValid(decimalOffset))
            {
                DecimalOffset = decimalOffset;
                HourMinute = new HourMinute(DecimalOffset, false);
            }
            else Error = ErrorTimeZoneEnum.InvalidOffset;
        }

        private Offset(dynamic input, Type type)
        {
            if 
            (
                type == null || (type != typeof(TimeZoneInfo) && 
                input.Error != ErrorTimeZoneEnum.None)
            )
            {
                Error = ErrorTimeZoneEnum.InvalidOffset;
                return;
            }

            Offset offset = OffsetInternal.GetOffsetFromTimeZone(input, type);

            if (offset != null)
            {
                DecimalOffset = offset.DecimalOffset;
                HourMinute = new HourMinute(offset.HourMinute);
            }
            else Error = ErrorTimeZoneEnum.InvalidOffset;
        }
    }

    public partial class HourMinute
    {
        public readonly int Hour;
        public readonly int Minute;
        public readonly ErrorTimeZoneEnum Error;

        public HourMinute(HourMinute hourMinute)
        {
            if (hourMinute == null || hourMinute.Error != ErrorTimeZoneEnum.None)
            {
                Error = ErrorTimeZoneEnum.InvalidOffset;
                return;
            }

            Hour = hourMinute.Hour;
            Minute = hourMinute.Minute;
        }

        public HourMinute(Offset offset)
        {
            if (offset == null || offset.HourMinute == null || offset.Error != ErrorTimeZoneEnum.None)
            {
                Error = ErrorTimeZoneEnum.InvalidOffset;
                return;
            }
           
            Hour = offset.HourMinute.Hour;
            Minute = offset.HourMinute.Minute;
        }

        public HourMinute(decimal supportedDecimalOffset) : this(supportedDecimalOffset, true) { }

        internal HourMinute(decimal supportedDecimalOffset, bool checkValidity)
        {
            if (!checkValidity || Common.DecimalOffsetIsValid(supportedDecimalOffset))
            {
                Hour = (int)supportedDecimalOffset;
                Minute = HourMinuteInternal.GetMinuteFromDecimal(supportedDecimalOffset, Hour);
            }
            else Error = ErrorTimeZoneEnum.InvalidOffset;
        }

        public HourMinute(int hourSupportedOffset, int minuteSupportedOffset)
        {
            if (Common.DecimalOffsetIsValid(hourSupportedOffset + minuteSupportedOffset / 60m))
            {
                Hour = hourSupportedOffset;
                Minute = minuteSupportedOffset;
            }
            else Error = ErrorTimeZoneEnum.InvalidOffset;
        }
    }
}
