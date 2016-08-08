﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FlexibleParser
{
    public partial class UnitP
    {
        private static ParsedUnit StartUnitParse(ParsedUnit parsedUnit)
        {
            parsedUnit = InitialParseActions(parsedUnit);

            return
            (
                StringCanBeCompound(parsedUnit.InputToParse) ?
                StartCompoundParse(parsedUnit) :
                StartIndividualUnitParse(parsedUnit)
            );
        }

        private static ParsedUnit StartIndividualUnitParse(ParsedUnit parsedUnit)
        {
            return ImproveIndividualUnitPart
            (
                ParseIndividualUnit(parsedUnit)
            );
        }

        private static ParsedUnit ParseIndividualUnit(ParsedUnit parsedUnit)
        {
            parsedUnit = PrefixAnalysis(parsedUnit);

            if (parsedUnit.UnitInfo.Unit == Units.None)
            {
                //The final unit might have already been found while analysing prefixes.
                parsedUnit.UnitInfo.Unit = GetUnitFromString(parsedUnit.InputToParse);
            }

            parsedUnit.UnitInfo = UpdateMainUnitVariables(parsedUnit.UnitInfo);

            return parsedUnit;
        }

        private static UnitInfo UpdateMainUnitVariables(UnitInfo unitInfo)
        {
            if (unitInfo.Unit == Units.None || unitInfo.Unit == Units.Unitless)
            {
                return unitInfo;
            }

            if (unitInfo.Type == UnitTypes.None)
            {
                unitInfo = GetTypeFromUnitInfo(unitInfo);
            }

            if (unitInfo.System == UnitSystems.None)
            {
                unitInfo = GetSystemFromUnitInfo(unitInfo);
            }

            return unitInfo;
        }

        //Stores all the information which might be required at any point while performing
        //parsing actions.
        private class ParsedUnit
        {
            public UnitInfo UnitInfo { get; set; }
            public UnitSystems System { get; set; }
            public UnitTypes Type { get; set; }        
            public string InputToParse { get; set; }
            public int Exponent { get; set; }
            //This ValidCompound isn't used when parsing individual units.
            public StringBuilder ValidCompound { get; set; }

            public ParsedUnit() : this(new UnitInfo()) { }

            public ParsedUnit(UnitInfo unitInfo)
            { 
                UnitInfo = new UnitInfo(unitInfo); 
            }
         
            public ParsedUnit(ParsedUnit parsedUnit, string inputToParse = "")
            {
                UnitInfo = new UnitInfo(parsedUnit.UnitInfo);
                System = parsedUnit.System;
                Exponent = parsedUnit.Exponent;
                InputToParse = 
                (
                    inputToParse != "" ? 
                    inputToParse : 
                    parsedUnit.InputToParse
                );
            }

            public ParsedUnit
            (
                decimal value, string inputToParse,  
                PrefixUsageTypes prefixUsage = PrefixUsageTypes.DefaultUsage,
                int exponent = 1
            )
            {
                UnitInfo = new UnitInfo
                (
                    value, Units.None, new Prefix(1, prefixUsage)
                );
                InputToParse = inputToParse;
                Exponent = exponent;
            }
        }
    }
}