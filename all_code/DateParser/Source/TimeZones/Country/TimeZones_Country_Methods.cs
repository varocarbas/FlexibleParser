using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    internal partial class TimeZonesCountryInternal
    {
        public static string CityOrRegion = null;

        public static string TimeZonesCountryToString(TimeZonesCountry timeZonesCountry)
        {
            if (timeZonesCountry == null || timeZonesCountry.Error != ErrorTimeZoneEnum.None)
            {
                return "Invalid country";
            }

            return CountryInternal.CountryToString
            (
                timeZonesCountry.Country, timeZonesCountry.CityOrRegion
            );
        }

        public static CountryEnum GetCountryFromString(string input)
        {
            string input2 = Common.PerformFirstStringChecks(input);
            if (input2 == null || input2 == "none") return CountryEnum.None;

            //Checking whether the input string includes country name or code.
            Country country = new Country(input2);
            if (country.Value != CountryEnum.None) return country.Value;


            var temp = AnalyseCitiesAreas
            (
                Common.GetWordsInString(input2)
            );

            CountryEnum output = 
            (
                temp == null ? CountryEnum.None : temp
            );

            if (output != CountryEnum.None)
            {
                UpdateCityOrRegionValue(input2);
            }

            return output;
        }

        private static void UpdateCityOrRegionValue(string input2)
        {
            CityOrRegion = string.Join
            (
                " ", CapitaliseEachWord
                (
                    Common.GetWordsInString(input2)
                )
            );

            if (CityOrRegion.Contains("-"))
            {
                CityOrRegion = string.Join
                (
                    "-", CapitaliseEachWord
                    (
                        CityOrRegion.Split
                        (
                            new string[] { "-" }, StringSplitOptions.None
                        )
                        , true
                    )
                );
            }
        }

        private static string[] CapitaliseEachWord(string[] words2, bool onlyUneven = false)
        {
            for (int i = 0; i < words2.Length; i++)
            {
                if (onlyUneven && i % 2 == 0) continue;

                words2[i] = words2[i].Substring(0, 1).ToUpper() + words2[i].Substring(1);
            }

            return words2;
        }

        private static dynamic AnalyseCitiesAreas(string[] words2)
        {
            //Analysing (+ loading all this information in memory) all the cities is perhaps the most expensive action which
            //this library can perform. On the other hand, this analysis delivers the most accurate answer under the current
            //conditions and that's why it goes first.
            //Note that the IANAAreas below aren't necessarily associated to just one country (i.e., the expected output of 
            //this analysis) and, consequently, one of various possible options might have to be randomly chosen.
            var temp = AnalyseAllCities(string.Join(" ", words2));
            if (temp != null) return temp;

            var temp2 = TimeZoneIANAInternal.TimeZoneIANAAreas.FirstOrDefault
            (
                x => x.Value.FirstOrDefault
                (
                    y => ThresholdIsMet
                    (
                        Common.GetWordsInString
                        (
                            y.ToString().ToLower()
                        ),
                        words2, (words2.Length <= 3 ? .9m : .75m)
                    )
                )
                != IANACityRegionEnum.None
            )
            .Key;

            if (temp2 == TimeZoneIANAEnum.None) return null;

            return TimeZoneIANAInternal.TimeZoneIANACountries[temp2].First();
        }

        private static bool ThresholdIsMet(string[] words2, string[] target, decimal threshold)
        {
            if (words2.Length >= .5m * target.Length && words2.Length <= target.Length)
            { }
            else return false;

            return
            (
                words2.Intersect(target).Count() >= threshold * words2.Length
            );
        }

        private static dynamic AnalyseAllCities(string input2)
        {
            Dictionary<CountryEnum, string[]> dict = Geography.GetAllCities();

            var temp = dict.FirstOrDefault(x => x.Value.FirstOrDefault(y => y.ToLower() == input2) != null);
            if (temp.Value == null) return null;

            return temp.Key;
        }
    }
}
