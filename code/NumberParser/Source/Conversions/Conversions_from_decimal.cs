using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    internal partial class Conversions
    {
        public static NumberD ConvertNumberToAny(Number number, Type target)
        {
            ErrorTypesNumber error = ErrorInfoNumber.GetConvertToAnyError(number, target);
            if (error != ErrorTypesNumber.None) return new NumberD(error);

            //The double/float ranges are bigger than the decimal one. 
            //That's why no further checks are needed.
            if (target == typeof(double))
            {
                return new NumberD(ConvertToDoubleInternal(number.Value));
            }
            else if (target == typeof(float))
            {
                return new NumberD(Convert.ToSingle(number.Value));
            }

            NumberD outNumber = new NumberD(number);
            decimal[] minMax = new decimal[]
            {
                ConvertToDecimalInternal(Basic.AllNumberMinMaxs[target][0]),
                ConvertToDecimalInternal(Basic.AllNumberMinMaxs[target][1])
            };

            if (outNumber.Value < minMax[0] || outNumber.Value > minMax[1])
            {
                outNumber = AdaptValueToTargetRange(outNumber, target, minMax);
            }

            outNumber.Value = ConvertAdaptedDecimalToTarget(outNumber.Value, target);

            return outNumber;
        }

        private static Number AdaptValueToTargetRange(Number outNumber, Type target, decimal[] minMax)
        {
            if (outNumber.Value == 0) return outNumber;

            decimal targetValue =
            (
                outNumber.Value < minMax[0] ? minMax[0] : minMax[1]
            );

            if (outNumber.Value < 0m && Basic.AllUnsignedTypes.Contains(target))
            {
                outNumber.Value = Math.Abs(outNumber.Value);

                if (outNumber.Value > minMax[1])
                {
                    //The resulting unsigned value (its absolute value, because the result of converting -123
                    //to a zero-based scale is 123) is outside the range of the given type and that's it needs
                    //further adaptation.
                    targetValue = minMax[1];
                }
                else return outNumber;
            }

            return ModifyValueToFitType(outNumber, target, targetValue);
        }

        private static Number ModifyValueToFitType(Number number, Type target, decimal targetValue)
        {
            decimal sign = 1m;
            if (number.Value < 0)
            {
                sign = -1m;
                number.Value *= sign;
            }

            if (!Basic.AllDecimalTypes.Contains(target))
            {
                number.Value = Math.Round(number.Value, MidpointRounding.AwayFromZero);
            }

            targetValue = Math.Abs(targetValue);
            bool increase = (number.Value < targetValue);

            while (true)
            {
                if (number.Value == targetValue || (increase && number.Value > targetValue) || (!increase && number.Value < targetValue))
                {
                    number.Value *= sign;

                    return number;
                }

                if (increase)
                {
                    number.Value *= 10;
                    number.BaseTenExponent -= 1;
                }
                else
                {
                    number.Value /= 10;
                    number.BaseTenExponent += 1;
                }
            }
        }

        //This method expects the input decimal value to lie within the MinMax value of the given type,
        //as defined in AllNumberMinMaxs.
        public static dynamic ConvertAdaptedDecimalToTarget(decimal value, Type type)
        {
            dynamic outValue = value;

            if (type == typeof(double))
            {
                outValue = Convert.ToDouble(value);
            }
            else if (type == typeof(float))
            {
                outValue = Convert.ToSingle(value);
            }
            else if (type == typeof(long))
            {
                outValue = Convert.ToInt64(value);
            }
            else if (type == typeof(ulong))
            {
                outValue = Convert.ToUInt64(value);
            }
            else if (type == typeof(int))
            {
                outValue = Convert.ToInt32(value);
            }
            else if (type == typeof(uint))
            {
                outValue = Convert.ToUInt32(value);
            }
            else if (type == typeof(short))
            {
                outValue = Convert.ToInt16(value);
            }
            else if (type == typeof(ushort))
            {
                outValue = Convert.ToUInt16(value);
            }
            else if (type == typeof(sbyte))
            {
                outValue = Convert.ToSByte(value);
            }
            else if (type == typeof(byte))
            {
                outValue = Convert.ToByte(value);
            }
            else if (type == typeof(char))
            {
                outValue = Convert.ToChar(Convert.ToInt32(value));
            }

            return outValue;
        }
    }
}
