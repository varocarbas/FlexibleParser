using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FlexibleParser
{
    public partial class NumberP
    {
        private static bool InputIsCultureFeature(string input, CultureFeatures feature, CultureInfo culture)
        {
            foreach (string item in GetCultureFeature(feature, culture))
            {
                if (item == input) return true;
            }

            return false;
        }

        private static string[] GetCultureFeature(CultureFeatures feature, CultureInfo culture)
        {
            if (culture == null) return new string[0];

            if (feature == CultureFeatures.DecimalSeparator)
            {
                return new string[] 
                { 
                    culture.NumberFormat.NumberDecimalSeparator,
                    culture.NumberFormat.PercentDecimalSeparator,
                    culture.NumberFormat.CurrencyDecimalSeparator
                }
                .Distinct().ToArray();
                //All the decimal (or thousands) separators associated with a given culture are (almost?) always 
                //identical. In any case, associating a given character to certain subtype (number/currency/percentage)
                //would go against the overall attitude of this library (i.e., parsing as restrictionless as possible).
                //That's why all of the separators are treated indistinctively.
            }
            else if (feature == CultureFeatures.ThousandSeparator)
            {
                return new string[] 
                { 
                    culture.NumberFormat.NumberGroupSeparator,
                    culture.NumberFormat.PercentGroupSeparator,
                    culture.NumberFormat.CurrencyGroupSeparator
                }
                .Distinct().ToArray();
            }
            else if (feature == CultureFeatures.CurrencySymbol)
            {
                return new string[] 
                { 
                    culture.NumberFormat.CurrencySymbol
                };
            }
            else if (feature == CultureFeatures.PercentageSymbol)
            {
                return new string[] 
                { 
                    culture.NumberFormat.PercentSymbol,
                    culture.NumberFormat.PerMilleSymbol
                };
            }

            return new string[0];
        }

        private enum CultureFeatures 
        { 
            DecimalSeparator, ThousandSeparator, CurrencySymbol, PercentageSymbol  
        }
    }
}
