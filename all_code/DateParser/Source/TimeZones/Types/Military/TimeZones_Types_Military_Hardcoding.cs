using System;
using System.Linq;

namespace FlexibleParser
{
    ///<summary><para>All the military timezones.</para></summary>
    public enum TimeZoneMilitaryEnum
    {
        ///<summary><para>None.</para></summary>
        None = 0,
        ///<summary><para>Alpha (A) -- UTC +01:00.</para></summary>
        Alpha = 1,
        ///<summary><para>Bravo (B) -- UTC +02:00.</para></summary>
        Bravo = 2,
        ///<summary><para>Charlie (C) -- UTC +03:00.</para></summary>
        Charlie = 3,
        ///<summary><para>Delta (D) -- UTC +04:00.</para></summary>
        Delta = 4,
        ///<summary><para>Echo (E) -- UTC +05:00.</para></summary>
        Echo = 5,
        ///<summary><para>Foxtrot (F) -- UTC +06:00.</para></summary>
        Foxtrot = 6,
        ///<summary><para>Golf (G) -- UTC +07:00.</para></summary>
        Golf = 7,
        ///<summary><para>Hotel (H) -- UTC +08:00.</para></summary>
        Hotel = 8,
        ///<summary><para>India (I) -- UTC +09:00.</para></summary>
        India = 9,
        ///<summary><para>Kilo (K) -- UTC +10:00.</para></summary>
        Kilo = 10,
        ///<summary><para>Lima (L) -- UTC +11:00.</para></summary>
        Lima = 11,
        ///<summary><para>Mike (M) -- UTC +12:00.</para></summary>
        Mike = 12,
        ///<summary><para>November (N) -- UTC -01:00.</para></summary>
        November = 13,
        ///<summary><para>Oscar (O) -- UTC -02:00.</para></summary>
        Oscar = 14,
        ///<summary><para>Papa (P) -- UTC -03:00.</para></summary>
        Papa = 15,
        ///<summary><para>Quebec (Q) -- UTC -04:00.</para></summary>
        Quebec = 16,
        ///<summary><para>Romeo (R) -- UTC -05:00.</para></summary>
        Romeo = 17,
        ///<summary><para>Sierra (S) -- UTC -06:00.</para></summary>
        Sierra = 18,
        ///<summary><para>Tango (T) -- UTC -07:00.</para></summary>
        Tango = 19,
        ///<summary><para>Uniform (U) -- UTC -08:00.</para></summary>
        Uniform = 20,
        ///<summary><para>Victor (V) -- UTC -09:00.</para></summary>
        Victor = 21,
        ///<summary><para>Whiskey (W) -- UTC -10:00.</para></summary>
        Whiskey = 22,
        ///<summary><para>X Ray (X) -- UTC -11:00.</para></summary>
        X_Ray = 23,
        ///<summary><para>Yankee (Y) -- UTC -12:00.</para></summary>
        Yankee = 24,
        ///<summary><para>Zulu (Z) -- UTC +00:00.</para></summary>
        Zulu = 25
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
