using System.Globalization;

namespace FlexibleParser
{
    public abstract partial class DateTimeFormat
    {
        internal static string GetPatternFromParts(DateTimeParts[] patternParts)
        {
            string outPattern = "";
            if (patternParts == null || patternParts.Length == 0)
            {
                return outPattern;
            }

            return GetPatternFromPartsInternal(patternParts);
        }

        private static string GetPatternFromPartsInternal(dynamic patternParts)
        {
            string outPattern = "";
            dynamic lastPart = null;

            foreach (dynamic part in patternParts)
            {
                if (!DatesInternal.IsValidDateTimePart(part)) continue;

                if (lastPart != null)
                {
                    DateOrTime dateOrTime = DatesInternal.IsDateOrTime(lastPart);

                    outPattern += 
                    (
                        dateOrTime == DateOrTime.Date ?
                        CultureInfo.CurrentCulture.DateTimeFormat.DateSeparator :
                        CultureInfo.CurrentCulture.DateTimeFormat.TimeSeparator
                    );
                }

                outPattern += part.ToString();
                lastPart = part;
            }

            return outPattern;
        }
    }
}
