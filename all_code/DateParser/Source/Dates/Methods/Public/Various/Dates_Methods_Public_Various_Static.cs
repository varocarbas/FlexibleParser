using System;
using System.Linq;

namespace FlexibleParser
{
    public partial class DateP
    {
        public static string GetCustomKeywordFromDatePart(DateParts part)
        {
            return
            (
                !DatesInternal.InputIsOK(part) ? "" : GetCustomKeywordFromDateTimePart
                (
                    DatesInternal.DateToDateTime[part]
                )    
            );
        }

        public static string GetCustomKeywordFromTimePart(TimeParts part)
        {
            return
            (
                !DatesInternal.InputIsOK(part) ? "" : GetCustomKeywordFromDateTimePart
                (
                    DatesInternal.TimeToDateTime[part]
                )
            );
        }

        public static string GetCustomKeywordFromDateTimePart(DateTimeParts part)
        {
            return
            (
                !DatesInternal.InputIsOK(part) ? "" : DatesInternal.KeywordsDateTimeParts.First
                (
                    x => x.Value == DatesInternal.DateTimeToInternal[part]
                )
                .Key
            );
        }

        public static DateTimeParts GetDateTimePartFromCustomKeyword(string keyword)
        {
            return
            (
                !DatesInternal.InputIsOK(keyword) ? DateTimeParts.None :  
                GetDateTimePartFromCustomKeywordInternal
                (
                    keyword.Trim().ToLower()
                )
            );  
        }


        public static DateParts GetDatePartFromCustomKeyword(string keyword)
        {
            if (!DatesInternal.InputIsOK(keyword))
            {
                return DateParts.None;
            }

            return DatesInternal.DateToDateTime.First
            (
                x => x.Value == GetDateTimePartFromCustomKeywordInternal
                (
                    keyword.Trim().ToLower()
                )
            )
            .Key;
        }

        public static TimeParts GetTimePartFromCustomKeyword(string keyword)
        {
            if (!DatesInternal.InputIsOK(keyword))
            {
                return TimeParts.None;
            }

            return DatesInternal.TimeToDateTime.First
            (
                x => x.Value == GetDateTimePartFromCustomKeywordInternal
                (
                    keyword.Trim().ToLower()
                )
            )
            .Key;
        }

        private static DateTimeParts GetDateTimePartFromCustomKeywordInternal(string keyword)
        {
            if (!DatesInternal.InputIsOK(keyword))
            {
                return DateTimeParts.None;
            }


            return
            (
                DatesInternal.KeywordsDateTimeParts.ContainsKey(keyword) ?
                DatesInternal.DateTimeToInternal.First
                (
                    x => x.Value == DatesInternal.KeywordsDateTimeParts[keyword]
                )
                .Key : DateTimeParts.None
            );
        }
    }
}
