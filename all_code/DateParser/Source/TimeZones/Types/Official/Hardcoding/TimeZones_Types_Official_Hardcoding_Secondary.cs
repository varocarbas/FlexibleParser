using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    internal partial class TimeZoneOfficialInternal
    {
        internal static IEnumerable<TimeZoneOfficialEnum> GetOfficialTimezonesFromUTC(TimeZoneUTCEnum UTCTimeZone)
        {
            if (UTCTimeZone == TimeZoneUTCEnum.Minus_11)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Niue_Time, TimeZoneOfficialEnum.Samoa_Standard_Time,
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Minus_10)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Cook_Island_Time, TimeZoneOfficialEnum.Hawaii_Aleutian_Standard_Time,
                    TimeZoneOfficialEnum.Tahiti_Time
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Minus_9_30)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Marquesas_Islands_Time
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Minus_9)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Alaska_Standard_Time, TimeZoneOfficialEnum.Gambier_Islands,
                    TimeZoneOfficialEnum.Hawaii_Aleutian_Daylight_Time
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Minus_8)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Alaska_Daylight_Time, TimeZoneOfficialEnum.Cayman_Islands_Standard_Time,
                    TimeZoneOfficialEnum.Pacific_Standard_Time, TimeZoneOfficialEnum.Brunei_Darussalam_Time
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Minus_7)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Mountain_Standard_Time, TimeZoneOfficialEnum.Pacific_Daylight_Time
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Minus_6)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Central_Standard_Time, TimeZoneOfficialEnum.Easter_Island_Standard_Time,
                    TimeZoneOfficialEnum.Galapagos_Time, TimeZoneOfficialEnum.Mountain_Daylight_Time
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Minus_5)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Central_Daylight_Time, TimeZoneOfficialEnum.Colombia_Time,
                    TimeZoneOfficialEnum.Cuba_Standard_Time, TimeZoneOfficialEnum.Ecuador_Time,
                    TimeZoneOfficialEnum.Eastern_Standard_Time, TimeZoneOfficialEnum.Acre_Time,
                    TimeZoneOfficialEnum.Easter_Island_Summer_Time, TimeZoneOfficialEnum.Peru_Time
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Minus_4)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Amazon_Time_Brazil, TimeZoneOfficialEnum.Atlantic_Standard_Time,
                    TimeZoneOfficialEnum.Bolivia_Time, TimeZoneOfficialEnum.Cuba_Daylight_Time,
                    TimeZoneOfficialEnum.Chile_Standard_Time, TimeZoneOfficialEnum.Eastern_Daylight_Time,
                    TimeZoneOfficialEnum.Falkland_Islands_Time, TimeZoneOfficialEnum.Guyana_Time,
                    TimeZoneOfficialEnum.Paraguay_Time, TimeZoneOfficialEnum.Venezuelan_Standard_Time
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Minus_3_30)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Newfoundland_Standard_Time, 

                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Minus_3)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Atlantic_Daylight_Time, TimeZoneOfficialEnum.Amazon_Summer_Time_Brazil,
                    TimeZoneOfficialEnum.Argentina_Time, TimeZoneOfficialEnum.Brasilia_Time,
                    TimeZoneOfficialEnum.Chile_Summer_Time, TimeZoneOfficialEnum.Falkland_Islands_Summer_Time,
                    TimeZoneOfficialEnum.French_Guiana_Time, TimeZoneOfficialEnum.Saint_Pierre_And_Miquelon_Standard_Time,
                    TimeZoneOfficialEnum.Paraguay_Summer_Time, TimeZoneOfficialEnum.Rothera_Time,
                    TimeZoneOfficialEnum.Suriname_Time, TimeZoneOfficialEnum.Uruguay_Time
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Minus_2_30)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Newfoundland_Daylight_Time
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Minus_2)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Brasilia_Summer_Time, TimeZoneOfficialEnum.Fernando_de_Noronha_Time,
                    TimeZoneOfficialEnum.Saint_Pierre_And_Miquelon_Daylight_Time, TimeZoneOfficialEnum.Uruguay_Summer_Time
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Minus_1)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Azores_Standard_Time, TimeZoneOfficialEnum.Cape_Verde_Time,
                    TimeZoneOfficialEnum.Eastern_Greenland_Time
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.UTC)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Azores_Summer_Time, TimeZoneOfficialEnum.Eastern_Greenland_Summer_Time,
                    TimeZoneOfficialEnum.Greenwich_Mean_Time, TimeZoneOfficialEnum.Coordinated_Universal_Time,
                    TimeZoneOfficialEnum.Western_European_Time
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Plus_1)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.British_Summer_Time, TimeZoneOfficialEnum.Central_European_Time,
                    TimeZoneOfficialEnum.Irish_Standard_Time, TimeZoneOfficialEnum.West_Africa_Time, 
                    TimeZoneOfficialEnum.Western_European_Summer_Time
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Plus_2)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Central_Africa_Time, TimeZoneOfficialEnum.Central_European_Summer_Time,
                    TimeZoneOfficialEnum.Eastern_European_Time, TimeZoneOfficialEnum.Israel_Standard_Time, 
                    TimeZoneOfficialEnum.South_African_Standard_Time, TimeZoneOfficialEnum.West_Africa_Summer_Time
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Plus_3)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Arabia_Standard_Time, TimeZoneOfficialEnum.East_Africa_Time,
                    TimeZoneOfficialEnum.Eastern_European_Summer_Time, TimeZoneOfficialEnum.Further_Eastern_European_Time,
                    TimeZoneOfficialEnum.Israel_Daylight_Time,                    TimeZoneOfficialEnum.Moscow_Standard_Time, TimeZoneOfficialEnum.Showa_Station_Time,
                    TimeZoneOfficialEnum.Turkey_Time
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Plus_3_30)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Iran_Standard_Time
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Plus_4)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Armenia_Time, TimeZoneOfficialEnum.Azerbaijan_Time,
                    TimeZoneOfficialEnum.Georgia_Standard_Time, TimeZoneOfficialEnum.Gulf_Standard_Time,
                    TimeZoneOfficialEnum.Mauritius_Time, TimeZoneOfficialEnum.Réunion_Time,
                    TimeZoneOfficialEnum.Samara_Time, TimeZoneOfficialEnum.Seychelles_Time
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Plus_4_30)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Afghanistan_Time, TimeZoneOfficialEnum.Iran_Daylight_Time
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Plus_5)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Mawson_Time, TimeZoneOfficialEnum.Maldives_Time, 
                    TimeZoneOfficialEnum.Pakistan_Standard_Time, TimeZoneOfficialEnum.Indian_Kerguelen, 
                    TimeZoneOfficialEnum.Tajikistan_Time, TimeZoneOfficialEnum.Turkmenistan_Time, 
                    TimeZoneOfficialEnum.Uzbekistan_Time, TimeZoneOfficialEnum.Yekaterinburg_Time,
                    TimeZoneOfficialEnum.Oral_Time
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Plus_5_30)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Indian_Standard_Time, TimeZoneOfficialEnum.Sri_Lanka_Standard_Time
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Plus_5_45)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Nepal_Time
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Plus_6)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Indian_Chagos_Time, TimeZoneOfficialEnum.Bangladesh_Standard_Time,
                    TimeZoneOfficialEnum.Bhutan_Time, TimeZoneOfficialEnum.Kyrgyzstan_Time,
                    TimeZoneOfficialEnum.Omsk_Standard_Time, TimeZoneOfficialEnum.Vostok_Station_Time
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Plus_6_30)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Cocos_Islands_Time, TimeZoneOfficialEnum.Myanmar_Standard_Time
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Plus_7)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Christmas_Island_Time, TimeZoneOfficialEnum.Davis_Time,
                    TimeZoneOfficialEnum.Indochina_Time,
                    TimeZoneOfficialEnum.Krasnoyarsk_Time, TimeZoneOfficialEnum.Thailand_Standard_Time,
                    TimeZoneOfficialEnum.Western_Indonesian_Time
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Plus_8)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Australian_Western_Standard_Time,
                    TimeZoneOfficialEnum.Choibalsan_Standard_Time, TimeZoneOfficialEnum.Central_Indonesia_Time,
                    TimeZoneOfficialEnum.China_Standard_Time, 
                    TimeZoneOfficialEnum.Hong_Kong_Time,
                    TimeZoneOfficialEnum.Irkutsk_Time, TimeZoneOfficialEnum.Malaysia_Time, 
                    TimeZoneOfficialEnum.Philippine_Time, TimeZoneOfficialEnum.Singapore_Time, 
                    TimeZoneOfficialEnum.Ulaanbaatar_Standard_Time
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Plus_8_45)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Australian_Central_Western_Standard_Time
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Plus_9)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Choibalsan_Summer_Time, TimeZoneOfficialEnum.Eastern_Indonesian_Time, 
                    TimeZoneOfficialEnum.Japan_Standard_Time, TimeZoneOfficialEnum.Korea_Standard_Time, 
                    TimeZoneOfficialEnum.East_Timor_Time, TimeZoneOfficialEnum.Ulaanbaatar_Summer_Time, 
                    TimeZoneOfficialEnum.Yakutsk_Time
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Plus_9_30)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Australian_Central_Standard_Time
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Plus_10)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Australian_Eastern_Standard_Time, TimeZoneOfficialEnum.Chamorro_Standard_Time,
                    TimeZoneOfficialEnum.Chuuk_Time, TimeZoneOfficialEnum.Dumont_DUrville_Time,
                    TimeZoneOfficialEnum.Australian_Eastern_Standard_Time, TimeZoneOfficialEnum.Papua_New_Guinea_Time,
                    TimeZoneOfficialEnum.Vladivostok_Time
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Plus_10_30)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Australian_Central_Daylight_Savings_Time,
                    TimeZoneOfficialEnum.Lord_Howe_Standard_Time
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Plus_11)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Bougainville_Standard_Time,
                    TimeZoneOfficialEnum.Australian_Eastern_Daylight_Time, TimeZoneOfficialEnum.Kosrae_Time,
                    TimeZoneOfficialEnum.Lord_Howe_Summer_Time,
                    TimeZoneOfficialEnum.New_Caledonia_Time, TimeZoneOfficialEnum.Norfolk_Time,
                    TimeZoneOfficialEnum.Pohnpei_Standard_Time, TimeZoneOfficialEnum.Sakhalin_Island_Time,
                    TimeZoneOfficialEnum.Solomon_Islands_Time, TimeZoneOfficialEnum.Srednekolymsk_Time,
                    TimeZoneOfficialEnum.Vanuatu_Time
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Plus_12)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Fiji_Time, TimeZoneOfficialEnum.Gilbert_Island_Time,
                    TimeZoneOfficialEnum.Magadan_Time, TimeZoneOfficialEnum.Marshall_Islands,
                    TimeZoneOfficialEnum.New_Zealand_Standard_Time, TimeZoneOfficialEnum.Kamchatka_Time,
                    TimeZoneOfficialEnum.Tuvalu_Time, TimeZoneOfficialEnum.Wake_Time
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Plus_12_45)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Chatham_Standard_Time
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Plus_13)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.New_Zealand_Daylight_Time, TimeZoneOfficialEnum.Phoenix_Island_Time,
                    TimeZoneOfficialEnum.Tokelau_Time, TimeZoneOfficialEnum.Tonga_Time
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Plus_13_45)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Chatham_Daylight_Time
                };
            }
            else if (UTCTimeZone == TimeZoneUTCEnum.Plus_14)
            {
                return new TimeZoneOfficialEnum[] 
                { 
                    TimeZoneOfficialEnum.Line_Islands_Time
                };
            }

            return new TimeZoneOfficialEnum[0];
        }
    }
}
