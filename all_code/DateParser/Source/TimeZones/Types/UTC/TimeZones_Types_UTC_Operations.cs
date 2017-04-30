using System;
using System.Collections.Generic;

namespace FlexibleParser
{
    public partial class TimeZoneUTC : IComparable<TimeZoneUTC>
    {
        ///<summary><para>Compares the current instance against another TimeZoneUTC one.</para></summary>
        ///<param name="other">The other TimeZoneUTC instance.</param>
        public int CompareTo(TimeZoneUTC other)
        {
            return Common.PerformComparison(this, other, typeof(TimeZoneUTC));
        }

        ///<summary><para>Outputs an error or "UTC [offset]".</para> </summary>
        public override string ToString()
        {
            return TimeZonesInternal.TimeZoneTypeToString(this);
        }

        ///<summary><para>Creates a new TimeZoneUTC instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimeZoneUTCEnum input.</param>
        public static implicit operator TimeZoneUTC(TimeZoneUTCEnum input)
        {
            return new TimeZoneUTC(input);
        }

        ///<summary><para>Creates a new TimeZoneUTC instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">String input.</param>
        public static implicit operator TimeZoneUTC(string input)
        {
            return new TimeZoneUTC(input);
        }

        ///<summary><para>Creates a new TimeZoneUTC instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimeZoneInfo input.</param>
        public static implicit operator TimeZoneUTC(TimeZoneInfo input)
        {
            return new TimeZoneUTC(input);
        } 

        ///<summary><para>Determines whether two TimeZoneUTC variables are equal.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator ==(TimeZoneUTC first, TimeZoneUTC second)
        {
            return Common.NoNullEquals(first, second, typeof(TimeZoneUTC));
        }

        ///<summary><para>Determines whether two TimeZoneUTC variables are different.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator !=(TimeZoneUTC first, TimeZoneUTC second)
        {
            return !Common.NoNullEquals(first, second, typeof(TimeZoneUTC));
        }

        ///<summary><para>Determines whether the current TimeZoneUTC variable is equal to other one.</para></summary>
        ///<param name="other">Other variable.</param>
        public bool Equals(TimeZoneUTC other)
        {
            return
            (
                object.Equals(other, null) ? false : 
                Common.PerformComparison(this, other, typeof(TimeZoneUTC)) == 0
            );
        }

        ///<summary><para>Determines whether the current TimeZoneUTC variable is equal to other one.</para></summary>
        ///<param name="obj">Other variable.</param>
        public override bool Equals(object obj)
        {
            return Equals(obj as TimeZoneUTC);
        }

        ///<summary><para>Returns the hash code for this TimeZoneUTC variable.</para></summary>
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
