using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FlexibleParser
{
    public partial class TimeZonesCountry
    {
        private static bool Populated = TimeZonesInternal.StartTimezones();

        public readonly Country Country;
        public readonly string CityOrRegion;
        public ReadOnlyCollection<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>> StandardDaylightTimezones;
        public readonly ErrorTimeZoneEnum Error;

        public TimeZonesCountry(string countryRegionOrCity) : this
        (
            TimeZonesCountryInternal.GetCountryFromString(countryRegionOrCity)
        ) 
        { }

        public TimeZonesCountry(Country country)
        {
            if (country == null || country.Error != ErrorCountryEnum.None)
            {
                Error = ErrorTimeZoneEnum.InvalidTimeZone;
                return;
            }

            Country = country.Value;
            CityOrRegion = TimeZonesCountryInternal.CityOrRegion;

            if (TimeZonesCountryInternal.CountryOfficials.ContainsKey(Country.Value))
            {
                StandardDaylightTimezones = TimeZonesCountryInternal.CountryOfficials[Country.Value].Distinct().ToList().AsReadOnly();
            }
        }
    }
}
