using System;

namespace FlexibleParser
{
    public partial class TimeZoneConventional : IComparable<TimeZoneConventional>
    {
        ///<summary><para>Compares the current instance against another TimeZoneConventional one.</para></summary>
        ///<param name="other">The other TimeZoneConventional instance.</param>
        public int CompareTo(TimeZoneConventional other)
        {
            return Common.PerformComparison(this, other, typeof(TimeZoneConventional));
        }

        ///<summary><para>Outputs an error or "[name] ([abbreviation]) -- UTC [offset]".</para> </summary>
        public override string ToString()
        {
            return TimeZonesInternal.TimeZoneTypeToString(this);
        }

        ///<summary><para>Creates a new TimeZoneConventional instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimeZoneConventionalEnum input.</param>
        public static implicit operator TimeZoneConventional(TimeZoneConventionalEnum input)
        {
            return new TimeZoneConventional(input);
        }

        ///<summary><para>Creates a new TimeZoneConventional instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">String input.</param>
        public static implicit operator TimeZoneConventional(string input)
        {
            return new TimeZoneConventional(input);
        }

        ///<summary><para>Creates a new TimeZoneConventional instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimeZoneInfo input.</param>
        public static implicit operator TimeZoneConventional(TimeZoneInfo input)
        {
            return new TimeZoneConventional(input);
        }

        ///<summary><para>Determines whether two TimeZoneConventional variables are equal.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator ==(TimeZoneConventional first, TimeZoneConventional second)
        {
            return Common.NoNullEquals(first, second, typeof(TimeZoneConventional));
        }

        ///<summary><para>Determines whether two TimeZoneConventional variables are different.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator !=(TimeZoneConventional first, TimeZoneConventional second)
        {
            return !Common.NoNullEquals(first, second, typeof(TimeZoneConventional));
        }

        ///<summary><para>Determines whether the current TimeZoneConventional variable is equal to other one.</para></summary>
        ///<param name="other">Other variable.</param>
        public bool Equals(TimeZoneConventional other)
        {
            return
            (
                object.Equals(other, null) ? false : 
                Common.PerformComparison(this, other, typeof(TimeZoneConventional)) == 0
            );
        }

        ///<summary><para>Determines whether the current TimeZoneConventional variable is equal to other one.</para></summary>
        ///<param name="obj">Other variable.</param>
        public override bool Equals(object obj)
        {
            return Equals(obj as TimeZoneConventional);
        }

        ///<summary><para>Returns the hash code for this TimeZoneConventional variable.</para></summary>
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
