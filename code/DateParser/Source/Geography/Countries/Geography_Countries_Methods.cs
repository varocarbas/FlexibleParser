using System;
using System.Collections.Generic;

namespace FlexibleParser
{
    internal partial class CountryInternal
    {
        public static string CountryToString(Country country, string cityRegion = null)
        {
            if (country == null || country.Error != ErrorCountryEnum.None)
            {
                return "Invalid country";
            }

            string output = country.Name + " (" + country.Code + ")";

            return output + 
            (
                cityRegion == null ? "" : 
                " -- " + cityRegion
            );
        }
    }
}
