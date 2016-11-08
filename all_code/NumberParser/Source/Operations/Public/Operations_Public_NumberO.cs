using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

namespace FlexibleParser
{
    public partial class NumberO
    {
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

        public static implicit operator NumberO(decimal input)
        {
            return new NumberO(input);
        }

        public static implicit operator NumberO(double input)
        {
            return new NumberO(input);
        }

        public static implicit operator NumberO(float input)
        {
            return new NumberO(input);
        }

        public static implicit operator NumberO(long input)
        {
            return new NumberO(input);
        }

        public static implicit operator NumberO(ulong input)
        {
            return new NumberO(input);
        }

        public static implicit operator NumberO(int input)
        {
            return new NumberO(input);
        }

        public static implicit operator NumberO(uint input)
        {
            return new NumberO(input);
        }

        public static implicit operator NumberO(short input)
        {
            return new NumberO(input);
        }

        public static implicit operator NumberO(ushort input)
        {
            return new NumberO(input);
        }

        public static implicit operator NumberO(byte input)
        {
            return new NumberO(input);
        }

        public static implicit operator NumberO(sbyte input)
        {
            return new NumberO(input);
        }

        public static implicit operator NumberO(char input)
        {
            return new NumberO(input);
        }

        public static implicit operator NumberO(Number input)
        {
            return new NumberO(input);
        }

        public static implicit operator NumberO(NumberD input)
        {
            return new NumberO(input);
        }

        public static implicit operator NumberO(NumberP input)
        {
            return new NumberO(input);
        }

        public static NumberO operator +(NumberO first, NumberO second)
        {
            return Operations.PerformArithmeticOperation
            (
                first, second, ExistingOperations.Addition
            );
        }

        public static NumberO operator -(NumberO first, NumberO second)
        {
            return Operations.PerformArithmeticOperation
            (
                first, second, ExistingOperations.Subtraction
            );
        }

        public static NumberO operator *(NumberO first, NumberO second)
        {
            return Operations.PerformArithmeticOperation
            (
                first, second, ExistingOperations.Multiplication
            );
        }

        public static NumberO operator /(NumberO first, NumberO second)
        {
            return Operations.PerformArithmeticOperation
            (
                first, second, ExistingOperations.Division
            );
        }

        public static decimal operator %(NumberO first, NumberO second)
        {
            return first.Value % second.Value;
        }

        public static bool operator >(NumberO first, NumberO second)
        {
            return Operations.PerformOtherOperation
            (
                first, second, ExistingOperations.Greater
            )
            .Value == 1m;
        }

        public static bool operator >=(NumberO first, NumberO second)
        {
            return Operations.PerformOtherOperation
            (
                first, second, ExistingOperations.GreaterOrEqual
            )
            .Value == 1m;
        }

        public static bool operator <(NumberO first, NumberO second)
        {
            return Operations.PerformOtherOperation
            (
                first, second, ExistingOperations.Smaller
            )
            .Value == 1m;
        }

        public static bool operator <=(NumberO first, NumberO second)
        {
            return Operations.PerformOtherOperation
            (
                first, second, ExistingOperations.SmallerOrEqual
            )
            .Value == 1m;
        }

        public static bool operator ==(NumberO first, NumberO second)
        {
            return Operations.NoNullEquals(first, second);
        }

        public static bool operator !=(NumberO first, NumberO second)
        {
            return !Operations.NoNullEquals(first, second);
        }

        public bool Equals(NumberO other)
        {
            return
            (
                object.Equals(other, null) ? false :
                Operations.NumberXsAreEqual(this, other)
            );
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as NumberO);
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}
