using System;
using System.Collections.Generic;
using System.Globalization;

namespace FlexibleParser
{
    public partial class Number
    {
        ///<summary>
        ///<para>Outputs an error or "Value*10^BaseTenExponent" (BaseTenExponent different than zero sample).</para>
        ///</summary>
        public override string ToString()
        {
            return ToString(CultureInfo.InvariantCulture);
        }

        ///<summary>
        ///<para>Outputs an error or "Value*10^BaseTenExponent" (BaseTenExponent different than zero sample).</para>
        ///</summary>
        ///<param name="culture">Culture.</param>
        public string ToString(CultureInfo culture)
        {
            if (Error != ErrorTypesNumber.None) return "Error. " + Error.ToString();
            if (culture == null) culture = CultureInfo.InvariantCulture;

            Number number = Operations.PassBaseTenToValue(this, true);
            return Operations.PrintNumberXInfo
            (
                number.Value, number.BaseTenExponent, null, culture
            );
        }

        ///<summary><para>Converts the input variable into a Number one.</para></summary>
        ///<param name="input">Variable to be converted to Number.</param>
        public static implicit operator Number(decimal input)
        {
            return new Number(input);
        }

        ///<summary><para>Converts the input variable into a Number one.</para></summary>
        ///<param name="input">Variable to be converted to Number.</param>
        public static implicit operator Number(double input)
        {
            return new Number(input);
        }

        ///<summary><para>Converts the input variable into a Number one.</para></summary>
        ///<param name="input">Variable to be converted to Number.</param>
        public static implicit operator Number(float input)
        {
            return new Number(input);
        }

        ///<summary><para>Converts the input variable into a Number one.</para></summary>
        ///<param name="input">Variable to be converted to Number.</param>
        public static implicit operator Number(long input)
        {
            return new Number(input);
        }

        ///<summary><para>Converts the input variable into a Number one.</para></summary>
        ///<param name="input">Variable to be converted to Number.</param>
        public static implicit operator Number(ulong input)
        {
            return new Number(input);
        }

        ///<summary><para>Converts the input variable into a Number one.</para></summary>
        ///<param name="input">Variable to be converted to Number.</param>
        public static implicit operator Number(int input)
        {
            return new Number(input);
        }
 
        ///<summary><para>Converts the input variable into a Number one.</para></summary>
        ///<param name="input">Variable to be converted to Number.</param>
        public static implicit operator Number(uint input)
        {
            return new Number(input);
        }

        ///<summary><para>Converts the input variable into a Number one.</para></summary>
        ///<param name="input">Variable to be converted to Number.</param>
        public static implicit operator Number(short input)
        {
            return new Number(input);
        }

        ///<summary><para>Converts the input variable into a Number one.</para></summary>
        ///<param name="input">Variable to be converted to Number.</param>
        public static implicit operator Number(ushort input)
        {
            return new Number(input);
        }

        ///<summary><para>Converts the input variable into a Number one.</para></summary>
        ///<param name="input">Variable to be converted to Number.</param>
        public static implicit operator Number(byte input)
        {
            return new Number(input);
        }

        ///<summary><para>Converts the input variable into a Number one.</para></summary>
        ///<param name="input">Variable to be converted to Number.</param>
        public static implicit operator Number(sbyte input)
        {
            return new Number(input);
        }

        ///<summary><para>Converts the input variable into a Number one.</para></summary>
        ///<param name="input">Variable to be converted to Number.</param>
        public static implicit operator Number(char input)
        {
            return new Number(input);
        }
 
        ///<summary><para>Converts the input variable into a Number one.</para></summary>
        ///<param name="input">Variable to be converted to Number.</param>
        public static implicit operator Number(NumberD input)
        {
            return new Number(input);
        }

        ///<summary><para>Converts the input variable into a Number one.</para></summary>
        ///<param name="input">Variable to be converted to Number.</param>
        public static implicit operator Number(NumberO input)
        {
            return new Number(input);
        }

        ///<summary><para>Converts the input variable into a Number one.</para></summary>
        ///<param name="input">Variable to be converted to Number.</param>
        public static implicit operator Number(NumberP input)
        {
            return new Number(input);
        }

        ///<summary><para>Adds two Number variables.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static Number operator +(Number first, Number second)
        {
            return Operations.PerformArithmeticOperation
            (
                first, second, ExistingOperations.Addition
            );
        }

        ///<summary><para>Subtracts two Number variables.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static Number operator -(Number first, Number second)
        {
            return Operations.PerformArithmeticOperation
            (
                first, second, ExistingOperations.Subtraction
            );
        }

        ///<summary><para>Multiplies two Number variables.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static Number operator *(Number first, Number second)
        {
            return Operations.PerformArithmeticOperation
            (
                first, second, ExistingOperations.Multiplication
            );
        }

        ///<summary><para>Divides two Number variables.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static Number operator /(Number first, Number second)
        {
            return Operations.PerformArithmeticOperation
            (
                first, second, ExistingOperations.Division
            );
        }

        ///<summary><para>Calculates the modulo of two Number variables.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static Number operator %(Number first, Number second)
        {
            return Operations.PerformOtherOperation
            (
                first, second, ExistingOperations.Modulo
            );
        }

        ///<summary><para>Determines whether a Number variable is greater than other.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator >(Number first, Number second)
        {
            return Operations.PerformOtherOperation
            (
                first, second, ExistingOperations.Greater
            )
            .Value == 1m;
        }

        ///<summary><para>Determines whether a Number variable is greater or equal than other.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator >=(Number first, Number second)
        {
            return Operations.PerformOtherOperation
            (
                first, second, ExistingOperations.GreaterOrEqual
            )
            .Value == 1m;
        }

        ///<summary><para>Determines whether a Number variable is smaller than other.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator <(Number first, Number second)
        {
            return Operations.PerformOtherOperation
            (
                first, second, ExistingOperations.Smaller
            )
            .Value == 1m;
        }

        ///<summary><para>Determines whether a Number variable is smaller or equal than other.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator <=(Number first, Number second)
        {
            return Operations.PerformOtherOperation
            (
                first, second, ExistingOperations.SmallerOrEqual
            )
            .Value == 1m;
        }

        ///<summary><para>Determines whether two Number variables are equal.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator ==(Number first, Number second)
        {
            return Operations.NoNullEquals(first, second);
        }

        ///<summary><para>Determines whether two Number variables are different.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator !=(Number first, Number second)
        {
            return !Operations.NoNullEquals(first, second);
        }

        ///<summary><para>Determines whether the current Number variable is equal to other one.</para></summary>
        ///<param name="other">Other variable.</param>
        public bool Equals(Number other)
        {
            return
            (
                object.Equals(other, null) ? false :
                Operations.NumberXsAreEqual(this, other)
            );
        }

        ///<summary><para>Determines whether the current Number variable is equal to other one.</para></summary>
        ///<param name="obj">Other variable.</param>
        public override bool Equals(object obj)
        {
            return Equals(obj as Number);
        }

        ///<summary><para>Returns the hash code for this Number variable.</para></summary>
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
