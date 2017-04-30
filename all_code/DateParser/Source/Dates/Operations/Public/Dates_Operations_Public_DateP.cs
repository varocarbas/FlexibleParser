using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    public partial class DateP : IComparable<DateP>
    {
        ///<summary><para>Compares the current instance against another DateP one.</para></summary>
        ///<param name="other">The other DateP instance.</param>
        public int CompareTo(DateP other)
        {
            return Common.PerformComparison(this, other, typeof(DateP));
        }

        ///<summary><para>Creates a new DateP instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">DateTime input.</param>
        public static implicit operator DateP(DateTime input)
        {
            return new DateP(input);
        }

        ///<summary><para>Creates a new DateP instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">String input.</param>
        public static implicit operator DateP(string input)
        {
            return new DateP(input);
        }

        ///<summary><para>Determines whether the first argument is greater than the second one.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator >(DateP first, DateP second)
        {
            return Common.PerformComparison(first, second, typeof(DateP)) == 1;
        }

        ///<summary><para>Determines whether the first argument is greater or equal than the second one.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator >=(DateP first, DateP second)
        {
            return Common.PerformComparison(first, second, typeof(DateP)) >= 0;
        }

        ///<summary><para>Determines whether the first argument is smaller than the second one.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator <(DateP first, DateP second)
        {
            return Common.PerformComparison(first, second, typeof(DateP)) == -1;
        }

        ///<summary><para>Determines whether the first argument is smaller or equal than the second one.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator <=(DateP first, DateP second)
        {
            return Common.PerformComparison(first, second, typeof(DateP)) <= 0;
        }

        ///<summary><para>Determines whether two DateP variables are equal.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator ==(DateP first, DateP second)
        {
            return Common.NoNullEquals(first, second, typeof(DateP));
        }

        ///<summary><para>Determines whether two DateP variables are different.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator !=(DateP first, DateP second)
        {
            return !Common.NoNullEquals(first, second, typeof(DateP));
        }

        ///<summary><para>Determines whether the current DateP variable is equal to other one.</para></summary>
        ///<param name="other">Other variable.</param>
        public bool Equals(DateP other)
        {
            return
            (
                object.Equals(other, null) ? false :
                Common.PerformComparison(this, other, typeof(DateP)) == 0
            );
        }

        ///<summary><para>Determines whether the current DateP variable is equal to other one.</para></summary>
        ///<param name="obj">Other variable.</param>
        public override bool Equals(object obj)
        {
            return Equals(obj as DateP);
        }

        ///<summary><para>Returns the hash code for this DateP variable.</para></summary>
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
