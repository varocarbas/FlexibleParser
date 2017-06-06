using System;

namespace FlexibleParser
{
    public partial class TimeZonesCountry : IComparable<TimeZonesCountry>
    {
        ///<summary><para>Compares the current instance against another TimeZonesCountry one.</para></summary>
        ///<param name="other">The other TimeZonesCountry instance.</param>
        public int CompareTo(TimeZonesCountry other)
        {
            return Common.PerformComparison(this, other, typeof(TimeZonesCountry));
        }

        ///<summary><para>Outputs an error or "[country] ([code])".</para> </summary>
        public override string ToString()
        {
            return TimeZonesCountryInternal.TimeZonesCountryToString(this);
        }

        ///<summary><para>Creates a new TimeZonesCountry instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">Country input.</param>
        public static implicit operator TimeZonesCountry(Country input)
        {
            return new TimeZonesCountry(input);
        }

        ///<summary><para>Creates a new TimeZonesCountry instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">CountryEnum input.</param>
        public static implicit operator TimeZonesCountry(CountryEnum input)
        {
            return new TimeZonesCountry(input);
        }

        ///<summary><para>Creates a new TimeZonesCountry instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">String input.</param>
        public static implicit operator TimeZonesCountry(string input)
        {
            return new TimeZonesCountry(input);
        }

        ///<summary><para>Determines whether two TimeZonesCountry variables are equal.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator ==(TimeZonesCountry first, TimeZonesCountry second)
        {
            return Common.NoNullEquals(first, second, typeof(TimeZonesCountry));
        }

        ///<summary><para>Determines whether two TimeZonesCountry variables are different.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator !=(TimeZonesCountry first, TimeZonesCountry second)
        {
            return !Common.NoNullEquals(first, second, typeof(TimeZonesCountry));
        }

        ///<summary><para>Determines whether the current TimeZonesCountry variable is equal to other one.</para></summary>
        ///<param name="other">Other variable.</param>
        public bool Equals(TimeZonesCountry other)
        {
            return
            (
                object.Equals(other, null) ? false :
                Common.PerformComparison(this, other, typeof(TimeZonesCountry)) == 0
            );
        }

        ///<summary><para>Determines whether the current TimeZonesCountry variable is equal to other one.</para></summary>
        ///<param name="obj">Other variable.</param>
        public override bool Equals(object obj)
        {
            return Equals(obj as TimeZonesCountry);
        }

        ///<summary><para>Returns the hash code for this TimeZonesCountry variable.</para></summary>
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
