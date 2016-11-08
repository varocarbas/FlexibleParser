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

        public static implicit operator NumberD(decimal input)
        {
            return new NumberD(input);
        }

        public static implicit operator NumberD(double input)
        {
            return new NumberD(input);
        }

        public static implicit operator NumberD(float input)
        {
            return new NumberD(input);
        }

        public static implicit operator NumberD(long input)
        {
            return new NumberD(input);
        }

        public static implicit operator NumberD(ulong input)
        {
            return new NumberD(input);
        }

        public static implicit operator NumberD(int input)
        {
            return new NumberD(input);
        }

        public static implicit operator NumberD(uint input)
        {
            return new NumberD(input);
        }

        public static implicit operator NumberD(short input)
        {
            return new NumberD(input);
        }

        public static implicit operator NumberD(ushort input)
        {
            return new NumberD(input);
        }

        public static implicit operator NumberD(byte input)
        {
            return new NumberD(input);
        }

        public static implicit operator NumberD(sbyte input)
        {
            return new NumberD(input);
        }

        public static implicit operator NumberD(char input)
        {
            return new NumberD(input);
        }

        public static implicit operator NumberD(Number input)
        {
            return new NumberD(input);
        }

        public static implicit operator NumberD(NumberO input)
        {
            return new NumberD(input);
        }

        public static implicit operator NumberD(NumberP input)
        {
            return new NumberD(input);
        }

        public static NumberD operator +(NumberD first, NumberD second)
        {
            return Operations.PerformArithmeticOperation
            (
                first, second, ExistingOperations.Addition
            );
        }

        public static NumberD operator -(NumberD first, NumberD second)
        {
            return Operations.PerformArithmeticOperation
            (
                first, second, ExistingOperations.Subtraction
            );
        }

        public static NumberD operator *(NumberD first, NumberD second)
        {
            return Operations.PerformArithmeticOperation
            (
                first, second, ExistingOperations.Multiplication
            );
        }

        public static NumberD operator /(NumberD first, NumberD second)
        {
            return Operations.PerformArithmeticOperation
            (
                first, second, ExistingOperations.Division
            );
        }

        public static decimal operator %(NumberD first, NumberD second)
        {
            return first.Value % second.Value;
        }

        public static bool operator >(NumberD first, NumberD second)
        {
            return Operations.PerformOtherOperation
            (
                first, second, ExistingOperations.Greater
            )
            .Value == 1m;
        }

        public static bool operator >=(NumberD first, NumberD second)
        {
            return Operations.PerformOtherOperation
            (
                first, second, ExistingOperations.GreaterOrEqual
            )
            .Value == 1m;
        }

        public static bool operator <(NumberD first, NumberD second)
        {
            return Operations.PerformOtherOperation
            (
                first, second, ExistingOperations.Smaller
            )
            .Value == 1m;
        }

        public static bool operator <=(NumberD first, NumberD second)
        {
            return Operations.PerformOtherOperation
            (
                first, second, ExistingOperations.SmallerOrEqual
            )
            .Value == 1m;
        }

        public static bool operator ==(NumberD first, NumberD second)
        {
            return Operations.NoNullEquals(first, second);
        }

        public static bool operator !=(NumberD first, NumberD second)
        {
            return !Operations.NoNullEquals(first, second);
        }

        public bool Equals(NumberD other)
        {
            return
            (
                object.Equals(other, null) ? false :
                Operations.NumberXsAreEqual(this, other)
            );
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as NumberD);
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}
