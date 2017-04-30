using System;
using System.Linq;
using System.Collections.Generic;

namespace FlexibleParser
{
    internal partial class TimeZonesInternal
    {
        internal static string CorrectEnumString(string input, Type type, bool fromEnum = true)
        {
            if (input == null) return "None";
            
            string input2 = Common.PerformFirstStringChecks(input);
            if (input2.Length < 1 || input2 == "none"|| !TimeZonesInternal.TimeZoneEnumTypes.Contains(type))
            {
                return "None";
            }

            return
            (
                type == typeof(TimeZoneUTCEnum) ? 
                CorrectEnumStringUTC(input2, fromEnum) :
                CorrectEnumStringOthers(input2, type, fromEnum)
            );
        }

        private static Dictionary<string, string> CommonEnumStringUTC = 
        new Dictionary<string, string>()
        {
            { "plus_", "+" }, { "minus_", "-" }, { "_", ":" }
        };

        private static string CorrectEnumStringUTC(string input2, bool fromEnum = true)
        {
            string output = input2;

            if (fromEnum)
            {
                if (output == "utc") return "UTC";
            }
            else
            {
                output = output.Replace("utc", "");
            }

            return (fromEnum ? "UTC " : "") + ReplaceCommonElements
            (
                output, CommonEnumStringUTC, fromEnum
            );
        }

        private static string ReplaceCommonElements(string output, Dictionary<string, string> replacements, bool fromEnum)
        {
            if (output.Contains("mid-")) return output;

            foreach (var item in replacements)
            {
                output =
                (
                    fromEnum ? output.Replace(item.Key, item.Value) :
                    output.Replace(item.Value, item.Key)
                );
            }

            return output;
        }

        private static Dictionary<string, string> CommonEnumStringOthers =
        new Dictionary<string, string>()
        {
            { "_plus_", "+" }, { "_minus_", "-" }
        };

        private static string[] SeparatorsEnumStringOthers = new string[] { " ", "/" };

        private static string CorrectEnumStringOthers(string input2, Type type, bool fromEnum = true)
        {
            string output = ReplaceCommonElements
            (
                input2, CommonEnumStringOthers, fromEnum
            );

            KeyValuePair<string, string> separators = new KeyValuePair<string, string> 
            (
                fromEnum ? "_" : SeparatorsEnumStringOthers[0],
                fromEnum ? SeparatorsEnumStringOthers[0] : "_"
            );
            if (!output.Contains(separators.Key))
            {
                return output.Substring(0, 1).ToUpper() + output.Substring(1);
            }

            string[] temp = output.Split
            (
                new string[] { separators.Key }, StringSplitOptions.None
            )
            .Select
            (
                x => 
                (
                    x.Substring(0, 1) == "(" ? x.Substring(1, 1) : x.Substring(0, 1)
                )
                .ToUpper() + x.Substring(1)
             )
             .ToArray();
            
            if (type != typeof(TimeZoneIANAEnum))
            {
                return CorrectSpecial
                (
                    string.Join(separators.Key, temp).Replace
                    (
                        separators.Key, separators.Value
                    ), 
                    type, fromEnum
                );
            }

            if (fromEnum)
            {
                output = temp[0] + "/";
                int start = 1;
                if (temp[1] == "Argentina" || temp[1] == "Indiana")
                {
                    output += temp[1] + "/";
                    start = 2;
                }

                for (int i = start; i < temp.Length; i++)
                {
                    output += temp[i] + " ";
                }
                output = output.Trim();
            }

            return output;
        }

        private static string CorrectSpecial(string outString, Type type, bool fromEnum)
        {
            if (type == typeof(TimeZoneOfficialEnum))
            {
                return TimeZonesInternal.CorrectOfficialSpecial(outString, fromEnum);
            }

            if (type == typeof(TimeZoneConventionalEnum))
            {
                return TimeZonesInternal.CorrectConventionalSpecial(outString, fromEnum);
            }

            if (type == typeof(TimeZoneWindowsEnum))
            {
                return TimeZonesInternal.CorrectWindowsSpecial(outString, fromEnum);
            }

            return outString;
        }
    }
}
