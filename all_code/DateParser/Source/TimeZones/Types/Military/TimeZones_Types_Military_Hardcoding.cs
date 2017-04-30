using System;
using System.Linq;
using System.Collections.Generic;

namespace FlexibleParser
{
    public enum TimeZoneMilitaryEnum
    {
        None = 0,
        Alpha = 1, Bravo = 2, Charlie = 3, Delta = 4, Echo = 5, Foxtrot = 6, Golf = 7,
        Hotel = 8, India = 9, Kilo = 10, Lima = 11, Mike = 12, November = 13, Oscar = 14,
        Papa = 15, Quebec = 16, Romeo = 17, Sierra = 18, Tango = 19, Uniform = 20,
        Victor = 21, Whiskey = 22, X_Ray = 23, Yankee = 24, Zulu = 25
    }

    internal partial class TimeZoneMilitaryInternal
    {
        internal static TimeZoneMilitaryEnum GetMilitaryTimeZoneFromUTC(TimeZoneUTCEnum utc)
        {
            if (utc == TimeZoneUTCEnum.UTC) return TimeZoneMilitaryEnum.Zulu;

            string utcString = utc.ToString();
            
            foreach (string symbol in new string[] { "Plus_", "Minus_" })
            {
                string[] temp = utcString.Split
                (
                    new string[] { symbol }, StringSplitOptions.None
                );

                if (temp.Length == 2 && temp[1].FirstOrDefault(x => !char.IsDigit(x)) == '\0')
                {
                    return (TimeZoneMilitaryEnum)
                    (
                        Convert.ToInt32(temp[1]) + 
                        (
                            symbol == "Plus_" ? 0 : 12    
                        )
                    );
                }
            }

            return TimeZoneMilitaryEnum.None;
        }

        public static string GetAbbreviationFromMilitaryTimeZone(TimeZoneMilitaryEnum timezoneMilitary)
        {
            return 
            (
                timezoneMilitary == TimeZoneMilitaryEnum.None ? "" :
                timezoneMilitary.ToString().Substring(0, 1)
            );
        }

        internal static TimeZones GetMilitaryTimeZoneFromAbbreviationFull(string abbreviation)
        {
            var timezone = GetMilitaryTimeZoneFromAbbreviation(abbreviation);
            
            return
            (
                timezone == TimeZoneMilitaryEnum.None ? null : new TimeZones(timezone)
                
            );
        }

        internal static TimeZoneMilitaryEnum GetMilitaryTimeZoneFromAbbreviation(string abbreviation)
        {
            if (abbreviation == null) return TimeZoneMilitaryEnum.None;
            abbreviation = abbreviation.Trim().ToUpper();

            var outName = Enum.GetNames(typeof(TimeZoneMilitaryEnum)).FirstOrDefault(x => x.StartsWith(abbreviation));

            return
            (
                outName == null ? TimeZoneMilitaryEnum.None :
                (TimeZoneMilitaryEnum)Enum.Parse(typeof(TimeZoneMilitaryEnum), outName)
            );
        }
    }
}
