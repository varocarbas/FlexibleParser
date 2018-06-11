using System;

namespace FlexibleParser
{
	///<summary>
	///<para>Math2 contains all the mathematical resources of NumberParser.</para>
	///</summary>
	public partial class Math2
	{
		///<summary>
		///<para>NumberD-adapted version of System.Math.Abs.</para>
		///</summary>
		///<param name="n">Input value.</param>
		public static NumberD Abs(NumberD n)
		{
			return PerformOperationOneOperand(n, ExistingOperations.Abs);
		}

		///<summary>
		///<para>NumberD-adapted version of System.Math.Acos.</para>
		///</summary>
		///<param name="n">Input values.</param>
		public static NumberD Acos(NumberD n)
		{
			return PerformOperationOneOperand(n, ExistingOperations.Acos);
		}

		///<summary>
		///<para>NumberD-adapted version of System.Math.Asin.</para>
		///</summary>
		///<param name="n">Input value.</param>
		public static NumberD Asin(NumberD n)
		{
			return PerformOperationOneOperand(n, ExistingOperations.Asin);
		}

		///<summary>
		///<para>NumberD-adapted version of System.Math.Atan.</para>
		///</summary>
		///<param name="n">Input value.</param>
		public static NumberD Atan(NumberD n)
		{
			return PerformOperationOneOperand(n, ExistingOperations.Atan);
		}

		///<summary>
		///<para>NumberD-adapted version of System.Math.Atan2.</para>
		///</summary>
		///<param name="n1">First value.</param>
		///<param name="n2">Second value.</param>
		public static NumberD Atan2(NumberD n1, NumberD n2)
		{
			return PerformOperationTwoOperands(n1, n2 , ExistingOperations.Atan2);
		}

		///<summary>
		///<para>NumberD-adapted version of System.Math.BigMul.</para>
		///</summary>
		///<param name="n1">First value to multiply.</param>
		///<param name="n2">Second value to multiply.</param>
		public static NumberD BigMul(NumberD n1, NumberD n2)
		{
			try
			{
				NumberD n12 = AdaptInputsToMathMethod(n1, Basic.GetSmallIntegers(), ExistingOperations.BigMul);
				if (n12.Error != ErrorTypesNumber.None)
				{
					return new NumberD(n12.Error);
				}
				NumberD n22 = AdaptInputsToMathMethod(n2, new Type[] { n12.Type }, ExistingOperations.BigMul);
				if (n22.Error != ErrorTypesNumber.None)
				{
					return new NumberD(n22.Error);
				}

				return new NumberD
				(
					Math.BigMul
					(
						(int)n12.Value, (int)n22.Value
					)
				);
			}
			catch
			{
				return new NumberD(ErrorTypesNumber.NativeMethodError);
			}
		}

		///<summary>
		///<para>NumberD-adapted version of System.Math.Ceiling.</para>
		///</summary>
		///<param name="n">Input value.</param>
		public static NumberD Ceiling(NumberD n)
		{
			return PerformOperationOneOperand(n, ExistingOperations.Ceiling);
		}

		///<summary>
		///<para>NumberD-adapted version of System.Math.Cos.</para>
		///</summary>
		///<param name="n">Input value.</param>
		public static NumberD Cos(NumberD n)
		{
			return PerformOperationOneOperand(n, ExistingOperations.Cos);
		}

		///<summary>
		///<para>NumberD-adapted version of System.Math.Cosh.</para>
		///</summary>
		///<param name="n">Input value.</param>
		public static NumberD Cosh(NumberD n)
		{
			return PerformOperationOneOperand(n, ExistingOperations.Cosh);
		}

		///<summary>
		///<para>NumberD-adapted version of System.Math.DivRem.</para>
		///</summary>
		///<param name="n1">Dividend.</param>
		///<param name="n2">Divisor.</param>
		///<param name="result">Remainder.</param>        
		public static NumberD DivRem(NumberD n1, NumberD n2, out NumberD result)
		{
			NumberD n12 = AdaptInputsToMathMethod(n1, Basic.GetSmallIntegers(), ExistingOperations.DivRem);
			if (n12.Error != ErrorTypesNumber.None)
			{
				result = new NumberD(n12.Error);
				return new NumberD(n12.Error);
			}
			NumberD n22 = AdaptInputsToMathMethod(n2, new Type[] { n12.Type }, ExistingOperations.DivRem);
			if (n22.Error != ErrorTypesNumber.None || n22.Value == 0)
			{
				result = new NumberD(n22.Error);
				return new NumberD(n22.Error);
			}

			n12 = Operations.PassBaseTenToValue(n12);
			n22 = Operations.PassBaseTenToValue(n22);

			NumberD outNumber = null;
			try
			{
				long result2 = 0;
				outNumber = new NumberD
				(
					Math.DivRem(n12.Value, n22.Value, out result2)
				);
				result = new NumberD(result2);
			}
			catch
			{
				outNumber = new NumberD(ErrorTypesNumber.NativeMethodError);
				result = new NumberD(ErrorTypesNumber.NativeMethodError);
			}

			return outNumber;
		}

		///<summary>
		///<para>NumberD-adapted version of System.Math.Exp.</para>
		///</summary>
		///<param name="n">Value to which e will be raised.</param>
		public static NumberD Exp(NumberD n)
		{
			return PerformOperationOneOperand(n, ExistingOperations.Exp);
		}

		///<summary>
		///<para>NumberD-adapted version of System.Math.Floor.</para>
		///</summary>
		///<param name="n">Input value.</param>
		public static NumberD Floor(NumberD n)
		{
			return PerformOperationOneOperand(n, ExistingOperations.Floor);
		}

		///<summary>
		///<para>NumberD-adapted version of System.Math.IEEERemainder.</para>
		///</summary>
		///<param name="n1">Dividend.</param>
		///<param name="n2">Divisor.</param>
		public static NumberD IEEERemainder(NumberD n1, NumberD n2)
		{
			return PerformOperationTwoOperands(n1, n2, ExistingOperations.IEEERemainder);
		}

		///<summary>
		///<para>NumberD-adapted version of System.Math.Log.</para>
		///</summary>
		///<param name="n">Value whose base-n logarithm will be calculated.</param>
		public static NumberD Log(NumberD n)
		{
			return PerformOperationOneOperand(n, ExistingOperations.Log);
		}

		///<summary>
		///<para>NumberD-adapted version of System.Math.Log.</para>
		///</summary>
		///<param name="n1">Value whose logarithm will be calculated.</param>
		///<param name="n2">Base of the logarithm.</param>
		public static NumberD Log(NumberD n1, NumberD n2)
		{
			return PerformOperationTwoOperands(n1, n2, ExistingOperations.Log);
		}

		///<summary>
		///<para>NumberD-adapted version of System.Math.Log10.</para>
		///</summary>
		///<param name="n">Value whose base-10 logarithm will be calculated.</param>
		public static NumberD Log10(NumberD n)
		{
			return PerformOperationOneOperand(n, ExistingOperations.Log10);
		}

		///<summary>
		///<para>NumberD-adapted version of System.Math.Max.</para>
		///</summary>
		///<param name="n1">First value.</param>
		///<param name="n2">Second value.</param>
		public static NumberD Max(NumberD n1, NumberD n2)
		{
			return PerformOperationTwoOperands(n1, n2, ExistingOperations.Max);
		}

		///<summary>
		///<para>NumberD-adapted version of System.Math.Min.</para>
		///</summary>
		///<param name="n1">First value.</param>
		///<param name="n2">Second value.</param>
		public static NumberD Min(NumberD n1, NumberD n2)
		{
			return PerformOperationTwoOperands(n1, n2, ExistingOperations.Min);
		}

		///<summary>
		///<para>NumberD-adapted version of System.Math.Pow.</para>
		///</summary>
		///<param name="n1">Base.</param>
		///<param name="n2">Exponent.</param>
		public static NumberD Pow(NumberD n1, NumberD n2)
		{
			return PerformOperationTwoOperands(n1, n2, ExistingOperations.Pow);
		}

		///<summary>
		///<para>NumberD-adapted version of System.Math.Round.</para>
		///</summary>
		///<param name="n">Input value.</param>
		public static NumberD Round(NumberD n)
		{
			return Round(n, 0);
		}

		///<summary>
		///<para>NumberD-adapted version of System.Math.Round.</para>
		///</summary>
		///<param name="n">Input value.</param>
		///<param name="decimals">Number of decimal places.</param>
		public static NumberD Round(NumberD n, int decimals)
		{
			return Round(n, decimals, MidpointRounding.ToEven);
		}

		///<summary>
		///<para>NumberD-adapted version of System.Math.Round.</para>
		///</summary>
		///<param name="n">Input value.</param>
		///<param name="mode">Midpoint rounding mode.</param>
		public static NumberD Round(NumberD n, MidpointRounding mode)
		{
			return Round(n, 0, mode);
		}

		///<summary>
		///<para>NumberD-adapted version of System.Math.Round.</para>
		///</summary>
		///<param name="n">Input value.</param>
		///<param name="decimals">Number of decimal places.</param>
		///<param name="mode">Midpoint rounding mode.</param>
		public static NumberD Round(NumberD n, int decimals, MidpointRounding mode)
		{
			try
			{
				NumberD n2 = AdaptInputsToMathMethod
				(
					n, Basic.AllDecimalTypes, ExistingOperations.Round
				);

				return
				(
					n2.Error != ErrorTypesNumber.None ? new NumberD(n2.Error) :
					new NumberD
					(
						Math.Round(n2.Value, decimals, mode), n2.BaseTenExponent
					)
				);
			}
			catch
			{
				return new NumberD(ErrorTypesNumber.NativeMethodError);
			}
		}

		///<summary>
		///<para>NumberD-adapted version of System.Math.Sign.</para>
		///</summary>
		///<param name="n">Input value.</param>
		public static NumberD Sign(NumberD n)
		{
			return PerformOperationOneOperand(n, ExistingOperations.Sign);
		}

		///<summary>
		///<para>NumberD-adapted version of System.Math.Sin.</para>
		///</summary>
		///<param name="n">Input value.</param>
		public static NumberD Sin(NumberD n)
		{
			return PerformOperationOneOperand(n, ExistingOperations.Sin);
		}

		///<summary>
		///<para>NumberD-adapted version of System.Math.Sinh.</para>
		///</summary>
		///<param name="n">Input value.</param>
		public static NumberD Sinh(NumberD n)
		{
			return PerformOperationOneOperand(n, ExistingOperations.Sinh);
		}

		///<summary>
		///<para>NumberD-adapted version of System.Math.Sqrt.</para>
		///</summary>
		///<param name="n">Input value.</param>
		public static NumberD Sqrt(NumberD n)
		{
			return PerformOperationOneOperand(n, ExistingOperations.Sqrt);
		}

		///<summary>
		///<para>NumberD-adapted version of System.Math.Tan.</para>
		///</summary>
		///<param name="n">Input value.</param>
		public static NumberD Tan(NumberD n)
		{
			return PerformOperationOneOperand(n, ExistingOperations.Tan);
		}

		///<summary>
		///<para>NumberD-adapted version of System.Math.Tanh.</para>
		///</summary>
		///<param name="n">Input value.</param>
		public static NumberD Tanh(NumberD n)
		{
			return PerformOperationOneOperand(n, ExistingOperations.Tanh);
		}

		///<summary>
		///<para>NumberD-adapted version of System.Math.Truncate.</para>
		///</summary>
		///<param name="n">Input value.</param>
		public static NumberD Truncate(NumberD n)
		{
			return PerformOperationOneOperand(n, ExistingOperations.Truncate);
		}
	}
}
