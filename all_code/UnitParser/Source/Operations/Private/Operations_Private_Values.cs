using System;
using System.Collections.Generic;
using System.Globalization;

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
            //The first operand (the one defining how errors should be handled) is a number and that's why exceptions have
            //to be let unhandled.
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
            //The first operand (the one defining how errors should be handled) is a number and that's why exceptions have
            //to be let unhandled.
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
                new UnitP(outInfo, unitP, operationString)
            );
        }

        private static ErrorTypes GetUnitValueOperationError(UnitP unitP, UnitInfo firstInfo, UnitInfo secondInfo, Operations operation)
        {
            ErrorTypes outError = GetOperationError
            (
                new UnitInfo[] { firstInfo, secondInfo }, operation
            );

            if (outError == ErrorTypes.None && unitP.Unit == Units.None)
            {
                outError = ErrorTypes.InvalidUnit;
            }
            else if (operation == Operations.Division && secondInfo.Value == 0m)
            {
                outError = ErrorTypes.NumericError;
            }

            return outError;
        }

        private static UnitInfo InversePrefix(UnitInfo outInfo)
        {
            if (outInfo.Prefix.Factor == 1m) return outInfo;

            UnitInfo newPrefix = NormaliseUnitInfo
            (
                PerformManagedOperationValues
                (
                    1m, outInfo.Prefix.Factor, Operations.Division
                )
            );

            newPrefix = GetBestPrefixForTarget
            (
                newPrefix, newPrefix.BaseTenExponent,
                (
                    outInfo.Prefix.Type != PrefixTypes.None ?
                    outInfo.Prefix.Type : PrefixTypes.SI
                )
            );

            return outInfo * newPrefix;
        }

        private static UnitInfo ParseDecimal(string stringToParse)
        {
            decimal value = 0m;
            
            if (decimal.TryParse(stringToParse, NumberStyles.Any, CultureInfo.InvariantCulture, out value))
            {
                //In some cases, decimal.TryParse might consider valid numbers beyond the actual scope of
                //decimal type. For example: 0.00000000000000000000000000000001m assumed to be zero.
                if (value != 0m) return new UnitInfo(value);
            }

            return ParseDouble(stringToParse);
        }

        private static UnitInfo ParseDouble(string stringToParse)
        {
            double valueDouble = 0.0;

            return
            (
                double.TryParse(stringToParse, NumberStyles.Any, CultureInfo.InvariantCulture, out valueDouble) ?
                ConvertDoubleToDecimal(valueDouble) : new UnitInfo(null, ErrorTypes.NumericParsingError)
            );
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

            //By default, numbers have a AlwaysTriggerException configuration.
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
