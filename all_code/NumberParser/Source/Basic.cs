﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
	internal class Basic
	{
		//Positive min./max. values for all the supported numeric types to behave accurately. 
		//This is useful when dealing with NumberX variables, to determine the points where BaseTenExponent
		//has to be brought into picture to complement Value.
		//Note that the main purpose of this collection is to set lower/upper precision limits for each type; an
		//issue which is mostly relevant for the .NET decimal type.
		public static Dictionary<Type, dynamic[]> AllNumberMinMaxPositives = new Dictionary<Type, dynamic[]>()
		{
			{ typeof(decimal), new dynamic[] { 1e-28m, 79228162514264337593543950335m } },
			{ typeof(double), new dynamic[] { 1e-308, double.MaxValue } },
			{ typeof(float), new dynamic[] { 1e-37, float.MaxValue } },
			{ typeof(ulong), new dynamic[] { 1, ulong.MaxValue } },
			{ typeof(long), new dynamic[] { 1, long.MaxValue } },  
			{ typeof(uint), new dynamic[] { 1, uint.MaxValue } },
			{ typeof(int), new dynamic[] { 1, int.MaxValue } },
			{ typeof(ushort), new dynamic[] { 1, ushort.MaxValue } },
			{ typeof(short), new dynamic[] { 1, short.MaxValue } },
			{ typeof(char), new dynamic[] { 1, char.MaxValue } },
			{ typeof(byte), new dynamic[] { 1, byte.MaxValue } },
			{ typeof(sbyte), new dynamic[] { 1, sbyte.MaxValue } },
		};

		//Min./max. values for all the supported numeric types.
		public static Dictionary<Type, dynamic[]> AllNumberMinMaxs = new Dictionary<Type, dynamic[]>()
		{
			{ typeof(decimal), new dynamic[] { decimal.MinValue, decimal.MaxValue } },
			{ typeof(double), new dynamic[] { double.MinValue, double.MaxValue } },
			{ typeof(float), new dynamic[] { float.MinValue, float.MaxValue } },
			{ typeof(ulong), new dynamic[] { ulong.MinValue, ulong.MaxValue } },
			{ typeof(long), new dynamic[] { long.MinValue, long.MaxValue } },  
			{ typeof(uint), new dynamic[] { uint.MinValue, uint.MaxValue } },
			{ typeof(int), new dynamic[] { int.MinValue, int.MaxValue } },
			{ typeof(ushort), new dynamic[] { ushort.MinValue, ushort.MaxValue } },
			{ typeof(short), new dynamic[] { short.MinValue, short.MaxValue } },
			{ typeof(char), new dynamic[] { char.MinValue, char.MaxValue } },
			{ typeof(byte), new dynamic[] { byte.MinValue, byte.MaxValue } },
			{ typeof(sbyte), new dynamic[] { sbyte.MinValue, sbyte.MaxValue } },
		};

		public static Type[] AllNumericTypes = new Type[]
		{
			typeof(decimal), typeof(double), typeof(float), typeof(long), 
			typeof(ulong), typeof(int), typeof(uint), typeof(short), 
			typeof(ushort), typeof(sbyte), typeof(byte), typeof(char)  
		};

		public static Type[] AllUnsignedTypes = new Type[]
		{
			typeof(ulong), typeof(uint), typeof(ushort), typeof(byte), typeof(char)
		};
		
		public static Type[] AllNumberClassTypes = new Type[]
		{
			typeof(Number), typeof(NumberD), typeof(NumberO), typeof(NumberP)
		};
		
		public static Type[] AllDecimalTypes = new Type[]
		{
			typeof(decimal), typeof(double), typeof(float)
		};

		//Returns all the types whose ranges are equal or smaller than int.
		public static Type[] GetSmallIntegers()
		{
			return AllNumericTypes.Where
			(
				x => x != typeof(long) && x != typeof(ulong) &&
				x != typeof(uint) && !AllDecimalTypes.Contains(x)
			)
			.ToArray();
		}

		//The purpose of this function is to easily create simple values for a given type.
		//It isn't prepared to deal with range incompatibilities between different types.
		//Sample inputs: 0, 1 or -1.
		public static dynamic GetNumberSpecificType(dynamic value, Type target)
		{
			Type type = ErrorInfoNumber.InputTypeIsValidNumeric(value);
			if (type == null || type == target) return value;

			return Conversions.CastDynamicToType(value, target);
		}

		//This method assumes that input and type are valid numeric types.
		public static bool InputInsideTypeRange(dynamic input, Type type)
		{
			dynamic value = null;
			dynamic[] minMax = null;

			value = 
			(
				(type == typeof(double) || type == typeof(float)) ? 
				Conversions.ConvertToDoubleInternal(input.Value) : 
				Conversions.ConvertToDecimalInternal(input.Value)
			);

			minMax = new dynamic[]
			{
				Convert.ToDecimal(AllNumberMinMaxs[type][0]),
				Convert.ToDecimal(AllNumberMinMaxs[type][1])
			};

			return (value >= minMax[0] && value <= minMax[1]);
		}
	}
}
