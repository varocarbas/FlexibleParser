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
                } : 
                ConvertAllUnitParts
                (
                    convertInfo,
                    GetAllUnitPartDict(convertInfo.Parts, target.Parts),
                    isInternal
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
        private static Dictionary<UnitPart, UnitPart> GetAllUnitPartDict(List<UnitPart> originals, List<UnitPart> targets)
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

            return
            (
                outParts.Count < Math.Max(originals.Count, targets.Count) ?
                GetUnitPartDictInCommon(originals, targets) : outParts
            );
        }

        //In some cases, the direct approach of GetAllUnitPartDict wouldn't work.
        //For example: BTU/s and Watts. Ignoring the dividable/non-dividable character isn't useful here.
        //The only solution is trying to find the common parts of both units. In this example, BTU-J and s-s.
        private static Dictionary<UnitPart, UnitPart> GetUnitPartDictInCommon(List<UnitPart> originals, List<UnitPart> targets)
        {
            return new Dictionary<UnitPart, UnitPart>();
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
}
