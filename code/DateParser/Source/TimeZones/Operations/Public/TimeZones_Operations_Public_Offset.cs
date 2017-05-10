using System;
using System.Collections.Generic;

namespace FlexibleParser
{
    public partial class Offset : IComparable<Offset>
    {
        ///<summary><para>Compares the current instance against another Offset one.</para></summary>
        ///<param name="other">The other Offset instance.</param>
        public int CompareTo(Offset other)
        {
            return Common.PerformComparison(this, other, typeof(Offset));
        }

        ///<summary><para>Outputs an error or [sign][hours]:[minutes].</para> </summary>
        public override string ToString()
        {
            return OffsetInternal.OffsetToString(this);
        }

        ///<summary><para>Creates a new Offset instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">Decimal input.</param>
        public static implicit operator Offset(decimal input)
        {
            return new Offset(input);
        }

        ///<summary><para>Creates a new Offset instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimeZoneOfficial input.</param>
        public static implicit operator Offset(TimeZoneOfficial input)
        {
            return new Offset(input);
        }

        ///<summary><para>Creates a new Offset instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimeZoneOfficialEnum input.</param>
        public static implicit operator Offset(TimeZoneOfficialEnum input)
        {
            return new Offset(input);
        }

        ///<summary><para>Creates a new Offset instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimeZoneIANA input.</param>
        public static implicit operator Offset(TimeZoneIANA input)
        {
            return new Offset(input);
        }

        ///<summary><para>Creates a new Offset instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimeZoneIANAEnum input.</param>
        public static implicit operator Offset(TimeZoneIANAEnum input)
        {
            return new Offset(input);
        }

        ///<summary><para>Creates a new Offset instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimeZoneConventional input.</param>
        public static implicit operator Offset(TimeZoneConventional input)
        {
            return new Offset(input);
        }

        ///<summary><para>Creates a new Offset instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimeZoneConventionalEnum input.</param>
        public static implicit operator Offset(TimeZoneConventionalEnum input)
        {
            return new Offset(input);
        }

        ///<summary><para>Creates a new Offset instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimeZoneUTC input.</param>
        public static implicit operator Offset(TimeZoneUTC input)
        {
            return new Offset(input);
        }

        ///<summary><para>Creates a new Offset instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimeZoneUTCEnum input.</param>
        public static implicit operator Offset(TimeZoneUTCEnum input)
        {
            return new Offset(input);
        }

        ///<summary><para>Creates a new Offset instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimeZoneWindows input.</param>
        public static implicit operator Offset(TimeZoneWindows input)
        {
            return new Offset(input);
        }

        ///<summary><para>Creates a new Offset instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimeZoneWindowsEnum input.</param>
        public static implicit operator Offset(TimeZoneWindowsEnum input)
        {
            return new Offset(input);
        }

        ///<summary><para>Creates a new Offset instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimeZoneMilitary input.</param>
        public static implicit operator Offset(TimeZoneMilitary input)
        {
            return new Offset(input);
        }

        ///<summary><para>Creates a new Offset instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimeZoneMilitaryEnum input.</param>
        public static implicit operator Offset(TimeZoneMilitaryEnum input)
        {
            return new Offset(input);
        }

        ///<summary><para>Determines whether the first argument is greater than the second one.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator >(Offset first, Offset second)
        {
            return Common.PerformComparison(first, second, typeof(Offset)) == 1;
        }

        ///<summary><para>Determines whether the first argument is greater or equal than the second one.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator >=(Offset first, Offset second)
        {
            return Common.PerformComparison(first, second, typeof(Offset)) >= 0;
        }

        ///<summary><para>Determines whether the first argument is smaller than the second one.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator <(Offset first, Offset second)
        {
            return Common.PerformComparison(first, second, typeof(Offset)) == -1;
        }

        ///<summary><para>Determines whether the first argument is smaller or equal than the second one.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator <=(Offset first, Offset second)
        {
            return Common.PerformComparison(first, second, typeof(Offset)) <= 0;
        }

        ///<summary><para>Determines whether two Offset variables are equal.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator ==(Offset first, Offset second)
        {
            return Common.NoNullEquals(first, second, typeof(Offset));
        }

        ///<summary><para>Determines whether two Offset variables are different.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator !=(Offset first, Offset second)
        {
            return !Common.NoNullEquals(first, second, typeof(Offset));
        }

        ///<summary><para>Determines whether the current Offset variable is equal to other one.</para></summary>
        ///<param name="other">Other variable.</param>
        public bool Equals(Offset other)
        {
            return
            (
                object.Equals(other, null) ? false : 
                Common.PerformComparison(this, other, typeof(Offset)) == 0
            );
        }

        ///<summary><para>Determines whether the current Offset variable is equal to other one.</para></summary>
        ///<param name="obj">Other variable.</param>
        public override bool Equals(object obj)
        {
            return Equals(obj as Offset);
        }

        ///<summary><para>Returns the hash code for this Offset variable.</para></summary>
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
