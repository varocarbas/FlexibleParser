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
                system : unitP2.UnitSystem
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
            UnitP unitP, ErrorTypes errorType,
            ExceptionHandlingTypes exceptionHandling = ExceptionHandlingTypes.NeverTriggerException
        )
        {
            if (unitP == null) unitP = new UnitP();

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
    }
}
