using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlexibleParser
{
    public partial class UnitP
    {
        private static UnitInfo RemoveAllValueInformation(UnitInfo unitInfo)
        {
            unitInfo.Value = 0m;
            unitInfo.BigNumberExponent = 0;
            unitInfo.Prefix = new Prefix(unitInfo.Prefix.PrefixUsage);

            return unitInfo;
        }

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
            unitInfo.Parts = GetUnitParts(unitInfo).Parts;

            //All the code below this line (further) avoids the eventuality of having more than one
            //unit part of the same type via conversions. GetUnitParts() is already taking care of
            //the same type version via fractional simplification. 
            //For example: N*ft (= kg*ft*m/s2) is converted into ft-to-m-ratio J (= kg*m2/s2).
            Dictionary<UnitPart, UnitPart> toConvert = GetUnitPartsToConvert(unitInfo);

            return
            (
                toConvert.Count == 0 ? unitInfo : 
                ConvertSelectedUnitParts(unitInfo, toConvert)
            );
        }

        private static Dictionary<UnitPart, UnitPart> GetUnitPartsToConvert(UnitInfo unitInfo)
        {
            Dictionary<UnitPart, UnitPart> outDict = new Dictionary<UnitPart, UnitPart>();

            var suitableMatches = unitInfo.Parts.OrderBy(x => unitInfo.InitialPositions
            .First(y => y.Key == x).Value).ThenByDescending(x => x.Exponent)
            .GroupBy(x => GetTypeFromUnit(x.Unit));

            foreach (var item in suitableMatches)
            {
                if (item.Count() > 1)
                {
                    UnitPart target = item.First();

                    foreach (var item2 in item.Where(x => x.Unit != target.Unit))
                    {
                        outDict.Add(item2, target);
                    }
                }
            }

            return outDict;
        }

        private static UnitInfo ConvertSelectedUnitParts(UnitInfo unitInfo, Dictionary<UnitPart, UnitPart> toConvertParts)
        {
            unitInfo = ConvertAllUnitParts(unitInfo, toConvertParts);
            unitInfo = UpdateVariableAfterConversion(unitInfo, toConvertParts);

            //Accounting for unitless side-effects of the aforementioned actions. 
            for (int i = unitInfo.Parts.Count - 1; i >= 0; i--)
            {
                if (unitInfo.Parts[i].Exponent == 0)
                {
                    unitInfo = RemoveUnitPart(unitInfo, unitInfo.Parts[i]);
                }
            }

            return unitInfo;
        }

        private static UnitInfo RemoveUnitPart(UnitInfo unitInfo, UnitPart part)
        {
            unitInfo.InitialPositions.Remove(part);
            unitInfo.Parts.Remove(part);

            return unitInfo;
        }

        private static UnitInfo UpdateVariableAfterConversion(UnitInfo unitInfo, Dictionary<UnitPart, UnitPart> toConvert)
        {
            foreach (var item in toConvert)
            {
                unitInfo = RemoveUnitPart
                (
                    unitInfo, unitInfo.Parts.First(x => x.Unit == item.Key.Unit)
                );
                
                unitInfo.Parts.First(x => x.Unit == item.Value.Unit).Exponent += item.Key.Exponent;
            }

            return unitInfo;
        }

        private static bool IsBasicUnit(Units unit)
        {
            foreach (var item in AllBasicUnits)
            {
                foreach (var item2 in item.Value)
                {
                    if (item2.Value.Unit == unit) return true;
                }            
            }

            return false;
        }

        //This method assumes a normalised UnitInfo variable (i.e., without prefixes).
        private static UnitInfo GetBestPrefixForTarget(UnitInfo unitInfo, int targetExponent, PrefixTypes prefixType, bool modifyBigNumberExponent = false)
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

            if (modifyBigNumberExponent)
            {
                unitInfo.BigNumberExponent -= targetExponent;
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
            if (unitInfo.Unit == Units.None || unitInfo.Unit == Units.Unitless || AllUnnamedUnits.ContainsValue(unitInfo.Unit))
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

        private static UnitTypes GetTypeFromUnitPart(UnitPart unitPart)
        {
            UnitPart unitPart2 = new UnitPart(unitPart);
            
            //Negative exponent do not affect type determination. For example, a unit consisting
            //in just the part m-1 is wavenumber (negative exponent being relevant), but this part
            //is actually length (-1 doesn't matter).
            unitPart2.Exponent = Math.Abs(unitPart2.Exponent);
            UnitInfo unitInfo = new UnitInfo(unitPart2.Unit, unitPart2.Prefix.Factor);
            unitInfo.Parts = new List<UnitPart>() { unitPart2 };

            UnitTypes outType = GetTypeFromUnitInfo(unitInfo).Type;
            return 
            (
                outType == UnitTypes.None && unitPart2.Exponent != 1 ?
                //To account for cases like kg^4 within compounds.
                GetTypeFromUnitPart(new UnitPart(unitPart2) { Exponent = 1 }) :
                outType
            );
        }

        private static UnitInfo GetTypeFromUnitInfo(UnitInfo unitInfo)
        {
            if (unitInfo.Parts.Count > 0)
            {
                unitInfo = GetBasicCompoundType(unitInfo);
            }

            if (unitInfo.Type == UnitTypes.None)
            {
                unitInfo.Type = GetTypeFromUnit(unitInfo.Unit);
            }

            return unitInfo;
        }

        private static UnitTypes GetTypeFromUnit(Units unit)
        {
            return
            (
                unit == Units.None || unit == Units.Unitless || AllUnnamedUnits.ContainsValue(unit) ?
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

        //Confirms whether the given minus sign ('-') is correct. "any-5" is right. On the other hand,
        //"any-6other" and "5-5" are wrong.
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
