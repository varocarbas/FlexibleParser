using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    //File including all the required resources to extract the main information of NumberX (i.e., the NumberParser classes
    //Number, NumberD, NumberO and NumberP) variables without including proper definitions of those classes.
    internal partial class OtherParts
    {
        internal static UnitP.UnitInfo GetUnitInfoFromNumberX
        (
            dynamic numberX, UnitP.ExceptionHandlingTypes exceptionHandling = UnitP.ExceptionHandlingTypes.NeverTriggerException,
            PrefixUsageTypes prefixUsage = PrefixUsageTypes.DefaultUsage
        )
        {
            UnitP.UnitInfo outInfo = new UnitP.UnitInfo(0, exceptionHandling, prefixUsage);

            if (numberX == null || !NumberXTypes.Contains((string)numberX.GetType().ToString()) || numberX.Error.ToString() != "None")
            {
                outInfo.Error = UnitP.ErrorTypes.NumericError;
            }
            else
            {
                NumberInternal temp = ConvertAnyValueToDecimal(numberX.Value);

                if (temp.IsWrong) outInfo.Error = UnitP.ErrorTypes.NumericError;
                else
                {
                    try
                    {
                        outInfo.Value = temp.Value;
                        outInfo.BaseTenExponent = temp.Exponent + numberX.BaseTenExponent;
                    }
                    catch
                    {
                        //Very unlikely but possible scenario.
                        outInfo.Error = UnitP.ErrorTypes.NumericError;
                    }
                }
            }

            return outInfo;
        }

        //The types have to be stored as strings to account for the eventuality of not having a reference to NumberParser.
        public static string[] NumberXTypes = new string[]
        {
            "FlexibleParser.Number", "FlexibleParser.NumberD", 
            "FlexibleParser.NumberO", "FlexibleParser.NumberP"
        };

        public static NumberInternal ConvertAnyValueToDecimal(dynamic value)
        {
            Type type = (value == null ? null : value.GetType());

            if (type != null && !AllNumericTypes.Contains(type))
            {
                type = null;
            }

            return 
            (
                type == null ? new NumberInternal() : 
                ConvertAnyValueToDecimal(value, type)
            );
        }

        private static NumberInternal ConvertAnyValueToDecimal(dynamic value, Type type)
        {
            return
            (
                type == typeof(double) || type == typeof(float) ?
                ConvertFloatingToDecimal(value, type) : 
                ConvertNonFloatingToDecimal(value, type)
            );
        }

        private static NumberInternal ConvertFloatingToDecimal(dynamic value, Type type)
        {
            NumberInternal outNumber = new NumberInternal(0m);
            if (value == CastDynamicToType(0, type))
            {
                return outNumber;
            }

            dynamic step = CastDynamicToType(10, type);

            dynamic[] minMax = new dynamic[] 
            {
                CastDynamicToType(1e-28m, type), 
                CastDynamicToType(79228162514264337593543950335m, type)
            };

            while (Math.Abs(value) < minMax[0])
            {
                outNumber.Exponent -= 1;
                value *= step;
            };
            while (Math.Abs(value) > minMax[1])
            {
                outNumber.Exponent += 1;
                value /= step;
            }

            outNumber.Value = ConvertToDecimalInternal(value, type);

            return outNumber;
        }

        private static dynamic CastDynamicToType(dynamic input, Type target)
        {
            try
            {
                if (target == typeof(decimal))
                {
                    return (decimal)input;
                }
                else if (target == typeof(double))
                {
                    return (double)input;
                }
                else if (target == typeof(float))
                {
                    return (float)input;
                }
                else if (target == typeof(long))
                {
                    return (long)input;
                }
                else if (target == typeof(ulong))
                {
                    return (ulong)input;
                }
                else if (target == typeof(int))
                {
                    return (int)input;
                }
                else if (target == typeof(uint))
                {
                    return (uint)input;
                }
                else if (target == typeof(short))
                {
                    return (short)input;
                }
                else if (target == typeof(ushort))
                {
                    return (ushort)input;
                }
                else if (target == typeof(byte))
                {
                    return (byte)input;
                }
                else if (target == typeof(sbyte))
                {
                    return (sbyte)input;
                }
                else if (target == typeof(char))
                {
                    return (char)input;
                }
            }
            catch { }

            return input;
        }

        private static NumberInternal ConvertNonFloatingToDecimal(dynamic value, Type type)
        {
            return new NumberInternal
            (
                ConvertToDecimalInternal(value, type)
            );
        }

        private static decimal ConvertToDecimalInternal(dynamic value, Type type)
        {
            if (type == typeof(decimal)) return value;

            return
            (
                type == typeof(char) ? Convert.ToDecimal((int)value) : Convert.ToDecimal(value)
            );
        }

        private static Type[] AllNumericTypes = new Type[]
        {
            typeof(decimal), typeof(double), typeof(float), typeof(long), 
            typeof(ulong), typeof(int), typeof(uint), typeof(short), 
            typeof(ushort), typeof(sbyte), typeof(byte), typeof(char)  
        };

        //Class used for operations among NumberX (i.e., NumberParser's Number, NumberD, NumberO or NumberP)
        //which emulates their only properties which are relevant here.
        public class NumberInternal
        {
            public decimal Value { get; set; }
            public int Exponent { get; set; }
            public bool IsWrong { get; set; }

            public NumberInternal()
            {
                IsWrong = true;
            }

            public NumberInternal(decimal value, int exponent = 0)
            {
                Value = value;
                Exponent = exponent;
            }
        }
    }
}
