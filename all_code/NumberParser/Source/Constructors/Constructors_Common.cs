using System;

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

		//This method expects numberX to be a valid NumberX type 
		//(i.e., Number, NumberD, NumberO or NumberP).
		public static Number ExtractDynamicToNumber(dynamic numberX)
		{
			if (numberX == null) return new Number(ErrorTypesNumber.InvalidInput);
			if (numberX.Error != ErrorTypesNumber.None) return new Number(numberX.Error);

			Type type = numberX.GetType();

			return
			(
				type == typeof(Number) || type == typeof(NumberO) ?
				new Number(numberX.Value, numberX.BaseTenExponent) :
				Conversions.ConvertNumberDToNumber(numberX)
			);
		}

		//This method expects numberX to be a valid NumberX type 
		//(i.e., Number, NumberD, NumberO or NumberP).
		public static NumberD ExtractDynamicToNumberD(dynamic numberX)
		{
			if (numberX == null) return new NumberD(ErrorTypesNumber.InvalidInput);
			if (numberX.Error != ErrorTypesNumber.None) return new NumberD(numberX.Error);

			return new NumberD()
			{
				Value = numberX.Value,
				BaseTenExponent = numberX.BaseTenExponent
			};
		}
	}
}
