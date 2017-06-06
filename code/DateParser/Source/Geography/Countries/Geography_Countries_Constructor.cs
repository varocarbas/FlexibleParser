using System.Collections.ObjectModel;

namespace FlexibleParser
{
    ///<summary><para>Errors triggered by the Country class.</para></summary>
    public enum ErrorCountryEnum
    {
        ///<summary><para>None.</para></summary>
        None = 0,
        ///<summary><para>Invalid country.</para></summary>
        InvalidCountry
    };

    ///<summary><para>Class dealing with all the country-related information.</para></summary>
    public partial class Country
    {
        ///<summary><para>CountryEnum variable associated with the country.</para></summary>
        public readonly CountryEnum Value;
        ///<summary><para>Country code.</para></summary>
        public readonly string Code;
        ///<summary><para>Country name.</para></summary>
        public readonly string Name;
        ///<summary><para>Alternative names of the country.</para></summary>
        public readonly ReadOnlyCollection<string> AlternativeNames;
        ///<summary><para>Error associated with the current instance.</para></summary>
        public readonly ErrorCountryEnum Error;

        ///<summary><para>Initialises a new Country instance.</para></summary>
        ///<param name="country">Country variable whose information will be used.</param>
        public Country(Country country) : this
        (
            country == null ? CountryEnum.None : country.Value
        )
        { }

        ///<summary><para>Initialises a new Country instance.</para></summary>
        ///<param name="codeOrCountryName">Code or country name to be parsed.</param>
        public Country(string codeOrCountryName)
        {
            CountryInternal.GetCodeCountry(codeOrCountryName);
            if (CountryInternal.CodeCountry.Value == CountryEnum.None)
            {
                Error = ErrorCountryEnum.InvalidCountry;
                return;
            }

            Value = CountryInternal.CodeCountry.Value;
            Code = 
            (
                CountryInternal.CodeCountry.Key == "" ? null : CountryInternal.CodeCountry.Key
            );
            Name = CountryInternal.GetNameFromEnum(Value);
            AlternativeNames = CountryInternal.GetAlternativeNames(Value).AsReadOnly();
        }

        ///<summary><para>Initialises a new Country instance.</para></summary>
        ///<param name="value">CountryEnum variable to be used.</param>
        public Country(CountryEnum value)
        {
            CountryInternal.GetCodeCountry(value);
            if (CountryInternal.CodeCountry.Value == CountryEnum.None)
            {
                Error = ErrorCountryEnum.InvalidCountry;
                return;
            }

            Value = CountryInternal.CodeCountry.Value;
            Code =
            (
                CountryInternal.CodeCountry.Key == "" ? null : CountryInternal.CodeCountry.Key
            );
            Name = CountryInternal.GetNameFromEnum(Value);
            AlternativeNames = CountryInternal.GetAlternativeNames(Value).AsReadOnly();
        }
    }
}
