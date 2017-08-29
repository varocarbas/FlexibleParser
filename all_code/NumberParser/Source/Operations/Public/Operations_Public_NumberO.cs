using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

namespace FlexibleParser
{
    public partial class NumberO : IComparable<NumberO>
    {
        ///<summary><para>Compares the current instance against another NumberO one.</para></summary>
        ///<param name="other">The other NumberO instance.</param>
        public int CompareTo(NumberO other)
        {
            return Operations.CompareDecimal(this, other);
        }

        ///<summary>
        ///<para>Outputs an error or "Value*10^BaseTenExponent (Type)" (BaseTenExponent different than zero sample) for the main information and all the items in Others.</para>
        ///</summary>
        public override string ToString()
        {
            return ToString(CultureInfo.InvariantCulture);
        }

        ///<summary>
        ///<para>Outputs an error or "Value*10^BaseTenExponent (Type)" (BaseTenExponent different than zero sample) for the main information and all the items in Others.</para>
        ///</summary>
        ///<param name="culture">Culture.</param>
        public string ToString(CultureInfo culture)
        {
            if (Error != ErrorTypesNumber.None) return "Error. " + Error.ToString();
            if (culture == null) culture = CultureInfo.InvariantCulture;

            Number number = Operations.PassBaseTenToValue((Number)this, true);
            string output = Operations.PrintNumberXInfo
            (
                number.Value, number.BaseTenExponent, typeof(decimal), culture
            );

            foreach (var other in Others)
            {
                output += Environment.NewLine + Operations.PrintNumberXInfo
                (
                    other.Value, other.BaseTenExponent, other.Type, culture
                );
            }

            return output;
        }

        ///<summary><para>Creates a new NumberO instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">Decimal input.</param>
        public static implicit operator NumberO(decimal input)
        {
            return new NumberO(input);
        }

        ///<summary><para>Creates a new NumberO instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">Double input.</param>
        public static implicit operator NumberO(double input)
        {
            return new NumberO(input);
        }

        ///<summary><para>Creates a new NumberO instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">Float input.</param>
        public static implicit operator NumberO(float input)
        {
            return new NumberO(input);
        }

        ///<summary><para>Creates a new NumberO instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">Long input.</param>
        public static implicit operator NumberO(long input)
        {
            return new NumberO(input);
        }

        ///<summary><para>Creates a new NumberO instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">Ulong input.</param>
        public static implicit operator NumberO(ulong input)
        {
            return new NumberO(input);
        }

        ///<summary><para>Creates a new NumberO instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">Int input.</param>
        public static implicit operator NumberO(int input)
        {
            return new NumberO(input);
        }

        ///<summary><para>Creates a new NumberO instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">Uint input.</param>
        public static implicit operator NumberO(uint input)
        {
            return new NumberO(input);
        }

        ///<summary><para>Creates a new NumberO instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">Short input.</param>
        public static implicit operator NumberO(short input)
        {
            return new NumberO(input);
        }

        ///<summary><para>Creates a new NumberO instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">Ushort input.</param>
        public static implicit operator NumberO(ushort input)
        {
            return new NumberO(input);
        }

        ///<summary><para>Creates a new NumberO instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">Byte input.</param>
        public static implicit operator NumberO(byte input)
        {
            return new NumberO(input);
        }

        ///<summary><para>Creates a new NumberO instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">Sbyte input.</param>
        public static implicit operator NumberO(sbyte input)
        {
            return new NumberO(input);
        }

        ///<summary><para>Creates a new NumberO instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">Char input.</param>
        public static implicit operator NumberO(char input)
        {
            return new NumberO(input);
        }

        ///<summary><para>Creates a new NumberO instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">Number input.</param>
        public static implicit operator NumberO(Number input)
        {
            return new NumberO(input);
        }

        ///<summary><para>Creates a new NumberO instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">NumberD input.</param>
        public static implicit operator NumberO(NumberD input)
        {
            return new NumberO(input);
        }

        ///<summary><para>Creates a new NumberO instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">NumberP input.</param>
        public static implicit operator NumberO(NumberP input)
        {
            return new NumberO(input);
        }

        ///<summary><para>Adds two NumberO variables.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static NumberO operator +(NumberO first, NumberO second)
        {
            return Operations.PerformArithmeticOperation
            (
                first, second, ExistingOperations.Addition
            );
        }

        ///<summary><para>Subtracts two NumberO variables.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static NumberO operator -(NumberO first, NumberO second)
        {
            return Operations.PerformArithmeticOperation
            (
                first, second, ExistingOperations.Subtraction
            );
        }

        ///<summary><para>Multiplies two NumberO variables.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static NumberO operator *(NumberO first, NumberO second)
        {
            return Operations.PerformArithmeticOperation
            (
                first, second, ExistingOperations.Multiplication
            );
        }

        ///<summary><para>Divides two NumberO variables.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static NumberO operator /(NumberO first, NumberO second)
        {
            return Operations.PerformArithmeticOperation
            (
                first, second, ExistingOperations.Division
            );
        }

        ///<summary><para>Calculates the modulo of two NumberO variables.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static decimal operator %(NumberO first, NumberO second)
        {
            return first.Value % second.Value;
        }

        ///<summary><para>Determines whether a NumberO variable is greater than other.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator >(NumberO first, NumberO second)
        {
			return Operations.CompareDecimal(first, second) == 1;
        }

        ///<summary><para>Determines whether a NumberO variable is greater or equal than other.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator >=(NumberO first, NumberO second)
        {
			return Operations.CompareDecimal(first, second) >= 0;
        }

        ///<summary><para>Determines whether a NumberO variable is smaller than other.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator <(NumberO first, NumberO second)
        {
			return Operations.CompareDecimal(first, second) == -1;
        }

        ///<summary><para>Determines whether a NumberO variable is smaller or equal than other.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator <=(NumberO first, NumberO second)
        {
			return Operations.CompareDecimal(first, second) <= 0;
        }

        ///<summary><para>Determines whether two NumberO variables are equal.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator ==(NumberO first, NumberO second)
        {
            return Operations.NoNullEquals(first, second);
        }

        ///<summary><para>Determines whether two NumberO variables are different.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator !=(NumberO first, NumberO second)
        {
            return !Operations.NoNullEquals(first, second);
        }

        ///<summary><para>Determines whether the current NumberO variable is equal to other one.</para></summary>
        ///<param name="other">Other variable.</param>
        public bool Equals(NumberO other)
        {
            return
            (
                object.Equals(other, null) ? false :
                Operations.NumberXsAreEqual(this, other)
            );
        }

        ///<summary><para>Determines whether the current NumberO variable is equal to other one.</para></summary>
        ///<param name="obj">Other variable.</param>
        public override bool Equals(object obj)
        {
            return Equals(obj as NumberO);
        }

        ///<summary><para>Returns the hash code for this NumberO variable.</para></summary>
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
