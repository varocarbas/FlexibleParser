using System;
using System.Collections.Generic;

namespace FlexibleParser
{
    public enum TimeZoneConventionalEnum
    {
        None = 0,
        International_Date_Line_West,
        Coordinated_Universal_Time_minus_11,
        Aleutian_Islands,
        Hawaii,
        Marquesas_Islands,
        Alaska,
        Coordinated_Universal_Time_minus_09,
        Baja_California,
        Coordinated_Universal_Time_minus_08,
        Pacific_Time_US_and_Canada,
        Arizona,
        Chihuahua_La_Paz_Mazatlan,
        Mountain_Time_US_and_Canada,
        Central_America,
        Central_Time_US_and_Canada,
        Easter_Island,
        Guadalajara_Mexico_City_Monterrey,
        Saskatchewan,
        Bogota_Lima_Quito_Rio_Branco,
        Chetumal,
        Eastern_Time_US_and_Canada,
        Haiti,
        Havana,
        Indiana_East,
        Asuncion,
        Atlantic_Time_Canada,
        Caracas,
        Cuiaba,
        Georgetown_La_Paz_Manaus_San_Juan,
        Santiago,
        Turks_and_Caicos,
        Newfoundland,
        Araguaina,
        Brasilia,
        Cayenne_Fortaleza,
        City_of_Buenos_Aires,
        Greenland,
        Montevideo,
        Saint_Pierre_and_Miquelon,
        Salvador,
        Coordinated_Universal_Time_minus_02,
        Azores,
        Cabo_Verde_Is,
        Coordinated_Universal_Time,
        Casablanca,
        Dublin_Edinburgh_Lisbon_London,
        Monrovia_Reykjavik,
        Amsterdam_Berlin_Bern_Rome_Stockholm_Vienna,
        Belgrade_Bratislava_Budapest_Ljubljana_Prague,
        Brussels_Copenhagen_Madrid_Paris,
        Sarajevo_Skopje_Warsaw_Zagreb,
        West_Central_Africa,
        Windhoek,
        Amman,
        Athens_Bucharest,
        Beirut,
        Cairo,
        Chisinau,
        Damascus,
        Gaza_Hebron,
        Harare_Pretoria,
        Helsinki_Kyiv_Riga_Sofia_Tallinn_Vilnius,
        Jerusalem,
        Kaliningrad,
        Tripoli,
        Baghdad,
        Istanbul,
        Kuwait_Riyadh,
        Minsk,
        Moscow_St_Petersburg_Volgograd,
        Nairobi,
        Tehran,
        Abu_Dhabi_Muscat,
        Astrakhan_Ulyanovsk,
        Baku,
        Izhevsk_Samara,
        Port_Louis,
        Tbilisi,
        Yerevan,
        Kabul,
        Ashgabat_Tashkent,
        Ekaterinburg,
        Islamabad_Karachi,
        Chennai_Kolkata_Mumbai_New_Delhi,
        Sri_Jayawardenepura,
        Kathmandu,
        Astana,
        Dhaka,
        Omsk,
        Yangon_Rangoon,
        Bangkok_Hanoi_Jakarta,
        Barnaul_Gorno_minus_Altaysk,
        Hovd,
        Krasnoyarsk,
        Novosibirsk,
        Tomsk,
        Beijing_Chongqing_Hong_Kong_Urumqi,
        Irkutsk,
        Kuala_Lumpur_Singapore,
        Perth,
        Taipei,
        Ulaanbaatar,
        Pyongyang,
        Eucla,
        Chita,
        Osaka_Sapporo_Tokyo,
        Seoul,
        Yakutsk,
        Adelaide,
        Darwin,
        Brisbane,
        Canberra_Melbourne_Sydney,
        Guam_Port_Moresby,
        Hobart,
        Vladivostok,
        Lord_Howe_Island,
        Bougainville_Island,
        Chokurdakh,
        Magadan,
        Norfolk_Island,
        Sakhalin,
        Solomon_Is_New_Caledonia,
        Anadyr_Petropavlovsk_minus_Kamchatsky,
        Auckland_Wellington,
        Coordinated_Universal_Time_plus_12,
        Fiji,
        Chatham_Islands,
        Nuku_alofa,
        Samoa,
        Kiritimati_Island,
        International_Business_Standard_Time,
        Western_European_Daylight_Time,
        Central_European_Daylight_Time,
        Eastern_European_Daylight_Time,
        Australian_Western_Daylight_Time,
        Chamorro_Standard_Time,
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
