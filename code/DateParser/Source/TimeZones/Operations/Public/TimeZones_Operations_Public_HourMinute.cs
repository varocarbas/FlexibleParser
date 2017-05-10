using System;

namespace FlexibleParser
{
    public partial class HourMinute : IComparable<HourMinute>
    {
        ///<summary><para>Compares the current instance against another HourMinute one.</para></summary>
        ///<param name="other">The other HourMinute instance.</param>
        public int CompareTo(HourMinute other)
        {
            return Common.PerformComparison(this, other, typeof(HourMinute));
        }

        ///<summary><para>Outputs an error or [sign][hours]:[minutes].</para> </summary>
        public override string ToString()
        {
            return HourMinuteInternal.HourMinuteToString(this);
        }

        ///<summary><para>Creates a new HourMinute instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">Offset input.</param>
        public static implicit operator HourMinute(Offset input)
        {
            return new HourMinute(input);
        }

        ///<summary><para>Creates a new HourMinute instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">Decimal input.</param>
        public static implicit operator HourMinute(decimal input)
        {
            return new HourMinute(input);
        }

        ///<summary><para>Determines whether the first argument is greater than the second one.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator >(HourMinute first, HourMinute second)
        {
            return Common.PerformComparison(first, second, typeof(HourMinute)) == 1;
        }

        ///<summary><para>Determines whether the first argument is greater or equal than the second one.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator >=(HourMinute first, HourMinute second)
        {
            return Common.PerformComparison(first, second, typeof(HourMinute)) >= 0;
        }

        ///<summary><para>Determines whether the first argument is smaller than the second one.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator <(HourMinute first, HourMinute second)
        {
            return Common.PerformComparison(first, second, typeof(HourMinute)) == -1;
        }

        ///<summary><para>Determines whether the first argument is smaller or equal than the second one.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator <=(HourMinute first, HourMinute second)
        {
            return Common.PerformComparison(first, second, typeof(HourMinute)) <= 0;
        }

        ///<summary><para>Determines whether two HourMinute variables are equal.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator ==(HourMinute first, HourMinute second)
        {
            return Common.NoNullEquals(first, second, typeof(HourMinute));
        }

        ///<summary><para>Determines whether two HourMinute variables are different.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator !=(HourMinute first, HourMinute second)
        {
            return !Common.NoNullEquals(first, second, typeof(HourMinute));
        }

        ///<summary><para>Determines whether the current HourMinute variable is equal to other one.</para></summary>
        ///<param name="other">Other variable.</param>
        public bool Equals(HourMinute other)
        {
            return
            (
                object.Equals(other, null) ? false :
                Common.PerformComparison(this, other, typeof(HourMinute)) == 0
            );
        }

        ///<summary><para>Determines whether the current HourMinute variable is equal to other one.</para></summary>
        ///<param name="obj">Other variable.</param>
        public override bool Equals(object obj)
        {
            return Equals(obj as HourMinute);
        }

        ///<summary><para>Returns the hash code for this HourMinute variable.</para></summary>
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
