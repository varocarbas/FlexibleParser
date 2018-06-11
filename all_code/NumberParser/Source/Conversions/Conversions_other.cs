using System;

namespace FlexibleParser
{
    internal partial class Conversions
    {
        //This method complements the in-built Convert.ToDecimal method by accounting for situations like
        //char type variables.
        public static decimal ConvertToDecimalInternal(dynamic value)
        {
            Type type = ErrorInfoNumber.InputTypeIsValidNumeric(value);
            if (type == null) return decimal.MinValue;

            return
            (
                type == typeof(char) ? 
                Convert.ToDecimal((int)value) : 
                Convert.ToDecimal(value)
            );
        }


        //Note that double is used as a backup type in quite a few situations. 
        //This method complements the in-built Convert.ToDouble method by accounting for situations like
        //char type variables.
        public static double ConvertToDoubleInternal(dynamic value)
        {
            Type type = ErrorInfoNumber.InputTypeIsValidNumeric(value);
            if (type == null) return double.NaN;

            return
            (
                type == typeof(char) ? 
                Convert.ToDouble((int)value) : 
                Convert.ToDouble(value)
            );
        }

        //When calling methods with different overloads (e.g., some of the System.Math ones),
        //the dynamic variables need to be cast to a specific type.
        //This method assumes that input can be directly cast to the target numeric type.
        public static dynamic CastDynamicToType(dynamic input, Type target)
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
    }
}
