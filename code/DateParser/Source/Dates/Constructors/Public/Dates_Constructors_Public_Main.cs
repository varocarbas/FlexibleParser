using System;

namespace FlexibleParser
{
    ///<summary><para>All the months.</para></summary>
    public enum Months
    {
        ///<summary><para>None.</para></summary>
        None = 0,
        ///<summary><para>January.</para></summary>
        January = 1,
        ///<summary><para>February.</para></summary>
        February = 2,
        ///<summary><para>March.</para></summary>
        March = 3,
        ///<summary><para>April.</para></summary>
        April = 4,
        ///<summary><para>May.</para></summary>
        May = 5,
        ///<summary><para>June.</para></summary>
        June = 6,
        ///<summary><para>July.</para></summary>
        July = 7,
        ///<summary><para>August.</para></summary>
        August = 8,
        ///<summary><para>September.</para></summary>
        September = 9,
        ///<summary><para>October.</para></summary>
        October = 10,
        ///<summary><para>November.</para></summary>
        November = 11,
        ///<summary><para>December.</para></summary>
        December = 12
    }

    ///<summary><para>All the date/time parts.</para></summary>
    public enum DateTimeParts 
    {
        ///<summary><para>None.</para></summary>
        None = 0,
        ///<summary><para>Year.</para></summary>
        Year,
        ///<summary><para>Month.</para></summary>
        Month,
        ///<summary><para>Week.</para></summary>
        Week,
        ///<summary><para>Day.</para></summary>
        Day,
        ///<summary><para>Hour.</para></summary>
        Hour,
        ///<summary><para>Minute.</para></summary>
        Minute,
        ///<summary><para>Second.</para></summary>
        Second,
        ///<summary><para>Millisecond.</para></summary>
        Millisecond
    }

    ///<summary><para>All the date parts.</para></summary>
    public enum DateParts
    {
        ///<summary><para>None.</para></summary>
        None = 0,
        ///<summary><para>Year.</para></summary>
        Year,
        ///<summary><para>Month.</para></summary>
        Month,
        ///<summary><para>Week.</para></summary>
        Week,
        ///<summary><para>Day.</para></summary>
        Day
    }

    ///<summary><para>All the time parts.</para></summary>
    public enum TimeParts
    {
        ///<summary><para>None.</para></summary>
        None = 0,
        ///<summary><para>Hour.</para></summary>
        Hour,
        ///<summary><para>Minute.</para></summary>
        Minute,
        ///<summary><para>Second.</para></summary>
        Second,
        ///<summary><para>Millisecond.</para></summary>
        Millisecond
    }

    ///<summary><para>Errors triggered by DateP and related classes.</para></summary>
    public enum ErrorDateEnum
    {
        ///<summary><para>None.</para></summary>
        None = 0,
        ///<summary><para>Parse error.</para></summary>
        ParseError,
        ///<summary><para>Invalid input.</para></summary>
        InvalidInput
    }

    ///<summary><para>Class dealing with all the functionalities extracting date/time information from strings.</para></summary>
    public partial class DateP
    {
        ///<summary><para>Input string containing the date/time information to be parsed.</para></summary>
        public readonly string InitialInput;
        ///<summary><para>CustomDateTimeFormat/StandardDateTimeFormat variable to be used.</para></summary>
        public readonly DateTimeFormat Format;
        ///<summary><para>Error associated with the current instance.</para></summary>
        public readonly ErrorDateEnum Error;
        
        private bool NoUpdates;
        
        private DateTime _Value { get; set; }
        ///<summary><para>DateTime variable associated with the current instance.</para></summary>
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
        ///<summary><para>Year of the date associated with the current instance.</para></summary>
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
        ///<summary><para>Month of the date associated with the current instance.</para></summary>
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
        ///<summary><para>Day of the week of the date associated with the current instance.</para></summary>
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
        ///<summary><para>Day of the date associated with the current instance.</para></summary>
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
        ///<summary><para>Hour of the time associated with the current instance.</para></summary>
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
        ///<summary><para>Minute of the time associated with the current instance.</para></summary>
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
        ///<summary><para>Second of the time associated with the current instance.</para></summary>
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
        ///<summary><para>Millisecond of the time associated with the current instance.</para></summary>
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
        ///<summary><para>Offset of the timezone associated with the current instance.</para></summary>
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

        ///<summary><para>Initialises a new DateP instance.</para></summary>
        ///<param name="dateP">DateP variable whose information will be used.</param>
        ///<param name="offset">Offset variable to be used.</param>
        public DateP(DateP dateP, Offset offset = null) : this
        (
            (
                dateP == null || dateP.Error == ErrorDateEnum.None ? 
                DateTime.Now : dateP.Value
            ), 
            offset
        ) 
        { }

        ///<summary><para>Initialises a new DateP instance.</para></summary>
        ///<param name="dateTime">DateTime variable whose information will be used.</param>
        ///<param name="offset">Offset variable to be used.</param>
        public DateP(DateTime dateTime, Offset offset = null)
        {
            DatePInternal datePInternal = new DatePInternal(dateTime);

            Value = datePInternal.Value;
            TimeZoneOffset = 
            (
                offset == null ? new Offset(TimeZoneInfo.Local) : offset
            );
        }

        ///<summary><para>Initialises a new DateP instance.</para></summary>
        ///<param name="inputString">String containing the date/time information to be parsed.</param>
        ///<param name="offset">Offset variable to be used.</param>
        public DateP(string inputString, Offset offset = null) : this(inputString, null, offset) { }

        ///<summary><para>Initialises a new DateP instance.</para></summary>
        ///<param name="inputString">String containing the date/time information to be parsed.</param>
        ///<param name="dateTimeFormat">CustomDateTimeFormat/StandardDateTimeFormat variable to be used.</param>
        ///<param name="offset">Offset variable to be used.</param>
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
}
