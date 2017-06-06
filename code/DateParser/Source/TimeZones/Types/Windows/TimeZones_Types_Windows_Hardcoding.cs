using System;
using System.Collections.Generic;

namespace FlexibleParser
{
    ///<summary><para>All the Windows timezones.</para></summary>
    public enum TimeZoneWindowsEnum
    {
        ///<summary><para>None.</para></summary>
        None = 0,
        ///<summary><para>Dateline Standard Time -- UTC -12:00.</para></summary>
        Dateline_Standard_Time,
        ///<summary><para>Utc-11 -- UTC -11:00.</para></summary>
        UTC_Minus_11,
        ///<summary><para>Hawaiian Standard Time -- UTC -10:00.</para></summary>
        Hawaiian_Standard_Time,
        ///<summary><para>Aleutian Standard Time -- UTC -10:00.</para></summary>
        Aleutian_Standard_Time,
        ///<summary><para>Marquesas Standard Time -- UTC -09:30.</para></summary>
        Marquesas_Standard_Time,
        ///<summary><para>Alaskan Standard Time -- UTC -09:00.</para></summary>
        Alaskan_Standard_Time,
        ///<summary><para>Utc-09 -- UTC -09:00.</para></summary>
        UTC_Minus_09,
        ///<summary><para>Pacific Standard Time Mexico -- UTC -08:00.</para></summary>
        Pacific_Standard_Time_Mexico,
        ///<summary><para>Pacific Standard Time -- UTC -08:00.</para></summary>
        Pacific_Standard_Time,
        ///<summary><para>Utc-08 -- UTC -08:00.</para></summary>
        UTC_Minus_08,
        ///<summary><para>Us Mountain Standard Time -- UTC -07:00.</para></summary>
        US_Mountain_Standard_Time,
        ///<summary><para>Mountain Standard Time Mexico -- UTC -07:00.</para></summary>
        Mountain_Standard_Time_Mexico,
        ///<summary><para>Mountain Standard Time -- UTC -07:00.</para></summary>
        Mountain_Standard_Time,
        ///<summary><para>Central America Standard Time -- UTC -06:00.</para></summary>
        Central_America_Standard_Time,
        ///<summary><para>Central Standard Time Mexico -- UTC -06:00.</para></summary>
        Central_Standard_Time_Mexico,
        ///<summary><para>Central Standard Time -- UTC -06:00.</para></summary>
        Central_Standard_Time,
        ///<summary><para>Easter Island Standard Time -- UTC -06:00.</para></summary>
        Easter_Island_Standard_Time,
        ///<summary><para>Canada Central Standard Time -- UTC -06:00.</para></summary>
        Canada_Central_Standard_Time,
        ///<summary><para>Sa Pacific Standard Time -- UTC -05:00.</para></summary>
        SA_Pacific_Standard_Time,
        ///<summary><para>Eastern Standard Time Mexico -- UTC -05:00.</para></summary>
        Eastern_Standard_Time_Mexico,
        ///<summary><para>Haiti Standard Time -- UTC -05:00.</para></summary>
        Haiti_Standard_Time,
        ///<summary><para>Eastern Standard Time -- UTC -05:00.</para></summary>
        Eastern_Standard_Time,
        ///<summary><para>Us Eastern Standard Time -- UTC -05:00.</para></summary>
        US_Eastern_Standard_Time,
        ///<summary><para>Cuba Standard Time -- UTC -05:00.</para></summary>
        Cuba_Standard_Time,
        ///<summary><para>Paraguay Standard Time -- UTC -04:00.</para></summary>
        Paraguay_Standard_Time,
        ///<summary><para>Venezuela Standard Time -- UTC -04:00.</para></summary>
        Venezuela_Standard_Time,
        ///<summary><para>Central Brazilian Standard Time -- UTC -04:00.</para></summary>
        Central_Brazilian_Standard_Time,
        ///<summary><para>Sa Western Standard Time -- UTC -04:00.</para></summary>
        SA_Western_Standard_Time,
        ///<summary><para>Atlantic Standard Time -- UTC -04:00.</para></summary>
        Atlantic_Standard_Time,
        ///<summary><para>Turks and Caicos Standard Time -- UTC -04:00.</para></summary>
        Turks_And_Caicos_Standard_Time,
        ///<summary><para>Pacific Sa Standard Time -- UTC -04:00.</para></summary>
        Pacific_SA_Standard_Time,
        ///<summary><para>Newfoundland Standard Time -- UTC -03:30.</para></summary>
        Newfoundland_Standard_Time,
        ///<summary><para>Tocantins Standard Time -- UTC -03:00.</para></summary>
        Tocantins_Standard_Time,
        ///<summary><para>E. South America Standard Time -- UTC -03:00.</para></summary>
        E_South_America_Standard_Time,
        ///<summary><para>Sa Eastern Standard Time -- UTC -03:00.</para></summary>
        SA_Eastern_Standard_Time,
        ///<summary><para>Argentina Standard Time -- UTC -03:00.</para></summary>
        Argentina_Standard_Time,
        ///<summary><para>Greenland Standard Time -- UTC -03:00.</para></summary>
        Greenland_Standard_Time,
        ///<summary><para>Montevideo Standard Time -- UTC -03:00.</para></summary>
        Montevideo_Standard_Time,
        ///<summary><para>Bahia Standard Time -- UTC -03:00.</para></summary>
        Bahia_Standard_Time,
        ///<summary><para>Saint Pierre Standard Time -- UTC -03:00.</para></summary>
        Saint_Pierre_Standard_Time,
        ///<summary><para>Invalid timezone.</para></summary>
        Mid_Atlantic_Standard_Time,
        ///<summary><para>Utc-02 -- UTC -02:00.</para></summary>
        UTC_Minus_02,
        ///<summary><para>Azores Standard Time -- UTC -02:00.</para></summary>
        Azores_Standard_Time,
        ///<summary><para>Cape Verde Standard Time -- UTC -01:00.</para></summary>
        Cape_Verde_Standard_Time,
        ///<summary><para>Utc -- UTC +00:00.</para></summary>
        UTC,
        ///<summary><para>Morocco Standard Time -- UTC +00:00.</para></summary>
        Morocco_Standard_Time,
        ///<summary><para>Gmt Standard Time -- UTC +00:00.</para></summary>
        GMT_Standard_Time,
        ///<summary><para>Greenwich Standard Time -- UTC +00:00.</para></summary>
        Greenwich_Standard_Time,
        ///<summary><para>W. Europe Standard Time -- UTC +01:00.</para></summary>
        W_Europe_Standard_Time,
        ///<summary><para>Central Europe Standard Time -- UTC +01:00.</para></summary>
        Central_Europe_Standard_Time,
        ///<summary><para>Romance Standard Time -- UTC +01:00.</para></summary>
        Romance_Standard_Time,
        ///<summary><para>Central European Standard Time -- UTC +01:00.</para></summary>
        Central_European_Standard_Time,
        ///<summary><para>Namibia Standard Time -- UTC +01:00.</para></summary>
        Namibia_Standard_Time,
        ///<summary><para>W. Central Africa Standard Time -- UTC +01:00.</para></summary>
        W_Central_Africa_Standard_Time,
        ///<summary><para>Jordan Standard Time -- UTC +02:00.</para></summary>
        Jordan_Standard_Time,
        ///<summary><para>Gtb Standard Time -- UTC +02:00.</para></summary>
        GTB_Standard_Time,
        ///<summary><para>Middle East Standard Time -- UTC +02:00.</para></summary>
        Middle_East_Standard_Time,
        ///<summary><para>E. Europe Standard Time -- UTC +02:00.</para></summary>
        E_Europe_Standard_Time,
        ///<summary><para>Syria Standard Time -- UTC +02:00.</para></summary>
        Syria_Standard_Time,
        ///<summary><para>Egypt Standard Time -- UTC +02:00.</para></summary>
        Egypt_Standard_Time,
        ///<summary><para>West Bank Standard Time -- UTC +02:00.</para></summary>
        West_Bank_Standard_Time,
        ///<summary><para>South Africa Standard Time -- UTC +02:00.</para></summary>
        South_Africa_Standard_Time,
        ///<summary><para>Fle Standard Time -- UTC +02:00.</para></summary>
        FLE_Standard_Time,
        ///<summary><para>Israel Standard Time -- UTC +02:00.</para></summary>
        Israel_Standard_Time,
        ///<summary><para>Kaliningrad Standard Time -- UTC +02:00.</para></summary>
        Kaliningrad_Standard_Time,
        ///<summary><para>Libya Standard Time -- UTC +02:00.</para></summary>
        Libya_Standard_Time,
        ///<summary><para>Arabic Standard Time -- UTC +03:00.</para></summary>
        Arabic_Standard_Time,
        ///<summary><para>Turkey Standard Time -- UTC +03:00.</para></summary>
        Turkey_Standard_Time,
        ///<summary><para>Arab Standard Time -- UTC +03:00.</para></summary>
        Arab_Standard_Time,
        ///<summary><para>Belarus Standard Time -- UTC +03:00.</para></summary>
        Belarus_Standard_Time,
        ///<summary><para>Russian Standard Time -- UTC +03:00.</para></summary>
        Russian_Standard_Time,
        ///<summary><para>E. Africa Standard Time -- UTC +03:00.</para></summary>
        E_Africa_Standard_Time,
        ///<summary><para>Iran Standard Time -- UTC +03:30.</para></summary>
        Iran_Standard_Time,
        ///<summary><para>Arabian Standard Time -- UTC +04:00.</para></summary>
        Arabian_Standard_Time,
        ///<summary><para>Astrakhan Standard Time -- UTC +04:00.</para></summary>
        Astrakhan_Standard_Time,
        ///<summary><para>Azerbaijan Standard Time -- UTC +04:00.</para></summary>
        Azerbaijan_Standard_Time,
        ///<summary><para>Caucasus Standard Time -- UTC +04:00.</para></summary>
        Caucasus_Standard_Time,
        ///<summary><para>Russia Time Zone 3 -- UTC +04:00.</para></summary>
        Russia_Time_Zone_3,
        ///<summary><para>Mauritius Standard Time -- UTC +04:00.</para></summary>
        Mauritius_Standard_Time,
        ///<summary><para>Georgian Standard Time -- UTC +04:00.</para></summary>
        Georgian_Standard_Time,
        ///<summary><para>Afghanistan Standard Time -- UTC +04:30.</para></summary>
        Afghanistan_Standard_Time,
        ///<summary><para>West Asia Standard Time -- UTC +05:00.</para></summary>
        West_Asia_Standard_Time,
        ///<summary><para>Ekaterinburg Standard Time -- UTC +05:00.</para></summary>
        Ekaterinburg_Standard_Time,
        ///<summary><para>Pakistan Standard Time -- UTC +05:00.</para></summary>
        Pakistan_Standard_Time,
        ///<summary><para>India Standard Time -- UTC +05:30.</para></summary>
        India_Standard_Time,
        ///<summary><para>Sri Lanka Standard Time -- UTC +05:30.</para></summary>
        Sri_Lanka_Standard_Time,
        ///<summary><para>Nepal Standard Time -- UTC +05:45.</para></summary>
        Nepal_Standard_Time,
        ///<summary><para>Central Asia Standard Time -- UTC +06:00.</para></summary>
        Central_Asia_Standard_Time,
        ///<summary><para>Bangladesh Standard Time -- UTC +06:00.</para></summary>
        Bangladesh_Standard_Time,
        ///<summary><para>Omsk Standard Time -- UTC +06:00.</para></summary>
        Omsk_Standard_Time,
        ///<summary><para>Myanmar Standard Time -- UTC +06:30.</para></summary>
        Myanmar_Standard_Time,
        ///<summary><para>Se Asia Standard Time -- UTC +07:00.</para></summary>
        SE_Asia_Standard_Time,
        ///<summary><para>Altai Standard Time -- UTC +07:00.</para></summary>
        Altai_Standard_Time,
        ///<summary><para>W. Mongolia Standard Time -- UTC +07:00.</para></summary>
        W_Mongolia_Standard_Time,
        ///<summary><para>North Asia Standard Time -- UTC +07:00.</para></summary>
        North_Asia_Standard_Time,
        ///<summary><para>N. Central Asia Standard Time -- UTC +07:00.</para></summary>
        N_Central_Asia_Standard_Time,
        ///<summary><para>Tomsk Standard Time -- UTC +07:00.</para></summary>
        Tomsk_Standard_Time,
        ///<summary><para>North Asia East Standard Time -- UTC +08:00.</para></summary>
        North_Asia_East_Standard_Time,
        ///<summary><para>Singapore Standard Time -- UTC +08:00.</para></summary>
        Singapore_Standard_Time,
        ///<summary><para>China Standard Time -- UTC +08:00.</para></summary>
        China_Standard_Time,
        ///<summary><para>W. Australia Standard Time -- UTC +08:00.</para></summary>
        W_Australia_Standard_Time,
        ///<summary><para>Taipei Standard Time -- UTC +08:00.</para></summary>
        Taipei_Standard_Time,
        ///<summary><para>Ulaanbaatar Standard Time -- UTC +08:00.</para></summary>
        Ulaanbaatar_Standard_Time,
        ///<summary><para>North Korea Standard Time -- UTC +08:30.</para></summary>
        North_Korea_Standard_Time,
        ///<summary><para>Aus Central W. Standard Time -- UTC +08:45.</para></summary>
        Aus_Central_W_Standard_Time,
        ///<summary><para>Transbaikal Standard Time -- UTC +09:00.</para></summary>
        Transbaikal_Standard_Time,
        ///<summary><para>Tokyo Standard Time -- UTC +09:00.</para></summary>
        Tokyo_Standard_Time,
        ///<summary><para>Korea Standard Time -- UTC +09:00.</para></summary>
        Korea_Standard_Time,
        ///<summary><para>Yakutsk Standard Time -- UTC +09:00.</para></summary>
        Yakutsk_Standard_Time,
        ///<summary><para>Cen. Australia Standard Time -- UTC +09:30.</para></summary>
        Cen_Australia_Standard_Time,
        ///<summary><para>Aus Central Standard Time -- UTC +09:30.</para></summary>
        Aus_Central_Standard_Time,
        ///<summary><para>E. Australia Standard Time -- UTC +10:00.</para></summary>
        E_Australia_Standard_Time,
        ///<summary><para>Aus Eastern Standard Time -- UTC +10:00.</para></summary>
        Aus_Eastern_Standard_Time,
        ///<summary><para>West Pacific Standard Time -- UTC +10:00.</para></summary>
        West_Pacific_Standard_Time,
        ///<summary><para>Tasmania Standard Time -- UTC +10:00.</para></summary>
        Tasmania_Standard_Time,
        ///<summary><para>Vladivostok Standard Time -- UTC +10:00.</para></summary>
        Vladivostok_Standard_Time,
        ///<summary><para>Lord Howe Standard Time -- UTC +10:30.</para></summary>
        Lord_Howe_Standard_Time,
        ///<summary><para>Russia Time Zone 10 -- UTC +11:00.</para></summary>
        Russia_Time_Zone_10,
        ///<summary><para>Bougainville Standard Time -- UTC +11:00.</para></summary>
        Bougainville_Standard_Time,
        ///<summary><para>Norfolk Standard Time -- UTC +11:00.</para></summary>
        Norfolk_Standard_Time,
        ///<summary><para>Central Pacific Standard Time -- UTC +11:00.</para></summary>
        Central_Pacific_Standard_Time,
        ///<summary><para>Magadan Standard Time -- UTC +11:00.</para></summary>
        Magadan_Standard_Time,
        ///<summary><para>Sakhalin Standard Time -- UTC +11:00.</para></summary>
        Sakhalin_Standard_Time,
        ///<summary><para>Russia Time Zone 11 -- UTC +12:00.</para></summary>
        Russia_Time_Zone_11,
        ///<summary><para>New Zealand Standard Time -- UTC +12:00.</para></summary>
        New_Zealand_Standard_Time,
        ///<summary><para>Fiji Standard Time -- UTC +12:00.</para></summary>
        Fiji_Standard_Time,
        ///<summary><para>Utc+12 -- UTC +12:00.</para></summary>
        UTC_Plus_12,
        ///<summary><para>Invalid timezone.</para></summary>
        Kamchatka_Standard_Time,
        ///<summary><para>Chatham Islands Standard Time -- UTC +12:45.</para></summary>
        Chatham_Islands_Standard_Time,
        ///<summary><para>Tonga Standard Time -- UTC +13:00.</para></summary>
        Tonga_Standard_Time,
        ///<summary><para>Samoa Standard Time -- UTC +13:00.</para></summary>
        Samoa_Standard_Time,
        ///<summary><para>Line Islands Standard Time -- UTC +14:00.</para></summary>
        Line_Islands_Standard_Time
    }

    internal partial class TimeZoneWindowsInternal
    {
        internal static Dictionary<TimeZoneWindowsEnum, string> TimeZoneWindowsNames;

        internal static void PopulateMain()
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
