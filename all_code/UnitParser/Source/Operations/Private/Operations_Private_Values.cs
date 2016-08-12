using System;
using System.Collections.Generic;

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
                            new ParsedUnit
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
                newPrefix, newPrefix.BigNumberExponent,
                (
                    outInfo.Prefix.Type != PrefixTypes.None ?
                    outInfo.Prefix.Type : PrefixTypes.SI
                )
            );

            return PerformManagedOperationUnits
            (
                outInfo, newPrefix, Operations.Multiplication
            );
        }

        private static UnitInfo ConvertDoubleToDecimal(double doubleVal)
        {
            if (doubleVal == 0.0) return new UnitInfo();

            UnitInfo outInfo = new UnitInfo();
            if (Math.Abs(doubleVal) < MinValue)
            {
                while (Math.Abs(doubleVal) < MinValue)
                {
                    outInfo.BigNumberExponent = outInfo.BigNumberExponent - 1;
                    doubleVal = doubleVal * 10.0;
                }
            }
            else if (Math.Abs(doubleVal) > MaxValue)
            {
                while (Math.Abs(doubleVal) > MaxValue)
                {
                    outInfo.BigNumberExponent = outInfo.BigNumberExponent + 1;
                    doubleVal = doubleVal / 10.0;
                }
            }

            outInfo.Value = Convert.ToDecimal(doubleVal);

            //By default, numbers have a AlwaysTriggerException configuration.
            outInfo.Error = new ErrorInfo
            (
                ErrorTypes.None,
                ExceptionHandlingTypes.AlwaysTriggerException
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
