using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FlexibleParser
{
    ///<summary><para>Class dealing with all the country-related timezones.</para></summary>
    public partial class TimeZonesCountry
    {
        internal static bool Populated = TimeZonesInternal.StartTimezones();

        ///<summary><para>Country variable associated with the current instance.</para></summary>
        public readonly Country Country;
        ///<summary><para>City/region associated with the current instance.</para></summary>
        public readonly string CityOrRegion;
        ///<summary><para>All the standard/daylight timezones associated with the current country.</para></summary>
        public ReadOnlyCollection<KeyValuePair<TimeZoneOfficial, TimeZoneOfficial>> StandardDaylightTimezones;
        ///<summary><para>Error associated with the current instance.</para></summary>
        public readonly ErrorTimeZoneEnum Error;

        ///<summary>
        ///<para>Initialises a new TimeZonesCountry instance.</para>
        ///<para>It recognises all the country names/codes and the cities listed in https://github.com/David-Haim/CountriesToCitiesJSON/blob/master/countriesToCities.json.</para>
        ///</summary>
        ///<param name="countryOrCity">Country or city information to be parsed.</param>
        public TimeZonesCountry(string countryOrCity) : this
        (
            TimeZonesCountryInternal.GetCountryFromString(countryOrCity)
        ) 
        { }

        ///<summary><para>Initialises a new TimeZonesCountry instance.</para></summary>
        ///<param name="country">Country variable whose information will be used.</param>
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
