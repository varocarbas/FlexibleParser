using System;
using System.Linq;

namespace FlexibleParser
{
    internal partial class Common
    {
        //Called from the public Equals methods to account for null-input scenarios.
        public static bool NoNullEquals(dynamic first, dynamic second, Type type)
        {
            return
            (
                object.Equals(first, null) ? object.Equals(second, null) : first.Equals(second)
            );
        }

        private static dynamic[] Firsts, Seconds;

        //Method called from all the public comparison methods of all the public classes (e.g., > or <=).
        public static int PerformComparison(dynamic first, dynamic second, Type type)
        {
            Firsts = new dynamic[] { first, null };
            Seconds = new dynamic[] { second, null };

            for (int i = 0; i < 2; i++)
            {
                int temp = Common.PerformComparisonNulls(Firsts[i], Seconds[i]);
                if (temp != -2) return temp;

                if (i == 0)
                {
                    temp = CheckErrors(first, second, type);
                    if (temp != -2) return temp;

                    temp = CheckSpecialComparisons(type);
                    if (temp != -2) return temp;

                    //Updating firsts and seconds with the corresponding values to check for
                    //nulls too and, eventually, to perform the final comparison.
                    GetSecondRoundToCompare(type);
                }
            }

            return Firsts[1].CompareTo(Seconds[1]);
        }

        private static Type[] TimeZoneErrorTypes = new Type[]
        {
            typeof(TimeZoneOfficial), typeof(TimeZoneIANA), typeof(TimeZoneConventional),
            typeof(TimeZoneUTC), typeof(TimeZoneWindows), typeof(TimeZoneMilitary),
            typeof(TimeZones), typeof(TimeZonesCountry), typeof(Offset), typeof(HourMinute)
        };

        private static int CheckErrors(dynamic first, dynamic second, Type type)
        {
            dynamic target = null;

            if (TimeZoneErrorTypes.Contains(type))
            {
                target = ErrorTimeZoneEnum.None;
            }
            else if (type == typeof(DateP))
            {
                target = ErrorDateEnum.None;
            }
            else if (type == typeof(Country))
            {
                target = ErrorCountryEnum.None;
            }
            else return -2;


            if (first.Error == target && second.Error == target)
            {
                return -2;
            }

            if (first.Error == second.Error) return 0;

            return
            (
                first.Error != target ? -1 : 1
            );
        }

        //This method accounts for types which don't fully verify the MainInstance.ValueToCheck ideas.
        private static int CheckSpecialComparisons(Type type)
        {
            if (type == typeof(HourMinute))
            {
                return HourMinuteInternal.GetDecimalFromHourMinute(Firsts[0]).CompareTo
                (
                    HourMinuteInternal.GetDecimalFromHourMinute(Seconds[0])
                );
            }
            else if (type == typeof(DateP))
            {
                return DatesInternal.PerformComparisonValid
                (
                    Firsts[0], Seconds[0]
                );
            }
            else if (type == typeof(TimeZones))
            {
                return PerformComparison
                (
                    Firsts[0].Offset, Seconds[0].Offset, typeof(Offset)
                );
            }
            else if (type == typeof(CustomDateTimeFormat))
            {
                return CheckCustomFormat();
            }
            else if (type == typeof(StandardDateTimeFormat))
            {
                return CheckStandardFormat();
            }

            return -2;
        }

        private static int CheckStandardFormat()
        {
            int temp = CheckFormatsInternal
            (
                new dynamic[] { Firsts[0].DateTimeStyle, Firsts[0].UseParseExact },
                new dynamic[] { Seconds[0].DateTimeStyle, Seconds[0].UseParseExact },
                typeof(bool)
            );
            if (temp != 0) return temp;

            temp = ComparePatterns();
            if (temp != 0) return temp;

            return PerformComparison
            (
                Firsts[0].FormatProvider, Firsts[0].FormatProvider, typeof(string)
            );
        }

        private static int ComparePatterns()
        {
            if (Firsts[0].Patterns == null || Seconds[0].Patterns == null)
            {
                return PerformComparisonNulls
                (
                    Firsts[0].Patterns, Seconds[0].Patterns
                );
            }

            for (int i = 0; i < Firsts[0].Patterns.Length; i++)
            {
                int temp = PerformComparison
                (
                    Firsts[0].Patterns[i], Firsts[0].Patterns[i], typeof(string)
                );
                if (temp != 0) return temp;
            }

            return 0;
        }

        private static int CheckCustomFormat()
        {
            if (Firsts[0].IgnoreIrrelevantCharacters == Seconds[0].IgnoreIrrelevantCharacters)
            {
                return CheckFormatsInternal
                (
                    new dynamic[] { Firsts[0].Pattern, Firsts[0].FormatProvider },
                    new dynamic[] { Seconds[0].Pattern, Seconds[0].FormatProvider },
                    typeof(string)
                );
            }

            return Firsts[0].IgnoreIrrelevantCharacters.CompareTo
            (
                Seconds[0].IgnoreIrrelevantCharacters
            );
        }

        //Method helping to compare two instances of the precisely-not-too-simple of the DateTimeFormat
        //classes (i.e., CustomDateTimeFormat and StandardDateTimeFormat). More specifically, it analyses
        //recursively variables which are compared as strings.
        private static int CheckFormatsInternal(dynamic[] properties1, dynamic[] properties2, Type type)
        {
            for (int i = 0; i < 2; i++)
            {
                if (properties1[i] != properties2[i])
                {
                    return PerformComparison
                    (
                        properties1[i], properties2[i], type
                    );
                }
            }

            return 0;
        }

        //After having confirmed that the input variable isn't null, it is time to check whether the main
        //value is also non-null (or non-"None").
        private static void GetSecondRoundToCompare(Type type)
        {
            if (type == typeof(Offset))
            {
                Firsts[1] = Firsts[0].DecimalOffset;
                Seconds[1] = Seconds[0].DecimalOffset;
            }
            else if (type == typeof(Country) || type == typeof(TimeZonesCountry))
            {
                Firsts[1] = Firsts[0].Country;
                Seconds[1] = Seconds[0].Country;
            }
            else
            {
                Firsts[1] = Firsts[0].Value;
                Seconds[1] = Seconds[0].Value;
            }
        }

        public static int PerformComparisonNulls(dynamic first, dynamic second)
        {
            if (first == null || second == null)
            {
                if (first == second) return 0;

                return
                (
                    first == null ? -1 : 1
                );
            }

            return -2;
        }
    }
}
