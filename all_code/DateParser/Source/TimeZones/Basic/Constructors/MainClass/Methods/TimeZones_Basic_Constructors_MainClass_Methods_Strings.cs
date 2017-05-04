using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    internal partial class TimeZonesInternal
    {
        internal static TemporaryVariables GetGlobalValuesFromString(string input, Type target = null)
        {
            if (input == null) return null;

            dynamic item = GetEnumItemFromString(input);
            if (item == null) return null;

            return
            (
                target == null ?
                GetGlobalValuesInternal(item, item.GetType()) : 
                GetGlobalValuesSpecific(item, item.GetType(), target)
            );
        }

        private static dynamic GetEnumItemFromString(string input)
        {
            if (input == null) return null;

            string input2 = Common.PerformFirstStringChecks(input);

            if (input2.Length == 1)
            {
                var temp = AnalyseOneCharacter(input2);
                if (temp != TimeZoneMilitaryEnum.None)
                {
                    return temp;
                }
            }

            dynamic temp2 = AnalyseEnumNames(input2);
            if (!EnumIsNothing(temp2)) return temp2;

            temp2 = AnalyseAbbreviations(input2);
            if (!EnumIsNothing(temp2)) return temp2;

            temp2 = AnalyseKeywords(input2);
            if (!EnumIsNothing(temp2)) return temp2;

            return null;
        }

        private static dynamic AnalyseOneCharacter(string input2)
        {
            var military = TimeZoneMilitaryInternal.GetMilitaryTimeZoneFromAbbreviation(input2);

            return
            (
                EnumIsNothing(military, typeof(TimeZoneMilitaryEnum)) ?
                TimeZoneMilitaryEnum.None : military
            );
        }

        private static dynamic AnalyseAbbreviations(string input2)
        {
            for (int i = 0; i < 2; i++)
            {
                var item2 = AnalyseAbbreviation(input2, i);

                if (!EnumIsNothing(item2)) return item2;
            }

            return null;
        }

        private static dynamic AnalyseAbbreviation(string input2, int i)
        {
            dynamic temp = null;

            if (i == 0)
            {
                return TimeZoneOfficial.GetOfficialFromAbbreviation(input2);
            }
            else
            {
                temp = TimeZoneConventionalInternal.TimeZoneConventionalAbbreviations.FirstOrDefault
                (
                    x => x.Value.ToLower() == input2
                );
            }

            return
            (
                temp.Value == null ? null : temp.Key
            );
        }

        internal static dynamic AnalyseEnumNames(string input2, Type target = null)
        {
            input2 = CorrectEnumString
            (
                input2, typeof(TimeZoneWindowsEnum), false
            );

            List<Type> types =
            (
                target == null ? new List<Type>(TimeZoneEnumTypes) :
                new List<Type>() { target }
            );

            var temp = CheckInputAsAWhole(input2, types);
            if (temp != null) return temp;

            return CheckInputWords(input2, types);
        }

        //The timezone-type-enum values show the exact name of the given timezone, but some characters have
        //to be replaced. This method tries to directly parse the input (i.e., a perfect match is expected)
        //after performing the corresponding replacements.
        private static dynamic CheckInputAsAWhole(string input2, List<Type> types)
        {
            foreach (Type type in types)
            {
                dynamic output = ParseStringToEnum(input2, type);
                if (!EnumIsNothing(output)) return output;
            }

            return null;
        }

        //CheckInputAsAWhole might not detect many input scenarios despite being clearly associated with a 
        //specific timezone. That's why this slower word-by-word approach has also to be used.
        private static dynamic CheckInputWords(string input2, List<Type> types)
        {
            string[] words2 = Common.GetWordsInString
            (
                Common.PerformFirstStringChecks(input2)
            );

            foreach (Type type in types)
            {
                foreach (var item in Enum.GetValues(type))
                {
                    if (EnumIsNothing(item)) continue;

                    string[] itemWords = Common.GetWordsInString
                    (
                        Common.PerformFirstStringChecks(item.ToString())
                    );

                    var remainings = itemWords.Except(words2);
                    if (remainings.Count() == 0) return item;
                    if (remainings.Count() < 3)
                    {
                        if (CheckInputWordsRemainingIsOK(remainings))
                        {
                            return item;
                        }
                    }
                }
            }

            return null;
        }

        private static string[] OKToRemain = new string[]
        {
            "the", "of", "at", "in", "for", "a", "an", "or", "on", "with"
        };

        private static bool CheckInputWordsRemainingIsOK(IEnumerable<string> remainings)
        {
            foreach (string remaining in remainings)
            {
                if (!OKToRemain.Contains(remaining)) return false;
            }

            return true;
        }

        private static dynamic ParseStringToEnum(string input2, Type type)
        {
            if (type == typeof(TimeZoneOfficialEnum))
            {
                TimeZoneOfficialEnum output = TimeZoneOfficialEnum.None;
                Enum.TryParse(input2, true, out output);

                return output;
            }
            else if (type == typeof(TimeZoneIANAEnum))
            {
                TimeZoneIANAEnum output = TimeZoneIANAEnum.None;
                Enum.TryParse(input2, true, out output);

                return output;
            }
            else if (type == typeof(TimeZoneConventionalEnum))
            {
                TimeZoneConventionalEnum output = TimeZoneConventionalEnum.None;
                Enum.TryParse(input2, true, out output);

                return output;
            }
            else if (type == typeof(TimeZoneUTCEnum))
            {
                TimeZoneUTCEnum output = TimeZoneUTCEnum.None;
                Enum.TryParse(input2, true, out output);

                return output;
            }
            else if (type == typeof(TimeZoneWindowsEnum))
            {
                TimeZoneWindowsEnum output = TimeZoneWindowsEnum.None;
                Enum.TryParse(input2, true, out output);

                return output;
            }
            else if (type == typeof(TimeZoneMilitaryEnum))
            {
                TimeZoneMilitaryEnum output = TimeZoneMilitaryEnum.None;
                Enum.TryParse(input2, true, out output);

                return output;
            }

            return null;
        }

        private static dynamic AnalyseKeywords(string input2)
        {
            string[] words2 = Common.GetWordsInString(input2);
            decimal threshold = (words2.Length <= 3 ? .9m : .75m);

            var temp = AllNames.FirstOrDefault
            (
                x => ThresholdIsMet
                (
                    words2, Common.GetWordsInString(x.Value.ToLower().Trim()), threshold
                ) 
            );

            return
            (
                temp.Value == null ? null : temp.Key
            ); 
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
    }
}
