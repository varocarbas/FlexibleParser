using System;

namespace FlexibleParser
{
    ///<summary><para>Class dealing with both the decimal and hour-minute versions of the timezone offsets.</para></summary>
    public partial class Offset
    {
        ///<summary><para>Decimal version of the current offset.</para></summary>
        public readonly decimal DecimalOffset;
        ///<summary><para>HourMinute version of the current offset.</para></summary>
        public readonly HourMinute HourMinute;
        ///<summary><para>Error associated with the current instance.</para></summary>
        public readonly ErrorTimeZoneEnum Error;

        ///<summary><para>Initialises a new Offset instance.</para></summary>
        ///<param name="supportedDecimalOffset">Decimal version of the supported timezone offset to be used.</param>
        public Offset(decimal supportedDecimalOffset) : this(supportedDecimalOffset, true) { }

        ///<summary><para>Initialises a new Offset instance.</para></summary>
        ///<param name="hourMinute">HourMinute version of the supported timezone offset to be used.</param>
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

        ///<summary><para>Initialises a new Offset instance.</para></summary>
        ///<param name="offset">Offset variable whose information will be used.</param>
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

        ///<summary><para>Initialises a new Offset instance.</para></summary>
        ///<param name="official">TimeZoneOfficial variable whose information will be used.</param>
        public Offset(TimeZoneOfficial official) : this
        (
            official, (official == null ? null : typeof(TimeZoneOfficialEnum))
        ) 
        { }

        ///<summary><para>Initialises a new Offset instance.</para></summary>
        ///<param name="iana">TimeZoneIANA variable whose information will be used.</param>
        public Offset(TimeZoneIANA iana) : this
        (
            iana, (iana == null ? null : typeof(TimeZoneIANAEnum))
        )
        { }

        ///<summary><para>Initialises a new Offset instance.</para></summary>
        ///<param name="conventional">TimeZoneConventional variable whose information will be used.</param>
        public Offset(TimeZoneConventional conventional) : this
        (
            conventional, (conventional == null ? null : typeof(TimeZoneConventionalEnum))
        )
        { }

        ///<summary><para>Initialises a new Offset instance.</para></summary>
        ///<param name="utc">TimeZoneUTC variable whose information will be used.</param>
        public Offset(TimeZoneUTC utc) : this
        (
            utc, (utc == null ? null : typeof(TimeZoneUTCEnum))
        )
        { }

        ///<summary><para>Initialises a new Offset instance.</para></summary>
        ///<param name="windows">TimeZoneWindows variable whose information will be used.</param>
        public Offset(TimeZoneWindows windows) : this
        (
            windows, (windows == null ? null : typeof(TimeZoneWindowsEnum))
        )
        { }

        ///<summary><para>Initialises a new Offset instance.</para></summary>
        ///<param name="military">TimeZoneMilitary variable whose information will be used.</param>
        public Offset(TimeZoneMilitary military) : this
        (
            military, (military == null ? null : typeof(TimeZoneMilitaryEnum))
        )
        { }

        ///<summary><para>Initialises a new Offset instance.</para></summary>
        ///<param name="timeZoneInfo">TimeZoneInfo variable whose information will be used.</param>
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

    ///<summary><para>Class dealing with hour-minute version of the timezone offsets.</para></summary>
    public partial class HourMinute
    {
        ///<summary><para>Hour of the hour-minute offset.</para></summary>
        public readonly int Hour;
        ///<summary><para>Minute of the hour-minute offset.</para></summary>
        public readonly int Minute;
        ///<summary><para>Error associated with the current offset.</para></summary>
        public readonly ErrorTimeZoneEnum Error;

        ///<summary><para>Initialises a new HourMinute instance.</para></summary>
        ///<param name="hourMinute">HourMinute variable whose information will be used.</param>
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

        ///<summary><para>Initialises a new HourMinute instance.</para></summary>
        ///<param name="offset">Offset variable whose information will be used.</param>
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

        ///<summary><para>Initialises a new HourMinute instance.</para></summary>
        ///<param name="supportedDecimalOffset">Decimal version of the supported timezone offset to be used.</param>
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

        ///<summary><para>Initialises a new HourMinute instance.</para></summary>
        ///<param name="hourSupportedOffset">Hour of a supported hour-minute offset.</param>
        ///<param name="minuteSupportedOffset">Minute of a supported hour-minute offset.</param>
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
