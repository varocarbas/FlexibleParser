using System;

namespace FlexibleParser
{
    public partial class TimeZoneOfficial : IComparable<TimeZoneOfficial>
    {
        ///<summary><para>Compares the current instance against another TimeZoneOfficial one.</para></summary>
        ///<param name="other">The other TimeZoneOfficial instance.</param>
        public int CompareTo(TimeZoneOfficial other)
        {
            return Common.PerformComparison(this, other, typeof(TimeZoneOfficial));
        }

        ///<summary><para>Outputs an error or "[name] ([abbreviation]) -- UTC [offset]".</para> </summary>
        public override string ToString()
        {
            return TimeZonesInternal.TimeZoneTypeToString(this);
        }

        ///<summary><para>Creates a new TimeZoneOfficial instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimeZoneOfficialEnum input.</param>
        public static implicit operator TimeZoneOfficial(TimeZoneOfficialEnum input)
        {
            return new TimeZoneOfficial(input);
        }

        ///<summary><para>Creates a new TimeZoneOfficial instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">String input.</param>
        public static implicit operator TimeZoneOfficial(string input)
        {
            return new TimeZoneOfficial(input);
        }

        ///<summary><para>Creates a new TimeZoneOfficial instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimeZoneInfo input.</param>
        public static implicit operator TimeZoneOfficial(TimeZoneInfo input)
        {
            return new TimeZoneOfficial(input);
        } 

        ///<summary><para>Determines whether two TimeZoneOfficial variables are equal.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator ==(TimeZoneOfficial first, TimeZoneOfficial second)
        {
            return Common.NoNullEquals(first, second, typeof(TimeZoneOfficial));
        }

        ///<summary><para>Determines whether two TimeZoneOfficial variables are different.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator !=(TimeZoneOfficial first, TimeZoneOfficial second)
        {
            return !Common.NoNullEquals(first, second, typeof(TimeZoneOfficial));
        }

        ///<summary><para>Determines whether the current TimeZoneOfficial variable is equal to other one.</para></summary>
        ///<param name="other">Other variable.</param>
        public bool Equals(TimeZoneOfficial other)
        {
            return
            (
                object.Equals(other, null) ? false :
                Common.PerformComparison(this, other, typeof(TimeZoneOfficial)) == 0
            );
        }

        ///<summary><para>Determines whether the current TimeZoneOfficial variable is equal to other one.</para></summary>
        ///<param name="obj">Other variable.</param>
        public override bool Equals(object obj)
        {
            return Equals(obj as TimeZoneOfficial);
        }

        ///<summary><para>Returns the hash code for this TimeZoneOfficial variable.</para></summary>
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
