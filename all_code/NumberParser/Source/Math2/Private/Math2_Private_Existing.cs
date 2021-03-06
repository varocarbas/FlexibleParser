﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
	public partial class Math2
	{
		private delegate double Method1Arg(double value);
		private delegate double Method2Arg(double value1, double value2);

		private static Dictionary<ExistingOperations, Method1Arg> AllMathDouble1 = new Dictionary<ExistingOperations, Method1Arg>()
		{
			{ ExistingOperations.Acos, Math.Acos }, { ExistingOperations.Asin, Math.Asin}, 
			{ ExistingOperations.Atan, Math.Atan }, { ExistingOperations.Cos, Math.Cos }, 
			{ ExistingOperations.Cosh, Math.Cosh }, { ExistingOperations.Exp, Math.Exp }, 
			{ ExistingOperations.Log, Math.Log }, { ExistingOperations.Log10, Math.Log10 }, 
			{ ExistingOperations.Sin, Math.Sin }, { ExistingOperations.Sinh, Math.Sinh }, 
			{ ExistingOperations.Sqrt, Math.Sqrt }, { ExistingOperations.Tan, Math.Tan }, 
			{ ExistingOperations.Tanh, Math.Tanh }
		};

		private static Dictionary<ExistingOperations, Method2Arg> AllMathDouble2 = new Dictionary<ExistingOperations, Method2Arg>()
		{
			{ ExistingOperations.Atan2, Math.Atan2 }, { ExistingOperations.IEEERemainder, Math.IEEERemainder }, 
			{ ExistingOperations.Log, Math.Log }, { ExistingOperations.Pow, Math.Pow }
		};

		private static NumberD PerformOperationOneOperand(NumberD n, ExistingOperations operation)
		{
			NumberD n2 = AdaptInputsToMathMethod(n, GetTypesOperation(operation), operation);
			if (n2.Error != ErrorTypesNumber.None) return new NumberD(n2.Error);

			try
			{
				return ApplyMethod1(n2, operation);
			}
			catch
			{
				return new NumberD(ErrorTypesNumber.NativeMethodError);
			}
		}

		private static NumberD PerformOperationTwoOperands(NumberD n1, NumberD n2, ExistingOperations operation)
		{
			NumberD[] ns = CheckTwoOperands
			(
				new NumberD[] { n1, n2 }, operation
			);
			if (ns[0].Error != ErrorTypesNumber.None) return ns[0];

			try
			{
				return ApplyMethod2(ns[0], ns[1], operation);
			}
			catch
			{
				return new NumberD(ErrorTypesNumber.NativeMethodError);
			}
		}

		private static NumberD[] CheckTwoOperands(NumberD[] ns, ExistingOperations operation)
		{
			ns = OrderTwoOperands(ns);

			for (int i = 0; i < ns.Length; i++)
			{
				ns[i] = AdaptInputsToMathMethod
				(
					ns[i], (i == 0 ? GetTypesOperation(operation) : new Type[] { ns[0].Type }), operation
				);
				if (ns[i].Error != ErrorTypesNumber.None)
				{
					return new NumberD[] { new NumberD(ns[i].Error) };
				}
			}

			return ns;
		}

		//When checking whether the input types are compatible with what the given System.Math method
		//expects, the order might matter.
		//Example: with double and int for a method expecting the same type, the double being analysed
		//first provoke the conclusion to always be OK (i.e., int being a different type but implicitly
		//convertible to double). On the other hand, if int was analysed first, it would be considered
		//invalid because double isn't implicitly convertible to int.
		private static NumberD[] OrderTwoOperands(NumberD[] ns)
		{
			return OrderByDecimalAndRange(ns).ToArray();
		}

		private static Type[] GetTypesOperation(ExistingOperations operation)
		{
			if 
			(
				operation == ExistingOperations.Ceiling || 
				operation == ExistingOperations.Floor || 
				operation == ExistingOperations.Truncate
			)
			{ return new Type[] { typeof(double), typeof(decimal) }; }

			if (operation == ExistingOperations.Abs || operation == ExistingOperations.Sign)
			{
				return new Type[]
				{
					typeof(decimal), typeof(double), typeof(float), 
					typeof(long), typeof(int), typeof(short), typeof(sbyte)
				};
			}

			if (operation == ExistingOperations.Max || operation == ExistingOperations.Min)
			{
				return new Type[]
				{
					typeof(decimal), typeof(double), typeof(float),
					typeof(ulong), typeof(long), typeof(uint), 
					typeof(int), typeof(ushort), typeof(short), 
					typeof(sbyte), typeof(byte)
				};
			}

			return new Type[] { typeof(double), typeof(float) };
		}

		//The given operation is associated with a System.Math method with just one argument.
		private static NumberD ApplyMethod1(NumberD n, ExistingOperations operation)
		{
			try
			{
				n.Value = Conversions.CastDynamicToType(n.Value, n.Type);

				if (operation == ExistingOperations.Abs)
				{
					n.Value = Math.Abs(n.Value);
					return n;
				}
				else if (operation == ExistingOperations.Ceiling)
				{
					n.Value = Math.Ceiling(n.Value);
					return n;
				}
				else if (operation == ExistingOperations.Floor)
				{
					n.Value = Math.Floor(n.Value);
					return n;
				}
				else if (operation == ExistingOperations.Truncate)
				{
					n.Value = Math.Truncate(n.Value);
					return n;
				}
				else if (operation == ExistingOperations.Sign)
				{
					n.Value = Math.Sign(n.Value);
					return n;
				}

				//The operation reaching this point matches the corresponding delegate perfectly.
				return ApplyMethod1Delegate(n, AllMathDouble1[operation]);
			}
			catch { return new NumberD(ErrorTypesNumber.NativeMethodError); }
		}

		//The target System.Math method fits the Method1Arg configuration: one double argument 
		//and a double variable returned. 
		private static NumberD ApplyMethod1Delegate(NumberD n, Method1Arg mathMethod)
		{
			n.Value = mathMethod(n.Value);

			return
			(
				ErrorInfoNumber.InputTypeIsValidNumeric(n.Value) != null ? n :
				new NumberD(ErrorTypesNumber.NativeMethodError)
			);
		}

		//The given operation is associated with a System.Math method with two arguments.
		private static NumberD ApplyMethod2(NumberD n1, NumberD n2, ExistingOperations operation)
		{
			try
			{
				n1.Value = Conversions.CastDynamicToType(n1.Value, n1.Type);
				n2.Value = Conversions.CastDynamicToType(n2.Value, n2.Type);

				if (operation == ExistingOperations.Min)
				{
					n1.Value = Math.Min(n1.Value, n2.Value);
					return n1;
				}
				else if (operation == ExistingOperations.Max)
				{
					
					n1.Value = Math.Max(n1.Value, n2.Value);
					return n1;
				}

				//The operation reaching this point matches the corresponding delegate perfectly.
				return ApplyMethod2Delegate(n1, n2, AllMathDouble2[operation]);
			}
			catch { return new NumberD(ErrorTypesNumber.NativeMethodError); }
		}

		//The target System.Math method fits the Method2Arg configuration: two double arguments 
		//and a double variable returned. 
		private static NumberD ApplyMethod2Delegate(NumberD n1, NumberD n2, Method2Arg mathMethod)
		{
			n1.Value = mathMethod(n1.Value, n2.Value);

			return
			(
				ErrorInfoNumber.InputTypeIsValidNumeric(n1.Value) != null ? n1 :
				new NumberD(ErrorTypesNumber.NativeMethodError)
			);
		}

		//Method adapting the input variable to the requirements of the given System.Math method.
		//If relying on the original type isn't possible, a conversion to double (i.e., biggest-range
		//type which is supported by all the System.Math methods when reaching here) would be performed.
		private static NumberD AdaptInputsToMathMethod(NumberD n, Type[] targets, ExistingOperations operation)
		{
			if (n == null) return new NumberD(ErrorTypesNumber.InvalidInput);
			if (n.Error != ErrorTypesNumber.None) return new NumberD(n.Error);
			if (ErrorInfoNumber.InputTypeIsValidNumeric(n.Value) == null)
			{
				return new NumberD(ErrorTypesNumber.InvalidInput);
			}

			NumberD n2 = AdaptInputToMathMethod2(new NumberD(n), targets, operation);
			if (n2.Error != ErrorTypesNumber.None) return n2;           

			if (n2.BaseTenExponent != 0) n2 = Operations.PassBaseTenToValue((NumberD)n2);

			if (n2.BaseTenExponent != 0)
			{
				if (n2.Type != typeof(double))
				{
					n2.Value = Conversions.ConvertToDoubleInternal(n2.Value);
					n2 = Operations.PassBaseTenToValue(n2);
				}

				if (n2.BaseTenExponent != 0)
				{
					//The value is outside the maximum supported range by the given System.Math method.
					n2 = new NumberD(ErrorTypesNumber.NativeMethodError);
				}
			}

			return n2;
		}

		private static NumberD AdaptInputToMathMethod2(NumberD n2, Type[] targets, ExistingOperations operation)
		{
			if (!targets.Contains(n2.Type))
			{
				NumberD n3 = AdaptInputToMathMethodImplicit(n2, targets);
				if (n3 != null) return n3;

				if (n2.Type != typeof(double) && operation != ExistingOperations.DivRem && operation != ExistingOperations.BigMul)
				{
					//Except DivRem and BigMul, all the System.Math methods accept double as argument.
					n2.Value = Conversions.ConvertToDoubleInternal(n2.Value);
				}
				else return new NumberD(ErrorTypesNumber.NativeMethodError);
			}
			if (n2.Type != typeof(double) && operation != ExistingOperations.DivRem && operation != ExistingOperations.BigMul)
			{
				//Except DivRem and BigMul, all the System.Math methods accept double as argument.
				if (!targets.Contains(n2.Type))
				{
					n2.Value = Conversions.ConvertToDoubleInternal(n2.Value);
				}
			}

			return n2;
		}

		private static NumberD AdaptInputToMathMethodImplicit(NumberD n, Type[] targets)
		{
			if (!Basic.AllDecimalTypes.Contains(n.Type))
			{
				var target = OrderByDecimalAndRange(targets).First();
				if (Basic.AllDecimalTypes.Contains(target))
				{
					//n is an integer and one of the targets is decimal. This means that an implicit
					//conversion is possible and, consequently, no further analysis is required to
					//conclude that the current input scenario is valid.
					return Conversions.ConvertNumberToAny(n, target);
				}
			}

			return null;
		}

		private static IEnumerable<NumberD> OrderByDecimalAndRange(IEnumerable<NumberD> ns)
		{
			return ns.OrderByDescending(x => Basic.AllDecimalTypes.Contains(x.Type)).ThenByDescending
			(
				x => Convert.ToDouble(Basic.AllNumberMinMaxs[x.Type][1]) - Convert.ToDouble(Basic.AllNumberMinMaxs[x.Type][0])
			);
		}

		private static IEnumerable<Type> OrderByDecimalAndRange(IEnumerable<Type> ns)
		{
			return ns.OrderByDescending(x => Basic.AllDecimalTypes.Contains(x)).ThenByDescending
			(
				x => Convert.ToDouble(Basic.AllNumberMinMaxs[x][1]) - Convert.ToDouble(Basic.AllNumberMinMaxs[x][0])
			);
		}
	}
}