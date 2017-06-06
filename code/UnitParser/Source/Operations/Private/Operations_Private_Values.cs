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
            //The first operand (the one defining exception handling) is a number and that's why exceptions have to be left unhandled.
            UnitP second2 = new UnitP(second, ErrorTypes.None, ExceptionHandlingTypes.AlwaysTriggerException);
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
            UnitP second2 = new UnitP(second, ErrorTypes.None, ExceptionHandlingTypes.AlwaysTriggerException);
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

                        outInfo = StartCompoundAnalysis
                        (
                            new ParseInfo
                            (
                                InversePrefix(InverseUnit(outInfo))
                            )
                        )
                        .UnitInfo;
                    }
                }
            }

            return
            (
                outInfo.Error.Type != ErrorTypes.None ? new UnitP(unitP, outInfo.Error.Type) :
                new UnitP
                (
                    outInfo, unitP, operationString,
                    (
                        //Multiplication/division are likely to provoke situations requiring a correction;
                        //for example, 1/(1/60) being converted into 60. On the other hand, cases like 
                        //1.0 - 0.000001 shouldn't be changed (e.g., converting 0.999999 to 1.0 is wrong).
                        operation == Operations.Multiplication ||
                        operation == Operations.Division
                    )
                )
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
    }
}
