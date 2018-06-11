namespace FlexibleParser
{
    //All the methods in this file pursue the same goal: returning an accurate enough
    //first guess allowing the Newton-Raphson method (GetNRoot) to calculate the n root
    //under the given conditions. The calculating approach involves coming up with a
    //quick enough way to accurately model the underlying trend relating all the roots
    //for the same n (i.e., 2 or multiples of 10). All this is explained in detail in the
    //varocarbas.com Project 10: http://varocarbas.com/fractional_exponentiation/.
    public partial class Math2
    {
        private static decimal GetSimpleNGuess(decimal value, decimal n)
        {
            if (value >= 1 && value < 10m) return value / 2;

            bool small = false;
            decimal value2 = GetInverseValue(value);
            if (value2 != -1m)
            {
                small = true;
                value = value2;
            }

            decimal[] vals = GetBaseValsSpecificNGuess(n);
            decimal outVal = 1m;

            int length = GetIntegerLength(value) - 1;
            int exponent = length - 1;
            decimal ratio = (int)(value / Power10Decimal[length] % 10) / 10m;
            if (ratio == 0.1m) ratio = 1m;

            int max = vals.Length - 1;
            if (length - 1 > max)
            {
                int exponent2 = length / (max + 1) - 1;
                int rem2 = length % (max + 1);
                if (rem2 == 0)
                {
                    exponent = max;
                }
                else
                {
                    exponent = rem2 - 1;
                    exponent2++;
                }
                outVal *= Power10Decimal[exponent2];
            }

            outVal *=
            (
                ratio == 1m || exponent == max ? vals[exponent] :
                vals[exponent] + ratio * (vals[exponent + 1] - vals[exponent])
            );

            return
            (
                !small ? outVal : outVal / Power10Decimal[length]
            );
        }

        private static decimal[] GetBaseValsSpecificNGuess(decimal n)
        {
            return
            (
                n == 10 ? new decimal[]
                {
                    1.2589254117941672104239541064m, 1.5848931924611134852021013734m,
                    1.9952623149688796013524553967m, 2.5118864315095801110850320678m,
                    3.1622776601683793319988935444m, 3.9810717055349725077025230509m,
                    5.0118723362727228500155418689m, 6.3095734448019324943436013663m,
                    7.943282347242815020659182828m, 10m
                } 
                : new decimal[] //n == 2.
                { 
                    3.1622776601683793319988935446m, 10m 
                }
            );
        }

        private static decimal GetBase10IniGuess(decimal value, decimal n)
        {
            return
            (
                n == 10m ? GetSimpleNGuess(value, 10m) :
                GetBigNBase10Guess(value, n)
            );
        }

        private static decimal GetInverseValue(decimal value)
        {
            if (value >= 1m) return -1;

            int zeroes = GetHeadingDecimalZeroCount(value);
            int exp10 = zeroes + 1;
            value *= Power10Decimal[exp10];
            value *= Power10Decimal[exp10];

            return value;
        }

        private static decimal GetBigNBase10Guess(decimal value, decimal n)
        {
            return
            (
                value >= 100m && value <= 500m ?
                GetSmallValueBase10Guess(value, n) :
                GetGenericBase10Guess(value, n)
            );
        }

        private static decimal GetSmallValueBase10Guess(decimal value, decimal n)
        {
            decimal[] vals = new decimal[] 
            { 
                0.4605m, 0.5298m, 0.5704m, 0.5991m, 0.6215m 
            };

            int index = (int)(value / 100m);
            decimal ratio = (value - index * 100m) / 100m;
            index--;

            decimal outVal = vals[index];
            if (ratio != 1m && index < 4)
            {
                outVal = vals[index] + ratio * (vals[index + 1] - vals[index]);
            }

            return 1m + outVal / Power10Decimal[GetIntegerLength(n) - 2];
        }

        private static decimal GetGenericBase10Guess(decimal value, decimal n)
        {
            bool small = false;
            decimal value2 = GetInverseValue(value);
            if (value2 != -1m)
            {
                small = true;
                value = value2;
            }

            decimal outVal = 1m;
            int exponent = GetIntegerLength(n) - 1;

            if (value >= 500m)
            {
                decimal ratio = value / 500m;
                if (ratio >= 100m)
                {
                    exponent--;
                    if (ratio >= 1000m)
                    {
                        int length = GetIntegerLength(ratio);
                        //length -> addition
                        //4 -> 0.25
                        //5 -> 0.5
                        //6 -> 0.75
                        //7 -> 1
                        //8 -> 1.25
                        //...
                        decimal rem = length % 4;
                        outVal = length / 4 + 0.25m * (rem + 1m);
                    }
                }
                else if (ratio >= 10m) outVal *= 9m;
                else if (ratio >= 1m) outVal *= 5m;
            }

            return
            (
                !small ? 1m + outVal / Power10Decimal[exponent] :
                (1m - 1m / Power10Decimal[exponent]) + outVal / Power10Decimal[exponent + 1]
            );
        }

        private static decimal GetBase2IniGuess(decimal value)
        {
            return GetSimpleNGuess(value, 2m);
        }
    }
}
