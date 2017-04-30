using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    public enum TimeZoneWindowsEnum
    {
        None = 0,
        Dateline_Standard_Time, UTC_Minus_11,
        Hawaiian_Standard_Time, Aleutian_Standard_Time,
        Marquesas_Standard_Time, Alaskan_Standard_Time,
        UTC_Minus_09, Pacific_Standard_Time_Mexico,
        Pacific_Standard_Time, UTC_Minus_08,
        US_Mountain_Standard_Time, Mountain_Standard_Time_Mexico,
        Mountain_Standard_Time, Central_America_Standard_Time,
        Central_Standard_Time_Mexico, Central_Standard_Time,
        Easter_Island_Standard_Time, Canada_Central_Standard_Time,
        SA_Pacific_Standard_Time, Eastern_Standard_Time_Mexico,
        Haiti_Standard_Time, Eastern_Standard_Time,
        US_Eastern_Standard_Time, Cuba_Standard_Time,
        Paraguay_Standard_Time, Venezuela_Standard_Time,
        Central_Brazilian_Standard_Time, SA_Western_Standard_Time,
        Atlantic_Standard_Time, Turks_And_Caicos_Standard_Time,
        Pacific_SA_Standard_Time, Newfoundland_Standard_Time,
        Tocantins_Standard_Time, E_South_America_Standard_Time,
        SA_Eastern_Standard_Time, Argentina_Standard_Time,
        Greenland_Standard_Time, Montevideo_Standard_Time,
        Bahia_Standard_Time, Saint_Pierre_Standard_Time,
        Mid_Atlantic_Standard_Time, UTC_Minus_02,
        Azores_Standard_Time, Cape_Verde_Standard_Time,
        UTC, Morocco_Standard_Time,
        GMT_Standard_Time, Greenwich_Standard_Time,
        W_Europe_Standard_Time, Central_Europe_Standard_Time,
        Romance_Standard_Time, Central_European_Standard_Time,
        Namibia_Standard_Time, W_Central_Africa_Standard_Time,
        Jordan_Standard_Time, GTB_Standard_Time,
        Middle_East_Standard_Time, E_Europe_Standard_Time,
        Syria_Standard_Time, Egypt_Standard_Time,
        West_Bank_Standard_Time, South_Africa_Standard_Time,
        FLE_Standard_Time, Israel_Standard_Time,
        Kaliningrad_Standard_Time, Libya_Standard_Time,
        Arabic_Standard_Time, Turkey_Standard_Time,
        Arab_Standard_Time, Belarus_Standard_Time,
        Russian_Standard_Time, E_Africa_Standard_Time,
        Iran_Standard_Time, Arabian_Standard_Time,
        Astrakhan_Standard_Time, Azerbaijan_Standard_Time,
        Caucasus_Standard_Time, Russia_Time_Zone_3,
        Mauritius_Standard_Time, Georgian_Standard_Time,
        Afghanistan_Standard_Time, West_Asia_Standard_Time,
        Ekaterinburg_Standard_Time, Pakistan_Standard_Time,
        India_Standard_Time, Sri_Lanka_Standard_Time,
        Nepal_Standard_Time, Central_Asia_Standard_Time,
        Bangladesh_Standard_Time, Omsk_Standard_Time,
        Myanmar_Standard_Time, SE_Asia_Standard_Time,
        Altai_Standard_Time, W_Mongolia_Standard_Time,
        North_Asia_Standard_Time, N_Central_Asia_Standard_Time,
        Tomsk_Standard_Time, North_Asia_East_Standard_Time,
        Singapore_Standard_Time, China_Standard_Time,
        W_Australia_Standard_Time, Taipei_Standard_Time,
        Ulaanbaatar_Standard_Time, North_Korea_Standard_Time,
        Aus_Central_W_Standard_Time, Transbaikal_Standard_Time,
        Tokyo_Standard_Time, Korea_Standard_Time,
        Yakutsk_Standard_Time, Cen_Australia_Standard_Time,
        Aus_Central_Standard_Time, E_Australia_Standard_Time,
        Aus_Eastern_Standard_Time, West_Pacific_Standard_Time,
        Tasmania_Standard_Time, Vladivostok_Standard_Time,
        Lord_Howe_Standard_Time, Russia_Time_Zone_10,
        Bougainville_Standard_Time, Norfolk_Standard_Time,
        Central_Pacific_Standard_Time, Magadan_Standard_Time,
        Sakhalin_Standard_Time, Russia_Time_Zone_11,
        New_Zealand_Standard_Time, Fiji_Standard_Time,
        UTC_Plus_12, Kamchatka_Standard_Time,
        Chatham_Islands_Standard_Time, Tonga_Standard_Time,
        Samoa_Standard_Time, Line_Islands_Standard_Time
    }

    public partial class TimeZoneWindows
    {
        internal static Dictionary<TimeZoneWindowsEnum, string> TimeZoneWindowsNames;

        internal static void PopulateTimeZoneWindows()
        {
            Dictionary<string, string> replacements = new Dictionary<string, string>()
            {
                { "_Minus_", " -" }, { "_Plus_", " +" }, { "W_", "W." }, 
                { "E_", "E." }, { "_Mexico", " (Mexico)" }, { "_", " " }
            };

            string[] inputs = Enum.GetNames(typeof(TimeZoneWindowsEnum));
            TimeZoneWindowsNames = new Dictionary<TimeZoneWindowsEnum, string>();

            foreach(string input in inputs)
            {
                string name = input;
                foreach (var item in replacements)
                {
                    name = name.Replace(item.Key, item.Value);
                }

                TimeZoneWindowsNames.Add
                (
                    (TimeZoneWindowsEnum)Enum.Parse
                    (
                        typeof(TimeZoneWindowsEnum), input
                    ), 
                    name
                );
            }
        }
    }
}
