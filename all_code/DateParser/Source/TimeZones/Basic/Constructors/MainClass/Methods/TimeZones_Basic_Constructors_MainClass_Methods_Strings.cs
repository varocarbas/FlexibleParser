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

                if (item2 != null) return item2;
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
            input2 = TimeZonesInternal.CorrectEnumString
            (
                input2, typeof(TimeZoneWindowsEnum), false
            );

            List<Type> types =
            (
                target == null ? new List<Type>(TimeZoneEnumTypes) :
                new List<Type>() { target }
            );

            foreach (Type type in types)
            {
                dynamic output = ParseStringToEnum(input2, type);
                if (!EnumIsNothing(output)) return output;
            }

            return null;
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
