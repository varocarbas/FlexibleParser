using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    internal partial class TimeZonesCountryInternal
    {
        internal static Dictionary<CountryEnum, KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>[]> CountryOfficials;
        private static Dictionary<CountryEnum, List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>> CountryOfficials_Temp;

        //Method populating the TimeZoneOfficialCountries dictionary by mostly relying on the information listed in
        //https://www.timeanddate.com/.
        //NOTE: some of the members of the TimeZoneOfficialEnum refer to obsolete or currently-not-used-in-any-country timezones.
        //That's why the associated dictionary doesn't account for all the items of the aforementioned enum.

        public static void PopulateMainDictionary()
        {
            CountryOfficials_Temp = new Dictionary<CountryEnum, List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>>();
            
            CountryEnum[] countries = new CountryEnum[] 
            { 
                CountryEnum.Australia
            };

            List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>> officials = new List<KeyValuePair<TimeZoneOfficial,TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Australian_Central_Standard_Time, TimeZoneOfficialEnum.Australian_Central_Daylight_Savings_Time
                ),
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Australian_Central_Standard_Time, TimeZoneOfficialEnum.Australian_Central_Standard_Time
                ),
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Australian_Central_Western_Standard_Time, TimeZoneOfficialEnum.Australian_Central_Western_Standard_Time
                ),
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Australian_Eastern_Standard_Time, TimeZoneOfficialEnum.Australian_Eastern_Daylight_Time
                ),
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Australian_Eastern_Standard_Time, TimeZoneOfficialEnum.Australian_Eastern_Standard_Time
                ),
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Australian_Western_Standard_Time, TimeZoneOfficialEnum.Australian_Western_Standard_Time
                ),
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Lord_Howe_Standard_Time, TimeZoneOfficialEnum.Lord_Howe_Summer_Time
                ),
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Norfolk_Time, TimeZoneOfficialEnum.Norfolk_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Brazil
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Acre_Time, TimeZoneOfficialEnum.Acre_Time
                ),
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Amazon_Time_Brazil, TimeZoneOfficialEnum.Amazon_Summer_Time_Brazil
                ),
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Amazon_Time_Brazil, TimeZoneOfficialEnum.Amazon_Time_Brazil
                ),
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Brasilia_Time, TimeZoneOfficialEnum.Brasilia_Summer_Time
                ),
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Brasilia_Time, TimeZoneOfficialEnum.Brasilia_Time
                ),
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Fernando_de_Noronha_Time, TimeZoneOfficialEnum.Fernando_de_Noronha_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.United_States
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Alaska_Standard_Time, TimeZoneOfficialEnum.Alaska_Daylight_Time
                ),
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Hawaii_Aleutian_Standard_Time, TimeZoneOfficialEnum.Hawaii_Aleutian_Daylight_Time
                ),
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Wake_Time, TimeZoneOfficialEnum.Wake_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Russia
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Krasnoyarsk_Time, TimeZoneOfficialEnum.Krasnoyarsk_Time
                ),
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Magadan_Time, TimeZoneOfficialEnum.Magadan_Time
                ),
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Omsk_Standard_Time, TimeZoneOfficialEnum.Omsk_Standard_Time
                ),
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Kamchatka_Time, TimeZoneOfficialEnum.Kamchatka_Time
                ),
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Sakhalin_Island_Time, TimeZoneOfficialEnum.Sakhalin_Island_Time
                ),
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Samara_Time, TimeZoneOfficialEnum.Samara_Time
                ),
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Srednekolymsk_Time, TimeZoneOfficialEnum.Srednekolymsk_Time
                ),
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Vladivostok_Time, TimeZoneOfficialEnum.Vladivostok_Time
                ),
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Yakutsk_Time, TimeZoneOfficialEnum.Yakutsk_Time
                ),
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Yekaterinburg_Time, TimeZoneOfficialEnum.Yekaterinburg_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Antarctica
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Davis_Time, TimeZoneOfficialEnum.Davis_Time
                ),
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Dumont_DUrville_Time, TimeZoneOfficialEnum.Dumont_DUrville_Time
                ),
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Mawson_Time, TimeZoneOfficialEnum.Mawson_Time
                ),
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Vostok_Station_Time, TimeZoneOfficialEnum.Vostok_Station_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Federated_States_Of_Micronesia
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Chuuk_Time, TimeZoneOfficialEnum.Chuuk_Time
                ),
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Kosrae_Time, TimeZoneOfficialEnum.Kosrae_Time
                ),
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Pohnpei_Standard_Time, TimeZoneOfficialEnum.Pohnpei_Standard_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Mongolia
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Choibalsan_Standard_Time, TimeZoneOfficialEnum.Choibalsan_Summer_Time
                ),
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Choibalsan_Standard_Time, TimeZoneOfficialEnum.Choibalsan_Standard_Time
                ),
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Ulaanbaatar_Standard_Time, TimeZoneOfficialEnum.Ulaanbaatar_Summer_Time
                ),
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Ulaanbaatar_Standard_Time, TimeZoneOfficialEnum.Ulaanbaatar_Standard_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Albania, CountryEnum.Andorra, CountryEnum.Austria, CountryEnum.Belgium,
                CountryEnum.Bosnia_and_Herzegovina, CountryEnum.Croatia, CountryEnum.Czech_Republic,
                CountryEnum.Denmark, CountryEnum.France, CountryEnum.Germany, CountryEnum.Gibraltar,
                CountryEnum.Vatican_City, CountryEnum.Hungary, CountryEnum.Italy, CountryEnum.Kosovo,
                CountryEnum.Liechtenstein, CountryEnum.Luxembourg, CountryEnum.Macedonia, CountryEnum.Malta,
                CountryEnum.Monaco, CountryEnum.Montenegro, CountryEnum.Netherlands, CountryEnum.Norway,
                CountryEnum.Poland, CountryEnum.San_Marino, CountryEnum.Serbia, CountryEnum.Slovakia,
                CountryEnum.Slovenia, CountryEnum.Spain, CountryEnum.Sweden, CountryEnum.Switzerland
            };

            officials = new List<KeyValuePair<TimeZoneOfficial,TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Central_European_Time, TimeZoneOfficialEnum.Central_European_Summer_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Botswana, CountryEnum.Burundi, CountryEnum.Democratic_Republic_of_the_Congo,
                CountryEnum.Malawi, CountryEnum.Mozambique, CountryEnum.Rwanda, CountryEnum.Zambia,
                CountryEnum.Zimbabwe
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Central_Africa_Time, TimeZoneOfficialEnum.Central_Africa_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Bahamas, CountryEnum.Canada, CountryEnum.Haiti, CountryEnum.United_States 
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Eastern_Standard_Time, TimeZoneOfficialEnum.Eastern_Daylight_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Cayman_Islands, CountryEnum.Jamaica, CountryEnum.Mexico, CountryEnum.Panama, 
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Eastern_Standard_Time, TimeZoneOfficialEnum.Eastern_Standard_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Canada 
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Atlantic_Standard_Time, TimeZoneOfficialEnum.Atlantic_Daylight_Time
                ),
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Atlantic_Standard_Time, TimeZoneOfficialEnum.Atlantic_Standard_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Bermuda, CountryEnum.Greenland 
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Atlantic_Standard_Time, TimeZoneOfficialEnum.Atlantic_Daylight_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Anguilla, CountryEnum.Antigua_and_Barbuda, CountryEnum.Aruba,
                CountryEnum.Barbados, CountryEnum.British_Virgin_Islands, CountryEnum.Caribbean_Netherlands,
                CountryEnum.Curaçao, CountryEnum.Dominica, CountryEnum.Dominican_Republic,
                CountryEnum.Grenada, CountryEnum.Guadeloupe, CountryEnum.Martinique, CountryEnum.Montserrat,
                CountryEnum.Puerto_Rico, CountryEnum.Saint_Barthelemy, CountryEnum.Saint_Kitts_and_Nevis,
                CountryEnum.Saint_Lucia, CountryEnum.Saint_Martin, CountryEnum.Saint_Vincent_and_the_Grenadines,
                CountryEnum.Sint_Maarten, CountryEnum.Trinidad_and_Tobago, CountryEnum.Turks_and_Caicos_Islands,
                CountryEnum.United_States_Virgin_Islands
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Atlantic_Standard_Time, TimeZoneOfficialEnum.Atlantic_Standard_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Djibouti, CountryEnum.Eritrea, CountryEnum.Ethiopia,
                CountryEnum.Kenya, CountryEnum.Madagascar, CountryEnum.Somalia,
                CountryEnum.South_Africa, CountryEnum.South_Sudan, CountryEnum.Sudan,
                CountryEnum.Tanzania, CountryEnum.Uganda
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.East_Africa_Time, TimeZoneOfficialEnum.East_Africa_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Guernsey, CountryEnum.Isle_of_Man, CountryEnum.Jersey, CountryEnum.United_Kingdom 
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Greenwich_Mean_Time, TimeZoneOfficialEnum.Greenwich_Mean_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Greenland, CountryEnum.Iceland, CountryEnum.Burkina_Faso, 
                CountryEnum.Ivory_Coast, CountryEnum.Gambia, CountryEnum.Ghana, 
                CountryEnum.Guinea, CountryEnum.Guinea_Bissau, CountryEnum.Liberia, 
                CountryEnum.Mali, CountryEnum.Mauritania, CountryEnum.Saint_Helena,
                CountryEnum.Sao_Tome_and_Principe, CountryEnum.Senegal, 
                CountryEnum.Sierra_Leone, CountryEnum.Togo, CountryEnum.Antarctica
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Greenwich_Mean_Time, TimeZoneOfficialEnum.Greenwich_Mean_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Bulgaria, CountryEnum.Estonia, CountryEnum.Finland, CountryEnum.Greece, 
                CountryEnum.Latvia, CountryEnum.Lithuania, CountryEnum.Moldova, CountryEnum.Romania, 
                CountryEnum.Ukraine, CountryEnum.Aland_Islands, CountryEnum.Cyprus, CountryEnum.Palestine,
                CountryEnum.Lebanon, CountryEnum.Syria
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Eastern_European_Time, TimeZoneOfficialEnum.Eastern_European_Summer_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Guernsey, CountryEnum.Isle_of_Man, CountryEnum.Jersey, CountryEnum.United_Kingdom 
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Greenwich_Mean_Time, TimeZoneOfficialEnum.British_Summer_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Bahrain, CountryEnum.Iraq, CountryEnum.Kuwait, CountryEnum.Qatar, 
                CountryEnum.Saudi_Arabia, CountryEnum.Yemen, CountryEnum.Jordan
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Arabia_Standard_Time, TimeZoneOfficialEnum.Arabia_Standard_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Canada, CountryEnum.Mexico, CountryEnum.United_States 
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Central_Standard_Time, TimeZoneOfficialEnum.Central_Standard_Time
                ),
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Mountain_Standard_Time, TimeZoneOfficialEnum.Mountain_Standard_Time
                ),
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Pacific_Standard_Time, TimeZoneOfficialEnum.Pacific_Standard_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Russia, CountryEnum.Egypt, CountryEnum.Libya 
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Eastern_European_Time, TimeZoneOfficialEnum.Eastern_European_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Portugal
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Azores_Standard_Time, TimeZoneOfficialEnum.Azores_Summer_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Ireland
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Greenwich_Mean_Time, TimeZoneOfficialEnum.Irish_Standard_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Angola, CountryEnum.Benin,
                CountryEnum.Cameroon, CountryEnum.Central_African_Republic,
                CountryEnum.Chad, CountryEnum.Congo_Republic, 
                CountryEnum.Democratic_Republic_of_the_Congo, CountryEnum.Equatorial_Guinea,
                CountryEnum.Gabon, CountryEnum.Niger, CountryEnum.Nigeria
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.West_Africa_Time, TimeZoneOfficialEnum.West_Africa_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Namibia
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.West_Africa_Time, TimeZoneOfficialEnum.West_Africa_Summer_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Spain, CountryEnum.Faroe_Islands, CountryEnum.Portugal,
                CountryEnum.Morocco, CountryEnum.Western_Sahara
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Western_European_Time, TimeZoneOfficialEnum.Western_European_Summer_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Antarctica, CountryEnum.Argentina
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Argentina_Time, TimeZoneOfficialEnum.Argentina_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Cuba
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Cuba_Standard_Time, TimeZoneOfficialEnum.Cuba_Daylight_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.New_Zealand
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Chatham_Standard_Time, TimeZoneOfficialEnum.Chatham_Daylight_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Antarctica, CountryEnum.New_Zealand
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.New_Zealand_Standard_Time, TimeZoneOfficialEnum.New_Zealand_Daylight_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Guam, CountryEnum.Northern_Mariana_Islands
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Chamorro_Standard_Time, TimeZoneOfficialEnum.Chamorro_Standard_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Chile
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Chile_Standard_Time, TimeZoneOfficialEnum.Chile_Summer_Time
                ),
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Easter_Island_Standard_Time, TimeZoneOfficialEnum.Easter_Island_Summer_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Greenland
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Eastern_Greenland_Time, TimeZoneOfficialEnum.Eastern_Greenland_Summer_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Falkland_Islands
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Falkland_Islands_Time, TimeZoneOfficialEnum.Falkland_Islands_Summer_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Oman, CountryEnum.United_Arab_Emirates
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Gulf_Standard_Time, TimeZoneOfficialEnum.Gulf_Standard_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Israel
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Israel_Standard_Time, TimeZoneOfficialEnum.Israel_Daylight_Time
                )
            };

            AddToDictionary(countries, officials);
            

            countries = new CountryEnum[] 
            { 
                CountryEnum.Iran
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Iran_Standard_Time, TimeZoneOfficialEnum.Iran_Daylight_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Canada
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Newfoundland_Standard_Time, TimeZoneOfficialEnum.Newfoundland_Daylight_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Saint_Pierre_and_Miquelon
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Saint_Pierre_And_Miquelon_Standard_Time, TimeZoneOfficialEnum.Saint_Pierre_And_Miquelon_Daylight_Time
                )
            };

            AddToDictionary(countries, officials);


            countries = new CountryEnum[] 
            { 
                CountryEnum.Paraguay
            };

            officials = new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
            {
                new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>
                (
                    TimeZoneOfficialEnum.Paraguay_Time, TimeZoneOfficialEnum.Paraguay_Summer_Time
                )
            };

            AddToDictionary(countries, officials);

            AddToDictionary(CountryEnum.Afghanistan, TimeZoneOfficialEnum.Afghanistan_Time);
            AddToDictionary(CountryEnum.Armenia, TimeZoneOfficialEnum.Armenia_Time);
            AddToDictionary(CountryEnum.Brunei, TimeZoneOfficialEnum.Brunei_Darussalam_Time);
            AddToDictionary(CountryEnum.British_Indian_Ocean_Territory, TimeZoneOfficialEnum.Indian_Chagos_Time);
            AddToDictionary(CountryEnum.Bolivia, TimeZoneOfficialEnum.Bolivia_Time);
            AddToDictionary(CountryEnum.Bangladesh, TimeZoneOfficialEnum.Bangladesh_Standard_Time);
            AddToDictionary(CountryEnum.Papua_New_Guinea, TimeZoneOfficialEnum.Bougainville_Standard_Time);
            AddToDictionary(CountryEnum.Papua_New_Guinea, TimeZoneOfficialEnum.Papua_New_Guinea_Time);
            AddToDictionary(CountryEnum.Bhutan, TimeZoneOfficialEnum.Bhutan_Time);
            AddToDictionary(CountryEnum.Cocos_Keeling_Islands, TimeZoneOfficialEnum.Cocos_Islands_Time);
            AddToDictionary(CountryEnum.Indonesia, TimeZoneOfficialEnum.Central_Indonesia_Time);
            AddToDictionary(CountryEnum.Indonesia, TimeZoneOfficialEnum.Eastern_Indonesian_Time);
            AddToDictionary(CountryEnum.Indonesia, TimeZoneOfficialEnum.Western_Indonesian_Time);
            AddToDictionary(CountryEnum.Cook_Islands, TimeZoneOfficialEnum.Cook_Island_Time);
            AddToDictionary(CountryEnum.Antarctica, TimeZoneOfficialEnum.Chile_Summer_Time);
            AddToDictionary(CountryEnum.Colombia, TimeZoneOfficialEnum.Colombia_Time);
            AddToDictionary(CountryEnum.China, TimeZoneOfficialEnum.China_Standard_Time);
            AddToDictionary(CountryEnum.Macau, TimeZoneOfficialEnum.China_Standard_Time);
            AddToDictionary(CountryEnum.Taiwan, TimeZoneOfficialEnum.China_Standard_Time);
            AddToDictionary(CountryEnum.Cape_Verde, TimeZoneOfficialEnum.Cape_Verde_Time);
            AddToDictionary(CountryEnum.Christmas_Island, TimeZoneOfficialEnum.Christmas_Island_Time);
            AddToDictionary(CountryEnum.Ecuador, TimeZoneOfficialEnum.Ecuador_Time);
            AddToDictionary(CountryEnum.Ecuador, TimeZoneOfficialEnum.Galapagos_Time);
            AddToDictionary(CountryEnum.Fiji, TimeZoneOfficialEnum.Fiji_Time);
            AddToDictionary(CountryEnum.French_Polynesia, TimeZoneOfficialEnum.Gambier_Islands);
            AddToDictionary(CountryEnum.French_Polynesia, TimeZoneOfficialEnum.Tahiti_Time);
            AddToDictionary(CountryEnum.Georgia, TimeZoneOfficialEnum.Georgia_Standard_Time);
            AddToDictionary(CountryEnum.French_Guiana, TimeZoneOfficialEnum.French_Guiana_Time);
            AddToDictionary(CountryEnum.Kiribati, TimeZoneOfficialEnum.Gilbert_Island_Time);
            AddToDictionary(CountryEnum.Kiribati, TimeZoneOfficialEnum.Line_Islands_Time);
            AddToDictionary(CountryEnum.Kiribati, TimeZoneOfficialEnum.Phoenix_Island_Time);
            AddToDictionary(CountryEnum.Guyana, TimeZoneOfficialEnum.Guyana_Time);
            AddToDictionary(CountryEnum.Hong_Kong, TimeZoneOfficialEnum.Hong_Kong_Time);
            AddToDictionary(CountryEnum.Cambodia, TimeZoneOfficialEnum.Indochina_Time);
            AddToDictionary(CountryEnum.Laos, TimeZoneOfficialEnum.Indochina_Time);
            AddToDictionary(CountryEnum.Thailand, TimeZoneOfficialEnum.Indochina_Time);
            AddToDictionary(CountryEnum.Vietnam, TimeZoneOfficialEnum.Indochina_Time);
            AddToDictionary(CountryEnum.India, TimeZoneOfficialEnum.Indian_Standard_Time);
            AddToDictionary(CountryEnum.Japan, TimeZoneOfficialEnum.Japan_Standard_Time);
            AddToDictionary(CountryEnum.Kyrgyzstan, TimeZoneOfficialEnum.Kyrgyzstan_Time);
            AddToDictionary(CountryEnum.South_Korea, TimeZoneOfficialEnum.Korea_Standard_Time);
            AddToDictionary(CountryEnum.French_Polynesia, TimeZoneOfficialEnum.Marquesas_Islands_Time);
            AddToDictionary(CountryEnum.Marshall_Islands, TimeZoneOfficialEnum.Marshall_Islands);
            AddToDictionary(CountryEnum.Myanmar, TimeZoneOfficialEnum.Myanmar_Standard_Time);
            AddToDictionary(CountryEnum.Georgia, TimeZoneOfficialEnum.Moscow_Standard_Time);
            AddToDictionary(CountryEnum.Russia, TimeZoneOfficialEnum.Moscow_Standard_Time);
            AddToDictionary(CountryEnum.Ukraine, TimeZoneOfficialEnum.Moscow_Standard_Time);
            AddToDictionary(CountryEnum.Malaysia, TimeZoneOfficialEnum.Malaysia_Time);
            AddToDictionary(CountryEnum.Mauritius, TimeZoneOfficialEnum.Mauritius_Time);
            AddToDictionary(CountryEnum.Maldives, TimeZoneOfficialEnum.Maldives_Time);
            AddToDictionary(CountryEnum.New_Caledonia, TimeZoneOfficialEnum.New_Caledonia_Time);
            AddToDictionary(CountryEnum.Nepal, TimeZoneOfficialEnum.Nepal_Time);
            AddToDictionary(CountryEnum.Niue, TimeZoneOfficialEnum.Niue_Time);
            AddToDictionary(CountryEnum.Kazakhstan, TimeZoneOfficialEnum.Oral_Time);
            AddToDictionary(CountryEnum.Peru, TimeZoneOfficialEnum.Peru_Time);
            AddToDictionary(CountryEnum.Philippines, TimeZoneOfficialEnum.Philippine_Time);
            AddToDictionary(CountryEnum.Pakistan, TimeZoneOfficialEnum.Pakistan_Standard_Time);
            AddToDictionary(CountryEnum.Reunion, TimeZoneOfficialEnum.Réunion_Time);
            AddToDictionary(CountryEnum.Lesotho, TimeZoneOfficialEnum.South_African_Standard_Time);
            AddToDictionary(CountryEnum.South_Africa, TimeZoneOfficialEnum.South_African_Standard_Time);
            AddToDictionary(CountryEnum.Swaziland, TimeZoneOfficialEnum.South_African_Standard_Time);
            AddToDictionary(CountryEnum.Solomon_Islands, TimeZoneOfficialEnum.Solomon_Islands_Time);
            AddToDictionary(CountryEnum.Seychelles, TimeZoneOfficialEnum.Seychelles_Time);
            AddToDictionary(CountryEnum.Singapore, TimeZoneOfficialEnum.Singapore_Time);
            AddToDictionary(CountryEnum.Suriname, TimeZoneOfficialEnum.Suriname_Time);
            AddToDictionary(CountryEnum.American_Samoa, TimeZoneOfficialEnum.Samoa_Standard_Time);
            AddToDictionary(CountryEnum.French_Southern_and_Antarctic_Lands, TimeZoneOfficialEnum.Indian_Kerguelen);
            AddToDictionary(CountryEnum.Tajikistan, TimeZoneOfficialEnum.Tajikistan_Time);
            AddToDictionary(CountryEnum.Tokelau, TimeZoneOfficialEnum.Tokelau_Time);
            AddToDictionary(CountryEnum.East_Timor, TimeZoneOfficialEnum.East_Timor_Time);
            AddToDictionary(CountryEnum.Turkmenistan, TimeZoneOfficialEnum.Turkmenistan_Time);
            AddToDictionary(CountryEnum.Turkey, TimeZoneOfficialEnum.Turkey_Time);
            AddToDictionary(CountryEnum.Tonga, TimeZoneOfficialEnum.Tonga_Time);
            AddToDictionary(CountryEnum.Tuvalu, TimeZoneOfficialEnum.Tuvalu_Time);
            AddToDictionary(CountryEnum.Uruguay, TimeZoneOfficialEnum.Uruguay_Time);
            AddToDictionary(CountryEnum.Uzbekistan, TimeZoneOfficialEnum.Uzbekistan_Time);
            AddToDictionary(CountryEnum.Venezuela, TimeZoneOfficialEnum.Venezuelan_Standard_Time);
            AddToDictionary(CountryEnum.Vanuatu, TimeZoneOfficialEnum.Vanuatu_Time);


            CountryOfficials = CountryOfficials_Temp.ToDictionary(x => x.Key, x => x.Value.ToArray());
            CountryOfficials_Temp = null;
        }

        private static void AddToDictionary(CountryEnum country, TimeZoneOfficialEnum official)
        {
            AddToDictionaryInternal
            (
                country, new List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>>()
                {
                    new KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>(official, official)
                }
            );
        }

        private static void AddToDictionary(CountryEnum[] countries, List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>> officials)
        {
            foreach (CountryEnum country in countries)
            {
                AddToDictionaryInternal(country, officials);
            }
        }

        private static void AddToDictionaryInternal(CountryEnum country, List<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>> officials)
        {
            if (CountryOfficials_Temp.ContainsKey(country))
            {
                CountryOfficials_Temp[country].AddRange(officials);
            }
            else CountryOfficials_Temp.Add(country, officials);
        }
    }
}
