using System;
using System.Linq;
using System.Collections.Generic;

namespace FlexibleParser
{
    internal partial class TimeZonesInternal
    {
        private static Dictionary<string, string> WindowsSpecialReplace = new Dictionary<string, string>()
        {
             { "(Mexico)", "Mexico" }, { "-", "_" }
        };

        private static Dictionary<string, string> WindowsSpecialIndividualReplace = new Dictionary<string, string>()
        {
             { "E.", "E" }, { "W.", "W" }, { "N.", "N" }, { "Cen.", "Cen" }
        };

        private static Dictionary<string, string> GetLocalDict(bool fromEnum, int i)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>
            (
                i == 0 ? WindowsSpecialReplace : WindowsSpecialIndividualReplace
            );

            return
            (
                fromEnum ? dict.ToDictionary(x => x.Value, x => x.Key) : dict
            );
        }

        public static string CorrectWindowsSpecial(string outString, bool fromEnum)
        {
            string separator = (fromEnum ? " " : "_");
            string[] words = Common.GetWordsInString(outString);

            for (int i = 0; i < 2; i++)
            {
                foreach (var item in GetLocalDict(fromEnum, i))
                {
                    if (i == 0)
                    {
                        outString = outString.Replace(item.Key, item.Value);
                    }
                    else
                    {
                        int i2 = Array.IndexOf(words, item.Key);
                        if (i2 < 0) continue;

                        words[i2] = words[i2].Replace(item.Key, item.Value);
                    }
                }

                if (i == 1) outString = string.Join(separator, words);
            }



            return outString;
        }
    }
}
