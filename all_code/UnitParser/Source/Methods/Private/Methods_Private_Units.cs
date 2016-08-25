using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    public partial class UnitP
    {
        private static UnitInfo RemoveAllUnitInformation(UnitInfo unitInfo)
        {
            unitInfo.Unit = Units.None;
            unitInfo.System = UnitSystems.None;
            unitInfo.Type = UnitTypes.None;

            return unitInfo;
        }

        private static UnitInfo ImproveUnitParts(UnitInfo unitInfo)
        {
            //GetUnitParts isn't just meant to get parts, but also to expand/simplify them.
            //That's why calling this method is one of the first steps when improving/analysing
            //a compound regardless of the fact of unit parts already being present.
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

            if (modifyBaseTenExponent)
            {
                unitInfo.BaseTenExponent -= targetExponent;
            }

            return unitInfo;
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

        private static Units GetDefaultEnglishMetricUnit(UnitTypes type, UnitSystems system)
        {
            foreach (var item in AllUnitConversionFactors.Where(x => GetTypeFromUnit(x.Key) == type))
            {
                UnitSystems system2 = GetSystemFromUnit(item.Key);
                if (system2 == UnitSystems.None) continue;

                if (AllMetricEnglish[system] == AllMetricEnglish[system2])
                {
                    return item.Key;
                }
            }

            return Units.None;
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

        private static Units GetUnitFromString(string input)
        {
            Units unit = GetUnitFromUnitSymbols(input);

            return
            (
                unit != Units.None ? unit :
                GetUnitFromUnitStrings(input)
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
  
        private static Units GetUnitFromUnitStrings(string input)
        {
            return AllUnitStrings.FirstOrDefault
            (
                x => x.Key.ToLower() == input.ToLower()
            )
            .Value;
        }

        private static UnitTypes GetTypeFromUnitPart(UnitPart unitPart, bool ignoreExponents = false)
        {
            UnitPart unitPart2 = new UnitPart(unitPart);

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
            if (unitInfo.Parts.Count > 0)
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
