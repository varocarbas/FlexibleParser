namespace FlexibleParser
{
	///<summary>
	///<para>Number is the simplest and lightest NumberX class.</para>
	///<para>It is implicitly convertible to NumberD, NumberO, NumberP and all the numeric types.</para>
	///</summary>
	public partial class Number
	{
		private decimal _Value;
		///<summary><para>Decimal variable storing the primary value.</para></summary>
		public decimal Value
		{
			get { return _Value; }
			set
			{
				_Value = value;
				if (_Value == 0) BaseTenExponent = 0;
			}
		} 
		///<summary><para>Base-ten exponent complementing the primary value.</para></summary>
		public int BaseTenExponent { get; set; }
		///<summary><para>Error.</para></summary>
		public readonly ErrorTypesNumber Error;

		///<summary><para>Initialises a new Number instance.</para></summary>
		public Number() { }

		///<summary><para>Initialises a new Number instance.</para></summary>
		///<param name="value">Main value to be used.</param>
		public Number(decimal value) : this(value, 0) { }

		///<summary><para>Initialises a new Number instance.</para></summary>
		///<param name="value">Main value to be used.</param>
		///<param name="baseTenExponent">Base-ten exponent to be used.</param>
		public Number(decimal value, int baseTenExponent)
		{
			//To avoid problems with the automatic actions triggered by some setters, it is better 
			//to always assign values in this order (i.e., first BaseTenExponent and then Value).
			BaseTenExponent = baseTenExponent;
			Value = value;
		}

		private ErrorTypesNumber PopulateNumberX(dynamic numberX)
		{
			Number tempVar = Common.ExtractDynamicToNumber(numberX);
			if (tempVar.Error != ErrorTypesNumber.None)
			{
				return tempVar.Error;
			}

			BaseTenExponent = tempVar.BaseTenExponent;
			Value = tempVar.Value;

			return ErrorTypesNumber.None;
		}

		///<summary><para>Initialises a new Number instance.</para></summary>
		///<param name="number">Number variable whose information will be used.</param>
		public Number(Number number)
		{
			Error = PopulateNumberX(number);
		}

		///<summary><para>Initialises a new Number instance.</para></summary>
		///<param name="numberD">NumberD variable whose information will be used.</param>
		public Number(NumberD numberD)
		{
			Error = PopulateNumberX(numberD);
		}

		///<summary><para>Initialises a new Number instance.</para></summary>
		///<param name="numberO">NumberO variable whose information will be used.</param>
		public Number(NumberO numberO)
		{
			Error = PopulateNumberX(numberO);
		}

		///<summary><para>Initialises a new Number instance.</para></summary>
		///<param name="numberP">NumberP variable whose information will be used.</param>
		public Number(NumberP numberP)
		{
			Error = PopulateNumberX(numberP);
		}

		private ErrorTypesNumber PopulateDoubleFloat(dynamic value)
		{
			Number tempVar = Conversions.ConvertAnyValueToDecimal(value);
			if (tempVar.Error != ErrorTypesNumber.None)
			{
				//double/float variable can trigger an error (e.g., NaN or infinity).
				return tempVar.Error;
			}

			//BaseTenExponent needs also to be considered because the float/double ranges are
			//bigger than the decimal one. 
			BaseTenExponent = tempVar.BaseTenExponent;
			Value = tempVar.Value;

			return ErrorTypesNumber.None;
		}

		internal Number(double value)
		{
			Error = PopulateDoubleFloat(value);
		}

		internal Number(float value)
		{
			Error = PopulateDoubleFloat(value);
		}

		internal Number(long value)
		{
			Value = Conversions.ConvertAnyValueToDecimal(value).Value;
		}

		internal Number(ulong value)
		{
			Value = Conversions.ConvertAnyValueToDecimal(value).Value;
		}

		internal Number(int value)
		{
			Value = Conversions.ConvertAnyValueToDecimal(value).Value;
		}

		internal Number(uint value)
		{
			Value = Conversions.ConvertAnyValueToDecimal(value).Value;
		}

		internal Number(short value)
		{
			Value = Conversions.ConvertAnyValueToDecimal(value).Value;
		}

		internal Number(ushort value)
		{
			Value = Conversions.ConvertAnyValueToDecimal(value).Value;
		}

		internal Number(byte value)
		{
			Value = Conversions.ConvertAnyValueToDecimal(value).Value;
		}

		internal Number(sbyte value)
		{
			Value = Conversions.ConvertAnyValueToDecimal(value).Value;
		}

		internal Number(char value)
		{
			Value = Conversions.ConvertAnyValueToDecimal(value).Value;
		}

		internal Number(ErrorTypesNumber error) { Error = error; }
	}
}
