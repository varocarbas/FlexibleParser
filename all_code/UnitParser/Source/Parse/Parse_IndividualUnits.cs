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
            foreach (var prefix in allPrefixes)
            {
                string remString = GetPrefixRemaining
                (
                    parseInfo.InputToParse, allPrefixNames[prefix.Key], prefix.Key
                );
                if (remString == "") continue;

                return AnalysePrefix
                (
                    parseInfo, prefixType, prefix, remString
                );
            }

            return parseInfo;
        }

        private static string GetPrefixRemaining(string input, string prefixName, string prefixSymbol)
        {
            string remString = GetPrefixRemainingSpecial
            (
                input, prefixName, prefixSymbol
            );
            if (remString != "") return remString;

            if (input.ToLower().StartsWith(prefixName))
            {
                //String representation. Caps don't matter.
                remString = input.Substring(prefixName.Length);
            }
            else if (input.StartsWith(prefixSymbol))
            {
                //Symbol. Caps matter.
                remString = input.Substring(prefixSymbol.Length);
            }

            return remString;
        }

        //Accounts for cases where the default check (i.e., firstly name and then symbol) fails.
        private static string GetPrefixRemainingSpecial(string input, string prefixName, string prefixSymbol)
        {
            if (input.StartsWith(prefixSymbol))
            {
                if (input.ToLower().StartsWith(prefixSymbol.ToLower() + "bit"))
                {
                    //For example: Kibit which might be misunderstood as kibi prefix (name check) and t unit.
                    return "bit";
                }
            }

            return "";
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
            else if (prefixType == PrefixTypes.Binary)
            {
                return AllBinaryPrefixTypes.Contains(GetUnitType(unit));
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
                bool isCentimetre = parseInfo.UnitInfo.Parts[0].Unit == Units.Centimetre;
                if(!isCentimetre) isCentimetre = 
                (
                    parseInfo.UnitInfo.Parts[0].Unit == Units.Metre &&
                    parseInfo.UnitInfo.Parts[0].Prefix.Factor == 0.01m
                );
                
                if(isCentimetre) 
                {
                    //Centimetre has a very special status: it is fully defined by centi+metre, but it also needs its
                    //own unit to be the CGS basic length unit. Formally, it is a compound (formed by 1 part); practically,
                    //it doesn't enjoy any of the benefits associated with this reality.
                    return parseInfo;
                }

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
