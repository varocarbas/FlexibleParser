using System;
using System.Collections.Generic;

namespace FlexibleParser
{
    public partial class TimeZoneIANA : IComparable<TimeZoneIANA>
    {
        ///<summary><para>Compares the current instance against another TimeZoneIANA one.</para></summary>
        ///<param name="other">The other TimeZoneIANA instance.</param>
        public int CompareTo(TimeZoneIANA other)
        {
            return Common.PerformComparison(this, other, typeof(TimeZoneIANA));
        }

        ///<summary><para>Outputs an error or "[name] -- UTC [offset]".</para> </summary>
        public override string ToString()
        {
            return TimeZonesInternal.TimeZoneTypeToString(this);
        }

        ///<summary><para>Creates a new TimeZoneIANA instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimeZoneIANAEnum input.</param>
        public static implicit operator TimeZoneIANA(TimeZoneIANAEnum input)
        {
            return new TimeZoneIANA(input);
        }

        ///<summary><para>Creates a new TimeZoneIANA instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">String input.</param>
        public static implicit operator TimeZoneIANA(string input)
        {
            return new TimeZoneIANA(input);
        }

        ///<summary><para>Creates a new TimeZoneIANA instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimeZoneInfo input.</param>
        public static implicit operator TimeZoneIANA(TimeZoneInfo input)
        {
            return new TimeZoneIANA(input);
        }

        ///<summary><para>Determines whether two TimeZoneIANA variables are equal.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator ==(TimeZoneIANA first, TimeZoneIANA second)
        {
            return Common.NoNullEquals(first, second, typeof(TimeZoneIANA));
        }

        ///<summary><para>Determines whether two TimeZoneIANA variables are different.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator !=(TimeZoneIANA first, TimeZoneIANA second)
        {
            return !Common.NoNullEquals(first, second, typeof(TimeZoneIANA));
        }

        ///<summary><para>Determines whether the current TimeZoneIANA variable is equal to other one.</para></summary>
        ///<param name="other">Other variable.</param>
        public bool Equals(TimeZoneIANA other)
        {
            return
            (
                object.Equals(other, null) ? false :
                Common.PerformComparison(this, other, typeof(TimeZoneIANA)) == 0
            );
        }

        ///<summary><para>Determines whether the current TimeZoneIANA variable is equal to other one.</para></summary>
        ///<param name="obj">Other variable.</param>
        public override bool Equals(object obj)
        {
            return Equals(obj as TimeZoneIANA);
        }

        ///<summary><para>Returns the hash code for this TimeZoneIANA variable.</para></summary>
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
