using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FlexibleParser
{
	///<summary>
	///<para>NumberO is the only NumberX class dealing with different numeric types at the same time.</para>
	///<para>It is implicitly convertible to Number, NumberD, NumberP and all the numeric types.</para>
	///</summary>
	public partial class NumberO
	{
		private decimal _Value;
		private int _BaseTenExponent;
		private IEnumerable<Type> _OtherTypes;
		///<summary><para>Decimal variable storing the primary value.</para></summary>
		public decimal Value
		{
			get { return _Value; }
			set
			{
				_Value = value;
				if (_Value == 0) BaseTenExponent = 0;
				Others = PopulateOthers
				(
					_Value, BaseTenExponent, CheckOtherTypes(_OtherTypes)
				)
				.AsReadOnly();
			}
		}
		///<summary><para>Base-ten exponent complementing the primary value.</para></summary>
		public int BaseTenExponent
		{
			get { return _BaseTenExponent; }
			set
			{
				_BaseTenExponent = value;
				Others = PopulateOthers
				(
					Value, _BaseTenExponent, CheckOtherTypes(_OtherTypes)
				)
				.AsReadOnly();
			}
		}
		///<summary><para>Readonly collection including all the other numeric types associated with the current conditions.</para></summary>
		public ReadOnlyCollection<NumberD> Others = new List<NumberD>().AsReadOnly();
		///<summary><para>Readonly member of the ErrorTypesNumber enum which best suits the current conditions.</para></summary>
		public readonly ErrorTypesNumber Error;

		///<summary><para>Initialises a new NumberO instance.</para></summary>
		public NumberO() : this(0m) { }

		///<summary><para>Initialises a new NumberO instance.</para></summary>
		///<param name="numberO">NumberO variable whose information will be used.</param>
		public NumberO(NumberO numberO) : this(numberO, null) { }

		///<summary><para>Initialises a new NumberO instance.</para></summary>
		///<param name="numberO">NumberO variable whose information will be used.</param>
		///<param name="otherType">Member of the OtherTypes enum determining the types to be considered.</param>
		public NumberO(NumberO numberO, OtherTypes otherType) : this
		(
			numberO, GetAssociatedTypes(otherType)
		)
		{ }

		///<summary><para>Initialises a new NumberO instance.</para></summary>
		///<param name="numberO">NumberO variable whose information will be used.</param>
		///<param name="otherTypes">Array containing the types to be considered.</param>
		public NumberO(NumberO numberO, IEnumerable<Type> otherTypes)
		{
			NumberD tempVar = Common.ExtractDynamicToNumberD(numberO);

			if (tempVar.Error != ErrorTypesNumber.None) Error = tempVar.Error;
			else
			{
				//To avoid problems with the automatic actions triggered by some setters, it is better 
				//to always assign values in this order (i.e., first _OtherTypes/Others,  then BaseTenExponent
				//and finally Value).
				if (otherTypes != null) _OtherTypes = otherTypes;
				else
				{
					Others = new List<NumberD>
					(
						numberO.Others.ToList()
					)
					.AsReadOnly();
				}
				BaseTenExponent = tempVar.BaseTenExponent;
				Value = tempVar.Value;
			}
		}

		///<summary><para>Initialises a new NumberO instance.</para></summary>
		///<param name="number">Number variable whose information will be used.</param>
		public NumberO(Number number)
		{
			NumberD tempVar = Common.ExtractDynamicToNumberD(number);
			
			if (tempVar.Error != ErrorTypesNumber.None)
			{
				Error = tempVar.Error;
			}
			else
			{
				BaseTenExponent = tempVar.BaseTenExponent;
				Value = tempVar.Value;
			}
		}

		///<summary><para>Initialises a new NumberO instance.</para></summary>
		///<param name="numberD">NumberD variable whose information will be used.</param>
		public NumberO(NumberD numberD)
		{
			Number tempVar = Common.ExtractDynamicToNumber(numberD);

			if (tempVar.Error != ErrorTypesNumber.None)
			{
				Error = tempVar.Error;
			}
			else
			{
				BaseTenExponent = tempVar.BaseTenExponent;
				Value = tempVar.Value;
			}
		}

		///<summary><para>Initialises a new NumberO instance.</para></summary>
		///<param name="numberP">NumberP variable whose information will be used.</param>
		public NumberO(NumberP numberP)
		{
			Number tempVar = Common.ExtractDynamicToNumber(numberP);

			if (tempVar.Error != ErrorTypesNumber.None)
			{
				Error = tempVar.Error;
			}
			else
			{
				BaseTenExponent = tempVar.BaseTenExponent;
				Value = tempVar.Value;
			}
		}

		///<summary><para>Initialises a new NumberO instance.</para></summary>
		///<param name="value">Main value to be used.</param>
		public NumberO(decimal value) : this
		(
			value, 0, OtherTypes.AllTypes, ErrorTypesNumber.None
		)
		{ }

		///<summary><para>Initialises a new NumberO instance.</para></summary>
		///<param name="value">Main value to be used.</param>
		///<param name="otherType">Member of the OtherTypes enum determining the types to be considered.</param>
		public NumberO(decimal value, OtherTypes otherType) : this
		(
			value, 0, otherType, ErrorTypesNumber.None
		)
		{ }

		///<summary><para>Initialises a new NumberO instance.</para></summary>
		///<param name="value">Main value to be used.</param>
		///<param name="otherTypes">Array containing the types to be considered.</param>
		public NumberO(decimal value, Type[] otherTypes) : this
		(
			value, 0, otherTypes, ErrorTypesNumber.None
		)
		{ }

		///<summary><para>Initialises a new NumberO instance.</para></summary>
		///<param name="value">Main value to be used.</param>
		///<param name="baseTenExponent">Base-ten exponent to be used.</param>
		///<param name="otherType">Member of the OtherTypes enum determining the types to be considered.</param>
		public NumberO(decimal value, int baseTenExponent, OtherTypes otherType) : this
		(
			value, baseTenExponent, otherType, ErrorTypesNumber.None
		)
		{ }

		///<summary><para>Initialises a new NumberO instance.</para></summary>
		///<param name="value">Main value to be used.</param>
		///<param name="baseTenExponent">Base-ten exponent to be used.</param>
		///<param name="otherTypes">Array containing the types to be considered.</param>
		public NumberO(decimal value, int baseTenExponent, Type[] otherTypes) : this
		(
			value, baseTenExponent, otherTypes, ErrorTypesNumber.None
		)
		{ }

		internal NumberO(double value)
		{
			Number tempVar = Conversions.ConvertAnyValueToDecimal(value);

			if (tempVar.Error != ErrorTypesNumber.None)
			{
				//The double type can deliver erroneous values (e.g., NaN or infinity) which are stored as errors.
				Error = tempVar.Error;
			}
			else
			{
				//BaseTenExponent needs also to be considered because the double range is bigger than the decimal one. 
				_OtherTypes = GetAssociatedTypes(OtherTypes.AllTypes);
				BaseTenExponent = tempVar.BaseTenExponent;
				Value = tempVar.Value;
			}
		}

		internal NumberO(float value)
		{
			Number tempVar = Conversions.ConvertAnyValueToDecimal(value);

			if (tempVar.Error != ErrorTypesNumber.None)
			{
				//The float type can deliver erroneous values (e.g., NaN or infinity) which are stored as errors.
				Error = tempVar.Error;
			}
			else
			{
				//BaseTenExponent needs also to be considered because the float range is bigger than the decimal one. 
				_OtherTypes = GetAssociatedTypes(OtherTypes.AllTypes);
				BaseTenExponent = tempVar.BaseTenExponent;
				Value = tempVar.Value;
			}
		}

		internal NumberO(long value) : this
		(
			Conversions.ConvertAnyValueToDecimal(value).Value, 
			0, OtherTypes.AllTypes, ErrorTypesNumber.None
		)
		{ }

		internal NumberO(ulong value) : this
		(
			Conversions.ConvertAnyValueToDecimal(value).Value,
			0, OtherTypes.AllTypes, ErrorTypesNumber.None
		)
		{ }

		internal NumberO(int value) : this
		(
			Conversions.ConvertAnyValueToDecimal(value).Value,
			0, OtherTypes.AllTypes, ErrorTypesNumber.None
		)
		{ }

		internal NumberO(uint value) : this
		(
			Conversions.ConvertAnyValueToDecimal(value).Value,
			0, OtherTypes.AllTypes, ErrorTypesNumber.None
		)
		{ }

		internal NumberO(short value) : this
		(
			Conversions.ConvertAnyValueToDecimal(value).Value,
			0, OtherTypes.AllTypes, ErrorTypesNumber.None
		)
		{ }

		internal NumberO(ushort value) : this
		(
			Conversions.ConvertAnyValueToDecimal(value).Value,
			0, OtherTypes.AllTypes, ErrorTypesNumber.None
		)
		{ }

		internal NumberO(byte value) : this
		(
			Conversions.ConvertAnyValueToDecimal(value).Value,
			0, OtherTypes.AllTypes, ErrorTypesNumber.None
		)
		{ }

		internal NumberO(sbyte value) : this
		(
			Conversions.ConvertAnyValueToDecimal(value).Value,
			0, OtherTypes.AllTypes, ErrorTypesNumber.None
		)
		{ }

		internal NumberO(char value) : this
		(
			Conversions.ConvertAnyValueToDecimal(value).Value,
			0, OtherTypes.AllTypes, ErrorTypesNumber.None
		)
		{ }

		internal NumberO(decimal value, int baseTenExponent) : this
		(
			value, baseTenExponent, OtherTypes.AllTypes, ErrorTypesNumber.None
		)
		{ }

		internal NumberO(ErrorTypesNumber error) { Error = error; }

		private NumberO(decimal value, int baseTenExponent, OtherTypes otherType, ErrorTypesNumber error) : this
		(
			value, baseTenExponent, GetAssociatedTypes(otherType), error
		)
		{ }

		private NumberO(decimal value, int baseTenExponent, IEnumerable<Type> otherTypes, ErrorTypesNumber error)
		{
			if (otherTypes == null) otherTypes = new Type[0];

			_OtherTypes = otherTypes;
			BaseTenExponent = baseTenExponent;
			Value = value;
			Error = error;
		}
	}
}
