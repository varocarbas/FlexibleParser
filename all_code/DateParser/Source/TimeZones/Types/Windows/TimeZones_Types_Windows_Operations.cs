using System;
using System.Collections.Generic;

namespace FlexibleParser
{
    public partial class TimeZoneWindows : IComparable<TimeZoneWindows>
    {
        ///<summary><para>Compares the current instance against another TimeZoneWindows one.</para></summary>
        ///<param name="other">The other TimeZoneWindows instance.</param>
        public int CompareTo(TimeZoneWindows other)
        {
            return Common.PerformComparison(this, other, typeof(TimeZoneWindows));
        }

        ///<summary><para>Outputs an error or "[name] -- UTC [offset]".</para> </summary>
        public override string ToString()
        {
            return TimeZonesInternal.TimeZoneTypeToString(this);
        }

        ///<summary><para>Creates a new TimeZoneWindows instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimeZoneWindowsEnum input.</param>
        public static implicit operator TimeZoneWindows(TimeZoneWindowsEnum input)
        {
            return new TimeZoneWindows(input);
        }

        ///<summary><para>Creates a new TimeZoneWindows instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">String input.</param>
        public static implicit operator TimeZoneWindows(string input)
        {
            return new TimeZoneWindows(input);
        }

        ///<summary><para>Creates a new TimeZoneWindows instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimeZoneInfo input.</param>
        public static implicit operator TimeZoneWindows(TimeZoneInfo input)
        {
            return new TimeZoneWindows(input);
        } 

        ///<summary><para>Determines whether two TimeZoneWindows variables are equal.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator ==(TimeZoneWindows first, TimeZoneWindows second)
        {
            return Common.NoNullEquals(first, second, typeof(TimeZoneWindows));
        }

        ///<summary><para>Determines whether two TimeZoneWindows variables are different.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator !=(TimeZoneWindows first, TimeZoneWindows second)
        {
            return !Common.NoNullEquals(first, second, typeof(TimeZoneWindows));
        }

        ///<summary><para>Determines whether the current TimeZoneWindows variable is equal to other one.</para></summary>
        ///<param name="other">Other variable.</param>
        public bool Equals(TimeZoneWindows other)
        {
            return
            (
                object.Equals(other, null) ? false :
                Common.PerformComparison(this, other, typeof(TimeZoneWindows)) == 0
            );
        }

        ///<summary><para>Determines whether the current TimeZoneWindows variable is equal to other one.</para></summary>
        ///<param name="obj">Other variable.</param>
        public override bool Equals(object obj)
        {
            return Equals(obj as TimeZoneWindows);
        }

        ///<summary><para>Returns the hash code for this TimeZoneWindows variable.</para></summary>
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
