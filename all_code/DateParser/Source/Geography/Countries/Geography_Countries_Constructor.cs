using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FlexibleParser
{
    public enum ErrorCountryEnum
    {
        None = 0,
        InvalidCountry
    };

    public partial class Country
    {
        public readonly CountryEnum Value;
        public readonly string Code;
        public readonly string Name;
        public readonly ReadOnlyCollection<string> AlternativeNames;
        public readonly ErrorCountryEnum Error;

        private Country(Country country) : this
        (
            country == null ? CountryEnum.None : country.Value
        )
        { }

        public Country(string codeOrCountryName)
        {
            CountryInternal.GetCodeCountry(codeOrCountryName);
            if (CountryInternal.CodeCountry.Value == CountryEnum.None)
            {
                Error = ErrorCountryEnum.InvalidCountry;
                return;
            }

            Value = CountryInternal.CodeCountry.Value;
            Code = CountryInternal.CodeCountry.Key;
            Name = GetNameFromEnum(Value);
            AlternativeNames = CountryInternal.GetAlternativeNames(Value).AsReadOnly();
        }

        public Country(CountryEnum value)
        {
            CountryInternal.GetCodeCountry(value);
            if (CountryInternal.CodeCountry.Value == CountryEnum.None)
            {
                Error = ErrorCountryEnum.InvalidCountry;
                return;
            }

            Value = CountryInternal.CodeCountry.Value;
            Code = CountryInternal.CodeCountry.Key;
            Name = GetNameFromEnum(Value);
            AlternativeNames = CountryInternal.GetAlternativeNames(Value).AsReadOnly();
        }
    }
}
