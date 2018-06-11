using System;

namespace FlexibleParser
{
	internal partial class Conversions
	{
		//This method assumes that numberD is a non-null valid NumberD variable.
		public static Number ConvertNumberDToNumber(NumberD numberD)
		{
			Number outNumber = ConvertAnyValueToDecimal(numberD.Value);

			return Operations.VaryBaseTenExponent
			(
				outNumber, numberD.BaseTenExponent
			);
		}

		public static Number ConvertAnyValueToDecimal(dynamic value)
		{
			Type type = ErrorInfoNumber.InputTypeIsValidNumeric(value);

			return
			(
				type == null ? new Number(ErrorTypesNumber.InvalidInput) :
				ConvertAnyValueToDecimal(value, value.GetType())
			);
		}

		//This method assumes that both value and type refer to valid numeric types.
		public static Number ConvertAnyValueToDecimal(dynamic value, Type type)
		{
			return
			(
				//Floating-point values might be outside the decimal range. That's why
				//they need a special conversion.
				type == typeof(double) || type == typeof(float) ?
				ConvertFloatingToDecimal(value) : 
				ConvertNonFloatingToDecimal(value)
			);
		}

		private static Number ConvertNonFloatingToDecimal(dynamic value)
		{
			return new Number()
			{
				//The ranges of all the .NET numeric types which aren't floating-point are
				//smaller than the decimal one. 
				Value = ConvertToDecimalInternal(value),
			};
		}

		//This method expects value to be a valid floating-point variable (i.e., double or float).
		public static Number ConvertFloatingToDecimal(dynamic value)
		{
			Type type = value.GetType();
			Number outNumber = new Number();
			if (value == CastDynamicToType(0, type))
			{
				return outNumber;
			}

			dynamic step = CastDynamicToType(10, type);

			dynamic[] minMax = new dynamic[] 
			{
				CastDynamicToType
				(
					Basic.AllNumberMinMaxPositives[typeof(decimal)][0], type
				), 
				CastDynamicToType
				(
					Basic.AllNumberMinMaxPositives[typeof(decimal)][1], type
				)
			};

			//Note that value can only be double or float and these types cannot provoke an overflow when 
			//using Math.Abs. Additionally, using Operations.AbsInternal right away isn't possible here 
			//because of provoking an infinite loop.
			while (Math.Abs(value) < minMax[0])
			{
				outNumber.BaseTenExponent -= 1;
				value *= step;
			};
			while (Math.Abs(value) > minMax[1])
			{
				outNumber.BaseTenExponent += 1;
				value /= step;
			}

			outNumber.Value = ConvertToDecimalInternal(value);

			return outNumber;
		}
	}
}
