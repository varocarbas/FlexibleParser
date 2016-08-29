using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    public partial class UnitP
    {
        private static UnitP PerformUnitOperation(UnitP first, UnitP second, Operations operation, string operationString)
        {
            ErrorTypes errorType = GetUnitOperationError(first, second, operation);
            if (errorType != ErrorTypes.None)
            {
                return new UnitP(first, errorType);
            }
            
            UnitInfo outInfo = new UnitInfo(first);
            UnitInfo secondInfo = new UnitInfo(second);

            bool unitlessDone = false;
            if (outInfo.Unit != Units.Unitless && secondInfo.Unit != Units.Unitless)
            {
                unitlessDone = true;
                if (operation == Operations.Addition || operation == Operations.Subtraction)
                {
                    UnitInfo[] tempInfos = PerformChecksBeforeAddition(outInfo, secondInfo);

                    foreach (UnitInfo tempInfo in tempInfos)
                    {
                        if (tempInfo.Error.Type != ErrorTypes.None)
                        {
                            return new UnitP(first, tempInfo.Error.Type);
                        }
                    }
                    //Only the second operator might have been modified.
                    secondInfo = tempInfos[1];
                }
                else outInfo = ModifyUnitPartsBeforeMultiplication(first, secondInfo, operation);
            }
            else if (outInfo.Unit == Units.Unitless && secondInfo.Unit != Units.Unitless)
            {
                outInfo = new UnitInfo(secondInfo);
            }

            if (outInfo.Error.Type != ErrorTypes.None || outInfo.Unit == Units.None)
            {
                return new UnitP
                (
                    first,
                    (
                        outInfo.Error.Type != ErrorTypes.None ?
                        outInfo.Error.Type : ErrorTypes.InvalidUnit
                    )
                );
            }

            if (outInfo.Unit != Units.Unitless || !unitlessDone)
            {
                outInfo = PerformManagedOperationUnits(outInfo, secondInfo, operation);
            }

            return 
            (
                outInfo.Error.Type != ErrorTypes.None ?
                new UnitP(first, outInfo.Error.Type) :
                new UnitP(outInfo, first, operationString)
            );
        }

        private static UnitInfo[] PerformChecksBeforeAddition(UnitInfo outInfo, UnitInfo secondInfo)
        {
            UnitInfo[] outInfos = new UnitInfo[] { outInfo, secondInfo };

            if (outInfo.Type != secondInfo.Type)
            {
                outInfos[0].Error = new ErrorInfo(ErrorTypes.InvalidUnit);
            }
            else if (outInfo.Unit != secondInfo.Unit || IsUnnamedUnit(outInfo.Unit))
            {
                outInfos[1] = ConvertUnit(secondInfo, outInfo);
            }

            return outInfos;
        }

        private static UnitInfo ModifyUnitPartsBeforeMultiplication(UnitP first, UnitInfo secondInfo, Operations operation)
        {
            UnitInfo outInfo = new UnitInfo(first);
            List<UnitPart> parts2 = new List<UnitPart>();
            int sign = (operation == Operations.Multiplication ? 1 : -1);

            foreach (UnitPart part in secondInfo.Parts)
            {
                parts2.Add
                (
                    new UnitPart(part) 
                    { 
                        Exponent = part.Exponent * sign 
                    }
                );
            }

            outInfo = AddNewUnitParts(outInfo, parts2);

            return StartCompoundAnalysis
            (
                new ParseInfo(outInfo)
            )
            .UnitInfo;
        }

        private static UnitInfo ConvertUnit(UnitInfo originalInfo, UnitInfo targetInfo, bool matchTargetPrefix = true)
        {
            return PerformConversion
            (
                originalInfo, targetInfo, matchTargetPrefix
            );
        }

        private static UnitInfo PerformConversion(UnitInfo originalInfo, UnitInfo targetInfo, bool isInternal = true)
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
                    ConvertUnitValue(outInfo, targetInfo2) :
                    PerformUnitPartConversion(outInfo, targetInfo2)
                );
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

        private static UnitInfo PerformUnitPartConversion(UnitInfo convertInfo, UnitInfo target)
        {
            return ConvertAllUnitParts
            (
                convertInfo,
                GetAllUnitPartDict(convertInfo.Parts, target.Parts)
            );
        }

        private static UnitInfo ConvertAllUnitParts(UnitInfo convertInfo, Dictionary<UnitPart, UnitPart> allParts)
        {
            foreach (var item in allParts)
            {
                convertInfo = ConvertUnitPartToTarget
                (
                    convertInfo, item.Key, item.Value
                );
            }

            return convertInfo;
        }

        private static UnitInfo ConvertUnitPartToTarget(UnitInfo outInfo, UnitPart originalPart, UnitPart targetPart, bool matchTargetPrefix = true)
        {
            ErrorTypes errorType = GetUnitPartConversionError(originalPart, targetPart);
            if (errorType != ErrorTypes.None)
            {
                outInfo.Error = new ErrorInfo(errorType);
                return outInfo;
            }

            UnitPart originalPart2 = new UnitPart(originalPart);
            UnitPart targetPart2 = new UnitPart(targetPart);

            if (originalPart2.Exponent == targetPart2.Exponent)
            {
                //In this situation, exponents don't need to be considered and it is better ignoring them during
                //the conversion to avoid problems.
                //For example: the part m2 has associated a specific unit (SquareMetre), but it might be converted
                //into units which don't have one, like ft2.
                originalPart2.Exponent = 1;
                targetPart2.Exponent = 1;
            }
            //Different exponents cannot be removed. For example: conversion between litre and m3, where the exponent
            //does define the unit. 


            return outInfo * RaiseToIntegerExponent
            (
                PerformConversion
                (
                    UnitPartToUnitInfo(originalPart2, 1m),
                    UnitPartToUnitInfo(targetPart2, 1m),
                    matchTargetPrefix
                ),
                //The original part being in the denominator means that the output value should be inverted.
                //Note that this output is always expected to modify the main value (= in the numerator).
                originalPart.Exponent / Math.Abs(originalPart.Exponent)
            );
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

            return outParts;
        }

        //The prefixes of both units are managed before reaching this point.
        private static UnitInfo ConvertUnitValue(UnitInfo original, UnitInfo target)
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
                return ConvertUnitValueSpecial(original, target); 
            }

            return 
            (
                original * GetUnitConversionFactor(original.Unit) /
                GetUnitConversionFactor(target.Unit)
            );
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
    }
}