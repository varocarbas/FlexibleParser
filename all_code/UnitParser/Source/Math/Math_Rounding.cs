using System;
using System.Collections.Generic;

namespace FlexibleParser
{
    public partial class UnitP
    {
        public enum RoundType { MidpointAwayFromZero, MidpointToZero };

        private static decimal[] roundPower10Decimal = new decimal[] 
    	{ 
	     1m, 1E1m, 1E2m, 1E3m, 1E4m, 1E5m, 1E6m, 1E7m, 1E8m, 1E9m, 1E10m, 1E11m, 1E12m,
             1E13m, 1E14m, 1E15m, 1E16m, 1E17m, 1E18m, 1E19m, 1E20m, 1E21m, 1E22m, 1E23m, 1E24m,
             1E25m, 1E26m, 1E27m, 1E28m
	};

        //This function (+ all the related code) is a version of my CoreFX RoundExact proposal to improve Math.Round (https://github.com/dotnet/corefx/issues/6308).
        //Note that the default Math.Round cannot meet the expectations of the method ImproveFinalValue (mainly when rounding down).
        private static decimal RoundExact(decimal d, int digits, RoundType type)
        {
            if (d == 0m) return 0m;

            decimal sign = (d > 0m ? 1m : -1m);

            return sign * RoundDecInternalAfter(Math.Abs(d), digits, type);
        }

        private static decimal RoundDecInternalAfter(decimal d, int digits, RoundType type)
        {
            decimal d2 = DecimalPartToInteger(d, digits);
            int length2 = GetIntegerLength(d2);

            return 
            (
                length2 - digits < 1 ? d : Math.Floor(d) + 
                (
                    RoundDecInternalBefore(d2, digits, type) / roundPower10Decimal[length2]
                )
            );
        }

        private static decimal DecimalPartToInteger(decimal d, int digits)
        {
            decimal d2 = d - Math.Floor(d);

            if (digits + 1 >= roundPower10Decimal.Length - 1)
            {
                d2 = d2 * roundPower10Decimal[roundPower10Decimal.Length - 1];
            }
            else
            {
                d2 = d2 * roundPower10Decimal[digits + 1];
                decimal lastDigit = Math.Floor(d2 / roundPower10Decimal[0] % 10);
                while (d2 < roundPower10Decimal[roundPower10Decimal.Length - 2] && lastDigit <= 5m && lastDigit > 0)
                {
                    d2 = d2 * 10;
                    lastDigit = Math.Floor(d2 / roundPower10Decimal[0] % 10);
                }
            }

            return d2;
        }

        //The argument value is always positive.
        private static int GetIntegerLength(decimal value)
        {
            for (int i = 0; i < roundPower10Decimal.Length - 1; i++)
            {
                if (value < roundPower10Decimal[i + 1])
                {
                    return i + 1;
                }
            }

            return roundPower10Decimal.Length + 1;
        }

        private static decimal RoundDecInternalBefore(decimal d, int digits, RoundType type)
        {
            int remDigits = GetIntegerLength(d) - digits;

            return 
            (
                remDigits < 1 ? d : 
                RoundIntegerInternal(d, type, remDigits)
            );
        }

        private static decimal RoundIntegerInternal(decimal d, RoundType type, int remDigits)
        {
            decimal rounded = RoundIntegerTypes
            (
                d, type, remDigits, 
                Math.Floor(d / roundPower10Decimal[remDigits])
            );
            
            return
            (
                //This only way to meet this condition is with an overflow.
                roundPower10Decimal[remDigits] * rounded < rounded ? d :
                rounded * roundPower10Decimal[remDigits]
            );
        }

        private static decimal RoundIntegerTypes(decimal d, RoundType type, int remDigits, decimal rounded)
        {
            if (remDigits >= 1)
            {
                int greaterEqual = MidPointGreaterEqual
                (
                    d, remDigits, rounded, 
                    Math.Floor(d / roundPower10Decimal[remDigits - 1] % 10)
                );

                if (greaterEqual == 1) rounded = rounded + 1;
                else if (greaterEqual == 0)
                {
                    if (type == RoundType.MidpointAwayFromZero)
                    {
                        rounded = rounded + 1;
                    }
                }
            }

            return rounded;
        }

        private static int MidPointGreaterEqual(decimal d, int remDigits, decimal rounded, decimal nextDigit)
        {
            int greaterEqual = (nextDigit < 5 ? -1 : 1);

            if (nextDigit == 5)
            {
                decimal middle = nextDigit * roundPower10Decimal[remDigits - 1];
                if (d - (rounded * roundPower10Decimal[remDigits]) == middle)
                {
                    greaterEqual = 0;
                }
            }

            return greaterEqual;
        }
    }
}
