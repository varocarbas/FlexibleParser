using System.Linq;

namespace FlexibleParser
{
    public partial class DateP
    {
        ///<summary><para>Returns the string variable which is associated with the DateParts input in the CustomDateTimeFormat pattern.</para></summary>
        ///<param name="part">DateParts variable to be used.</param>
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

        ///<summary><para>Returns the string variable which is associated with the TimeParts input in the CustomDateTimeFormat pattern.</para></summary>
        ///<param name="part">TimeParts variable to be used.</param>
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

        ///<summary><para>Returns the string variable which is associated with the DateTimeParts input in the CustomDateTimeFormat pattern.</para></summary>
        ///<param name="part">DateTimeParts variable to be used.</param>
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

        ///<summary><para>Returns the DateTimeParts variable which is associated with the string input in the CustomDateTimeFormat pattern.</para></summary>
        ///<param name="keyword">String variable to be used.</param>
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

        ///<summary><para>Returns the DateParts variable which is associated with the string input in the CustomDateTimeFormat pattern.</para></summary>
        ///<param name="keyword">String variable to be used.</param>
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

        ///<summary><para>Returns the TimeParts variable which is associated with the string input in the CustomDateTimeFormat pattern.</para></summary>
        ///<param name="keyword">String variable to be used.</param>
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

        ///<summary><para>Returns the DateTimeParts variable which is associated with the string input in the CustomDateTimeFormat pattern.</para></summary>
        ///<param name="keyword">String variable to be used.</param>
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
