using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    ///<summary><para>All the countries.</para></summary>
    public enum CountryEnum
    {
        ///<summary><para>None.</para></summary>
        None = 0,
        ///<summary><para>Andorra (AD).</para></summary>
        Andorra,
        ///<summary><para>United Arab Emirates (AE).</para></summary>
        United_Arab_Emirates,
        ///<summary><para>Afghanistan (AF).</para></summary>
        Afghanistan,
        ///<summary><para>Antigua and Barbuda (AG).</para></summary>
        Antigua_and_Barbuda,
        ///<summary><para>Anguilla (AI).</para></summary>
        Anguilla,
        ///<summary><para>Albania (AL).</para></summary>
        Albania,
        ///<summary><para>Armenia (AM).</para></summary>
        Armenia,
        ///<summary><para>Angola (AO).</para></summary>
        Angola,
        ///<summary><para>Antarctica (AQ).</para></summary>
        Antarctica,
        ///<summary><para>Argentina (AR).</para></summary>
        Argentina,
        ///<summary><para>American Samoa (AS).</para></summary>
        American_Samoa,
        ///<summary><para>Austria (AT).</para></summary>
        Austria,
        ///<summary><para>Australia (AU).</para></summary>
        Australia,
        ///<summary><para>Aruba (AW).</para></summary>
        Aruba,
        ///<summary><para>Aland Islands (AX).</para></summary>
        Aland_Islands,
        ///<summary><para>Azerbaijan (AZ).</para></summary>
        Azerbaijan,
        ///<summary><para>Bosnia and Herzegovina (BA).</para></summary>
        Bosnia_and_Herzegovina,
        ///<summary><para>Barbados (BB).</para></summary>
        Barbados,
        ///<summary><para>Bangladesh (BD).</para></summary>
        Bangladesh,
        ///<summary><para>Belgium (BE).</para></summary>
        Belgium,
        ///<summary><para>Burkina Faso (BF).</para></summary>
        Burkina_Faso,
        ///<summary><para>Bulgaria (BG).</para></summary>
        Bulgaria,
        ///<summary><para>Bahrain (BH).</para></summary>
        Bahrain,
        ///<summary><para>Burundi (BI).</para></summary>
        Burundi,
        ///<summary><para>Benin (BJ).</para></summary>
        Benin,
        ///<summary><para>Saint Barthelemy (BL).</para></summary>
        Saint_Barthelemy,
        ///<summary><para>Bermuda (BM).</para></summary>
        Bermuda,
        ///<summary><para>Brunei (BN).</para></summary>
        Brunei,
        ///<summary><para>Bolivia (BO).</para></summary>
        Bolivia,
        ///<summary><para>Caribbean Netherlands (BQ).</para></summary>
        Caribbean_Netherlands,
        ///<summary><para>Brazil (BR).</para></summary>
        Brazil,
        ///<summary><para>Bahamas (BS).</para></summary>
        Bahamas,
        ///<summary><para>Bhutan (BT).</para></summary>
        Bhutan,
        ///<summary><para>Bouvet Island (BV).</para></summary>
        Bouvet_Island,
        ///<summary><para>Botswana (BW).</para></summary>
        Botswana,
        ///<summary><para>Belarus (BY).</para></summary>
        Belarus,
        ///<summary><para>Belize (BZ).</para></summary>
        Belize,
        ///<summary><para>Canada (CA).</para></summary>
        Canada,
        ///<summary><para>Cocos Keeling Islands (CC).</para></summary>
        Cocos_Keeling_Islands,
        ///<summary><para>Democratic Republic of the Congo (CD).</para></summary>
        Democratic_Republic_of_the_Congo,
        ///<summary><para>Central African Republic (CF).</para></summary>
        Central_African_Republic,
        ///<summary><para>Congo Republic (CG).</para></summary>
        Congo_Republic,
        ///<summary><para>Switzerland (CH).</para></summary>
        Switzerland,
        ///<summary><para>Ivory Coast (CI).</para></summary>
        Ivory_Coast,
        ///<summary><para>Cook Islands (CK).</para></summary>
        Cook_Islands,
        ///<summary><para>Chile (CL).</para></summary>
        Chile,
        ///<summary><para>Cameroon (CM).</para></summary>
        Cameroon,
        ///<summary><para>China (CN).</para></summary>
        China,
        ///<summary><para>Colombia (CO).</para></summary>
        Colombia,
        ///<summary><para>Costa Rica (CR).</para></summary>
        Costa_Rica,
        ///<summary><para>Cuba (CU).</para></summary>
        Cuba,
        ///<summary><para>Cape Verde (CV).</para></summary>
        Cape_Verde,
        ///<summary><para>Curaçao (CW).</para></summary>
        Curaçao,
        ///<summary><para>Christmas Island (CX).</para></summary>
        Christmas_Island,
        ///<summary><para>Cyprus (CY).</para></summary>
        Cyprus,
        ///<summary><para>Czech Republic (CZ).</para></summary>
        Czech_Republic,
        ///<summary><para>Germany (DE).</para></summary>
        Germany,
        ///<summary><para>Djibouti (DJ).</para></summary>
        Djibouti,
        ///<summary><para>Denmark (DK).</para></summary>
        Denmark,
        ///<summary><para>Dominica (DM).</para></summary>
        Dominica,
        ///<summary><para>Dominican Republic (DO).</para></summary>
        Dominican_Republic,
        ///<summary><para>Algeria (DZ).</para></summary>
        Algeria,
        ///<summary><para>Ecuador (EC).</para></summary>
        Ecuador,
        ///<summary><para>Estonia (EE).</para></summary>
        Estonia,
        ///<summary><para>Egypt (EG).</para></summary>
        Egypt,
        ///<summary><para>Western Sahara (EH).</para></summary>
        Western_Sahara,
        ///<summary><para>Eritrea (ER).</para></summary>
        Eritrea,
        ///<summary><para>Spain (ES).</para></summary>
        Spain,
        ///<summary><para>Ethiopia (ET).</para></summary>
        Ethiopia,
        ///<summary><para>Finland (FI).</para></summary>
        Finland,
        ///<summary><para>Fiji (FJ).</para></summary>
        Fiji,
        ///<summary><para>Falkland Islands (FK).</para></summary>
        Falkland_Islands,
        ///<summary><para>Federated States of Micronesia (FM).</para></summary>
        Federated_States_of_Micronesia,
        ///<summary><para>Faroe Islands (FO).</para></summary>
        Faroe_Islands,
        ///<summary><para>France (FR).</para></summary>
        France,
        ///<summary><para>Gabon (GA).</para></summary>
        Gabon,
        ///<summary><para>United Kingdom (GB).</para></summary>
        United_Kingdom,
        ///<summary><para>Grenada (GD).</para></summary>
        Grenada,
        ///<summary><para>Georgia (GE).</para></summary>
        Georgia,
        ///<summary><para>French Guiana (GF).</para></summary>
        French_Guiana,
        ///<summary><para>Guernsey (GG).</para></summary>
        Guernsey,
        ///<summary><para>Ghana (GH).</para></summary>
        Ghana,
        ///<summary><para>Gibraltar (GI).</para></summary>
        Gibraltar,
        ///<summary><para>Greenland (GL).</para></summary>
        Greenland,
        ///<summary><para>Gambia (GM).</para></summary>
        Gambia,
        ///<summary><para>Guinea (GN).</para></summary>
        Guinea,
        ///<summary><para>Guadeloupe (GP).</para></summary>
        Guadeloupe,
        ///<summary><para>Equatorial Guinea (GQ).</para></summary>
        Equatorial_Guinea,
        ///<summary><para>Greece (GR).</para></summary>
        Greece,
        ///<summary><para>South Georgia and the South Sandwich Islands (GS).</para></summary>
        South_Georgia_and_the_South_Sandwich_Islands,
        ///<summary><para>Guatemala (GT).</para></summary>
        Guatemala,
        ///<summary><para>Guam (GU).</para></summary>
        Guam,
        ///<summary><para>Guinea Bissau (GW).</para></summary>
        Guinea_Bissau,
        ///<summary><para>Guyana (GY).</para></summary>
        Guyana,
        ///<summary><para>Hong Kong (HK).</para></summary>
        Hong_Kong,
        ///<summary><para>Heard Island and McDonald Islands (HM).</para></summary>
        Heard_Island_and_McDonald_Islands,
        ///<summary><para>Honduras (HN).</para></summary>
        Honduras,
        ///<summary><para>Croatia (HR).</para></summary>
        Croatia,
        ///<summary><para>Haiti (HT).</para></summary>
        Haiti,
        ///<summary><para>Hungary (HU).</para></summary>
        Hungary,
        ///<summary><para>Indonesia (ID).</para></summary>
        Indonesia,
        ///<summary><para>Ireland (IE).</para></summary>
        Ireland,
        ///<summary><para>Israel (IL).</para></summary>
        Israel,
        ///<summary><para>Isle of Man (IM).</para></summary>
        Isle_of_Man,
        ///<summary><para>India (IN).</para></summary>
        India,
        ///<summary><para>Kosovo.</para></summary>
        Kosovo,
        ///<summary><para>British Indian Ocean Territory (IO).</para></summary>
        British_Indian_Ocean_Territory,
        ///<summary><para>Iraq (IQ).</para></summary>
        Iraq,
        ///<summary><para>Iran (IR).</para></summary>
        Iran,
        ///<summary><para>Iceland (IS).</para></summary>
        Iceland,
        ///<summary><para>Italy (IT).</para></summary>
        Italy,
        ///<summary><para>Jersey (JE).</para></summary>
        Jersey,
        ///<summary><para>Jamaica (JM).</para></summary>
        Jamaica,
        ///<summary><para>Jordan (JO).</para></summary>
        Jordan,
        ///<summary><para>Japan (JP).</para></summary>
        Japan,
        ///<summary><para>Kenya (KE).</para></summary>
        Kenya,
        ///<summary><para>Kyrgyzstan (KG).</para></summary>
        Kyrgyzstan,
        ///<summary><para>Cambodia (KH).</para></summary>
        Cambodia,
        ///<summary><para>Kiribati (KI).</para></summary>
        Kiribati,
        ///<summary><para>Comoros (KM).</para></summary>
        Comoros,
        ///<summary><para>Saint Kitts and Nevis (KN).</para></summary>
        Saint_Kitts_and_Nevis,
        ///<summary><para>North Korea (KP).</para></summary>
        North_Korea,
        ///<summary><para>South Korea (KR).</para></summary>
        South_Korea,
        ///<summary><para>Kuwait (KW).</para></summary>
        Kuwait,
        ///<summary><para>Cayman Islands (KY).</para></summary>
        Cayman_Islands,
        ///<summary><para>Kazakhstan (KZ).</para></summary>
        Kazakhstan,
        ///<summary><para>Laos (LA).</para></summary>
        Laos,
        ///<summary><para>Lebanon (LB).</para></summary>
        Lebanon,
        ///<summary><para>Saint Lucia (LC).</para></summary>
        Saint_Lucia,
        ///<summary><para>Liechtenstein (LI).</para></summary>
        Liechtenstein,
        ///<summary><para>Sri Lanka (LK).</para></summary>
        Sri_Lanka,
        ///<summary><para>Liberia (LR).</para></summary>
        Liberia,
        ///<summary><para>Lesotho (LS).</para></summary>
        Lesotho,
        ///<summary><para>Lithuania (LT).</para></summary>
        Lithuania,
        ///<summary><para>Luxembourg (LU).</para></summary>
        Luxembourg,
        ///<summary><para>Latvia (LV).</para></summary>
        Latvia,
        ///<summary><para>Libya (LY).</para></summary>
        Libya,
        ///<summary><para>Morocco (MA).</para></summary>
        Morocco,
        ///<summary><para>Monaco (MC).</para></summary>
        Monaco,
        ///<summary><para>Moldova (MD).</para></summary>
        Moldova,
        ///<summary><para>Montenegro (ME).</para></summary>
        Montenegro,
        ///<summary><para>Saint Martin (MF).</para></summary>
        Saint_Martin,
        ///<summary><para>Madagascar (MG).</para></summary>
        Madagascar,
        ///<summary><para>Marshall Islands (MH).</para></summary>
        Marshall_Islands,
        ///<summary><para>Macedonia (MK).</para></summary>
        Macedonia,
        ///<summary><para>Mali (ML).</para></summary>
        Mali,
        ///<summary><para>Myanmar (MM).</para></summary>
        Myanmar,
        ///<summary><para>Mongolia (MN).</para></summary>
        Mongolia,
        ///<summary><para>Macau (MO).</para></summary>
        Macau,
        ///<summary><para>Northern Mariana Islands (MP).</para></summary>
        Northern_Mariana_Islands,
        ///<summary><para>Martinique (MQ).</para></summary>
        Martinique,
        ///<summary><para>Mauritania (MR).</para></summary>
        Mauritania,
        ///<summary><para>Montserrat (MS).</para></summary>
        Montserrat,
        ///<summary><para>Malta (MT).</para></summary>
        Malta,
        ///<summary><para>Mauritius (MU).</para></summary>
        Mauritius,
        ///<summary><para>Maldives (MV).</para></summary>
        Maldives,
        ///<summary><para>Malawi (MW).</para></summary>
        Malawi,
        ///<summary><para>Mexico (MX).</para></summary>
        Mexico,
        ///<summary><para>Malaysia (MY).</para></summary>
        Malaysia,
        ///<summary><para>Mozambique (MZ).</para></summary>
        Mozambique,
        ///<summary><para>Namibia (NA).</para></summary>
        Namibia,
        ///<summary><para>New Caledonia (NC).</para></summary>
        New_Caledonia,
        ///<summary><para>Niger (NE).</para></summary>
        Niger,
        ///<summary><para>Norfolk Island (NF).</para></summary>
        Norfolk_Island,
        ///<summary><para>Nigeria (NG).</para></summary>
        Nigeria,
        ///<summary><para>Nicaragua (NI).</para></summary>
        Nicaragua,
        ///<summary><para>Netherlands (NL).</para></summary>
        Netherlands,
        ///<summary><para>Norway (NO).</para></summary>
        Norway,
        ///<summary><para>Nepal (NP).</para></summary>
        Nepal,
        ///<summary><para>Nauru (NR).</para></summary>
        Nauru,
        ///<summary><para>Niue (NU).</para></summary>
        Niue,
        ///<summary><para>New Zealand (NZ).</para></summary>
        New_Zealand,
        ///<summary><para>Oman (OM).</para></summary>
        Oman,
        ///<summary><para>Panama (PA).</para></summary>
        Panama,
        ///<summary><para>Peru (PE).</para></summary>
        Peru,
        ///<summary><para>French Polynesia (PF).</para></summary>
        French_Polynesia,
        ///<summary><para>Papua New Guinea (PG).</para></summary>
        Papua_New_Guinea,
        ///<summary><para>Philippines (PH).</para></summary>
        Philippines,
        ///<summary><para>Pakistan (PK).</para></summary>
        Pakistan,
        ///<summary><para>Poland (PL).</para></summary>
        Poland,
        ///<summary><para>Saint Pierre and Miquelon (PM).</para></summary>
        Saint_Pierre_and_Miquelon,
        ///<summary><para>Pitcairn Islands (PN).</para></summary>
        Pitcairn_Islands,
        ///<summary><para>Puerto Rico (PR).</para></summary>
        Puerto_Rico,
        ///<summary><para>Palestine (PS).</para></summary>
        Palestine,
        ///<summary><para>Portugal (PT).</para></summary>
        Portugal,
        ///<summary><para>Palau (PW).</para></summary>
        Palau,
        ///<summary><para>Paraguay (PY).</para></summary>
        Paraguay,
        ///<summary><para>Qatar (QA).</para></summary>
        Qatar,
        ///<summary><para>Reunion (RE).</para></summary>
        Reunion,
        ///<summary><para>Romania (RO).</para></summary>
        Romania,
        ///<summary><para>Serbia (RS).</para></summary>
        Serbia,
        ///<summary><para>Russia (RU).</para></summary>
        Russia,
        ///<summary><para>Rwanda (RW).</para></summary>
        Rwanda,
        ///<summary><para>Saudi Arabia (SA).</para></summary>
        Saudi_Arabia,
        ///<summary><para>Solomon Islands (SB).</para></summary>
        Solomon_Islands,
        ///<summary><para>Seychelles (SC).</para></summary>
        Seychelles,
        ///<summary><para>Sudan (SD).</para></summary>
        Sudan,
        ///<summary><para>Sweden (SE).</para></summary>
        Sweden,
        ///<summary><para>Singapore (SG).</para></summary>
        Singapore,
        ///<summary><para>Saint Helena (SH).</para></summary>
        Saint_Helena,
        ///<summary><para>Slovenia (SI).</para></summary>
        Slovenia,
        ///<summary><para>Svalbard and Jan Mayen (SJ).</para></summary>
        Svalbard_and_Jan_Mayen,
        ///<summary><para>Slovakia (SK).</para></summary>
        Slovakia,
        ///<summary><para>Sierra Leone (SL).</para></summary>
        Sierra_Leone,
        ///<summary><para>San Marino (SM).</para></summary>
        San_Marino,
        ///<summary><para>Senegal (SN).</para></summary>
        Senegal,
        ///<summary><para>Somalia (SO).</para></summary>
        Somalia,
        ///<summary><para>Suriname (SR).</para></summary>
        Suriname,
        ///<summary><para>South Sudan (SS).</para></summary>
        South_Sudan,
        ///<summary><para>Sao Tome and Principe (ST).</para></summary>
        Sao_Tome_and_Principe,
        ///<summary><para>El Salvador (SV).</para></summary>
        El_Salvador,
        ///<summary><para>Sint Maarten (SX).</para></summary>
        Sint_Maarten,
        ///<summary><para>Syria (SY).</para></summary>
        Syria,
        ///<summary><para>Swaziland (SZ).</para></summary>
        Swaziland,
        ///<summary><para>Turks and Caicos Islands (TC).</para></summary>
        Turks_and_Caicos_Islands,
        ///<summary><para>Chad (TD).</para></summary>
        Chad,
        ///<summary><para>French Southern and Antarctic Lands (TF).</para></summary>
        French_Southern_and_Antarctic_Lands,
        ///<summary><para>Togo (TG).</para></summary>
        Togo,
        ///<summary><para>Thailand (TH).</para></summary>
        Thailand,
        ///<summary><para>Tajikistan (TJ).</para></summary>
        Tajikistan,
        ///<summary><para>Tokelau (TK).</para></summary>
        Tokelau,
        ///<summary><para>East Timor (TL).</para></summary>
        East_Timor,
        ///<summary><para>Turkmenistan (TM).</para></summary>
        Turkmenistan,
        ///<summary><para>Tunisia (TN).</para></summary>
        Tunisia,
        ///<summary><para>Tonga (TO).</para></summary>
        Tonga,
        ///<summary><para>Turkey (TR).</para></summary>
        Turkey,
        ///<summary><para>Trinidad and Tobago (TT).</para></summary>
        Trinidad_and_Tobago,
        ///<summary><para>Tuvalu (TV).</para></summary>
        Tuvalu,
        ///<summary><para>Taiwan (TW).</para></summary>
        Taiwan,
        ///<summary><para>Tanzania (TZ).</para></summary>
        Tanzania,
        ///<summary><para>Ukraine (UA).</para></summary>
        Ukraine,
        ///<summary><para>Uganda (UG).</para></summary>
        Uganda,
        ///<summary><para>United States Minor Outlying Islands (UM).</para></summary>
        United_States_Minor_Outlying_Islands,
        ///<summary><para>United States (US).</para></summary>
        United_States,
        ///<summary><para>Uruguay (UY).</para></summary>
        Uruguay,
        ///<summary><para>Uzbekistan (UZ).</para></summary>
        Uzbekistan,
        ///<summary><para>Vatican City (VA).</para></summary>
        Vatican_City,
        ///<summary><para>Saint Vincent and the Grenadines (VC).</para></summary>
        Saint_Vincent_and_the_Grenadines,
        ///<summary><para>Venezuela (VE).</para></summary>
        Venezuela,
        ///<summary><para>British Virgin Islands (VG).</para></summary>
        British_Virgin_Islands,
        ///<summary><para>United States Virgin Islands (VI).</para></summary>
        United_States_Virgin_Islands,
        ///<summary><para>Vietnam (VN).</para></summary>
        Vietnam,
        ///<summary><para>Vanuatu (VU).</para></summary>
        Vanuatu,
        ///<summary><para>Wallis and Futuna (WF).</para></summary>
        Wallis_and_Futuna,
        ///<summary><para>Samoa (WS).</para></summary>
        Samoa,
        ///<summary><para>Yemen (YE).</para></summary>
        Yemen,
        ///<summary><para>Mayotte (YT).</para></summary>
        Mayotte,
        ///<summary><para>South Africa (ZA).</para></summary>
        South_Africa,
        ///<summary><para>Zambia (ZM).</para></summary>
        Zambia,
        ///<summary><para>Zimbabwe (ZW).</para></summary>
        Zimbabwe
    }

    internal partial class CountryInternal
    {
        public static KeyValuePair<string, CountryEnum> CodeCountry;

        public static List<string> GetAlternativeNames(CountryEnum country)
        {
            if (country == CountryEnum.Aland_Islands)
            {
                return new List<string>() { "Åland Islands" };
            }
            else if (country == CountryEnum.Bosnia_and_Herzegovina)
            {
                return new List<string>() { "Bosnia-Herzegovina", "Bosnia" };
            }
            else if (country == CountryEnum.Cocos_Keeling_Islands)
            {
                return new List<string>() { "Cocos Islands", "Keeling Islands" };
            }
            else if (country == CountryEnum.Congo_Republic)
            {
                return new List<string>() { "Congo" };
            }
            else if (country == CountryEnum.East_Timor)
            {
                return new List<string>() { "Timor-Leste" };
            }
            else if (country == CountryEnum.Faroe_Islands)
            {
                return new List<string>() { "Faeroes" };
            }
            else if (country == CountryEnum.French_Guiana)
            {
                return new List<string>() { "Guiana", "Guyane" };
            }
            else if (country == CountryEnum.Ivory_Coast)
            {
                return new List<string>() { "Cote d'Ivoire" };
            }
            else if (country == CountryEnum.Guinea_Bissau)
            {
                return new List<string>() { "Guinea-Bissau" };
            }
            else if (country == CountryEnum.Macau)
            {
                return new List<string>() { "Macao" };
            }
            else if (country == CountryEnum.Myanmar)
            {
                return new List<string>() { "Burma" };
            }
            else if (country == CountryEnum.Macedonia)
            {
                return new List<string>() { "Republic of Macedonia", "FYROM", "FYR Macedonia" };
            }
            else if (country == CountryEnum.Federated_States_of_Micronesia)
            {
                return new List<string>() { "Micronesia" };
            }
            else if (country == CountryEnum.Reunion)
            {
                return new List<string>() { "Réunion" };
            }
            else if (country == CountryEnum.Saint_Barthelemy)
            {
                return new List<string>() { "Saint Barthélemy" };
            }
            else if (country == CountryEnum.Sao_Tome_and_Principe)
            {
                return new List<string>() { "São Tomé and Príncipe" };
            }
            else if (country == CountryEnum.United_Kingdom)
            {
                return new List<string>() { "UK", "Great Britain" };
            }
            else if (country == CountryEnum.United_States)
            {
                return new List<string>() { "USA", "US" };
            }
            else if (country == CountryEnum.United_States_Minor_Outlying_Islands)
            {
                return new List<string>() { "US Minor Outlying Islands" };
            }
            else if (country == CountryEnum.United_States_Virgin_Islands)
            {
                return new List<string>() { "US Virgin Islands" };
            }
            else if (country == CountryEnum.Vatican_City)
            {
                return new List<string>() { "Vatican", "Holy See" };
            }

            return new List<string>() { };
        }

        private static void PopulateCodeCountry()
        {
            PopulateCodeCountry("", CountryEnum.None);
        }

        private static void PopulateCodeCountry(string code, CountryEnum country)
        {
            CodeCountry = new KeyValuePair<string, CountryEnum>(code, country);
        }

        public static void GetCodeCountry(CountryEnum country)
        {
            if (country == CountryEnum.None)
            {
                PopulateCodeCountry();
                return;
            }

            Dictionary<string, CountryEnum> dict = GetAllCodesCountries();

            var temp = dict.FirstOrDefault(x => x.Value == country);
            if (temp.Key != null)
            {
                PopulateCodeCountry(temp.Key, temp.Value);
            }
            else PopulateCodeCountry();
        }

        public static void GetCodeCountry(string codeOrCountryName)
        {
            string input2 = Common.PerformFirstStringChecks(codeOrCountryName);
            if (input2 == null)
            {
                PopulateCodeCountry();
                return;
            }

            Dictionary<string, CountryEnum> dict = GetAllCodesCountries();
            input2 = input2.ToUpper();

            if (dict.ContainsKey(input2))
            {
                PopulateCodeCountry(input2, dict[input2]);
            }
            else
            {
                AnalyseCountryName(input2.ToLower(), dict);
            }
        }

        private static List<string> GetCountryNames(CountryEnum country)
        {
            List<string> names = new List<string>() { country.ToString() };
            
            names.AddRange(GetAlternativeNames(country));

            return names;
        }

        private static void AnalyseCountryName(string input2, Dictionary<string, CountryEnum> dict)
        {
            string[] words2 = Common.GetWordsInString(input2);

            foreach (CountryEnum country in Enum.GetValues(typeof(CountryEnum)))
            {
                if (country == CountryEnum.None) continue;

                bool found = false;
                foreach (var name in GetCountryNames(country))
                {
                    found = NameMatchesWords(name, words2);
                    if (found) break;
                }

                if (found)
                {
                    PopulateCodeCountry
                    (
                        dict.First(x => x.Value == country).Key, country
                    );

                    return;
                }
            }

            PopulateCodeCountry();
        }

        private static bool NameMatchesWords(string name, string[] words2)
        {
            string name2 = name.ToLower();
            string[] nameWords = Common.GetWordsInString(name2);
            if (nameWords.Length != words2.Length) return false;

            return 
            (
                nameWords.Except(words2).Count() == 0
            );
        }

        //Due to the big amount of hardcoded information stored via global variables, minimising memory usage
        //is a top priority here. That's why some small and irrelevant enough collections are created locally
        //and only when strictly required; precisely the case of the dictionary storing all the codes and 
        //countries. On the other hand, this dataset is extensively used in some specific contexts and that's why
        //also enabling this secondary alternative (i.e., global variable whose resources are expected to be
        //released after the corresponding actions have finished).
        public static Dictionary<string, CountryEnum> CodeCountryTemp;
        public static void PopulateGlobalTemp()
        {
            CodeCountryTemp = GetAllCodesCountries();
        }

        private static Dictionary<string, CountryEnum> GetAllCodesCountries()
        {
            //Recognised as a country, but still with no official code.

            return new Dictionary<string, CountryEnum>()
            {
                { "", CountryEnum.Kosovo },
                { "AD", CountryEnum.Andorra }, { "AE", CountryEnum.United_Arab_Emirates }, { "AF", CountryEnum.Afghanistan }, 
                { "AG", CountryEnum.Antigua_and_Barbuda }, { "AI", CountryEnum.Anguilla }, { "AL", CountryEnum.Albania },
                { "AM", CountryEnum.Armenia }, { "AO", CountryEnum.Angola }, { "AQ", CountryEnum.Antarctica },
                { "AR", CountryEnum.Argentina }, { "AS", CountryEnum.American_Samoa }, { "AT", CountryEnum.Austria },
                { "AU", CountryEnum.Australia }, { "AW", CountryEnum.Aruba }, { "AX", CountryEnum.Aland_Islands },
                { "AZ", CountryEnum.Azerbaijan }, { "BA", CountryEnum.Bosnia_and_Herzegovina }, { "BB", CountryEnum.Barbados },
                { "BD", CountryEnum.Bangladesh }, { "BE", CountryEnum.Belgium }, { "BF", CountryEnum.Burkina_Faso },
                { "BG", CountryEnum.Bulgaria }, { "BH", CountryEnum.Bahrain }, { "BI", CountryEnum.Burundi }, 
                { "BJ", CountryEnum.Benin }, { "BL", CountryEnum.Saint_Barthelemy }, { "BM", CountryEnum.Bermuda },
                { "BN", CountryEnum.Brunei }, { "BO", CountryEnum.Bolivia }, { "BQ", CountryEnum.Caribbean_Netherlands },
                { "BR", CountryEnum.Brazil }, { "BS", CountryEnum.Bahamas }, { "BT", CountryEnum.Bhutan },
                { "BV", CountryEnum.Bouvet_Island }, { "BW", CountryEnum.Botswana }, { "BY", CountryEnum.Belarus }, 
                { "BZ", CountryEnum.Belize }, { "CA", CountryEnum.Canada }, { "CC", CountryEnum.Cocos_Keeling_Islands },
                { "CD", CountryEnum.Democratic_Republic_of_the_Congo },  { "CF", CountryEnum.Central_African_Republic },
                { "CG", CountryEnum.Congo_Republic }, { "CH", CountryEnum.Switzerland }, { "CI", CountryEnum.Ivory_Coast },
                { "CK", CountryEnum.Cook_Islands },  { "CL", CountryEnum.Chile }, { "CM", CountryEnum.Cameroon },
                { "CN", CountryEnum.China }, { "CO", CountryEnum.Colombia }, { "CR", CountryEnum.Costa_Rica },
                { "CU", CountryEnum.Cuba }, { "CV", CountryEnum.Cape_Verde }, { "CW", CountryEnum.Curaçao }, 
                { "CX", CountryEnum.Christmas_Island }, { "CY", CountryEnum.Cyprus }, { "CZ", CountryEnum.Czech_Republic },
                { "DE", CountryEnum.Germany }, { "DJ", CountryEnum.Djibouti }, { "DK", CountryEnum.Denmark }, 
                { "DM", CountryEnum.Dominica }, { "DO", CountryEnum.Dominican_Republic }, { "DZ", CountryEnum.Algeria },
                { "EC", CountryEnum.Ecuador }, { "EE", CountryEnum.Estonia }, { "EG", CountryEnum.Egypt }, 
                { "EH", CountryEnum.Western_Sahara }, { "ER", CountryEnum.Eritrea }, { "ES", CountryEnum.Spain },
                { "ET", CountryEnum.Ethiopia }, { "FI", CountryEnum.Finland }, { "FJ", CountryEnum.Fiji }, 
                { "FK", CountryEnum.Falkland_Islands }, { "FM", CountryEnum.Federated_States_of_Micronesia }, 
                { "FO", CountryEnum.Faroe_Islands }, { "FR", CountryEnum.France }, { "GA", CountryEnum.Gabon }, 
                { "GB", CountryEnum.United_Kingdom }, { "GD", CountryEnum.Grenada }, { "GE", CountryEnum.Georgia },
                { "GF", CountryEnum.French_Guiana },  { "GG", CountryEnum.Guernsey }, { "GH", CountryEnum.Ghana },
                { "GI", CountryEnum.Gibraltar }, { "GL", CountryEnum.Greenland }, { "GM", CountryEnum.Gambia },
                { "GN", CountryEnum.Guinea }, { "GP", CountryEnum.Guadeloupe }, { "GQ", CountryEnum.Equatorial_Guinea },
                { "GR", CountryEnum.Greece }, { "GS", CountryEnum.South_Georgia_and_the_South_Sandwich_Islands },
                { "GT", CountryEnum.Guatemala }, { "GU", CountryEnum.Guam }, { "GW", CountryEnum.Guinea_Bissau },
                { "GY", CountryEnum.Guyana }, { "HK", CountryEnum.Hong_Kong }, { "HM", CountryEnum.Heard_Island_and_McDonald_Islands },
                { "HN", CountryEnum.Honduras }, { "HR", CountryEnum.Croatia }, { "HT", CountryEnum.Haiti }, 
                { "HU", CountryEnum.Hungary }, { "ID", CountryEnum.Indonesia }, { "IE", CountryEnum.Ireland },
                { "IL", CountryEnum.Israel }, { "IM", CountryEnum.Isle_of_Man }, { "IN", CountryEnum.India },
                { "IO", CountryEnum.British_Indian_Ocean_Territory }, { "IQ", CountryEnum.Iraq }, { "IR", CountryEnum.Iran },
                { "IS", CountryEnum.Iceland }, { "IT", CountryEnum.Italy }, { "JE", CountryEnum.Jersey }, { "JM", CountryEnum.Jamaica },
                { "JO", CountryEnum.Jordan }, { "JP", CountryEnum.Japan }, { "KE", CountryEnum.Kenya }, { "KG", CountryEnum.Kyrgyzstan },
                { "KH", CountryEnum.Cambodia }, { "KI", CountryEnum.Kiribati }, { "KM", CountryEnum.Comoros }, 
                { "KN", CountryEnum.Saint_Kitts_and_Nevis }, { "KP", CountryEnum.North_Korea }, { "KR", CountryEnum.South_Korea },
                { "KW", CountryEnum.Kuwait }, { "KY", CountryEnum.Cayman_Islands }, { "KZ", CountryEnum.Kazakhstan },
                { "LA", CountryEnum.Laos }, { "LB", CountryEnum.Lebanon }, { "LC", CountryEnum.Saint_Lucia }, 
                { "LI", CountryEnum.Liechtenstein }, { "LK", CountryEnum.Sri_Lanka }, { "LR", CountryEnum.Liberia },
                { "LS", CountryEnum.Lesotho }, { "LT", CountryEnum.Lithuania }, { "LU", CountryEnum.Luxembourg }, 
                { "LV", CountryEnum.Latvia }, { "LY", CountryEnum.Libya }, { "MA", CountryEnum.Morocco }, { "MC", CountryEnum.Monaco },
                { "MD", CountryEnum.Moldova }, { "ME", CountryEnum.Montenegro }, { "MF", CountryEnum.Saint_Martin },
                { "MG", CountryEnum.Madagascar }, { "MH", CountryEnum.Marshall_Islands }, { "MK", CountryEnum.Macedonia },
                { "ML", CountryEnum.Mali }, { "MM", CountryEnum.Myanmar }, { "MN", CountryEnum.Mongolia}, { "MO", CountryEnum.Macau },
                { "MP", CountryEnum.Northern_Mariana_Islands }, { "MQ", CountryEnum.Martinique }, { "MR", CountryEnum.Mauritania },
                { "MS", CountryEnum.Montserrat }, { "MT", CountryEnum.Malta }, { "MU", CountryEnum.Mauritius },
                { "MV", CountryEnum.Maldives }, { "MW", CountryEnum.Malawi }, { "MX", CountryEnum.Mexico }, { "MY", CountryEnum.Malaysia },
                { "MZ", CountryEnum.Mozambique }, { "NA", CountryEnum.Namibia }, { "NC", CountryEnum.New_Caledonia }, 
                { "NE", CountryEnum.Niger }, { "NF", CountryEnum.Norfolk_Island }, { "NG", CountryEnum.Nigeria }, 
                { "NI", CountryEnum.Nicaragua }, { "NL", CountryEnum.Netherlands }, { "NO", CountryEnum.Norway }, { "NP", CountryEnum.Nepal },
                { "NR", CountryEnum.Nauru }, { "NU", CountryEnum.Niue }, { "NZ", CountryEnum.New_Zealand }, { "OM", CountryEnum.Oman },
                { "PA", CountryEnum.Panama }, { "PE", CountryEnum.Peru }, { "PF", CountryEnum.French_Polynesia }, 
                { "PG", CountryEnum.Papua_New_Guinea }, { "PH", CountryEnum.Philippines }, { "PK", CountryEnum.Pakistan },
                { "PL", CountryEnum.Poland }, { "PM", CountryEnum.Saint_Pierre_and_Miquelon }, { "PN", CountryEnum.Pitcairn_Islands },
                { "PR", CountryEnum.Puerto_Rico }, { "PS", CountryEnum.Palestine }, { "PT", CountryEnum.Portugal }, { "PW", CountryEnum.Palau },
                { "PY", CountryEnum.Paraguay }, { "QA", CountryEnum.Qatar }, { "RE", CountryEnum.Reunion }, { "RO", CountryEnum.Romania },
                { "RS", CountryEnum.Serbia }, { "RU", CountryEnum.Russia }, { "RW", CountryEnum.Rwanda }, { "SA", CountryEnum.Saudi_Arabia },
                { "SB", CountryEnum.Solomon_Islands }, { "SC", CountryEnum.Seychelles }, { "SD", CountryEnum.Sudan }, 
                { "SE", CountryEnum.Sweden }, { "SG", CountryEnum.Singapore }, { "SH", CountryEnum.Saint_Helena }, { "SI", CountryEnum.Slovenia },
                { "SJ", CountryEnum.Svalbard_and_Jan_Mayen }, { "SK", CountryEnum.Slovakia }, { "SL", CountryEnum.Sierra_Leone },
                { "SM", CountryEnum.San_Marino }, { "SN", CountryEnum.Senegal }, { "SO", CountryEnum.Somalia }, { "SR", CountryEnum.Suriname },
                { "SS", CountryEnum.South_Sudan }, { "ST", CountryEnum.Sao_Tome_and_Principe }, { "SV", CountryEnum.El_Salvador },
                { "SX", CountryEnum.Sint_Maarten }, { "SY", CountryEnum.Syria }, { "SZ", CountryEnum.Swaziland }, 
                { "TC", CountryEnum.Turks_and_Caicos_Islands }, { "TD", CountryEnum.Chad }, { "TF", CountryEnum.French_Southern_and_Antarctic_Lands },
                { "TG", CountryEnum.Togo }, { "TH", CountryEnum.Thailand }, { "TJ", CountryEnum.Tajikistan }, { "TK", CountryEnum.Tokelau },
                { "TL", CountryEnum.East_Timor }, { "TM", CountryEnum.Turkmenistan }, { "TN", CountryEnum.Tunisia }, { "TO", CountryEnum.Tonga },
                { "TR", CountryEnum.Turkey }, { "TT", CountryEnum.Trinidad_and_Tobago }, { "TV", CountryEnum.Tuvalu }, { "TW", CountryEnum.Taiwan },
                { "TZ", CountryEnum.Tanzania }, { "UA", CountryEnum.Ukraine }, { "UG", CountryEnum.Uganda }, 
                { "UM", CountryEnum.United_States_Minor_Outlying_Islands }, { "US", CountryEnum.United_States }, { "UY", CountryEnum.Uruguay },
                { "UZ", CountryEnum.Uzbekistan }, { "VA", CountryEnum.Vatican_City }, { "VC", CountryEnum.Saint_Vincent_and_the_Grenadines },
                { "VE", CountryEnum.Venezuela }, { "VG", CountryEnum.British_Virgin_Islands }, { "VI", CountryEnum.United_States_Virgin_Islands },
                { "VN", CountryEnum.Vietnam }, { "VU", CountryEnum.Vanuatu }, { "WF", CountryEnum.Wallis_and_Futuna }, { "WS", CountryEnum.Samoa },
                { "YE", CountryEnum.Yemen }, { "YT", CountryEnum.Mayotte }, { "ZA", CountryEnum.South_Africa }, { "ZM", CountryEnum.Zambia},
                { "ZW", CountryEnum.Zimbabwe }
            };
        }
    }
}
