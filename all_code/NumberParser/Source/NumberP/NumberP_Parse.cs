using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FlexibleParser
{
	public partial class NumberP
	{
		internal static NumberD ParseAnyMain(string input, ParseConfig config)
		{
			return Conversions.ConvertNumberToAny
			(
				ParseDecimalMain(input, config.Culture, config.NumberStyle), config.Target
			);
		}

		internal static Number ParseDecimalMain(string input, CultureInfo culture, NumberStyles style)
		{
			Number outNumber = ParseDecimal(input, culture, style);

			return
			(
				outNumber.Error == ErrorTypesNumber.None ? outNumber :
				ParseDoubleAndBeyond(input, culture, style)
			);
		}

		private static Number ParseDoubleAndBeyond(string input, CultureInfo culture, NumberStyles style)
		{
			double value = 0.0;

			return
			(
				double.TryParse(input, style, culture, out value) && value != 0.0 ?
				Conversions.ConvertFloatingToDecimal(value) : ParseBeyondDouble(input, culture, style)
			);
		}

		private static Number ParseBeyondDouble(string input, CultureInfo culture, NumberStyles style)
		{
			string stringToParse = input.Trim().ToLower();

			//The numeric-parsing .NET format is expected. That is: only integer exponents after the letter "e", what 
			//has to be understood as before-e * 10^after-e.
			if (stringToParse.Contains("e"))
			{
				double[] tempVals = ExponentialPartsAnalysis
				(
					stringToParse, culture, style
				);

				if (tempVals != null)
				{
					return Operations.VaryBaseTenExponent
					(
						Conversions.ConvertFloatingToDecimal(tempVals[0]), (int)tempVals[1]
					);
				}
			}
			else
			{
				if (stringToParse.Length < 300)
				{
					double tempDoub = 0.0;
					if (double.TryParse(stringToParse, style, culture, out tempDoub))
					{
						return Conversions.ConvertFloatingToDecimal(tempDoub);
					}
				}
				else
				{
					double startNumber = 0.0;
					if (double.TryParse(stringToParse.Substring(0, 299), style, culture, out startNumber))
					{
						string remString = stringToParse.Substring(299);
						if (remString.FirstOrDefault(x => !char.IsDigit(x) && !InputIsCultureFeature(x.ToString(), CultureFeatures.ThousandSeparator, culture)) != '\0')
						{
							//Finding a decimal separator here is considered an error because it wouldn't
							//be too logical (300 digits before the decimal separator!). Mainly by bearing
							//in mind the exponential alternative above.
							return new Number(ErrorTypesNumber.InvalidInput);
						}

						int beyondCount = GetBeyondDoubleCharacterCount
						(
							stringToParse.Substring(299), culture
						);

						if (beyondCount <= 0)
						{
							return new Number
							(
								beyondCount == 0 ? ErrorTypesNumber.InvalidInput : 
								ErrorTypesNumber.NumericOverflow
							);
						}

						//Accounting for the differences 0.001/1000 -> 10^-3/10^3.
						int sign = (Math.Abs(startNumber) < 1.0 ? -1 : 1);

						if (startNumber == 0.0 && sign == -1)
						{
							//This code accounts for situations like 0.00000[...]00001 where, for the aforementioned double.TryParse, startNumber is zero.
							bool found = false;
							int length2 = (remString.Length > 299 ? 299 : remString.Length);
							for (int i = 0; i < remString.Length; i++)
							{
								if (remString[i] != '0' && !InputIsCultureFeature(remString[i].ToString(), CultureFeatures.ThousandSeparator, culture))
								{
									//The default interpretation is initial_part*10^remString.Length (up to the maximum length natively supported by double). 
									//For example, 0.0000012345[...]4565879561424 is correctly understood as 0.0000012345*10^length-after-[...]. In this specific 
									//situation (i.e., initial_part understood as zero), some digits after [...] have to also be considered to form initial_part. 
									//Thus, startNumber is being redefined as all the digits (up to the maximum length natively supported by double) after the 
									//first non-zero one; and beyondCount (i.e., the associated 10-base exponent) such that it also includes all the digits since the start.
									found = true;
									startNumber = double.Parse(remString.Substring(i, length2 - i), culture);
									beyondCount = 297 + length2;
									break;
								}
							}

							if(!found) return new Number(0m);
						}

						return Operations.VaryBaseTenExponent
						(
							Conversions.ConvertFloatingToDecimal(startNumber), sign * beyondCount
						);
					}
				}
			}

			return new Number(ErrorTypesNumber.ParseError);
		}

		//This method is only called after having confirmed that the given input contains "e".
		private static double[] ExponentialPartsAnalysis(string input, CultureInfo culture, NumberStyles style)
		{
			string[] tempStrings = input.Split('e');
			if (tempStrings.Length != 2) return null;

			double[] outVals = new double[2];
			//Standard double.TryParse is fine because the input string isn't expected to fulfill the NumberP rules, but the
			//standard .NET ones (i.e., double + "e" + int).
			if (!double.TryParse(tempStrings[0], style, culture, out outVals[0]))
			{
				return null;
			}
			else if (!double.TryParse(tempStrings[1], style, culture, out outVals[1]) || Math.Abs(outVals[1]) > int.MaxValue)
			{
				return null;
			}

			return outVals;
		}

		private static int GetBeyondDoubleCharacterCount(string remString, CultureInfo culture)
		{
			int outCount = 0;

			try
			{
				foreach (char item in remString.ToCharArray())
				{
					if (!char.IsDigit(item) && !InputIsCultureFeature(item.ToString(), CultureFeatures.ThousandSeparator, culture))
					{
						return 0;
					}
					outCount = outCount + 1;
				}
			}
			catch
			{
				//The really unlikely scenario of hitting int.MaxValue.
				outCount = -1;
			}

			return outCount;
		}

		internal static Number ParseDecimal(string input, CultureInfo culture, NumberStyles style)
		{
			decimal value = 0m;

			return
			(
				decimal.TryParse(input, style, culture, out value)
				//decimal.TryParse can consider as valid numbers inputs beyond the actual scope of the decimal type 
				//(e.g., 0.00000000000000000000000000000001m assumed to be zero). That's why an error is assumed for 
				//cases where the parsed value is zero. Note that getting an error here would imply a call to ParseDouble 
				//(+ eventually ParseBeyondDouble), where an actual zero would be properly recognised.
				&& (value != 0m) ? new Number(value) : new Number(ErrorTypesNumber.InvalidInput)
			);
		}
	}
}
