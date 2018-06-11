using System;
using System.Globalization;

namespace FlexibleParser
{
	public partial class NumberP : IComparable<NumberP>
	{
		///<summary><para>Compares the current instance against another NumberP one.</para></summary>
		///<param name="other">The other NumberP instance.</param>
		public int CompareTo(NumberP other)
		{
			return Operations.CompareDynamic(this, other);
		}

		///<summary>
		///<para>Outputs an error or "Value*10^BaseTenExponent (OriginalString)" together with all the Config information via invoking its ToString() method (BaseTenExponent different than zero).</para>
		///</summary>
		public override string ToString()
		{
			return ToString(CultureInfo.InvariantCulture);
		}

		///<summary>
		///<para>Outputs an error or "Value*10^BaseTenExponent (OriginalString)" together with all the Config information via invoking its ToString() method (BaseTenExponent different than zero).</para>
		///</summary>
		///<param name="culture">Culture.</param>
		public string ToString(CultureInfo culture)
		{
			if (Error != ErrorTypesNumber.None) return "Error. " + Error.ToString();
			if (culture == null) culture = CultureInfo.InvariantCulture;

			NumberD numberD = Operations.PassBaseTenToValue((NumberD)this, true);
			return Operations.PrintNumberXInfo
			(
				numberD.Value, numberD.BaseTenExponent, null, culture
			)
			+ " (" + OriginalString + ")" + Environment.NewLine + Config.ToString();
		}

		///<summary><para>Creates a new NumberP instance by relying on the most adequate constructor.</para></summary>
		///<param name="input">String input.</param>
		public static implicit operator NumberP(string input)
		{
			return new NumberP(input);
		}

		///<summary><para>Creates a new NumberP instance by relying on the most adequate constructor.</para></summary>
		///<param name="input">Number input.</param>
		public static implicit operator NumberP(Number input)
		{
			return new NumberP(input);
		}

		///<summary><para>Creates a new NumberP instance by relying on the most adequate constructor.</para></summary>
		///<param name="input">NumberD input.</param>
		public static implicit operator NumberP(NumberD input)
		{
			return new NumberP(input);
		}

		///<summary><para>Creates a new NumberP instance by relying on the most adequate constructor.</para></summary>
		///<param name="input">NumberO input.</param>
		public static implicit operator NumberP(NumberO input)
		{
			return new NumberP(input);
		}

		///<summary><para>Adds two NumberP variables.</para></summary>
		///<param name="first">First operand.</param>
		///<param name="second">Second operand.</param>
		public static NumberP operator +(NumberP first, NumberP second)
		{
			return Operations.PerformArithmeticOperation
			(
				first, second, ExistingOperations.Addition
			);
		}

		///<summary><para>Subtracts two NumberP variables.</para></summary>
		///<param name="first">First operand.</param>
		///<param name="second">Second operand.</param>
		public static NumberP operator -(NumberP first, NumberP second)
		{
			return Operations.PerformArithmeticOperation
			(
				first, second, ExistingOperations.Subtraction
			);
		}

		///<summary><para>Multiplies two NumberP variables.</para></summary>
		///<param name="first">First operand.</param>
		///<param name="second">Second operand.</param>
		public static NumberP operator *(NumberP first, NumberP second)
		{
			return Operations.PerformArithmeticOperation
			(
				first, second, ExistingOperations.Multiplication
			);
		}

		///<summary><para>Divides two NumberP variables.</para></summary>
		///<param name="first">First operand.</param>
		///<param name="second">Second operand.</param>
		public static NumberP operator /(NumberP first, NumberP second)
		{
			return Operations.PerformArithmeticOperation
			(
				first, second, ExistingOperations.Division
			);
		}

		///<summary><para>Calculates the modulo of two NumberP variables.</para></summary>
		///<param name="first">First operand.</param>
		///<param name="second">Second operand.</param>
		public static decimal operator %(NumberP first, NumberP second)
		{
			return first.Value % second.Value;
		}

		///<summary><para>Determines whether a NumberP variable is greater than other.</para></summary>
		///<param name="first">First operand.</param>
		///<param name="second">Second operand.</param>
		public static bool operator >(NumberP first, NumberP second)
		{
			return Operations.CompareDynamic(first, second) == 1;
		}

		///<summary><para>Determines whether a NumberP variable is greater or equal than other.</para></summary>
		///<param name="first">First operand.</param>
		///<param name="second">Second operand.</param>
		public static bool operator >=(NumberP first, NumberP second)
		{
			return Operations.CompareDynamic(first, second) >= 0;
		}

		///<summary><para>Determines whether a NumberP variable is smaller than other.</para></summary>
		///<param name="first">First operand.</param>
		///<param name="second">Second operand.</param>
		public static bool operator <(NumberP first, NumberP second)
		{
			return Operations.CompareDynamic(first, second) == -1;
		}

		///<summary><para>Determines whether a NumberP variable is smaller or equal than other.</para></summary>
		///<param name="first">First operand.</param>
		///<param name="second">Second operand.</param>
		public static bool operator <=(NumberP first, NumberP second)
		{
			return Operations.CompareDynamic(first, second) <= 0;
		}

		///<summary><para>Determines whether two NumberP variables are equal.</para></summary>
		///<param name="first">First operand.</param>
		///<param name="second">Second operand.</param>
		public static bool operator ==(NumberP first, NumberP second)
		{
			return Operations.NoNullEquals(first, second);
		}

		///<summary><para>Determines whether two NumberP variables are different.</para></summary>
		///<param name="first">First operand.</param>
		///<param name="second">Second operand.</param>
		public static bool operator !=(NumberP first, NumberP second)
		{
			return !Operations.NoNullEquals(first, second);
		}

		///<summary><para>Determines whether the current NumberP variable is equal to other one.</para></summary>
		///<param name="other">Other variable.</param>
		public bool Equals(NumberP other)
		{
			return
			(
				object.Equals(other, null) ? false :
				Operations.NumberXsAreEqual(this, other)
			);
		}

		///<summary><para>Determines whether the current NumberP variable is equal to other one.</para></summary>
		///<param name="obj">Other variable.</param>
		public override bool Equals(object obj)
		{
			return Equals(obj as NumberP);
		}

		///<summary><para>Returns the hash code for this NumberP variable.</para></summary>
		public override int GetHashCode()
		{
			return 0;
		}
	}
}
