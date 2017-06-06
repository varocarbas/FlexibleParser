using System.Collections.Generic;

namespace FlexibleParser
{
    internal partial class TimeZonesInternal
    {
        private static Dictionary<string, string> OfficialSpecial = new Dictionary<string, string>()
        {
             { "Further", "Further-Eastern European Time" }, { "Dumont", "Dumont-d'Urville" }
        };

        public static string CorrectOfficialSpecial(string outString, bool fromEnum)
        {
            foreach (var item in OfficialSpecial)
            {
                KeyValuePair<string, string> item2 = new KeyValuePair<string, string>
                (
                    (fromEnum ? item.Key : item.Value),
                    (fromEnum ? item.Value : item.Key)
                );
                if (outString.Contains(item2.Key))
                {
                    return item2.Value;
                }
            }

            return outString;
        }
    }
}
