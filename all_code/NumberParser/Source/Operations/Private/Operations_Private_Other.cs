using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FlexibleParser
{
	internal partial class Operations
	{
		internal static string PrintNumberXInfo(dynamic value, int baseTenExponent, Type type, CultureInfo culture)
		{
			string outString = Operations.PrintDynamicValue(value, culture) +
			(
				baseTenExponent != 0 ? "*10^" + baseTenExponent.ToString(culture) : ""
			);

			if (type != null)
			{
				outString += " (" + type.ToString() + ")";
			}

			return outString;
		}

		//This method assumes that input is a valid instance of Number, NumberD, NumberO or NumberP. 
		public static dynamic VaryBaseTenExponent(dynamic input, int baseTenIncrease, bool isDivision = false)
		{
			long val1 = input.BaseTenExponent;
			long val2 = baseTenIncrease;

			if (isDivision)
			{
				//Converting a negative value into positive might provoke an overflow error for the int type
				//(e.g., Math.Abs(int.MinValue)). Converting both variables to long is a quick and effective
				//way to avoid this problem.
				val2 *= -1;
			}

			return
			(
				(val2 > 0 && val1 > int.MaxValue - val2) || (val2 < 0 && val1 < int.MinValue - val2) ?
				ErrorInfoNumber.GetNumberXError(input.GetType(), ErrorTypesNumber.NumericOverflow) :
				Common.InitialiseNumberX(input.GetType(), input.Value, (int)(val1 + val2))
			);
		}

		//This method assumes that the input culture isn't null.
		public static string PrintDynamicValue(dynamic value, CultureInfo culture)
		{
			if (value == null) return "";
			Type type = value.GetType();
			if (!Basic.AllNumericTypes.Contains(type)) return "";

			string output = value.ToString(culture);
			
			return
			(
				type == typeof(char) ? "'" + output + "'" : output
			);
		}

		public static NumberD AbsInternal(dynamic input)
		{
			Type type = ErrorInfoNumber.InputTypeIsValidNumericOrNumberX(input);

			return 
			(
				type == null ? 
				new NumberD(ErrorTypesNumber.InvalidInput) : 
				AbsInternalValue(new NumberD(input))
			);
		}

		private static NumberD AbsInternalValue(NumberD numberD)
		{
			if (Basic.AllUnsignedTypes.Contains(numberD.Type))
			{
				return numberD;
			}

			if (IsSpecialAbsCase(numberD.Value, numberD.Type))
			{
				//In certain cases, the minimum value of a type is bigger than the
				//maximum one. That's why a modification is required before calculating
				//the absolute value.
				if (numberD.BaseTenExponent == int.MaxValue)
				{
					return new NumberD(ErrorTypesNumber.InvalidInput);
				}

				numberD.Value /= Basic.GetNumberSpecificType(10, numberD.Type);
				numberD.BaseTenExponent++;
			}

			numberD.Value = PerformArithmeticOperationDynamicVariables
			(
				numberD.Value, Conversions.CastDynamicToType
				(
					numberD.Value < 0 ? -1 : 1, numberD.Type
				), 
				ExistingOperations.Multiplication
			);

			return numberD;
		}

		private static bool IsSpecialAbsCase(dynamic value, Type type)
		{
			return 
			(
				(type == typeof(int) && value == Basic.AllNumberMinMaxs[type][0]) ||
				(type == typeof(long) && value == Basic.AllNumberMinMaxs[type][0]) ||
				(type == typeof(sbyte) && value == Basic.AllNumberMinMaxs[type][0]) ||
				(type == typeof(short) && value == Basic.AllNumberMinMaxs[type][0])
			);
		}
	}
}
