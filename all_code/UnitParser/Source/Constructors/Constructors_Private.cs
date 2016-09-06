using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    public partial class UnitP
    {
        private UnitP(UnitP unitP, decimal value, int baseTenExponent)
        {
            Value = value;
            BaseTenExponent = baseTenExponent;
            Unit = unitP.Unit;
            UnitType = unitP.UnitType;
            UnitSystem = unitP.UnitSystem;
            UnitPrefix = new Prefix(unitP.UnitPrefix);
            UnitParts = new List<UnitPart>(unitP.UnitParts.ToList()).AsReadOnly();
            UnitString = unitP.UnitString;
            OriginalUnitString = unitP.Value.ToString() +
            (
                unitP.BaseTenExponent != 0 ?
                "*10^" + unitP.BaseTenExponent.ToString() : ""
            );
            ValueAndUnitString = Value.ToString() +
            (
                BaseTenExponent != 0 ?
                "*10^" + BaseTenExponent.ToString() : ""
            ) + " " + UnitString;
            Error = new ErrorInfo(unitP.Error);
        }

        private UnitP
        (
            ParseInfo parseInfo, string originalUnitString = "", UnitSystems system = UnitSystems.None,
            ExceptionHandlingTypes exceptionHandling = ExceptionHandlingTypes.NeverTriggerException,
            PrefixUsageTypes prefixUsage = PrefixUsageTypes.DefaultUsage, bool noPrefixImprovement = false,
            bool improveFinalValue = true
        )
        {
            UnitPConstructor unitP2 = new UnitPConstructor
            (
                originalUnitString, parseInfo.UnitInfo, parseInfo.UnitInfo.Type, parseInfo.UnitInfo.System,
                ErrorTypes.None, exceptionHandling, noPrefixImprovement, improveFinalValue
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

        private UnitP(UnitInfo unitInfo, UnitP unitP, bool noPrefixImprovement) : this
        (
            new ParseInfo(unitInfo), unitP.OriginalUnitString, unitP.UnitSystem,
            unitP.Error.ExceptionHandling, unitP.UnitPrefix.PrefixUsage,
            noPrefixImprovement
        )
        { }

        private UnitP
        (
            UnitInfo unitInfo, UnitP unitP, string originalUnitString = "",
            bool improveFinalValue = true
        )
        : this
        (
            new ParseInfo(unitInfo), originalUnitString, UnitSystems.None,
            unitP.Error.ExceptionHandling, unitInfo.Prefix.PrefixUsage, 
            false, improveFinalValue
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
                UnitPrefix = new Prefix(unitP2.UnitInfo.Prefix.PrefixUsage);
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
            return GetUnitP2
            (
                new UnitInfo(value, exceptionHandling, prefixUsage), unitString
            );
        }

        private UnitPConstructor GetUnitP2(UnitInfo unitInfo, string unitString)
        {
            ParseInfo parseInfo = ParseInputs
            (
                new ParseInfo(unitInfo, unitString)
            );

            return new UnitPConstructor
            (
                unitString, parseInfo.UnitInfo, parseInfo.UnitInfo.Type, parseInfo.UnitInfo.System,
                (
                    parseInfo.UnitInfo.Unit == Units.None ?
                    ErrorTypes.InvalidUnit : ErrorTypes.None 
                ), 
                unitInfo.Error.ExceptionHandling, false, (unitInfo.Value != parseInfo.UnitInfo.Value)
            );
        }

        private ParseInfo ParseInputs(ParseInfo parseInfo)
        {
            parseInfo = StartUnitParse(parseInfo);
            bool isOK = 
            (
                parseInfo.UnitInfo.Error.Type == ErrorTypes.None &&
                parseInfo.UnitInfo.Unit != Units.None
            );

            if (!isOK && parseInfo.InputToParse.Contains(" "))
            {
                //No intermediate spaces (within the unit) should be expected,
                //but well...
                ParseInfo parseInfo2 = new ParseInfo
                (
                    parseInfo,
                    string.Join("", parseInfo.InputToParse.Split(' ').Select(x => x.Trim()))
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

        //Class helping to deal with such a big number of constructors including so many readonly variables.
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
                bool noPrefixImprovement = false, bool improveFinalValue = true
            )
            {
                OriginalUnitString =
                (
                    originalUnitString == null ? "" :
                    originalUnitString.Trim()
                );
                ErrorType = errorType;
                ExceptionHandling = exceptionHandling;

                if (ErrorType != ErrorTypes.None)
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

                    Value =
                    (
                        improveFinalValue ? 
                        //Values like 1.999999 are assumed to be a not-that-good version of 2.0 (+ precision loss).
                        //This assumption doesn't hold with operations performed by the user (who might want that result).
                        ImproveFinalValue(UnitInfo.Value) :
                        UnitInfo.Value
                    );

                    ValueAndUnitString = Value.ToString() +
                    (
                        UnitInfo.BaseTenExponent != 0 ?
                        "*10^" + UnitInfo.BaseTenExponent.ToString() : ""
                    )
                    + " " + UnitString;
                }
            }
        }
    }
}
