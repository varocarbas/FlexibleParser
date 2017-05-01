using System;
using System.Collections.Generic;
using System.Globalization;

namespace FlexibleParser
{
    internal enum DateOrTime { Date, Time }

    internal partial class DatesInternal
    {
        public static Dictionary<DateParts, DateTimeParts> DateToDateTime = new Dictionary<DateParts, DateTimeParts>()
        {
            { DateParts.None, DateTimeParts.None },
            { DateParts.Year, DateTimeParts.Year }, { DateParts.Month, DateTimeParts.Month },
            { DateParts.Day, DateTimeParts.Day }
        };

        public static Dictionary<TimeParts, DateTimeParts> TimeToDateTime = new Dictionary<TimeParts, DateTimeParts>()
        {
            { TimeParts.None, DateTimeParts.None },
            { TimeParts.Hour, DateTimeParts.Hour }, { TimeParts.Minute, DateTimeParts.Minute },
            { TimeParts.Second, DateTimeParts.Second }, { TimeParts.Millisecond, DateTimeParts.Millisecond }
        };

        public static bool IsValidDateTimePart(dynamic item)
        {
            if (item == null) return false;

            Type type = item.GetType();
            
            return
            (
                type == typeof(DateTimeParts) || 
                type == typeof(DateParts) ||
                type == typeof(TimeParts)
            );
        }

        //This function performs all the final actions which are common to the main public ToString() methods.
        public static string ToStringFinal(string outputSofar, DateP dateP)
        {
            if (dateP.Error != ErrorDateEnum.None)
            {
                return
                (
                    dateP.Error == ErrorDateEnum.InvalidInput ?
                    "Invalid input" : "Parse error"
                );
            }

            return outputSofar +
            (
                dateP.TimeZoneOffset == null ? "" : " " +
                OffsetInternal.OffsetToString(dateP.TimeZoneOffset, true)
            );
        }

        //Method performing all the actions required by the public non-static AdaptTimeToTimezone overloads.
        public static DateP AdaptTimeToTimezoneTypes(dynamic input, DateP dateP, bool isEnum = false)
        {
            if
            (
                (isEnum && TimeZonesInternal.EnumIsNothing(input)) ||
                (!isEnum && (input == null || input.Offset == null))
            )
            { return dateP; }

            dynamic input2 =
            (
                !isEnum ? input : TimeZonesInternal.GetTimeZoneClassFromEnum
                (
                    input, input.GetType()
                )
            );

            dateP.TimeZoneOffset = input2.Offset;

            return dateP;
        }

        //Method used by the public static methods to perform a preliminary validity check of the input variable.
        public static bool InputIsOK(dynamic input)
        {
            if (input == null) return false;

            Type type = input.GetType();

            if (type == typeof(string))
            {
                return (input.Trim().Length > 0);
            }
            else if (type == typeof(DateTimeParts))
            {
                return (input != DateTimeParts.None);
            }
            else if (type == typeof(DateParts))
            {
                return (input != DateParts.None);
            }
            else
            {
                return (input != TimeParts.None);
            }
        }

        //All the required checks were performed before calling this method and part is certainly a member of 
        //one of the expected enums.
        public static DateOrTime IsDateOrTime(dynamic part)
        {
            Type type = part.GetType();

            if (type == typeof(DateParts)) return DateOrTime.Date;
            if (type == typeof(TimeParts)) return DateOrTime.Time;

            return
            (
                DateToDateTime.ContainsValue(part) ?
                DateOrTime.Date : DateOrTime.Time
            );
        }

        public static Dictionary<DateTimeParts, DateTimeInternalParts> DateTimeToInternal =
        new Dictionary<DateTimeParts, DateTimeInternalParts>()
        {
            { DateTimeParts.None, DateTimeInternalParts.None }, { DateTimeParts.Year, DateTimeInternalParts.Year },
            { DateTimeParts.Month, DateTimeInternalParts.Month },{ DateTimeParts.Week, DateTimeInternalParts.DayWeek },
            { DateTimeParts.Day, DateTimeInternalParts.Day }, { DateTimeParts.Hour, DateTimeInternalParts.Hour },
            { DateTimeParts.Minute, DateTimeInternalParts.Minute }, { DateTimeParts.Second, DateTimeInternalParts.Second },
            { DateTimeParts.Millisecond, DateTimeInternalParts.Millisecond }
        };

        public static int GetMonthDays(Months month, int year)
        {
            if 
            (
                month == Months.January || month == Months.March || 
                month == Months.May || month == Months.July || 
                month == Months.August || month == Months.October ||
                month == Months.December
            )
            { return 31; }
            else if (month == Months.February)
            {
                return 
                (
                    YearIsLeap(year) ? 29 : 28
                );
            }

            return 30;
        }

        public static bool YearIsLeap(int year)
        {
            return DateTime.IsLeapYear(year);
        }

        public static bool ValueFitsDatePart(int value, DateTimeInternalParts part, int[] additionals = null)
        {
            if (value < 1)
            {
                if (value == 0)
                {
                    if (part == DateTimeInternalParts.Day || part == DateTimeInternalParts.Month)
                    {
                        return false;
                    }
                }
                else return false;
            }

            if (part == DateTimeInternalParts.DayWeek)
            {
                return (value < 6);
            }
            else if (part == DateTimeInternalParts.Day)
            {
                return
                (
                    value < 28 ? true : value <= GetMonthDays
                    (
                        (Months)additionals[0], additionals[1]
                    )
                );
            }
            else if (part == DateTimeInternalParts.Month)
            {
                return (value <= 12);
            }
            else if (part == DateTimeInternalParts.Year)
            {
                return (value <= 9999);
            }
            else if (part == DateTimeInternalParts.Second || part == DateTimeInternalParts.Minute)
            {
                return (value < 60);
            }
            else if (part == DateTimeInternalParts.Hour)
            {
                return (value < 24);
            }
            else if (part == DateTimeInternalParts.Millisecond)
            {
                return true;
            }

            return false;
        }

        public static TemporaryVariables UpdateDateTimePart
        (
            int origVal, int newVal, int[] additionals, 
            DateTimeInternalParts part, DateTime dateTime
        )
        {
            if (ValueFitsDatePart(newVal, part, additionals) && newVal != origVal)
            {
                int newArg = 
                (
                    part == DateTimeInternalParts.DayWeek ?  
                    newVal : newVal - origVal
                );

                return new TemporaryVariables
                (
                    new List<dynamic>()
                    {
                        newVal, UpdateValueDateTimePart
                        (
                            newArg, part, (DateTime?)dateTime
                        )
                    }
                );
            }

            return null;
        }

        public static DateTime AddPartToDate(int value, DateTimeInternalParts part, DateTime dateTime)
        {
            DateTime? outDateTime = AddPartToDate
            (
                value, part, (DateTime?)dateTime, null
            );

            return
            (
                outDateTime == null ? new DateTime() : outDateTime.Value
            );
        }

        public static DateTime? AddPartToDate(int value, DateTimeInternalParts part, DateTime? dateTime, DateTimeFormatInfo formatInfo)
        {
            return AddPartToDate
            (
                (part == DateTimeInternalParts.Year ? value : dateTime.Value.Year),
                (part == DateTimeInternalParts.Month ? value : dateTime.Value.Month),
                (part == DateTimeInternalParts.Day ? value : dateTime.Value.Day),
                (part == DateTimeInternalParts.Hour ? value : dateTime.Value.Hour),
                (part == DateTimeInternalParts.Minute ? value : dateTime.Value.Minute),
                (part == DateTimeInternalParts.Second ? value : dateTime.Value.Second),
                (part == DateTimeInternalParts.Millisecond ? value : dateTime.Value.Millisecond),
                formatInfo.Calendar
            );
        }

        private static DateTime? AddPartToDate(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar)
        {
            return new DateTime(year, month, day, hour, minute, second, millisecond, calendar);
        }

        private static DateTime UpdateValueDateTimePart(int value, DateTimeInternalParts part, DateTime? dateTime)
        {
            DateTime? outDateTime =
            (
                part == DateTimeInternalParts.DayWeek ?
                UpdateWeek(value, dateTime) :
                UpdateOtherThanWeek(value, part, dateTime)
            );

            return
            (
                outDateTime == null ? new DateTime() : outDateTime.Value
            );
        }

        private static DateTime? UpdateWeek(int newWeek, DateTime? dateTime)
        {
            if (!dateTime.HasValue) dateTime = DateTime.Now;
            int curWeek = (int)dateTime.Value.DayOfWeek;

            return dateTime.Value.AddDays(newWeek - curWeek);
        }

        private static DateTime? UpdateOtherThanWeek(int value, DateTimeInternalParts part, DateTime? dateTime)
        {
            if (part == DateTimeInternalParts.Year)
            {
                return dateTime.Value.AddYears(value);
            }
            else if (part == DateTimeInternalParts.Month)
            {
                return dateTime.Value.AddMonths(value);
            }
            else if (part == DateTimeInternalParts.Day)
            {
                return dateTime.Value.AddDays(value);
            }
            else if (part == DateTimeInternalParts.Hour)
            {
                return dateTime.Value.AddHours(value);
            }
            else if (part == DateTimeInternalParts.Minute)
            {
                return dateTime.Value.AddMinutes(value);
            }
            else if (part == DateTimeInternalParts.Second)
            {
                return dateTime.Value.AddSeconds(value);
            }
            else if (part == DateTimeInternalParts.Millisecond)
            {
                return dateTime.Value.AddMilliseconds(value);
            }

            return dateTime;
        }

        //https://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx
        public static Dictionary<DateTimeParts, string> DateTimePartStandardKeyword =
        new Dictionary<DateTimeParts, string>()
        {
            { DateTimeParts.Year, "yyyy" }, { DateTimeParts.Month, "MM" },
            { DateTimeParts.Week, "ddd" }, { DateTimeParts.Day, "dd" },
            { DateTimeParts.Hour, "HH" }, { DateTimeParts.Minute, "mm" },
            { DateTimeParts.Second, "ss" }, { DateTimeParts.Millisecond, "ffffff" }
        };
    }
}
