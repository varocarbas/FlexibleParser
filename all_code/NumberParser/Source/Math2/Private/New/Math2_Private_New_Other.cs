using System;
using System.Collections.Generic;

namespace FlexibleParser
{
	public partial class Math2
	{
		internal static NumberD FactorialInternal(NumberD n)
		{
			ErrorTypesNumber error = ErrorInfoNumber.GetFactorialError(n);
			if (error != ErrorTypesNumber.None) return new NumberD(error);
			if (n.Value <= 1) return new NumberD(Conversions.CastDynamicToType(1, n.Type));

			Type type = n.Type;
			if (type != typeof(long) && type != typeof(ulong) && type != typeof(int) && type != typeof(uint))
			{
				n.Value = Convert.ToInt64(n.Value);
			}
			n = Operations.PassBaseTenToValue(n);

			//At this point, output.BaseTenExponent has to be zero.
			NumberD output = new NumberD(1, n.Type);
			dynamic i = Conversions.CastDynamicToType(2, n.Type);

			while (i <= n.Value)
			{
				output = Operations.MultiplyInternal(output, i);
				i++;
			}

			return output;
		}
	}
}
