using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    public enum CountryEnum
    {
        None = 0,
        Andorra, United_Arab_Emirates, Afghanistan, Antigua_and_Barbuda, Anguilla,
        Albania, Armenia, Angola, Antarctica, Argentina, American_Samoa, Austria,
        Australia, Aruba, Aland_Islands, Azerbaijan, Bosnia_and_Herzegovina,
        Barbados, Bangladesh, Belgium, Burkina_Faso, Bulgaria, Bahrain, Burundi,
        Benin, Saint_Barthélemy, Bermuda, Brunei, Bolivia, Caribbean_Netherlands,
        Brazil, Bahamas, Bhutan, Bouvet_Island, Botswana, Belarus, Belize, Canada,
        Cocos_Keeling_Islands, Democratic_Republic_of_the_Congo, Central_African_Republic,
        Congo_Republic, Switzerland, Ivory_Coast, Cook_Islands, Chile, Cameroon,
        China, Colombia, Costa_Rica, Cuba, Cape_Verde, Curaçao, Christmas_Island,
        Cyprus, Czech_Republic, Germany, Djibouti, Denmark, Dominica, Dominican_Republic,
        Algeria, Ecuador, Estonia, Egypt, Western_Sahara, Eritrea, Spain, Ethiopia,
        Finland, Fiji, Falkland_Islands, Federated_States_Of_Micronesia, Faroe_Islands,
        France, Gabon, United_Kingdom, Grenada, Georgia, French_Guiana, Guernsey, Ghana,
        Gibraltar, Greenland, Gambia, Guinea, Guadeloupe, Equatorial_Guinea, Greece,
        South_Georgia_and_the_South_Sandwich_Islands, Guatemala, Guam, Guinea_Bissau,
        Guyana, Hong_Kong, Heard_Island_And_McDonald_Islands, Honduras, Croatia,
        Haiti, Hungary, Indonesia, Ireland, Israel, Isle_of_Man, India, Kosovo,
        British_Indian_Ocean_Territory, Iraq, Iran, Iceland, Italy, Jersey, Jamaica,
        Jordan, Japan, Kenya, Kyrgyzstan, Cambodia, Kiribati, Comoros, Saint_Kitts_and_Nevis,
        North_Korea, South_Korea, Kuwait, Cayman_Islands, Kazakhstan, Laos, Lebanon,
        Saint_Lucia, Liechtenstein, Sri_Lanka, Liberia, Lesotho, Lithuania, Luxembourg,
        Latvia, Libya, Morocco, Monaco, Moldova, Montenegro, Saint_Martin, Madagascar,
        Marshall_Islands, Macedonia, Mali, Myanmar, Mongolia, Macau, Northern_Mariana_Islands,
        Martinique, Mauritania, Montserrat, Malta, Mauritius, Maldives, Malawi, Mexico,
        Malaysia, Mozambique, Namibia, New_Caledonia, Niger, Norfolk_Island, Nigeria,
        Nicaragua, Netherlands, Norway, Nepal, Nauru, Niue, New_Zealand, Oman, Panama,
        Peru, French_Polynesia, Papua_New_Guinea, Philippines, Pakistan, Poland,
        Saint_Pierre_and_Miquelon, Pitcairn_Islands, Puerto_Rico, Palestine, Portugal,
        Palau, Paraguay, Qatar, Reunion, Romania, Serbia, Russia, Rwanda, Saudi_Arabia,
        Solomon_Islands, Seychelles, Sudan, Sweden, Singapore, Saint_Helena, Slovenia,
        Svalbard_and_Jan_Mayen, Slovakia, Sierra_Leone, San_Marino, Senegal, Somalia,
        Suriname, South_Sudan, Sao_Tome_and_Principe, El_Salvador, Sint_Maarten, Syria,
        Swaziland, Turks_and_Caicos_Islands, Chad, French_Southern_and_Antarctic_Lands,
        Togo, Thailand, Tajikistan, Tokelau, East_Timor, Turkmenistan, Tunisia, Tonga,
        Turkey, Trinidad_and_Tobago, Tuvalu, Taiwan, Tanzania, Ukraine, Uganda,
        United_States_Minor_Outlying_Islands, United_States, Uruguay, Uzbekistan,
        Vatican_City, Saint_Vincent_and_the_Grenadines, Venezuela, British_Virgin_Islands,
        United_States_Virgin_Islands, Vietnam, Vanuatu, Wallis_and_Futuna, Samoa, Yemen,
        Mayotte, South_Africa, Zambia, Zimbabwe
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
            else if (country == CountryEnum.Federated_States_Of_Micronesia)
            {
                return new List<string>() { "Micronesia" };
            }
            else if (country == CountryEnum.Reunion)
            {
                return new List<string>() { "Réunion" };
            }
            else if (country == CountryEnum.Saint_Barthélemy)
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

            int commonCount = nameWords.Intersect(words2).Count();
            return (commonCount == words2.Length);  
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
                { "BJ", CountryEnum.Benin }, { "BL", CountryEnum.Saint_Barthélemy }, { "BM", CountryEnum.Bermuda },
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
                { "FK", CountryEnum.Falkland_Islands }, { "FM", CountryEnum.Federated_States_Of_Micronesia }, 
                { "FO", CountryEnum.Faroe_Islands }, { "FR", CountryEnum.France }, { "GA", CountryEnum.Gabon }, 
                { "GB", CountryEnum.United_Kingdom }, { "GD", CountryEnum.Grenada }, { "GE", CountryEnum.Georgia },
                { "GF", CountryEnum.French_Guiana },  { "GG", CountryEnum.Guernsey }, { "GH", CountryEnum.Ghana },
                { "GI", CountryEnum.Gibraltar }, { "GL", CountryEnum.Greenland }, { "GM", CountryEnum.Gambia },
                { "GN", CountryEnum.Guinea }, { "GP", CountryEnum.Guadeloupe }, { "GQ", CountryEnum.Equatorial_Guinea },
                { "GR", CountryEnum.Greece }, { "GS", CountryEnum.South_Georgia_and_the_South_Sandwich_Islands },
                { "GT", CountryEnum.Guatemala }, { "GU", CountryEnum.Guam }, { "GW", CountryEnum.Guinea_Bissau },
                { "GY", CountryEnum.Guyana }, { "HK", CountryEnum.Hong_Kong }, { "HM", CountryEnum.Heard_Island_And_McDonald_Islands },
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
