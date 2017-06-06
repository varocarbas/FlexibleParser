using System;
using System.Linq;
using System.Collections.Generic;

namespace FlexibleParser
{
    internal class TimeZonesMainMap
    {
        private string ConventionalName { get; set; } 
        public IEnumerable<TimeZoneConventionalEnum> ConventionalTimeZones { get; set; }
        public IEnumerable<TimeZoneOfficialEnum> OfficialTimeZones { get; set; }
        public IEnumerable<TimeZoneIANAEnum> IANATimeZones { get; set; }
        public TimeZoneWindowsEnum WindowsTimeZone { get; set; }
        public TimeZoneUTCEnum UTCTimeZone { get; set; }
        public TimeZoneMilitaryEnum MilitaryTimeZone { get; set; }

        public TimeZonesMainMap
        (
            string name, decimal decimalOffset, TimeZoneWindowsEnum windowsZone, IEnumerable<TimeZoneIANAEnum> ianaZones
        )
        : this
        (
            name, TimeZoneUTCInternal.GetTimeZoneUTCFromDecimalOffset(decimalOffset), windowsZone, ianaZones
        ) 
        { }

        public TimeZonesMainMap
        (
            string name, TimeZoneUTCEnum timezoneUTC, TimeZoneWindowsEnum windowsZone, IEnumerable<TimeZoneIANAEnum> ianaZones
        )
        {
            if (name == null)
            {
                ConventionalTimeZones = new TimeZoneConventionalEnum[] { TimeZoneConventionalEnum.None };
                OfficialTimeZones = new TimeZoneOfficialEnum[] { TimeZoneOfficialEnum.None };
                IANATimeZones = new TimeZoneIANAEnum[] { TimeZoneIANAEnum.None };
                return;
            }

            ConventionalName = name;
            UTCTimeZone = timezoneUTC;
            OfficialTimeZones = TimeZoneOfficialInternal.GetOfficialTimezonesFromUTC(UTCTimeZone);
            WindowsTimeZone = windowsZone;
            MilitaryTimeZone = TimeZoneMilitaryInternal.GetMilitaryTimeZoneFromUTC(UTCTimeZone);

            if 
            (
                windowsZone == TimeZoneWindowsEnum.Mid_Atlantic_Standard_Time || 
                windowsZone == TimeZoneWindowsEnum.Kamchatka_Standard_Time
            )
            {
                ConventionalTimeZones = new TimeZoneConventionalEnum[] { };
                IANATimeZones = new TimeZoneIANAEnum[] { };
            }
            else
            {
                ConventionalTimeZones = TimeZoneConventionalInternal.GetConventionalTimezonesFromString(ConventionalName);
                IANATimeZones = ianaZones;
            }
        }

        //This constructor is exclusively meant to be used with unmapped (i.e., with a Windows timezone equivalence) UTC timezones.
        public TimeZonesMainMap(TimeZoneUTCEnum utc)
        {
            ConventionalTimeZones = new TimeZoneConventionalEnum[] { };
            UTCTimeZone = utc;
            OfficialTimeZones = TimeZoneOfficialInternal.GetOfficialTimezonesFromUTC(UTCTimeZone);
            IANATimeZones = new TimeZoneIANAEnum[] { };
        }
    }

    internal partial class TimeZonesInternal
    {
        internal static Type[] TimeZoneEnumTypes;
        internal static List<TimeZonesMainMap> AllTimezonesInternal_Temp;
        internal static TimeZonesMainMap[] AllTimezonesInternal;
        internal static Dictionary<dynamic, string> AllNames;
        internal static Dictionary<Type, dynamic> NoneItems;
        private static bool Populated = StartTimezones();

        //Making StartTimezones a bool function (always returning true) rather than the 
        //a-priori-more-logical void is a merely instrumental resource to allow it to be 
        //called via global variables at class instantiation. 
        internal static bool StartTimezones()
        {
            if (!Populated)
            {
                //Setting Timezones.Populated to true is required to avoid infinite recursions.
                //The remaining variables (e.g., TimeZoneConventional.Populated) aren't expected
                //to be used other than as mere placeholders to call this method at class instantiation.
                Populated = true;
                CountryInternal.PopulateGlobalTemp();

                PopulateEnumToClassMap();
                GetAllNoneItems();
                GetAllNames();
                TimeZoneWindowsInternal.PopulateMain();
                TimeZoneIANAInternal.PopulateMain();
                TimeZoneConventionalInternal.PopulateMain();
                TimeZoneMilitaryInternal.PopulateMain();
                PopulateTimezoneMappingMain();

                TimeZonesCountryInternal.PopulateMainDictionary();

                CountryInternal.CodeCountryTemp = null;
            }

            return true;
        }

        private static void PopulateEnumToClassMap()
        {
            TimeZoneEnumTypes = new Type[]
            {
                typeof(TimeZoneOfficialEnum), typeof(TimeZoneIANAEnum), 
                typeof(TimeZoneConventionalEnum), typeof(TimeZoneUTCEnum), 
                typeof(TimeZoneWindowsEnum), typeof(TimeZoneMilitaryEnum)
            };
        }

        private static void GetAllNoneItems()
        {
            NoneItems = new Dictionary<Type,dynamic>()
            {
                { typeof(TimeZoneOfficialEnum), TimeZoneOfficialEnum.None },
                { typeof(TimeZoneIANAEnum), TimeZoneIANAEnum.None },
                { typeof(TimeZoneConventionalEnum), TimeZoneConventionalEnum.None },
                { typeof(TimeZoneUTCEnum), TimeZoneUTCEnum.None },
                { typeof(TimeZoneWindowsEnum), TimeZoneWindowsEnum.None },
                { typeof(TimeZoneMilitaryEnum), TimeZoneMilitaryEnum.None }
            };
        }

        private static void GetAllNames()
        {
            AllNames = GetAllNamesInternal().ToDictionary(x => x.Key, x => x.Value);
        }

        private static IEnumerable<KeyValuePair<dynamic, string>> GetAllNamesInternal()
        {
            foreach (Type type in TimeZoneEnumTypes)
            {
                foreach (var item in Enum.GetValues(type))
                {
                    yield return
                    (
                        EnumIsNothing(item) ? new KeyValuePair<dynamic, string>
                        (
                            item, "None"
                        ) 
                        : new KeyValuePair<dynamic, string>
                        (
                            item, GetEnumItemName(item, type)
                        )
                    );
                }
            }
        }

        //This method populates the global collection AllTimezonesInternal (indirectly, via AllTimezonesInternal_Temp) which represents
        //the main reference used by this library to related timezones of different types.
        //Most of this information comes from http://unicode.org/repos/cldr/trunk/common/supplemental/windowsZones.xml, by also bearing
        //in mind the exceptions listed in http://cldr.unicode.org/development/development-process/design-proposals/extended-windows-olson-zid-mapping#TOC-Unmappable-Windows-Time-Zone-Mid-Atlantic-Standard-Time-.
        private static void PopulateTimezoneMappingMain()
        {
            AllTimezonesInternal_Temp = new List<TimeZonesMainMap>();

            AddTimezoneMappingItem
            (
                "International Date Line West", -12m, TimeZoneWindowsEnum.Dateline_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Etc_GMT_Plus_12 }
            );

            AddTimezoneMappingItem
            (
                "Coordinated Universal Time-11", -11m, TimeZoneWindowsEnum.UTC_Minus_11,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Etc_GMT_Plus_11, TimeZoneIANAEnum.Pacific_Pago_Pago,
                    TimeZoneIANAEnum.Pacific_Niue, TimeZoneIANAEnum.Pacific_Midway
                }
            );

            AddTimezoneMappingItem
            (
                "Aleutian Islands", -10m, TimeZoneWindowsEnum.Aleutian_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.America_Adak }
            );

            AddTimezoneMappingItem
            (
                "Hawaii", -10m, TimeZoneWindowsEnum.Hawaiian_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Pacific_Honolulu, TimeZoneIANAEnum.Pacific_Rarotonga,
                    TimeZoneIANAEnum.Pacific_Tahiti, TimeZoneIANAEnum.Pacific_Johnston,
                    TimeZoneIANAEnum.Etc_GMT_Plus_10
                }
            );

            AddTimezoneMappingItem
            (
                "Marquesas Islands", -9.5m, TimeZoneWindowsEnum.Marquesas_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Pacific_Marquesas }
            );

            AddTimezoneMappingItem
            (
                "Alaska", -9m, TimeZoneWindowsEnum.Alaskan_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.America_Anchorage, TimeZoneIANAEnum.America_Juneau,
                    TimeZoneIANAEnum.America_Metlakatla, TimeZoneIANAEnum.America_Nome,
                    TimeZoneIANAEnum.America_Sitka, TimeZoneIANAEnum.America_Yakutat
                }
            );

            AddTimezoneMappingItem
            (
                "Coordinated Universal Time-09", -9m, TimeZoneWindowsEnum.UTC_Minus_09,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Etc_GMT_Plus_9, TimeZoneIANAEnum.Pacific_Gambier
                }
            );

            AddTimezoneMappingItem
            (
                "Baja California", -8m, TimeZoneWindowsEnum.Pacific_Standard_Time_Mexico,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.America_Tijuana, TimeZoneIANAEnum.America_Santa_Isabel
                }
            );

            AddTimezoneMappingItem
            (
                "Coordinated Universal Time-08", -8m, TimeZoneWindowsEnum.UTC_Minus_08,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Etc_GMT_Plus_8, TimeZoneIANAEnum.Pacific_Pitcairn
                }
            );

            AddTimezoneMappingItem
            (
                "Pacific Time (US & Canada)", -8m, TimeZoneWindowsEnum.Pacific_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.America_Los_Angeles, TimeZoneIANAEnum.America_Vancouver,
                    TimeZoneIANAEnum.America_Dawson, TimeZoneIANAEnum.America_Whitehorse,
                    TimeZoneIANAEnum.PST8PDT
                }
            );

            AddTimezoneMappingItem
            (
                "Arizona", -7m, TimeZoneWindowsEnum.US_Mountain_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.America_Phoenix, TimeZoneIANAEnum.America_Dawson_Creek,
                    TimeZoneIANAEnum.America_Creston, TimeZoneIANAEnum.America_Fort_Nelson,
                    TimeZoneIANAEnum.America_Hermosillo, TimeZoneIANAEnum.Etc_GMT_Plus_7
                }
            );

            AddTimezoneMappingItem
            (
                "Chihuahua, La Paz, Mazatlan", -7m, TimeZoneWindowsEnum.Mountain_Standard_Time_Mexico,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.America_Chihuahua, TimeZoneIANAEnum.America_Mazatlan
                }
            );

            AddTimezoneMappingItem
            (
                "Mountain Time (US & Canada)", -7m, TimeZoneWindowsEnum.Mountain_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.America_Denver, TimeZoneIANAEnum.America_Edmonton,
                    TimeZoneIANAEnum.America_Cambridge_Bay, TimeZoneIANAEnum.America_Inuvik,
                    TimeZoneIANAEnum.America_Yellowknife, TimeZoneIANAEnum.America_Ojinaga,
                    TimeZoneIANAEnum.America_Boise, TimeZoneIANAEnum.MST7MDT
                }
            );

            AddTimezoneMappingItem
            (
                "Central America", -6m, TimeZoneWindowsEnum.Central_America_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.America_Guatemala, TimeZoneIANAEnum.America_Belize,
                    TimeZoneIANAEnum.America_Costa_Rica, TimeZoneIANAEnum.Pacific_Galapagos,
                    TimeZoneIANAEnum.America_Tegucigalpa, TimeZoneIANAEnum.America_Managua, 
                    TimeZoneIANAEnum.America_El_Salvador, TimeZoneIANAEnum.Etc_GMT_Plus_6
                }
            );

            AddTimezoneMappingItem
            (
                "Central Time (US & Canada)", -6m, TimeZoneWindowsEnum.Central_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.America_Chicago, TimeZoneIANAEnum.America_Winnipeg,
                    TimeZoneIANAEnum.America_Rainy_River, TimeZoneIANAEnum.America_Rankin_Inlet,
                    TimeZoneIANAEnum.America_Resolute, TimeZoneIANAEnum.America_Matamoros,
                    TimeZoneIANAEnum.America_Indiana_Knox, TimeZoneIANAEnum.America_Indiana_Tell_City, 
                    TimeZoneIANAEnum.America_Menominee, TimeZoneIANAEnum.America_North_Dakota_Beulah, 
                    TimeZoneIANAEnum.America_North_Dakota_Center, TimeZoneIANAEnum.America_North_Dakota_New_Salem
                }
            );

            AddTimezoneMappingItem
            (
                "Easter Island", -6m, TimeZoneWindowsEnum.Easter_Island_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Pacific_Easter }
            );

            AddTimezoneMappingItem
            (
                "Guadalajara, Mexico City, Monterrey", -6m, TimeZoneWindowsEnum.Central_Standard_Time_Mexico,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.America_Mexico_City, TimeZoneIANAEnum.America_Bahia_Banderas,
                    TimeZoneIANAEnum.America_Merida, TimeZoneIANAEnum.America_Monterrey
                }
            );

            AddTimezoneMappingItem
            (
                "Saskatchewan", -6m, TimeZoneWindowsEnum.Canada_Central_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.America_Regina, TimeZoneIANAEnum.America_Swift_Current
                }
            );

            AddTimezoneMappingItem
            (
                "Bogota, Lima, Quito, Rio Branco", -5m, TimeZoneWindowsEnum.SA_Pacific_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.America_Bogota, TimeZoneIANAEnum.America_Rio_Branco,
                    TimeZoneIANAEnum.America_Eirunepe, TimeZoneIANAEnum.America_Coral_Harbour,
                    TimeZoneIANAEnum.America_Guayaquil, TimeZoneIANAEnum.America_Jamaica, 
                    TimeZoneIANAEnum.America_Cayman, TimeZoneIANAEnum.America_Panama, 
                    TimeZoneIANAEnum.America_Lima, TimeZoneIANAEnum.Etc_GMT_Plus_5
                }
            );

            AddTimezoneMappingItem
            (
                "Chetumal", -5m, TimeZoneWindowsEnum.Eastern_Standard_Time_Mexico,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.America_Cancun }
            );

            AddTimezoneMappingItem
            (
                "Eastern Time (US & Canada)", -5m, TimeZoneWindowsEnum.Eastern_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.America_New_York, TimeZoneIANAEnum.America_Nassau,
                    TimeZoneIANAEnum.America_Toronto, TimeZoneIANAEnum.America_Iqaluit,
                    TimeZoneIANAEnum.America_Montreal, TimeZoneIANAEnum.America_Nipigon,
                    TimeZoneIANAEnum.America_Pangnirtung, TimeZoneIANAEnum.America_Thunder_Bay,
                    TimeZoneIANAEnum.America_Detroit, TimeZoneIANAEnum.America_Indiana_Petersburg,
                    TimeZoneIANAEnum.America_Indiana_Vincennes, TimeZoneIANAEnum.America_Indiana_Winamac,
                    TimeZoneIANAEnum.America_Kentucky_Monticello, TimeZoneIANAEnum.America_Louisville
                }
            );

            AddTimezoneMappingItem
            (
                "Haiti", -5m, TimeZoneWindowsEnum.Haiti_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.America_Port_au_Prince }
            );

            AddTimezoneMappingItem
            (
                "Havana", -5m, TimeZoneWindowsEnum.Cuba_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.America_Havana }
            );

            AddTimezoneMappingItem
            (
                "Indiana (East)", -5m, TimeZoneWindowsEnum.US_Eastern_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.America_Indiana_Indianapolis, TimeZoneIANAEnum.America_Indiana_Marengo,
                    TimeZoneIANAEnum.America_Indiana_Vevay
                }
            );

            AddTimezoneMappingItem
            (
                "Asuncion", -4m, TimeZoneWindowsEnum.Paraguay_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.America_Asuncion }
            );

            AddTimezoneMappingItem
            (
                "Atlantic Time (Canada)", -4m, TimeZoneWindowsEnum.Atlantic_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.America_Halifax, TimeZoneIANAEnum.Atlantic_Bermuda,
                    TimeZoneIANAEnum.America_Glace_Bay, TimeZoneIANAEnum.America_Goose_Bay, 
                    TimeZoneIANAEnum.America_Moncton, TimeZoneIANAEnum.America_Thule
                }
            );

            AddTimezoneMappingItem
            (
                "Caracas", -4m, TimeZoneWindowsEnum.Venezuela_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.America_Caracas }
            );

            AddTimezoneMappingItem
            (
                "Cuiaba", -4m, TimeZoneWindowsEnum.Central_Brazilian_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.America_Cuiaba, TimeZoneIANAEnum.America_Campo_Grande 
                }
            );

            AddTimezoneMappingItem
            (
                "Georgetown, La Paz, Manaus, San Juan", -4m, TimeZoneWindowsEnum.SA_Western_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.America_La_Paz, TimeZoneIANAEnum.America_Antigua,
                    TimeZoneIANAEnum.America_Araguaina, TimeZoneIANAEnum.America_Aruba,
                    TimeZoneIANAEnum.America_Barbados, TimeZoneIANAEnum.America_St_Barthelemy,
                    TimeZoneIANAEnum.America_Kralendijk, TimeZoneIANAEnum.America_Manaus, 
                    TimeZoneIANAEnum.America_Boa_Vista, TimeZoneIANAEnum.America_Porto_Velho,
                    TimeZoneIANAEnum.America_Blanc_Sablon, TimeZoneIANAEnum.America_Curacao, 
                    TimeZoneIANAEnum.America_Dominica, TimeZoneIANAEnum.America_Santo_Domingo, 
                    TimeZoneIANAEnum.America_Grenada, TimeZoneIANAEnum.America_Guadeloupe, 
                    TimeZoneIANAEnum.America_Guyana, TimeZoneIANAEnum.America_St_Kitts, 
                    TimeZoneIANAEnum.America_St_Lucia, TimeZoneIANAEnum.America_Marigot, 
                    TimeZoneIANAEnum.America_Martinique, TimeZoneIANAEnum.America_Montserrat, 
                    TimeZoneIANAEnum.America_Puerto_Rico, TimeZoneIANAEnum.America_Lower_Princes, 
                    TimeZoneIANAEnum.America_Port_of_Spain, TimeZoneIANAEnum.America_St_Vincent, 
                    TimeZoneIANAEnum.America_Tortola, TimeZoneIANAEnum.America_St_Thomas, 
                    TimeZoneIANAEnum.Etc_GMT_Plus_4, 
                }
            );

            AddTimezoneMappingItem
            (
                "Santiago", -4m, TimeZoneWindowsEnum.Pacific_SA_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.America_Santiago }
            );

            AddTimezoneMappingItem
            (
                "Turks and Caicos", -4m, TimeZoneWindowsEnum.Turks_And_Caicos_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.America_Grand_Turk }
            );

            AddTimezoneMappingItem
            (
                "Newfoundland", -3.5m, TimeZoneWindowsEnum.Newfoundland_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.America_St_Johns }
            );

            AddTimezoneMappingItem
            (
                "Araguaina", -3m, TimeZoneWindowsEnum.Tocantins_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.America_Araguaina }
            );

            AddTimezoneMappingItem
            (
                "Brasilia", -3m, TimeZoneWindowsEnum.E_South_America_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.America_Sao_Paulo }
            );

            AddTimezoneMappingItem
            (
                "Cayenne, Fortaleza", -3m, TimeZoneWindowsEnum.SA_Eastern_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.America_Cayenne, TimeZoneIANAEnum.Antarctica_Rothera, 
                    TimeZoneIANAEnum.Antarctica_Palmer, TimeZoneIANAEnum.America_Fortaleza, 
                    TimeZoneIANAEnum.America_Belem, TimeZoneIANAEnum.America_Maceio, 
                    TimeZoneIANAEnum.America_Recife, TimeZoneIANAEnum.America_Santarem, 
                    TimeZoneIANAEnum.America_Punta_Arenas, TimeZoneIANAEnum.Atlantic_Stanley, 
                    TimeZoneIANAEnum.America_Paramaribo, TimeZoneIANAEnum.Etc_GMT_Plus_3
                }
            );

            AddTimezoneMappingItem
            (
                "City of Buenos Aires", -3m, TimeZoneWindowsEnum.Argentina_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.America_Argentina_Buenos_Aires, TimeZoneIANAEnum.America_Argentina_Cordoba, 
                    TimeZoneIANAEnum.America_Argentina_Salta, TimeZoneIANAEnum.America_Argentina_Jujuy, 
                    TimeZoneIANAEnum.America_Argentina_Catamarca, TimeZoneIANAEnum.America_Argentina_Tucuman,
                    TimeZoneIANAEnum.America_Argentina_La_Rioja, TimeZoneIANAEnum.America_Argentina_San_Juan, 
                    TimeZoneIANAEnum.America_Argentina_Mendoza, TimeZoneIANAEnum.America_Argentina_San_Luis, 
                    TimeZoneIANAEnum.America_Argentina_Rio_Gallegos, TimeZoneIANAEnum.America_Argentina_Ushuaia
                }
            );

            AddTimezoneMappingItem
            (
                "Greenland", -3m, TimeZoneWindowsEnum.Greenland_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.America_Godthab }
            );

            AddTimezoneMappingItem
            (
                "Montevideo", -3m, TimeZoneWindowsEnum.Montevideo_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.America_Montevideo }
            );

            AddTimezoneMappingItem
            (
                "Saint Pierre and Miquelon", -3m, TimeZoneWindowsEnum.Saint_Pierre_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.America_Miquelon }
            );

            AddTimezoneMappingItem
            (
                "Salvador", -3m, TimeZoneWindowsEnum.Bahia_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.America_Bahia }
            );

            AddTimezoneMappingItem
            (
                "Coordinated Universal Time-02", -2m, TimeZoneWindowsEnum.UTC_Minus_02,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Etc_GMT_Plus_2, TimeZoneIANAEnum.America_Noronha, 
                    TimeZoneIANAEnum.Atlantic_South_Georgia
                }
            );

            AddTimezoneMappingItem
            (
                "Azores", -2m, TimeZoneWindowsEnum.Azores_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Atlantic_Azores, TimeZoneIANAEnum.America_Scoresbysund
                }
            );

            AddTimezoneMappingItem
            (
                "Cabo Verde Is.", -1m, TimeZoneWindowsEnum.Cape_Verde_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Atlantic_Cape_Verde, TimeZoneIANAEnum.Etc_GMT_Plus_1
                }
            );

            AddTimezoneMappingItem
            (
                "Coordinated Universal Time", 0m, TimeZoneWindowsEnum.UTC,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.America_Danmarkshavn, TimeZoneIANAEnum.Etc_GMT
                }
            );

            AddTimezoneMappingItem
            (
                "Casablanca", 0m, TimeZoneWindowsEnum.Morocco_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Africa_Casablanca, TimeZoneIANAEnum.Africa_El_Aaiun
                }
            );

            AddTimezoneMappingItem
            (
                "Dublin, Edinburgh, Lisbon, London", 0m, TimeZoneWindowsEnum.GMT_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Europe_London, TimeZoneIANAEnum.Atlantic_Canary,
                    TimeZoneIANAEnum.Atlantic_Faeroe, TimeZoneIANAEnum.Europe_Guernsey, 
                    TimeZoneIANAEnum.Europe_Dublin, TimeZoneIANAEnum.Europe_Isle_Of_Man,
                    TimeZoneIANAEnum.Europe_Jersey, TimeZoneIANAEnum.Europe_Lisbon,
                    TimeZoneIANAEnum.Atlantic_Madeira, TimeZoneIANAEnum.Etc_Greenwich,
                    TimeZoneIANAEnum.Etc_UTC, TimeZoneIANAEnum.Etc_Universal
                }
            );

            AddTimezoneMappingItem
            (
                "Monrovia, Reykjavik", 0m, TimeZoneWindowsEnum.Greenwich_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Atlantic_Reykjavik, TimeZoneIANAEnum.Africa_Ouagadougou,
                    TimeZoneIANAEnum.Africa_Abidjan, TimeZoneIANAEnum.Africa_Accra,
                    TimeZoneIANAEnum.Africa_Banjul, TimeZoneIANAEnum.Africa_Conakry, 
                    TimeZoneIANAEnum.Africa_Monrovia, TimeZoneIANAEnum.Africa_Bamako, 
                    TimeZoneIANAEnum.Africa_Nouakchott, TimeZoneIANAEnum.Atlantic_St_Helena, 
                    TimeZoneIANAEnum.Africa_Freetown, TimeZoneIANAEnum.Africa_Dakar, 
                    TimeZoneIANAEnum.Africa_Sao_Tome, TimeZoneIANAEnum.Africa_Lome,
                    TimeZoneIANAEnum.Africa_Bissau
                }
            );

            AddTimezoneMappingItem
            (
                "Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna", 1m, TimeZoneWindowsEnum.W_Europe_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Europe_Berlin, TimeZoneIANAEnum.Europe_Andorra,
                    TimeZoneIANAEnum.Europe_Vienna, TimeZoneIANAEnum.Europe_Zurich,
                    TimeZoneIANAEnum.Europe_Busingen, TimeZoneIANAEnum.Europe_Gibraltar, 
                    TimeZoneIANAEnum.Europe_Rome, TimeZoneIANAEnum.Europe_Vaduz, 
                    TimeZoneIANAEnum.Europe_Luxembourg, TimeZoneIANAEnum.Europe_Monaco, 
                    TimeZoneIANAEnum.Europe_Malta, TimeZoneIANAEnum.Europe_Amsterdam, 
                    TimeZoneIANAEnum.Europe_Oslo, TimeZoneIANAEnum.Europe_Stockholm, 
                    TimeZoneIANAEnum.Europe_Longyearbyen, TimeZoneIANAEnum.Europe_San_Marino, 
                    TimeZoneIANAEnum.Europe_Vatican
                }
            );

            AddTimezoneMappingItem
            (
                "Belgrade, Bratislava, Budapest, Ljubljana, Prague", 1m, TimeZoneWindowsEnum.Central_Europe_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Europe_Budapest, TimeZoneIANAEnum.Europe_Tirane,
                    TimeZoneIANAEnum.Europe_Prague, TimeZoneIANAEnum.Europe_Podgorica, 
                    TimeZoneIANAEnum.Europe_Belgrade, TimeZoneIANAEnum.Europe_Ljubljana, 
                    TimeZoneIANAEnum.Europe_Bratislava
                }
            );

            AddTimezoneMappingItem
            (
                "Brussels, Copenhagen, Madrid, Paris", 1m, TimeZoneWindowsEnum.Romance_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Europe_Paris, TimeZoneIANAEnum.Europe_Brussels,
                    TimeZoneIANAEnum.Europe_Copenhagen, TimeZoneIANAEnum.Europe_Madrid,
                    TimeZoneIANAEnum.Africa_Ceuta
                }
            );

            AddTimezoneMappingItem
            (
                "Sarajevo, Skopje, Warsaw, Zagreb", 1m, TimeZoneWindowsEnum.Central_European_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Europe_Warsaw, TimeZoneIANAEnum.Europe_Sarajevo,
                    TimeZoneIANAEnum.Europe_Zagreb, TimeZoneIANAEnum.Europe_Skopje
                }
            );

            AddTimezoneMappingItem
            (
                "West Central Africa", 1m, TimeZoneWindowsEnum.W_Central_Africa_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Africa_Lagos, TimeZoneIANAEnum.Africa_Luanda,
                    TimeZoneIANAEnum.Africa_Porto_Novo, TimeZoneIANAEnum.Africa_Kinshasa,
                    TimeZoneIANAEnum.Africa_Bangui, TimeZoneIANAEnum.Africa_Brazzaville,
                    TimeZoneIANAEnum.Africa_Douala, TimeZoneIANAEnum.Africa_Algiers,
                    TimeZoneIANAEnum.Africa_Libreville, TimeZoneIANAEnum.Africa_Malabo,
                    TimeZoneIANAEnum.Africa_Niamey, TimeZoneIANAEnum.Africa_Ndjamena, 
                    TimeZoneIANAEnum.Africa_Tunis, TimeZoneIANAEnum.Etc_GMT_Minus_1
                }
            );

            AddTimezoneMappingItem
            (
                "Windhoek", 1m, TimeZoneWindowsEnum.Namibia_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Africa_Windhoek }
            );

            AddTimezoneMappingItem
            (
                "Amman", 2m, TimeZoneWindowsEnum.Jordan_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Asia_Amman }
            );

            AddTimezoneMappingItem
            (
                "Athens, Bucharest", 2m, TimeZoneWindowsEnum.GTB_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Europe_Bucharest, TimeZoneIANAEnum.Asia_Nicosia,
                    TimeZoneIANAEnum.Europe_Athens
                }
            );

            AddTimezoneMappingItem
            (
                "Beirut", 2m, TimeZoneWindowsEnum.Middle_East_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Asia_Beirut }
            );

            AddTimezoneMappingItem
            (
                "Cairo", 2m, TimeZoneWindowsEnum.Egypt_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Africa_Cairo }
            );

            AddTimezoneMappingItem
            (
                "Chisinau", 2m, TimeZoneWindowsEnum.E_Europe_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Europe_Chisinau }
            );

            AddTimezoneMappingItem
            (
                "Damascus", 2m, TimeZoneWindowsEnum.Syria_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Asia_Damascus }
            );

            AddTimezoneMappingItem
            (
                "Gaza, Hebron", 2m, TimeZoneWindowsEnum.West_Bank_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Asia_Hebron, TimeZoneIANAEnum.Asia_Gaza
                }
            );

            AddTimezoneMappingItem
            (
                "Harare, Pretoria", 2m, TimeZoneWindowsEnum.South_Africa_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Africa_Johannesburg, TimeZoneIANAEnum.Africa_Bujumbura,
                    TimeZoneIANAEnum.Africa_Gaborone, TimeZoneIANAEnum.Africa_Lubumbashi,
                    TimeZoneIANAEnum.Africa_Maseru, TimeZoneIANAEnum.Africa_Blantyre,
                    TimeZoneIANAEnum.Africa_Maputo, TimeZoneIANAEnum.Africa_Kigali,
                    TimeZoneIANAEnum.Africa_Mbabane, TimeZoneIANAEnum.Africa_Lusaka, 
                    TimeZoneIANAEnum.Africa_Harare, TimeZoneIANAEnum.Etc_GMT_Minus_2
                }
            );

            AddTimezoneMappingItem
            (
                "Helsinki, Kyiv, Riga, Sofia, Tallinn, Vilnius", 2m, TimeZoneWindowsEnum.FLE_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Europe_Kiev, TimeZoneIANAEnum.Europe_Mariehamn,
                    TimeZoneIANAEnum.Europe_Sofia, TimeZoneIANAEnum.Europe_Tallinn,
                    TimeZoneIANAEnum.Europe_Helsinki, TimeZoneIANAEnum.Europe_Vilnius,
                    TimeZoneIANAEnum.Europe_Riga, TimeZoneIANAEnum.Europe_Uzhgorod, 
                    TimeZoneIANAEnum.Europe_Zaporozhye
                }
            );

            AddTimezoneMappingItem
            (
                "Jerusalem", 2m, TimeZoneWindowsEnum.Israel_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Asia_Jerusalem }
            );

            AddTimezoneMappingItem
            (
                "Kaliningrad", 2m, TimeZoneWindowsEnum.Kaliningrad_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Europe_Kaliningrad }
            );

            AddTimezoneMappingItem
            (
                "Tripoli", 2m, TimeZoneWindowsEnum.Libya_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Africa_Tripoli }
            );

            AddTimezoneMappingItem
            (
                "Baghdad", 3m, TimeZoneWindowsEnum.Arabic_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Asia_Baghdad }
            );

            AddTimezoneMappingItem
            (
                "Istanbul", 3m, TimeZoneWindowsEnum.Turkey_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Europe_Istanbul, TimeZoneIANAEnum.Asia_Famagusta
                }
            );

            AddTimezoneMappingItem
            (
                "Kuwait, Riyadh", 3m, TimeZoneWindowsEnum.Arab_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Asia_Riyadh, TimeZoneIANAEnum.Asia_Bahrain,
                    TimeZoneIANAEnum.Asia_Kuwait, TimeZoneIANAEnum.Asia_Qatar,
                    TimeZoneIANAEnum.Asia_Aden
                }
            );

            AddTimezoneMappingItem
            (
                "Minsk", 3m, TimeZoneWindowsEnum.Belarus_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Europe_Minsk }
            );

            AddTimezoneMappingItem
            (
                "Moscow, St. Petersburg, Volgograd", 3m, TimeZoneWindowsEnum.Russian_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Europe_Moscow, TimeZoneIANAEnum.Europe_Kirov,
                    TimeZoneIANAEnum.Europe_Volgograd, TimeZoneIANAEnum.Europe_Simferopol
                }
            );

            AddTimezoneMappingItem
            (
                "Nairobi", 3m, TimeZoneWindowsEnum.E_Africa_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Africa_Nairobi, TimeZoneIANAEnum.Antarctica_Syowa,
                    TimeZoneIANAEnum.Africa_Djibouti, TimeZoneIANAEnum.Africa_Asmera,
                    TimeZoneIANAEnum.Africa_Addis_Ababa, TimeZoneIANAEnum.Indian_Comoro, 
                    TimeZoneIANAEnum.Indian_Antananarivo, TimeZoneIANAEnum.Africa_Khartoum, 
                    TimeZoneIANAEnum.Africa_Mogadishu, TimeZoneIANAEnum.Africa_Juba, 
                    TimeZoneIANAEnum.Africa_Dar_Es_Salaam, TimeZoneIANAEnum.Africa_Kampala, 
                    TimeZoneIANAEnum.Indian_Mayotte, TimeZoneIANAEnum.Etc_GMT_Minus_3
                }
            );

            AddTimezoneMappingItem
            (
                "Tehran", 3.5m, TimeZoneWindowsEnum.Iran_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Asia_Tehran }
            );

            AddTimezoneMappingItem
            (
                "Abu Dhabi, Muscat", 4m, TimeZoneWindowsEnum.Arabian_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Asia_Dubai, TimeZoneIANAEnum.Asia_Muscat,
                    TimeZoneIANAEnum.Etc_GMT_Minus_4
                }
            );

            AddTimezoneMappingItem
            (
                "Astrakhan, Ulyanovsk", 4m, TimeZoneWindowsEnum.Astrakhan_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Europe_Astrakhan, TimeZoneIANAEnum.Europe_Saratov,
                    TimeZoneIANAEnum.Europe_Ulyanovsk
                }
            );

            AddTimezoneMappingItem
            (
                "Baku", 4m, TimeZoneWindowsEnum.Azerbaijan_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Asia_Baku }
            );

            AddTimezoneMappingItem
            (
                "Izhevsk, Samara", 4m, TimeZoneWindowsEnum.Russia_Time_Zone_3,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Europe_Samara }
            );

            AddTimezoneMappingItem
            (
                "Port Louis", 4m, TimeZoneWindowsEnum.Mauritius_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Indian_Mauritius, TimeZoneIANAEnum.Indian_Reunion,
                    TimeZoneIANAEnum.Indian_Mahe
                }
            );

            AddTimezoneMappingItem
            (
                "Tbilisi", 4m, TimeZoneWindowsEnum.Georgian_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Asia_Tbilisi }
            );

            AddTimezoneMappingItem
            (
                "Yerevan", 4m, TimeZoneWindowsEnum.Caucasus_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Asia_Yerevan }
            );

            AddTimezoneMappingItem
            (
                "Kabul", 4.5m, TimeZoneWindowsEnum.Afghanistan_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Asia_Kabul }
            );

            AddTimezoneMappingItem
            (
                "Ashgabat, Tashkent", 5m, TimeZoneWindowsEnum.West_Asia_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Asia_Tashkent, TimeZoneIANAEnum.Antarctica_Mawson,
                    TimeZoneIANAEnum.Asia_Oral, TimeZoneIANAEnum.Asia_Aqtau, 
                    TimeZoneIANAEnum.Asia_Aqtobe, TimeZoneIANAEnum.Asia_Atyrau,
                    TimeZoneIANAEnum.Indian_Maldives, TimeZoneIANAEnum.Indian_Kerguelen,
                    TimeZoneIANAEnum.Asia_Dushanbe, TimeZoneIANAEnum.Asia_Ashgabat,
                    TimeZoneIANAEnum.Asia_Samarkand, TimeZoneIANAEnum.Etc_GMT_Minus_5
                }
            );

            AddTimezoneMappingItem
            (
                "Ekaterinburg", 5m, TimeZoneWindowsEnum.Ekaterinburg_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Asia_Ekaterinburg, TimeZoneIANAEnum.Asia_Yekaterinburg }
            );

            AddTimezoneMappingItem
            (
                "Islamabad, Karachi", 5m, TimeZoneWindowsEnum.Pakistan_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Asia_Karachi }
            );

            AddTimezoneMappingItem
            (
                "Chennai, Kolkata, Mumbai, New Delhi", 5.5m, TimeZoneWindowsEnum.India_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Asia_Calcutta }
            );

            AddTimezoneMappingItem
            (
                "Sri Jayawardenepura", 5.5m, TimeZoneWindowsEnum.Sri_Lanka_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Asia_Colombo }
            );

            AddTimezoneMappingItem
            (
                "Kathmandu", 5.75m, TimeZoneWindowsEnum.Nepal_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Asia_Kathmandu }
            );

            AddTimezoneMappingItem
            (
                "Astana", 6m, TimeZoneWindowsEnum.Central_Asia_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Asia_Almaty, TimeZoneIANAEnum.Antarctica_Vostok,
                    TimeZoneIANAEnum.Asia_Urumqi, TimeZoneIANAEnum.Indian_Chagos,
                    TimeZoneIANAEnum.Asia_Bishkek, TimeZoneIANAEnum.Asia_Qyzylorda, 
                    TimeZoneIANAEnum.Etc_GMT_Minus_6
                }
            );

            AddTimezoneMappingItem
            (
                "Dhaka", 6m, TimeZoneWindowsEnum.Bangladesh_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Asia_Dhaka, TimeZoneIANAEnum.Asia_Thimphu
                }
            );

            AddTimezoneMappingItem
            (
                "Omsk", 6m, TimeZoneWindowsEnum.Omsk_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Asia_Omsk }
            );

            AddTimezoneMappingItem
            (
                "Yangon (Rangoon)", 6.5m, TimeZoneWindowsEnum.Myanmar_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Asia_Rangoon, TimeZoneIANAEnum.Indian_Cocos
                }
            );

            AddTimezoneMappingItem
            (
                "Bangkok, Hanoi, Jakarta", 7m, TimeZoneWindowsEnum.SE_Asia_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Asia_Bangkok, TimeZoneIANAEnum.Antarctica_Davis,
                    TimeZoneIANAEnum.Indian_Christmas, TimeZoneIANAEnum.Asia_Jakarta, 
                    TimeZoneIANAEnum.Asia_Pontianak, TimeZoneIANAEnum.Asia_Phnom_Penh,
                    TimeZoneIANAEnum.Asia_Vientiane, TimeZoneIANAEnum.Asia_Saigon, 
                    TimeZoneIANAEnum.Etc_GMT_Minus_7
                }
            );

            AddTimezoneMappingItem
            (
                "Barnaul, Gorno-Altaysk", 7m, TimeZoneWindowsEnum.Altai_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Asia_Barnaul }
            );

            AddTimezoneMappingItem
            (
                "Hovd", 7m, TimeZoneWindowsEnum.W_Mongolia_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Asia_Hovd }
            );

            AddTimezoneMappingItem
            (
                "Krasnoyarsk", 7m, TimeZoneWindowsEnum.North_Asia_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Asia_Krasnoyarsk, TimeZoneIANAEnum.Asia_Novokuznetsk 
                }
            );

            AddTimezoneMappingItem
            (
                "Novosibirsk", 7m, TimeZoneWindowsEnum.N_Central_Asia_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Asia_Novosibirsk }
            );

            AddTimezoneMappingItem
            (
                "Tomsk", 7m, TimeZoneWindowsEnum.Tomsk_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Asia_Tomsk }
            );

            AddTimezoneMappingItem
            (
                "Beijing, Chongqing, Hong Kong, Urumqi", 8m, TimeZoneWindowsEnum.China_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Asia_Shanghai, TimeZoneIANAEnum.Asia_Hong_Kong,
                    TimeZoneIANAEnum.Asia_Macau
                }
            );

            AddTimezoneMappingItem
            (
                "Irkutsk", 8m, TimeZoneWindowsEnum.North_Asia_East_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Asia_Irkutsk }
            );

            AddTimezoneMappingItem
            (
                "Kuala Lumpur, Singapore", 8m, TimeZoneWindowsEnum.Singapore_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Asia_Singapore, TimeZoneIANAEnum.Asia_Brunei,
                    TimeZoneIANAEnum.Asia_Makassar, TimeZoneIANAEnum.Asia_Kuala_Lumpur,
                    TimeZoneIANAEnum.Asia_Kuching, TimeZoneIANAEnum.Asia_Manila,
                    TimeZoneIANAEnum.Etc_GMT_Minus_8
                }
            );

            AddTimezoneMappingItem
            (
                "Perth", 8m, TimeZoneWindowsEnum.W_Australia_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Australia_Perth }
            );

            AddTimezoneMappingItem
            (
                "Taipei", 8m, TimeZoneWindowsEnum.Taipei_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Asia_Taipei }
            );

            AddTimezoneMappingItem
            (
                "Ulaanbaatar", 8m, TimeZoneWindowsEnum.Ulaanbaatar_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Asia_Ulaanbaatar, TimeZoneIANAEnum.Asia_Choibalsan 
                }
            );

            AddTimezoneMappingItem
            (
                "Pyongyang", 8.5m, TimeZoneWindowsEnum.North_Korea_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Asia_Pyongyang }
            );

            AddTimezoneMappingItem
            (
                "Eucla", 8.75m, TimeZoneWindowsEnum.Aus_Central_W_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Australia_Eucla }
            );

            AddTimezoneMappingItem
            (
                "Chita", 9m, TimeZoneWindowsEnum.Transbaikal_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Asia_Chita }
            );

            AddTimezoneMappingItem
            (
                "Osaka, Sapporo, Tokyo", 9m, TimeZoneWindowsEnum.Tokyo_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Asia_Tokyo, TimeZoneIANAEnum.Asia_Jayapura,
                    TimeZoneIANAEnum.Pacific_Palau, TimeZoneIANAEnum.Asia_Dili, 
                    TimeZoneIANAEnum.Etc_GMT_Minus_9
                }
            );

            AddTimezoneMappingItem
            (
                "Seoul", 9m, TimeZoneWindowsEnum.Korea_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Asia_Seoul }
            );

            AddTimezoneMappingItem
            (
                "Yakutsk", 9m, TimeZoneWindowsEnum.Yakutsk_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Asia_Yakutsk, TimeZoneIANAEnum.Asia_Khandyga 
                }
            );

            AddTimezoneMappingItem
            (
                "Adelaide", 9.5m, TimeZoneWindowsEnum.Cen_Australia_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Australia_Adelaide, TimeZoneIANAEnum.Australia_Broken_Hill 
                }
            );

            AddTimezoneMappingItem
            (
                "Darwin", 9.5m, TimeZoneWindowsEnum.Aus_Central_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Australia_Darwin }
            );

            AddTimezoneMappingItem
            (
                "Brisbane", 10m, TimeZoneWindowsEnum.E_Australia_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Australia_Brisbane, TimeZoneIANAEnum.Australia_Lindeman 
                }
            );

            AddTimezoneMappingItem
            (
                "Canberra, Melbourne, Sydney", 10m, TimeZoneWindowsEnum.Aus_Eastern_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Australia_Sydney, TimeZoneIANAEnum.Australia_Melbourne 
                }
            );

            AddTimezoneMappingItem
            (
                "Guam, Port Moresby", 10m, TimeZoneWindowsEnum.West_Pacific_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Pacific_Port_Moresby, TimeZoneIANAEnum.Antarctica_DumontDUrville,
                    TimeZoneIANAEnum.Pacific_Truk, TimeZoneIANAEnum.Pacific_Guam,
                    TimeZoneIANAEnum.Pacific_Saipan, TimeZoneIANAEnum.Etc_GMT_Minus_10
                }
            );

            AddTimezoneMappingItem
            (
                "Hobart", 10m, TimeZoneWindowsEnum.Tasmania_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Australia_Hobart, TimeZoneIANAEnum.Australia_Currie
                }
            );

            AddTimezoneMappingItem
            (
                "Vladivostok", 10m, TimeZoneWindowsEnum.Vladivostok_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Asia_Vladivostok, TimeZoneIANAEnum.Asia_Ust_Nera
                }
            );

            AddTimezoneMappingItem
            (
                "Lord Howe Island", 10.5m, TimeZoneWindowsEnum.Lord_Howe_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Australia_Lord_Howe }
            );

            AddTimezoneMappingItem
            (
                "Bougainville Island", 11m, TimeZoneWindowsEnum.Bougainville_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Pacific_Bougainville }
            );

            AddTimezoneMappingItem
            (
                "Chokurdakh", 11m, TimeZoneWindowsEnum.Russia_Time_Zone_10,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Asia_Srednekolymsk }
            );

            AddTimezoneMappingItem
            (
                "Magadan", 11m, TimeZoneWindowsEnum.Magadan_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Asia_Magadan }
            );

            AddTimezoneMappingItem
            (
                "Norfolk Island", 11m, TimeZoneWindowsEnum.Norfolk_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Pacific_Norfolk }
            );

            AddTimezoneMappingItem
            (
                "Sakhalin", 11m, TimeZoneWindowsEnum.Sakhalin_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Asia_Sakhalin }
            );

            AddTimezoneMappingItem
            (
                "Solomon Is., New Caledonia", 11m, TimeZoneWindowsEnum.Central_Pacific_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Pacific_Guadalcanal, TimeZoneIANAEnum.Antarctica_Casey,
                    TimeZoneIANAEnum.Antarctica_Macquarie, TimeZoneIANAEnum.Pacific_Ponape, 
                    TimeZoneIANAEnum.Pacific_Kosrae, TimeZoneIANAEnum.Pacific_Noumea,
                    TimeZoneIANAEnum.Pacific_Efate, TimeZoneIANAEnum.Etc_GMT_Minus_11
                }
            );

            AddTimezoneMappingItem
            (
                "Anadyr, Petropavlovsk-Kamchatsky", 12m, TimeZoneWindowsEnum.Russia_Time_Zone_11,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Asia_Kamchatka, TimeZoneIANAEnum.Asia_Anadyr
                }
            );

            AddTimezoneMappingItem
            (
                "Auckland, Wellington", 12m, TimeZoneWindowsEnum.New_Zealand_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Pacific_Auckland, TimeZoneIANAEnum.Antarctica_McMurdo
                }
            );

            AddTimezoneMappingItem
            (
                "Coordinated Universal Time+12", 12m, TimeZoneWindowsEnum.UTC_Plus_12,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Pacific_Tarawa, TimeZoneIANAEnum.Pacific_Majuro,
                    TimeZoneIANAEnum.Pacific_Kwajalein, TimeZoneIANAEnum.Pacific_Nauru,
                    TimeZoneIANAEnum.Pacific_Funafuti, TimeZoneIANAEnum.Pacific_Wake,
                    TimeZoneIANAEnum.Pacific_Wallis, TimeZoneIANAEnum.Etc_GMT_Minus_12
                }
            );

            AddTimezoneMappingItem
            (
                "Fiji", 12m, TimeZoneWindowsEnum.Fiji_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Pacific_Fiji }
            );

            AddTimezoneMappingItem
            (
                "Chatham Islands", 12.75m, TimeZoneWindowsEnum.Chatham_Islands_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Pacific_Chatham }
            );

            AddTimezoneMappingItem
            (
                "Nuku'alofa", 13m, TimeZoneWindowsEnum.Tonga_Standard_Time,
                new TimeZoneIANAEnum[] 
                { 
                    TimeZoneIANAEnum.Pacific_Tongatapu, TimeZoneIANAEnum.Pacific_Enderbury,
                    TimeZoneIANAEnum.Pacific_Fakaofo, TimeZoneIANAEnum.Etc_GMT_Minus_13
                }
            );

            AddTimezoneMappingItem
            (
                "Samoa", 13m, TimeZoneWindowsEnum.Samoa_Standard_Time,
                new TimeZoneIANAEnum[] { TimeZoneIANAEnum.Pacific_Apia }
            );

            AddTimezoneMappingItem
            (
                "Kiritimati Island", 14m, TimeZoneWindowsEnum.Line_Islands_Standard_Time,
                new TimeZoneIANAEnum[]
                { 
                    TimeZoneIANAEnum.Pacific_Kiritimati, TimeZoneIANAEnum.Etc_GMT_Minus_14
                }
            );

            AddTimezoneMappingItem();
            AddTimezoneMappingItem
            (
                "Mid-Atlantic Standard Time", -2m, TimeZoneWindowsEnum.Mid_Atlantic_Standard_Time
            );
            AddTimezoneMappingItem
            (
                "Kamchatka Standard Time", 12m, TimeZoneWindowsEnum.Kamchatka_Standard_Time
            );
            AddUnmappedUTC();


            //To perform a so complex population, a list is more handy. On the other hand, relying on  a more efficient 
            //collection (array) seems the best approach to globally store a relatively big amount of information which
            //doesn't require any list feature.
            AllTimezonesInternal = AllTimezonesInternal_Temp.ToArray();
            AllTimezonesInternal_Temp = null;
        }

        //The whole AllTimezonesInternal population is focused on the Windows timezones, which aren't related to all the
        //possible UTC (and official) ones.
        private static void AddUnmappedUTC()
        {
            foreach (TimeZoneUTCEnum item in Enum.GetValues(typeof(TimeZoneUTCEnum)))
            {
                var temp = AllTimezonesInternal_Temp.FirstOrDefault(x => x.UTCTimeZone == item);
                if (temp == null)
                {
                    AllTimezonesInternal_Temp.Add
                    (
                        new TimeZonesMainMap(item)
                    );
                }
            }
        }

        private static void AddTimezoneMappingItem
        (
            string name = null, decimal decimalOffset = 0m, 
            TimeZoneWindowsEnum windowsZone = TimeZoneWindowsEnum.None, 
            TimeZoneIANAEnum[] ianaZones = null
        )
        {
            AllTimezonesInternal_Temp.Add
            (
                new TimeZonesMainMap
                (
                    name, decimalOffset, windowsZone, ianaZones
                )
            );
        }
    }
}
