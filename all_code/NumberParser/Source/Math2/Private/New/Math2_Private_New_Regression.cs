using System;
using System.Linq;

namespace FlexibleParser
{
	public partial class Math2
	{        
		private static NumberD ApplyPolynomialFitInternal(Polynomial coefficients, NumberD xValue)
		{
			ErrorTypesNumber error = ErrorInfoNumber.ApplyPolynomialFitError(coefficients, xValue);
			if (error != ErrorTypesNumber.None) return new NumberD(error);

			if (coefficients.A.Type != xValue.Type)
			{
				xValue = Conversions.ConvertNumberToAny(xValue, coefficients.A.Type);
			}

			//coefficients.A + coefficients.B * xValue + coefficients.C * xValue * xValue;
			return Operations.AddInternal
			(
				coefficients.A, Operations.AddInternal
				(
					Operations.MultiplyInternal(coefficients.B, xValue),
					Operations.MultiplyInternal(coefficients.C, Operations.MultiplyInternal(xValue, xValue))
				)
			);
		}

		private static Polynomial GetPolynomialFitInternal(NumberD[] xValues, NumberD[] yValues)
		{
			ErrorTypesNumber error = ErrorInfoNumber.GetPolynomialFitError(xValues, yValues);
			if (error != ErrorTypesNumber.None) return new Polynomial(error);

			Type type = xValues[0].Type;
			if (!Basic.AllDecimalTypes.Contains(type))
			{
				//An integer type would provoke the subsequent calculations to fail.
				type = typeof(decimal);
			}
			xValues = SyncType(xValues, type);
			yValues = SyncType(yValues, type);

			//Getting the coefficients matrix generated after calculating least squares.
			GaussJordan gaussJordan = GetGaussJordanCoeffs(xValues, yValues, type);

			for (int i = 0; i < 3; i++)
			{
				for (int i2 = 0; i2 < 3; i2++)
				{
					if (i != i2)
					{
						//factor = 
						//( 
						//    gaussJordan.a[i, i] == 0 ? 0 : 
						//    -1 * gaussJordan.a[i2, i] / gaussJordan.a[i, i]
						//);

						NumberD factor =
						(
							gaussJordan.a[i, i] == new NumberD(type) ? new NumberD(type) :
							Operations.MultiplyInternal
							(
								new NumberD(Basic.GetNumberSpecificType(-1, type)), 
								Operations.DivideInternal
								(
									gaussJordan.a[i2, i], gaussJordan.a[i, i]
								)
							)
						);

						for (int i3 = 0; i3 < 3; i3++)
						{
							//gaussJordan.a[i2, i3] = factor * gaussJordan.a[i, i3] + gaussJordan.a[i2, i3];
							gaussJordan.a[i2, i3] = Operations.AddInternal
							(
								Operations.MultiplyInternal
								(
									factor, gaussJordan.a[i, i3]
								),
								gaussJordan.a[i2, i3]
							);
						}

						//gaussJordan.b[i2] = factor * gaussJordan.b[i] + gaussJordan.b[i2];
						gaussJordan.b[i2] = Operations.AddInternal
						(
							Operations.MultiplyInternal(factor, gaussJordan.b[i]),
							gaussJordan.b[i2]
						);
					}
				}
			}

			return new Polynomial
			(
				CalculatePolynomialCoefficient(gaussJordan, 0, type),
				CalculatePolynomialCoefficient(gaussJordan, 1, type),
				CalculatePolynomialCoefficient(gaussJordan, 2, type)
			);
		}

		private static NumberD[] SyncType(NumberD[] array, Type type)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Type != type)
				{
					array[i] = Conversions.ConvertNumberToAny(array[i], type);
				}
			}

			return array;
		}

		private static NumberD CalculatePolynomialCoefficient(GaussJordan gaussJordan, int i, Type type)
		{
			return
			(
				gaussJordan.a[i, i] == new NumberD(type) ? new NumberD(type) :
				Operations.DivideInternal(gaussJordan.b[i], gaussJordan.a[i, i])
			);
		}

		private static GaussJordan GetGaussJordanCoeffs(NumberD[] xValues, NumberD[] yValues, Type type)
		{
			LeastSquares leastSquares = new LeastSquares(type);

			for (int i = 0; i < xValues.Length; i++)
			{
				NumberD curX2 = new NumberD
				(
					Operations.MultiplyInternal(xValues[i], xValues[i])
				);

				leastSquares.sumX1 = Operations.AddInternal
				(
					leastSquares.sumX1, xValues[i]
				);
				leastSquares.sumX2 = Operations.AddInternal
				(
					leastSquares.sumX2, curX2
				);
				leastSquares.sumX12 = Operations.AddInternal
				(
					leastSquares.sumX12, Operations.MultiplyInternal
					(
						xValues[i], curX2
					)
				);
				leastSquares.sumX1Y = Operations.AddInternal
				(
					leastSquares.sumX1Y, Operations.MultiplyInternal
					(
						xValues[i], yValues[i]
					)
				);
				leastSquares.sumX22 = Operations.AddInternal
				(
					leastSquares.sumX22, Operations.MultiplyInternal
					(
						curX2, curX2
					)
				);
				leastSquares.sumX2Y = Operations.AddInternal
				(
					leastSquares.sumX2Y, Operations.MultiplyInternal
					(
						curX2, yValues[i]
					)
				);
				leastSquares.sumY = Operations.AddInternal
				(
					leastSquares.sumY, yValues[i]
				);
			}

			//a/b arrays emulating the matrix storing the least square outputs, as defined by:
			// a[0, 0]   a[0, 1]  a[0, 2]  | b[0]
			// a[1, 0]   a[1, 1]  a[1, 2]  | b[1]
			// a[2, 0]   a[2, 1]  a[2, 2]  | b[2]
			GaussJordan gaussJordan = new GaussJordan();
			gaussJordan.a[0, 0] = xValues.Length;
			gaussJordan.a[0, 1] = leastSquares.sumX1;
			gaussJordan.a[0, 2] = leastSquares.sumX2;
			gaussJordan.a[1, 0] = leastSquares.sumX1;
			gaussJordan.a[1, 1] = leastSquares.sumX2;
			gaussJordan.a[1, 2] = leastSquares.sumX12;
			gaussJordan.a[2, 0] = leastSquares.sumX2;
			gaussJordan.a[2, 1] = leastSquares.sumX12;
			gaussJordan.a[2, 2] = leastSquares.sumX22;

			gaussJordan.b[0] = leastSquares.sumY;
			gaussJordan.b[1] = leastSquares.sumX1Y;
			gaussJordan.b[2] = leastSquares.sumX2Y;

			return gaussJordan;
		}

		private class GaussJordan
		{
			public NumberD[,] a;
			public NumberD[] b;

			public GaussJordan()
			{
				a = new NumberD[3, 3];
				b = new NumberD[3];
			}
		}

		private class LeastSquares
		{
			public NumberD sumX1, sumX2, sumX12, sumX1Y, sumX22, sumX2Y, sumY;

			public LeastSquares(Type type)
			{
				sumX1 = new NumberD(type);
				sumX2 = new NumberD(type);
				sumX12 = new NumberD(type);
				sumX1Y = new NumberD(type);
				sumX22 = new NumberD(type);
				sumX2Y = new NumberD(type);
				sumY = new NumberD(type);
			}
		}
	}
}
