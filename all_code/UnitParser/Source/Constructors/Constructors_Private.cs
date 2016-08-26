using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    public partial class UnitP
    {
        private UnitP
        (
            ParseInfo parseInfo, string originalUnitString = "", UnitSystems system = UnitSystems.None,
            ExceptionHandlingTypes exceptionHandling = ExceptionHandlingTypes.NeverTriggerException,
            PrefixUsageTypes prefixUsage = PrefixUsageTypes.DefaultUsage, bool noPrefixImprovement = false
        )
        {
            UnitPConstructor unitP2 = new UnitPConstructor
            (
                originalUnitString, parseInfo.UnitInfo, parseInfo.UnitInfo.Type, parseInfo.UnitInfo.System,
                ErrorTypes.None, exceptionHandling, noPrefixImprovement
            );

            OriginalUnitString = unitP2.OriginalUnitString;
            Value = unitP2.Value;
            BaseTenExponent = unitP2.UnitInfo.BaseTenExponent;
            Unit = unitP2.UnitInfo.Unit;
            UnitType = unitP2.UnitType;
            UnitSystem =
            (
                system != UnitSystems.None ?
                system : //After some operations, the system cannot be easily determined from the unit.
                unitP2.UnitSystem
            );
            UnitPrefix = new Prefix(unitP2.UnitInfo.Prefix);
            UnitParts = unitP2.UnitInfo.Parts.AsReadOnly();
            UnitString = unitP2.UnitString;
            ValueAndUnitString = unitP2.ValueAndUnitString;
            //If applicable, this instantiation would trigger an exception right away.
            Error = new ErrorInfo(unitP2.ErrorType, unitP2.ExceptionHandling);
        }

        private UnitP
        (
            UnitInfo unitInfo, UnitP unitP, bool noPrefixImprovement
        )
        : this
        (
            new ParseInfo(unitInfo), "", UnitSystems.None,
            ExceptionHandlingTypes.NeverTriggerException, unitInfo.Prefix.PrefixUsage,
            noPrefixImprovement
        )
        { }

        private UnitP
        (
            UnitInfo unitInfo, UnitP unitP, string originalUnitString = "", 
            UnitSystems system = UnitSystems.None, bool noPrefixImprovement = false
        )
        : this
        (
            new ParseInfo(unitInfo), originalUnitString, system,
            unitP.Error.ExceptionHandling, unitInfo.Prefix.PrefixUsage,
            noPrefixImprovement
        )
        { }

        private UnitP
        (
            UnitInfo unitInfo, string originalUnitString = "", 
            ExceptionHandlingTypes exceptionHandling = ExceptionHandlingTypes.NeverTriggerException
        ) 
        : this(new ParseInfo(unitInfo), originalUnitString, UnitSystems.None, exceptionHandling) { }

        private UnitP
        (
            UnitP unitP, ErrorTypes errorType,
            ExceptionHandlingTypes exceptionHandling = ExceptionHandlingTypes.NeverTriggerException
        )
        {
            if (unitP == null) unitP = new UnitP(new UnitInfo());

            UnitPConstructor unitP2 = new UnitPConstructor
            (
                unitP.OriginalUnitString, new UnitInfo(unitP),
                UnitTypes.None, UnitSystems.None, errorType,
                (
                    exceptionHandling != ExceptionHandlingTypes.NeverTriggerException ?
                    exceptionHandling : unitP.Error.ExceptionHandling
                )
            );

            if (unitP2.ErrorType != ErrorTypes.None)
            {
                Value = 0m;
                BaseTenExponent = 0;
                UnitPrefix = new Prefix();
                UnitParts = new List<UnitPart>().AsReadOnly();
            }
            else
            {
                OriginalUnitString = unitP2.OriginalUnitString;
                Value = unitP2.Value;
                BaseTenExponent = unitP2.UnitInfo.BaseTenExponent;
                Unit = unitP2.UnitInfo.Unit;
                UnitType = unitP2.UnitType;
                UnitSystem = unitP2.UnitSystem;
                UnitPrefix = new Prefix(unitP2.UnitInfo.Prefix);
                UnitParts = unitP2.UnitInfo.Parts.AsReadOnly();
                UnitString = unitP2.UnitString;
                ValueAndUnitString = unitP2.ValueAndUnitString;
            }

            //If applicable, this instantiation would trigger an exception right away.
            Error = new ErrorInfo(unitP2.ErrorType, unitP2.ExceptionHandling);
        }

        private UnitP(UnitP unitP, ExceptionHandlingTypes exceptionHandling) : this
        (
            unitP, ErrorTypes.None, exceptionHandling
        ) 
        { }

        private UnitPConstructor GetUnitP2
        (
            decimal value, string unitString,
            ExceptionHandlingTypes exceptionHandling = ExceptionHandlingTypes.NeverTriggerException, 
            PrefixUsageTypes prefixUsage = PrefixUsageTypes.DefaultUsage
        )
        {
            ParseInfo parseInfo = ParseInputs
            (
                value, unitString, prefixUsage
            );

            return new UnitPConstructor
            (
                unitString, parseInfo.UnitInfo, parseInfo.UnitInfo.Type, parseInfo.UnitInfo.System,
                (
                    parseInfo.UnitInfo.Unit == Units.None ?
                    ErrorTypes.InvalidUnit : ErrorTypes.None 
                ), 
                exceptionHandling
            );
        }

        private ParseInfo ParseInputs(decimal value, string unitString, PrefixUsageTypes prefixUsage)
        {
            ParseInfo parseInfo = new ParseInfo(value, unitString, prefixUsage);
            parseInfo = StartUnitParse(parseInfo);
            bool isOK = 
            (
                parseInfo.UnitInfo.Error.Type != ErrorTypes.None &&
                parseInfo.UnitInfo.Unit != Units.None
            );

            if (!isOK && unitString.Contains(" "))
            {
                //No intermediate spaces (within the unit) should be expected,
                //but well...
                ParseInfo parseInfo2 = new ParseInfo
                (
                    parseInfo,
                    string.Join("", unitString.Split(' ').Select(x => x.Trim()))
                );
                parseInfo2.UnitInfo.Error = new ErrorInfo();
                parseInfo2 = StartUnitParse(parseInfo2);

                if (parseInfo2.UnitInfo.Unit != Units.None)
                {
                    parseInfo = new ParseInfo(parseInfo2);
                }
            }

            return parseInfo;
        }

        //Class helping to deal with various constructors including so many readonly variables.
        private class UnitPConstructor
        {
            public decimal Value;
            public string OriginalUnitString, UnitString, ValueAndUnitString;
            public UnitTypes UnitType;
            public UnitSystems UnitSystem;
            public UnitInfo UnitInfo;
            public ErrorTypes ErrorType;
            public ExceptionHandlingTypes ExceptionHandling;

            public UnitPConstructor
            (
                string originalUnitString, UnitInfo unitInfo,
                UnitTypes unitType = UnitTypes.None, UnitSystems unitSystem = UnitSystems.None,
                ErrorTypes errorType = ErrorTypes.None,
                ExceptionHandlingTypes exceptionHandling = ExceptionHandlingTypes.NeverTriggerException,
                bool noPrefixImprovement = false
            )
            {

                if (originalUnitString == null || originalUnitString.Trim().Length < 1)
                {
                    originalUnitString = (UnitString != null ? UnitString : "");
                }
                OriginalUnitString = originalUnitString.Trim();
                ErrorType = errorType;
                ExceptionHandling = exceptionHandling;

                if (errorType != ErrorTypes.None)
                {
                    UnitInfo = new UnitInfo();
                }
                else
                {
                    UnitInfo = ImproveUnitInfo(unitInfo, noPrefixImprovement);
                    UnitType = 
                    (
                        UnitInfo.Type != UnitTypes.None ? UnitInfo.Type :
                        GetTypeFromUnitInfo(UnitInfo)
                    );
                    UnitSystem = 
                    (
                        UnitInfo.System != UnitSystems.None && UnitInfo.System != UnitSystems.Imperial ?
                        UnitInfo.System : GetSystemFromUnit(UnitInfo.Unit, false, true)
                    );
                    if (UnitSystem == UnitSystems.Imperial && UnitInfo.Unit == Units.ValidImperialUSCSUnit)
                    {
                        UnitInfo.Unit = Units.ValidImperialUnit;
                    }
                    UnitString = GetUnitString(UnitInfo);

                    Value = UnitInfo.Value;

                    ValueAndUnitString = Value +
                    (
                        UnitInfo.BaseTenExponent != 0 ?
                        "*10^" + UnitInfo.BaseTenExponent.ToString() : ""
                    )
                    + " " + UnitString;
                }
            }

            private static UnitInfo ImproveUnitInfo(UnitInfo unitInfo, bool noPrefixImprovement)
            {
                if (unitInfo.Parts.Count == 0)
                {
                    if (unitInfo.Prefix.Factor != 1m)
                    {
                        unitInfo = NormaliseUnitInfo(unitInfo);
                    }

                    unitInfo.Unit = Units.Unitless;
                    unitInfo.Prefix = new Prefix(1m, unitInfo.Prefix.PrefixUsage);
                }
                else if (Math.Abs(unitInfo.Value) < 1 && unitInfo.Prefix.Factor > 1)
                {
                    unitInfo.Value = unitInfo.Value * unitInfo.Prefix.Factor;
                    unitInfo.Prefix = new Prefix(unitInfo.Prefix.PrefixUsage);
                }

                if (!noPrefixImprovement)
                {
                    unitInfo = ImprovePrefixes(unitInfo);
                }

                return ReduceBigValueExp(unitInfo);
            }

            private static UnitInfo ReduceBigValueExp(UnitInfo unitInfo)
            {
                if (unitInfo.BaseTenExponent == 0) return unitInfo;

                decimal maxVal = 1000000m;
                decimal minVal = 0.0001m;

                if (unitInfo.BaseTenExponent > 0)
                {
                    while (unitInfo.BaseTenExponent > 0 && unitInfo.Value <= maxVal / 10)
                    {
                        unitInfo.BaseTenExponent -= 1;
                        unitInfo.Value *= 10;
                    }
                }
                else
                {
                    while (unitInfo.BaseTenExponent < 0 && unitInfo.Value >= minVal * 10)
                    {
                        unitInfo.BaseTenExponent += 1;
                        unitInfo.Value /= 10;
                    }
                }

                return unitInfo;
            }

            private static UnitInfo ImprovePrefixes(UnitInfo unitInfo)
            {
                decimal absValue = Math.Abs(unitInfo.Value);
                bool valueIsOK = (absValue >= 0.001m && absValue <= 1000m);                     

                if (valueIsOK && unitInfo.BaseTenExponent == 0 && unitInfo.Prefix.Factor == 1m)
                {
                    return unitInfo;
                }

                PrefixTypes prefixType = 
                (
                    unitInfo.Prefix.Type != PrefixTypes.None ? 
                    unitInfo.Prefix.Type : PrefixTypes.SI   
                );

                bool prefixIsOK =
                (
                    !PrefixCanBeUsedBasic(unitInfo.Unit, prefixType, unitInfo.Prefix.PrefixUsage) ?
                    false : PrefixCanBeUsedCompound(unitInfo)
                );

                if (!prefixIsOK || !valueIsOK || unitInfo.BaseTenExponent != 0)
                {
                    unitInfo = NormaliseUnitInfo(unitInfo);

                    if (prefixIsOK)
                    {
                        unitInfo = GetBestPrefixForTarget
                        (
                            unitInfo, unitInfo.BaseTenExponent, 
                            prefixType, true
                        );
                    }
                }

                return CompensateBaseTenExponentWithPrefix(unitInfo);
            }

            private static UnitInfo CompensateBaseTenExponentWithPrefix(UnitInfo unitInfo)
            {
                if (unitInfo.BaseTenExponent == 0 || unitInfo.Prefix.Factor == 1m) return unitInfo;

                UnitInfo tempInfo = NormaliseUnitInfo
                (
                    new UnitInfo(unitInfo) { Value = 1m }
                );

                tempInfo = GetBestPrefixForTarget
                (
                    tempInfo, tempInfo.BaseTenExponent,
                    unitInfo.Prefix.Type, true
                );

                unitInfo = new UnitInfo(unitInfo)
                {
                    BaseTenExponent = tempInfo.BaseTenExponent,
                    Prefix = new Prefix(tempInfo.Prefix),
                    Value = unitInfo.Value
                };

                return PerformManagedOperationValues
                (
                    unitInfo, tempInfo = new UnitInfo(tempInfo) 
                    { 
                        BaseTenExponent = 0, Prefix = new Prefix() 
                    },
                    Operations.Multiplication
                );
            }
        }
    }
}
