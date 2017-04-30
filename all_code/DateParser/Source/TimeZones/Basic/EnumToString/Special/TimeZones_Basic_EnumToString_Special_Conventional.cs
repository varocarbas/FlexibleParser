using System;
using System.Linq;
using System.Collections.Generic;

namespace FlexibleParser
{
    internal partial class TimeZonesInternal
    {
        private static Dictionary<string, string> ConventionalSpecial = new Dictionary<string, string>()
        {
            { "Chihuahua", "Chihuahua, La Paz, Mazatlan" },
            { "Guadalajara", "Guadalajara, Mexico City, Monterrey" }, { "Bogota", "Bogota, Lima, Quito, Rio Branco" },
            { "Indiana East", "Indiana East" }, { "Atlantic Time Canada", "Atlantic Time (Canada)" },
            { "Georgetown", "Georgetown, La Paz, Manaus, San Juan" }, { "Cayenne", "Cayenne, Fortaleza" },
            { "Dublin", "Dublin, Edinburgh, Lisbon, London" }, { "Monrovia", "Monrovia, Reykjavik" },
            { "Amsterdam", "Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna" }, 
            { "Belgrade", "Belgrade, Bratislava, Budapest, Ljubljana, Prague" }, 
            { "Brussels", "Brussels, Copenhagen, Madrid, Paris" }, { "Sarajevo", "Sarajevo, Skopje, Warsaw, Zagreb" }, 
            { "Athens", "Athens, Bucharest" }, { "Gaza", "Gaza, Hebron" }, { "Harare", "Harare, Pretoria" },
            { "Helsinki", "Helsinki, Kyiv, Riga, Sofia, Tallinn, Vilnius" }, { "Kuwait", "Kuwait, Riyadh" },
            { "Moscow", "Moscow, St. Petersburg, Volgogradh" }, { "Abu Dhabi", "Abu Dhabi, Muscat" },
            { "Astrakhan", "Astrakhan, Ulyanovsk" }, { "Izhevsk", "Izhevsk, Samara" },
            { "Ashgabat", "Ashgabat, Tashkent" }, { "Islamabad", "Islamabad, Karachi" },
            { "Chennai", "Chennai, Kolkata, Mumbai, New Delhi" }, { "Yangon", "Yangon (Rangoon)" },
            { "Bangkok", "Bangkok, Hanoi, Jakarta" }, { "Barnaul", "Barnaul, Gorno-Altaysk" },
            { "Beijing", "Beijing, Chongqing, Hong Kong, Urumqi" }, { "Kuala Lumpur", "Kuala Lumpur, Singapore" },
            { "Osaka", "Osaka, Sapporo, Tokyo" }, { "Canberra", "Canberra, Melbourne, Sydney" },
            { "Guam", "Guam, Port Moresby" }, { "Solomon Is", "Solomon Is., New Caledonia" },
            { "Anadyr", "Anadyr, Petropavlovsk-Kamchatsky" }, { "Auckland", "Auckland, Wellington" },
            { "Nuku", "Nuku'alofa" },
        };

        internal static string CorrectConventionalSpecial(string outString, bool fromEnum)
        {
            if (outString.Contains("Coordinated Universal Time"))
            {
                if (outString.Contains("minus"))
                {
                    return outString.Replace(" minus ", "-");
                }
                else if (outString.Contains("plus"))
                {
                    return outString.Replace(" plus ", "+");
                }
            }

            if (outString.Contains("Time US and Canada"))
            {
                return outString.Replace("Time US and Canada", "(US &amp; Canada)");
            }

            if (outString == "Cabo Verde Is") return "Cabo Verde Is.";

            foreach (var item in ConventionalSpecial)
            {
                if (outString.Contains(item.Key))
                {
                    return item.Value;
                }
            }

            return outString;
        }
    }
}
