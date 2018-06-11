using System;

namespace FlexibleParser
{
    public partial class Math2
    {
        private static Number RoundExactInternal(Number number, int digits, RoundType type, RoundSeparator separator)
        {
            ErrorTypesNumber error = ErrorInfoNumber.GetPowTruncateError(number);
            if (error != ErrorTypesNumber.None) return new Number(error);

            if (digits <= 0) digits = 0;
            if (digits > Power10Decimal.Length - 1)
            {
                digits = Power10Decimal.Length - 1;
            }

            Number outNumber = new Number(number);

            outNumber.Value = Math.Sign(number.Value) * RoundInternal
            (
                Math.Abs(outNumber.Value), digits, type, separator
            );

            return outNumber;
        }

        private static decimal RoundInternal(decimal d, int digits, RoundType type, RoundSeparator separator)
        {
            return
            (
                digits == 0 || separator == RoundSeparator.BeforeDecimalSeparator ?
                RoundInternalBefore(d, digits, type) : 
                RoundInternalAfter(d, digits, type)
            );
        }

        private static decimal RoundInternalBefore(decimal d, int digits, RoundType type)
        {
            if (digits == 0) return PerformRound(d, 0, type, Math.Floor(d));

            int length = GetIntegerLength(d);

            return
            (
                digits > length ? d : RoundExactInternal
                (
                    d, length - digits, type
                )
            );
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

        private static decimal RoundInternalAfterZeroes(decimal d, int digits, RoundType type, decimal d2, int zeroCount)
        {
            if (digits < zeroCount)
            {
                //Cases like 0.001 with 1 digit or 0.0001 with 2 digits can reach this point.
                //On the other hand, something like 0.001 with 2 digits requires further analysis.
                return Math.Floor(d) +
                (
                    type != RoundType.AlwaysAwayFromZero ? 0m :
                    1m / Power10Decimal[digits]
                );
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
            if (type == RoundType.AlwaysToZero || type == RoundType.AlwaysAwayFromZero)
            {
                return rounded + 
                (
                    type == RoundType.AlwaysAwayFromZero ? 1m : 0m
                );
            }
            else
            {
                int lastDigit = (int)(d / Power10Decimal[remDigits] % 10m);

                if (type == RoundType.AlwaysToEven)
                {
                    if (lastDigit % 2 != 0) rounded += 1m;
                }
                else
                {
                    int greaterEqual = MidPointGreaterEqual(d, remDigits, rounded);

                    if (greaterEqual == 1) rounded += 1m;
                    else if (greaterEqual == 0)
                    {
                        if (type == RoundType.MidpointAwayFromZero || (type == RoundType.MidpointToEven && lastDigit % 2 != 0))
                        {
                            rounded += 1m;
                        }
                    }
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
    }
}
