using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FlexibleParser
{
    public partial class UnitP
    {
        private static UnitP PerformUnitOperation(UnitP first, double secondValue, Operations operation, string operationString)
        {
            return PerformUnitValueOperation
            (
                first, new UnitInfo(first), ConvertDoubleToDecimal(secondValue),
                operation, operationString
            );
        }

        private static UnitP PerformUnitOperation(double firstValue, UnitP second, Operations operation, string operationString)
        {
            //The first operand (the one defining exception handling) is a number and that's why exceptions have to be left unhandled.
            UnitP second2 = new UnitP(second, ExceptionHandlingTypes.AlwaysTriggerException);
            return PerformUnitValueOperation
            (
                second2, ConvertDoubleToDecimal(firstValue), new UnitInfo(second),
                operation, operationString
            );
        }

        private static UnitP PerformUnitOperation(UnitP first, decimal secondValue, Operations operation, string operationString)
        {
            return PerformUnitValueOperation
            (
                first, new UnitInfo(first), new UnitInfo(secondValue), operation, operationString
            );
        }

        private static UnitP PerformUnitOperation(decimal firstValue, UnitP second, Operations operation, string operationString)
        {
            //The first operand (the one defining exception handling) is a number and that's why exceptions have to be left unhandled.
            UnitP second2 = new UnitP(second, ExceptionHandlingTypes.AlwaysTriggerException);
            return PerformUnitValueOperation
            (
                second2, new UnitInfo(firstValue), new UnitInfo(second), operation, operationString
            );
        }

        private static UnitP PerformUnitValueOperation(UnitP unitP, UnitInfo firstInfo, UnitInfo secondInfo, Operations operation, string operationString)
        {
            UnitInfo outInfo = new UnitInfo(firstInfo)
            {
                Unit = unitP.Unit,
                Parts = new List<UnitPart>(unitP.UnitParts)
            };

            outInfo.Error = new ErrorInfo
            (
                GetUnitValueOperationError
                (
                    unitP, firstInfo, secondInfo, operation
                )
            );

            if (outInfo.Error.Type == ErrorTypes.None)
            {
                outInfo = PerformManagedOperationUnits
                (
                    outInfo, secondInfo, operation
                );

                if (operation == Operations.Division && secondInfo.Unit != Units.None && secondInfo.Unit != Units.Unitless)
                {
                    if (outInfo.Parts.Count > 0)
                    {
                        //value/unit is the only scenario value-unit operation where the unit
                        //information needs further analysis.

                        for (int i = 0; i < outInfo.Parts.Count; i++)
                        {
                            outInfo.Parts[i].Exponent *= -1;
                        }

                        outInfo = StartCompoundAnalysis
                        (
                            new ParseInfo
                            (
                                InversePrefix(outInfo)
                            )
                        )
                        .UnitInfo;
                    }
                }
            }

            return
            (
                outInfo.Error.Type != ErrorTypes.None ? new UnitP(unitP, outInfo.Error.Type) :
                new UnitP(outInfo, unitP, operationString, false)
            );
        }

        private static UnitInfo InversePrefix(UnitInfo outInfo)
        {
            if (outInfo.Prefix.Factor == 1m) return outInfo;

            outInfo = new UnitInfo(outInfo) 
            {
                Prefix = new Prefix(outInfo.Prefix.PrefixUsage)
            };

            return 
            (
                //No need to find a new prefix.
                outInfo * PerformManagedOperationValues
                (
                    1m, outInfo.Prefix.Factor, Operations.Division
                )               
            );
        }

        private static UnitInfo ParseDecimal(string stringToParse)
        {
            decimal value = 0m;
            
            if (decimal.TryParse(stringToParse, NumberStyles.Any, CultureInfo.InvariantCulture, out value))
            {
                //In some cases, decimal.TryParse might consider valid numbers beyond the actual scope of
                //decimal type. For example: 0.00000000000000000000000000000001m assumed to be zero.
                //if (value != 0m) return new UnitInfo(value);
            }

            return ParseDouble(stringToParse);
        }

        private static UnitInfo ParseDouble(string stringToParse)
        {
            double valueDouble = 0.0;

            return
            (
                double.TryParse(stringToParse, NumberStyles.Any, CultureInfo.InvariantCulture, out valueDouble) ?
                ConvertDoubleToDecimal(valueDouble) : GetInfoBeyondDouble(stringToParse)
            );
        }

        //This method complements .NET numeric-type parsing methods beyond the double range (i.e., > 1e300).
        private static UnitInfo GetInfoBeyondDouble(string stringToParse)
        {
            stringToParse = stringToParse.ToLower();
            UnitInfo errorInfo = new UnitInfo
            (
                new UnitInfo(), ErrorTypes.NumericError
            );

            //The double-parsing .NET format is expected. That is: only integer exponents after a
            //letter "e", what indicates before-e * 10^after-e.
            if (stringToParse.Contains("e"))
            {
                string[] temp = stringToParse.Split('e');
                if (temp.Length == 2)
                {
                    UnitInfo outInfo = ParseDouble(temp[0]);
                    if (outInfo.Error.Type == ErrorTypes.None)
                    {
                        int expInt = 0;
                        if (int.TryParse(temp[1], out expInt))
                        {
                            outInfo.BaseTenExponent += expInt;

                            return outInfo;
                        }
                    }
                    else return errorInfo;
                }
            }
            else
            {
                if (stringToParse.Length < 300)
                {
                    double tempDoub = 0.0;
                    if (double.TryParse(stringToParse, out tempDoub))
                    {
                        return ConvertDoubleToDecimal(tempDoub);
                    }
                }
                else
                {
                    double startNumber = 0.0;
                    if (double.TryParse(stringToParse.Substring(0, 299), out startNumber))
                    {
                        string remString = stringToParse.Substring(299);
                        if (remString.FirstOrDefault(x => !char.IsDigit(x) && x != ',') != '\0')
                        {
                            //Finding a decimal separator here is considered an error because it wouldn't
                            //be too logical (300 digits before the decimal separator!). Mainly by bearing
                            //in mind the exponential alternative above.
                            return errorInfo;
                        }
                        UnitInfo outInfo = ConvertDoubleToDecimal(startNumber);
                        outInfo.BaseTenExponent += GetBeyondDoubleCharacterCount
                        (
                            stringToParse.Substring(299)
                        );
                        return outInfo;
                    }
                }
            }

            return errorInfo;
        }

        private static int GetBeyondDoubleCharacterCount(string remString)
        {
            int outCount = 0;

            try
            {
                foreach (char item in remString.ToCharArray())
                {
                    if (!char.IsDigit(item) || item == '.')
                    {
                        //It would mean that it isn't a valid 
                        return 0;
                    }
                    outCount = outCount + 1;
                }
            }
            catch
            {
                //The really unlikely scenario of hitting int.MaxValue.
                outCount = 0;
            }

            return outCount;
        }

        private static UnitInfo ConvertDoubleToDecimal(double valueDouble)
        {
            if (valueDouble == 0.0) return new UnitInfo();

            UnitInfo outInfo = new UnitInfo();
            if (Math.Abs(valueDouble) < MinValue)
            {
                while (Math.Abs(valueDouble) < MinValue)
                {
                    outInfo.BaseTenExponent = outInfo.BaseTenExponent - 1;
                    valueDouble = valueDouble * 10.0;
                }
            }
            else if (Math.Abs(valueDouble) > MaxValue)
            {
                while (Math.Abs(valueDouble) > MaxValue)
                {
                    outInfo.BaseTenExponent = outInfo.BaseTenExponent + 1;
                    valueDouble = valueDouble / 10.0;
                }
            }

            outInfo.Value = Convert.ToDecimal(valueDouble);

            //Numbers always rely on ExceptionHandlingTypes.AlwaysTriggerException.
            outInfo.Error = new ErrorInfo
            (
                ErrorTypes.None, ExceptionHandlingTypes.AlwaysTriggerException
            );

            return outInfo;
        }

        private static UnitInfo RaiseToIntegerExponent(decimal baseValue, int exponent)
        {
            return RaiseToIntegerExponent(new UnitInfo(baseValue), exponent);
        }

        private static UnitInfo RaiseToIntegerExponent(UnitInfo baseInfo, int exponent)
        {
            if (exponent <= 1 && exponent >= 0)
            {
                baseInfo.Value = (exponent == 0 ? 1m : baseInfo.Value);
                return baseInfo;
            }

            UnitInfo outInfo = new UnitInfo(baseInfo);

            for (int i = 1; i < Math.Abs(exponent); i++)
            {
                outInfo = PerformManagedOperationValues
                (
                    outInfo, baseInfo,  Operations.Multiplication
                );
                if (outInfo.Error.Type != ErrorTypes.None) return outInfo;
            }

            return
            (
                exponent < 0 ? 
                PerformManagedOperationValues(new UnitInfo(1m), outInfo, Operations.Division) :
                outInfo
            );
        }
    }
}
