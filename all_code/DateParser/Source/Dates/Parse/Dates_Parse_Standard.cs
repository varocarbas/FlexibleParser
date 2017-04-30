using System;
using System.Collections.Generic;

namespace FlexibleParser
{
    internal partial class DatesInternal
    {
        public static DateTime? FromStringStandardFormat(string input, StandardDateTimeFormat standardFormat)
        {
            return
            (
                standardFormat.UseParseExact ?
                GetTryParseExactResult(input, standardFormat) :
                GetTryParseResult(input, standardFormat)
            );
        }

        private static DateTime? GetTryParseResult(string input, StandardDateTimeFormat standardFormat)
        {
            DateTime outDateTime;
            bool isOK =
            (
                standardFormat.FormatProvider == null ? 
                DateTime.TryParse(input, out outDateTime) :
                DateTime.TryParse
                (
                    input, standardFormat.FormatProvider,
                    standardFormat.DateTimeStyle, out outDateTime
                )
            );

            return
            (
                isOK ? (DateTime?)outDateTime : null
            );
        }

        private static DateTime? GetTryParseExactResult(string input, StandardDateTimeFormat standardFormat)
        {
            DateTime outDateTime;

            bool isOK = DateTime.TryParseExact
            (
                input, standardFormat.Patterns, standardFormat.FormatProvider,
                standardFormat.DateTimeStyle, out outDateTime
            );

            return
            (
                isOK ? (DateTime?)outDateTime : null
            );
        }
    }
}
