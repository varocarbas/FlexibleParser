using System;
using System.Collections.Generic;

namespace FlexibleParser
{
    public partial class UnitP
    {
        private enum RoundType { MidpointAwayFromZero, MidpointToZero };

        //The current implementation only needs the decimal type and that's why all this part
        //of the algorithm is declared as decimal. 
        //Nevertheless, the original version of this code (i.e., an improved version of the one 
        //I submitted for the CoreFX issue https://github.com/dotnet/corefx/issues/6308) was built
        //on dynamic. Thus, the current structure can be easily adapted to deal with as many types
        //as required.
        private static decimal[] Power10Decimal = PopulateRoundPower10Array();

        internal static decimal[] PopulateRoundPower10Array()
        {
            return new decimal[]
            { 
                1e0m, 1e1m, 1e2m, 1e3m, 1e4m, 1e5m, 1e6m, 1e7m, 
                1e8m, 1e9m, 1e10m, 1e11m, 1e12m, 1e13m, 1e14m, 
                1e15m, 1e16m, 1e17m, 1e18m, 1e19m, 1e20m, 1e21m,
                1e22m, 1e23m, 1e24m, 1e25m, 1e26m, 1e27m, 1e28m
            };
        }
        
        //This function (+ all the related code) is a version of NumberParser's Math2.RoundExact
        //(https://github.com/varocarbas/FlexibleParser/blob/master/all_code/NumberParser/Source/Math2/Private/New/Math2_Private_New_RoundTruncate.cs).
        //Note that Math.Round cannot deal with the rounding-down expectations of ImproveFinalValue.
        private static decimal RoundExact(decimal d, int digits, RoundType type)
        {
            return
            (
                d == 0m ? 0m : (d > 0m ? 1m : -1m) * 
                RoundInternalAfter(Math.Abs(d), digits, type)
            );
        }

        private static decimal RoundInternalAfter(decimal d, int digits, RoundType type)
        {
            decimal d2 = d - Math.Floor(d);
            int zeroCount = GetHeadingDecimalZeroCount(d2);

            return
            (
                zeroCount == 0 ? RoundInternalAfterNoZeroes(d, d2, digits, type) :
                RoundInternalAfterZeroes(d, digits, type, d2, zeroCount)
            );
        }

        private static decimal RoundInternalAfterZeroes(decimal d, int digits, RoundType type, decimal d2, int zeroCount)
        {
            if (digits < zeroCount)
            {
                //Cases like 0.001 with 1 digit or 0.0001 with 2 digits can reach this point.
                //On the other hand, something like 0.001 with 2 digits requires further analysis.
                return Math.Floor(d);
            }

            //d3 represent the decimal part after all the heading zeroes.
            decimal d3 = d2 * Power10Decimal[zeroCount];
            d3 = DecimalPartToInteger(d3 - Math.Floor(d3), 0, true);
            int length3 = GetIntegerLength(d3);

            decimal headingBit = 0;
            digits -= zeroCount;
            if (digits == 0)
            {
                //In a situation like 0.005 with 2 digits, the number to be analysed would be
                //05 what cannot be (i.e., treated as 5, something different). That's why, in 
                //these cases, adding a heading number is required.
                headingBit = 2; //2 avoids the ...ToEven types to be misinterpreted.
                d3 = headingBit * Power10Decimal[length3] + d3;
                digits = 0;
            }

            decimal output =
            (
                RoundExactInternal(d3, length3 - digits, type)
                / Power10Decimal[length3]
            )
            - headingBit;

            return Math.Floor(d) +
            (
                output == 0m ? 0m :
                output /= Power10Decimal[zeroCount]
            );
        }

        //This method expects the input value to always be positive.
        private static int GetIntegerLength(decimal d)
        {
            if (d == 0) return 0;

            for (int i = 0; i < Power10Decimal.Length - 1; i++)
            {
                if (d < Power10Decimal[i + 1]) return i + 1;
            }

            return Power10Decimal.Length;
        }
     
        private static decimal RoundInternalAfterNoZeroes(decimal d, decimal d2, int digits, RoundType type)
        {
            d2 = DecimalPartToInteger(d2, digits);
            int length2 = GetIntegerLength(d2);

            return
            (
                digits >= length2 ? d : Math.Floor(d) +
                (
                    RoundExactInternal(d2, length2 - digits, type)
                    / Power10Decimal[length2]
                )
            );
        }

        private static decimal RoundExactInternal(decimal d, int remDigits, RoundType type)
        {
            decimal rounded = PerformRound
            (
                d, remDigits, type,
                Math.Floor(d / Power10Decimal[remDigits])
            );

            decimal rounded2 = rounded * Power10Decimal[remDigits];
            return
            (
                rounded2 > rounded ? rounded2 :
                d //A numeric overflow occurred.
            );
        }

        private static decimal PerformRound(decimal d, int remDigits, RoundType type, decimal rounded)
        {
            int greaterEqual = MidPointGreaterEqual(d, remDigits, rounded);

            if (greaterEqual == 1) rounded += 1m;
            else if (greaterEqual == 0)
            {
                if (type == RoundType.MidpointAwayFromZero)
                {
                    rounded += 1m;
                }
            }

            return rounded;
        }

        private static int MidPointGreaterEqual(decimal d, int remDigits, decimal rounded)
        {
            return
            (
                remDigits > 0 ?
                //There are some additional digits after the last rounded one. It can be before or after. 
                //Example: 12345.6789 being rounded to 12000 or to 12345.68.
                MidPointGreaterEqualRem(d, remDigits, rounded) :
                //No additional digits after the last rounded one and the decimal digits have to be considered.
                //Only before is relevant here. Example: 12345.6789 rounded to 12345 and considering .6789.
                MidPointGreaterEqualNoRem(d, rounded)
            );
        }

        private static int MidPointGreaterEqualNoRem(decimal d, decimal rounded)
        {
            decimal d2 = d - rounded;
            d2 = DecimalPartToInteger(d2 - Math.Floor(d2));
            int length2 = GetIntegerLength(d2);
            if (length2 < 1) return 0;

            int nextDigit = (int)(d2 / Power10Decimal[length2 - 1] % 10);
            if (nextDigit != 5) return (nextDigit < 5 ? -1 : 1);

            while (Math.Floor(d2) != d2 && d2 < Power10Decimal[Power10Decimal.Length - 1])
            {
                d2 *= 10;
                length2++;
            }

            int count = length2 - 1;
            while (count > 0)
            {
                count--;
                if ((int)(d2 / Power10Decimal[count] % 10) != 0)
                {
                    //Just one digit different than zero is enough to conclude that is greater.
                    return 1;
                }
            }

            return 0;
        }

        private static int MidPointGreaterEqualRem(decimal d, int remDigits, decimal rounded)
        {
            int nextDigit = (int)(d / Power10Decimal[remDigits - 1] % 10);
            if (nextDigit != 5) return (nextDigit < 5 ? -1 : 1);

            decimal middle = nextDigit * Math.Floor(Power10Decimal[remDigits - 1]);
            return
            (
                d - rounded * Math.Floor(Power10Decimal[remDigits]) == middle ? 0 : 1
            );
        }

        //This method expects the input value to always be positive.
        private static decimal DecimalPartToInteger(decimal d2, int digits = 0, bool untilEnd = false)
        {
            if (digits + 1 >= Power10Decimal.Length - 1)
            {
                d2 *= Power10Decimal[Power10Decimal.Length - 1];
            }
            else
            {
                d2 *= Power10Decimal[digits + 1];
                decimal lastDigit = Math.Floor(d2 / Power10Decimal[0] % 10);
                while (d2 < Power10Decimal[Power10Decimal.Length - 3] && (untilEnd || (lastDigit > 0 && lastDigit <= 5m)))
                {
                    d2 *= 10;
                    lastDigit = Math.Floor(d2 / Power10Decimal[0] % 10);
                }
            }

            return d2;
        }

        //This method depends upon the decimal-type native precision/Math.Floor and, consequently,
        //some extreme cases might be misunderstood. Example: 100000000000000000.00000000000001m
        //outputting zero because of being automatically converted into 100000000000000000m.
        //This method expects the input value to always be positive.
        private static int GetHeadingDecimalZeroCount(decimal d)
        {
            decimal d2 = (d > 1m ? d - Math.Floor(d) : d);
            if (d2 == 0) return 0;

            int zeroCount = 0;
            while (d2 <= decimal.MaxValue / 10m)
            {
                d2 *= 10m;
                if (Math.Floor(d2 / Power10Decimal[0] % 10) != 0m)
                {
                    return zeroCount;
                }
                zeroCount++;
            }

            return zeroCount;
        }
    }
}
