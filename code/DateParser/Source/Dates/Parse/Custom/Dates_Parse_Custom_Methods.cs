using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;

namespace FlexibleParser
{
    internal partial class DatesInternal
    {
        public static DateTime? FromStringCustomFormat(string input, CustomDateTimeFormat format)
        {
            if (format == null || format.Pattern2.Length < 1)
            {
                return FromStringStandardFormat
                (
                    input, new StandardDateTimeFormat(format.FormatProvider)
                );
            }

            return CustomFormatToStringDate(input, format);
        }

        public static string ToStringCustomFormat(DateTime input, CustomDateTimeFormat format)
        {
            return CustomFormatToStringDate
            (
                input, format
            );
        }

        private static dynamic CustomFormatToStringDate(dynamic input, CustomDateTimeFormat format)
        {
            bool stringIsReturned = true;
            TemporaryVariables tempVars = null;

            if (input.GetType() == typeof(string))
            {
                stringIsReturned = false;
                tempVars = new TemporaryVariables
                (
                    new List<dynamic>()
                    {
                        format.Pattern2, (DateTime?)DateTime.Now, input
                    }
                );
            }
            else
            {
                tempVars = new TemporaryVariables
                (
                    new List<dynamic>()
                    {
                        format.Pattern2, "", null
                    }
                );
            }

            for (int i = 0; i < tempVars.Vars[0].Length; i++)
            {
                tempVars = CustomFormatToStringDate2
                (
                    input, format, tempVars, stringIsReturned
                );

                if (!stringIsReturned && tempVars.Vars[1] == null)
                {
                    break;
                }
            }

            return tempVars.Vars[1];
        }

        private static TemporaryVariables CustomFormatToStringDate2(dynamic input, CustomDateTimeFormat format, TemporaryVariables tempVars, bool stringIsReturned)
        {
            DatePatternBit patternBit = GetNextPatternBit
            (
                tempVars.Vars[0], (stringIsReturned ? null : tempVars.Vars[2]), format.IgnoreIrrelevantCharacters
            );
            if (patternBit == null) return tempVars;

            tempVars.Vars[0] = patternBit.RemainingPattern;
            if (!stringIsReturned)
            {
                tempVars.Vars[2] = patternBit.RemainingInput;
            }

            return
            (
                stringIsReturned ?
                CustomFormatStringIsReturned(input, format, tempVars, patternBit) :
                CustomFormatDateTimeIsReturned
                (
                    patternBit.CurrentValue, format, tempVars, patternBit
                )
            );
        }

        private static TemporaryVariables CustomFormatDateTimeIsReturned(string input, CustomDateTimeFormat format, TemporaryVariables tempVars, DatePatternBit patternBit)
        {
            tempVars.Vars[1] = FormatToStringDate
            (
                input, patternBit.Part, format.FormatProvider, false, tempVars.Vars[1]
            );

            return tempVars;
        }

        private static TemporaryVariables CustomFormatStringIsReturned(DateTime input, CustomDateTimeFormat format, TemporaryVariables tempVars, DatePatternBit patternBit)
        {
            string bitToString = FormatToStringDate
            (
                input, patternBit.Part, format.FormatProvider, true, null
            );

            tempVars.Vars[1] += patternBit.Inbetweeners +
            (
                bitToString == null ? "" : bitToString
            );

            return tempVars;
        }

        private static DateTime? FormatToStringDateDateTimeIsReturned(dynamic input, DateTimeInternalParts part, DateTimeFormatInfo formatInfo, DateTime? varSoFar)
        {
            if (part == DateTimeInternalParts.DayWeek)
            {
                return null;
            }

            int input_int = 0;
            if (part == DateTimeInternalParts.MonthName)
            {
                input_int = GetMonthFromName(input, formatInfo);
                if (input_int < 1) return null;
                part = DateTimeInternalParts.Month;
            }
            else
            {
                if (((string)input).FirstOrDefault(x => !char.IsNumber(x)) != '\0')
                {
                    input_int = GetMonthFromName(input, formatInfo);
                    if (input_int < 1 || part != DateTimeInternalParts.Month)
                    {
                        return null;
                    }
                }
                else input_int = Convert.ToInt32(input);
            }

            return AddPartToDate
            (
                input_int, part, varSoFar, formatInfo
            );
        }

        private static int GetMonthFromName(string input, DateTimeFormatInfo formatInfo)
        {
            input = input.Trim().ToLower();
            if (input.Length < 1) return 0;

            List<DateTimeFormatInfo> formatInfos = new List<DateTimeFormatInfo>() { formatInfo };
            if (formatInfo.MonthNames[0] != "January")
            {
                formatInfos.Add(CultureInfo.InvariantCulture.DateTimeFormat);
            }

            foreach (DateTimeFormatInfo item in formatInfos)
            {
                string[][] nameArrays = new string[][]
                {
                    item.MonthNames.Select(x => x.ToLower()).ToArray(), 
                    item.MonthGenitiveNames.Select(x => x.ToLower()).ToArray(),  
                    item.AbbreviatedMonthNames.Select(x => x.ToLower()).ToArray(), 
                    item.AbbreviatedMonthGenitiveNames.Select(x => x.ToLower()).ToArray()
                };

                foreach (string[] names in nameArrays)
                {
                    int out_int = Array.IndexOf(names, input);
                    if (out_int >= 0) return out_int + 1;
                }
            }

            return 0;
        }

        private static string FormatToStringDateStringIsReturned(DateTime input, DateTimeInternalParts part, DateTimeFormatInfo formatInfo)
        {
            if (part == DateTimeInternalParts.Year)
            {
                return input.Year.ToString();
            }
            else if (part == DateTimeInternalParts.Month)
            {
                return input.Month.ToString();
            }
            else if (part == DateTimeInternalParts.MonthName)
            {
                return formatInfo.GetMonthName
                (
                    input.Month
                );
            }
            else if (part == DateTimeInternalParts.DayWeek)
            {
                return formatInfo.GetDayName
                (
                    input.DayOfWeek
                );
            }
            else if (part == DateTimeInternalParts.Day)
            {
                return input.Day.ToString();
            }
            else if (part == DateTimeInternalParts.Hour)
            {
                return input.Hour.ToString();
            }
            else if (part == DateTimeInternalParts.Minute)
            {
                return input.Minute.ToString();
            }
            else if (part == DateTimeInternalParts.Second)
            {
                return input.Second.ToString();
            }
            else if (part == DateTimeInternalParts.Millisecond)
            {
                return input.Millisecond.ToString();
            }

            return null;
        }

        private static dynamic FormatToStringDate(dynamic input, DateTimeInternalParts part, DateTimeFormatInfo formatInfo, bool stringIsReturned, dynamic varSoFar)
        {
            return
            (
                !stringIsReturned ? FormatToStringDateDateTimeIsReturned
                (
                    input, part, formatInfo, varSoFar
                ) 
                : FormatToStringDateStringIsReturned
                (
                    input, part, formatInfo
                )
            );
        }

        private static DatePatternBit GetNextPatternBit(string pattern, string input, bool ignoreIrrelevant)
        {
            return
            (
                input == null ? GetNextPatternBitStringIsReturned(pattern) :
                GetNextPatternBitDateTimeIsReturned(pattern, input, ignoreIrrelevant)
            );
        }

        private static DatePatternBit GetNextPatternBitDateTimeIsReturned(string pattern, string input, bool ignoreIrrelevant)
        {
            KeyValuePair<int, string> nextMatch = GetNextKeyword(pattern);
            if (nextMatch.Key < 0) return null;

            input = input.Trim();
            DatePatternBit datePatternBit = GetCommonDatePatternBit
            (
                nextMatch, pattern
            );

            KeyValuePair<int, string> nextMatch2 = GetNextKeyword(datePatternBit.RemainingPattern);
            string endChar = "";
            if (nextMatch2.Key >= 0)
            {
                DatePatternBit datePatternBit2 = GetCommonDatePatternBit
                (
                    nextMatch2, datePatternBit.RemainingPattern
                );

                endChar = datePatternBit2.Inbetweeners;
            }

            TemporaryVariables tempVars = GetStringBitMatchingDatePart
            (
                input, endChar, ignoreIrrelevant
            );
            if (tempVars == null || tempVars.Vars[0] == null || tempVars.Vars[1] == null)
            {
                return null;
            }

            datePatternBit.CurrentValue = tempVars.Vars[0];
            datePatternBit.RemainingInput = tempVars.Vars[1];
            if (endChar.Length > 0)
            {
                if (datePatternBit.RemainingInput.Length > endChar.Length)
                {
                    datePatternBit.RemainingInput = datePatternBit.RemainingInput.Substring
                    (
                        endChar.Length
                    );
                }

                if (datePatternBit.RemainingInput.Length > endChar.Length)
                {
                    datePatternBit.RemainingPattern = datePatternBit.RemainingPattern.Substring
                    (
                        endChar.Length
                    );
                }
            }

            return datePatternBit;
        }

        private static string[] DateTimeSeparators = new string[]
        {
            "-", "/", ".", ":"
        };

        private static bool IrrelevantFound(string input, int i)
        {
            string bit = input.Substring(i, 1).Trim();
            if (bit.Length == 0 || DateTimeSeparators.Contains(bit))
            {
                return true;
            }
            else if (Char.IsNumber(Convert.ToChar(bit))) return false;

            return BeforeIrrelevantIsOK(input.Substring(0, i));
        }

        private static bool BeforeIrrelevantIsOK(string previous)
        {
            int tempInt = 0;

            return int.TryParse(previous, out tempInt); 
        }

        private static TemporaryVariables GetStringBitMatchingDatePart(string input, string endChar, bool ignoreIrrelevant)
        {
            bool endFound = false;
            endChar = endChar.Trim().ToLower();

            string outVal = "";
            int lastI = -1;
            int lastInput = input.Length - 1;
            for (int i = 0; i <= lastInput; i++)
            {
                string bit = input.Substring(i, 1).Trim().ToLower();
                if (bit.Length < 1) break;

                if (bit == endChar || (ignoreIrrelevant && IrrelevantFound(input, i)))
                {
                    endFound = true;
                    break;
                }
                else outVal += bit;
                lastI = i;
            }
            lastI += 1;

            return
            (
                (!ignoreIrrelevant && endChar.Length > 0 && !endFound)
                || lastI == 0 ? null : new TemporaryVariables
                (
                    new List<dynamic>()
                    {
                        outVal, (lastI < lastInput ? input.Substring(lastI) : "")
                    }
                )
            );
        }

        private static DatePatternBit GetNextPatternBitStringIsReturned(string pattern)
        {
            KeyValuePair<int, string> nextMatch = GetNextKeyword(pattern);
            if (nextMatch.Key < 0) return null;

            return GetCommonDatePatternBit
            (
                nextMatch, pattern
            );
        }

        private static DatePatternBit GetCommonDatePatternBit(KeyValuePair<int, string> nextMatch, string pattern)
        {
            return new DatePatternBit
            (
                nextMatch.Value,
                (
                    pattern.Length <= nextMatch.Value.Length ? "" :
                    pattern.Substring(nextMatch.Key + nextMatch.Value.Length)
                ),
                (
                    nextMatch.Key == 0 ? "" :
                    pattern.Substring(0, nextMatch.Key)
                )
            );
        }

        private static KeyValuePair<int, string> GetNextKeyword(string pattern)
        {
            Dictionary<int, string> matches = new Dictionary<int, string>();

            foreach (var item in KeywordsDateTimeParts)
            {
                int i2 = pattern.IndexOf(item.Key);
                if (i2 < 0) continue;
                matches.Add(i2, item.Key);
            }

            return
            (
                matches.Count < 1 ?
                new KeyValuePair<int, string>(-1, "") :
                matches.OrderBy(x => x.Key).First()
            );
        }

        private class DatePatternBit
        {
            public DateTimeInternalParts Part { get; set; }
            public string RemainingPattern { get; set; }
            public string Inbetweeners { get; set; }
            public string RemainingInput { get; set; } //Only used when extracting DateTime from string.
            public string CurrentValue { get; set; } //Only used when extracting DateTime from string.

            //Used when outputting string from DateTime.
            public DatePatternBit(string keyword, string remainingPattern, string inbetweeners, string remainingInput = null, string currentValue = null)
            {
                Part = KeywordsDateTimeParts[keyword];
                RemainingPattern = remainingPattern;
                Inbetweeners = inbetweeners;
                RemainingInput = remainingInput;
                CurrentValue = currentValue;
            }
        }
    }
}
