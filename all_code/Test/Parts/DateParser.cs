using System;
using System.Collections.Generic;
using System.Linq;
using FlexibleParser;
using System.Globalization;

namespace Test
{
    public class DateParser
    {
        public static void StartTest()
        {
            Console.WriteLine("-------------- DateParser --------------");
            Console.WriteLine();

            //------ All the public classes include some basic features to ease their usage.

            //--- All of them can be used right away with the most common LINQ methods (e.g., IComparable implemented).
            PrintSampleItem
            (
                "Ini1", new TimeZoneOfficial[] 
                { 
                    TimeZoneOfficialEnum.Alaska_Daylight_Time 
                }
                .Except
                (
                    new TimeZoneOfficial[] 
                    { 
                        TimeZoneOfficialEnum.Alaska_Daylight_Time, TimeZoneOfficialEnum.Alaska_Standard_Time 
                    }
                )
                .Count()
            );

            //--- All of them include custom ToString() overloads displaying the most relevant information.
            PrintSampleItem("Ini2", new Offset(-5m).ToString());
            PrintSampleItem("Ini3", new Country("ES").ToString());
            PrintSampleItem("Ini4", new DateP(DateTime.Now).ToString());

            //--- All of them are implicitly convertible to 1-argument constructors.
            PrintSampleItem("Ini5", ((Offset)(-5m)).ToString());
            PrintSampleItem("Ini6", ((Country)"ES").ToString());
            PrintSampleItem("Ini7", ((DateP)DateTime.Now).ToString());

            //--- All the errors are managed internally (no exceptions) and communicated via simple enum.
            PrintSampleItem("Ini8", new Offset(50m));
            PrintSampleItem("Ini9", (Country)CountryEnum.None);

            //------ DateP is the main class dealing with date/time(-parsing) actions.

            //--- It can be instantiated by relying on standard .NET formats or in custom ones.
            PrintSampleItem("Date1", new DateP("2-1-2001", new StandardDateTimeFormat(new CultureInfo("en-US").DateTimeFormat)));
            PrintSampleItem("Date2", new DateP("2-1-2001", new CustomDateTimeFormat("day-month-year")));
            PrintSampleItem("Date3", new DateP(DateTime.Now, TimeZoneUTCEnum.Minus_12));
            PrintSampleItem
            (
                "Date4", new DateP
                 (
                    "2 january-2001", (CustomDateTimeFormat)new DateTimeParts[] 
                    {
                        DateTimeParts.Day, DateTimeParts.Month, DateTimeParts.Year
                    }
                 )
            );
         
            //--- All the modifications of the DateP's global properties are inmediately updated in the associated DateTime value. 
            PrintSampleItem("Date5", new DateP("2-1-2001 14:30", -1m) { TimeZoneOffset = 5m });
            PrintSampleItem
            (
                "Date6", new DateP("30-4-2017", (CustomDateTimeFormat)"day-month-year") { Week = DayOfWeek.Wednesday }
            );

            //--- DateP also supports comparison operators.
            PrintSampleItem("Date7", (new DateP("30-4-2017 14:30", 0m) < new DateP("30-4-2017 18:30", 4m)));
            PrintSampleItem("Date8", (new DateP("30-4-2017 14:30", 0m) >= new DateP("30-4-2017 18:30", 4m)));

            //--- DateP and their instances also include various public methods.
            PrintSampleItem("Date9", DateP.GetCustomKeywordFromTimePart(TimeParts.Millisecond));
            PrintSampleItem
            (
                "Date10", new DateP("30-4-2017 14:30", 1m).AdaptTimeToTimezone(TimeZoneIANAEnum.America_Aruba)
            );

            //------ The part dealing with timezones includes lots of information.

            //--- There are quite a few ways to manage a relevant number of features and classifications.
            PrintSampleItem("TZ1", (TimeZones)"+1");
            PrintSampleItem
            (
                "TZ2", new TimeZoneOfficial(((TimeZoneOfficial)TimeZoneOfficialEnum.Greenwich_Mean_Time).Abbreviation)
            );
            PrintSampleItem
            (
                "TZ3", TimeZoneInfo.FindSystemTimeZoneById
                (
                    new TimeZoneWindows(TimeZoneWindowsEnum.Caucasus_Standard_Time).Name
                )
                .Id
            );
            //--- All these classes are also compatible with DateP and with other secondary ones.
            PrintSampleItem("TZ4", new DateP("1-1-2001 10:00", ((TimeZoneMilitary)"B").Offset));
            PrintSampleItem("TZ5", new HourMinute((Offset)10.5m));

            //--- There are a relevant amount of geographical information too.
            PrintSampleItem("TZ6", new Country("es"));
            PrintSampleItem("TZ7", new TimeZonesCountry(CountryEnum.Andorra));
            PrintSampleItem("TZ8", new TimeZonesCountry("bristol"));

            Console.WriteLine();
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();
            Console.ReadLine();
        }

        private static void PrintSampleItem(string sampleId, dynamic input)
        {
            Console.WriteLine
            (
                sampleId + " -- "
                + input.ToString()
                + Environment.NewLine
            );
        }
    }
}
