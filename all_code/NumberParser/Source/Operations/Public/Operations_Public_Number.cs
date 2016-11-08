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

        public static implicit operator Number(decimal input)
        {
            return new Number(input);
        }

        public static implicit operator Number(double input)
        {
            return new Number(input);
        }
        
        public static implicit operator Number(float input)
        {
            return new Number(input);
        }

        public static implicit operator Number(long input)
        {
            return new Number(input);
        }

        public static implicit operator Number(ulong input)
        {
            return new Number(input);
        }

        public static implicit operator Number(int input)
        {
            return new Number(input);
        }

        public static implicit operator Number(uint input)
        {
            return new Number(input);
        }

        public static implicit operator Number(short input)
        {
            return new Number(input);
        }

        public static implicit operator Number(ushort input)
        {
            return new Number(input);
        }

        public static implicit operator Number(byte input)
        {
            return new Number(input);
        }

        public static implicit operator Number(sbyte input)
        {
            return new Number(input);
        }

        public static implicit operator Number(char input)
        {
            return new Number(input);
        }

        public static implicit operator Number(NumberD input)
        {
            return new Number(input);
        }

        public static implicit operator Number(NumberO input)
        {
            return new Number(input);
        }

        public static implicit operator Number(NumberP input)
        {
            return new Number(input);
        }

        public static Number operator +(Number first, Number second)
        {
            return Operations.PerformArithmeticOperation
            (
                first, second, ExistingOperations.Addition
            );
        }

        public static Number operator -(Number first, Number second)
        {
            return Operations.PerformArithmeticOperation
            (
                first, second, ExistingOperations.Subtraction
            );
        }

        public static Number operator *(Number first, Number second)
        {
            return Operations.PerformArithmeticOperation
            (
                first, second, ExistingOperations.Multiplication
            );
        }

        public static Number operator /(Number first, Number second)
        {
            return Operations.PerformArithmeticOperation
            (
                first, second, ExistingOperations.Division
            );
        }

        public static Number operator %(Number first, Number second)
        {
            return Operations.PerformOtherOperation
            (
                first, second, ExistingOperations.Modulo
            );
        }

        public static bool operator >(Number first, Number second)
        {
            return Operations.PerformOtherOperation
            (
                first, second, ExistingOperations.Greater
            )
            .Value == 1m;
        }

        public static bool operator >=(Number first, Number second)
        {
            return Operations.PerformOtherOperation
            (
                first, second, ExistingOperations.GreaterOrEqual
            )
            .Value == 1m;
        }

        public static bool operator <(Number first, Number second)
        {
            return Operations.PerformOtherOperation
            (
                first, second, ExistingOperations.Smaller
            )
            .Value == 1m;
        }

        public static bool operator <=(Number first, Number second)
        {
            return Operations.PerformOtherOperation
            (
                first, second, ExistingOperations.SmallerOrEqual
            )
            .Value == 1m;
        }

        public static bool operator ==(Number first, Number second)
        {
            return Operations.NoNullEquals(first, second);
        }

        public static bool operator !=(Number first, Number second)
        {
            return !Operations.NoNullEquals(first, second);
        }

        public bool Equals(Number other)
        {
            return
            (
                object.Equals(other, null) ? false :
                Operations.NumberXsAreEqual(this, other)
            );
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Number);
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}
