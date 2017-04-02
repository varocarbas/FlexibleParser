using System;
using System.Collections.Generic;
using System.Text;

namespace FlexibleParser
{
    public partial class UnitP
    {
        private static ParseInfo StartUnitParse(ParseInfo parseInfo)
        {
            parseInfo = InitialParseActions(parseInfo);

            return
            (
                StringCanBeCompound(parseInfo.InputToParse) ?
                StartCompoundParse(parseInfo) :
                StartIndividualUnitParse(parseInfo)
            );
        }

        private static ParseInfo InitialParseActions(ParseInfo parseInfo)
        {
            if (parseInfo.InputToParse == null) parseInfo.InputToParse = "";
            parseInfo.InputToParse = parseInfo.InputToParse.Trim();

            if (parseInfo.InputToParse.Length < 1)
            {
                if (parseInfo.UnitInfo.Error.Type == ErrorTypes.None)
                {
                    parseInfo.UnitInfo.Error = new ErrorInfo(ErrorTypes.InvalidUnit);
                }

                return parseInfo;
            }

            foreach (string ignored in UnitP.UnitParseIgnored)
            {
                parseInfo.InputToParse = parseInfo.InputToParse.Replace(ignored, "");
            }

            parseInfo.UnitInfo = RemoveAllUnitInformation(parseInfo.UnitInfo, true);

            return parseInfo;
        }

        private static UnitInfo PopulateUnitRelatedInfo(UnitInfo unitInfo, Units unit)
        {
            unitInfo.Unit = unit;

            if (unitInfo.Unit != Units.None && unitInfo.Unit != Units.Unitless && !IsUnnamedUnit(unitInfo.Unit))
            {
                unitInfo.Type = AllUnitTypes[unitInfo.Unit];
                unitInfo.System = AllUnitSystems[unitInfo.Unit];
            }

            return unitInfo;
        }

        private static UnitInfo UpdateMainUnitVariables(UnitInfo unitInfo, bool recalculateAlways = false)
        {
            if (unitInfo.Unit == Units.None || unitInfo.Unit == Units.Unitless)
            {
                return unitInfo;
            }

            if (recalculateAlways)
            {
                unitInfo.Type = UnitTypes.None;
                unitInfo.System = UnitSystems.None;
            }

            if (unitInfo.Type == UnitTypes.None)
            {
                unitInfo.Type = GetTypeFromUnitInfo(unitInfo);
            }

            if (unitInfo.System == UnitSystems.None)
            {
                unitInfo.System = GetSystemFromUnitInfo(unitInfo);
            }

            return unitInfo;
        }

        //Stores all the information which might be required at any point while performing
        //parsing actions.
        private class ParseInfo
        {
            public UnitInfo UnitInfo { get; set; }   
            public string InputToParse { get; set; }
            //ValidCompound isn't used when parsing individual units.
            public StringBuilder ValidCompound { get; set; }

            public ParseInfo() : this(null) { }

            public ParseInfo(UnitInfo unitInfo)
            {
                UnitInfo = new UnitInfo(unitInfo);
                InputToParse = "";
            }

            public ParseInfo(UnitInfo unitInfo, string inputToParse = "") :
            this(new ParseInfo(unitInfo), inputToParse) { }
            
            public ParseInfo(ParseInfo parseInfo, string inputToParse = "")
            {
                if (parseInfo == null) parseInfo = new ParseInfo();

                UnitInfo = new UnitInfo(parseInfo.UnitInfo);
                InputToParse = 
                (
                    inputToParse != "" ? inputToParse : 
                    parseInfo.InputToParse
                );
            }

            public ParseInfo
            (
                decimal value, string inputToParse,  
                PrefixUsageTypes prefixUsage = PrefixUsageTypes.DefaultUsage,
                int exponent = 1
            )
            {
                UnitInfo = new UnitInfo
                (
                    value, Units.None, new Prefix(prefixUsage)
                );
                InputToParse = inputToParse;
            }
        }
    }
}