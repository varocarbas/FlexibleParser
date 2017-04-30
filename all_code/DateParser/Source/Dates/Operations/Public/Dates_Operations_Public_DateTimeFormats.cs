using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    public partial class CustomDateTimeFormat : IComparable<CustomDateTimeFormat>
    {
        ///<summary><para>Compares the current instance against another CustomDateTimeFormat one.</para></summary>
        ///<param name="other">The other CustomDateTimeFormat instance.</param>
        public int CompareTo(CustomDateTimeFormat other)
        {
            return Common.PerformComparison(this, other, typeof(CustomDateTimeFormat));
        }

        ///<summary><para>Outputs an error or "[name] ([abbreviation]) -- UTC [offset]".</para> </summary>
        public override string ToString()
        {
            return TimeZonesInternal.TimeZoneTypeToString(this);
        }

        ///<summary><para>Creates a new CustomDateTimeFormat instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">DateTimeParts[] input.</param>
        public static implicit operator CustomDateTimeFormat(DateTimeParts[] input)
        {
            return new CustomDateTimeFormat(input);
        }

        ///<summary><para>Creates a new CustomDateTimeFormat instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">String input.</param>
        public static implicit operator CustomDateTimeFormat(string input)
        {
            return new CustomDateTimeFormat(input);
        }

        ///<summary><para>Determines whether two CustomDateTimeFormat variables are equal.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator ==(CustomDateTimeFormat first, CustomDateTimeFormat second)
        {
            return Common.NoNullEquals(first, second, typeof(CustomDateTimeFormat));
        }

        ///<summary><para>Determines whether two CustomDateTimeFormat variables are different.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator !=(CustomDateTimeFormat first, CustomDateTimeFormat second)
        {
            return !Common.NoNullEquals(first, second, typeof(CustomDateTimeFormat));
        }

        ///<summary><para>Determines whether the current CustomDateTimeFormat variable is equal to other one.</para></summary>
        ///<param name="other">Other variable.</param>
        public bool Equals(CustomDateTimeFormat other)
        {
            return
            (
                object.Equals(other, null) ? false :
                Common.PerformComparison(this, other, typeof(CustomDateTimeFormat)) == 0
            );
        }

        ///<summary><para>Determines whether the current CustomDateTimeFormat variable is equal to other one.</para></summary>
        ///<param name="obj">Other variable.</param>
        public override bool Equals(object obj)
        {
            return Equals(obj as CustomDateTimeFormat);
        }

        ///<summary><para>Returns the hash code for this CustomDateTimeFormat variable.</para></summary>
        public override int GetHashCode()
        {
            return 0;
        }
    }

    public partial class StandardDateTimeFormat
    {

    }
}
