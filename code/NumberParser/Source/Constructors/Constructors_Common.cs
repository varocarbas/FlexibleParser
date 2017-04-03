using System;
using System.Collections.Generic;

namespace FlexibleParser
{
    internal partial class Common
    {
        public static dynamic InitialiseNumberX(Type type, dynamic value, int baseTenExponent)
        {
            if (type == typeof(Number))
            {
                return new Number(value, baseTenExponent);
            }
            else if (type == typeof(NumberD))
            {
                return new NumberD(value, baseTenExponent);
            }
            else if (type == typeof(NumberO))
            {
                return new NumberO(value, baseTenExponent);
            }
            else if (type == typeof(NumberP))
            {
                return new NumberP(value, baseTenExponent);
            }

            return null;
        }

        //This method expects numberX to be a NumberD or NumberP variable.
        public static Number ExtractDynamicToDecimalInfo(dynamic numberX)
        {
            if (numberX == null) return new Number(ErrorTypesNumber.InvalidInput);

            return
            (
                numberX.Error != ErrorTypesNumber.None ? new Number(numberX.Error) :
                Conversions.ConvertNumberDToNumber(numberX)
            );
        }

        public static NumberD ExtractSameTypeNumberXInfo(dynamic numberX)
        {
            if (numberX == null) return new Number(ErrorTypesNumber.InvalidInput);
            if (numberX.Error != ErrorTypesNumber.None) return new NumberD(numberX.Error);

            NumberD numberD = new NumberD();
            numberD.Value = numberX.Value;
            numberD.BaseTenExponent = numberX.BaseTenExponent;
            
            return numberD;
        }
    }
}
