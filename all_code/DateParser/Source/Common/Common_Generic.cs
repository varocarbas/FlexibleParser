using System;
using System.Collections.Generic;

namespace FlexibleParser
{
    internal partial class Common
    {
        public static void JustToAvoidCompilerWarnings()
        {
            dynamic dumbArray = new dynamic[]
            {
                TimeZoneOfficial.Populated, TimeZoneIANA.Populated,
                TimeZoneConventional.Populated, TimeZoneUTC.Populated,
                TimeZoneWindows.Populated, TimeZoneMilitary.Populated,
                TimeZonesCountry.Populated
            };

            for (int i = 1; i < dumbArray.Length; i++)
            {
                if (dumbArray[i] == dumbArray[i - 1]) break;
            }
        }

        public static bool DecimalOffsetIsValid(decimal decimalOffset)
        {
            foreach (TimeZoneUTCEnum utc in Enum.GetValues(typeof(TimeZoneUTCEnum)))
            {
                if (utc == TimeZoneUTCEnum.None) continue;

                if (new Offset(utc).DecimalOffset == decimalOffset)
                {
                    return true;
                }
            }

            return false;
        }

        //Non-alphanumeric characters which aren't required to understand any string-parsing scenario. 
        private static string[] redundants = new string[]
        {
            ".", ",", ";", "!", "?", "@", "#", "%", "'", "\"", 
            "=", "(", ")", "[", "]", "{", "}", "<", ">"
        };

        //Method performing the actions which are required before starting to analyse any input string 
        //(e.g., all the analyses expect ToLower() inputs).
        public static string PerformFirstStringChecks(string input)
        {
            if (input == null || input.Trim().Length < 1) return null;

            string input2 = input.Trim().ToLower(); 

            foreach (string redundant in redundants)
            {
                input2 = input2.Replace(redundant, "");
            }

            string[] words2 = input2.Split
            (
                new string[] { " " }, StringSplitOptions.None 
            );

            for (int i = 0; i < words2.Length; i++)
            {
                words2[i] = words2[i].Trim();
            }

            return string.Join(" ", words2);
        }

        public static string[] GetWordsInString(string input)
        {
            return input.Split
            (
                new string[] { " ", "_" },
                StringSplitOptions.RemoveEmptyEntries
            );
        }
    }

    internal class TemporaryVariables
    {
        public List<dynamic> Vars { get; set; }

        public TemporaryVariables()
        {
            Vars = new List<dynamic>();
        }

        //This constructor is only called from GetGlobalValues to populate a Timezone-inherited instance.
        public TemporaryVariables(dynamic value, Type type)
        {
            Vars = new List<dynamic>();

            for (int i = 0; i < TimezoneDefaults.Length; i++)
            {
                Vars.Add
                (
                    TimezoneDefaults[i].GetType() == type ? 
                    value : TimezoneDefaults[i]
                );
            }
        }

        private dynamic[] TimezoneDefaults = new dynamic[] 
        {
            new List<TimeZoneOfficialEnum>() { TimeZoneOfficialEnum.None }.AsReadOnly(), 
            new List<TimeZoneIANAEnum>() { TimeZoneIANAEnum.None }.AsReadOnly(),
            new List<TimeZoneConventionalEnum>() { TimeZoneConventionalEnum.None }.AsReadOnly(),
            TimeZoneWindowsEnum.None, TimeZoneUTCEnum.None, TimeZoneMilitaryEnum.None
        };

        public TemporaryVariables(List<dynamic> vars)
        {
            Vars = new List<dynamic>();

            foreach (dynamic variable in vars)
            {
                Vars.Add(variable);
            }
        }
    }
}
