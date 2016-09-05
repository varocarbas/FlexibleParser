using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    public partial class UnitP
    {
        private static UnitInfo ConvertUnit(UnitInfo originalInfo, UnitInfo targetInfo, bool isInternal = true)
        {
            return PerformConversion
            (
                originalInfo, targetInfo, isInternal
            );
        }

        private static UnitInfo PerformConversion(UnitInfo originalInfo, UnitInfo targetInfo, bool isInternal = true, bool inverseOutputs = false)
        {
            ErrorTypes errorType = GetConversionError(originalInfo, targetInfo);
            if (errorType != ErrorTypes.None)
            {
                return new UnitInfo(originalInfo)
                {
                    Error = new ErrorInfo(errorType)
                };
            }

            UnitInfo targetInfo2 = NormaliseTargetUnit(targetInfo, isInternal);
            UnitInfo outInfo = AccountForTargetUnitPrefixes(originalInfo, targetInfo2);

            bool convertIt =
            (
                outInfo.Unit != targetInfo2.Unit ||
                (
                    IsUnnamedUnit(originalInfo.Unit) &&
                    IsUnnamedUnit(targetInfo.Unit)
                )
            );

            if (convertIt)
            {
                UnitInfo tempInfo =
                (
                    UnitsCanBeConvertedDirectly(outInfo, targetInfo2) ?
                    ConvertUnitValue(outInfo, targetInfo2, inverseOutputs) :
                    PerformUnitPartConversion(outInfo, targetInfo2, isInternal)
                );
                if (tempInfo.Error.Type != ErrorTypes.None)
                {
                    return tempInfo;
                }

                outInfo = new UnitInfo(targetInfo);
                outInfo.Prefix = new Prefix(outInfo.Prefix.PrefixUsage);
                outInfo.Value = tempInfo.Value;
                outInfo.BaseTenExponent = tempInfo.BaseTenExponent;

                if (!isInternal)
                {
                    //When the target unit has a prefix, the conversion is adapted to it (e.g., lb to kg is 0.453).
                    //Such an output isn't always desirable (kg isn't a valid unit, but g + kilo).
                    //That's why the output value has to be multiplied by the prefix when reaching this point 
                    //(i.e., conversion delivered to the user).
                    outInfo = outInfo * targetInfo.Prefix.Factor;
                }
            }

            return outInfo;
        }


        private static UnitInfo NormaliseTargetUnit(UnitInfo targetInfo, bool isInternal)
        {
            UnitInfo outInfo = new UnitInfo(targetInfo);

            if (isInternal)
            {
                //The only relevant part of the target unit value should always be the prefix.
                //But this isn't always compatible with external conversions, where the target
                //unit might have been parsed and made the other values relevant too (e.g., conversion).
                outInfo.Value = 1;
                outInfo.BaseTenExponent = 0;
            }

            return NormaliseUnitInfo(outInfo);
        }

        //This function expects targetInfo2 to be normalised.
        private static UnitInfo AccountForTargetUnitPrefixes(UnitInfo originalInfo, UnitInfo targetInfo2)
        {
            int newExponent = GetBaseTenExponentIncreasePrefixes
            (
                originalInfo, targetInfo2.BaseTenExponent
            );

            return NormaliseUnitInfo
            (
                new UnitInfo(originalInfo)
                {
                    BaseTenExponent = newExponent,
                    //Prefix.Factor is already included in newExponent.
                    Prefix = new Prefix(originalInfo.Prefix.PrefixUsage)
                }
            );
        }

        private static int GetBaseTenExponentIncreasePrefixes(UnitInfo originalInfo, int targetInfo2Exp)
        {
            //targetInfo2 is being normalised by only accounting for the value information which is 
            //relevant to the conversion (i.e., the prefix).
            UnitInfo originalTemp = new UnitInfo(originalInfo) { Value = 1m, BaseTenExponent = 0 };
            originalTemp = NormaliseUnitInfo(originalTemp);

            return originalTemp.BaseTenExponent - targetInfo2Exp;
        }

        //Determines whether the conversion might be performed directly. That is: by only considering the
        //main unit information (i.e., Units enum member), rather than its constituent parts.
        private static bool UnitsCanBeConvertedDirectly(UnitInfo original, UnitInfo target)
        {
            if (original.Unit != Units.None && original.Unit != Units.Unitless && !IsUnnamedUnit(original.Unit))
            {
                if (target.Unit != Units.None && target.Unit != Units.Unitless && !IsUnnamedUnit(target.Unit))
                {
                    return true;
                }
            }

            return false;
        }

        private static UnitInfo PerformUnitPartConversion(UnitInfo convertInfo, UnitInfo target, bool isInternal = true)
        {
            Dictionary<UnitPart, UnitPart> allParts = GetAllUnitPartDict
            (
                convertInfo.Parts, target.Parts
            );

            return
            (
                allParts.Count == 0 ? new UnitInfo(convertInfo) 
                { 
                    Error = new ErrorInfo(ErrorTypes.InvalidUnitConversion) 
                } 
                : ConvertAllUnitParts
                (
                    convertInfo, allParts, isInternal
                )
            );
        }

        private static UnitInfo ConvertAllUnitParts(UnitInfo convertInfo, Dictionary<UnitPart, UnitPart> allParts, bool isInternal = true)
        {
            foreach (var item in allParts)
            {
                convertInfo = ConvertUnitPartToTarget
                (
                    convertInfo, item.Key, item.Value, isInternal
                );

                if (convertInfo.Error.Type != ErrorTypes.None)
                {
                    return convertInfo;
                }
            }

            return convertInfo;
        }

        private static UnitInfo ConvertUnitPartToTarget(UnitInfo outInfo, UnitPart originalPart, UnitPart targetPart, bool isInternal = true)
        {
            ErrorTypes errorType = GetUnitPartConversionError(originalPart, targetPart);
            if (errorType != ErrorTypes.None)
            {
                outInfo.Error = new ErrorInfo(errorType);
                return outInfo;
            }

            UnitPart originalPart2 = new UnitPart(originalPart);
            UnitPart targetPart2 = new UnitPart(targetPart);

            int exponent2 = 1;
            if (originalPart2.Exponent == targetPart2.Exponent)
            {
                if (!isInternal)
                {
                    //In the internal calculations, exponents might be relevant or not when performing
                    //a unit part conversion; this issue is being managed by the code calling this function.
                    //With conversions delivered to the user, exponents have to be brought into account.
                    //NOTE 1: isInternal isn't passed to PerformConversion because the associated modifications
                    //only make sense for the main prefix (not what this is about).
                    //NOTE 2: both parts have the same type and that's why there is only two possible scenarios.
                    //Either same exponent or different exponent which should be ignored (e.g., L & m3).
                    exponent2 = Math.Abs(targetPart2.Exponent);
                }
                //In this situation, exponents don't need to be considered and it is better ignoring them during
                //the conversion to avoid problems.
                //For example: the part m2 has associated a specific unit (SquareMetre), but it might be converted
                //into units which don't have one, like ft2.
                originalPart2.Exponent = 1;
                targetPart2.Exponent = 1;
            }
            //Different exponents cannot be removed. For example: conversion between litre and m3, where the exponent
            //does define the unit. 

            UnitInfo info2 = PerformConversion
            (
                AdaptPartToConversion(originalPart2, originalPart.Exponent),
                AdaptPartToConversion(targetPart2, targetPart.Exponent), true,
                //The original part being in the denominator means that the output value has to be inverted.
                //Note that this value is always expected to modify the main value (= in the numerator).
                //This is the only conversion where such a scenario is being considered; but the information
                //is passed to PerformConversion anyway to ensure the highest accuracy. Even the decimal type
                //can output noticeable differences in cases like 1/(val1/val2) vs. val2/val1.
                originalPart.Exponent / Math.Abs(originalPart.Exponent) == -1
            );

            return
            (
                info2.Error.Type != ErrorTypes.None ? info2 :
                outInfo *
                (
                    exponent2 == 1 ? info2 :
                    RaiseToIntegerExponent(info2, exponent2)
                )
            );
        }

        private static UnitInfo AdaptPartToConversion(UnitPart unitPart, int exponent)
        {
            UnitInfo outInfo = UnitPartToUnitInfo(unitPart, 1m);
            if (outInfo.Unit == Units.Centimetre)
            {
                //To avoid inconsistencies with individual unit conversions.
                outInfo.Unit = Units.Metre;
                outInfo.BaseTenExponent -= 2; 
            }

            if (unitPart.Prefix.Factor != 1 && exponent != 1)
            {
                UnitInfo prefixInfo = RaiseToIntegerExponent
                (
                    unitPart.Prefix.Factor, exponent
                );

                outInfo.Prefix = new Prefix();
                outInfo = outInfo * prefixInfo;
            }

            return outInfo;
        }

        //Relates all the original/target unit parts between each other in order to facilitate the subsequent unit conversion.
        private static Dictionary<UnitPart, UnitPart> GetAllUnitPartDict(List<UnitPart> originals, List<UnitPart> targets, bool findCommonPartMatches = true)
        {
            Dictionary<UnitPart, UnitPart> outParts = new Dictionary<UnitPart, UnitPart>();

            foreach (UnitPart original in originals)
            {
                UnitTypes type = GetTypeFromUnitPart(original);

                var target = targets.FirstOrDefault
                (
                    x => GetTypeFromUnitPart(x) == type
                );
                if (target == null || target.Unit == Units.None) continue;

                outParts.Add(original, target);
            }

            if (outParts.Count < Math.Max(originals.Count, targets.Count))
            {
                if (findCommonPartMatches)
                {
                    outParts = GetUnitPartDictInCommon
                    (
                        new ConversionItems(originals, targets)
                    );
                }
                else outParts = new Dictionary<UnitPart, UnitPart>();
            }

            return outParts;
        }

        //In some cases, the GetAllUnitPartDict approach doesn't work. For example: BTU/s and W. 
        //Note that ignoring the dividable/non-dividable differences right away (useful in other situations) isn't good here.
        //The only solution is looking for common parts to both units (always the case, by bearing in mind that have the same type). 
        //In the aforementioned example of BTU/s to W, the two pairs BTU-J and s-s will be returned.
        private static Dictionary<UnitPart, UnitPart> GetUnitPartDictInCommon(ConversionItems conversionItems)
        {
            conversionItems = GetNonDividableUnitPartDictInCommon(conversionItems);

            if (conversionItems.OutDict.Count == 0 || (conversionItems.Originals.Count == conversionItems.Targets.Count && conversionItems.Originals.Count == 0))
            {
                //Reaching here and not finding a single non-dividable match is a clear error (managed by the calling function).
                //On the other hand, no originals/targets left would mean that no further analysis is required.
                return conversionItems.OutDict;
            }

            //Trying to match the remaining parts (i.e., individual units not matching any non-dividable compound).
            Dictionary<UnitPart, UnitPart> outDict2 = GetAllUnitPartDict
            (
                conversionItems.Originals, conversionItems.Targets, false
            );
            if (outDict2.Count == 0) return outDict2;

            foreach (var item2 in outDict2)
            {
                conversionItems.OutDict.Add(item2.Key, item2.Value);
            }

            return conversionItems.OutDict;
        }

        //Method looking for proper matches for each non-dividable compound. For example, BTU might be
        //matched with kg*m/s2 (= N).
        private static ConversionItems GetNonDividableUnitPartDictInCommon(ConversionItems conversionItems)
        {
            int count = 0;
            while (count < 2 && (conversionItems.Originals.Count > 0 && conversionItems.Targets.Count > 0))
            {
                count = count + 1;
                if (count == 1)
                {
                    conversionItems.Others = conversionItems.Targets;
                    conversionItems.NonDividables = GetNonDividableParts(conversionItems.Originals);
                }
                else
                {
                    conversionItems.Others = conversionItems.Originals;
                    conversionItems.NonDividables = GetNonDividableParts(conversionItems.Targets);
                }

                for (int i = conversionItems.NonDividables.Count - 1; i >= 0; i--)
                {
                    if (conversionItems.Others.Count == 0) return new ConversionItems();

                    conversionItems = MatchNonDividableParts(conversionItems, i);
                    if (conversionItems.TempPair.Key == null || conversionItems.TempPair.Key.Unit == Units.None)
                    {
                        return new ConversionItems();
                    }

                    //A proper match for the non-divididable part was found.
                    //That is, a set of common parts in others which also form a valid compound. 
                    conversionItems = UpdateConversionItems(conversionItems, count, i);
                }
            }

            return conversionItems;
        }

        private static ConversionItems UpdateConversionItems(ConversionItems conversionItems, int count, int i)
        {
            conversionItems.NonDividables.RemoveAt(i);

            //Originals & Targets are automatically updated with any modification in Others.
            //Not the case with NonDividables, that's why having to call this method.
            conversionItems = RemoveNonDividableOriginals(conversionItems, count);

            return UpdateOutDict(conversionItems, count);
        }
        
        private static ConversionItems UpdateOutDict(ConversionItems conversionItems, int count)
        {
            if (count == 1)
            {
                conversionItems.OutDict.Add
                (
                    conversionItems.TempPair.Key, conversionItems.TempPair.Value
                );
            }
            else
            {
                conversionItems.OutDict.Add
                (
                    conversionItems.TempPair.Value, conversionItems.TempPair.Key
                );
            }

            return conversionItems;
        }

        private static ConversionItems RemoveNonDividableOriginals(ConversionItems conversionItems, int count)
        {
            List<UnitPart> nonOriginals =
            (
                count == 1 ? conversionItems.Originals : conversionItems.Targets
            );

            for (int i = nonOriginals.Count - 1; i >= 0; i--)
            {
                if (!AllNonDividableUnits.Contains(nonOriginals[i].Unit)) continue;

                if (!conversionItems.NonDividables.Contains(nonOriginals[i]))
                {
                    nonOriginals.RemoveAt(i);
                }
            }

            return conversionItems;
        }

        //Method trying to match each item of conversionItems.NonDividables with parts of conversionItems.Others. 
        //Bear in mind that the goal isn't just looking for sets of unit parts defining the given type; they have 
        //also to be associated with a valid compound unit. For example: when trying to match the force unit lbf,
        //lb*ft/s2 wouldn't a good match; these parts do define a force, but not a supported unit. A good match
        //would be kg*m/s2, which also defines the supported unit newton.
        private static ConversionItems MatchNonDividableParts(ConversionItems conversionItems, int i)
        {
            conversionItems.TempPair = new KeyValuePair<UnitPart, UnitPart>();

            var matchedPart = conversionItems.Others.FirstOrDefault(x => x.Unit == conversionItems.NonDividables[i].Unit);
            if (matchedPart != null)
            {
                conversionItems.Others.Remove(matchedPart);
                conversionItems.TempPair = new KeyValuePair<UnitPart, UnitPart>
                (
                    conversionItems.NonDividables[i], matchedPart
                );
            }

            List<UnitPart> parts2 = new List<UnitPart>();
            foreach (var nonPart in AllCompounds[GetTypeFromUnitPart(conversionItems.NonDividables[i])][0].Parts)
            {
                matchedPart = conversionItems.Others.FirstOrDefault
                (
                    x => nonPart.Type == GetTypeFromUnit(x.Unit)
                );
                if (matchedPart == null)
                {
                    return conversionItems;
                }

                //Exponent of the corresponding conversionItems.Others part after having
                //removed the current nonPart. This exponent doesn't have to match the one
                //of the associated type (i.e., the one being stored in parts2).
                int exponent = matchedPart.Exponent -
                (
                    Math.Sign(conversionItems.NonDividables[i].Exponent) * nonPart.Exponent
                );

                parts2.Add
                (
                    new UnitPart(matchedPart.Unit, nonPart.Exponent)
                );
                conversionItems.Others.Remove(matchedPart);

                if (exponent != 0)
                {
                    conversionItems.Others.Add
                    (
                        new UnitPart
                        (
                            matchedPart.Unit, 
                            matchedPart.Prefix.Factor,
                            exponent 
                        )
                    );
                }
            }

            UnitInfo tempInfo = GetCompoundUnitFromParts
            (
                GetPartsFromUnit
                (
                    new UnitInfo() { Parts = new List<UnitPart>(parts2) }
                )
            );

            if (tempInfo.Unit != Units.None)
            {
                //Condition avoiding situations like assuming that lb*ft/s2 & BTU are identical.

                conversionItems.TempPair = new KeyValuePair<UnitPart, UnitPart>
                (
                    conversionItems.NonDividables[i], new UnitPart
                    (
                        tempInfo.Unit, tempInfo.Prefix.Factor,
                        conversionItems.NonDividables[i].Exponent
                    )
                );
            }

            return conversionItems;
        }

        private static List<UnitPart> GetNonDividableParts(List<UnitPart> iniList)
        {
            List<UnitPart> outList = iniList.Where
            (
                x => AllNonDividableUnits.Contains(x.Unit)
            )
            .ToList();

            Dictionary<UnitPart, UnitTypes> types = new Dictionary<UnitPart, UnitTypes>();
            for (int i = outList.Count - 1; i >= 0; i--)
            {
                UnitTypes type = GetTypeFromUnitPart(outList[i]);
                if (!AllCompounds.ContainsKey(type))
                {
                    //Theoretically, this shouldn't ever happen because AllNonDividableUnits
                    //is expected to only contain compounds. On the other hand, an error here
                    //wouldn't be that weird and, in any case, not too influential. That's why
                    //this in-the-safest-scenario check is acceptable.
                    outList.RemoveAt(i);
                }
                else types.Add(outList[i], type);
            }

            return outList.OrderByDescending
            (
                x => AllCompounds[types[x]][0].Parts.Count
            )
            .ToList();
        }

        //The prefixes of both units are being managed before reaching this point.
        private static UnitInfo ConvertUnitValue(UnitInfo original, UnitInfo target, bool inverseOutputs = false)
        {
            if
            (
                original.Unit == Units.None || original.Unit == Units.Unitless ||
                target.Unit == Units.Unitless || target.Unit == Units.Unitless
            )
            {
                original.Error = new ErrorInfo(ErrorTypes.InvalidUnit);
                return original;
            }

            //Both units have the same type.
            if (original.Type != UnitTypes.None && SpecialConversionTypes.Contains(original.Type))
            {
                UnitInfo tempInfo = ConvertUnitValueSpecial(original, target);
                return inverseOutputs ? 1m / tempInfo : tempInfo;
            }
            else
            {
                UnitInfo convFactor =
                (
                    inverseOutputs ?
                    GetUnitConversionFactor(target.Unit) / GetUnitConversionFactor(original.Unit) :
                    GetUnitConversionFactor(original.Unit) / GetUnitConversionFactor(target.Unit)
                );

                return original * convFactor;
            }
        }

        private static UnitInfo GetUnitConversionFactor(Units unit)
        {
            return
            (
                AllUnitConversionFactors[unit] < 0 ?
                AllBeyondDecimalConversionFactors[AllUnitConversionFactors[unit]] :
                new UnitInfo(AllUnitConversionFactors[unit])
            );
        }

        //Takes care of conversions which do not rely on conversion factors.
        private static UnitInfo ConvertUnitValueSpecial(UnitInfo original, UnitInfo target)
        {
            UnitInfo convertInfo = new UnitInfo(original);

            if (original.Type == UnitTypes.Temperature)
            {
                convertInfo = ConvertTemperature(convertInfo, target.Unit);
            }

            return convertInfo;
        }

        private static UnitInfo ConvertTemperature(UnitInfo outInfo, Units targetUnit)
        {
            if (targetUnit == Units.Kelvin)
            {
                outInfo = ConvertTemperatureToKelvin(outInfo);
            }
            else if (outInfo.Unit == Units.Kelvin)
            {
                outInfo = ConvertTemperatureFromKelvin(new UnitInfo(outInfo) { Unit = targetUnit });
            }
            else
            {
                outInfo = ConvertTemperatureToKelvin(new UnitInfo(outInfo));
                outInfo = ConvertTemperatureFromKelvin(new UnitInfo(outInfo) { Unit = targetUnit });
            }

            return outInfo;
        }

        private static UnitInfo ConvertTemperatureToKelvin(UnitInfo outInfo)
        {
            if (outInfo.Unit == Units.Celsius)
            {
                outInfo = outInfo + 273.15m;
            }
            else if (outInfo.Unit == Units.Fahrenheit)
            {
                outInfo = (outInfo + 459.67m) / 1.8m;
            }
            else if (outInfo.Unit == Units.Rankine)
            {
                outInfo = outInfo / 1.8m;
            }

            return outInfo;
        }

        private static UnitInfo ConvertTemperatureFromKelvin(UnitInfo outInfo)
        {
            if (outInfo.Unit == Units.Celsius)
            {
                outInfo = outInfo - 273.15m;
            }
            else if (outInfo.Unit == Units.Fahrenheit)
            {
                outInfo = 1.8m * outInfo - 459.67m;
            }
            else if (outInfo.Unit == Units.Rankine)
            {
                outInfo = 1.8m * outInfo;
            }

            return outInfo;
        }
    }

    //Class exclusively meant to ease the communication between different functions in one of the unit part
    //conversion scenarios.
    internal class ConversionItems
    {
        public List<UnitPart> Originals, Targets, NonDividables, Others;
        public Dictionary<UnitPart, UnitPart> OutDict;
        public KeyValuePair<UnitPart, UnitPart> TempPair;

        public ConversionItems() : this(new List<UnitPart>(), new List<UnitPart>()) { }
        public ConversionItems(List<UnitPart> originals, List<UnitPart> targets)
        {
            Originals = new List<UnitPart>(originals);
            Targets = new List<UnitPart>(targets);
            OutDict = new Dictionary<UnitPart, UnitPart>();
            NonDividables = new List<UnitPart>();
            Others = new List<UnitPart>();
        }
    }
}
