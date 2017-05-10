using System;

namespace FlexibleParser
{
    public partial class TimeZoneMilitary : IComparable<TimeZoneMilitary>
    {
        ///<summary><para>Compares the current instance against another TimeZoneMilitary one.</para></summary>
        ///<param name="other">The other TimeZoneMilitary instance.</param>
        public int CompareTo(TimeZoneMilitary other)
        {
            return Common.PerformComparison(this, other, typeof(TimeZoneMilitary));
        }

        ///<summary><para>Outputs an error or "[name] ([abbreviation]) -- UTC [offset]".</para> </summary>
        public override string ToString()
        {
            return TimeZonesInternal.TimeZoneTypeToString(this);
        }

        ///<summary><para>Creates a new TimeZoneMilitary instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimeZoneMilitaryEnum input.</param>
        public static implicit operator TimeZoneMilitary(TimeZoneMilitaryEnum input)
        {
            return new TimeZoneMilitary(input);
        }

        ///<summary><para>Creates a new TimeZoneMilitary instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">String input.</param>
        public static implicit operator TimeZoneMilitary(string input)
        {
            return new TimeZoneMilitary(input);
        }

        ///<summary><para>Creates a new TimeZoneMilitary instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimeZoneInfo input.</param>
        public static implicit operator TimeZoneMilitary(TimeZoneInfo input)
        {
            return new TimeZoneMilitary(input);
        } 

        ///<summary><para>Determines whether two TimeZoneMilitary variables are equal.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator ==(TimeZoneMilitary first, TimeZoneMilitary second)
        {
            return Common.NoNullEquals(first, second, typeof(TimeZoneMilitary));
        }

        ///<summary><para>Determines whether two TimeZoneMilitary variables are different.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator !=(TimeZoneMilitary first, TimeZoneMilitary second)
        {
            return !Common.NoNullEquals(first, second, typeof(TimeZoneMilitary));
        }

        ///<summary><para>Determines whether the current TimeZoneMilitary variable is equal to other one.</para></summary>
        ///<param name="other">Other variable.</param>
        public bool Equals(TimeZoneMilitary other)
        {
            return
            (
                object.Equals(other, null) ? false :
                Common.PerformComparison(this, other, typeof(TimeZoneMilitary)) == 0
            );
        }

        ///<summary><para>Determines whether the current TimeZoneMilitary variable is equal to other one.</para></summary>
        ///<param name="obj">Other variable.</param>
        public override bool Equals(object obj)
        {
            return Equals(obj as TimeZoneMilitary);
        }

        ///<summary><para>Returns the hash code for this TimeZoneMilitary variable.</para></summary>
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
