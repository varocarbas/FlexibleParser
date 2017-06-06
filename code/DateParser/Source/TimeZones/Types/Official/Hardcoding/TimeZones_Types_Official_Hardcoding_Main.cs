using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    ///<summary><para>All the official timezones.</para></summary>
    public enum TimeZoneOfficialEnum
    {
        ///<summary><para>None.</para></summary>
        None = 0,
        ///<summary><para>Australian Central Daylight Savings Time (ACDT) -- UTC +10:30.</para></summary>
        Australian_Central_Daylight_Savings_Time,
        ///<summary><para>Australian Central Standard Time (ACST) -- UTC +09:30.</para></summary>
        Australian_Central_Standard_Time,
        ///<summary><para>Australian Central Western Standard Time (ACWST) -- UTC +08:45.</para></summary>
        Australian_Central_Western_Standard_Time,
        ///<summary><para>Acre Time (ACT) -- UTC -05:00.</para></summary>
        Acre_Time,
        ///<summary><para>Atlantic Daylight Time (ADT) -- UTC -03:00.</para></summary>
        Atlantic_Daylight_Time,
        ///<summary><para>Australian Eastern Standard Time (AEST) -- UTC +10:00.</para></summary>
        Australian_Eastern_Standard_Time,
        ///<summary><para>Afghanistan Time (AFT) -- UTC +04:30.</para></summary>
        Afghanistan_Time,
        ///<summary><para>Alaska Daylight Time (AKDT) -- UTC -08:00.</para></summary>
        Alaska_Daylight_Time,
        ///<summary><para>Alaska Standard Time (AKST) -- UTC -09:00.</para></summary>
        Alaska_Standard_Time,
        ///<summary><para>Amazon Summer Time Brazil (AMST) -- UTC -03:00.</para></summary>
        Amazon_Summer_Time_Brazil,
        ///<summary><para>Amazon Time Brazil (AMT) -- UTC -04:00.</para></summary>
        Amazon_Time_Brazil,
        ///<summary><para>Armenia Time (AMT) -- UTC +04:00.</para></summary>
        Armenia_Time,
        ///<summary><para>Argentina Time (ART) -- UTC -03:00.</para></summary>
        Argentina_Time,
        ///<summary><para>Arabia Standard Time (AST) -- UTC +03:00.</para></summary>
        Arabia_Standard_Time,
        ///<summary><para>Atlantic Standard Time (AST) -- UTC -04:00.</para></summary>
        Atlantic_Standard_Time,
        ///<summary><para>Australian Western Standard Time (AWST) -- UTC +08:00.</para></summary>
        Australian_Western_Standard_Time,
        ///<summary><para>Azores Summer Time (AZOST) -- UTC +00:00.</para></summary>
        Azores_Summer_Time,
        ///<summary><para>Azores Standard Time (AZOT) -- UTC -01:00.</para></summary>
        Azores_Standard_Time,
        ///<summary><para>Azerbaijan Time (AZT) -- UTC +04:00.</para></summary>
        Azerbaijan_Time,
        ///<summary><para>Brunei Darussalam Time (BNT) -- UTC -08:00.</para></summary>
        Brunei_Darussalam_Time,
        ///<summary><para>Indian Chagos Time (IOT) -- UTC +06:00.</para></summary>
        Indian_Chagos_Time,
        ///<summary><para>Bolivia Time (BOT) -- UTC -04:00.</para></summary>
        Bolivia_Time,
        ///<summary><para>Brasilia Summer Time (BRST) -- UTC -02:00.</para></summary>
        Brasilia_Summer_Time,
        ///<summary><para>Brasilia Time (BRT) -- UTC -03:00.</para></summary>
        Brasilia_Time,
        ///<summary><para>Bangladesh Standard Time (BST) -- UTC +06:00.</para></summary>
        Bangladesh_Standard_Time,
        ///<summary><para>Bougainville Standard Time (BST) -- UTC +11:00.</para></summary>
        Bougainville_Standard_Time,
        ///<summary><para>British Summer Time (BST) -- UTC +01:00.</para></summary>
        British_Summer_Time,
        ///<summary><para>Bhutan Time (BTT) -- UTC +06:00.</para></summary>
        Bhutan_Time,
        ///<summary><para>Central Africa Time (CAT) -- UTC +02:00.</para></summary>
        Central_Africa_Time,
        ///<summary><para>Cocos Islands Time (CCT) -- UTC +06:30.</para></summary>
        Cocos_Islands_Time,
        ///<summary><para>Central Daylight Time (CDT) -- UTC -05:00.</para></summary>
        Central_Daylight_Time,
        ///<summary><para>Cuba Daylight Time (CDT) -- UTC -04:00.</para></summary>
        Cuba_Daylight_Time,
        ///<summary><para>Central European Summer Time (CEST) -- UTC +02:00.</para></summary>
        Central_European_Summer_Time,
        ///<summary><para>Central European Time (CET) -- UTC +01:00.</para></summary>
        Central_European_Time,
        ///<summary><para>Chatham Daylight Time (CHADT) -- UTC +13:45.</para></summary>
        Chatham_Daylight_Time,
        ///<summary><para>Chatham Standard Time (CHAST) -- UTC +12:45.</para></summary>
        Chatham_Standard_Time,
        ///<summary><para>Choibalsan Standard Time (CHOT) -- UTC +08:00.</para></summary>
        Choibalsan_Standard_Time,
        ///<summary><para>Choibalsan Summer Time (CHOST) -- UTC +09:00.</para></summary>
        Choibalsan_Summer_Time,
        ///<summary><para>Chamorro Standard Time (CHST) -- UTC +10:00.</para></summary>
        Chamorro_Standard_Time,
        ///<summary><para>Chuuk Time (CHUT) -- UTC +10:00.</para></summary>
        Chuuk_Time,
        ///<summary><para>Cayman Islands Standard Time (CIST) -- UTC -08:00.</para></summary>
        Cayman_Islands_Standard_Time,
        ///<summary><para>Central Indonesia Time (WITA) -- UTC +08:00.</para></summary>
        Central_Indonesia_Time,
        ///<summary><para>Cook Island Time (CKT) -- UTC -10:00.</para></summary>
        Cook_Island_Time,
        ///<summary><para>Chile Summer Time (CLST) -- UTC -03:00.</para></summary>
        Chile_Summer_Time,
        ///<summary><para>Chile Standard Time (CLT) -- UTC -04:00.</para></summary>
        Chile_Standard_Time,
        ///<summary><para>Colombia Time (COT) -- UTC -05:00.</para></summary>
        Colombia_Time,
        ///<summary><para>Central Standard Time (CST) -- UTC -06:00.</para></summary>
        Central_Standard_Time,
        ///<summary><para>China Standard Time (CST) -- UTC +08:00.</para></summary>
        China_Standard_Time,
        ///<summary><para>Cuba Standard Time (CST) -- UTC -05:00.</para></summary>
        Cuba_Standard_Time,
        ///<summary><para>Cape Verde Time (CVT) -- UTC -01:00.</para></summary>
        Cape_Verde_Time,
        ///<summary><para>Christmas Island Time (CXT) -- UTC +07:00.</para></summary>
        Christmas_Island_Time,
        ///<summary><para>Davis Time (DAVT) -- UTC +07:00.</para></summary>
        Davis_Time,
        ///<summary><para>Dumont-d'Urville (DDUT) -- UTC +10:00.</para></summary>
        Dumont_DUrville_Time,
        ///<summary><para>Easter Island Summer Time (EASST) -- UTC -05:00.</para></summary>
        Easter_Island_Summer_Time,
        ///<summary><para>Easter Island Standard Time (EAST) -- UTC -06:00.</para></summary>
        Easter_Island_Standard_Time,
        ///<summary><para>East Africa Time (EAT) -- UTC +03:00.</para></summary>
        East_Africa_Time,
        ///<summary><para>Ecuador Time (ECT) -- UTC -05:00.</para></summary>
        Ecuador_Time,
        ///<summary><para>Eastern Daylight Time (EDT) -- UTC -04:00.</para></summary>
        Eastern_Daylight_Time,
        ///<summary><para>Australian Eastern Daylight Time (AEDT) -- UTC +11:00.</para></summary>
        Australian_Eastern_Daylight_Time,
        ///<summary><para>Eastern European Summer Time (EEST) -- UTC +03:00.</para></summary>
        Eastern_European_Summer_Time,
        ///<summary><para>Eastern European Time (EET) -- UTC +02:00.</para></summary>
        Eastern_European_Time,
        ///<summary><para>Eastern Greenland Summer Time (EGST) -- UTC +00:00.</para></summary>
        Eastern_Greenland_Summer_Time,
        ///<summary><para>Eastern Greenland Time (EGT) -- UTC -01:00.</para></summary>
        Eastern_Greenland_Time,
        ///<summary><para>Eastern Indonesian Time (WIT) -- UTC +09:00.</para></summary>
        Eastern_Indonesian_Time,
        ///<summary><para>Western Indonesian Time (WIB) -- UTC +07:00.</para></summary>
        Western_Indonesian_Time,
        ///<summary><para>Eastern Standard Time (EST) -- UTC -05:00.</para></summary>
        Eastern_Standard_Time,
        ///<summary><para>Further-Eastern European Time (FET) -- UTC +03:00.</para></summary>
        Further_Eastern_European_Time,
        ///<summary><para>Fiji Time (FJT) -- UTC +12:00.</para></summary>
        Fiji_Time,
        ///<summary><para>Falkland Islands Summer Time (FKST) -- UTC -03:00.</para></summary>
        Falkland_Islands_Summer_Time,
        ///<summary><para>Falkland Islands Time (FKT) -- UTC -04:00.</para></summary>
        Falkland_Islands_Time,
        ///<summary><para>Fernando De Noronha Time (FNT) -- UTC -02:00.</para></summary>
        Fernando_de_Noronha_Time,
        ///<summary><para>Galapagos Time (GALT) -- UTC -06:00.</para></summary>
        Galapagos_Time,
        ///<summary><para>Gambier Islands (GAMT) -- UTC -09:00.</para></summary>
        Gambier_Islands,
        ///<summary><para>Georgia Standard Time (GET) -- UTC +04:00.</para></summary>
        Georgia_Standard_Time,
        ///<summary><para>French Guiana Time (GFT) -- UTC -03:00.</para></summary>
        French_Guiana_Time,
        ///<summary><para>Gilbert Island Time (GILT) -- UTC +12:00.</para></summary>
        Gilbert_Island_Time,
        ///<summary><para>Greenwich Mean Time (GMT) -- UTC +00:00.</para></summary>
        Greenwich_Mean_Time,
        ///<summary><para>Gulf Standard Time (GST) -- UTC +04:00.</para></summary>
        Gulf_Standard_Time,
        ///<summary><para>Guyana Time (GYT) -- UTC -04:00.</para></summary>
        Guyana_Time,
        ///<summary><para>Hawaii Aleutian Daylight Time (HADT) -- UTC -09:00.</para></summary>
        Hawaii_Aleutian_Daylight_Time,
        ///<summary><para>Hawaii Aleutian Standard Time (HAST) -- UTC -10:00.</para></summary>
        Hawaii_Aleutian_Standard_Time,
        ///<summary><para>Hong Kong Time (HKT) -- UTC +08:00.</para></summary>
        Hong_Kong_Time,
        ///<summary><para>Indochina Time (ICT) -- UTC +07:00.</para></summary>
        Indochina_Time,
        ///<summary><para>Israel Daylight Time (IDT) -- UTC +03:00.</para></summary>
        Israel_Daylight_Time,
        ///<summary><para>Iran Daylight Time (IRDT) -- UTC +04:30.</para></summary>
        Iran_Daylight_Time,
        ///<summary><para>Irkutsk Time (IRKT) -- UTC +08:00.</para></summary>
        Irkutsk_Time,
        ///<summary><para>Iran Standard Time (IRST) -- UTC +03:30.</para></summary>
        Iran_Standard_Time,
        ///<summary><para>Indian Standard Time (IST) -- UTC +05:30.</para></summary>
        Indian_Standard_Time,
        ///<summary><para>Irish Standard Time (IST) -- UTC +01:00.</para></summary>
        Irish_Standard_Time,
        ///<summary><para>Israel Standard Time (IST) -- UTC +02:00.</para></summary>
        Israel_Standard_Time,
        ///<summary><para>Japan Standard Time (JST) -- UTC +09:00.</para></summary>
        Japan_Standard_Time,
        ///<summary><para>Kyrgyzstan Time (KGT) -- UTC +06:00.</para></summary>
        Kyrgyzstan_Time,
        ///<summary><para>Kosrae Time (KOST) -- UTC +11:00.</para></summary>
        Kosrae_Time,
        ///<summary><para>Krasnoyarsk Time (KRAT) -- UTC +07:00.</para></summary>
        Krasnoyarsk_Time,
        ///<summary><para>Korea Standard Time (KST) -- UTC +09:00.</para></summary>
        Korea_Standard_Time,
        ///<summary><para>Lord Howe Standard Time (LHST) -- UTC +10:30.</para></summary>
        Lord_Howe_Standard_Time,
        ///<summary><para>Lord Howe Summer Time (LHST) -- UTC +11:00.</para></summary>
        Lord_Howe_Summer_Time,
        ///<summary><para>Line Islands Time (LINT) -- UTC +14:00.</para></summary>
        Line_Islands_Time,
        ///<summary><para>Magadan Time (MAGT) -- UTC +12:00.</para></summary>
        Magadan_Time,
        ///<summary><para>Marquesas Islands Time (MART) -- UTC -09:30.</para></summary>
        Marquesas_Islands_Time,
        ///<summary><para>Mawson Time (MAWT) -- UTC +05:00.</para></summary>
        Mawson_Time,
        ///<summary><para>Mountain Daylight Time (MDT) -- UTC -06:00.</para></summary>
        Mountain_Daylight_Time,
        ///<summary><para>Marshall Islands (MHT) -- UTC +12:00.</para></summary>
        Marshall_Islands,
        ///<summary><para>Myanmar Standard Time (MMT) -- UTC +06:30.</para></summary>
        Myanmar_Standard_Time,
        ///<summary><para>Moscow Standard Time (MSK) -- UTC +03:00.</para></summary>
        Moscow_Standard_Time,
        ///<summary><para>Malaysia Time (MYT) -- UTC +08:00.</para></summary>
        Malaysia_Time,
        ///<summary><para>Mountain Standard Time (MST) -- UTC -07:00.</para></summary>
        Mountain_Standard_Time,
        ///<summary><para>Mauritius Time (MUT) -- UTC +04:00.</para></summary>
        Mauritius_Time,
        ///<summary><para>Maldives Time (MVT) -- UTC +05:00.</para></summary>
        Maldives_Time,
        ///<summary><para>New Caledonia Time (NCT) -- UTC +11:00.</para></summary>
        New_Caledonia_Time,
        ///<summary><para>Newfoundland Daylight Time (NDT) -- UTC -02:30.</para></summary>
        Newfoundland_Daylight_Time,
        ///<summary><para>Norfolk Time (NFT) -- UTC +11:00.</para></summary>
        Norfolk_Time,
        ///<summary><para>Nepal Time (NPT) -- UTC +05:45.</para></summary>
        Nepal_Time,
        ///<summary><para>Newfoundland Standard Time (NST) -- UTC -03:30.</para></summary>
        Newfoundland_Standard_Time,
        ///<summary><para>Niue Time (NUT) -- UTC -11:00.</para></summary>
        Niue_Time,
        ///<summary><para>New Zealand Daylight Time (NZDT) -- UTC +13:00.</para></summary>
        New_Zealand_Daylight_Time,
        ///<summary><para>New Zealand Standard Time (NZST) -- UTC +12:00.</para></summary>
        New_Zealand_Standard_Time,
        ///<summary><para>Omsk Standard Time (OMST) -- UTC +06:00.</para></summary>
        Omsk_Standard_Time,
        ///<summary><para>Oral Time (ORAT) -- UTC +05:00.</para></summary>
        Oral_Time,
        ///<summary><para>Pacific Daylight Time (PDT) -- UTC -07:00.</para></summary>
        Pacific_Daylight_Time,
        ///<summary><para>Peru Time (PET) -- UTC -05:00.</para></summary>
        Peru_Time,
        ///<summary><para>Kamchatka Time (PETT) -- UTC +12:00.</para></summary>
        Kamchatka_Time,
        ///<summary><para>Papua New Guinea Time (PGT) -- UTC +10:00.</para></summary>
        Papua_New_Guinea_Time,
        ///<summary><para>Phoenix Island Time (PHOT) -- UTC +13:00.</para></summary>
        Phoenix_Island_Time,
        ///<summary><para>Philippine Time (PHT) -- UTC +08:00.</para></summary>
        Philippine_Time,
        ///<summary><para>Pakistan Standard Time (PKT) -- UTC +05:00.</para></summary>
        Pakistan_Standard_Time,
        ///<summary><para>Saint Pierre and Miquelon Daylight Time (PMDT) -- UTC -02:00.</para></summary>
        Saint_Pierre_And_Miquelon_Daylight_Time,
        ///<summary><para>Saint Pierre and Miquelon Standard Time (PMST) -- UTC -03:00.</para></summary>
        Saint_Pierre_And_Miquelon_Standard_Time,
        ///<summary><para>Pohnpei Standard Time (PONT) -- UTC +11:00.</para></summary>
        Pohnpei_Standard_Time,
        ///<summary><para>Pacific Standard Time (PST) -- UTC -08:00.</para></summary>
        Pacific_Standard_Time,
        ///<summary><para>Paraguay Summer Time (PYST) -- UTC -03:00.</para></summary>
        Paraguay_Summer_Time,
        ///<summary><para>Paraguay Time (PYT) -- UTC -04:00.</para></summary>
        Paraguay_Time,
        ///<summary><para>R궮ion Time (RET) -- UTC +04:00.</para></summary>
        Réunion_Time,
        ///<summary><para>Rothera Time (ROTT) -- UTC -03:00.</para></summary>
        Rothera_Time,
        ///<summary><para>Sakhalin Island Time (SAKT) -- UTC +11:00.</para></summary>
        Sakhalin_Island_Time,
        ///<summary><para>Samara Time (SAMT) -- UTC +04:00.</para></summary>
        Samara_Time,
        ///<summary><para>South African Standard Time (SAST) -- UTC +02:00.</para></summary>
        South_African_Standard_Time,
        ///<summary><para>Solomon Islands Time (SBT) -- UTC +11:00.</para></summary>
        Solomon_Islands_Time,
        ///<summary><para>Seychelles Time (SCT) -- UTC +04:00.</para></summary>
        Seychelles_Time,
        ///<summary><para>Singapore Time (SGT) -- UTC +08:00.</para></summary>
        Singapore_Time,
        ///<summary><para>Sri Lanka Standard Time (SLST) -- UTC +05:30.</para></summary>
        Sri_Lanka_Standard_Time,
        ///<summary><para>Srednekolymsk Time (SRET) -- UTC +11:00.</para></summary>
        Srednekolymsk_Time,
        ///<summary><para>Suriname Time (SRT) -- UTC -03:00.</para></summary>
        Suriname_Time,
        ///<summary><para>Samoa Standard Time (SST) -- UTC -11:00.</para></summary>
        Samoa_Standard_Time,
        ///<summary><para>Showa Station Time (SYOT) -- UTC +03:00.</para></summary>
        Showa_Station_Time,
        ///<summary><para>Tahiti Time (TAHT) -- UTC -10:00.</para></summary>
        Tahiti_Time,
        ///<summary><para>Thailand Standard Time (THA) -- UTC +07:00.</para></summary>
        Thailand_Standard_Time,
        ///<summary><para>Indian Kerguelen (TFT) -- UTC +05:00.</para></summary>
        Indian_Kerguelen,
        ///<summary><para>Tajikistan Time (TJT) -- UTC +05:00.</para></summary>
        Tajikistan_Time,
        ///<summary><para>Tokelau Time (TKT) -- UTC +13:00.</para></summary>
        Tokelau_Time,
        ///<summary><para>East Timor Time (TLT) -- UTC +09:00.</para></summary>
        East_Timor_Time,
        ///<summary><para>Turkmenistan Time (TMT) -- UTC +05:00.</para></summary>
        Turkmenistan_Time,
        ///<summary><para>Turkey Time (TRT) -- UTC +03:00.</para></summary>
        Turkey_Time,
        ///<summary><para>Tonga Time (TOT) -- UTC +13:00.</para></summary>
        Tonga_Time,
        ///<summary><para>Tuvalu Time (TVT) -- UTC +12:00.</para></summary>
        Tuvalu_Time,
        ///<summary><para>Ulaanbaatar Summer Time (ULAST) -- UTC +09:00.</para></summary>
        Ulaanbaatar_Summer_Time,
        ///<summary><para>Ulaanbaatar Standard Time (ULAT) -- UTC +08:00.</para></summary>
        Ulaanbaatar_Standard_Time,
        ///<summary><para>Coordinated Universal Time (UTC) -- UTC +00:00.</para></summary>
        Coordinated_Universal_Time,
        ///<summary><para>Uruguay Summer Time (UYST) -- UTC -02:00.</para></summary>
        Uruguay_Summer_Time,
        ///<summary><para>Uruguay Time (UYT) -- UTC -03:00.</para></summary>
        Uruguay_Time,
        ///<summary><para>Uzbekistan Time (UZT) -- UTC +05:00.</para></summary>
        Uzbekistan_Time,
        ///<summary><para>Venezuelan Standard Time (VET) -- UTC -04:00.</para></summary>
        Venezuelan_Standard_Time,
        ///<summary><para>Vladivostok Time (VLAT) -- UTC +10:00.</para></summary>
        Vladivostok_Time,
        ///<summary><para>Vostok Station Time (VOST) -- UTC +06:00.</para></summary>
        Vostok_Station_Time,
        ///<summary><para>Vanuatu Time (VUT) -- UTC +11:00.</para></summary>
        Vanuatu_Time,
        ///<summary><para>Wake Time (WAKT) -- UTC +12:00.</para></summary>
        Wake_Time,
        ///<summary><para>West Africa Summer Time (WAST) -- UTC +02:00.</para></summary>
        West_Africa_Summer_Time,
        ///<summary><para>West Africa Time (WAT) -- UTC +01:00.</para></summary>
        West_Africa_Time,
        ///<summary><para>Western European Summer Time (WEST) -- UTC +01:00.</para></summary>
        Western_European_Summer_Time,
        ///<summary><para>Western European Time (WET) -- UTC +00:00.</para></summary>
        Western_European_Time,
        ///<summary><para>Yakutsk Time (YAKT) -- UTC +09:00.</para></summary>
        Yakutsk_Time,
        ///<summary><para>Yekaterinburg Time (YEKT) -- UTC +05:00.</para></summary>
        Yekaterinburg_Time
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
