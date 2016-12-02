using System;
using System.Collections.Generic;
using System.Globalization;

namespace FlexibleParser
{
    public partial class NumberD
    {
        ///<summary>
        ///<para>Outputs an error or "Value*10^BaseTenExponent (Type)" (BaseTenExponent different than zero sample).</para>
        ///</summary>
        public override string ToString()
        {
            return ToString(CultureInfo.InvariantCulture);
        }

        ///<summary>
        ///<para>Outputs an error or "Value*10^BaseTenExponent (Type)" (BaseTenExponent different than zero sample).</para>
        ///</summary>
        ///<param name="culture">Culture.</param>
        public string ToString(CultureInfo culture)
        {
            if (Error != ErrorTypesNumber.None) return "Error. " + Error.ToString();
            if (culture == null) culture = CultureInfo.InvariantCulture;

            NumberD numberD = Operations.PassBaseTenToValue((NumberD)this, true);
            return Operations.PrintNumberXInfo
            (
                numberD.Value, numberD.BaseTenExponent, Type, culture
            );
        }

        ///<summary><para>Converts the input variable into a NumberD one.</para></summary>
        ///<param name="input">Variable to be converted to NumberD.</param>
        public static implicit operator NumberD(decimal input)
        {
            return new NumberD(input);
        }

        ///<summary><para>Converts the input variable into a NumberD one.</para></summary>
        ///<param name="input">Variable to be converted to NumberD.</param>
        public static implicit operator NumberD(double input)
        {
            return new NumberD(input);
        }

        ///<summary><para>Converts the input variable into a NumberD one.</para></summary>
        ///<param name="input">Variable to be converted to NumberD.</param>
        public static implicit operator NumberD(float input)
        {
            return new NumberD(input);
        }

        ///<summary><para>Converts the input variable into a NumberD one.</para></summary>
        ///<param name="input">Variable to be converted to NumberD.</param>
        public static implicit operator NumberD(long input)
        {
            return new NumberD(input);
        }

        ///<summary><para>Converts the input variable into a NumberD one.</para></summary>
        ///<param name="input">Variable to be converted to NumberD.</param>
        public static implicit operator NumberD(ulong input)
        {
            return new NumberD(input);
        }

        ///<summary><para>Converts the input variable into a NumberD one.</para></summary>
        ///<param name="input">Variable to be converted to NumberD.</param>
        public static implicit operator NumberD(int input)
        {
            return new NumberD(input);
        }

        ///<summary><para>Converts the input variable into a NumberD one.</para></summary>
        ///<param name="input">Variable to be converted to NumberD.</param>
        public static implicit operator NumberD(uint input)
        {
            return new NumberD(input);
        }

        ///<summary><para>Converts the input variable into a NumberD one.</para></summary>
        ///<param name="input">Variable to be converted to NumberD.</param>
        public static implicit operator NumberD(short input)
        {
            return new NumberD(input);
        }

        ///<summary><para>Converts the input variable into a NumberD one.</para></summary>
        ///<param name="input">Variable to be converted to NumberD.</param>
        public static implicit operator NumberD(ushort input)
        {
            return new NumberD(input);
        }

        ///<summary><para>Converts the input variable into a NumberD one.</para></summary>
        ///<param name="input">Variable to be converted to NumberD.</param>
        public static implicit operator NumberD(byte input)
        {
            return new NumberD(input);
        }

        ///<summary><para>Converts the input variable into a NumberD one.</para></summary>
        ///<param name="input">Variable to be converted to NumberD.</param>
        public static implicit operator NumberD(sbyte input)
        {
            return new NumberD(input);
        }

        ///<summary><para>Converts the input variable into a NumberD one.</para></summary>
        ///<param name="input">Variable to be converted to NumberD.</param>
        public static implicit operator NumberD(char input)
        {
            return new NumberD(input);
        }

        ///<summary><para>Converts the input variable into a NumberD one.</para></summary>
        ///<param name="input">Variable to be converted to NumberD.</param>
        public static implicit operator NumberD(Number input)
        {
            return new NumberD(input);
        }

        ///<summary><para>Converts the input variable into a NumberD one.</para></summary>
        ///<param name="input">Variable to be converted to NumberD.</param>
        public static implicit operator NumberD(NumberO input)
        {
            return new NumberD(input);
        }

        ///<summary><para>Converts the input variable into a NumberD one.</para></summary>
        ///<param name="input">Variable to be converted to NumberD.</param>
        public static implicit operator NumberD(NumberP input)
        {
            return new NumberD(input);
        }

        ///<summary><para>Adds two NumberD variables.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static NumberD operator +(NumberD first, NumberD second)
        {
            return Operations.PerformArithmeticOperation
            (
                first, second, ExistingOperations.Addition
            );
        }

        ///<summary><para>Subtracts two NumberD variables.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static NumberD operator -(NumberD first, NumberD second)
        {
            return Operations.PerformArithmeticOperation
            (
                first, second, ExistingOperations.Subtraction
            );
        }

        ///<summary><para>Multiplies two NumberD variables.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static NumberD operator *(NumberD first, NumberD second)
        {
            return Operations.PerformArithmeticOperation
            (
                first, second, ExistingOperations.Multiplication
            );
        }

        ///<summary><para>Divides two NumberD variables.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static NumberD operator /(NumberD first, NumberD second)
        {
            return Operations.PerformArithmeticOperation
            (
                first, second, ExistingOperations.Division
            );
        }

        ///<summary><para>Calculates the modulo of two NumberD variables.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static decimal operator %(NumberD first, NumberD second)
        {
            return first.Value % second.Value;
        }

        ///<summary><para>Determines whether a NumberD variable is greater than other.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator >(NumberD first, NumberD second)
        {
            return Operations.PerformOtherOperation
            (
                first, second, ExistingOperations.Greater
            )
            .Value == 1m;
        }

        ///<summary><para>Determines whether a NumberD variable is greater or equal than other.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator >=(NumberD first, NumberD second)
        {
            return Operations.PerformOtherOperation
            (
                first, second, ExistingOperations.GreaterOrEqual
            )
            .Value == 1m;
        }

        ///<summary><para>Determines whether a NumberD variable is smaller than other.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator <(NumberD first, NumberD second)
        {
            return Operations.PerformOtherOperation
            (
                first, second, ExistingOperations.Smaller
            )
            .Value == 1m;
        }

        ///<summary><para>Determines whether a NumberD variable is smaller or equal than other.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator <=(NumberD first, NumberD second)
        {
            return Operations.PerformOtherOperation
            (
                first, second, ExistingOperations.SmallerOrEqual
            )
            .Value == 1m;
        }

        ///<summary><para>Determines whether two NumberD variables are equal.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator ==(NumberD first, NumberD second)
        {
            return Operations.NoNullEquals(first, second);
        }

        ///<summary><para>Determines whether two NumberD variables are different.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator !=(NumberD first, NumberD second)
        {
            return !Operations.NoNullEquals(first, second);
        }

        ///<summary><para>Determines whether the current NumberD variable is equal to other one.</para></summary>
        ///<param name="other">Other variable.</param>
        public bool Equals(NumberD other)
        {
            return
            (
                object.Equals(other, null) ? false :
                Operations.NumberXsAreEqual(this, other)
            );
        }

        ///<summary><para>Determines whether the current NumberD variable is equal to other one.</para></summary>
        ///<param name="obj">Other variable.</param>
        public override bool Equals(object obj)
        {
            return Equals(obj as NumberD);
        }

        ///<summary><para>Returns the hash code for this NumberD variable.</para></summary>
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
