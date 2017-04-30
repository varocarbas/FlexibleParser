using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    public enum TimeZoneOfficialEnum
    {
        None = 0,
        Australian_Central_Daylight_Savings_Time, //	UTC+10:30 ACDT
        Australian_Central_Standard_Time, //	UTC+09:30 ACST
        Australian_Central_Western_Standard_Time, //	UTC+08:45 ACWST
        Acre_Time, //	UTC-05 ACT Acre
        Atlantic_Daylight_Time, //	UTC-03 ADT
        Australian_Eastern_Standard_Time, //	UTC+10 AEST
        Afghanistan_Time, //	UTC+04:30 AFT
        Alaska_Daylight_Time, //	UTC-08 AKDT
        Alaska_Standard_Time, //	UTC-09 AKST
        Amazon_Summer_Time_Brazil, //	UTC-03 AMST
        Amazon_Time_Brazil, //	UTC-04 AMT
        Armenia_Time, //	UTC+04 AMT
        Argentina_Time, //	UTC-03 ART
        Arabia_Standard_Time, //	UTC+03 AST
        Atlantic_Standard_Time, //	UTC-04 AST
        Australian_Western_Standard_Time, //	UTC+08 AWST
        Azores_Summer_Time, //	UTC±00 AZOST
        Azores_Standard_Time, //	UTC-01 AZOT
        Azerbaijan_Time, //	UTC+04 AZT
        Brunei_Darussalam_Time, //	UTC+08 BNT
        Indian_Chagos_Time, //	UTC+06 IOT
        Bolivia_Time, //	UTC-04 BOT
        Brasilia_Summer_Time, //	UTC-02 BRST
        Brasilia_Time, //	UTC-03 BRT
        Bangladesh_Standard_Time, //	UTC+06 BST
        Bougainville_Standard_Time, //	UTC+11 BST
        British_Summer_Time, //	UTC+01 BST
        Bhutan_Time, //	UTC+06 BTT
        Central_Africa_Time, //	UTC+02 CAT
        Cocos_Islands_Time, //	UTC+06:30 CCT
        Central_Daylight_Time, //	UTC-05 CDT
        Cuba_Daylight_Time, //	UTC-04 CDT
        Central_European_Summer_Time, //	UTC+02 CEST
        Central_European_Time, //	UTC+01 CET
        Chatham_Daylight_Time, //	UTC+13:45 CHADT
        Chatham_Standard_Time, //	UTC+12:45 CHAST
        Choibalsan_Standard_Time, //	UTC+08 CHOT
        Choibalsan_Summer_Time, //	UTC+09 CHOST
        Chamorro_Standard_Time, //	UTC+10 CHST
        Chuuk_Time, //	UTC+10 CHUT
        Cayman_Islands_Standard_Time, //	UTC-08 CIST
        Central_Indonesia_Time, //	UTC+08 WITA
        Cook_Island_Time, //	UTC-10 CKT
        Chile_Summer_Time, //	UTC-03 CLST
        Chile_Standard_Time, //	UTC-04 CLT
        Colombia_Time, //	UTC-05 COT
        Central_Standard_Time, //	UTC-06 CST
        China_Standard_Time, //	UTC+08 CST
        Cuba_Standard_Time, //	UTC-05 CST
        Cape_Verde_Time, //	UTC-01 CVT
        Christmas_Island_Time, //	UTC+07 CXT
        Davis_Time, //	UTC+07 DAVT
        Dumont_DUrville_Time, //	UTC+10 DDUT
        Easter_Island_Summer_Time, //	UTC-05 EASST
        Easter_Island_Standard_Time, //	UTC-06 EAST
        East_Africa_Time, //	UTC+03 EAT
        Ecuador_Time, //	UTC-05 ECT
        Eastern_Daylight_Time, //	UTC-04 EDT
        Australian_Eastern_Daylight_Time, //	UTC+11 AEDT
        Eastern_European_Summer_Time, //	UTC+03 EEST
        Eastern_European_Time, //	UTC+02 EET
        Eastern_Greenland_Summer_Time, //	UTC±00 EGST
        Eastern_Greenland_Time, //	UTC-01 EGT
        Eastern_Indonesian_Time, //	UTC+09 WIT
        Western_Indonesian_Time, //	UTC+07 WIB
        Eastern_Standard_Time, //	UTC-05 EST
        Further_Eastern_European_Time, //	UTC+03 FET
        Fiji_Time, //	UTC+12 FJT
        Falkland_Islands_Summer_Time, //	UTC-03 FKST
        Falkland_Islands_Time, //	UTC-04 FKT
        Fernando_de_Noronha_Time, //	UTC-02 FNT
        Galapagos_Time, //	UTC-06 GALT
        Gambier_Islands, //	UTC-09 GAMT
        Georgia_Standard_Time, //	UTC+04 GET
        French_Guiana_Time, //	UTC-03 GFT
        Gilbert_Island_Time, //	UTC+12 GILT
        Greenwich_Mean_Time, //	UTC±00 GMT
        Gulf_Standard_Time, //	UTC+04 GST
        Guyana_Time, //	UTC-04 GYT
        Hawaii_Aleutian_Daylight_Time, //	UTC-09 HADT
        Hawaii_Aleutian_Standard_Time, //	UTC-10 HAST
        Hong_Kong_Time, //	UTC+08 HKT
        Indochina_Time, //	UTC+07 ICT
        Israel_Daylight_Time, //	UTC+03 IDT
        Iran_Daylight_Time, //	UTC+04:30 IRDT
        Irkutsk_Time, //	UTC+08 IRKT
        Iran_Standard_Time, //	UTC+03:30 IRST
        Indian_Standard_Time, //	UTC+05:30 IST
        Irish_Standard_Time, //	UTC+01 IST
        Israel_Standard_Time, //	UTC+02 IST
        Japan_Standard_Time, //	UTC+09 JST
        Kyrgyzstan_Time, //	UTC+06 KGT
        Kosrae_Time, //	UTC+11 KOST
        Krasnoyarsk_Time, //	UTC+07 KRAT
        Korea_Standard_Time, //	UTC+09 KST
        Lord_Howe_Standard_Time, //	UTC+10:30 LHST
        Lord_Howe_Summer_Time, //	UTC+11 LHST
        Line_Islands_Time, //	UTC+14 LINT
        Magadan_Time, //	UTC+12 MAGT
        Marquesas_Islands_Time, //	UTC-09:30 MART
        Mawson_Time, //	UTC+05 MAWT
        Mountain_Daylight_Time, //	UTC-06 MDT
        Marshall_Islands, //	UTC+12 MHT
        Myanmar_Standard_Time, //	UTC+06:30 MMT
        Moscow_Standard_Time, //	UTC+03 MSK
        Malaysia_Time, //	UTC+08 MYT
        Mountain_Standard_Time, //	UTC-07 MST
        Mauritius_Time, //	UTC+04 MUT
        Maldives_Time, //	UTC+05 MVT
        New_Caledonia_Time, //	UTC+11 NCT
        Newfoundland_Daylight_Time, //	UTC-02:30 NDT
        Norfolk_Time, //	UTC+11 NFT
        Nepal_Time, //	UTC+05:45 NPT
        Newfoundland_Standard_Time, //	UTC-03:30 NST
        Niue_Time, //	UTC-11 NUT
        New_Zealand_Daylight_Time, //	UTC+13 NZDT
        New_Zealand_Standard_Time, //	UTC+12 NZST
        Omsk_Standard_Time, //	UTC+06 OMST
        Oral_Time, //	UTC+05 ORAT
        Pacific_Daylight_Time, //	UTC-07 PDT
        Peru_Time, //	UTC-05 PET
        Kamchatka_Time, //	UTC+12 PETT
        Papua_New_Guinea_Time, //	UTC+10 PGT
        Phoenix_Island_Time, //	UTC+13 PHOT
        Philippine_Time, //	UTC+08 PHT
        Pakistan_Standard_Time, //	UTC+05 PKT
        Saint_Pierre_And_Miquelon_Daylight_Time, //	UTC-02 PMDT
        Saint_Pierre_And_Miquelon_Standard_Time, //	UTC-03 PMST
        Pohnpei_Standard_Time, //	UTC+11 PONT
        Pacific_Standard_Time, //	UTC-08 PST
        Paraguay_Summer_Time, //	UTC-03 PYST
        Paraguay_Time, //	UTC-04 PYT
        Réunion_Time, //	UTC+04 RET
        Rothera_Time, //	UTC-03 ROTT
        Sakhalin_Island_Time, //	UTC+11 SAKT
        Samara_Time, //	UTC+04 SAMT
        South_African_Standard_Time, //	UTC+02 SAST
        Solomon_Islands_Time, //	UTC+11 SBT
        Seychelles_Time, //	UTC+04 SCT
        Singapore_Time, //	UTC+08 SGT
        Sri_Lanka_Standard_Time, //	UTC+05:30 SLST
        Srednekolymsk_Time, //	UTC+11 SRET
        Suriname_Time, //	UTC-03 SRT
        Samoa_Standard_Time, //	UTC-11 SST
        Showa_Station_Time, //	UTC+03 SYOT
        Tahiti_Time, //	UTC-10 TAHT
        Thailand_Standard_Time, //	UTC+07 THA
        Indian_Kerguelen, //	UTC+05 TFT
        Tajikistan_Time, //	UTC+05 TJT
        Tokelau_Time, //	UTC+13 TKT
        East_Timor_Time, //	UTC+09 TLT
        Turkmenistan_Time, //	UTC+05 TMT
        Turkey_Time, //	UTC+03 TRT
        Tonga_Time, //	UTC+13 TOT
        Tuvalu_Time, //	UTC+12 TVT
        Ulaanbaatar_Summer_Time, //	UTC+09 ULAST
        Ulaanbaatar_Standard_Time, //	UTC+08 ULAT
        Coordinated_Universal_Time, //	UTC±00 UTC
        Uruguay_Summer_Time, //	UTC-02 UYST
        Uruguay_Time, //	UTC-03 UYT
        Uzbekistan_Time, //	UTC+05 UZT
        Venezuelan_Standard_Time, //	UTC-04 VET
        Vladivostok_Time, //	UTC+10 VLAT
        Vostok_Station_Time, //	UTC+06 VOST
        Vanuatu_Time, //	UTC+11 VUT
        Wake_Time, //	UTC+12 WAKT
        West_Africa_Summer_Time, //	UTC+02 WAST
        West_Africa_Time, //	UTC+01 WAT
        Western_European_Summer_Time, //	UTC+01 WEST
        Western_European_Time, //	UTC±00 WET
        Yakutsk_Time, //	UTC+09 YAKT
        Yekaterinburg_Time //	UTC+05 YEKT
    }

    public partial class TimeZoneOfficial
    {
        //Storing all the enum/abbreviation information in a global dictionary populated once would certainly be a
        //much more efficient approach. On the other hand, this library consumes already a relevant amount of memory
        //(what might be even further increased via extending some hardcoded resources) and that's why better minimising
        //the impact on this aspect when possible like in this case.
        internal static TimeZoneOfficialEnum GetOfficialFromAbbreviation(string abbreviation)
        {
            string abbreviation2 = abbreviation.Trim().ToLower();
            Dictionary<TimeZoneOfficialEnum, string> officialAbbreviations = PopulateTimeZoneOfficialAbbreviations();

            var temp = officialAbbreviations.FirstOrDefault(x => x.Value.ToLower() == abbreviation2);

            return
            (
                temp.Value == null ? TimeZoneOfficialEnum.None : temp.Key
            );
        }

        internal static string GetAbbreviationFromOfficial(TimeZoneOfficialEnum official)
        {
            Dictionary<TimeZoneOfficialEnum, string> officialAbbreviations = PopulateTimeZoneOfficialAbbreviations();

            return
            (
                officialAbbreviations.ContainsKey(official) ? 
                officialAbbreviations[official] : null
            );
        }

        private static Dictionary<TimeZoneOfficialEnum, string> PopulateTimeZoneOfficialAbbreviations()
        {
            return new Dictionary<TimeZoneOfficialEnum, string>()
            {
                { TimeZoneOfficialEnum.Australian_Central_Daylight_Savings_Time, "ACDT" },
                { TimeZoneOfficialEnum.Australian_Central_Standard_Time, "ACST" },
                { TimeZoneOfficialEnum.Acre_Time, "ACT" },
                { TimeZoneOfficialEnum.Atlantic_Daylight_Time, "ADT" },
                { TimeZoneOfficialEnum.Australian_Eastern_Standard_Time, "AEST" },
                { TimeZoneOfficialEnum.Afghanistan_Time, "AFT" },
                { TimeZoneOfficialEnum.Alaska_Daylight_Time, "AKDT" },
                { TimeZoneOfficialEnum.Alaska_Standard_Time, "AKST" },
                { TimeZoneOfficialEnum.Amazon_Summer_Time_Brazil, "AMST" },
                { TimeZoneOfficialEnum.Amazon_Time_Brazil, "AMT" },
                { TimeZoneOfficialEnum.Armenia_Time, "AMT" },
                { TimeZoneOfficialEnum.Argentina_Time, "ART" },
                { TimeZoneOfficialEnum.Arabia_Standard_Time, "AST" },
                { TimeZoneOfficialEnum.Atlantic_Standard_Time, "AST" },
                { TimeZoneOfficialEnum.Australian_Western_Standard_Time, "AWST" },
                { TimeZoneOfficialEnum.Azores_Summer_Time, "AZOST" },
                { TimeZoneOfficialEnum.Azores_Standard_Time, "AZOT" },
                { TimeZoneOfficialEnum.Azerbaijan_Time, "AZT" },
                { TimeZoneOfficialEnum.Brunei_Darussalam_Time, "BNT" },
                { TimeZoneOfficialEnum.Indian_Chagos_Time, "IOT" },
                { TimeZoneOfficialEnum.Bolivia_Time, "BOT" },
                { TimeZoneOfficialEnum.Brasilia_Summer_Time, "BRST" },
                { TimeZoneOfficialEnum.Brasilia_Time, "BRT" },
                { TimeZoneOfficialEnum.Bangladesh_Standard_Time, "BST" },
                { TimeZoneOfficialEnum.Bougainville_Standard_Time, "BST" },
                { TimeZoneOfficialEnum.British_Summer_Time, "BST" },
                { TimeZoneOfficialEnum.Bhutan_Time, "BTT" },
                { TimeZoneOfficialEnum.Central_Africa_Time, "CAT" },
                { TimeZoneOfficialEnum.Cocos_Islands_Time, "CCT" },
                { TimeZoneOfficialEnum.Central_Daylight_Time, "CDT" },
                { TimeZoneOfficialEnum.Cuba_Daylight_Time, "CDT" },
                { TimeZoneOfficialEnum.Central_European_Summer_Time, "CEST" },
                { TimeZoneOfficialEnum.Central_European_Time, "CET" },
                { TimeZoneOfficialEnum.Chatham_Daylight_Time, "CHADT" },
                { TimeZoneOfficialEnum.Chatham_Standard_Time, "CHAST" },
                { TimeZoneOfficialEnum.Choibalsan_Standard_Time, "CHOT" },
                { TimeZoneOfficialEnum.Choibalsan_Summer_Time, "CHOST" },
                { TimeZoneOfficialEnum.Chamorro_Standard_Time, "CHST" },
                { TimeZoneOfficialEnum.Chuuk_Time, "CHUT" },
                { TimeZoneOfficialEnum.Cayman_Islands_Standard_Time, "CIST" },
                { TimeZoneOfficialEnum.Central_Indonesia_Time, "WITA" },
                { TimeZoneOfficialEnum.Cook_Island_Time, "CKT" },
                { TimeZoneOfficialEnum.Chile_Summer_Time, "CLST" },
                { TimeZoneOfficialEnum.Chile_Standard_Time, "CLT" },
                { TimeZoneOfficialEnum.Colombia_Time, "COT" },
                { TimeZoneOfficialEnum.Central_Standard_Time, "CST" },
                { TimeZoneOfficialEnum.China_Standard_Time, "CST" },
                { TimeZoneOfficialEnum.Cuba_Standard_Time, "CST" },
                { TimeZoneOfficialEnum.Cape_Verde_Time, "CVT" },
                { TimeZoneOfficialEnum.Australian_Central_Western_Standard_Time, "ACWST" },
                { TimeZoneOfficialEnum.Christmas_Island_Time, "CXT" },
                { TimeZoneOfficialEnum.Davis_Time, "DAVT" },
                { TimeZoneOfficialEnum.Dumont_DUrville_Time, "DDUT" },
                { TimeZoneOfficialEnum.Easter_Island_Summer_Time, "EASST" },
                { TimeZoneOfficialEnum.Easter_Island_Standard_Time, "EAST" },
                { TimeZoneOfficialEnum.East_Africa_Time, "EAT" },
                { TimeZoneOfficialEnum.Ecuador_Time, "ECT" },
                { TimeZoneOfficialEnum.Eastern_Daylight_Time, "EDT" },
                { TimeZoneOfficialEnum.Australian_Eastern_Daylight_Time, "AEDT" },
                { TimeZoneOfficialEnum.Eastern_European_Summer_Time, "EEST" },
                { TimeZoneOfficialEnum.Eastern_European_Time, "EET" },
                { TimeZoneOfficialEnum.Eastern_Greenland_Summer_Time, "EGST" },
                { TimeZoneOfficialEnum.Eastern_Greenland_Time, "EGT" },
                { TimeZoneOfficialEnum.Eastern_Indonesian_Time, "WIT" },
                { TimeZoneOfficialEnum.Eastern_Standard_Time, "EST" },
                { TimeZoneOfficialEnum.Further_Eastern_European_Time, "FET" },
                { TimeZoneOfficialEnum.Fiji_Time, "FJT" },
                { TimeZoneOfficialEnum.Falkland_Islands_Summer_Time, "FKST" },
                { TimeZoneOfficialEnum.Falkland_Islands_Time, "FKT" },
                { TimeZoneOfficialEnum.Fernando_de_Noronha_Time, "FNT" },
                { TimeZoneOfficialEnum.Galapagos_Time, "GALT" },
                { TimeZoneOfficialEnum.Gambier_Islands, "GAMT" },
                { TimeZoneOfficialEnum.Georgia_Standard_Time, "GET" },
                { TimeZoneOfficialEnum.French_Guiana_Time, "GFT" },
                { TimeZoneOfficialEnum.Gilbert_Island_Time, "GILT" },
                { TimeZoneOfficialEnum.Greenwich_Mean_Time, "GMT" },
                { TimeZoneOfficialEnum.Gulf_Standard_Time, "GST" },
                { TimeZoneOfficialEnum.Guyana_Time, "GYT" },
                { TimeZoneOfficialEnum.Hawaii_Aleutian_Daylight_Time, "HADT" },
                { TimeZoneOfficialEnum.Hawaii_Aleutian_Standard_Time, "HAST" },
                { TimeZoneOfficialEnum.Hong_Kong_Time, "HKT" },
                { TimeZoneOfficialEnum.Indochina_Time, "ICT" },
                { TimeZoneOfficialEnum.Israel_Daylight_Time, "IDT" },
                { TimeZoneOfficialEnum.Iran_Daylight_Time, "IRDT" },
                { TimeZoneOfficialEnum.Irkutsk_Time, "IRKT" },
                { TimeZoneOfficialEnum.Iran_Standard_Time, "IRST" },
                { TimeZoneOfficialEnum.Indian_Standard_Time, "IST" },
                { TimeZoneOfficialEnum.Irish_Standard_Time, "IST" },
                { TimeZoneOfficialEnum.Israel_Standard_Time, "IST" },
                { TimeZoneOfficialEnum.Japan_Standard_Time, "JST" },
                { TimeZoneOfficialEnum.Kyrgyzstan_Time, "KGT" },
                { TimeZoneOfficialEnum.Kosrae_Time, "KOST" },
                { TimeZoneOfficialEnum.Krasnoyarsk_Time, "KRAT" },
                { TimeZoneOfficialEnum.Korea_Standard_Time, "KST" },
                { TimeZoneOfficialEnum.Lord_Howe_Standard_Time, "LHST" },
                { TimeZoneOfficialEnum.Lord_Howe_Summer_Time, "LHST" },
                { TimeZoneOfficialEnum.Line_Islands_Time, "LINT" },
                { TimeZoneOfficialEnum.Magadan_Time, "MAGT" },
                { TimeZoneOfficialEnum.Marquesas_Islands_Time, "MART" }, 
                { TimeZoneOfficialEnum.Mawson_Time, "MAWT" },
                { TimeZoneOfficialEnum.Mountain_Daylight_Time, "MDT" },
                { TimeZoneOfficialEnum.Marshall_Islands, "MHT" },
                { TimeZoneOfficialEnum.Myanmar_Standard_Time, "MMT" },
                { TimeZoneOfficialEnum.Moscow_Standard_Time, "MSK" },
                { TimeZoneOfficialEnum.Mountain_Standard_Time, "MST" },
                { TimeZoneOfficialEnum.Mauritius_Time, "MUT" },
                { TimeZoneOfficialEnum.Maldives_Time, "MVT" },
                { TimeZoneOfficialEnum.Malaysia_Time, "MYT" },
                { TimeZoneOfficialEnum.New_Caledonia_Time, "NCT" },
                { TimeZoneOfficialEnum.Newfoundland_Daylight_Time, "NDT" },
                { TimeZoneOfficialEnum.Norfolk_Time, "NFT" },
                { TimeZoneOfficialEnum.Nepal_Time, "NPT" },
                { TimeZoneOfficialEnum.Newfoundland_Standard_Time, "NST" },
                { TimeZoneOfficialEnum.Niue_Time, "NUT" },
                { TimeZoneOfficialEnum.New_Zealand_Daylight_Time, "NZDT" },
                { TimeZoneOfficialEnum.New_Zealand_Standard_Time, "NZST" },
                { TimeZoneOfficialEnum.Omsk_Standard_Time, "OMST" },
                { TimeZoneOfficialEnum.Oral_Time, "ORAT" },
                { TimeZoneOfficialEnum.Pacific_Daylight_Time, "PDT" },
                { TimeZoneOfficialEnum.Peru_Time, "PET" },
                { TimeZoneOfficialEnum.Kamchatka_Time, "PETT" },
                { TimeZoneOfficialEnum.Papua_New_Guinea_Time, "PGT" },
                { TimeZoneOfficialEnum.Phoenix_Island_Time, "PHOT" },
                { TimeZoneOfficialEnum.Philippine_Time, "PHT" },
                { TimeZoneOfficialEnum.Pakistan_Standard_Time, "PKT" },
                { TimeZoneOfficialEnum.Saint_Pierre_And_Miquelon_Daylight_Time, "PMDT" },
                { TimeZoneOfficialEnum.Saint_Pierre_And_Miquelon_Standard_Time, "PMST" },
                { TimeZoneOfficialEnum.Pohnpei_Standard_Time, "PONT" },
                { TimeZoneOfficialEnum.Pacific_Standard_Time, "PST" },
                { TimeZoneOfficialEnum.Paraguay_Summer_Time, "PYST" },
                { TimeZoneOfficialEnum.Paraguay_Time, "PYT" },
                { TimeZoneOfficialEnum.Réunion_Time, "RET" },
                { TimeZoneOfficialEnum.Rothera_Time, "ROTT" },
                { TimeZoneOfficialEnum.Sakhalin_Island_Time, "SAKT" },
                { TimeZoneOfficialEnum.Samara_Time, "SAMT" },
                { TimeZoneOfficialEnum.South_African_Standard_Time, "SAST" },
                { TimeZoneOfficialEnum.Solomon_Islands_Time, "SBT" },
                { TimeZoneOfficialEnum.Seychelles_Time, "SCT" },
                { TimeZoneOfficialEnum.Singapore_Time, "SGT" },
                { TimeZoneOfficialEnum.Sri_Lanka_Standard_Time, "SLST" },
                { TimeZoneOfficialEnum.Srednekolymsk_Time, "SRET" },
                { TimeZoneOfficialEnum.Suriname_Time, "SRT" },
                { TimeZoneOfficialEnum.Samoa_Standard_Time, "SST" },
                { TimeZoneOfficialEnum.Showa_Station_Time, "SYOT" },
                { TimeZoneOfficialEnum.Tahiti_Time, "TAHT" },
                { TimeZoneOfficialEnum.Thailand_Standard_Time, "THA" },
                { TimeZoneOfficialEnum.Indian_Kerguelen, "TFT" },
                { TimeZoneOfficialEnum.Tajikistan_Time, "TJT" },
                { TimeZoneOfficialEnum.Tokelau_Time, "TKT" },
                { TimeZoneOfficialEnum.East_Timor_Time, "TLT" },
                { TimeZoneOfficialEnum.Turkmenistan_Time, "TMT" },
                { TimeZoneOfficialEnum.Turkey_Time, "TRT" },
                { TimeZoneOfficialEnum.Tonga_Time, "TOT" },
                { TimeZoneOfficialEnum.Tuvalu_Time, "TVT" },
                { TimeZoneOfficialEnum.Ulaanbaatar_Summer_Time, "ULAST" },
                { TimeZoneOfficialEnum.Ulaanbaatar_Standard_Time, "ULAT" },
                { TimeZoneOfficialEnum.Coordinated_Universal_Time, "UTC" },
                { TimeZoneOfficialEnum.Uruguay_Summer_Time, "UYST" },
                { TimeZoneOfficialEnum.Uruguay_Time, "UYT" },
                { TimeZoneOfficialEnum.Uzbekistan_Time, "UZT" },
                { TimeZoneOfficialEnum.Venezuelan_Standard_Time, "VET" },
                { TimeZoneOfficialEnum.Vladivostok_Time, "VLAT" },
                { TimeZoneOfficialEnum.Vostok_Station_Time, "VOST" },
                { TimeZoneOfficialEnum.Vanuatu_Time, "VUT" },
                { TimeZoneOfficialEnum.Wake_Time, "WAKT" },
                { TimeZoneOfficialEnum.West_Africa_Summer_Time, "WAST" },
                { TimeZoneOfficialEnum.West_Africa_Time, "WAT" },
                { TimeZoneOfficialEnum.Western_European_Summer_Time, "WEST" },
                { TimeZoneOfficialEnum.Western_European_Time, "WET" },
                { TimeZoneOfficialEnum.Western_Indonesian_Time, "WIB" },
                { TimeZoneOfficialEnum.Yakutsk_Time, "YAKT" },
                { TimeZoneOfficialEnum.Yekaterinburg_Time, "YEKT" }
            };
        }
    }
}
