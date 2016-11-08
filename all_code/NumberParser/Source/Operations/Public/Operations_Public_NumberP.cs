using System;
using System.Collections.Generic;
using System.Globalization;

namespace FlexibleParser
{
    public partial class NumberP
    {
        ///<summary>
        ///<para>Outputs an error or "Value*10^BaseTenExponent (OriginalString)" together with all the Config information via invoking its ToString() method (BaseTenExponent different than zero).</para>
        ///</summary>
        public override string ToString()
        {
            return ToString(CultureInfo.InvariantCulture);
        }

        ///<summary>
        ///<para>Outputs an error or "Value*10^BaseTenExponent (OriginalString)" together with all the Config information via invoking its ToString() method (BaseTenExponent different than zero).</para>
        ///</summary>
        ///<param name="culture">Culture.</param>
        public string ToString(CultureInfo culture)
        {
            if (Error != ErrorTypesNumber.None) return "Error. " + Error.ToString();
            if (culture == null) culture = CultureInfo.InvariantCulture;

            NumberD numberD = Operations.PassBaseTenToValue((NumberD)this, true);
            return Operations.PrintNumberXInfo
            (
                numberD.Value, numberD.BaseTenExponent, null, culture
            )
            + " (" + OriginalString + ")" + Environment.NewLine + Config.ToString();
        }

        public static implicit operator NumberP(string input)
        {
            return new NumberP(input);
        }

        public static implicit operator NumberP(Number input)
        {
            return new NumberP(input);
        }

        public static implicit operator NumberP(NumberD input)
        {
            return new NumberP(input);
        }

        public static implicit operator NumberP(NumberO input)
        {
            return new NumberP(input);
        }

        public static NumberP operator +(NumberP first, NumberP second)
        {
            return Operations.PerformArithmeticOperation
            (
                first, second, ExistingOperations.Addition
            );
        }

        public static NumberP operator -(NumberP first, NumberP second)
        {
            return Operations.PerformArithmeticOperation
            (
                first, second, ExistingOperations.Subtraction
            );
        }

        public static NumberP operator *(NumberP first, NumberP second)
        {
            return Operations.PerformArithmeticOperation
            (
                first, second, ExistingOperations.Multiplication
            );
        }

        public static NumberP operator /(NumberP first, NumberP second)
        {
            return Operations.PerformArithmeticOperation
            (
                first, second, ExistingOperations.Division
            );
        }

        public static decimal operator %(NumberP first, NumberP second)
        {
            return first.Value % second.Value;
        }

        public static bool operator >(NumberP first, NumberP second)
        {
            return Operations.PerformOtherOperation
            (
                first, second, ExistingOperations.Greater
            )
            .Value == 1m;
        }

        public static bool operator >=(NumberP first, NumberP second)
        {
            return Operations.PerformOtherOperation
            (
                first, second, ExistingOperations.GreaterOrEqual
            )
            .Value == 1m;
        }

        public static bool operator <(NumberP first, NumberP second)
        {
            return Operations.PerformOtherOperation
            (
                first, second, ExistingOperations.Smaller
            )
            .Value == 1m;
        }

        public static bool operator <=(NumberP first, NumberP second)
        {
            return Operations.PerformOtherOperation
            (
                first, second, ExistingOperations.SmallerOrEqual
            )
            .Value == 1m;
        }

        public static bool operator ==(NumberP first, NumberP second)
        {
            return Operations.NoNullEquals(first, second);
        }

        public static bool operator !=(NumberP first, NumberP second)
        {
            return !Operations.NoNullEquals(first, second);
        }

        public bool Equals(NumberP other)
        {
            return
            (
                object.Equals(other, null) ? false :
                Operations.NumberXsAreEqual(this, other)
            );
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as NumberP);
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}
