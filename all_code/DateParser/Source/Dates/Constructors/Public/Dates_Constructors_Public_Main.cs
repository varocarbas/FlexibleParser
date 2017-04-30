using System;
using System.Globalization;

namespace FlexibleParser
{
    public enum DateTimeParts 
    {
        None = 0,
        Year, Month, Week, Day, Hour, Minute, Second, Millisecond
    }
    
    public enum DateParts
    {
        None = 0,
        Year, Month, Week, Day
    }

    public enum TimeParts
    {
        None = 0,
        Hour, Minute, Second, Millisecond
    }

    public partial class DateP
    {
        public readonly string InitialInput;
        public readonly DateTimeFormat Format;
        public readonly ErrorDateEnum Error;
        
        private bool NoUpdates;
        
        private DateTime _Value { get; set; }
        public DateTime Value
        {
            get { return _Value; }
            set
            {
                _Value = new DateTime(value.Ticks);
                NoUpdates = true;
                Year = _Value.Year;
                Month = (Months)_Value.Month;
                Week = _Value.DayOfWeek;
                Day = _Value.Day;
                Hour = _Value.Hour;
                Minute = _Value.Minute;
                Second = _Value.Second;
                Millisecond = _Value.Millisecond;
                NoUpdates = false;
            }
        }

        private int _Year { get; set; }
        public int Year
        {
            get { return _Year; }
            set
            {
                TemporaryVariables temp = DatesInternal.UpdateDateTimePart
                (
                    _Year, value, null, DateTimeInternalParts.Year, Value
                );

                if (temp != null)
                {
                    _Year = temp.Vars[0];

                    if (!NoUpdates)
                    {
                        Value = temp.Vars[1];
                    }
                }
            }
        }

        private Months _Month { get; set; }
        public Months Month
        {
            get { return _Month; }
            set
            {
                TemporaryVariables temp = DatesInternal.UpdateDateTimePart
                (
                    (int)_Month, (int)value, null, DateTimeInternalParts.Month, Value
                );

                if (temp != null)
                {
                    _Month = (Months)temp.Vars[0];

                    if (!NoUpdates)
                    {
                        Value = temp.Vars[1];
                    }
                }
            }
        }

        private DayOfWeek _Week { get; set; }
        public DayOfWeek Week
        {
            get { return _Week; }
            set
            {
                TemporaryVariables temp = DatesInternal.UpdateDateTimePart
                (
                    (int)_Week, (int)value, null, DateTimeInternalParts.DayWeek, Value
                );

                if (temp != null)
                {
                    _Week = (DayOfWeek)temp.Vars[0];

                    if (!NoUpdates)
                    {
                        Value = temp.Vars[1];
                    }
                }
            }
        }

        private int _Day { get; set; }
        public int Day
        {
            get { return _Day; }
            set
            {
                TemporaryVariables temp = DatesInternal.UpdateDateTimePart
                (
                    _Day, value, new int[] { (int)Month, Year }, DateTimeInternalParts.Day, Value
                );

                if (temp != null)
                {
                    _Day = temp.Vars[0];

                    if (!NoUpdates)
                    {
                        Value = temp.Vars[1];
                    }
                }
            }
        }

        private int _Hour { get; set; }
        public int Hour
        {
            get { return _Hour; }
            set
            {
                TemporaryVariables temp = DatesInternal.UpdateDateTimePart
                (
                    _Hour, value, null, DateTimeInternalParts.Hour, Value
                );

                if (temp != null)
                {
                    _Hour = temp.Vars[0];

                    if (!NoUpdates)
                    {
                        Value = temp.Vars[1];
                    }
                }
            }
        }

        private int _Minute { get; set; }
        public int Minute
        {
            get { return _Minute; }
            set
            {
                TemporaryVariables temp = DatesInternal.UpdateDateTimePart
                (
                    _Minute, value, null, DateTimeInternalParts.Minute, Value
                );

                if (temp != null)
                {
                    _Minute = temp.Vars[0];

                    if (!NoUpdates)
                    {
                        Value = temp.Vars[1];
                    }
                }
            }
        }

        private int _Second { get; set; }
        public int Second
        {
            get { return _Second; }
            set
            {
                TemporaryVariables temp = DatesInternal.UpdateDateTimePart
                (
                    _Second, value, null, DateTimeInternalParts.Second, Value
                );

                if (temp != null)
                {
                    _Second = temp.Vars[0];

                    if (!NoUpdates)
                    {
                        Value = temp.Vars[1];
                    }
                }
            }
        }

        private int _Millisecond { get; set; }
        public int Millisecond
        {
            get { return _Millisecond; }
            set
            {
                TemporaryVariables temp = DatesInternal.UpdateDateTimePart
                (
                    _Millisecond, value, null, DateTimeInternalParts.Millisecond, Value
                );

                if (temp != null)
                {
                    _Millisecond = temp.Vars[0];

                    if (!NoUpdates)
                    {
                        Value = temp.Vars[1];
                    }
                }
            }
        }

        private Offset _TimeZoneOffset { get; set; }
        public Offset TimeZoneOffset
        {
            get { return _TimeZoneOffset; }
            set
            {
                if (value != null && TimeZoneOffset != null && value.Error == ErrorTimeZoneEnum.None)
                {
                    Value = Value.AddHours
                    (
                        Convert.ToDouble(value.DecimalOffset - TimeZoneOffset.DecimalOffset)
                    );
                }

                _TimeZoneOffset = value;
            }
        }

        public DateP(DateP dateP, Offset offset = null) : this
        (
            (
                dateP == null || dateP.Error == ErrorDateEnum.None ? 
                DateTime.Now : dateP.Value
            ), 
            offset
        ) 
        { }

        public DateP(DateTime dateTime, Offset offset = null)
        {
            DatePInternal datePInternal = new DatePInternal(dateTime);

            Value = datePInternal.Value;
            TimeZoneOffset = 
            (
                offset == null ? new Offset(TimeZoneInfo.Local) : offset
            );
        }

        public DateP(string inputString, Offset offset = null) : this(inputString, null, offset) { }

        public DateP(string inputString, DateTimeFormat dateTimeFormat, Offset offset = null)
        {
            if (inputString == null || inputString.Trim().Length < 1)
            {
                Error = ErrorDateEnum.InvalidInput;
                return;
            }

            DatePInternal datePInternal = new DatePInternal
            (
                inputString, dateTimeFormat
            );

            InitialInput = datePInternal.InitialInput;
            Format = datePInternal.DateTimeFormat;
            Value = datePInternal.Value;
            NoUpdates = true;
            Year = datePInternal.Year;
            Month = datePInternal.Month;
            Week = datePInternal.Week;
            Day = datePInternal.Day;
            Hour = datePInternal.Hour;
            Minute = datePInternal.Minute;
            Second = datePInternal.Second;
            Millisecond = datePInternal.Millisecond;
            Error = datePInternal.Error;
            TimeZoneOffset =
            (
                offset == null ? new Offset(TimeZoneInfo.Local) : offset
            );
            NoUpdates = false;
        }
    }

    public enum ErrorDateEnum 
    { 
        None = 0, 
        ParseError, InvalidInput 
    } 
}
