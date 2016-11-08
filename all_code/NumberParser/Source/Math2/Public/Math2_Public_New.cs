using System;
using System.Collections.Generic;
using System.Globalization;

namespace FlexibleParser
{
    public partial class Math2
    {
        ///<summary>
        ///<para>Returns the dependent variable (y), as defined by y = A + B*x + C*x^2.</para>
        ///</summary>
        ///<param name="polynomial">Coefficients (A, B, C) defining the given polynomial fit.</param>
        ///<param name="x">Independent variable (x).</param>
        public static NumberD ApplyPolynomialFit(Polynomial polynomial, NumberD x)
        {
            return ApplyPolynomialFitInternal(polynomial, x);
        }

        ///<summary>
        ///<para>Determines (least squares) the best polynomial fit for the input x/y sets.</para>
        ///</summary>
        ///<param name="x">Array containing all the independent variable (x) values. It has to contain the same number of elements than y.</param>
        ///<param name="y">Array containing all the dependent variable (y) values. It has to contain the same number of elements than x.</param>
        public static Polynomial GetPolynomialFit(NumberD[] x, NumberD[] y)
        {
            return GetPolynomialFitInternal(x, y);
        }

        ///<summary>
        ///<para>Calculates the factorial of input value.</para>
        ///</summary>
        ///<param name="n">Input value. It has to be smaller than 100000.</param>
        public static NumberD Factorial(NumberD n)
        {
            return FactorialInternal(n);
        }

        ///<summary>
        ///<para>Calculates the square root of the input value.</para>
        ///<para>To know more about the underlying approach, visit http://varocarbas.com/fractional_exponentiation/.</para>
        ///</summary>
        ///<param name="n">Input value.</param>
        public static Number SqrtDecimal(Number n)
        {
            return PowSqrtInternal(n, 0.5m);
        }

        ///<summary>
        ///<para>Raises the input value to the exponent.</para>
        ///<para>To know more about the underlying approach, visit http://varocarbas.com/fractional_exponentiation/.</para>
        ///</summary>
        ///<param name="n">Input value.</param>
        ///<param name="exponent">Exponent.</param>
        public static Number PowDecimal(Number n, decimal exponent)
        {
            return PowSqrtInternal(n, exponent);
        }

        ///<summary>
        ///<para>Truncates the input value as instructed.</para>
        ///</summary>
        ///<param name="n">Input value.</param>
        public static Number TruncateExact(Number n)
        {
            return TruncateExact(n, 0);
        }

        ///<summary>
        ///<para>Truncates the input value as instructed.</para>
        ///</summary>
        ///<param name="n">Input value.</param>
        ///<param name="decimals">Number of decimal positions in the result.</param>
        public static Number TruncateExact(Number n, int decimals)
        {
            return RoundExact
            (
                n, decimals, RoundType.AlwaysToZero, RoundSeparator.AfterDecimalSeparator
            );
        }

        ///<summary>
        ///<para>Rounds the input value as instructed.</para>
        ///</summary>
        ///<param name="n">Input value.</param>
        public static Number RoundExact(Number n)
        {
            return RoundExact(n, 0);
        }

        ///<summary>
        ///<para>Rounds the input value as instructed.</para>
        ///</summary>
        ///<param name="n">Input value.</param>
        ///<param name="digits">Number of digits to be considered when rounding.</param>
        public static Number RoundExact(Number n, int digits)
        {
            return RoundExact
            (
                n, digits, RoundType.MidpointToEven, RoundSeparator.AfterDecimalSeparator
            );
        }

        ///<summary>
        ///<para>Rounds the input value as instructed.</para>
        ///</summary>
        ///<param name="n">Input value.</param>
        ///<param name="type">Type of rounding.</param>
        public static Number RoundExact(Number n, RoundType type)
        {
            return RoundExact
            (
                n, 0, type, RoundSeparator.AfterDecimalSeparator
            );
        }

        ///<summary>
        ///<para>Rounds the input value as instructed.</para>
        ///</summary>
        ///<param name="n">Input value.</param>
        ///<param name="digits">Number of digits to be considered.</param>
        ///<param name="type">Type of rounding.</param>
        public static Number RoundExact(Number n, int digits, RoundType type)
        {
            return RoundExact
            (
                n, digits, type, RoundSeparator.AfterDecimalSeparator
            );
        }

        ///<summary>
        ///<para>Rounds the input value as instructed.</para>
        ///</summary>
        ///<param name="n">Input value.</param>
        ///<param name="digits">Number of digits to be considered.</param>
        ///<param name="separator">Location of the digits to be rounded (before or after the decimal separator).</param>
        public static Number RoundExact(Number n, int digits, RoundSeparator separator)
        {
            return RoundExact
            (
                n, digits, RoundType.MidpointToEven, separator
            );
        }

        ///<summary>
        ///<para>Rounds the input value as instructed.</para>
        ///</summary>
        ///<param name="n">Input value.</param>
        ///<param name="digits">Number of digits to be considered.</param>
        ///<param name="type">Type of rounding.</param>
        ///<param name="separator">Location of the digits to be rounded (before or after the decimal separator).</param>
        public static Number RoundExact(Number n, int digits, RoundType type, RoundSeparator separator)
        {
            return RoundExactInternal
            (
                n, digits, type, separator
            );
        }

        ///<summary><para>Decimal version of Math.PI. First 28 decimal digits with no rounding.</para></summary>
        public const decimal PI = 3.1415926535897932384626433832m;
        ///<summary><para>Decimal version of Math.E. First 28 decimal digits with no rounding.</para></summary>
        public const decimal E = 2.7182818284590452353602874713m;
    }

    ///<summary>
    ///<para>Indicates the type of rounding, as defined by the way in which the last digit is being rounded.</para>
    ///</summary>
    public enum RoundType
    {
        ///<summary><para>When a number is halfway between two others, it is rounded to the number which is further from zero.</para></summary>
        MidpointAwayFromZero = 0,
        ///<summary><para>When a number is halfway between two others, it is rounded to the number which is even.</para></summary>
        MidpointToEven,
        ///<summary><para>When a number is halfway between two others, it is rounded to the number which is closer to zero.</para></summary>
        MidpointToZero,
        ///<summary><para>A number is always rounded to the number which is further from zero.</para></summary>           
        AlwaysToEven,
        ///<summary><para>A number is always rounded to the number which is closer to zero.</para></summary>     
        AlwaysAwayFromZero,
        ///<summary><para>A number is always rounded to the even number.</para></summary> 
        AlwaysToZero
    }

    ///<summary>
    ///<para>Indicates the location of the digits being rounded (i.e., before or after the decimal separator).</para>
    ///</summary>
    public enum RoundSeparator
    {
        ///<summary><para>Only the digits after the decimal separator are rounded.</para></summary>
        AfterDecimalSeparator = 0,
        ///<summary><para>Only the digits before the decimal separator are rounded. The digits after the decimal separator might also be analysed (e.g., midpoint determination).</para></summary>
        BeforeDecimalSeparator
    }

    ///<summary>
    ///<para>Stores the coefficients defining a second degree polynomial fit via y = A + B*x + C*x^2.</para>
    ///</summary>
    public class Polynomial
    {
        ///<summary>
        ///<para>Outputs the values of all the polynomial coefficients by invoking the NumberD ToString() method.</para>
        ///</summary>
        public override string ToString()
        {
            return ToString(CultureInfo.InvariantCulture);
        }
        ///<summary>
        ///<para>Outputs the values of all the polynomial coefficients by invoking the NumberD ToString() method.</para>
        ///</summary>
        ///<param name="culture">Culture.</param>
        public string ToString(CultureInfo culture)
        {
            if (Error != ErrorTypesNumber.None) return "Error. " + Error.ToString();
            if (culture == null) culture = CultureInfo.InvariantCulture;

            return
            (
                A == null || B == null || C == null ? "" :
                "A: " + A.ToString(culture) + Environment.NewLine +
                "B: " + B.ToString(culture) + Environment.NewLine +  
                "C: " + C.ToString(culture)
            );
        }

        ///<summary><para>Polynomial coefficient A, as defined by y = A + B*x + C*x^2.</para></summary>
        public NumberD A { get; set; }
        ///<summary><para>Polynomial coefficient B, as defined by y = A + B*x + C*x^2.</para></summary>
        public NumberD B { get; set; }
        ///<summary><para>Polynomial coefficient C, as defined by y = A + B*x + C*x^2.</para></summary>
        public NumberD C { get; set; }
        ///<summary><para>Error.</para></summary>
        public readonly ErrorTypesNumber Error;

        ///<summary><para>Initialises a new Polynomial instance.</para></summary>
        ///<param name="a">Coefficient A in y = A + B*x + C*x^2.</param>
        ///<param name="b">Coefficient B in y = A + B*x + C*x^2.</param>
        ///<param name="c">Coefficient C in y = A + B*x + C*x^2.</param>
        public Polynomial(NumberD a, NumberD b, NumberD c)
        {
            A = new NumberD(a);
            B = new NumberD(b);
            C = new NumberD(c);

            if (A.Error != ErrorTypesNumber.None || B.Error != ErrorTypesNumber.None || C.Error != ErrorTypesNumber.None)
            {
                A = null;
                B = null;
                C = null;
                Error = ErrorTypesNumber.InvalidInput;
            }
        }

        internal Polynomial(ErrorTypesNumber error) { Error = error; }

        public static bool operator ==(Polynomial first, Polynomial second)
        {
            return Operations.NoNullEquals(first, second);
        }

        public static bool operator !=(Polynomial first, Polynomial second)
        {
            return !Operations.NoNullEquals(first, second);
        }

        public bool Equals(Polynomial other)
        {
            return
            (
                object.Equals(other, null) ? false :
                Operations.PolynomialsAreEqual(this, other)
            );
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ParseConfig);
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}
