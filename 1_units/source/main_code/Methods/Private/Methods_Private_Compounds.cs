﻿using System;
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
                foreach (var compoundUnit in AllUnitTypes.Where(x => x.Value == type && UnitIsCompound(x.Key)))
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
                        outUnitInfo = UpdateMainUnitVariables(outUnitInfo);
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

        private static List<UnitPart> GetBasicCompoundUnitParts(UnitTypes type, UnitSystems system)
        {
            if (AllBasicCompounds.ContainsKey(type) && AllBasicCompounds[type].ContainsKey(system))
            {
                return GetCompoundUnitParts(AllBasicCompounds[type][system], true);
            }

            return new List<UnitPart>();
        }

        private static List<UnitPart> GetCompoundUnitParts(Units unit, bool basicCompound)
        {
            List<UnitPart> unitParts = new List<UnitPart>();

            if (basicCompound)
            {
                UnitTypes type2 = GetTypeFromUnit(unit);
                UnitSystems system2 = GetSystemFromUnit(unit);

                if (AllBasicCompounds.ContainsKey(type2) && AllBasicCompounds[type2].ContainsKey(system2))
                {
                    foreach (CompoundPart compoundPart in AllCompounds[type2][0].Parts)
                    {
                        BasicUnit basicUnit = AllBasicUnits[compoundPart.Type][system2];

                        unitParts.Add
                        (
                            new UnitPart
                            (
                                basicUnit.Unit, basicUnit.PrefixFactor,
                                compoundPart.Exponent
                            )
                        );
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

            if (UnitIsCompound(unitInfo.Unit))
            {
                canBeUsed = AllCompoundsUsingPrefixes.Contains(unitInfo.Unit);
            }
            else if (unitInfo.Parts.Count > 1) canBeUsed = false;

            return canBeUsed;
        }

        private static bool UnitIsCompound(Units unit)
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

            //Better starting with the non-basic compounds because of being less misinterpretation-prone.
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
                if (compound.Value.Length == unitInfo.Parts.Count)
                {
                    if (UnitPartListsAreEqual(unitInfo.Parts, compound.Value.ToList()))
                    {
                        unitInfo.Unit = compound.Key;
                        unitInfo.Type = AllUnitTypes[unitInfo.Unit];
                        unitInfo.System = AllUnitSystems[unitInfo.Unit];

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
                unitInfo.Type = GetTypeFromUnitInfo(unitInfo).Type;
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

        private static UnitInfo GetSystemFromUnitInfo(UnitInfo unitInfo)
        {
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
                    if (unitInfo.System == UnitSystems.None) unitInfo.System = system2;
                    else if (unitInfo.System != system2) break;
                }
            }

            if (unitInfo.System == UnitSystems.None && allNeutral && neutralSystems.Count > 0)
            {
                //When all the units are "neutral", their defining system cannot be ignored.
                unitInfo.System = neutralSystems.GroupBy(x => x).OrderByDescending(x => x.Count()).First().Key;
            }

            if (unitInfo.System == UnitSystems.None)
            {
                unitInfo.System = GetSystemFromUnit(unitInfo.Unit);
            }

            return unitInfo;
        }

        private static List<UnitPart> GetUnitPartsFromBasicCompound(Compound compound, UnitSystems system)
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
                        basic.Unit, basic.PrefixFactor, compoundPart.Exponent
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
                unitInfo.Type = GetTypeFromUnitInfo(unitInfo).Type;
            }

            return
            (
                unitInfo.Type == UnitTypes.None ? unitInfo :
                GetBasicCompoundUnit(unitInfo)
            );
        }

        private static UnitInfo GetBasicCompoundUnit(UnitInfo unitInfo)
        {
            unitInfo.Unit = AllUnnamedUnits[unitInfo.System];
            if (unitInfo.System == UnitSystems.None) return unitInfo;

            Units basicCompound =
            (
                AllBasicCompounds[unitInfo.Type].ContainsKey(unitInfo.System) ?
                AllBasicCompounds[unitInfo.Type][unitInfo.System] :
                Units.None
            );

            if (basicCompound == Units.None) return unitInfo;

            UnitSystems basicSystem = AllBasicSystems[unitInfo.System];
            List<UnitPart> basicUnitParts = GetBasicCompoundUnitParts(unitInfo.Type, basicSystem);
            if (basicUnitParts.Count != unitInfo.Parts.Count) return unitInfo;

            if (UnitPartsMatchCompoundUnitParts(unitInfo, basicUnitParts, true))
            {
                unitInfo.Unit = basicCompound;
                if (!UnitPartsMatchCompoundUnitParts(unitInfo, basicUnitParts))
                {
                    //Some prefixes differ from the basic configuration.
                    unitInfo = AdaptPrefixesToMatchBasicCompound(unitInfo, basicUnitParts);
                }
            }
            else ConvertUnmatchedUnitPartsCompound(unitInfo, basicUnitParts);

            return unitInfo;
        }

        private static UnitInfo ConvertUnmatchedUnitPartsCompound(UnitInfo unitInfo, List<UnitPart> basicUnitParts)
        {
            return PerformUnitPartConversion
            (
                unitInfo, new UnitInfo()
                { 
                    Parts = new List<UnitPart>(basicUnitParts) 
                }
            );
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

            if (allPrefixes.Value != 1.0m)
            {
                allPrefixes = NormaliseUnitInfo(allPrefixes);

                if (allPrefixes.BigNumberExponent == 0)
                {
                    unitInfo = PerformManagedOperationValues
                    (
                        unitInfo, allPrefixes, Operations.Multiplication
                    );
                }
                else
                {
                    unitInfo = GetBestPrefixForTarget
                    (
                        unitInfo, allPrefixes.BigNumberExponent,
                        //There is no basic compound relying on binary prefixes.
                        PrefixTypes.SI
                    ); 
                }
            }

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
            unitInfo = ExpandUnitParts(unitInfo);
            unitInfo = SimplifyUnitParts(unitInfo);

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

            if (unitInfo.InitialPositions == null)
            {
                unitInfo.InitialPositions = new Dictionary<UnitPart, int>();
            }

            if (unitInfo.InitialPositions.Count == 0)
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
            unitInfo.Type = GetTypeFromUnitInfo(unitInfo).Type;
            
            return
            (
                !AllNonDividableUnits.Contains(unitInfo.Unit) && 
                (
                    AllCompounds.ContainsKey(unitInfo.Type) ||
                    AllNonBasicCompounds.ContainsKey(unitInfo.Unit)
                )
            );
        }

        private static UnitInfo SimplifyUnitParts(UnitInfo unitInfo, List<int> initialPositions = null)
        {
            if (unitInfo.Parts.Count < 1) return unitInfo;

            for (int i = unitInfo.Parts.Count - 1; i >= 0; i--)
            {
                if (unitInfo.Parts[i].Unit == Units.None || unitInfo.Parts[i].Unit == Units.Unitless)
                {
                    continue;
                }

                for (int i2 = i - 1; i2 >= 0; i2--)
                {
                    if (unitInfo.Parts[i].Unit == unitInfo.Parts[i2].Unit)
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

                        unitInfo = RemoveUnitPart(unitInfo, unitInfo.Parts[i2]);
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

        private static UnitInfo ExpandNonBasicCompoundToUnitPart(UnitInfo unitInfo, UnitInfo partInfo, int i)
        {
            return AddExpandedUnitPart
            (
                unitInfo, i, 
                AllNonBasicCompounds[unitInfo.Unit].ToList()
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
                PrefixTypes prefixType = 
                (
                    unitInfo.Prefix.Type != PrefixTypes.None ?
                    unitInfo.Prefix.Type : PrefixTypes.SI
                );

                if (firstTime && unitInfo.Parts[i].Prefix.Factor != 1m)
                {
                    newPrefixInfo = NormaliseUnitInfo
                    (
                        PerformManagedOperationValues
                        (
                            unitInfo.Parts[i].Prefix.Factor, part.Prefix.Factor, 
                            Operations.Multiplication
                        )
                    );

                    newPrefixInfo = GetBestPrefixForTarget
                    (
                        newPrefixInfo, newPrefixInfo.BigNumberExponent, prefixType
                    );

                    if (newPrefixInfo.Value != 1 || newPrefixInfo.BigNumberExponent != 0)
                    {
                        unitInfo = PerformManagedOperationValues
                        (
                            unitInfo, new UnitInfo(newPrefixInfo) { Prefix = new Prefix() }, 
                            Operations.Multiplication
                        );
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

            return AddNewParts(unitInfo, i, newParts);
        }

        private static UnitInfo AddNewParts(UnitInfo unitInfo, int i, List<UnitPart> newParts)
        {
            if (newParts.Count < 1) return unitInfo;

            foreach (UnitPart part in newParts)
            {
                unitInfo.Parts.Add(part);
                if (!unitInfo.InitialPositions.ContainsKey(part))
                {
                    unitInfo.InitialPositions.Add(part, i);
                }
            }
            unitInfo = RemoveUnitPart(unitInfo, unitInfo.Parts[i]);

            return unitInfo;
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

            return PerformManagedOperationUnits
            (
                info1, info2, Operations.Multiplication
            );
        }
    }
}