using System;
using System.Collections.Generic;

namespace FlexibleParser
{
    ///<summary><para>All the conventional timezones.</para></summary>
    public enum TimeZoneConventionalEnum
    {
        ///<summary><para>None.</para></summary>
        None = 0,
        ///<summary><para>International Date Line West -- UTC -12:00.</para></summary>
        International_Date_Line_West,
        ///<summary><para>Coordinated Universal Time-11 -- UTC -11:00.</para></summary>
        Coordinated_Universal_Time_minus_11,
        ///<summary><para>Aleutian Islands -- UTC -10:00.</para></summary>
        Aleutian_Islands,
        ///<summary><para>Hawaii -- UTC -10:00.</para></summary>
        Hawaii,
        ///<summary><para>Marquesas Islands -- UTC -09:30.</para></summary>
        Marquesas_Islands,
        ///<summary><para>Alaska (AK) -- UTC -09:00.</para></summary>
        Alaska,
        ///<summary><para>Coordinated Universal Time-09 -- UTC -09:00.</para></summary>
        Coordinated_Universal_Time_minus_09,
        ///<summary><para>Baja California -- UTC -08:00.</para></summary>
        Baja_California,
        ///<summary><para>Coordinated Universal Time-08 -- UTC -08:00.</para></summary>
        Coordinated_Universal_Time_minus_08,
        ///<summary><para>Pacific Time Us and Canada -- UTC -08:00.</para></summary>
        Pacific_Time_US_and_Canada,
        ///<summary><para>Arizona -- UTC -07:00.</para></summary>
        Arizona,
        ///<summary><para>Chihuahua, La Paz, Mazatlan -- UTC -07:00.</para></summary>
        Chihuahua_La_Paz_Mazatlan,
        ///<summary><para>Mountain Time Us and Canada -- UTC -07:00.</para></summary>
        Mountain_Time_US_and_Canada,
        ///<summary><para>Central America -- UTC -06:00.</para></summary>
        Central_America,
        ///<summary><para>Central Time Us and Canada -- UTC -06:00.</para></summary>
        Central_Time_US_and_Canada,
        ///<summary><para>Easter Island -- UTC -06:00.</para></summary>
        Easter_Island,
        ///<summary><para>Guadalajara, Mexico City, Monterrey -- UTC -06:00.</para></summary>
        Guadalajara_Mexico_City_Monterrey,
        ///<summary><para>Saskatchewan -- UTC -06:00.</para></summary>
        Saskatchewan,
        ///<summary><para>Bogota, Lima, Quito, Rio Branco -- UTC -05:00.</para></summary>
        Bogota_Lima_Quito_Rio_Branco,
        ///<summary><para>Chetumal -- UTC -05:00.</para></summary>
        Chetumal,
        ///<summary><para>Eastern Time Us and Canada -- UTC -05:00.</para></summary>
        Eastern_Time_US_and_Canada,
        ///<summary><para>Haiti -- UTC -05:00.</para></summary>
        Haiti,
        ///<summary><para>Havana -- UTC -05:00.</para></summary>
        Havana,
        ///<summary><para>Indiana East -- UTC -05:00.</para></summary>
        Indiana_East,
        ///<summary><para>Asuncion -- UTC -04:00.</para></summary>
        Asuncion,
        ///<summary><para>Atlantic Time (Canada) -- UTC -04:00.</para></summary>
        Atlantic_Time_Canada,
        ///<summary><para>Caracas -- UTC -04:00.</para></summary>
        Caracas,
        ///<summary><para>Cuiaba -- UTC -04:00.</para></summary>
        Cuiaba,
        ///<summary><para>Georgetown, La Paz, Manaus, San Juan -- UTC -04:00.</para></summary>
        Georgetown_La_Paz_Manaus_San_Juan,
        ///<summary><para>Santiago -- UTC -04:00.</para></summary>
        Santiago,
        ///<summary><para>Turks and Caicos -- UTC -04:00.</para></summary>
        Turks_and_Caicos,
        ///<summary><para>Newfoundland -- UTC -03:30.</para></summary>
        Newfoundland,
        ///<summary><para>Araguaina -- UTC -03:00.</para></summary>
        Araguaina,
        ///<summary><para>Brasilia -- UTC -03:00.</para></summary>
        Brasilia,
        ///<summary><para>Cayenne, Fortaleza -- UTC -03:00.</para></summary>
        Cayenne_Fortaleza,
        ///<summary><para>City of Buenos Aires -- UTC -03:00.</para></summary>
        City_of_Buenos_Aires,
        ///<summary><para>Greenland -- UTC -03:00.</para></summary>
        Greenland,
        ///<summary><para>Montevideo -- UTC -03:00.</para></summary>
        Montevideo,
        ///<summary><para>Saint Pierre and Miquelon -- UTC -03:00.</para></summary>
        Saint_Pierre_and_Miquelon,
        ///<summary><para>Salvador -- UTC -03:00.</para></summary>
        Salvador,
        ///<summary><para>Coordinated Universal Time-02 -- UTC -02:00.</para></summary>
        Coordinated_Universal_Time_minus_02,
        ///<summary><para>Azores -- UTC -02:00.</para></summary>
        Azores,
        ///<summary><para>Cabo Verde Is. -- UTC -01:00.</para></summary>
        Cabo_Verde_Is,
        ///<summary><para>Coordinated Universal Time -- UTC +00:00.</para></summary>
        Coordinated_Universal_Time,
        ///<summary><para>Casablanca -- UTC +00:00.</para></summary>
        Casablanca,
        ///<summary><para>Dublin, Edinburgh, Lisbon, London -- UTC +00:00.</para></summary>
        Dublin_Edinburgh_Lisbon_London,
        ///<summary><para>Monrovia, Reykjavik -- UTC +00:00.</para></summary>
        Monrovia_Reykjavik,
        ///<summary><para>Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna -- UTC +01:00.</para></summary>
        Amsterdam_Berlin_Bern_Rome_Stockholm_Vienna,
        ///<summary><para>Belgrade, Bratislava, Budapest, Ljubljana, Prague -- UTC +01:00.</para></summary>
        Belgrade_Bratislava_Budapest_Ljubljana_Prague,
        ///<summary><para>Brussels, Copenhagen, Madrid, Paris -- UTC +01:00.</para></summary>
        Brussels_Copenhagen_Madrid_Paris,
        ///<summary><para>Sarajevo, Skopje, Warsaw, Zagreb -- UTC +01:00.</para></summary>
        Sarajevo_Skopje_Warsaw_Zagreb,
        ///<summary><para>West Central Africa -- UTC +01:00.</para></summary>
        West_Central_Africa,
        ///<summary><para>Windhoek -- UTC +01:00.</para></summary>
        Windhoek,
        ///<summary><para>Amman -- UTC +02:00.</para></summary>
        Amman,
        ///<summary><para>Athens, Bucharest -- UTC +02:00.</para></summary>
        Athens_Bucharest,
        ///<summary><para>Beirut -- UTC +02:00.</para></summary>
        Beirut,
        ///<summary><para>Cairo -- UTC +02:00.</para></summary>
        Cairo,
        ///<summary><para>Chisinau -- UTC +02:00.</para></summary>
        Chisinau,
        ///<summary><para>Damascus -- UTC +02:00.</para></summary>
        Damascus,
        ///<summary><para>Gaza, Hebron -- UTC +02:00.</para></summary>
        Gaza_Hebron,
        ///<summary><para>Harare, Pretoria -- UTC +02:00.</para></summary>
        Harare_Pretoria,
        ///<summary><para>Helsinki, Kyiv, Riga, Sofia, Tallinn, Vilnius -- UTC +02:00.</para></summary>
        Helsinki_Kyiv_Riga_Sofia_Tallinn_Vilnius,
        ///<summary><para>Jerusalem -- UTC +02:00.</para></summary>
        Jerusalem,
        ///<summary><para>Kaliningrad -- UTC +02:00.</para></summary>
        Kaliningrad,
        ///<summary><para>Tripoli -- UTC +02:00.</para></summary>
        Tripoli,
        ///<summary><para>Baghdad -- UTC +03:00.</para></summary>
        Baghdad,
        ///<summary><para>Istanbul -- UTC +03:00.</para></summary>
        Istanbul,
        ///<summary><para>Kuwait, Riyadh -- UTC +03:00.</para></summary>
        Kuwait_Riyadh,
        ///<summary><para>Minsk -- UTC +03:00.</para></summary>
        Minsk,
        ///<summary><para>Moscow, St. Petersburg, Volgogradh -- UTC +03:00.</para></summary>
        Moscow_St_Petersburg_Volgograd,
        ///<summary><para>Nairobi -- UTC +03:00.</para></summary>
        Nairobi,
        ///<summary><para>Tehran -- UTC +03:30.</para></summary>
        Tehran,
        ///<summary><para>Abu Dhabi, Muscat -- UTC +04:00.</para></summary>
        Abu_Dhabi_Muscat,
        ///<summary><para>Astrakhan, Ulyanovsk -- UTC +04:00.</para></summary>
        Astrakhan_Ulyanovsk,
        ///<summary><para>Baku -- UTC +04:00.</para></summary>
        Baku,
        ///<summary><para>Izhevsk, Samara -- UTC +04:00.</para></summary>
        Izhevsk_Samara,
        ///<summary><para>Port Louis -- UTC +04:00.</para></summary>
        Port_Louis,
        ///<summary><para>Tbilisi -- UTC +04:00.</para></summary>
        Tbilisi,
        ///<summary><para>Yerevan -- UTC +04:00.</para></summary>
        Yerevan,
        ///<summary><para>Kabul -- UTC +04:30.</para></summary>
        Kabul,
        ///<summary><para>Ashgabat, Tashkent -- UTC +05:00.</para></summary>
        Ashgabat_Tashkent,
        ///<summary><para>Ekaterinburg -- UTC +05:00.</para></summary>
        Ekaterinburg,
        ///<summary><para>Islamabad, Karachi -- UTC +05:00.</para></summary>
        Islamabad_Karachi,
        ///<summary><para>Chennai, Kolkata, Mumbai, New Delhi -- UTC +05:30.</para></summary>
        Chennai_Kolkata_Mumbai_New_Delhi,
        ///<summary><para>Sri Jayawardenepura -- UTC +05:30.</para></summary>
        Sri_Jayawardenepura,
        ///<summary><para>Kathmandu -- UTC +05:45.</para></summary>
        Kathmandu,
        ///<summary><para>Astana -- UTC +06:00.</para></summary>
        Astana,
        ///<summary><para>Dhaka -- UTC +06:00.</para></summary>
        Dhaka,
        ///<summary><para>Omsk -- UTC +06:00.</para></summary>
        Omsk,
        ///<summary><para>Yangon (Rangoon) -- UTC +06:30.</para></summary>
        Yangon_Rangoon,
        ///<summary><para>Bangkok, Hanoi, Jakarta -- UTC +07:00.</para></summary>
        Bangkok_Hanoi_Jakarta,
        ///<summary><para>Barnaul, Gorno-Altaysk -- UTC +07:00.</para></summary>
        Barnaul_Gorno_minus_Altaysk,
        ///<summary><para>Hovd -- UTC +07:00.</para></summary>
        Hovd,
        ///<summary><para>Krasnoyarsk -- UTC +07:00.</para></summary>
        Krasnoyarsk,
        ///<summary><para>Novosibirsk -- UTC +07:00.</para></summary>
        Novosibirsk,
        ///<summary><para>Tomsk -- UTC +07:00.</para></summary>
        Tomsk,
        ///<summary><para>Beijing, Chongqing, Hong Kong, Urumqi -- UTC +08:00.</para></summary>
        Beijing_Chongqing_Hong_Kong_Urumqi,
        ///<summary><para>Irkutsk -- UTC +08:00.</para></summary>
        Irkutsk,
        ///<summary><para>Kuala Lumpur, Singapore -- UTC +08:00.</para></summary>
        Kuala_Lumpur_Singapore,
        ///<summary><para>Perth -- UTC +08:00.</para></summary>
        Perth,
        ///<summary><para>Taipei -- UTC +08:00.</para></summary>
        Taipei,
        ///<summary><para>Ulaanbaatar -- UTC +08:00.</para></summary>
        Ulaanbaatar,
        ///<summary><para>Pyongyang -- UTC +08:30.</para></summary>
        Pyongyang,
        ///<summary><para>Eucla -- UTC +08:45.</para></summary>
        Eucla,
        ///<summary><para>Chita -- UTC +09:00.</para></summary>
        Chita,
        ///<summary><para>Osaka, Sapporo, Tokyo -- UTC +09:00.</para></summary>
        Osaka_Sapporo_Tokyo,
        ///<summary><para>Seoul -- UTC +09:00.</para></summary>
        Seoul,
        ///<summary><para>Yakutsk -- UTC +09:00.</para></summary>
        Yakutsk,
        ///<summary><para>Adelaide -- UTC +09:30.</para></summary>
        Adelaide,
        ///<summary><para>Darwin -- UTC +09:30.</para></summary>
        Darwin,
        ///<summary><para>Brisbane -- UTC +10:00.</para></summary>
        Brisbane,
        ///<summary><para>Canberra, Melbourne, Sydney -- UTC +10:00.</para></summary>
        Canberra_Melbourne_Sydney,
        ///<summary><para>Guam, Port Moresby -- UTC +10:00.</para></summary>
        Guam_Port_Moresby,
        ///<summary><para>Hobart -- UTC +10:00.</para></summary>
        Hobart,
        ///<summary><para>Vladivostok -- UTC +10:00.</para></summary>
        Vladivostok,
        ///<summary><para>Lord Howe Island -- UTC +10:30.</para></summary>
        Lord_Howe_Island,
        ///<summary><para>Bougainville Island -- UTC +11:00.</para></summary>
        Bougainville_Island,
        ///<summary><para>Chokurdakh -- UTC +11:00.</para></summary>
        Chokurdakh,
        ///<summary><para>Magadan -- UTC +11:00.</para></summary>
        Magadan,
        ///<summary><para>Norfolk Island -- UTC +11:00.</para></summary>
        Norfolk_Island,
        ///<summary><para>Sakhalin -- UTC +11:00.</para></summary>
        Sakhalin,
        ///<summary><para>Solomon Is., New Caledonia -- UTC +11:00.</para></summary>
        Solomon_Is_New_Caledonia,
        ///<summary><para>Anadyr, Petropavlovsk-Kamchatsky -- UTC +12:00.</para></summary>
        Anadyr_Petropavlovsk_minus_Kamchatsky,
        ///<summary><para>Auckland, Wellington -- UTC +12:00.</para></summary>
        Auckland_Wellington,
        ///<summary><para>Coordinated Universal Time+12 -- UTC +12:00.</para></summary>
        Coordinated_Universal_Time_plus_12,
        ///<summary><para>Fiji -- UTC +12:00.</para></summary>
        Fiji,
        ///<summary><para>Chatham Islands -- UTC +12:45.</para></summary>
        Chatham_Islands,
        ///<summary><para>Nuku'alofa -- UTC +13:00.</para></summary>
        Nuku_alofa,
        ///<summary><para>Samoa -- UTC +13:00.</para></summary>
        Samoa,
        ///<summary><para>Kiritimati Island -- UTC +14:00.</para></summary>
        Kiritimati_Island,
        ///<summary><para>International Business Standard Time (IBST) -- UTC +00:00.</para></summary>
        International_Business_Standard_Time,
        ///<summary><para>Western European Daylight Time (WEDT) -- UTC +01:00.</para></summary>
        Western_European_Daylight_Time,
        ///<summary><para>Central European Daylight Time (CEDT) -- UTC +02:00.</para></summary>
        Central_European_Daylight_Time,
        ///<summary><para>Eastern European Daylight Time (EEDT) -- UTC +03:00.</para></summary>
        Eastern_European_Daylight_Time,
        ///<summary><para>Australian Western Daylight Time (AWDT) -- UTC +09:00.</para></summary>
        Australian_Western_Daylight_Time,
        ///<summary><para>Chamorro Standard Time (ChST) -- UTC +10:00.</para></summary>
        Chamorro_Standard_Time,
        ///<summary><para>Hawaii Standard Time (HST) -- UTC -10:00.</para></summary>
        Hawaii_Standard_Time
    }

    internal partial class TimeZoneConventionalInternal
    {
        internal static Dictionary<TimeZoneConventionalEnum, string> TimeZoneConventionalAbbreviations;

        internal static IEnumerable<TimeZoneConventionalEnum> GetConventionalTimezonesFromString(string input)
        {
            TimeZoneConventionalEnum timezone = GetConventionalTimezoneFromString(input);
            if (timezone != TimeZoneConventionalEnum.None) yield return timezone;

            foreach (TimeZoneConventionalEnum related in GetConventionalTimezoneRelated(timezone))
            {
                if (related == TimeZoneConventionalEnum.None) continue;

                yield return related;
            }
        }

        //In principle, the conventional timezones refer to the heading comments in http://unicode.org/repos/cldr/trunk/common/supplemental/windowsZones.xml,
        //but also other designations not included in any other category (e.g., International Business Standard Time). This method accounts for the not too
        //common eventuality where various non-official references share the same conventional timezone (usually via same UTC).
        private static TimeZoneConventionalEnum[] GetConventionalTimezoneRelated(TimeZoneConventionalEnum timezone)
        {
            if (timezone == TimeZoneConventionalEnum.Coordinated_Universal_Time)
            {
                return new TimeZoneConventionalEnum[] 
                { 
                    TimeZoneConventionalEnum.International_Business_Standard_Time
                };
            }
            else if (timezone == TimeZoneConventionalEnum.Amsterdam_Berlin_Bern_Rome_Stockholm_Vienna)
            {
                return new TimeZoneConventionalEnum[] 
                { 
                    TimeZoneConventionalEnum.Western_European_Daylight_Time
                };
            }
            else if (timezone == TimeZoneConventionalEnum.Helsinki_Kyiv_Riga_Sofia_Tallinn_Vilnius)
            {
                return new TimeZoneConventionalEnum[] 
                { 
                    TimeZoneConventionalEnum.Central_European_Daylight_Time
                };
            }
            else if (timezone == TimeZoneConventionalEnum.Moscow_St_Petersburg_Volgograd)
            {
                return new TimeZoneConventionalEnum[] 
                { 
                    TimeZoneConventionalEnum.Eastern_European_Daylight_Time
                };
            }
            else if (timezone == TimeZoneConventionalEnum.Osaka_Sapporo_Tokyo)
            {
                return new TimeZoneConventionalEnum[] 
                { 
                    TimeZoneConventionalEnum.Australian_Western_Daylight_Time
                };
            }
            else if (timezone == TimeZoneConventionalEnum.Guam_Port_Moresby)
            {
                return new TimeZoneConventionalEnum[] 
                { 
                    TimeZoneConventionalEnum.Chamorro_Standard_Time
                };
            }
            else if (timezone == TimeZoneConventionalEnum.Hawaii)
            {
                return new TimeZoneConventionalEnum[] 
                { 
                    TimeZoneConventionalEnum.Hawaii_Standard_Time
                };
            }

            return new TimeZoneConventionalEnum[] 
            { 
                TimeZoneConventionalEnum.None 
            };
        }

        private static TimeZoneConventionalEnum GetConventionalTimezoneFromString(string input)
        {
            string output = input.Replace(" ", "_");
            output = output.Replace("-", "_minus_");
            output = output.Replace("+", "_plus_");
            output = output.Replace("&", "and");
            output = output.Replace("(", "");
            output = output.Replace(")", "");
            output = output.Replace(",", "");
            output = output.Replace(".", "");
            output = output.Replace("'", "_");

            return (TimeZoneConventionalEnum)Enum.Parse
            (
                typeof(TimeZoneConventionalEnum), output.Replace(" ", "")
            );
        }

        internal static void PopulateTimeZoneConventional()
        {
            PopulateTimeZoneConventionalAbbreviations();
        }

        private static void PopulateTimeZoneConventionalAbbreviations()
        {
            TimeZoneConventionalAbbreviations = new Dictionary<TimeZoneConventionalEnum, string>()
            {
                { TimeZoneConventionalEnum.Alaska, "AK" }, 
                { TimeZoneConventionalEnum.International_Business_Standard_Time, "IBST" },
                { TimeZoneConventionalEnum.Western_European_Daylight_Time, "WEDT" },
                { TimeZoneConventionalEnum.Central_European_Daylight_Time, "CEDT" },
                { TimeZoneConventionalEnum.Eastern_European_Daylight_Time, "EEDT" },
                { TimeZoneConventionalEnum.Australian_Western_Daylight_Time, "AWDT" },
                { TimeZoneConventionalEnum.Chamorro_Standard_Time, "ChST" },
                { TimeZoneConventionalEnum.Hawaii_Standard_Time, "HST" }
            };
        }
    }
}
