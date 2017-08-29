using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FlexibleParser
{
	internal partial class Operations
	{
		//This method is called from CompareTo of Number/NumberO instances.
		public static int CompareDecimal(dynamic thisVar, dynamic other)
		{
			int tempInt = ComparePreanalysis(thisVar, other);
			if (tempInt != -2) return tempInt;

			return
			(
				thisVar.BaseTenExponent == other.BaseTenExponent ?
				thisVar.Value.CompareTo(other.Value) :
				thisVar.BaseTenExponent.CompareTo(other.BaseTenExponent)
			);
		}

		//This method is called from CompareTo of NumberD/NumberP instances. 
		//It always relies on the same common type (i.e., decimal) to avoid different-type-Value problems.
		public static int CompareDynamic(dynamic thisVar, dynamic other)
		{
			int tempInt = ComparePreanalysis(thisVar, other);
			if (tempInt != -2) return tempInt;

			NumberD[] adapted = ComparedInstancesToDecimal(thisVar, other);

			return
			(
				adapted[0].BaseTenExponent == adapted[1].BaseTenExponent ?
				adapted[0].Value.CompareTo(adapted[1].Value) :
				adapted[0].BaseTenExponent.CompareTo(adapted[1].BaseTenExponent)
			);
		}

		//Both arguments are non-null NumberD/NumberP instances.
		private static NumberD[] ComparedInstancesToDecimal(dynamic thisVar, dynamic other)
		{
			//Decimal is the most precise type and NumberParser is an eminently-precision-focused library.
			//The decimal range is notably smaller than the one of other types (e.g., double) and, consequently,
			//that decision might provoke (avoidable) errors. In any case, the scenarios provoking those errors
			//happen under so extreme conditions (i.e., over/under +-10^2147483647) that cannot justify the reliance
			//on a less precise type.
			return new NumberD[]
			{
				new NumberD(thisVar.Value, thisVar.BaseTenExponent, typeof(decimal)),
				new NumberD(other.Value, other.BaseTenExponent, typeof(decimal))
			};
		}

		private static int ComparePreanalysis(dynamic thisVar, dynamic other)
		{
			if (thisVar == null || other == null)
			{
				if (thisVar == other) return 0;

				return (thisVar == null ? -1 : 1);
			}

			if 
			(
				thisVar.Error != ErrorTypesNumber.None || 
				other.Error != ErrorTypesNumber.None
			)
			{
				if (thisVar.Error == other.Error) return 0;

				return 
				(
					other.Error != ErrorTypesNumber.None ? -1 : 1
				);
			}

			return -2;
		}
	}
}
