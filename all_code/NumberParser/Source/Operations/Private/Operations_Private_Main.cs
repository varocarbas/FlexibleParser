using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FlexibleParser
{
	internal enum ExistingOperations
	{
		Multiplication, Division, Addition, Subtraction, Greater, GreaterOrEqual, Smaller, 
		SmallerOrEqual, Modulo, Abs, Acos, Asin, Atan, Atan2, BigMul, Ceiling, Cos, Cosh, 
		DivRem, Exp, Floor, IEEERemainder, Log, Log10, Max, Min, Pow, Round, Sign, Sin, Sinh,
		Sqrt, Tan, Tanh, Truncate
	}

	internal partial class Operations
	{
		public static NumberD PerformOtherOperation(NumberD first, NumberD second, ExistingOperations operation, bool checkError = true)
		{
			return new NumberD
			(
				PerformOtherOperation
				(
					new Number(first), new Number(second), operation, checkError
				)
			);
		}

		public static NumberO PerformOtherOperation(NumberO first, NumberO second, ExistingOperations operation, bool checkError = true)
		{
			return new NumberO
			(
				PerformOtherOperation
				(
					new Number(first), new Number(second), operation, checkError
				),
				first.Others.Select(x => x.Type).ToArray()
			);
		}

		public static NumberP PerformOtherOperation(NumberP first, NumberP second, ExistingOperations operation, bool checkError = true)
		{
			return new NumberP
			(
				PerformOtherOperation
				(
					new Number(first), new Number(second), operation, checkError
				), 
				GetOperationString
				(
					first, second, operation, first.Config.Culture
				),
				first.Config
			);
		}

		public static Number PerformOtherOperation(Number first, Number second, ExistingOperations operation, bool checkError = true)
		{
			if (checkError)
			{
				ErrorTypesNumber error = ErrorInfoNumber.GetOperationError
				(
					first, second, operation
				);
				if (error != ErrorTypesNumber.None) return new Number(error);
			}

			Number first2 = PassBaseTenToValue(new Number(first));
			Number second2 = PassBaseTenToValue(new Number(second));

			Number numberOut = new Number();

			if (operation == ExistingOperations.Modulo)
			{
				if (first2.BaseTenExponent == 0 && second2.BaseTenExponent == 0)
				{
					return new Number(first2.Value % second2.Value);
				}
				
				numberOut = PerformArithmeticOperation
				(
					first2, MultiplyInternal
					(
						(Number)Math2.Floor
						(
							DivideInternal(first2, second2)
						), 
						second2
					), 
					ExistingOperations.Subtraction, false
				);
			}
			else
			{
				numberOut.Value = GetComparisonResult
				(
					first2, second2, operation
				);
			}

			return PassBaseTenToValue(numberOut);
		}

		private static decimal GetComparisonResult(Number first, Number second, ExistingOperations operation)
		{
			if (first.BaseTenExponent != second.BaseTenExponent)
			{
				if (operation == ExistingOperations.GreaterOrEqual)
				{
					if (first.BaseTenExponent >= second.BaseTenExponent) return 1m;
				}
				else if (operation == ExistingOperations.Greater)
				{
					if (first.BaseTenExponent > second.BaseTenExponent) return 1m;
				}
				else if (operation == ExistingOperations.SmallerOrEqual)
				{
					if (first.BaseTenExponent <= second.BaseTenExponent) return 1m;
				}
				else if (operation == ExistingOperations.Smaller)
				{
					if (first.BaseTenExponent < second.BaseTenExponent) return 1m;
				}
			}
			else
			{
				if (operation == ExistingOperations.GreaterOrEqual)
				{
					if (first.Value >= second.Value) return 1m;
				}
				else if (operation == ExistingOperations.Greater)
				{
					if (first.Value > second.Value) return 1m;
				}
				else if (operation == ExistingOperations.SmallerOrEqual)
				{
					if (first.Value <= second.Value) return 1m;
				}
				else if (operation == ExistingOperations.Smaller)
				{
					if (first.Value < second.Value) return 1m;
				}
			}

			return 0m;
		}

		private static string GetOperationString(dynamic first, dynamic second, ExistingOperations operation, CultureInfo culture)
		{
			return
			(
				//It doesn't matter whether first/second are NumberX variables or values, because all the NumberX classes
				//have their own ToString(CultureInfo) methods.
				first.ToString(culture) + " " + GetOperationSymbol(operation) + " " + second.ToString(culture)
			);
		}

		private static string GetOperationSymbol(ExistingOperations operation)
		{
			if (operation == ExistingOperations.Addition) return "+";
			if (operation == ExistingOperations.Subtraction) return "-";
			if (operation == ExistingOperations.Multiplication) return "*";
			if (operation == ExistingOperations.Division) return "/";
			if (operation == ExistingOperations.GreaterOrEqual) return ">=";
			if (operation == ExistingOperations.Greater) return ">";
			if (operation == ExistingOperations.SmallerOrEqual) return "<=";
			if (operation == ExistingOperations.Smaller) return "<";
			if (operation == ExistingOperations.Modulo) return "%";

			return "";
		}
	}
}