using System;
using System.Collections.Generic;

namespace FlexibleParser
{
	internal partial class OtherParts
	{
		public static Number GetNumberFromUnitP(dynamic unitP)
		{
			return
			(
				unitP == null || unitP.GetType().ToString() != "FlexibleParser.UnitP" || unitP.Error.Type.ToString() != "None" ?
				new Number(ErrorTypesNumber.InvalidInput) : UnitPToNumber(unitP)
			);
		}

		private static Number UnitPToNumber(dynamic unitP)
		{
			return
			(
				Operations.MultiplyInternal
				( 
					new Number(unitP.UnitPrefix.Factor), 
					new Number(unitP.Value, unitP.BaseTenExponent)
				)
			);
		}
	}
}
