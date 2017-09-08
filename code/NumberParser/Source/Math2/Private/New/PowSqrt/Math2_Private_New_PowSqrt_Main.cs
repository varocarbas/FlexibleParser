using System;
using System.Collections.Generic;

namespace FlexibleParser
{
    public partial class Math2
    {
        internal static Number PowSqrtInternal(Number number, decimal exponent, bool showUser = true)
        {
            ErrorTypesNumber error = ErrorInfoNumber.GetPowTruncateError(number);
            if (error != ErrorTypesNumber.None) return new Number(error);
            
            bool isSqrt = (exponent == 0.5m);
            bool expIsInteger = (exponent == Math.Floor(exponent));

            Number tempVar = PowSqrtPreCheck(number, exponent, expIsInteger);
            if (tempVar != null) return tempVar;

            Number outNumber = Operations.PassBaseTenToValue(number);

            //The BaseTenExponent is calculated independently in order to avoid operations with too big values.
            //Note that this BaseTenExponent represents all the additional information on top of the maximum
            //decimal range. During the calculations, the outNumber variable will be normalised and, consequently,
            //a new BaseTenExponent will be used.
            //In any case, the first calculation step involving the outNumber information (i.e., first guess for the
            //Newton-Raphson method to determine the corresponding root) is expecting a non-normalised variable.
            int baseTenExp = outNumber.BaseTenExponent; 
            outNumber.BaseTenExponent = 0;

            //Better simplifying the intermediate calculations as much as possible by ignoring all the signs.
            int sign =
            (
                exponent % 2 == 0 ? 1 : Math.Sign(outNumber.Value)
            );
            outNumber.Value = Math.Abs(outNumber.Value);

            int sign2 = Math.Sign(exponent);
            exponent = Math.Abs(exponent);

            if (isSqrt) outNumber = sign * GetBase2Root(outNumber);
            else
            {
                outNumber = PowInternalPositive
                (
                    outNumber, exponent, expIsInteger
                );
                outNumber.Value = sign * outNumber.Value;

                if (sign2 == -1)
                {
                    outNumber = Operations.DivideInternal(new Number(1m), outNumber);
                }
            }

            if (baseTenExp != 0)
            {
                outNumber = Operations.MultiplyInternal
                (
                    outNumber, RaiseBaseTenToExponent(baseTenExp, exponent)
                );
            }

            return Operations.PassBaseTenToValue(outNumber, showUser);
        }

        private static Number PowSqrtPreCheck(Number number, decimal exponent, bool expIsInteger)
        {
            if (number.Value < 0m && !expIsInteger)
            {
                return new Number(ErrorTypesNumber.InvalidOperation);
            }
            if (number == 1m || number == 0 || exponent == 1m) return new Number(number);
            if (exponent == 0m) return new Number(1m);
            if (number == -1m) return new Number(exponent % 2 == 0 ? 1m : -1m);

            return null;
        }

        private static Number RaiseBaseTenToExponent(int existingBaseTen, decimal exponent)
        {
            decimal tempBaseTen = existingBaseTen * exponent;
            if ((existingBaseTen < 0 && tempBaseTen < int.MinValue - existingBaseTen) || (existingBaseTen > 0 && tempBaseTen > int.MaxValue - existingBaseTen))
            {
                return new Number(ErrorTypesNumber.NumericOverflow);
            }

            Number outNumber = new Number(1m, (int)tempBaseTen);
            return
            (
                outNumber.BaseTenExponent == tempBaseTen ? outNumber :
                Operations.MultiplyInternal
                (
                    outNumber, PowFractionalPositive(10m, tempBaseTen - outNumber.BaseTenExponent)
                )
            );
        }

        private static Number GetBase2Root(Number number)
        {
            return GetNRoot
            (
                number.Value, 2m, GetBase2IniGuess(number.Value)
            );
        }

        private static Number PowInternalPositive(Number number, decimal exponent, bool expIsInteger)
        {
            return
            (
                expIsInteger ? PowIntegerPositive(number, exponent) :
                PowFractionalPositive(number, exponent)
            );
        }

        private static Number PowFractionalPositive(Number number, decimal exponent)
        {
            decimal[] fraction = GetFraction(exponent);

            return PowIntegerPositive
            (
                GetBase10Root(number, fraction[1]), fraction[0]
            );
        }

        private static decimal[] GetFraction(decimal exponent)
        {
            decimal[] fraction = new decimal[] { exponent, 1m };
            decimal exponent2 = Math.Floor(exponent);
            if (exponent2 == exponent) return fraction;
            exponent2 = exponent - exponent2;

            while (fraction[1] <= Basic.AllNumberMinMaxPositives[typeof(decimal)][1] / 10m && Math.Floor(fraction[0]) != fraction[0])
            {
                fraction[0] *= 10m;
                fraction[1] *= 10m;

                if (fraction[1] >= 1e25m)
                {
                    //Although the decimal type precision can deal with numbers up to 1e28m, fraction[1] being 
                    //1e25 implies a fraction[0] of 1e26, what provokes that the initial guess in the subsequent
                    //root calculation to have around 25 relevant digits, which cannot be reduced via BaseTenExponent
                    //(i.e., it has the form 1.00000...1). The subsequent calculations to find the right result will 
                    //be performed within the range [ini_guess, ini_guess+3 more digits]; for example, if the first 
                    //guess is 1.001, the range would be [1.001, 1.000001]). This situation is likely to provoke 
                    //calculations around the maximum decimal precision, a potential-infinite-loop (or exited without
                    //having found the right result, what is equally bad) scenario. For example, by assuming an initial
                    //guess of 1.001 and a maximum decimal precision of 1.000001, when the calculations reach 1.000001 
                    //(what is very likely to occur), the corresponding value would be (mis)understood as 1.0 and the
                    //correct result would never be found.
                    fraction[0] = Math.Truncate(fraction[0]);
                    return fraction;
                }
            }

            return fraction;
        }

        internal static Number PowIntegerPositive(Number number, decimal exponent)
        {
            if (exponent == 0m) return new Number(1m);
            if (exponent == 1m) return new Number(number);

            Number x = new Number(number);
            Number y = new Number(1m);

            while (true)
            {
                if (exponent % 2m == 0m)
                {
                    x = Operations.MultiplyInternal(x, x);
                    exponent /= 2m;
                }
                else
                {
                    y = Operations.MultiplyInternal(y, x);
                    x = Operations.MultiplyInternal(x, x);
                    exponent = (exponent - 1m) / 2m;
                }

                if (x.Error != ErrorTypesNumber.None || y.Error != ErrorTypesNumber.None)
                {
                    //There is only one type of error which might reach this point: the calculations have
                    //provoked BaseTenExponent to grow beyond the int type range.
                    return new Number(ErrorTypesNumber.NumericOverflow);
                }

                //This algorithm expects to deal with an integer exponent. Note that it isn't possible to 
                //rely on an integer type because their ranges are much smaller than the decimal one.
                exponent = Math.Floor(exponent); 
                if (exponent <= 1m)
                {
                    return Operations.MultiplyInternal(y, x);
                }
            }
        }

        private static Number GetBase10Root(Number number, decimal n)
        {
            return GetNRoot
            (
                number.Value, n, GetBase10IniGuess(number.Value, n)
            );
        }

        //Optimised version of the Newton-Raphson method to get the root of f(x) = x^n - value.
        //The input value is expected to be always positive.
        private static Number GetNRoot(decimal value, decimal n, decimal iniGuess)
        {
            Number nN = new Number(n);
            decimal n_1 = n - 1;
            Number n_1N = new Number(n_1);
            Number valueN = new Number(value);
            Number[] fx = new Number[] 
            { 
                new Number(valueN), 
                new Number(iniGuess) 
            };

            decimal gap = 0m;
            int lowCount = 0;
            while (true)
            {
                fx[0] = new Number(fx[1]);

                //fx[1] = ((n_1N * fx[0]) + (valueN / PowIntegerPositive(fx[0], n_1))) / n;
                fx[1] = Operations.DivideInternal
                (
                    Operations.AddInternal
                    (
                        Operations.MultiplyInternal(n_1N, fx[0]),
                        Operations.DivideInternal(valueN, PowIntegerPositive(fx[0], n_1))
                    ),
                    nN
                );

                gap = Math.Abs(fx[0].Value - fx[1].Value);
                if (gap <= Basic.AllNumberMinMaxPositives[typeof(decimal)][0])
                {
                    return fx[1];
                }

                if (gap <= Basic.AllNumberMinMaxPositives[typeof(decimal)][0] * 5m)
                {
                    lowCount++;
                    if (lowCount > 3)
                    {
                        //To account for the not-too-likely scenario of getting stuck between two very similar values,
                        //despite having actually found the right answer. This problem usually happens with sqrt/base-2.
                        return fx[1];
                    }
                }
            }
        }
    }
}
