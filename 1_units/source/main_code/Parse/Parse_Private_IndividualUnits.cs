using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    public partial class UnitP
    {
        private static ParsedUnit InitialParseActions(ParsedUnit parsedUnit)
        {
            parsedUnit.InputToParse = parsedUnit.InputToParse.Trim();

            foreach (string symbol in UnitP.IgnoredUnitSymbols)
            {
                parsedUnit.InputToParse = parsedUnit.InputToParse.Replace(symbol, "");
            }

            return parsedUnit;
        }

        private static ParsedUnit PrefixAnalysis(ParsedUnit parsedUnit)
        {
            parsedUnit = BeforePrefixAnalysis(parsedUnit);

            return 
            (
                parsedUnit.UnitInfo.Unit == Units.None ?
                CheckPrefixes(parsedUnit) : parsedUnit
            );
        }

        private static ParsedUnit BeforePrefixAnalysis(ParsedUnit parsedUnit)
        {
            parsedUnit.UnitInfo.Unit = GetUnitFromString(parsedUnit.InputToParse);
            
            if (parsedUnit.UnitInfo.Unit != Units.None)
            {
                if (AllPrefixBeforeUnits.Contains(parsedUnit.UnitInfo.Unit))
                {
                    //The default behaviour is preferring units before prefixes but this
                    //rule has exceptions.
                    parsedUnit.UnitInfo = new UnitInfo(parsedUnit.UnitInfo.Value);
                }
            }

            return parsedUnit;
        }

        private static ParsedUnit CheckPrefixes(ParsedUnit parsedUnit)
        {
            if (parsedUnit.InputToParse.Length < 2) return parsedUnit;

            string origString = parsedUnit.InputToParse;

            parsedUnit.UnitInfo.Prefix = new Prefix
            (
                parsedUnit.UnitInfo.Prefix.PrefixUsage
            );

            ParsedUnit tempSI = CheckSIPrefixes(new ParsedUnit(parsedUnit));
            ParsedUnit tempBinary = CheckBinaryPrefixes(new ParsedUnit(parsedUnit));

            if (tempSI.UnitInfo.Prefix.Factor != 1m)
            {
                if (tempBinary.UnitInfo.Prefix.Factor != 1m)
                {
                    //Both SI and binary prefixes were detected, what is an error.
                    parsedUnit = new ParsedUnit();
                    parsedUnit.InputToParse = origString;
                    parsedUnit.UnitInfo.Error = new ErrorInfo(ErrorTypes.InvalidUnit);
                }
                else parsedUnit = new ParsedUnit(tempSI);
            }
            else if (tempBinary.UnitInfo.Prefix.Factor != 1m)
            {
                parsedUnit = new ParsedUnit(tempBinary);
            }

            return parsedUnit;
        }
        
        private static ParsedUnit CheckBinaryPrefixes(ParsedUnit parsedUnit)
        {
            return CheckPrefixes
            (
                parsedUnit, PrefixTypes.Binary,
                AllBinaryPrefixSymbols, AllBinaryPrefixNames
            );
        }

        private static ParsedUnit CheckSIPrefixes(ParsedUnit parsedUnit)
        {
            return CheckPrefixes
            (
                parsedUnit, PrefixTypes.SI,
                AllSIPrefixSymbols, AllSIPrefixNames
            );
        }
        
        private static ParsedUnit CheckPrefixes(ParsedUnit parsedUnit, PrefixTypes prefixType, Dictionary<string, decimal> allPrefixes, Dictionary<string, string> allPrefixNames)
        {
            string remString = "";
            foreach (var prefix in allPrefixes)
            {
                if (parsedUnit.InputToParse.ToLower().StartsWith(allPrefixNames[prefix.Key]))
                {
                    remString = parsedUnit.InputToParse.Substring(allPrefixNames[prefix.Key].Length);
                }
                else if (parsedUnit.InputToParse.StartsWith(prefix.Key))
                {
                    remString = parsedUnit.InputToParse.Substring(prefix.Key.Length);
                }
                if (remString != "")
                {
                    return AnalysePrefix
                    (
                        parsedUnit, prefixType, prefix, remString
                    );
                }
            }

            return parsedUnit;
        }
        
        private static ParsedUnit AnalysePrefix(ParsedUnit parsedUnit, PrefixTypes prefixType, KeyValuePair<string, decimal> prefix, string remString)
        {
            Units unit = GetUnitFromString(remString);

            if (unit != Units.None && PrefixCanBeUsedBasic(unit, prefixType, parsedUnit.UnitInfo.Prefix.PrefixUsage))
            {
                parsedUnit.UnitInfo = UpdateUnitInformation
                (
                    parsedUnit.UnitInfo, unit, new Prefix
                    (
                        prefix.Value, parsedUnit.UnitInfo.Prefix.PrefixUsage
                    )
                );

                //Useful in case of looking for further prefixes.
                parsedUnit.InputToParse = remString;
            }

            return parsedUnit;
        }

        private static UnitInfo UpdateUnitInformation(UnitInfo unitInfo, Units unit, Prefix prefix)
        {
            unitInfo.Unit = unit;
            unitInfo.Prefix = new Prefix(prefix);
            unitInfo.Parts = GetUnitParts(unitInfo).Parts;

            return unitInfo;
        }

        private static bool PrefixCanBeUsedBasic(Units unit, PrefixTypes prefixType, PrefixUsageTypes prefixUsage)
        {
            if (prefixUsage == PrefixUsageTypes.AllUnits) return true;
            
            if (prefixType == PrefixTypes.SI)
            {
                if (AllOtherSIPrefixUnits.Contains(unit)) return true;
                
                UnitSystems system = GetSystemFromUnit(unit);
                return (system == UnitSystems.SI || system == UnitSystems.CGS);
            }
            else if (prefixType == PrefixTypes.Binary && GetTypeFromUnit(unit) == UnitTypes.Information)
            {
                return true;
            }

            return false;
        }

        private static string GetUnitStringIndividual(UnitInfo unitInfo)
        {
            string unitString = "None";
            
            if (unitInfo.Unit != Units.None && unitInfo.Unit != Units.Unitless && !AllUnnamedUnits.ContainsValue(unitInfo.Unit))
            {
                if (AllUnitSymbols.ContainsValue(unitInfo.Unit))
                {
                    unitString = AllUnitSymbols.First(x => x.Value == unitInfo.Unit).Key;
                    if (unitInfo.Prefix.Symbol != "")
                    {
                        unitString = unitInfo.Prefix.Symbol + unitString;
                    }
                }
            }

            return unitString;
        }

        private static ParsedUnit ImproveIndividualUnitPart(ParsedUnit parsedUnit)
        {
            if (parsedUnit.UnitInfo.Unit == Units.None || parsedUnit.UnitInfo.Error.Type != ErrorTypes.None)
            {
                return parsedUnit;
            }

            if (parsedUnit.UnitInfo.Parts.Count == 0)
            {
                parsedUnit.UnitInfo = GetUnitParts(parsedUnit.UnitInfo);
            }

            if (parsedUnit.UnitInfo.Parts.Count == 1)
            {
                //Parsing an individual unit might output more than 1 part.
                if (parsedUnit.UnitInfo.Parts[0].Prefix.Factor != 1m)
                {
                    //A unit like 1 km should be understood as 1 metre with a kilo prefix, formed
                    //by one part of a meter (no prefix). Note that the compound parsing methods
                    //take care of this issue automatically.
                    parsedUnit.UnitInfo.Prefix = new Prefix
                    (
                        parsedUnit.UnitInfo.Parts[0].Prefix
                    );
                    parsedUnit.UnitInfo.Parts[0].Prefix = new Prefix(parsedUnit.UnitInfo.Parts[0].Prefix.PrefixUsage);
                }
            }

            return parsedUnit;
        }
    }
}
