using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    public partial class UnitP
    {
        private static UnitInfo UnitPartToUnitInfo(UnitPart unitPart, decimal value = 0m)
        {
            Units unit = unitPart.Unit;
            UnitInfo outUnitInfo = new UnitInfo(value, unit, unitPart.Prefix);
            outUnitInfo = UpdateMainUnitVariables(outUnitInfo);

            UnitTypes type = GetTypeFromUnitPart(unitPart);

            if (GetTypeFromUnit(unit) != type)
            {
                //unitPart.Unit doesn't account for unitPart.Exponent. That is: it took the basic
                //unit rather than the compound which it was forming.
                Dictionary<Units, UnitPart> potentials = new Dictionary<Units, UnitPart>();
                foreach (var compoundUnit in AllUnitTypes.Where(x => x.Value == type && UnitIsNamedCompound(x.Key)))
                {
                    Units unit2 = compoundUnit.Key;

                    int count = 0;
                    while (count < 2)
                    {
                        count = count + 1;
                        List<UnitPart> unitParts = GetCompoundUnitParts(unit2, count == 2);
                        if (unitParts.Count == 1)
                        {
                            potentials.Add(unit2, unitParts[0]);
                            break;
                        }
                    }
                }

                foreach (var potential in potentials)
                {
                    if (UnitPartsAreEquivalent(potential.Value, unitPart))
                    {
                        outUnitInfo.Unit = potential.Key;
                        outUnitInfo = UpdateMainUnitVariables(outUnitInfo, true);
                        return outUnitInfo;
                    }
                }
            }

            return outUnitInfo;
        }

        private static bool UnitPartsAreEquivalent(UnitPart unitPart1, UnitPart unitPart2)
        {
            if (unitPart1.Unit == unitPart2.Unit && unitPart1.Prefix == unitPart2.Prefix)
            {
                if (Math.Abs(unitPart1.Exponent) == Math.Abs(unitPart2.Exponent))
                {
                    //Exponent sign isn't too relevant in quite a few matching UnitPart scenarios.
                    //For example: standalone kg and the one in L/kg.
                    return true;
                }
            }

            return false;
        }

        private static List<UnitPart> GetBasicCompoundUnitParts(UnitTypes type, UnitSystems system, bool onePartCompound = false)
        {
            if (AllBasicCompounds.ContainsKey(type) && AllBasicCompounds[type].ContainsKey(system))
            {
                return GetCompoundUnitParts
                (
                    AllBasicCompounds[type][system], true, 
                    type, system, onePartCompound
                );
            }

            return new List<UnitPart>();
        }
        
        private static List<UnitPart> GetBasicCompoundUnitParts(Units unit, bool onePartCompound = false)
        {
            return GetCompoundUnitParts
            (
                unit, true, UnitTypes.None, 
                UnitSystems.None, onePartCompound
            );
        }

        private static List<UnitPart> GetCompoundUnitParts(Units unit, bool basicCompound, UnitTypes type = UnitTypes.None, UnitSystems system = UnitSystems.None, bool onePartCompound = false)
        {
            List<UnitPart> unitParts = new List<UnitPart>();

            if (basicCompound)
            {
                if (type == UnitTypes.None)
                {
                    type = GetTypeFromUnit(unit);
                    system = GetSystemFromUnit(unit);
                }

                if (AllBasicCompounds.ContainsKey(type) && AllBasicCompounds[type].ContainsKey(system))
                {
                    foreach (Compound compound in AllCompounds[type])
                    {
                        if (onePartCompound && compound.Parts.Count > 1)
                        {
                            //AllCompounds includes various versions for each compound. onePartCompound being true
                            //means that only 1-part compounds are relevant.
                            continue;
                        }
                        //When onePartCompound is false, the primary/most-expanded version is expected.
                        //In AllCompounds, this version is always located in the first position for the given type.

                        foreach (CompoundPart compoundPart in compound.Parts)
                        {
                            BasicUnit basicUnit = AllBasicUnits[compoundPart.Type][system];

                            unitParts.Add
                            (
                                new UnitPart
                                (
                                    basicUnit.Unit, basicUnit.PrefixFactor,
                                    compoundPart.Exponent
                                )
                            );
                        }

                        return unitParts;
                    }
                }
            }
            else if (AllNonBasicCompounds.ContainsKey(unit))
            {
                unitParts.AddRange(AllNonBasicCompounds[unit]);
            }

            return unitParts;
        }

        //It is better to avoid using prefixes with some compounds to avoid misinterpretations.
        //For example: 1000 m2 converted into k m2 is easily misinterpretable as km2 (completely different).
        private static bool PrefixCanBeUsedCompound(UnitInfo unitInfo)
        {
            bool canBeUsed = true;

            if (UnitIsNamedCompound(unitInfo.Unit))
            {
                canBeUsed = AllCompoundsUsingPrefixes.Contains(unitInfo.Unit);
            }
            else if (unitInfo.Parts.Count > 1) canBeUsed = false;

            return canBeUsed;
        }

        private static bool UnitIsNamedCompound(Units unit)
        {
            return
            (
                UnitIsNonBasicCompound(unit) ? true :
                UnitIsBasicCompound(unit)
            );
        }

        private static bool UnitIsBasicCompound(Units unit)
        {
            foreach (var item in AllBasicCompounds)
            {
                if (item.Value.ContainsValue(unit))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool UnitIsNonBasicCompound(Units unit)
        {
            return AllNonBasicCompounds.ContainsKey(unit);
        }

        private static UnitInfo GetCompoundUnitFromParts(UnitInfo unitInfo)
        {
            //Perhaps just a simple unit.
            unitInfo = GetIndividualUnitFromParts(unitInfo);
            if (unitInfo.Unit != Units.None) return unitInfo;

            //Better starting with the quicker non-basic-compound check.
            unitInfo = GetNonBasicCompoundUnitFromParts(unitInfo);

            return
            (
                unitInfo.Unit != Units.None ? unitInfo :
                GetBasicCompoundUnitInfo(unitInfo)
            );
        }

        private static UnitInfo GetIndividualUnitFromParts(UnitInfo unitInfo)
        {
            if (unitInfo.Parts.Count == 1 && unitInfo.Parts[0].Exponent == 1m)
            {
                if (AllUnitConversionFactors.ContainsKey(unitInfo.Parts[0].Unit))
                {
                    unitInfo.Unit = unitInfo.Parts[0].Unit;
                    unitInfo.Type = AllUnitTypes[unitInfo.Unit];
                    unitInfo.System = AllUnitSystems[unitInfo.Unit];
                }
            }

            return unitInfo;
        }

        private static UnitInfo GetNonBasicCompoundUnitFromParts(UnitInfo unitInfo)
        {
            foreach (var compound in AllNonBasicCompounds)
            {
                if (NonBasicCompoundsToSkip.Contains(compound.Key))
                {
                    continue;
                }

                if (compound.Value.Length == unitInfo.Parts.Count)
                {
                    List<UnitPart> targetParts = compound.Value.ToList();
                    if (UnitPartsMatchCompoundUnitParts(unitInfo, targetParts, true))
                    {
                        unitInfo = PopulateUnitRelatedInfo(unitInfo, compound.Key);
                        if (!UnitPartsMatchCompoundUnitParts(unitInfo, targetParts))
                        {
                            //Some prefixes differ from the basic configuration.
                            unitInfo = AdaptPrefixesToMatchBasicCompound(unitInfo, targetParts);
                        }

                        return unitInfo;
                    }
                }
            }

            return unitInfo;
        }

        //Crosschecking the unit parts against all the supported (basic and non-basic) compounds.
        private static UnitInfo GetPartsFromUnitCompound(UnitInfo unitInfo)
        {
            //Always better to start compound analyses with the more-to-the-point non-basic ones.
            unitInfo = GetPartsFromUnitCompoundNonBasic(new UnitInfo(unitInfo));
            if (unitInfo.Parts.Count > 0) return unitInfo;

            if (unitInfo.Type == UnitTypes.None)
            {
                unitInfo.Type = GetTypeFromUnitInfo(unitInfo);
            }
            if (unitInfo.System == UnitSystems.None)
            {
                unitInfo.System = GetSystemFromUnit(unitInfo.Unit);
            }               

            return
            (
                GetBasicCompoundUnitInfo(unitInfo).Unit == unitInfo.Unit ?
                GetPartsFromUnitCompoundBasic(unitInfo) :
                unitInfo
            );
        }

        private static UnitInfo GetPartsFromUnitCompoundBasic(UnitInfo unitInfo)
        {
            if (unitInfo.System == UnitSystems.None || !AllCompounds.ContainsKey(unitInfo.Type))
            {
                return unitInfo;
            }

            unitInfo.Parts.AddRange
            (
                GetUnitPartsFromBasicCompound
                (
                    AllCompounds[unitInfo.Type][0], unitInfo.System
                )
            );

            return unitInfo;
        }

        private static UnitInfo GetPartsFromUnitCompoundNonBasic(UnitInfo unitInfo)
        {
            if (AllNonBasicCompounds.ContainsKey(unitInfo.Unit))
            {
                unitInfo.Parts.AddRange(AllNonBasicCompounds[unitInfo.Unit]);
            }

            return unitInfo;
        }

        private static UnitSystems GetSystemFromUnitInfo(UnitInfo unitInfo)
        {
            UnitSystems outSystem = UnitSystems.None;

            //It helps to avoid misunderstandingg with "neutral types".
            //For example, to avoid s*ft to be understood as SI.
            List<UnitSystems> neutralSystems = new List<UnitSystems>();
            bool allNeutral = true;

            foreach (UnitPart part in unitInfo.Parts)
            {
                Units partUnit = part.Unit;
                
                //Both determinations are 100% accurate because of refering to a unit part.
                UnitTypes partType = GetTypeFromUnit(partUnit);
                UnitSystems system2 = GetSystemFromUnit(partUnit);

                if (NeutralTypes.Contains(partType)) neutralSystems.Add(system2);
                else
                {
                    allNeutral = false;
                    if (outSystem == UnitSystems.None) outSystem = system2;
                    else if (outSystem != system2) break;
                }
            }

            if (outSystem == UnitSystems.None && allNeutral && neutralSystems.Count > 0)
            {
                //When all the units are "neutral", their defining system cannot be ignored.
                outSystem = neutralSystems.GroupBy(x => x).OrderByDescending(x => x.Count()).First().Key;
            }

            return
            (
                outSystem != UnitSystems.None ?
                outSystem : GetSystemFromUnit(unitInfo.Unit)
            );
        }

        private static List<UnitPart> GetUnitPartsFromBasicCompound(Compound compound, UnitSystems system, int sign = 1)
        {
            List<UnitPart> outParts = new List<UnitPart>();
            if (system == UnitSystems.None) return outParts;

            foreach (CompoundPart compoundPart in compound.Parts)
            {
                BasicUnit basic = AllBasicUnits[compoundPart.Type][AllBasicSystems[system]];
                outParts.Add
                (
                    new UnitPart
                    (
                        basic.Unit, basic.PrefixFactor, 
                        sign * compoundPart.Exponent
                    )
                );
            }

            return outParts;
        }

        private static UnitInfo GetBasicCompoundUnitInfo(UnitInfo unitInfo)
        {
            if (unitInfo.Parts.Count < 1) return unitInfo;

            if (unitInfo.Type == UnitTypes.None)
            {
                unitInfo.Type = GetTypeFromUnitInfo(unitInfo);
            }

            return
            (
                unitInfo.Type == UnitTypes.None ? unitInfo :
                GetBasicCompoundUnit(unitInfo)
            );
        }

        private static UnitInfo GetBasicCompoundUnit(UnitInfo unitInfo)
        {
            unitInfo.Unit = DefaultUnnamedUnits[unitInfo.System];
            
            UnitSystems system2 = unitInfo.System;
            if (system2 == UnitSystems.None)
            {
                system2 = UnitSystems.SI;
            }

            Units basicCompound =
            (
                AllBasicCompounds[unitInfo.Type].ContainsKey(system2) ?
                AllBasicCompounds[unitInfo.Type][system2] :
                Units.None
            );

            if (basicCompound == Units.None) return unitInfo;

            UnitSystems basicSystem = AllBasicSystems[system2];
            List<UnitPart> basicUnitParts = GetBasicCompoundUnitParts(unitInfo.Type, basicSystem);
            if (basicUnitParts.Count != unitInfo.Parts.Count) return unitInfo;

            if (UnitPartsMatchCompoundUnitParts(unitInfo, basicUnitParts, true))
            {
                unitInfo = PopulateUnitRelatedInfo(unitInfo, basicCompound);
                if (!UnitPartsMatchCompoundUnitParts(unitInfo, basicUnitParts))
                {
                    //Some prefixes differ from the basic configuration.
                    unitInfo = AdaptPrefixesToMatchBasicCompound(unitInfo, basicUnitParts);
                }
            }

            return unitInfo;
        }

        private static UnitInfo AdaptPrefixesToMatchBasicCompound(UnitInfo unitInfo, List<UnitPart> basicUnitParts)
        {
            UnitInfo allPrefixes = new UnitInfo(1m);
            Dictionary<Units, decimal> basicPrefixes = basicUnitParts.ToDictionary
            (
                x => x.Unit, x => x.Prefix.Factor
            );

            foreach (UnitPart part in unitInfo.Parts)
            {
                if (basicUnitParts.FirstOrDefault(x => x == part) == null)
                {
                    UnitInfo prefixRem = PerformManagedOperationValues
                    (
                        part.Prefix.Factor, basicPrefixes[part.Unit], Operations.Division
                    );

                    prefixRem = RaiseToIntegerExponent(prefixRem, part.Exponent);

                    allPrefixes = PerformManagedOperationValues
                    (
                        allPrefixes, prefixRem, Operations.Multiplication
                    );

                    part.Prefix = new Prefix(basicPrefixes[part.Unit]);
                }
            }

            if (allPrefixes.Value != 1.0m) unitInfo = unitInfo * allPrefixes;

            return unitInfo;
        }

        private static bool UnitPartsMatchCompoundUnitParts(UnitInfo unitInfo, List<UnitPart> basicUnitParts, bool onlyUnits = false)
        {
            foreach (UnitPart basic in basicUnitParts)
            {
                if (onlyUnits)
                {
                    if (unitInfo.Parts.FirstOrDefault(x => x.Unit == basic.Unit) == null)
                    {
                        return false;
                    }
                }
                else if (unitInfo.Parts.FirstOrDefault(x => x == basic) == null)
                {
                    return false;
                }
            }

            return true;
        }

        private static UnitInfo GetUnitParts(UnitInfo unitInfo)
        {
            if (AllNonDividableUnits.Contains(unitInfo.Unit))
            {
                unitInfo.Parts = new List<UnitPart>()
                {
                    new UnitPart(unitInfo.Unit, 1)
                };

                return unitInfo;
            }

            unitInfo = ExpandUnitParts(unitInfo);
            if (unitInfo.Parts.Count > 1)
            {
                unitInfo = SimplifyUnitParts(unitInfo);
            }

            return UpdateInitialPositions(unitInfo);
        }

        private static UnitInfo UpdateInitialPositions(UnitInfo unitInfo)
        {
            if (unitInfo.Parts.Count < 1) return unitInfo;

            int newPos = 
            (
                unitInfo.InitialPositions.Count > 0 ?
                unitInfo.InitialPositions.Max(x => x.Value) : 0
            );

            //Some unit part modifications get a bit too messy and InitialPositions isn't always updated properly.
            //This is a simple and reliable way to account for eventual problems on this front.
            foreach (UnitPart part in unitInfo.Parts)
            {
                if (!unitInfo.InitialPositions.ContainsKey(part))
                {
                    newPos = newPos + 1;
                    unitInfo.InitialPositions.Add(part, newPos);
                }
            }

            unitInfo.Parts = unitInfo.Parts.OrderBy
            (
                x => unitInfo.InitialPositions.First(y => y.Key == x).Value
            )
            .ThenByDescending(x => x.Exponent).ToList();
            
            return unitInfo;
        }

        private static Dictionary<UnitPart, int> GetInitialPositions(List<UnitPart> unitParts)
        {
            Dictionary<UnitPart, int> outDict = new Dictionary<UnitPart, int>();

            for (int i = 0; i < unitParts.Count; i++)
            {
                if (!outDict.ContainsKey(unitParts[i]))
                {
                    outDict.Add(unitParts[i], i);
                }
            }

            return outDict;
        }

        private static UnitInfo ExpandUnitParts(UnitInfo unitInfo)
        {
            if (unitInfo.Parts.Count == 0) unitInfo = GetPartsFromUnit(unitInfo);

            if (unitInfo.InitialPositions == null || unitInfo.InitialPositions.Count == 0)
            {
                unitInfo.InitialPositions = GetInitialPositions(unitInfo.Parts);
            }

            for (int i = unitInfo.Parts.Count - 1; i >= 0; i--)
            {
                UnitInfo infoPart = new UnitInfo
                (
                    0m, unitInfo.Parts[i].Unit, 
                    unitInfo.Parts[i].Prefix, false
                );

                if (IsDividable(infoPart))
                {
                    unitInfo = AddCompoundParts(unitInfo, infoPart, i);
                }
            }

            return unitInfo;
        }

        private static UnitInfo AddCompoundParts(UnitInfo unitInfo, UnitInfo partInfo, int i)
        {
            return
            (
                AllNonBasicCompounds.ContainsKey(partInfo.Unit) ?
                ExpandNonBasicCompoundToUnitPart(unitInfo, partInfo, i) :
                ExpandBasicCompoundToUnitPart(unitInfo, partInfo, i)
            );
        }

        private static bool IsDividable(UnitInfo unitInfo)
        {
            unitInfo.Type = GetTypeFromUnitInfo(unitInfo);
            
            return
            (
                !AllNonDividableUnits.Contains(unitInfo.Unit) && 
                (
                    AllCompounds.ContainsKey(unitInfo.Type) ||
                    AllNonBasicCompounds.ContainsKey(unitInfo.Unit)
                )
            );
        }

        private static UnitInfo SimplifyUnitParts(UnitInfo unitInfo)
        {
            if (unitInfo.Parts.Count < 1) return unitInfo;

            //When having more than one part of the same type, a conversion (+ later removal) is performed.
            //By default, the part with the higher index is kept. That's why it is better to reorder the parts
            //such that the ones belonging to systems more likely to be preferred are kept.
            unitInfo.Parts = unitInfo.Parts.OrderBy
            (
                x => GetUnitSystem(x.Unit) == UnitSystems.None
            )
            .ThenBy
            (
                x => AllMetricEnglish[GetUnitSystem(x.Unit)] == UnitSystems.Imperial
            )
            .ToList();

            for (int i = unitInfo.Parts.Count - 1; i >= 0; i--)
            {
                if (unitInfo.Parts[i].Unit == Units.None || unitInfo.Parts[i].Unit == Units.Unitless)
                {
                    continue;
                }

                //Checking non-basic compounds is very quick (+ can avoid some of the subsequent analyses).
                //Additionally, some of these compounds wouldn't be detected in case of performing a full
                //simplification. For example: in Wh, all the time parts would be converted into hour or second
                //and, consequently, recognised as other energy unit (joule or unnamed one).
                unitInfo = GetNonBasicCompoundUnitFromParts(unitInfo);
                if(unitInfo.Type != UnitTypes.None)
                {
                    return unitInfo;
                }

                for (int i2 = i - 1; i2 >= 0; i2--)
                {
                    bool remove = false;
                    if (unitInfo.Parts[i].Unit == unitInfo.Parts[i2].Unit)
                    {
                        remove = true;
                    }
                    else
                    {
                        UnitInfo tempInfo = AdaptUnitParts(unitInfo, i, i2);
                        if (tempInfo != null)
                        {
                            remove = true;
                            unitInfo = tempInfo;
                        }
                    }

                    if (remove)
                    {
                        unitInfo = SimplifyUnitPartsRemove(unitInfo, i, i2);
                    }

                    if (unitInfo.Parts.Count == 0 || i > unitInfo.Parts.Count - 1)
                    {
                        i = unitInfo.Parts.Count;
                        break;
                    }
                }
            }

            if (unitInfo.Parts.Count == 0) unitInfo.Unit = Units.Unitless;

            return unitInfo;
        }

        private static UnitInfo SimplifyUnitPartsRemove(UnitInfo unitInfo, int i, int i2)
        {
            if (unitInfo.Parts[i].Prefix.Factor == unitInfo.Parts[i2].Prefix.Factor)
            {
                unitInfo.Parts[i].Exponent += unitInfo.Parts[i2].Exponent;
            }
            else unitInfo = UpdateDifferentPrefixParts(unitInfo, i, i2);

            if (unitInfo.Parts[i].Exponent == 0)
            {
                unitInfo = RemoveUnitPart(unitInfo, unitInfo.Parts[i]);
            }

            return RemoveUnitPart(unitInfo, unitInfo.Parts[i2]);
        }

        private static UnitInfo AdaptUnitParts(UnitInfo unitInfo, int i, int i2)
        {
            UnitPart[] parts2 = GetUnitPartsConversion
            (
                new UnitPart(unitInfo.Parts[i]), new UnitPart(unitInfo.Parts[i2])
            );
            if (parts2 == null) return null;

            UnitInfo tempInfo = ConvertUnitPartToTarget
            (
                new UnitInfo(1m), new UnitPart(parts2[0]),
                new UnitPart(parts2[1])
            );

            //Firstly, note that GetUnitPartsConversion might have affected the exponent. For example: in m4/L2,
            //both exponents have to be modified to reach the convertible m3/L.
            //Secondly, bear in mind that parts2 exponents are always positive.
            
            int sign = unitInfo.Parts[i].Exponent / Math.Abs(unitInfo.Parts[i].Exponent);
            int exponent = sign * unitInfo.Parts[i].Exponent / parts2[0].Exponent;
            int outExponent = exponent;
            if (exponent != 1m)
            {
                tempInfo = RaiseToIntegerExponent(tempInfo, exponent);
            }

            if (Math.Abs(unitInfo.Parts[i].Exponent) > Math.Abs(parts2[0].Exponent * exponent))
            {
                exponent = sign * unitInfo.Parts[i].Exponent - parts2[0].Exponent * exponent;
                if (exponent > 0)
                {
                    //Account for a case like m4 converted to litre where 1 metre is left
                    //uncompensated.
                    UnitPart newPart = new UnitPart
                    (
                        parts2[0].Unit, parts2[0].Prefix.Factor, sign * exponent
                    );
                    unitInfo.Parts.Add(newPart);

                    if (!unitInfo.InitialPositions.ContainsKey(newPart))
                    {
                        unitInfo.InitialPositions.Add
                        (
                            newPart, unitInfo.InitialPositions.Max(x => x.Value) + 1
                        );
                    }
                }
            }

            if (sign == -1) tempInfo = 1m / tempInfo;
            outExponent = sign * outExponent;

            unitInfo = unitInfo * tempInfo;
            unitInfo.Parts[i].Unit = parts2[1].Unit;
            //outExponent indicates the number of times which the target exponent is
            //repeated to match the original unit. For example: in L to m3, the final
            //unit is m and outExponent is 1 (= the original unit contains 1 target 
            //unit/exponent); but the output exponent should be 3 (1 * target exponent).
            unitInfo.Parts[i].Exponent = outExponent * parts2[1].Exponent;

            return unitInfo;
        }

        private static UnitPart[] GetUnitPartsConversion(UnitPart part1, UnitPart part2)
        {
            UnitPart[] unitParts = new UnitPart[] 
            {
                //Exponent signs will be managed at a later stage.
                new UnitPart(part1) { Exponent = Math.Abs(part1.Exponent) },
                new UnitPart(part2) { Exponent = Math.Abs(part2.Exponent) }
            };

            UnitPart[] tempParts = GetUnitPartsConversionSameType(unitParts);
            if (tempParts != null) return tempParts;

            int count = 0;
            while (count < 2)
            {
                count = count + 1;
                int i = 0;
                int i2 = 1;
                if (count == 2)
                {
                    i = 1;
                    i2 = 0;
                }

                for (int i11 = unitParts[i].Exponent; i11 > 0; i11--)
                {
                    unitParts[i].Exponent = i11;
                    for (int i22 = unitParts[i2].Exponent; i22 > 0; i22--)
                    {
                        unitParts[i2].Exponent = i22;
                        tempParts = GetUnitPartsConversionSameType(unitParts);
                        if (tempParts != null)
                        {
                            //Accounts for scenarios on the lines of m3/L2.
                            return tempParts;
                        }
                    }
                }
            }

            return null;
        }

        private static UnitPart[] GetUnitPartsConversionSameType(UnitPart[] unitParts)
        {
            if (GetTypeFromUnitInfo(new UnitInfo(unitParts[0].Unit, 1m)) == GetTypeFromUnitInfo(new UnitInfo(unitParts[1].Unit, 1m)))
            {
                unitParts[0].Exponent = 1;
                unitParts[1].Exponent = 1;

                return unitParts;     
            }
            else if (GetTypeFromUnitPart(unitParts[0]) == GetTypeFromUnitPart(unitParts[1]))
            {
                return unitParts;
            }
            else if (GetTypeFromUnitPart(unitParts[0], true) == GetTypeFromUnitPart(unitParts[1], true))
            {
                //Cases like m2/ft3 recognised as length. Exponents are being managed later.
                unitParts[0].Exponent = 1;
                unitParts[1].Exponent = 1;

                return unitParts;
            }

            return null;
        }

        private static UnitInfo ExpandNonBasicCompoundToUnitPart(UnitInfo unitInfo, UnitInfo partInfo, int i)
        {
            return AddExpandedUnitPart
            (
                unitInfo, i,
                AllNonBasicCompounds[partInfo.Unit].ToList()
            );
        }
        private static UnitInfo ExpandBasicCompoundToUnitPart(UnitInfo unitInfo, UnitInfo partInfo, int i)
        {
            UnitTypes nonDividableType = 
            (
                AllNonDividableUnits.Contains(unitInfo.Parts[i].Unit) ?
                partInfo.Type : UnitTypes.None
            );

            for (int i2 = 0; i2 < AllCompounds[partInfo.Type].Length; i2++)
            {
                Compound compound = AllCompounds[partInfo.Type][i2];
                if (AllCompounds[partInfo.Type][i2].Parts.Count < 2)
                {
                    //The whole point here is expanding.
                    continue;
                }

                if (nonDividableType == UnitTypes.None || compound.Parts.FirstOrDefault(x => x.Type == nonDividableType) != null)
                {
                    unitInfo = AddExpandedUnitPart
                    (
                        unitInfo, i, GetUnitPartsFromBasicCompound
                        (
                            AllCompounds[partInfo.Type][i2],
                            AllBasicSystems[unitInfo.System]
                        )
                    );
                    break;
                }
            }

            return unitInfo;
        }

        private static UnitInfo AddExpandedUnitPart(UnitInfo unitInfo, int i, List<UnitPart> compoundUnitParts)
        {
            //The prefix of the original unit is added to the first unit part.
            bool firstTime = true;
            UnitSystems basicSystem = AllBasicSystems[unitInfo.System];
            List<UnitPart> newParts = new List<UnitPart>();

            foreach (UnitPart part in compoundUnitParts)
            {
                UnitInfo newPrefixInfo = new UnitInfo(Units.None, part.Prefix.Factor);

                if (firstTime && unitInfo.Parts[i].Prefix.Factor != 1m)
                {
                    //Finding the most adequate new prefix isn't required at this point.
                    newPrefixInfo = PerformManagedOperationValues
                    (
                        unitInfo.Parts[i].Prefix.Factor, part.Prefix.Factor,
                        Operations.Multiplication
                    );

                    if (newPrefixInfo.Value != 1 || newPrefixInfo.BaseTenExponent != 0)
                    {
                        unitInfo = unitInfo * newPrefixInfo;
                    }
                }

                newParts.Add
                (
                    new UnitPart
                    (
                        part.Unit, newPrefixInfo.Prefix.Factor,
                        part.Exponent * unitInfo.Parts[i].Exponent
                    )
                );

                firstTime = false;
            }

            return AddNewUnitParts(unitInfo, newParts, i);
        }

        private static UnitInfo AddNewUnitParts(UnitInfo unitInfo, List<UnitPart> newParts, int i = -1)
        {
            if (newParts.Count < 1) return unitInfo;

            int i2 = i;
            if (i2 == -1)
            {
                i2 =
                (
                    unitInfo.InitialPositions.Count == 0 ? 0 :
                    unitInfo.InitialPositions.Max(x => x.Value) + 1
                );
            }

            foreach (UnitPart part in newParts)
            {
                unitInfo.Parts.Add(part);
                if (!unitInfo.InitialPositions.ContainsKey(part))
                {
                    unitInfo.InitialPositions.Add(part, i2);
                }
            }

            return
            (
                i == -1 ? unitInfo : //i == -1 means that old parts aren't being replaced.
                RemoveUnitPart(unitInfo, unitInfo.Parts[i])
            );
        }

        private static UnitInfo UpdateDifferentPrefixParts(UnitInfo unitInfo, int i, int i2)
        {
            //Example: m/cm is converted into 0.01 m/m where 0.01 isn't stored as a part but globally.
            UnitInfo newInfo = GetNewPrefixUnitPart(unitInfo, i, i2);
            newInfo = PerformManagedOperationUnits
            (
                newInfo, unitInfo.Prefix.Factor,
                Operations.Multiplication
            );

            unitInfo = PerformManagedOperationValues
            (
                unitInfo, newInfo, Operations.Multiplication
            );

            unitInfo.Parts[i].Prefix = new Prefix(unitInfo.Parts[i].Prefix.PrefixUsage);
            unitInfo.Parts[i].Exponent = unitInfo.Parts[i].Exponent + unitInfo.Parts[i2].Exponent;

            return unitInfo;
        }

        private static UnitInfo GetNewPrefixUnitPart(UnitInfo unitInfo, int i, int i2)
        {
            UnitInfo info1 = RaiseToIntegerExponent
            (
                unitInfo.Parts[i].Prefix.Factor,
                unitInfo.Parts[i].Exponent
            );

            UnitInfo info2 = RaiseToIntegerExponent
            (
                unitInfo.Parts[i2].Prefix.Factor,
                unitInfo.Parts[i2].Exponent
            );

            return info1 * info2;
        }
    }
}
