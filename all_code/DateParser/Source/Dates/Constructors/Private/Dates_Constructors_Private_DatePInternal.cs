using System;
using System.Globalization;

namespace FlexibleParser
{
    internal class DatePInternal
    {
        public string InitialInput { get; set; }
        public bool IsCustomFormat { get; set; }
        public DateTimeFormat DateTimeFormat { get; set; }
        public DateTime Value { get; set; }
        public int Year { get; set; }
        public Months Month { get; set; }
        public int Day { get; set; }
        public string Era { get; set; }
        public DayOfWeek Week { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
        public int Millisecond { get; set; }
        public ErrorDateEnum Error { get; set; }

        public DatePInternal(DateTime dateTime)
        {
            PerformCommonActions
            (
                GetDateTime(dateTime)
            );
        }

        public DatePInternal(string inputString, DateTimeFormat format)
        {
            if (format != null && format.GetType() == typeof(CustomDateTimeFormat))
            {
                IsCustomFormat = true;
                DateTimeFormat = new CustomDateTimeFormat
                (
                    (CustomDateTimeFormat)format
                );
            }
            else
            {
                if (format == null) format = new StandardDateTimeFormat();
                DateTimeFormat = new StandardDateTimeFormat
                (
                    (StandardDateTimeFormat)format
                );
            }

            PerformCommonActions
            (
                GetDateTime(inputString, DateTimeFormat), inputString
            );
        }

        private void PerformCommonActions(DateTime dateTime, string inputString = "", TimeZoneInfo timeZone = null)
        {
            InitialInput = 
            (
                inputString == null ? "" : inputString
            )
            .Trim();
            
            Value = dateTime;
            PopulateDateTimeVars();   
        }

        private void PopulateDateTimeVars()
        {
            Year = Value.Year;
            Month = (Months)Value.Month;
            Week = Value.DayOfWeek;
            Day = Value.Day;
            Hour = Value.Hour;
            Minute = Value.Minute;
            Second = Value.Second;
            Millisecond = Value.Millisecond;
        }

        private DateTime GetDateTime(DateTime dateTime)
        {
            return new DateTime(dateTime.Ticks);
        }

        private DateTime GetDateTime(string input, DateTimeFormat dateTimeFormat)
        {
            DateTime? nullDateTime = null;

            if (IsCustomFormat)
            {
                nullDateTime = DatesInternal.FromStringCustomFormat
                (
                    input, (CustomDateTimeFormat)dateTimeFormat
                );
            }
            else
            {
                nullDateTime = DatesInternal.FromStringStandardFormat
                (
                    input, (StandardDateTimeFormat)dateTimeFormat
                );
            }

            DateTime outDateTime = new DateTime();

            if (!nullDateTime.HasValue)
            {
                Error = ErrorDateEnum.ParseError;
            }
            else outDateTime = nullDateTime.Value;

            return outDateTime;
        }
    }

    public enum Months 
    {
        January = 1, February = 2, March = 3, April = 4, 
        May = 5, June = 6, July = 7, August = 8, September = 9, 
        October = 10, November = 11, December = 12
    }

}
