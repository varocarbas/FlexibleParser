using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    internal partial class TimeZoneIANAInternal
    {
        //Note that this approach of relying on small dictionaries has proven to deliver a better performance than 
        //alternatives storing all the information in the same class. 
        internal static Dictionary<TimeZoneIANAEnum, CountryEnum[]> TimeZoneIANACountries;
        internal static Dictionary<TimeZoneIANAEnum, IEnumerable<IANACityRegionEnum>> TimeZoneIANAAreas;

        internal static void PopulateMain()
        {
            PopulateCountriesDictionary();

            TimeZoneIANAAreas = new Dictionary<TimeZoneIANAEnum, IEnumerable<IANACityRegionEnum>>();

            foreach (string input in Enum.GetNames(typeof(TimeZoneIANAEnum)))
            {
                TimeZoneIANAEnum enumItem = (TimeZoneIANAEnum)Enum.Parse
                (
                    typeof(TimeZoneIANAEnum), input
                );
                if (HasNoGeo(enumItem)) continue;

                string[] temp = input.Split('_');

                TimeZoneIANAAreas.Add
                (
                    enumItem, GetAreaNames(enumItem, temp).ToArray()
                );
            }
        }

        private static IEnumerable<IANACityRegionEnum> GetAreaNames(TimeZoneIANAEnum enumItem, string[] temp)
        {
            yield return GetMainAreaName(temp);

            //As listed in zone1970.tab (https://www.iana.org/time-zones/repository/releases/tzdata2017a.tar.gz);

            if (enumItem == TimeZoneIANAEnum.America_Argentina_Buenos_Aires)
            {
                yield return IANACityRegionEnum.Distrito_Federal;  //CF
            }
            else if (enumItem == TimeZoneIANAEnum.America_Argentina_Cordoba)
            {
                yield return IANACityRegionEnum.Chaco; //CC
                yield return IANACityRegionEnum.Entre_Ríos; //ER
                yield return IANACityRegionEnum.Misiones; //MN
                yield return IANACityRegionEnum.Santiago_del_Estero; //SE
                yield return IANACityRegionEnum.Santa_Fe; //SF              
            }
            else if (enumItem == TimeZoneIANAEnum.America_Argentina_Salta)
            {
                yield return IANACityRegionEnum.La_Pampa; //LP;
                yield return IANACityRegionEnum.Neuquén; //NQ;
                yield return IANACityRegionEnum.Río_Negro; //RN;              
            }
            else if (enumItem == TimeZoneIANAEnum.America_Argentina_Catamarca)
            {
                yield return IANACityRegionEnum.Chubut;
            }
            else if (enumItem == TimeZoneIANAEnum.Pacific_Pago_Pago)
            {
                yield return IANACityRegionEnum.Samoa;
            }
            else if (enumItem == TimeZoneIANAEnum.Australia_Hobart)
            {
                yield return IANACityRegionEnum.Tasmania;
            }
            else if (enumItem == TimeZoneIANAEnum.Australia_Currie)
            {
                yield return IANACityRegionEnum.King_Island;
            }
            else if (enumItem == TimeZoneIANAEnum.Australia_Melbourne)
            {
                yield return IANACityRegionEnum.Victoria;
            }
            else if (enumItem == TimeZoneIANAEnum.Australia_Sydney)
            {
                yield return IANACityRegionEnum.New_South_Walles;
            }
            else if (enumItem == TimeZoneIANAEnum.Australia_Broken_Hill)
            {
                yield return IANACityRegionEnum.Yancowinna;
            }
            else if (enumItem == TimeZoneIANAEnum.Australia_Brisbane)
            {
                yield return IANACityRegionEnum.Queensland;
            }
            else if (enumItem == TimeZoneIANAEnum.Australia_Lindeman)
            {
                yield return IANACityRegionEnum.Whitsunday_Islands;
            }
            else if (enumItem == TimeZoneIANAEnum.Australia_Adelaide)
            {
                yield return IANACityRegionEnum.Southern_Australia;
            }
            else if (enumItem == TimeZoneIANAEnum.Australia_Darwin)
            {
                yield return IANACityRegionEnum.Northern_Territory;
            }
            else if (enumItem == TimeZoneIANAEnum.Australia_Perth)
            {
                yield return IANACityRegionEnum.Western_Australia;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Noronha)
            {
                yield return IANACityRegionEnum.Atlantic_Islands;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Belem)
            {
                yield return IANACityRegionEnum.Eastern_Pará;
                yield return IANACityRegionEnum.Amapá;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Fortaleza)
            {
                yield return IANACityRegionEnum.Northeastern_Brazil;
                yield return IANACityRegionEnum.Maranhão; //MA
                yield return IANACityRegionEnum.Piauí; //PI
                yield return IANACityRegionEnum.Ceará; //CE
                yield return IANACityRegionEnum.Rio_Grande_do_Norte; //RN
                yield return IANACityRegionEnum.Paraíba; //PB
            }
            else if (enumItem == TimeZoneIANAEnum.America_Recife)
            {
                yield return IANACityRegionEnum.Pernambuco;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Araguaina)
            {
                yield return IANACityRegionEnum.Tocantins;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Maceio)
            {
                yield return IANACityRegionEnum.Alagoas;
                yield return IANACityRegionEnum.Sergipe;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Sao_Paulo)
            {
                yield return IANACityRegionEnum.São_Paulo;
                yield return IANACityRegionEnum.Southeastern_Brazil;
                yield return IANACityRegionEnum.Goiás; //GO
                yield return IANACityRegionEnum.Distrito_Federal; //DF
                yield return IANACityRegionEnum.Minas_Gerais; //MG
                yield return IANACityRegionEnum.Espírito_Santo; //ES
                yield return IANACityRegionEnum.Rio_de_Janeiro; //RJ
                yield return IANACityRegionEnum.Paraná; //PR
                yield return IANACityRegionEnum.Santa_Catarina; //SC
                yield return IANACityRegionEnum.Rio_Grande_do_Sul; //RS
            }
            else if (enumItem == TimeZoneIANAEnum.America_Campo_Grande)
            {
                yield return IANACityRegionEnum.Mato_Grosso_do_Sul;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Cuiaba)
            {
                yield return IANACityRegionEnum.Mato_Grosso;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Santarem)
            {
                yield return IANACityRegionEnum.Western_Pará;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Porto_Velho)
            {
                yield return IANACityRegionEnum.Rondônia;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Boa_Vista)
            {
                yield return IANACityRegionEnum.Roraima;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Manaus)
            {
                yield return IANACityRegionEnum.Eastern_Amazonas;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Eirunepe)
            {
                yield return IANACityRegionEnum.Western_Amazonas;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Rio_Branco)
            {
                yield return IANACityRegionEnum.Acre;
            }
            else if (enumItem == TimeZoneIANAEnum.America_St_Johns)
            {
                yield return IANACityRegionEnum.Newfoundland;
                yield return IANACityRegionEnum.Southeastern_Labrador;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Halifax)
            {
                yield return IANACityRegionEnum.Nova_Scotia;
                yield return IANACityRegionEnum.Prince_Edward_Island;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Glace_Bay)
            {
                yield return IANACityRegionEnum.Cape_Breton;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Moncton)
            {
                yield return IANACityRegionEnum.New_Brunswick;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Goose_Bay)
            {
                yield return IANACityRegionEnum.Labrador;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Blanc_Sablon)
            {
                yield return IANACityRegionEnum.Lower_North_Shore;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Toronto)
            {
                yield return IANACityRegionEnum.Ontario;
                yield return IANACityRegionEnum.Quebec;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Nipigon)
            {
                yield return IANACityRegionEnum.Ontario;
                yield return IANACityRegionEnum.Quebec;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Iqaluit)
            {
                yield return IANACityRegionEnum.Nunavut;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Winnipeg)
            {
                yield return IANACityRegionEnum.Western_Ontario;
                yield return IANACityRegionEnum.Manitoba;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Rainy_River)
            {
                yield return IANACityRegionEnum.Rainy_River;
                yield return IANACityRegionEnum.Fort_Frances;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Rankin_Inlet)
            {
                yield return IANACityRegionEnum.Central_Nunavut;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Regina)
            {
                yield return IANACityRegionEnum.Saskatchewan;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Swift_Current)
            {
                yield return IANACityRegionEnum.Midwestern_Saskatchewan;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Edmonton)
            {
                yield return IANACityRegionEnum.Alberta;
                yield return IANACityRegionEnum.Eastern_British_Columbia;
                yield return IANACityRegionEnum.Western_Saskatchewan;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Cambridge_Bay)
            {
                yield return IANACityRegionEnum.Western_Nunavut;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Yellowknife)
            {
                yield return IANACityRegionEnum.Central_Northwest_Territories;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Inuvik)
            {
                yield return IANACityRegionEnum.Western_Northwest_Territories;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Dawson_Creek)
            {
                yield return IANACityRegionEnum.Fort_St_John;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Vancouver)
            {
                yield return IANACityRegionEnum.British_Columbia;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Whitehorse)
            {
                yield return IANACityRegionEnum.Southern_Yucon;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Dawson)
            {
                yield return IANACityRegionEnum.Northern_Yucon;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Punta_Arenas)
            {
                yield return IANACityRegionEnum.Region_of_Magallanes;
            }
            else if (enumItem == TimeZoneIANAEnum.Asia_Shanghai)
            {
                yield return IANACityRegionEnum.Beijing;
            }
            else if (enumItem == TimeZoneIANAEnum.Asia_Urumqi)
            {
                yield return IANACityRegionEnum.Xinjiang;
            }
            else if (enumItem == TimeZoneIANAEnum.Asia_Famagusta)
            {
                yield return IANACityRegionEnum.Northern_Cyprus;
            }
            else if (enumItem == TimeZoneIANAEnum.Africa_Ceuta)
            {
                yield return IANACityRegionEnum.Melilla;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Danmarkshavn)
            {
                yield return IANACityRegionEnum.National_Park;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Scoresbysund)
            {
                yield return IANACityRegionEnum.Ittoqqortoormiit;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Thule)
            {
                yield return IANACityRegionEnum.Pituffik;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Thule)
            {
                yield return IANACityRegionEnum.Pituffik;
            }
            else if (enumItem == TimeZoneIANAEnum.Asia_Jakarta)
            {
                yield return IANACityRegionEnum.Java;
                yield return IANACityRegionEnum.Sumatra;
            }
            else if (enumItem == TimeZoneIANAEnum.Asia_Pontianak)
            {
                yield return IANACityRegionEnum.Western_Borneo;
                yield return IANACityRegionEnum.Central_Borneo;
            }
            else if (enumItem == TimeZoneIANAEnum.Asia_Makassar)
            {
                yield return IANACityRegionEnum.Eastern_Borneo;
                yield return IANACityRegionEnum.Southern_Borneo;
                yield return IANACityRegionEnum.Sulawesi;
                yield return IANACityRegionEnum.Celebes;
                yield return IANACityRegionEnum.Bali;
                yield return IANACityRegionEnum.Nusa_Tengarra;
                yield return IANACityRegionEnum.Western_Timor;
            }
            else if (enumItem == TimeZoneIANAEnum.Asia_Jayapura)
            {
                yield return IANACityRegionEnum.West_Papua;
                yield return IANACityRegionEnum.Irian_Jaya;
                yield return IANACityRegionEnum.Malukus;
                yield return IANACityRegionEnum.Moluccas;
            }
            else if (enumItem == TimeZoneIANAEnum.Pacific_Tarawa)
            {
                yield return IANACityRegionEnum.Gilbert_Islands;
            }
            else if (enumItem == TimeZoneIANAEnum.Pacific_Enderbury)
            {
                yield return IANACityRegionEnum.Phoenix_Islands;
            }
            else if (enumItem == TimeZoneIANAEnum.Pacific_Kiritimati)
            {
                yield return IANACityRegionEnum.Line_Islands;
            }
            else if (enumItem == TimeZoneIANAEnum.Asia_Almaty)
            {
                yield return IANACityRegionEnum.Kazakhstan;
            }
            else if (enumItem == TimeZoneIANAEnum.Asia_Qyzylorda)
            {
                yield return IANACityRegionEnum.Kyzylorda;
                yield return IANACityRegionEnum.Kzyl_Orda;
            }
            else if (enumItem == TimeZoneIANAEnum.Asia_Aqtobe)
            {
                yield return IANACityRegionEnum.Aqtöbe;
                yield return IANACityRegionEnum.Aktobe;
            }
            else if (enumItem == TimeZoneIANAEnum.Asia_Aqtau)
            {
                yield return IANACityRegionEnum.Mangghystaū;
                yield return IANACityRegionEnum.Mankistau;
            }
            else if (enumItem == TimeZoneIANAEnum.Asia_Atyrau)
            {
                yield return IANACityRegionEnum.Atyraū;
                yield return IANACityRegionEnum.Atirau;
                yield return IANACityRegionEnum.Gur_Yev;
            }
            else if (enumItem == TimeZoneIANAEnum.Asia_Oral)
            {
                yield return IANACityRegionEnum.West_Kazakhstan;
            }
            else if (enumItem == TimeZoneIANAEnum.Asia_Hovd)
            {
                yield return IANACityRegionEnum.Bayan_Ölgii;
                yield return IANACityRegionEnum.Govi_Altai;
                yield return IANACityRegionEnum.Uvs;
                yield return IANACityRegionEnum.Zavkhan;
            }
            else if (enumItem == TimeZoneIANAEnum.Asia_Choibalsan)
            {
                yield return IANACityRegionEnum.Dornod;
                yield return IANACityRegionEnum.Sükhbaatar;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Cancun)
            {
                yield return IANACityRegionEnum.Quintana_Roo;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Merida)
            {
                yield return IANACityRegionEnum.Campeche;
                yield return IANACityRegionEnum.Yucatán;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Monterrey)
            {
                yield return IANACityRegionEnum.Durango;
                yield return IANACityRegionEnum.Coahuila;
                yield return IANACityRegionEnum.Nuevo_León;
                yield return IANACityRegionEnum.Tamaulipas;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Matamoros)
            {
                yield return IANACityRegionEnum.US_border_Coahuila;
                yield return IANACityRegionEnum.US_border_Nuevo_León;
                yield return IANACityRegionEnum.US_border_Tamaulipas;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Mazatlan)
            {
                yield return IANACityRegionEnum.Baja_California_Sur;
                yield return IANACityRegionEnum.Nayarit;
                yield return IANACityRegionEnum.Sinaloa;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Ojinaga)
            {
                yield return IANACityRegionEnum.US_border_Chihuahua;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Hermosillo)
            {
                yield return IANACityRegionEnum.Sonora;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Tijuana)
            {
                yield return IANACityRegionEnum.Baja_California;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Bahia_Banderas)
            {
                yield return IANACityRegionEnum.Bahía_de_Banderas;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Bahia_Banderas)
            {
                yield return IANACityRegionEnum.Bahía_de_Banderas;
            }
            else if (enumItem == TimeZoneIANAEnum.Asia_Kuala_Lumpur)
            {
                yield return IANACityRegionEnum.Malaysia;
            }
            else if (enumItem == TimeZoneIANAEnum.Asia_Kuching)
            {
                yield return IANACityRegionEnum.Sabah;
                yield return IANACityRegionEnum.Sarawak;
            }
            else if (enumItem == TimeZoneIANAEnum.Pacific_Tahiti)
            {
                yield return IANACityRegionEnum.Society_Islands;
            }
            else if (enumItem == TimeZoneIANAEnum.Asia_Gaza)
            {
                yield return IANACityRegionEnum.Gaza_Strip;
            }
            else if (enumItem == TimeZoneIANAEnum.Asia_Hebron)
            {
                yield return IANACityRegionEnum.West_Bank;
            }
            else if (enumItem == TimeZoneIANAEnum.Indian_Reunion)
            {
                yield return IANACityRegionEnum.Réunion;
                yield return IANACityRegionEnum.Crozet;
                yield return IANACityRegionEnum.Scattered_Islands;
            }
            else if (enumItem == TimeZoneIANAEnum.Europe_Simferopol)
            {
                yield return IANACityRegionEnum.Crimea;
            }
            else if (enumItem == TimeZoneIANAEnum.Europe_Samara)
            {
                yield return IANACityRegionEnum.Udmurtia;
            }
            else if (enumItem == TimeZoneIANAEnum.Asia_Yekaterinburg)
            {
                yield return IANACityRegionEnum.Urals;
            }
            else if (enumItem == TimeZoneIANAEnum.Asia_Barnaul)
            {
                yield return IANACityRegionEnum.Altai;
            }
            else if (enumItem == TimeZoneIANAEnum.Asia_Novokuznetsk)
            {
                yield return IANACityRegionEnum.Kemerovo;
            }
            else if (enumItem == TimeZoneIANAEnum.Asia_Krasnoyarsk)
            {
                yield return IANACityRegionEnum.Krasnoyarsk_area;
            }
            else if (enumItem == TimeZoneIANAEnum.Asia_Irkutsk)
            {
                yield return IANACityRegionEnum.Buryatia;
            }
            else if (enumItem == TimeZoneIANAEnum.Asia_Chita)
            {
                yield return IANACityRegionEnum.Zabaykalsky;
            }
            else if (enumItem == TimeZoneIANAEnum.Asia_Yakutsk)
            {
                yield return IANACityRegionEnum.Lena_River;
            }
            else if (enumItem == TimeZoneIANAEnum.Asia_Khandyga)
            {
                yield return IANACityRegionEnum.Tomponsky;
                yield return IANACityRegionEnum.Ust_Maysky;
            }
            else if (enumItem == TimeZoneIANAEnum.Asia_Vladivostok)
            {
                yield return IANACityRegionEnum.Amur_River;
            }
            else if (enumItem == TimeZoneIANAEnum.Asia_Ust_Nera)
            {
                yield return IANACityRegionEnum.Oymyakonsky;
            }
            else if (enumItem == TimeZoneIANAEnum.Asia_Srednekolymsk)
            {
                yield return IANACityRegionEnum.Eastern_Sakha;
                yield return IANACityRegionEnum.North_Kuril_Islands;
            }
            else if (enumItem == TimeZoneIANAEnum.Asia_Anadyr)
            {
                yield return IANACityRegionEnum.Bering_Sea;
            }
            else if (enumItem == TimeZoneIANAEnum.Indian_Kerguelen)
            {
                yield return IANACityRegionEnum.St_Paul_Island;
                yield return IANACityRegionEnum.Amsterdam_Island;
            }
            else if (enumItem == TimeZoneIANAEnum.Asia_Bangkok)
            {
                yield return IANACityRegionEnum.Indochina;
            }
            else if (enumItem == TimeZoneIANAEnum.Europe_Zaporozhye)
            {
                yield return IANACityRegionEnum.Zaporozh_Ye;
                yield return IANACityRegionEnum.Zaporizhia;
                yield return IANACityRegionEnum.Eastern_Lugansk;
                yield return IANACityRegionEnum.Eastern_Luhansk;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Detroit)
            {
                yield return IANACityRegionEnum.Michigan;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Kentucky_Monticello)
            {
                yield return IANACityRegionEnum.Wayne;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Indiana_Vincennes)
            {
                yield return IANACityRegionEnum.Daviess; //Da
                yield return IANACityRegionEnum.Dubois; //Du
                yield return IANACityRegionEnum.Knox; //K
                yield return IANACityRegionEnum.Martin; //Mb
            }
            else if (enumItem == TimeZoneIANAEnum.America_Indiana_Winamac)
            {
                yield return IANACityRegionEnum.Pulaski;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Indiana_Marengo)
            {
                yield return IANACityRegionEnum.Crawford;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Indiana_Petersburg)
            {
                yield return IANACityRegionEnum.Pike;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Indiana_Vevay)
            {
                yield return IANACityRegionEnum.Switzerland;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Indiana_Tell_City)
            {
                yield return IANACityRegionEnum.Perry;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Indiana_Knox)
            {
                yield return IANACityRegionEnum.Starke;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Indiana_Knox)
            {
                yield return IANACityRegionEnum.Starke;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Menominee)
            {
                yield return IANACityRegionEnum.Wisconsin_border_Michigan;
            }
            else if (enumItem == TimeZoneIANAEnum.America_North_Dakota_Center)
            {
                yield return IANACityRegionEnum.Oliver;
            }
            else if (enumItem == TimeZoneIANAEnum.America_North_Dakota_New_Salem)
            {
                yield return IANACityRegionEnum.Morton_rural;
            }
            else if (enumItem == TimeZoneIANAEnum.America_North_Dakota_Beulah)
            {
                yield return IANACityRegionEnum.Mercer;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Phoenix)
            {
                yield return IANACityRegionEnum.Arizona;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Juneau)
            {
                yield return IANACityRegionEnum.Juneau_area;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Sitka)
            {
                yield return IANACityRegionEnum.Sitka_area;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Metlakatla)
            {
                yield return IANACityRegionEnum.Annette_Island;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Nome)
            {
                yield return IANACityRegionEnum.Alaska_West;
            }
            else if (enumItem == TimeZoneIANAEnum.America_Adak)
            {
                yield return IANACityRegionEnum.Aleutian_Islands;
            }
            else if (enumItem == TimeZoneIANAEnum.Pacific_Honolulu)
            {
                yield return IANACityRegionEnum.Hawaii;
            }
            else if (enumItem == TimeZoneIANAEnum.Asia_Samarkand)
            {
                yield return IANACityRegionEnum.Western_Uzbekistan;
            }
            else if (enumItem == TimeZoneIANAEnum.Asia_Tashkent)
            {
                yield return IANACityRegionEnum.Eastern_Uzbekistan;
            }
        }

        internal static CountryEnum[] GetCountriesArray(string[] abbreviations)
        {
            List<CountryEnum> outCountries = new List<CountryEnum>();

            foreach (string abbreviation in abbreviations)
            {
                outCountries.Add
                (
                    CountryInternal.CodeCountryTemp[abbreviation]
                );
            }

            return outCountries.ToArray();
        }

        internal static void AddCountries(TimeZoneIANAEnum timezone, string[] abbreviations)
        {
            TimeZoneIANACountries.Add
            (
                timezone, GetCountriesArray(abbreviations)
            );
        }

        private static void PopulateCountriesDictionary()
        {
            TimeZoneIANACountries = new Dictionary<TimeZoneIANAEnum, CountryEnum[]>();

            AddCountries(TimeZoneIANAEnum.Asia_Kabul, new string[] { "AF" });
            AddCountries(TimeZoneIANAEnum.Europe_Tirane, new string[] { "AL" });
            AddCountries(TimeZoneIANAEnum.Asia_Yerevan, new string[] { "AM" });
            AddCountries(TimeZoneIANAEnum.Antarctica_Casey, new string[] { "AQ" });
            AddCountries(TimeZoneIANAEnum.Antarctica_Davis, new string[] { "AQ" });
            AddCountries(TimeZoneIANAEnum.Antarctica_DumontDUrville, new string[] { "AQ" });
            AddCountries(TimeZoneIANAEnum.Antarctica_Mawson, new string[] { "AQ" });
            AddCountries(TimeZoneIANAEnum.Antarctica_Palmer, new string[] { "AQ" });
            AddCountries(TimeZoneIANAEnum.Antarctica_Rothera, new string[] { "AQ" });
            AddCountries(TimeZoneIANAEnum.Antarctica_Syowa, new string[] { "AQ" });
            AddCountries(TimeZoneIANAEnum.Antarctica_Vostok, new string[] { "AQ" });
            AddCountries(TimeZoneIANAEnum.America_Argentina_Buenos_Aires, new string[] { "AR" });
            AddCountries(TimeZoneIANAEnum.America_Argentina_Cordoba, new string[] { "AR" });
            AddCountries(TimeZoneIANAEnum.America_Argentina_Salta, new string[] { "AR" });
            AddCountries(TimeZoneIANAEnum.America_Argentina_Jujuy, new string[] { "AR" });
            AddCountries(TimeZoneIANAEnum.America_Argentina_Catamarca, new string[] { "AR" });
            AddCountries(TimeZoneIANAEnum.America_Argentina_Tucuman, new string[] { "AR" });
            AddCountries(TimeZoneIANAEnum.America_Argentina_La_Rioja, new string[] { "AR" });
            AddCountries(TimeZoneIANAEnum.America_Argentina_San_Juan, new string[] { "AR" });
            AddCountries(TimeZoneIANAEnum.America_Argentina_Mendoza, new string[] { "AR" });
            AddCountries(TimeZoneIANAEnum.America_Argentina_San_Luis, new string[] { "AR" });
            AddCountries(TimeZoneIANAEnum.America_Argentina_Rio_Gallegos, new string[] { "AR" });
            AddCountries(TimeZoneIANAEnum.America_Argentina_Ushuaia, new string[] { "AR" });
            AddCountries(TimeZoneIANAEnum.Pacific_Pago_Pago, new string[] { "AS", "UM" });
            AddCountries(TimeZoneIANAEnum.Pacific_Midway, new string[] { "UM" });
            AddCountries(TimeZoneIANAEnum.Europe_Vienna, new string[] { "AT" });
            AddCountries(TimeZoneIANAEnum.Australia_Lord_Howe, new string[] { "AU" });
            AddCountries(TimeZoneIANAEnum.Antarctica_Macquarie, new string[] { "AU" });
            AddCountries(TimeZoneIANAEnum.Australia_Hobart, new string[] { "AU" });
            AddCountries(TimeZoneIANAEnum.Australia_Currie, new string[] { "AU" });
            AddCountries(TimeZoneIANAEnum.Australia_Melbourne, new string[] { "AU" });
            AddCountries(TimeZoneIANAEnum.Australia_Sydney, new string[] { "AU" });
            AddCountries(TimeZoneIANAEnum.Australia_Broken_Hill, new string[] { "AU" });
            AddCountries(TimeZoneIANAEnum.Australia_Brisbane, new string[] { "AU" });
            AddCountries(TimeZoneIANAEnum.Australia_Lindeman, new string[] { "AU" });
            AddCountries(TimeZoneIANAEnum.Australia_Adelaide, new string[] { "AU" });
            AddCountries(TimeZoneIANAEnum.Australia_Darwin, new string[] { "AU" });
            AddCountries(TimeZoneIANAEnum.Australia_Perth, new string[] { "AU" });
            AddCountries(TimeZoneIANAEnum.Australia_Eucla, new string[] { "AU" });
            AddCountries(TimeZoneIANAEnum.Asia_Baku, new string[] { "AZ" });
            AddCountries(TimeZoneIANAEnum.Asia_Bahrain, new string[] { "BH" });
            AddCountries(TimeZoneIANAEnum.Asia_Kuwait, new string[] { "KW" });
            AddCountries(TimeZoneIANAEnum.Asia_Aden, new string[] { "YE" });
            AddCountries(TimeZoneIANAEnum.America_Barbados, new string[] { "BB" });
            AddCountries(TimeZoneIANAEnum.Asia_Dhaka, new string[] { "BD" });
            AddCountries(TimeZoneIANAEnum.Europe_Brussels, new string[] { "BE" });
            AddCountries(TimeZoneIANAEnum.Europe_Sofia, new string[] { "BG" });
            AddCountries(TimeZoneIANAEnum.Atlantic_Bermuda, new string[] { "BM" });
            AddCountries(TimeZoneIANAEnum.Asia_Brunei, new string[] { "BN" });
            AddCountries(TimeZoneIANAEnum.America_La_Paz, new string[] { "BO" });
            AddCountries(TimeZoneIANAEnum.America_Kralendijk, new string[] { "BQ" });
            AddCountries(TimeZoneIANAEnum.America_Dominica, new string[] { "DM" });
            AddCountries(TimeZoneIANAEnum.America_Grenada, new string[] { "GD" });
            AddCountries(TimeZoneIANAEnum.America_Guadeloupe, new string[] { "GP" });
            AddCountries(TimeZoneIANAEnum.America_St_Kitts, new string[] { "KN" });
            AddCountries(TimeZoneIANAEnum.America_St_Lucia, new string[] { "LC" });
            AddCountries(TimeZoneIANAEnum.America_Marigot, new string[] { "MF" });
            AddCountries(TimeZoneIANAEnum.America_Antigua, new string[] { "AG" });
            AddCountries(TimeZoneIANAEnum.America_Aruba, new string[] { "AW" });
            AddCountries(TimeZoneIANAEnum.America_St_Barthelemy, new string[] { "BL" });
            AddCountries(TimeZoneIANAEnum.America_Noronha, new string[] { "BR" });
            AddCountries(TimeZoneIANAEnum.America_Belem, new string[] { "BR" });
            AddCountries(TimeZoneIANAEnum.America_Fortaleza, new string[] { "BR" });
            AddCountries(TimeZoneIANAEnum.America_Recife, new string[] { "BR" });
            AddCountries(TimeZoneIANAEnum.America_Araguaina, new string[] { "BR" });
            AddCountries(TimeZoneIANAEnum.America_Maceio, new string[] { "BR" });
            AddCountries(TimeZoneIANAEnum.America_Bahia, new string[] { "BR" });
            AddCountries(TimeZoneIANAEnum.America_Sao_Paulo, new string[] { "BR" });
            AddCountries(TimeZoneIANAEnum.America_Campo_Grande, new string[] { "BR" });
            AddCountries(TimeZoneIANAEnum.America_Cuiaba, new string[] { "BR" });
            AddCountries(TimeZoneIANAEnum.America_Santarem, new string[] { "BR" });
            AddCountries(TimeZoneIANAEnum.America_Porto_Velho, new string[] { "BR" });
            AddCountries(TimeZoneIANAEnum.America_Boa_Vista, new string[] { "BR" });
            AddCountries(TimeZoneIANAEnum.America_Manaus, new string[] { "BR" });
            AddCountries(TimeZoneIANAEnum.America_Eirunepe, new string[] { "BR" });
            AddCountries(TimeZoneIANAEnum.America_Rio_Branco, new string[] { "BR" });
            AddCountries(TimeZoneIANAEnum.America_Nassau, new string[] { "BR" });
            AddCountries(TimeZoneIANAEnum.Asia_Thimphu, new string[] { "BT" });
            AddCountries(TimeZoneIANAEnum.Europe_Minsk, new string[] { "BY" });
            AddCountries(TimeZoneIANAEnum.America_Belize, new string[] { "BZ" });
            AddCountries(TimeZoneIANAEnum.America_St_Johns, new string[] { "CA" });
            AddCountries(TimeZoneIANAEnum.America_Halifax, new string[] { "CA" });
            AddCountries(TimeZoneIANAEnum.America_Glace_Bay, new string[] { "CA" });
            AddCountries(TimeZoneIANAEnum.America_Moncton, new string[] { "CA" });
            AddCountries(TimeZoneIANAEnum.America_Goose_Bay, new string[] { "CA" });
            AddCountries(TimeZoneIANAEnum.America_Blanc_Sablon, new string[] { "CA" });
            AddCountries(TimeZoneIANAEnum.America_Toronto, new string[] { "CA" });
            AddCountries(TimeZoneIANAEnum.America_Nipigon, new string[] { "CA" });
            AddCountries(TimeZoneIANAEnum.America_Thunder_Bay, new string[] { "CA" });
            AddCountries(TimeZoneIANAEnum.America_Iqaluit, new string[] { "CA" });
            AddCountries(TimeZoneIANAEnum.America_Montreal, new string[] { "CA" });
            AddCountries(TimeZoneIANAEnum.America_Pangnirtung, new string[] { "CA" });
            AddCountries(TimeZoneIANAEnum.America_Coral_Harbour, new string[] { "CA" });
            AddCountries(TimeZoneIANAEnum.America_Winnipeg, new string[] { "CA" });
            AddCountries(TimeZoneIANAEnum.America_Rainy_River, new string[] { "CA" });
            AddCountries(TimeZoneIANAEnum.America_Resolute, new string[] { "CA" });
            AddCountries(TimeZoneIANAEnum.America_Rankin_Inlet, new string[] { "CA" });
            AddCountries(TimeZoneIANAEnum.America_Regina, new string[] { "CA" });
            AddCountries(TimeZoneIANAEnum.America_Swift_Current, new string[] { "CA" });
            AddCountries(TimeZoneIANAEnum.America_Edmonton, new string[] { "CA" });
            AddCountries(TimeZoneIANAEnum.America_Cambridge_Bay, new string[] { "CA" });
            AddCountries(TimeZoneIANAEnum.America_Yellowknife, new string[] { "CA" });
            AddCountries(TimeZoneIANAEnum.America_Inuvik, new string[] { "CA" });
            AddCountries(TimeZoneIANAEnum.America_Creston, new string[] { "CA" });
            AddCountries(TimeZoneIANAEnum.America_Dawson_Creek, new string[] { "CA" });
            AddCountries(TimeZoneIANAEnum.America_Fort_Nelson, new string[] { "CA" });
            AddCountries(TimeZoneIANAEnum.America_Vancouver, new string[] { "CA" });
            AddCountries(TimeZoneIANAEnum.America_Whitehorse, new string[] { "CA" });
            AddCountries(TimeZoneIANAEnum.America_Dawson, new string[] { "CA" });
            AddCountries(TimeZoneIANAEnum.Indian_Cocos, new string[] { "CA" });
            AddCountries(TimeZoneIANAEnum.Europe_Zurich, new string[] { "CH", "DE", "LI" });
            AddCountries(TimeZoneIANAEnum.Africa_Abidjan, new string[] { "CI", "BF", "GM", "GN", "ML", "MR", "SH", "SL", "SN", "ST", "TG" });
            AddCountries(TimeZoneIANAEnum.Pacific_Rarotonga, new string[] { "CK" });
            AddCountries(TimeZoneIANAEnum.America_Santiago, new string[] { "CL" });
            AddCountries(TimeZoneIANAEnum.America_Punta_Arenas, new string[] { "CL" });
            AddCountries(TimeZoneIANAEnum.Pacific_Easter, new string[] { "CL" });
            AddCountries(TimeZoneIANAEnum.Asia_Shanghai, new string[] { "CN" });
            AddCountries(TimeZoneIANAEnum.Asia_Urumqi, new string[] { "CN" });
            AddCountries(TimeZoneIANAEnum.America_Bogota, new string[] { "CO" });
            AddCountries(TimeZoneIANAEnum.America_Costa_Rica, new string[] { "CR" });
            AddCountries(TimeZoneIANAEnum.America_Havana, new string[] { "CU" });
            AddCountries(TimeZoneIANAEnum.Atlantic_Cape_Verde, new string[] { "CV" });
            AddCountries(TimeZoneIANAEnum.America_Curacao, new string[] { "CW", "AW", "BQ", "SX" });
            AddCountries(TimeZoneIANAEnum.Indian_Christmas, new string[] { "CX" });
            AddCountries(TimeZoneIANAEnum.Asia_Nicosia, new string[] { "CY" });
            AddCountries(TimeZoneIANAEnum.Asia_Famagusta, new string[] { "CY" });
            AddCountries(TimeZoneIANAEnum.Europe_Prague, new string[] { "CZ", "SK" });
            AddCountries(TimeZoneIANAEnum.Europe_Berlin, new string[] { "DE" });
            AddCountries(TimeZoneIANAEnum.Europe_Copenhagen, new string[] { "DK" });
            AddCountries(TimeZoneIANAEnum.America_Santo_Domingo, new string[] { "DO" });
            AddCountries(TimeZoneIANAEnum.Africa_Algiers, new string[] { "DZ" });
            AddCountries(TimeZoneIANAEnum.America_Guayaquil, new string[] { "EC" });
            AddCountries(TimeZoneIANAEnum.Pacific_Galapagos, new string[] { "EC" });
            AddCountries(TimeZoneIANAEnum.Europe_Tallinn, new string[] { "EE" });
            AddCountries(TimeZoneIANAEnum.Europe_Mariehamn, new string[] { "AX" });
            AddCountries(TimeZoneIANAEnum.Africa_Cairo, new string[] { "EG" });
            AddCountries(TimeZoneIANAEnum.Africa_El_Aaiun, new string[] { "EH" });
            AddCountries(TimeZoneIANAEnum.Europe_Madrid, new string[] { "ES" });
            AddCountries(TimeZoneIANAEnum.Africa_Ceuta, new string[] { "ES" });
            AddCountries(TimeZoneIANAEnum.Atlantic_Canary, new string[] { "ES" });
            AddCountries(TimeZoneIANAEnum.Atlantic_Faeroe, new string[] { "FO" });
            AddCountries(TimeZoneIANAEnum.Europe_Guernsey, new string[] { "GG" });
            AddCountries(TimeZoneIANAEnum.Europe_Helsinki, new string[] { "FI", "AX" });
            AddCountries(TimeZoneIANAEnum.Pacific_Fiji, new string[] { "FJ" });
            AddCountries(TimeZoneIANAEnum.Atlantic_Stanley, new string[] { "FK" });
            AddCountries(TimeZoneIANAEnum.Pacific_Kosrae, new string[] { "FM" });
            AddCountries(TimeZoneIANAEnum.Europe_Paris, new string[] { "FR" });
            AddCountries(TimeZoneIANAEnum.Europe_London, new string[] { "GB", "GG", "IM", "JE" });
            AddCountries(TimeZoneIANAEnum.Asia_Tbilisi, new string[] { "GE" });
            AddCountries(TimeZoneIANAEnum.America_Cayenne, new string[] { "GF" });
            AddCountries(TimeZoneIANAEnum.Africa_Accra, new string[] { "GH" });
            AddCountries(TimeZoneIANAEnum.Europe_Gibraltar, new string[] { "GI" });
            AddCountries(TimeZoneIANAEnum.America_Godthab, new string[] { "GL" });
            AddCountries(TimeZoneIANAEnum.America_Danmarkshavn, new string[] { "GL" });
            AddCountries(TimeZoneIANAEnum.America_Scoresbysund, new string[] { "GL" });
            AddCountries(TimeZoneIANAEnum.America_Thule, new string[] { "GL" });
            AddCountries(TimeZoneIANAEnum.Europe_Athens, new string[] { "GR" });
            AddCountries(TimeZoneIANAEnum.Atlantic_South_Georgia, new string[] { "GS" });
            AddCountries(TimeZoneIANAEnum.America_Guatemala, new string[] { "GT" });
            AddCountries(TimeZoneIANAEnum.Pacific_Guam, new string[] { "GU", "MP" });
            AddCountries(TimeZoneIANAEnum.Africa_Bissau, new string[] { "GW" });
            AddCountries(TimeZoneIANAEnum.America_Guyana, new string[] { "GY" });
            AddCountries(TimeZoneIANAEnum.Asia_Hong_Kong, new string[] { "HK" });
            AddCountries(TimeZoneIANAEnum.America_Tegucigalpa, new string[] { "HN" });
            AddCountries(TimeZoneIANAEnum.America_Port_au_Prince, new string[] { "HT" });
            AddCountries(TimeZoneIANAEnum.Europe_Budapest, new string[] { "HU" });
            AddCountries(TimeZoneIANAEnum.Asia_Jakarta, new string[] { "ID" });
            AddCountries(TimeZoneIANAEnum.Asia_Pontianak, new string[] { "ID" });
            AddCountries(TimeZoneIANAEnum.Asia_Makassar, new string[] { "ID" });
            AddCountries(TimeZoneIANAEnum.Asia_Jayapura, new string[] { "ID" });
            AddCountries(TimeZoneIANAEnum.Europe_Dublin, new string[] { "IE" });
            AddCountries(TimeZoneIANAEnum.Europe_Isle_Of_Man, new string[] { "IM" });
            AddCountries(TimeZoneIANAEnum.Europe_Jersey, new string[] { "JE" });
            AddCountries(TimeZoneIANAEnum.Asia_Jerusalem, new string[] { "IL" });
            AddCountries(TimeZoneIANAEnum.Indian_Chagos, new string[] { "ID" });
            AddCountries(TimeZoneIANAEnum.Asia_Baghdad, new string[] { "IQ" });
            AddCountries(TimeZoneIANAEnum.Asia_Tehran, new string[] { "IR" });
            AddCountries(TimeZoneIANAEnum.Atlantic_Reykjavik, new string[] { "IS" });
            AddCountries(TimeZoneIANAEnum.Europe_Rome, new string[] { "IT", "SM", "VA" });
            AddCountries(TimeZoneIANAEnum.Europe_Vaduz, new string[] { "LI" });
            AddCountries(TimeZoneIANAEnum.Europe_Busingen, new string[] { "DE" });
            AddCountries(TimeZoneIANAEnum.Europe_Longyearbyen, new string[] { "SJ" });
            AddCountries(TimeZoneIANAEnum.Europe_San_Marino, new string[] { "SM" });
            AddCountries(TimeZoneIANAEnum.Europe_Vatican, new string[] { "VA" });
            AddCountries(TimeZoneIANAEnum.Europe_Podgorica, new string[] { "ME" });
            AddCountries(TimeZoneIANAEnum.Europe_Ljubljana, new string[] { "SI" });
            AddCountries(TimeZoneIANAEnum.Europe_Bratislava, new string[] { "SK" });
            AddCountries(TimeZoneIANAEnum.America_Jamaica, new string[] { "JM" });
            AddCountries(TimeZoneIANAEnum.America_Cayman, new string[] { "KY" });
            AddCountries(TimeZoneIANAEnum.Asia_Amman, new string[] { "JO" });
            AddCountries(TimeZoneIANAEnum.Asia_Tokyo, new string[] { "JP" });
            AddCountries(TimeZoneIANAEnum.Africa_Nairobi, new string[] { "KE", "DJ", "ER", "ET", "KM", "MG", "SO", "TZ", "UG", "YT" });
            AddCountries(TimeZoneIANAEnum.Asia_Bishkek, new string[] { "KG" });
            AddCountries(TimeZoneIANAEnum.Pacific_Tarawa, new string[] { "KI" });
            AddCountries(TimeZoneIANAEnum.Pacific_Enderbury, new string[] { "KI" });
            AddCountries(TimeZoneIANAEnum.Pacific_Kiritimati, new string[] { "KI" });
            AddCountries(TimeZoneIANAEnum.Asia_Pyongyang, new string[] { "KP" });
            AddCountries(TimeZoneIANAEnum.Asia_Seoul, new string[] { "KR" });
            AddCountries(TimeZoneIANAEnum.Asia_Almaty, new string[] { "KZ" });
            AddCountries(TimeZoneIANAEnum.Asia_Qyzylorda, new string[] { "KZ" });
            AddCountries(TimeZoneIANAEnum.Asia_Aqtobe, new string[] { "KZ" });
            AddCountries(TimeZoneIANAEnum.Asia_Aqtau, new string[] { "KZ" });
            AddCountries(TimeZoneIANAEnum.Asia_Atyrau, new string[] { "KZ" });
            AddCountries(TimeZoneIANAEnum.Asia_Oral, new string[] { "KZ" });
            AddCountries(TimeZoneIANAEnum.Asia_Beirut, new string[] { "LB" });
            AddCountries(TimeZoneIANAEnum.Asia_Colombo, new string[] { "LK" });
            AddCountries(TimeZoneIANAEnum.Africa_Monrovia, new string[] { "LR" });
            AddCountries(TimeZoneIANAEnum.Europe_Vilnius, new string[] { "LT" });
            AddCountries(TimeZoneIANAEnum.Europe_Luxembourg, new string[] { "LU" });
            AddCountries(TimeZoneIANAEnum.Europe_Riga, new string[] { "LV" });
            AddCountries(TimeZoneIANAEnum.Africa_Tripoli, new string[] { "LY" });
            AddCountries(TimeZoneIANAEnum.Africa_Casablanca, new string[] { "MA" });
            AddCountries(TimeZoneIANAEnum.Europe_Monaco, new string[] { "MC" });
            AddCountries(TimeZoneIANAEnum.Europe_Chisinau, new string[] { "MD" });
            AddCountries(TimeZoneIANAEnum.Pacific_Majuro, new string[] { "MH" });
            AddCountries(TimeZoneIANAEnum.Pacific_Kwajalein, new string[] { "MH" });
            AddCountries(TimeZoneIANAEnum.Asia_Ulaanbaatar, new string[] { "MN" });
            AddCountries(TimeZoneIANAEnum.Asia_Hovd, new string[] { "MN" });
            AddCountries(TimeZoneIANAEnum.Asia_Choibalsan, new string[] { "MN" });
            AddCountries(TimeZoneIANAEnum.Asia_Macau, new string[] { "MO" });
            AddCountries(TimeZoneIANAEnum.America_Martinique, new string[] { "MQ" });
            AddCountries(TimeZoneIANAEnum.America_Montserrat, new string[] { "MS" });
            AddCountries(TimeZoneIANAEnum.Europe_Malta, new string[] { "MT" });
            AddCountries(TimeZoneIANAEnum.Indian_Mauritius, new string[] { "MU" });
            AddCountries(TimeZoneIANAEnum.Indian_Maldives, new string[] { "MV" });
            AddCountries(TimeZoneIANAEnum.America_Mexico_City, new string[] { "MX" });
            AddCountries(TimeZoneIANAEnum.America_Cancun, new string[] { "MX" });
            AddCountries(TimeZoneIANAEnum.America_Merida, new string[] { "MX" });
            AddCountries(TimeZoneIANAEnum.America_Monterrey, new string[] { "MX" });
            AddCountries(TimeZoneIANAEnum.America_Matamoros, new string[] { "MX" });
            AddCountries(TimeZoneIANAEnum.America_Mazatlan, new string[] { "MX" });
            AddCountries(TimeZoneIANAEnum.America_Chihuahua, new string[] { "MX" });
            AddCountries(TimeZoneIANAEnum.America_Ojinaga, new string[] { "MX" });
            AddCountries(TimeZoneIANAEnum.America_Hermosillo, new string[] { "MX" });
            AddCountries(TimeZoneIANAEnum.America_Tijuana, new string[] { "MX" });
            AddCountries(TimeZoneIANAEnum.America_Santa_Isabel, new string[] { "MX" });
            AddCountries(TimeZoneIANAEnum.America_Bahia_Banderas, new string[] { "MX" });
            AddCountries(TimeZoneIANAEnum.Asia_Kuala_Lumpur, new string[] { "MY" });
            AddCountries(TimeZoneIANAEnum.Asia_Kuching, new string[] { "MY" });
            AddCountries(TimeZoneIANAEnum.Africa_Maputo, new string[] { "MZ", "BI", "BW", "CD", "MW", "RW", "ZM", "ZW" });
            AddCountries(TimeZoneIANAEnum.Africa_Windhoek, new string[] { "NA" });
            AddCountries(TimeZoneIANAEnum.Pacific_Noumea, new string[] { "NC" });
            AddCountries(TimeZoneIANAEnum.Pacific_Norfolk, new string[] { "NF" });
            AddCountries(TimeZoneIANAEnum.Africa_Lagos, new string[] { "NG", "AO", "BJ", "CD", "CF", "CG", "CM", "GA", "GQ", "NE" });
            AddCountries(TimeZoneIANAEnum.America_Managua, new string[] { "NI" });
            AddCountries(TimeZoneIANAEnum.Europe_Amsterdam, new string[] { "NL" });
            AddCountries(TimeZoneIANAEnum.Europe_Oslo, new string[] { "NO", "SJ" });
            AddCountries(TimeZoneIANAEnum.Asia_Kathmandu, new string[] { "NP" });
            AddCountries(TimeZoneIANAEnum.Pacific_Nauru, new string[] { "NR" });
            AddCountries(TimeZoneIANAEnum.Pacific_Niue, new string[] { "NU" });
            AddCountries(TimeZoneIANAEnum.Pacific_Auckland, new string[] { "NZ", "AQ" });
            AddCountries(TimeZoneIANAEnum.Pacific_Chatham, new string[] { "NZ" });
            AddCountries(TimeZoneIANAEnum.America_Panama, new string[] { "PA", "KY" });
            AddCountries(TimeZoneIANAEnum.America_Lima, new string[] { "PE" });
            AddCountries(TimeZoneIANAEnum.Pacific_Tahiti, new string[] { "PF" });
            AddCountries(TimeZoneIANAEnum.Pacific_Johnston, new string[] { "UM" });
            AddCountries(TimeZoneIANAEnum.Pacific_Marquesas, new string[] { "PF" });
            AddCountries(TimeZoneIANAEnum.Pacific_Gambier, new string[] { "PF" });
            AddCountries(TimeZoneIANAEnum.Pacific_Port_Moresby, new string[] { "PG" });
            AddCountries(TimeZoneIANAEnum.Pacific_Bougainville, new string[] { "PG" });
            AddCountries(TimeZoneIANAEnum.Asia_Manila, new string[] { "PH" });
            AddCountries(TimeZoneIANAEnum.Asia_Karachi, new string[] { "PK" });
            AddCountries(TimeZoneIANAEnum.Europe_Warsaw, new string[] { "PL" });
            AddCountries(TimeZoneIANAEnum.Europe_Sarajevo, new string[] { "BA" });
            AddCountries(TimeZoneIANAEnum.Europe_Zagreb, new string[] { "HR" });
            AddCountries(TimeZoneIANAEnum.Europe_Skopje, new string[] { "MK" });
            AddCountries(TimeZoneIANAEnum.America_Miquelon, new string[] { "PM" });
            AddCountries(TimeZoneIANAEnum.Pacific_Pitcairn, new string[] { "PN" });
            AddCountries(TimeZoneIANAEnum.America_Puerto_Rico, new string[] { "PR" });
            AddCountries(TimeZoneIANAEnum.America_Lower_Princes, new string[] { "SX" });
            AddCountries(TimeZoneIANAEnum.Asia_Gaza, new string[] { "PS" });
            AddCountries(TimeZoneIANAEnum.Asia_Hebron, new string[] { "PS" });
            AddCountries(TimeZoneIANAEnum.Europe_Lisbon, new string[] { "PT" });
            AddCountries(TimeZoneIANAEnum.Atlantic_Madeira, new string[] { "PT" });
            AddCountries(TimeZoneIANAEnum.Atlantic_Azores, new string[] { "PT" });
            AddCountries(TimeZoneIANAEnum.Pacific_Palau, new string[] { "PW" });
            AddCountries(TimeZoneIANAEnum.America_Asuncion, new string[] { "PY" });
            AddCountries(TimeZoneIANAEnum.Asia_Qatar, new string[] { "QA", "BH" });
            AddCountries(TimeZoneIANAEnum.Indian_Reunion, new string[] { "RE", "TF" });
            AddCountries(TimeZoneIANAEnum.Europe_Bucharest, new string[] { "RO" });
            AddCountries(TimeZoneIANAEnum.Europe_Belgrade, new string[] { "RS", "BA", "HR", "ME", "MK", "SI" });
            AddCountries(TimeZoneIANAEnum.Europe_Kaliningrad, new string[] { "RU" });
            AddCountries(TimeZoneIANAEnum.Europe_Moscow, new string[] { "RU" });
            AddCountries(TimeZoneIANAEnum.Europe_Simferopol, new string[] { "RU" });
            AddCountries(TimeZoneIANAEnum.Europe_Volgograd, new string[] { "RU" });
            AddCountries(TimeZoneIANAEnum.Europe_Kirov, new string[] { "RU" });
            AddCountries(TimeZoneIANAEnum.Europe_Astrakhan, new string[] { "RU" });
            AddCountries(TimeZoneIANAEnum.Europe_Saratov, new string[] { "RU" });
            AddCountries(TimeZoneIANAEnum.Europe_Ulyanovsk, new string[] { "RU" });
            AddCountries(TimeZoneIANAEnum.Europe_Samara, new string[] { "RU" });
            AddCountries(TimeZoneIANAEnum.Asia_Yekaterinburg, new string[] { "RU" });
            AddCountries(TimeZoneIANAEnum.Asia_Omsk, new string[] { "RU" });
            AddCountries(TimeZoneIANAEnum.Asia_Novosibirsk, new string[] { "RU" });
            AddCountries(TimeZoneIANAEnum.Asia_Barnaul, new string[] { "RU" });
            AddCountries(TimeZoneIANAEnum.Asia_Tomsk, new string[] { "RU" });
            AddCountries(TimeZoneIANAEnum.Asia_Novokuznetsk, new string[] { "RU" });
            AddCountries(TimeZoneIANAEnum.Asia_Krasnoyarsk, new string[] { "RU" });
            AddCountries(TimeZoneIANAEnum.Asia_Irkutsk, new string[] { "RU" });
            AddCountries(TimeZoneIANAEnum.Asia_Chita, new string[] { "RU" });
            AddCountries(TimeZoneIANAEnum.Asia_Yakutsk, new string[] { "RU" });
            AddCountries(TimeZoneIANAEnum.Asia_Khandyga, new string[] { "RU" });
            AddCountries(TimeZoneIANAEnum.Asia_Vladivostok, new string[] { "RU" });
            AddCountries(TimeZoneIANAEnum.Asia_Ust_Nera, new string[] { "RU" });
            AddCountries(TimeZoneIANAEnum.Asia_Magadan, new string[] { "RU" });
            AddCountries(TimeZoneIANAEnum.Asia_Sakhalin, new string[] { "RU" });
            AddCountries(TimeZoneIANAEnum.Asia_Srednekolymsk, new string[] { "RU" });
            AddCountries(TimeZoneIANAEnum.Asia_Kamchatka, new string[] { "RU" });
            AddCountries(TimeZoneIANAEnum.Asia_Anadyr, new string[] { "RU" });
            AddCountries(TimeZoneIANAEnum.Asia_Riyadh, new string[] { "SA", "KW", "YE" });
            AddCountries(TimeZoneIANAEnum.Pacific_Guadalcanal, new string[] { "SB" });
            AddCountries(TimeZoneIANAEnum.Indian_Mahe, new string[] { "SC" });
            AddCountries(TimeZoneIANAEnum.Africa_Khartoum, new string[] { "SD", "SS" });
            AddCountries(TimeZoneIANAEnum.Europe_Stockholm, new string[] { "SE" });
            AddCountries(TimeZoneIANAEnum.Asia_Singapore, new string[] { "SG" });
            AddCountries(TimeZoneIANAEnum.America_Paramaribo, new string[] { "SR" });
            AddCountries(TimeZoneIANAEnum.America_El_Salvador, new string[] { "SV" });
            AddCountries(TimeZoneIANAEnum.Asia_Damascus, new string[] { "SY" });
            AddCountries(TimeZoneIANAEnum.America_Grand_Turk, new string[] { "TC" });
            AddCountries(TimeZoneIANAEnum.Africa_Ndjamena, new string[] { "TD" });
            AddCountries(TimeZoneIANAEnum.Indian_Kerguelen, new string[] { "TF" });
            AddCountries(TimeZoneIANAEnum.Asia_Bangkok, new string[] { "TH", "KH", "LA", "VN" });
            AddCountries(TimeZoneIANAEnum.Asia_Dushanbe, new string[] { "TJ" });
            AddCountries(TimeZoneIANAEnum.Pacific_Fakaofo, new string[] { "TK" });
            AddCountries(TimeZoneIANAEnum.Asia_Dili, new string[] { "TL" });
            AddCountries(TimeZoneIANAEnum.Asia_Ashgabat, new string[] { "TM" });
            AddCountries(TimeZoneIANAEnum.Africa_Tunis, new string[] { "TN" });
            AddCountries(TimeZoneIANAEnum.Pacific_Tongatapu, new string[] { "TO" });
            AddCountries(TimeZoneIANAEnum.Europe_Istanbul, new string[] { "TR" });
            AddCountries(TimeZoneIANAEnum.America_Port_of_Spain, new string[] { "TT", "AG", "AI", "BL", "DM", "GD", "GP", "KN", "LC", "MF", "MS", "VC", "VG", "VI" });
            AddCountries(TimeZoneIANAEnum.America_St_Vincent, new string[] { "VC" });
            AddCountries(TimeZoneIANAEnum.America_Tortola, new string[] { "VG" });
            AddCountries(TimeZoneIANAEnum.America_St_Thomas, new string[] { "VI" });
            AddCountries(TimeZoneIANAEnum.Pacific_Funafuti, new string[] { "TV" });
            AddCountries(TimeZoneIANAEnum.Asia_Taipei, new string[] { "TW" });
            AddCountries(TimeZoneIANAEnum.Europe_Kiev, new string[] { "UA" });
            AddCountries(TimeZoneIANAEnum.Europe_Uzhgorod, new string[] { "UA" });
            AddCountries(TimeZoneIANAEnum.Europe_Zaporozhye, new string[] { "UA" });
            AddCountries(TimeZoneIANAEnum.Pacific_Wake, new string[] { "UM" });
            AddCountries(TimeZoneIANAEnum.America_New_York, new string[] { "US" });
            AddCountries(TimeZoneIANAEnum.America_Detroit, new string[] { "US" });
            AddCountries(TimeZoneIANAEnum.America_Kentucky_Monticello, new string[] { "US" });
            AddCountries(TimeZoneIANAEnum.America_Louisville, new string[] { "US" });
            AddCountries(TimeZoneIANAEnum.America_Indiana_Indianapolis, new string[] { "US" });
            AddCountries(TimeZoneIANAEnum.America_Indiana_Vincennes, new string[] { "US" });
            AddCountries(TimeZoneIANAEnum.America_Indiana_Winamac, new string[] { "US" });
            AddCountries(TimeZoneIANAEnum.America_Indiana_Marengo, new string[] { "US" });
            AddCountries(TimeZoneIANAEnum.America_Indiana_Petersburg, new string[] { "US" });
            AddCountries(TimeZoneIANAEnum.America_Indiana_Vevay, new string[] { "US" });
            AddCountries(TimeZoneIANAEnum.America_Chicago, new string[] { "US" });
            AddCountries(TimeZoneIANAEnum.America_Indiana_Tell_City, new string[] { "US" });
            AddCountries(TimeZoneIANAEnum.America_Indiana_Knox, new string[] { "US" });
            AddCountries(TimeZoneIANAEnum.America_Menominee, new string[] { "US" });
            AddCountries(TimeZoneIANAEnum.America_North_Dakota_Center, new string[] { "US" });
            AddCountries(TimeZoneIANAEnum.America_North_Dakota_New_Salem, new string[] { "US" });
            AddCountries(TimeZoneIANAEnum.America_North_Dakota_Beulah, new string[] { "US" });
            AddCountries(TimeZoneIANAEnum.America_Denver, new string[] { "US" });
            AddCountries(TimeZoneIANAEnum.America_Boise, new string[] { "US" });
            AddCountries(TimeZoneIANAEnum.America_Phoenix, new string[] { "US" });
            AddCountries(TimeZoneIANAEnum.America_Los_Angeles, new string[] { "US" });
            AddCountries(TimeZoneIANAEnum.America_Anchorage, new string[] { "US" });
            AddCountries(TimeZoneIANAEnum.America_Juneau, new string[] { "US" });
            AddCountries(TimeZoneIANAEnum.America_Sitka, new string[] { "US" });
            AddCountries(TimeZoneIANAEnum.America_Metlakatla, new string[] { "US" });
            AddCountries(TimeZoneIANAEnum.America_Yakutat, new string[] { "US" });
            AddCountries(TimeZoneIANAEnum.America_Nome, new string[] { "US" });
            AddCountries(TimeZoneIANAEnum.America_Adak, new string[] { "US" });
            AddCountries(TimeZoneIANAEnum.Pacific_Honolulu, new string[] { "US", "UM" });
            AddCountries(TimeZoneIANAEnum.America_Montevideo, new string[] { "UY" });
            AddCountries(TimeZoneIANAEnum.Asia_Samarkand, new string[] { "UZ" });
            AddCountries(TimeZoneIANAEnum.Asia_Tashkent, new string[] { "UZ" });
            AddCountries(TimeZoneIANAEnum.America_Caracas, new string[] { "VE" });
            AddCountries(TimeZoneIANAEnum.Pacific_Efate, new string[] { "VU" });
            AddCountries(TimeZoneIANAEnum.Pacific_Wallis, new string[] { "WF" });
            AddCountries(TimeZoneIANAEnum.Pacific_Apia, new string[] { "WS" });
            AddCountries(TimeZoneIANAEnum.Africa_Johannesburg, new string[] { "ZA", "LS", "SZ" });
            AddCountries(TimeZoneIANAEnum.Africa_Ouagadougou, new string[] { "BF" });
            AddCountries(TimeZoneIANAEnum.Africa_Banjul, new string[] { "GM" });
            AddCountries(TimeZoneIANAEnum.Africa_Conakry, new string[] { "GN" });
            AddCountries(TimeZoneIANAEnum.Africa_Bamako, new string[] { "ML" });
            AddCountries(TimeZoneIANAEnum.Africa_Nouakchott, new string[] { "MR" });
            AddCountries(TimeZoneIANAEnum.Atlantic_St_Helena, new string[] { "SH" });
            AddCountries(TimeZoneIANAEnum.Africa_Freetown, new string[] { "SL" });
            AddCountries(TimeZoneIANAEnum.Africa_Dakar, new string[] { "SN" });
            AddCountries(TimeZoneIANAEnum.Africa_Sao_Tome, new string[] { "ST" });
            AddCountries(TimeZoneIANAEnum.Africa_Luanda, new string[] { "AO" });
            AddCountries(TimeZoneIANAEnum.Africa_Porto_Novo, new string[] { "BJ" });
            AddCountries(TimeZoneIANAEnum.Africa_Kinshasa, new string[] { "CD" });
            AddCountries(TimeZoneIANAEnum.Africa_Bangui, new string[] { "CF" });
            AddCountries(TimeZoneIANAEnum.Africa_Brazzaville, new string[] { "CG" });
            AddCountries(TimeZoneIANAEnum.Africa_Douala, new string[] { "CM" });
            AddCountries(TimeZoneIANAEnum.Africa_Libreville, new string[] { "GA" });
            AddCountries(TimeZoneIANAEnum.Africa_Malabo, new string[] { "GQ" });
            AddCountries(TimeZoneIANAEnum.Africa_Niamey, new string[] { "NE" });
            AddCountries(TimeZoneIANAEnum.Africa_Bujumbura, new string[] { "BI" });
            AddCountries(TimeZoneIANAEnum.Africa_Gaborone, new string[] { "BW" });
            AddCountries(TimeZoneIANAEnum.Africa_Lubumbashi, new string[] { "CD" });
            AddCountries(TimeZoneIANAEnum.Africa_Maseru, new string[] { "LS" });
            AddCountries(TimeZoneIANAEnum.Africa_Blantyre, new string[] { "MW" });
            AddCountries(TimeZoneIANAEnum.Africa_Kigali, new string[] { "RW" });
            AddCountries(TimeZoneIANAEnum.Africa_Mbabane, new string[] { "SZ" });
            AddCountries(TimeZoneIANAEnum.Africa_Lusaka, new string[] { "ZM" });
            AddCountries(TimeZoneIANAEnum.Africa_Djibouti, new string[] { "DJ" });
            AddCountries(TimeZoneIANAEnum.Africa_Asmera, new string[] { "ER" });
            AddCountries(TimeZoneIANAEnum.Africa_Addis_Ababa, new string[] { "ET" });
            AddCountries(TimeZoneIANAEnum.Indian_Comoro, new string[] { "KM" });
            AddCountries(TimeZoneIANAEnum.Indian_Antananarivo, new string[] { "MG" });
            AddCountries(TimeZoneIANAEnum.Africa_Mogadishu, new string[] { "SO" });
            AddCountries(TimeZoneIANAEnum.Africa_Juba, new string[] { "SS" });
            AddCountries(TimeZoneIANAEnum.Africa_Dar_Es_Salaam, new string[] { "TZ" });
            AddCountries(TimeZoneIANAEnum.Africa_Kampala, new string[] { "UG" });
            AddCountries(TimeZoneIANAEnum.Indian_Mayotte, new string[] { "YT" });
            AddCountries(TimeZoneIANAEnum.Africa_Harare, new string[] { "ZW" });
            AddCountries(TimeZoneIANAEnum.Asia_Dubai, new string[] { "AE" });
            AddCountries(TimeZoneIANAEnum.Asia_Muscat, new string[] { "OM" });
            AddCountries(TimeZoneIANAEnum.Asia_Ekaterinburg, new string[] { "RU" });
            AddCountries(TimeZoneIANAEnum.Asia_Calcutta, new string[] { "IN" });
            AddCountries(TimeZoneIANAEnum.Asia_Phnom_Penh, new string[] { "KH" });
            AddCountries(TimeZoneIANAEnum.Asia_Rangoon, new string[] { "MM" });
            AddCountries(TimeZoneIANAEnum.Asia_Saigon, new string[] { "VN" });
            AddCountries(TimeZoneIANAEnum.Asia_Vientiane, new string[] { "LA" });
            AddCountries(TimeZoneIANAEnum.Pacific_Truk, new string[] { "FM" });
            AddCountries(TimeZoneIANAEnum.Pacific_Saipan, new string[] { "MP" });
            AddCountries(TimeZoneIANAEnum.Pacific_Ponape, new string[] { "FM" });
            AddCountries(TimeZoneIANAEnum.Antarctica_McMurdo, new string[] { "AQ" });
            AddCountries(TimeZoneIANAEnum.Africa_Lome, new string[] { "TG" });
        }

        internal static IANACityRegionEnum GetMainAreaName(string[] temp)
        {
            if (temp[0] == "None") return IANACityRegionEnum.None;
            int startI = 1;
            if (temp.Length > 2 && (temp[1] == "Argentina" || temp[1] == "Indiana" || temp[1] == "Kentucky"))
            {
                startI = 2;
            }
            else if (temp.Length > 3 && (temp[1] == "North" && temp[2] == "Dakota"))
            {
                startI = 3;
            }

            string outString = string.Join("_", temp, startI, temp.Length - startI);

            if (outString == "DumontDUrville")
            {
                outString = "Dumont_D_Urville";
            }
            
            return (IANACityRegionEnum)Enum.Parse
            (
                typeof(IANACityRegionEnum), outString
            );
        }
    }
}
