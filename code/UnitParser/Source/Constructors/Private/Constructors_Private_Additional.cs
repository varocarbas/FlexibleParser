using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    public partial class UnitP
    {
        private UnitInfo ParseValueAndUnit(string valueAndUnit)
        {
            UnitInfo unitInfo = new UnitInfo();
            string[] parts = valueAndUnit.Trim().Split(' ');

            //Note that ParseDecimal can deal with any number (i.e., decimal, double or beyond double).
            if (parts.Length >= 2)
            {
                unitInfo = ParseDecimal(parts[0]);

                if (unitInfo.Error.Type == ErrorTypes.None)
                {
                    unitInfo.TempString = String.Join(" ", parts, 1, parts.Length - 1);
                }
            }
            else if (parts.Length == 1)
            {
                unitInfo = ParseDecimal(valueAndUnit);

                if (unitInfo.Error.Type == ErrorTypes.None)
                {
                    unitInfo.TempString = "Unitless";
                }
            }

            return 
            (
                unitInfo.Error.Type == ErrorTypes.None ? unitInfo :
                ParseValueAndUnitNoBlank(valueAndUnit)
            );
        }

        private UnitInfo ParseValueAndUnitNoBlank(string valueAndUnit)
        {
            string valueString = string.Join
            (
                "", valueAndUnit.Trim().ToLower().TakeWhile
                (
                    x => char.IsDigit(x) || x == 'e' || 
                    x == '-' || x == '+' || x == '.' || x == ','
                )
            );
            UnitInfo unitInfo = ParseDecimal(valueString);

            if (unitInfo.Error.Type == ErrorTypes.None)
            {
                unitInfo.TempString = valueAndUnit.Replace(valueString, "");
            }

            return unitInfo;
        }

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
            ParseInfo parseInfo = 
            (
                unitInfo.Error.Type != ErrorTypes.None ?
                new ParseInfo(unitInfo) : ParseInputs
                (
                    new ParseInfo(unitInfo, unitString)
                )
            );

            if (parseInfo.UnitInfo.Error.Type == ErrorTypes.None && parseInfo.UnitInfo.Unit == Units.None)
            {
                parseInfo.UnitInfo.Error = new ErrorInfo(ErrorTypes.InvalidUnit);
            }

            return new UnitPConstructor
            (
                unitString, parseInfo.UnitInfo, parseInfo.UnitInfo.Type, 
                parseInfo.UnitInfo.System, parseInfo.UnitInfo.Error.Type, 
                unitInfo.Error.ExceptionHandling, false, 
                (unitInfo.Value != parseInfo.UnitInfo.Value)
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

        //Class helping to deal with the relevant number of constructors including quite a few readonly variables.
        private class UnitPConstructor
        {
            public decimal Value;
            public string OriginalUnitString, UnitString, ValueAndUnitString;
            public UnitTypes UnitType;
            public UnitSystems UnitSystem;
            public UnitInfo UnitInfo;
            public ErrorTypes ErrorType;
            public ExceptionHandlingTypes ExceptionHandling;

            public UnitPConstructor(string originalUnitString, UnitInfo unitInfo) : this
            (
                originalUnitString, unitInfo, UnitTypes.None, 
                UnitSystems.None, unitInfo.Error.Type 
            )
            { }

            public UnitPConstructor
            (
                string originalUnitString, UnitInfo unitInfo, UnitTypes unitType, 
                UnitSystems unitSystem = UnitSystems.None, ErrorTypes errorType = ErrorTypes.None,
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
                        //Values like 1.999999 are assumed to be a not-that-good version of 2.0 + some precision loss.
                        //This assumption doesn't hold every time (e.g., input value which wasn't part of any operation).
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
