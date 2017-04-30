﻿using System;
using System.Collections.Generic;

namespace FlexibleParser
{
    public enum IANACityRegionEnum
    {
        //IANA timezone names.
        None = 0,
        Dubai, Kabul, Yerevan, Dhaka, Brunei,
        Thimphu, Shanghai, Urumqi, Nicosia,
        Famagusta, Tbilisi, Hong_Kong, Jakarta,
        Pontianak, Makassar, Jayapura, Pyongyang,
        Seoul, Almaty, Qyzylorda, Aqtobe, Aqtau,
        Atyrau, Oral, Beirut, Colombo, Jerusalem,
        Baghdad, Tehran, Amman, Tokyo,
        Bishkek, Ulaanbaatar, Hovd,
        Choibalsan, Macau, Kuala_Lumpur, Kuching,
        Kathmandu, Manila, Karachi, Gaza, Hebron,
        Qatar, Yekaterinburg, Omsk, Novosibirsk,
        Barnaul, Tomsk, Novokuznetsk, Krasnoyarsk,
        Irkutsk, Chita, Yakutsk, Khandyga,
        Vladivostok, Ust_Nera, Magadan, Sakhalin,
        Srednekolymsk, Kamchatka, Anadyr, Riyadh,
        Singapore, Damascus, Samarkand,
        Dili, Ashgabat, Tashkent, Bangkok,
        Dushanbe, Taipei, Baku, Bahrain, Kuwait,
        Aden, Muscat, Ekaterinburg, Calcutta,
        Rangoon, Phnom_Penh, Saigon, Vientiane,
        Andorra, Tirane, Vienna, Brussels,
        Sofia, Minsk, Zurich, Prague,
        Berlin, Copenhagen, Tallinn, Madrid,
        Paris, London, Helsinki, Gibraltar,
        Athens, Budapest, Dublin, Rome,
        Vilnius, Luxembourg, Riga, Monaco,
        Chisinau, Malta, Amsterdam, Oslo,
        Warsaw, Lisbon, Bucharest, Belgrade,
        Kaliningrad, Moscow, Simferopol,
        Volgograd, Kirov, Astrakhan, Saratov,
        Ulyanovsk, Samara, Stockholm, Istanbul,
        Kiev, Uzhgorod, Zaporozhye, Guernsey,
        Isle_Of_Man, Jersey, Vaduz, Busingen,
        Longyearbyen, San_Marino, Vatican,
        Podgorica, Ljubljana, Bratislava,
        Sarajevo, Zagreb, Skopje, Mariehamn,
        Casey, Davis, Dumont_D_Urville,
        Mawson, Palmer, Rothera,
        Syowa, Vostok,
        Macquarie, McMurdo,
        Buenos_Aires, Cordoba,
        Salta, Jujuy,
        Tucuman, Catamarca,
        La_Rioja, San_Juan,
        Mendoza, San_Luis,
        Rio_Gallegos, Ushuaia,
        Barbados, La_Paz, Kralendijk,
        Noronha, Dominica, Grenada,
        Guadeloupe, St_Kitts, St_Lucia,
        Belem, Fortaleza, Recife,
        Araguaina, Maceio, Bahia,
        Sao_Paulo, Campo_Grande, Cuiaba,
        Santarem, Porto_Velho, Boa_Vista,
        Manaus, Eirunepe, Rio_Branco,
        Nassau, Belize, St_Johns,
        Halifax, Glace_Bay, Moncton,
        Goose_Bay, Blanc_Sablon,
        Toronto, Nipigon, Thunder_Bay,
        Iqaluit, Pangnirtung, Atikokan,
        Winnipeg, Rainy_River, Resolute,
        Rankin_Inlet, Regina, Swift_Current,
        Edmonton, Cambridge_Bay, Yellowknife,
        Inuvik, Creston, Dawson_Creek,
        Fort_Nelson, Vancouver, Whitehorse,
        Dawson, Santiago, Punta_Arenas,
        Bogota, Costa_Rica, Havana,
        Curacao, Santo_Domingo, Guayaquil,
        Cayenne, Godthab, Danmarkshavn,
        Scoresbysund, Thule, Guatemala,
        Guyana, Tegucigalpa, Port_au_Prince,
        Jamaica, Martinique, Montserrat,
        Mexico_City,
        Cancun, Merida, Monterrey,
        Matamoros, Mazatlan, Chihuahua,
        Ojinaga, Hermosillo, Tijuana,
        Bahia_Banderas, Managua, Panama,
        Port_of_Spain, St_Vincent, Tortola,
        St_Thomas, New_York, Detroit,
        Monticello,
        Indianapolis, Vincennes,
        Winamac, Marengo,
        Petersburg, Vevay,
        Chicago, Tell_City,
        Knox, Menominee, Center,
        New_Salem, Beulah,
        Denver, Boise, Phoenix, Los_Angeles,
        Anchorage, Juneau, Sitka, Metlakatla,
        Yakutat, Nome, Adak, Montevideo,
        Caracas, Lima, Miquelon, Puerto_Rico,
        Asuncion, Paramaribo, El_Salvador,
        Grand_Turk, Santa_Isabel, Coral_Harbour,
        Cayman, Montreal, Louisville,
        Antigua, Aruba, St_Barthelemy,
        Pago_Pago, Rarotonga, Easter, Galapagos,
        Fiji, Chuuk, Pohnpei, Kosrae,
        Guam, Noumea, Norfolk, Tarawa,
        Enderbury, Kiritimati, Majuro, Kwajalein,
        Nauru, Niue, Auckland, Chatham,
        Tahiti, Marquesas, Gambier, Port_Moresby,
        Bougainville, Pitcairn, Palau, Guadalcanal,
        Funafuti, Fakaofo, Tongatapu, Wake,
        Efate, Honolulu, Wallis, Apia,
        Midway, Johnston, Marigot, Lower_Princes,
        Truk, Saipan, Ponape,
        Lord_Howe, Hobart, Currie,
        Melbourne, Sydney, Broken_Hill,
        Brisbane, Lindeman, Adelaide,
        Darwin, Perth, Eucla,
        Bermuda, Cape_Verde, Stanley, Canary,
        South_Georgia, Reykjavik, Madeira,
        Azores, Faeroe,
        Cocos, Christmas, Chagos, Mauritius,
        Maldives, Reunion, Mahe, Kerguelen,
        Comoro, Antananarivo, Mayotte,
        Abidjan, Algiers, Cairo, El_Aaiun,
        Ceuta, Lagos, Accra, Bissau,
        Nairobi, Monrovia, Tripoli, Casablanca,
        Maputo, Windhoek, Khartoum, Ndjamena,
        Tunis, Johannesburg, Ouagadougou, Banjul,
        Conakry, Bamako, Nouakchott, St_Helena,
        Freetown, Dakar, Sao_Tome, Lome, Luanda,
        Porto_Novo, Kinshasa, Bangui, Brazzaville,
        Douala, Libreville, Malabo, Niamey,
        Bujumbura, Gaborone, Lubumbashi, Maseru,
        Blantyre, Kigali, Mbabane, Lusaka,
        Harare, Djibouti, Asmera, Addis_Ababa,
        Mogadishu, Juba, Dar_Es_Salaam, Kampala,

        //Other cities/regions, listed in zone1970.tab and not included in any timezone name.
        Distrito_Federal,
        Chaco, Entre_Ríos, Misiones,
        Santiago_del_Estero, Santa_Fe,
        La_Pampa, Neuquén, Río_Negro,
        Chubut, Samoa, Tasmania,
        King_Island, Victoria, New_South_Walles,
        Yancowinna, Queensland, Whitsunday_Islands,
        Southern_Australia, Northern_Territory,
        Western_Australia, Atlantic_Islands,
        Eastern_Pará, Amapá, Northeastern_Brazil,
        Maranhão, Piauí, Ceará, Rio_Grande_do_Norte,
        Paraíba, Pernambuco, Tocantins,
        Alagoas, Sergipe, São_Paulo,
        Southeastern_Brazil, Goiás, Minas_Gerais,
        Espírito_Santo, Rio_de_Janeiro, Paraná,
        Santa_Catarina, Rio_Grande_do_Sul,
        Mato_Grosso_do_Sul, Mato_Grosso,
        Western_Pará, Rondônia,
        Roraima, Eastern_Amazonas,
        Western_Amazonas, Acre,
        Newfoundland, Southeastern_Labrador,
        Nova_Scotia, Prince_Edward_Island,
        Cape_Breton, New_Brunswick,
        Labrador, Lower_North_Shore,
        Ontario, Quebec, Nunavut, Western_Ontario, 
        Manitoba, Fort_Frances,
        Central_Nunavut, Saskatchewan,
        Midwestern_Saskatchewan, Alberta,
        Eastern_British_Columbia,
        Western_Saskatchewan, Western_Nunavut,
        Central_Northwest_Territories,
        Western_Northwest_Territories,
        Fort_St_John, British_Columbia,
        Southern_Yucon, Northern_Yucon,
        Region_of_Magallanes, Beijing,
        Xinjiang, Northern_Cyprus,
        Melilla, Yap, National_Park, 
        Ittoqqortoormiit, Pituffik, Java, Sumatra,
        Western_Borneo, Central_Borneo,
        Eastern_Borneo, Southern_Borneo,
        Sulawesi, Celebes, Bali,
        Nusa_Tengarra, Western_Timor,
        West_Papua, Irian_Jaya, Malukus,
        Moluccas, Gilbert_Islands,
        Phoenix_Islands, Line_Islands,
        Kazakhstan, Kyzylorda, Kzyl_Orda,
        Aqtöbe, Aktobe, Mangghystaū,
        Mankistau, Atyraū, Atirau, Gur_Yev,
        West_Kazakhstan, Bayan_Ölgii,
        Govi_Altai, Uvs, Zavkhan, Dornod,
        Sükhbaatar, Quintana_Roo,
        Campeche, Yucatán, Durango,
        Coahuila, Nuevo_León, Tamaulipas,
        US_border_Coahuila, US_border_Nuevo_León,
        US_border_Tamaulipas, Baja_California_Sur,
        Nayarit, Sinaloa, US_border_Chihuahua,
        Sonora, Baja_California, Bahía_de_Banderas,
        Malaysia, Sabah, Sarawak, Society_Islands,
        Gaza_Strip, West_Bank, Réunion, Crozet,
        Scattered_Islands, Crimea, Udmurtia,
        Urals, Altai, Kemerovo, Krasnoyarsk_area,
        Buryatia, Zabaykalsky, Lena_River,
        Tomponsky, Ust_Maysky, Amur_River,
        Oymyakonsky, Eastern_Sakha, North_Kuril_Islands,
        Bering_Sea, St_Paul_Island, Amsterdam_Island,
        Indochina, Zaporozh_Ye, Zaporizhia,
        Eastern_Lugansk, Eastern_Luhansk, Michigan,
        Wayne, Daviess, Dubois, Martin,
        Pulaski, Crawford, Pike, Switzerland,
        Perry, Starke, Wisconsin_border_Michigan,
        Oliver, Morton_rural, Mercer, Arizona,
        Juneau_area, Sitka_area, Annette_Island,
        Alaska_West, Aleutian_Islands, Hawaii,
        Western_Uzbekistan, Eastern_Uzbekistan,
        Southern_Vietnam 
    }
}
