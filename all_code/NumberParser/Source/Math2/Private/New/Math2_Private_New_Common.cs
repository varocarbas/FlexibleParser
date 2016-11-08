using System;
using System.Collections.Generic;

namespace FlexibleParser
{
    public partial class Math2
    {
        //The current implementation only needs the decimal type and that's why all this part
        //of the algorithm is declared as decimal. 
        //Nevertheless, the original version of this code (i.e., an improved version of the one 
        //I submitted for the CoreFX issue https://github.com/dotnet/corefx/issues/6308) was built
        //on dynamic. Thus, the current structure can be easily adapted to deal with as many types
        //as required.
        internal static decimal[] Power10Decimal = PopulateRoundPower10Array();

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

        //This method depends upon the decimal-type native precision/Math.Floor and, consequently,
        //some extreme cases might be misunderstood. Example: 100000000000000000.00000000000001m
        //outputting zero because of being automatically converted into 100000000000000000m.
        //This method expects the input value to always be positive.
        private static int GetHeadingDecimalZeroCount(decimal d)
        {
            decimal d2 = (d > 1m ? d - Math.Floor(d) : d);
            if (d2 == 0) return 0;

            int zeroCount = 0;
            while (d2 <= Basic.AllNumberMinMaxPositives[typeof(decimal)][1] / 10m)
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
    }
}
