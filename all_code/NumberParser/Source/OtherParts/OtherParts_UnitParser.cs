namespace FlexibleParser
{
	//File including all the required resources to extract the main information of UnitP (i.e., the main UnitParser class)
	//variables without including a proper definition of that class.
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
