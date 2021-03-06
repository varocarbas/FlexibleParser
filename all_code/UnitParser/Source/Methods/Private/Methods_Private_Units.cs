﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    public partial class UnitP
    {
        private static UnitInfo RemoveAllUnitInformation(UnitInfo unitInfo, bool partsToo = false)
        {
            unitInfo.Unit = Units.None;
            unitInfo.System = UnitSystems.None;
            unitInfo.Type = UnitTypes.None;

            if (partsToo) unitInfo.Parts = new List<UnitPart>();

            return unitInfo;
        }

        private static UnitInfo ImproveUnitParts(UnitInfo unitInfo)
        {
            //GetUnitParts isn't just meant to get parts, but also to expand/simplify them.
            //That's why calling this method is one of the first steps when improving/analysing
            //a compound regardless of the fact that unit parts are already present.
            return GetUnitParts(unitInfo);
        }

        private static bool IsUnnamedUnit(Units unit)
        {
            return
            (
                unit == Units.ValidImperialUnit || 
                DefaultUnnamedUnits.ContainsValue(unit)
            );
        }

        private static UnitInfo RemoveUnitPart(UnitInfo unitInfo, UnitPart part)
        {
            unitInfo.InitialPositions.Remove(part);
            unitInfo.Parts.Remove(part);
            return unitInfo;
        }

        //This method assumes a normalised UnitInfo variable (i.e., without prefixes).
        private static UnitInfo GetBestPrefixForTarget(UnitInfo unitInfo, int targetExponent, PrefixTypes prefixType, bool modifyBaseTenExponent = false)
        {
            if (targetExponent == 0 || prefixType == PrefixTypes.None) return unitInfo;

            if (targetExponent > 24) targetExponent = 24;
            else if(targetExponent < -24) targetExponent = -24;

            UnitInfo targetInfo = RaiseToIntegerExponent(10, targetExponent);

            foreach (decimal prefixFactor in GetAllPrefixFactors(targetExponent, prefixType))
            {
                if ((targetExponent > 0 && targetInfo.Value >= prefixFactor) || (targetExponent < 0 && targetInfo.Value <= prefixFactor))
                {
                    unitInfo = IncludeRemainingTargetPrefix
                    (
                        unitInfo, targetInfo, new UnitInfo(prefixFactor)
                    );
                    unitInfo.Prefix = new Prefix(prefixFactor, unitInfo.Prefix.PrefixUsage);
                    break;
                }
            }

            return
            (
                modifyBaseTenExponent ?
                VaryBaseTenExponent(unitInfo, -targetExponent) :
                unitInfo
            );
        }

        private static bool PrefixCanBeUsedWithUnit(UnitInfo unitInfo, PrefixTypes prefixType)
        {
            return 
            (
                !PrefixCanBeUsedWithUnitBasicCheck(unitInfo, prefixType) ? false :
                PrefixCanBeUsedCompound(unitInfo)
            );
        }

        private static bool PrefixCanBeUsedWithUnitBasicCheck(UnitInfo unitInfo, PrefixTypes prefixType)
        {
            if (unitInfo.Prefix.PrefixUsage == PrefixUsageTypes.AllUnits) return true;

            if (prefixType == PrefixTypes.SI)
            {
                if (AllOtherSIPrefixUnits.Contains(unitInfo.Unit)) return true;

                UnitSystems system =
                (
                    unitInfo.System == UnitSystems.None ?
                    GetSystemFromUnitInfo(unitInfo) :
                    unitInfo.System
                );

                return (system == UnitSystems.SI || system == UnitSystems.CGS);
            }
            else if (prefixType == PrefixTypes.Binary)
            {
                UnitTypes type =
                (
                    unitInfo.Type == UnitTypes.None ?
                    GetTypeFromUnitInfo(unitInfo) :
                    unitInfo.Type
                );
                return AllBinaryPrefixTypes.Contains(type);
            }

            return false;
        }

        private static UnitInfo IncludeRemainingTargetPrefix(UnitInfo unitInfo, UnitInfo expectedTarget, UnitInfo actualTarget)
        {
            UnitInfo remInfo = NormaliseUnitInfo
            (
                PerformManagedOperationValues
                (
                    expectedTarget, actualTarget, Operations.Division
                )
            );

            return PerformManagedOperationValues(unitInfo, remInfo, Operations.Multiplication);
        }

        private static IEnumerable<decimal> GetAllPrefixFactors(int targetExponent, PrefixTypes prefixType)
        {
            IEnumerable<decimal> allPrefixes = null;

            InitialiseBigSmallPrefixValues(prefixType);
            if (prefixType == PrefixTypes.SI)
            {
                allPrefixes =
                (
                    targetExponent > 0 ? BigSIPrefixValues :
                    SmallSIPrefixValues
                );
            }
            else
            {
                allPrefixes =
                (
                    targetExponent > 0 ? BigBinaryPrefixValues :
                    SmallBinaryPrefixValues
                );
            }

            return allPrefixes;
        }

        private static UnitInfo GetPartsFromUnit(UnitInfo unitInfo)
        {
            if (unitInfo.Unit == Units.None || unitInfo.Unit == Units.Unitless || IsUnnamedUnit(unitInfo.Unit))
            {
                return unitInfo;
            }
            
            unitInfo = GetPartsFromUnitCompound(unitInfo);
           
            if (unitInfo.Parts.Count < 1)
            {
                unitInfo.Parts = new List<UnitPart>()
                {
                    new UnitPart(unitInfo.Unit, 1)
                };
            }

            return unitInfo;
        }

        private static string GetUnitString(UnitInfo unitInfo)
        {
            string outUnit = GetUnitStringIndividual(unitInfo);

            return
            (
                outUnit == "None" ?
                GetUnitStringCompound(unitInfo) : outUnit
            );
        }

        private static string GetUnitStringIndividual(UnitInfo unitInfo)
        {
            string unitString = "None";

            if (unitInfo.Unit != Units.None && unitInfo.Unit != Units.Unitless && !IsUnnamedUnit(unitInfo.Unit))
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

        private static Units GetUnitFromString(string input, ParseInfo parseInfo = null)
        {
            Units unit = GetUnitFromUnitSymbols(input);

            return
            (
                unit != Units.None ? unit : 
                GetUnitFromUnitStrings(input, parseInfo)
            );
        }

        private static Units GetUnitFromUnitSymbols(string input)
        {
            return
            (
                AllUnitSymbols.ContainsKey(input) ?
                AllUnitSymbols[input] :
                GetUnitFromUnitSymbols2(input)
            );
        }

        private static Units GetUnitFromUnitSymbols2(string input)
        {
            return
            (
                AllUnitSymbols2.ContainsKey(input) ?
                AllUnitSymbols2[input] : Units.None
            );
        }
  
        private static Units GetUnitFromUnitStrings(string input, ParseInfo parseInfo)
        {
            string inputLower = input.ToLower();

            Units unit = AllUnitStrings.FirstOrDefault
            (
                x => (x.Key.ToLower() == inputLower || GetUnitStringPlural(x.Key.ToLower()) == inputLower)
            )
            .Value;

            if (parseInfo != null && unit != Units.None)
            {
                //To account for somehow unlikely scenarios where non-official abbreviations are 
                //misinterpreted. For example, Gs misunderstood as grams rather than as gigasecond.
                ParseInfo temp = CheckPrefixes(parseInfo);
                if (temp.UnitInfo.Unit != Units.None)
                {
                    //The unit (+ prefix) will be adequately analysed somewhere else.
                    return Units.None;
                }
            }

            return unit;
        }

        //A proper plural determination isn't required. The outputs of this method are quite secondary and
        //the supported units quite regular on this front.
        private static string GetUnitStringPlural(string unitString)
        {
            if (unitString.EndsWith("y"))
            {
                return unitString.Substring(0, unitString.Length - 1) + "ies";
            }
            unitString = unitString.Replace("inches", "inch");
            unitString = unitString.Replace("inch", "inches");
            unitString = unitString.Replace("foot", "feet");

            return
            (
                unitString.EndsWith("inches") || unitString.EndsWith("feet") ?
                unitString : unitString + "s"
            );
        }

        private static UnitTypes GetTypeFromUnitPart(UnitPart unitPart, bool ignoreExponents = false, bool simplestApproach = false)
        {
            UnitPart unitPart2 = new UnitPart(unitPart);

            if (simplestApproach)
            {
                //This is reached when calling from parts of the code likely to provoke an infinite loop. 
                UnitTypes type2 = GetTypeFromUnit(unitPart2.Unit);
                
                if (type2 == UnitTypes.Length)
                {
                    if (unitPart2.Exponent == 2)
                    {
                        type2 = UnitTypes.Area;
                    }
                    else if (unitPart2.Exponent == 3)
                    {
                        type2 = UnitTypes.Volume;
                    }
                }

                return type2;
            }

            //When comparing unit part types, the exponent is often irrelevant. For example: in the compound
            //kg*m4, looking for m4 would yield no match (unlikely looking just for m).
            if (ignoreExponents) unitPart2.Exponent = 1;

            //Negative exponents do not affect type determination. For example, a unit consisting
            //in just the part m-1 is wavenumber (negative exponent being relevant), but is expected
            //to be treated as length (-1 doesn't matter) because of being used in internal calculations.
            unitPart2.Exponent = Math.Abs(unitPart2.Exponent);
            UnitInfo unitInfo = new UnitInfo(unitPart2.Unit, unitPart2.Prefix.Factor);
            unitInfo.Parts = new List<UnitPart>() { unitPart2 };

            UnitTypes outType = GetTypeFromUnitInfo(unitInfo);
            return 
            (
                outType == UnitTypes.None && unitPart2.Exponent != 1 ?
                //To account for cases like kg4 within compounds, expected to be understood as kg.
                GetTypeFromUnitPart(new UnitPart(unitPart2) { Exponent = 1 }) :
                outType
            );
        }

        private static UnitTypes GetTypeFromUnitInfo(UnitInfo unitInfo)
        {
            UnitTypes outType = UnitTypes.None;
            if (unitInfo.Parts.Count > 1 || unitInfo.Parts.FirstOrDefault(x => x.Exponent != 1) != null)   
            {
                outType = GetBasicCompoundType(unitInfo).Type;
            }

            return
            (
                outType != UnitTypes.None ?
                outType : GetTypeFromUnit(unitInfo.Unit)
            );
        }

        private static UnitTypes GetTypeFromUnit(Units unit)
        {
            return
            (
                unit == Units.None || unit == Units.Unitless || IsUnnamedUnit(unit) ?
                UnitTypes.None : AllUnitTypes[unit]
            );
        }

        private static UnitSystems GetSystemFromUnit(Units unit, bool getBasicVersion = false, bool getImperialAndUSCS = false)
        {
            if (unit == Units.None || unit == Units.Unitless)
            {
                return UnitSystems.None;
            }

            UnitSystems system = AllUnitSystems[unit];

            if (getImperialAndUSCS)
            {
                if (system == UnitSystems.Imperial && AllImperialAndUSCSUnits.Contains(unit))
                {
                    system = UnitSystems.ImperialAndUSCS;
                }
            }

            return 
            (
                getBasicVersion ? AllBasicSystems[system] : system
            );
        }

        //Confirms whether the given minus sign (-) is correct. Any-5 is right, but 5-5 is wrong.
        private static bool MinusIsOK(char[] inputArray, int i)
        {
            if (i > 0 && i < inputArray.Length)
            {
                if (char.IsNumber(inputArray[i + 1]) && !char.IsNumber(inputArray[i - 1]))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
