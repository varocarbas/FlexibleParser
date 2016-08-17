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

            outInfo.Parts.AddRange(parts2);

            return StartCompoundAnalysis
            (
                new ParsedUnit(outInfo)
            )
            .UnitInfo;
        }

        private static UnitInfo AnalyseNewUnitOperationPart(UnitP unitP, UnitPart part)
        {
            UnitInfo outInfo = new UnitInfo(1m, part.Unit, part.Prefix);
            if (unitP.UnitParts.Count < 1) return outInfo;

            UnitInfo newInfo = SyncUnitWithPreviousParts(unitP, outInfo.Unit);
            if (newInfo.Unit != outInfo.Unit)
            {
                newInfo = ConvertUnit(outInfo, newInfo);
                newInfo = RaiseToIntegerExponent(newInfo, part.Exponent);
                if (newInfo.Error.Type == ErrorTypes.None)
                {
                    return newInfo;
                }
            }

            return outInfo;
        }

        private static UnitInfo SyncUnitWithPreviousParts(UnitP first, Units unit)
        {
            UnitInfo outInfo = new UnitInfo(1m, unit, new Prefix(1m));
            if (first.UnitParts.FirstOrDefault(x => x.Unit == unit) != null)
            {
                return outInfo;
            }
 
            Units unit2 = GetSameTypePart(first.UnitParts.ToList(), unit);
            if (unit2 != unit) return new UnitInfo(outInfo) { Unit = unit2 };

            UnitSystems systemFirst =
            (
                first.UnitSystem == UnitSystems.None ?
                GetSystemFromUnitInfo(new UnitInfo(first)).System :
                first.UnitSystem
            );
            UnitSystems system = GetSystemFromUnit(unit);

            return new UnitInfo(outInfo)
            {
                Unit =
                (
                    AllMetricEnglish[systemFirst] != AllMetricEnglish[system] ?
                    GetDefaultEnglishMetricUnit(GetTypeFromUnit(unit), system) : unit
                )
            };
        }

        private static Units GetSameTypePart(List<UnitPart> parts, Units unit)
        {
            foreach (UnitPart part in parts)
            {
                if (GetTypeFromUnitPart(part) == GetTypeFromUnit(unit))
                {
                    return part.Unit;
                }
            }

            return unit;
        }

        private static UnitInfo ConvertUnit(UnitInfo originalInfo, UnitInfo targetInfo)
        {
            return
            (
                originalInfo.Type == targetInfo.Type ? 
                PerformConversion(originalInfo, targetInfo) :
                new UnitInfo(originalInfo) 
                { 
                    Error = new ErrorInfo(ErrorTypes.InvalidUnit) 
                }
            );
        }

        private static UnitInfo PerformConversion(UnitInfo originalInfo, UnitInfo targetInfo)
        {
            ErrorTypes errorType = GetConversionError(originalInfo.Unit, targetInfo.Unit);
            if (errorType != ErrorTypes.None)
            {
                return new UnitInfo(originalInfo) { Error = new ErrorInfo(errorType) };
            }

            UnitInfo targetInfo2 = NormaliseTargetUnit(targetInfo);
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
            }

            return outInfo;
        }

        private static ErrorTypes GetConversionError(Units originalUnit, Units targetUnit)
        {
            ErrorTypes outError = ErrorTypes.None;

            if (originalUnit == Units.None || targetUnit == Units.None)
            {
                outError = ErrorTypes.InvalidUnit;
            }
            else if (originalUnit == Units.Unitless || targetUnit == Units.Unitless)
            {
                outError = ErrorTypes.InvalidUnitConversion;
            }

            return outError;
        }

        //The only relevant part of the target unit value is the prefix.
        private static UnitInfo NormaliseTargetUnit(UnitInfo targetInfo)
        {
            UnitInfo outInfo = new UnitInfo(targetInfo)
            {
                Value = 1,
                BaseTenExponent = 0
            };

            return NormaliseUnitInfo(outInfo);
        }

        //NOTE: targetInfo2 is assumed be normalised.
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
                    Prefix = new Prefix(originalInfo.Prefix.PrefixUsage) //Already included in the newExponent.
                }
            );
        }

        private static int GetBaseTenExponentIncreasePrefixes(UnitInfo originalInfo, int targetInfo2Exp)
        {
            //targetInfo2 is already normalised by only accounting for the value information which is 
            //relevant for the conversion (i.e., the prefix).
            UnitInfo originalTemp = new UnitInfo(originalInfo) { Value = 1m, BaseTenExponent = 0 };
            originalTemp = NormaliseUnitInfo(originalTemp);

            return originalTemp.BaseTenExponent - targetInfo2Exp;
        }

        //Checks whether the conversion might be performed directly (i.e., without parts analysis).
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

        private static UnitInfo ConvertUnitPartToTarget(UnitInfo outInfo, UnitPart originalPart, UnitPart targetPart)
        {
            ErrorTypes errorType = GetUnitPartConversionError(originalPart, targetPart);
            if (errorType != ErrorTypes.None)
            {
                outInfo.Error = new ErrorInfo(errorType);
                return outInfo;
            }

            //Both parts have the same type (= exponent) that's why better ignoring this issue during
            //the conversion to avoid problems.
            //For example: the part m^2 has associated a specific unit, but it might be converted
            //to parts which don't have one.
            UnitPart originalPart2 = new UnitPart(originalPart) { Exponent = 1 };
            UnitPart targetPart2 = new UnitPart(targetPart) { Exponent = 1 };

            return outInfo * RaiseToIntegerExponent
            (
                PerformConversion
                (
                    UnitPartToUnitInfo(originalPart2, 1m),
                    UnitPartToUnitInfo(targetPart2, 1m)
                ),
                originalPart.Exponent
            );
        }

        private static ErrorTypes GetUnitPartConversionError(UnitPart originalPart, UnitPart targetPart)
        {
            ErrorTypes outError = ErrorTypes.None;
            
            if (GetTypeFromUnitPart(originalPart) != GetTypeFromUnitPart(targetPart))
            {
                outError = ErrorTypes.InvalidUnitConversion;
            }
            else if (IsUnnamedUnit(originalPart.Unit) || IsUnnamedUnit(targetPart.Unit))
            {
                //Finding a compound here would be certainly an error.
                outError = ErrorTypes.InvalidUnitConversion;
            }

            return outError;
        }

        //Relates all the original/target unit parts between each other in order to facilitate the subsequent unit conversion.
        private static Dictionary<UnitPart, UnitPart> GetAllUnitPartDict(List<UnitPart> originals, List<UnitPart> targets)
        {
            Dictionary<UnitPart, UnitPart> outParts = new Dictionary<UnitPart, UnitPart>();

            foreach (UnitPart original in originals)
            {
                //GetTypeFromUnit always works with UnitPart units (never unnamed).
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

            return original * (GetUnitConversionFactor(original.Unit) / GetUnitConversionFactor(target.Unit));
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

        //Accounts for situations where a specific conversion (i.e., not just conversion factors) is required.
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
