using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    public partial class UnitP
    {
        private static ParseInfo StartIndividualUnitParse(ParseInfo parseInfo)
        {
            return GetIndividualUnitParts
            (
                ParseIndividualUnit(parseInfo)
            );
        }

        private static ParseInfo ParseIndividualUnit(ParseInfo parseInfo)
        {
            parseInfo = PrefixAnalysis(parseInfo);

            if (parseInfo.UnitInfo.Unit == Units.None)
            {
                //The final unit might have already been found while analysing prefixes.
                parseInfo.UnitInfo.Unit = GetUnitFromString(parseInfo.InputToParse);
            }

            parseInfo.UnitInfo = UpdateMainUnitVariables(parseInfo.UnitInfo);

            return parseInfo;
        }

        private static ParseInfo PrefixAnalysis(ParseInfo parseInfo)
        {
            parseInfo.UnitInfo.Unit = GetUnitFromString(parseInfo.InputToParse);
            return 
            (
                parseInfo.UnitInfo.Unit == Units.None ?
                CheckPrefixes(parseInfo) : parseInfo
            );
        }

        private static ParseInfo CheckPrefixes(ParseInfo parseInfo)
        {
            if (parseInfo.InputToParse.Length < 2) return parseInfo;

            string origString = parseInfo.InputToParse;

            parseInfo.UnitInfo.Prefix = new Prefix
            (
                parseInfo.UnitInfo.Prefix.PrefixUsage
            );

            ParseInfo tempSI = CheckSIPrefixes(new ParseInfo(parseInfo));
            ParseInfo tempBinary = CheckBinaryPrefixes(new ParseInfo(parseInfo));

            if (tempSI.UnitInfo.Prefix.Factor != 1m)
            {
                if (tempBinary.UnitInfo.Prefix.Factor != 1m)
                {
                    //Both SI and binary prefixes were detected, what is an error.
                    parseInfo = new ParseInfo();
                    parseInfo.InputToParse = origString;
                    parseInfo.UnitInfo.Error = new ErrorInfo(ErrorTypes.InvalidUnit);
                }
                else parseInfo = new ParseInfo(tempSI);
            }
            else if (tempBinary.UnitInfo.Prefix.Factor != 1m)
            {
                parseInfo = new ParseInfo(tempBinary);
            }

            return parseInfo;
        }
        
        private static ParseInfo CheckBinaryPrefixes(ParseInfo parseInfo)
        {
            return CheckPrefixes
            (
                parseInfo, PrefixTypes.Binary,
                AllBinaryPrefixSymbols, AllBinaryPrefixNames
            );
        }

        private static ParseInfo CheckSIPrefixes(ParseInfo parseInfo)
        {
            return CheckPrefixes
            (
                parseInfo, PrefixTypes.SI,
                AllSIPrefixSymbols, AllSIPrefixNames
            );
        }
        
        private static ParseInfo CheckPrefixes(ParseInfo parseInfo, PrefixTypes prefixType, Dictionary<string, decimal> allPrefixes, Dictionary<string, string> allPrefixNames)
        {
            string remString = "";

            foreach (var prefix in allPrefixes)
            {
                if (parseInfo.InputToParse.StartsWith(prefix.Key))
                {
                    //Symbol. Caps matter.
                    remString = parseInfo.InputToParse.Substring(prefix.Key.Length);
                }
                else if (parseInfo.InputToParse.ToLower().StartsWith(allPrefixNames[prefix.Key]))
                {
                    //String representation. Caps don't matter.
                    remString = parseInfo.InputToParse.Substring(allPrefixNames[prefix.Key].Length);
                }
                
                if (remString != "")
                {
                    return AnalysePrefix
                    (
                        parseInfo, prefixType, prefix, remString
                    );
                }
            }

            return parseInfo;
        }
        
        private static ParseInfo AnalysePrefix(ParseInfo parseInfo, PrefixTypes prefixType, KeyValuePair<string, decimal> prefix, string remString)
        {
            Units unit = GetUnitFromString(remString);

            if (unit != Units.None && PrefixCanBeUsedBasic(unit, prefixType, parseInfo.UnitInfo.Prefix.PrefixUsage))
            {
                parseInfo.UnitInfo = UpdateUnitInformation
                (
                    parseInfo.UnitInfo, unit, new Prefix
                    (
                        prefix.Value, parseInfo.UnitInfo.Prefix.PrefixUsage
                    )
                );

                //Useful in case of looking for further prefixes.
                parseInfo.InputToParse = remString;
            }

            return parseInfo;
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

        private static ParseInfo GetIndividualUnitParts(ParseInfo parseInfo)
        {
            if (parseInfo.UnitInfo.Unit == Units.None || parseInfo.UnitInfo.Error.Type != ErrorTypes.None)
            {
                return parseInfo;
            }

            if (parseInfo.UnitInfo.Parts.Count == 0)
            {
                parseInfo.UnitInfo = GetUnitParts(parseInfo.UnitInfo);
            }

            if (parseInfo.UnitInfo.Parts.Count == 1)
            {
                //Parsing an individual unit might output more than 1 part.
                if (parseInfo.UnitInfo.Parts[0].Prefix.Factor != 1m)
                {
                    //1 km should be understood as 1 metre with a kilo prefix, formed
                    //by one part of a metre (no prefix). 
                    parseInfo.UnitInfo.Prefix = new Prefix
                    (
                        parseInfo.UnitInfo.Parts[0].Prefix
                    );
                    parseInfo.UnitInfo.Parts[0].Prefix = new Prefix
                    (
                        parseInfo.UnitInfo.Parts[0].Prefix.PrefixUsage
                    );
                }
            }

            return parseInfo;
        }
    }
}
