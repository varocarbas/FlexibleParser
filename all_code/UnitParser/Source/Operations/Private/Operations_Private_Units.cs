using System;
using System.Collections.Generic;

namespace FlexibleParser
{
    public partial class UnitP
    {
        private static UnitP PerformUnitOperation(UnitP first, UnitP second, Operations operation, string operationString)
        {
            ErrorTypes errorType = GetUnitOperationError(first, second, operation);
            if (errorType != ErrorTypes.None)
            {
                return new UnitP(first, errorType);
            }
            
            UnitInfo outInfo = new UnitInfo(first);
            UnitInfo secondInfo = new UnitInfo(second);

            bool unitlessDone = false;
            if (outInfo.Unit != Units.Unitless && secondInfo.Unit != Units.Unitless)
            {
                unitlessDone = true;
                if (operation == Operations.Addition || operation == Operations.Subtraction)
                {
                    UnitInfo[] tempInfos = PerformChecksBeforeAddition(outInfo, secondInfo);

                    foreach (UnitInfo tempInfo in tempInfos)
                    {
                        if (tempInfo.Error.Type != ErrorTypes.None)
                        {
                            return new UnitP(first, tempInfo.Error.Type);
                        }
                    }
                    //Only the second operator might have been modified.
                    secondInfo = tempInfos[1];
                }
                else outInfo = ModifyUnitPartsBeforeMultiplication(first, secondInfo, operation);
            }
            else if (outInfo.Unit == Units.Unitless && secondInfo.Unit != Units.Unitless)
            {
                outInfo = new UnitInfo(secondInfo) 
                { 
                    Value = outInfo.Value,
                    Prefix = new Prefix(outInfo.Prefix),
                    BaseTenExponent = outInfo.BaseTenExponent
                };
            }

            if (outInfo.Error.Type != ErrorTypes.None || outInfo.Unit == Units.None)
            {
                return new UnitP
                (
                    first,
                    (
                        outInfo.Error.Type != ErrorTypes.None ?
                        outInfo.Error.Type : ErrorTypes.InvalidUnit
                    )
                );
            }

            if (outInfo.Unit != Units.Unitless || !unitlessDone)
            {
                outInfo = PerformManagedOperationUnits(outInfo, secondInfo, operation);
            }

            return 
            (
                outInfo.Error.Type != ErrorTypes.None ?
                new UnitP(first, outInfo.Error.Type) :
                new UnitP(outInfo, first, operationString, false)
            );
        }

        private static UnitInfo[] PerformChecksBeforeAddition(UnitInfo outInfo, UnitInfo secondInfo)
        {
            UnitInfo[] outInfos = new UnitInfo[] { outInfo, secondInfo };

            if (outInfo.Type != secondInfo.Type)
            {
                outInfos[0].Error = new ErrorInfo(ErrorTypes.InvalidUnit);
            }
            else if (outInfo.Unit != secondInfo.Unit || IsUnnamedUnit(outInfo.Unit))
            {
                outInfos[1] = ConvertUnit(secondInfo, outInfo, false);
            }

            return outInfos;
        }

        private static UnitInfo ModifyUnitPartsBeforeMultiplication(UnitP first, UnitInfo secondInfo, Operations operation)
        {
            UnitInfo outInfo = new UnitInfo(first);
            List<UnitPart> parts2 = new List<UnitPart>();
            int sign = (operation == Operations.Multiplication ? 1 : -1);

            foreach (UnitPart part in secondInfo.Parts)
            {
                parts2.Add
                (
                    new UnitPart(part) 
                    { 
                        Exponent = part.Exponent * sign 
                    }
                );
            }

            outInfo = AddNewUnitParts(outInfo, parts2);

            return StartCompoundAnalysis
            (
                new ParseInfo(outInfo)
            )
            .UnitInfo;
        }
    }
}