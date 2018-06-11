using System;

namespace FlexibleParser
{
    public partial class Country : IComparable<Country>
    {
        ///<summary><para>Compares the current instance against another Country one.</para></summary>
        ///<param name="other">The other Country instance.</param>
        public int CompareTo(Country other)
        {
            return Common.PerformComparison(this, other, typeof(Country));
        }
        
        ///<summary><para>Outputs an error or "[name] ([code]) -- [city/region]".</para> </summary>
        public override string ToString()
        {
            return TimeZonesCountryInternal.TimeZonesCountryToString(this);
        }

        ///<summary><para>Creates a new Country instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">CountryEnum input.</param>
        public static implicit operator Country(CountryEnum input)
        {
            return new Country(input);
        }

        ///<summary><para>Creates a new Country instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">String input.</param>
        public static implicit operator Country(string input)
        {
            return new Country(input);
        }

        ///<summary><para>Creates a new Country instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimeZoneInfo input.</param>
        public static implicit operator Country(TimeZoneInfo input)
        {
            return new Country(input);
        }

        ///<summary><para>Determines whether two Country variables are equal.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator ==(Country first, Country second)
        {
            return Common.NoNullEquals(first, second, typeof(Country));
        }

        ///<summary><para>Determines whether two Country variables are different.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator !=(Country first, Country second)
        {
            return !Common.NoNullEquals(first, second, typeof(Country));
        }

        ///<summary><para>Determines whether the current Country variable is equal to other one.</para></summary>
        ///<param name="other">Other variable.</param>
        public bool Equals(Country other)
        {
            return
            (
                object.Equals(other, null) ? false :
                Common.PerformComparison(this, other, typeof(Country)) == 0
            );
        }

        ///<summary><para>Determines whether the current Country variable is equal to other one.</para></summary>
        ///<param name="obj">Other variable.</param>
        public override bool Equals(object obj)
        {
            return Equals(obj as Country);
        }

        ///<summary><para>Returns the hash code for this Country variable.</para></summary>
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
