﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlexibleParser
{
    public partial class UnitP
    {
        private static ParsedUnit StartCompoundParse(ParsedUnit parsedUnit)
        {
            parsedUnit.ValidCompound = new StringBuilder();

            return StartCompoundAnalysis
            (
                ExtractUnitParts(parsedUnit)
            );
        }

        private static ParsedUnit ExtractUnitParts(ParsedUnit parsedUnit)
        {
            StringBuilder previous = new StringBuilder();
            string origInput = parsedUnit.InputToParse;
            parsedUnit = UnitInDenominator(parsedUnit);
            bool isNumerator = (origInput == parsedUnit.InputToParse);
            char symbol = ' ';
            char[] inputArray = parsedUnit.InputToParse.ToArray();

            for (int i = 0; i < inputArray.Length; i++)
            {
                char bit = inputArray[i];

                if (IsCompoundDescriptive(bit, true))
                {
                    if (bit == '-')
                    {
                        if (MinusIsOK(inputArray, i))
                        {
                            previous.Append(bit);
                            continue;
                        }
                        else
                        {
                            parsedUnit.UnitInfo.Parts = new List<UnitPart>();
                            return parsedUnit;
                        }
                    }

                    parsedUnit = UpdateUnitParts
                    (
                        parsedUnit, previous, isNumerator, symbol
                    );

                    if (parsedUnit.UnitInfo.Error.Type != ErrorTypes.None)
                    {
                        return parsedUnit;
                    }

                    if (bit != '-')
                    {
                        symbol = bit;
                        if (OperationSymbols[Operations.Division].Contains(bit))
                        {
                            isNumerator = false;
                        }
                    }

                    previous = new StringBuilder();
                }
                else previous.Append(bit);
            }

            return
            (
                previous.Length == 0 ? parsedUnit :
                UpdateUnitParts(parsedUnit, previous, isNumerator, symbol)
            );
        }

        //When parsing a unit, only exponents are considered valid numbers (e.g., unit2).
        //Inverse units (e.g., 1/m) are the exception to this rule. This method corrects the input
        //string to allow ExtractUnitParts to account for such an eventuality.
        //For example: 1/m*s being parsed as m-1*s-1.
        private static ParsedUnit UnitInDenominator(ParsedUnit parsedUnit)
        {
            foreach (var symbol in OperationSymbols[Operations.Division])
            {
                string[] tempArray = parsedUnit.InputToParse.Split(symbol);
                if (tempArray.Length >= 2)
                {
                    UnitInfo tempInfo = ParseDouble(tempArray[0]);
                    if (tempInfo.Value != 0m && tempInfo.Error.Type == ErrorTypes.None)
                    {
                        parsedUnit.UnitInfo = parsedUnit.UnitInfo * tempInfo;
                        parsedUnit.InputToParse = parsedUnit.InputToParse.Substring
                        (
                            tempArray[0].Length + symbol.ToString().Length
                        );
                        return parsedUnit;
                    }
                }
            }

            return parsedUnit;
        }

        private static ParsedUnit StartCompoundAnalysis(ParsedUnit parsedUnit)
        {
            if (parsedUnit.UnitInfo.Error.Type != ErrorTypes.None)
            {
                return parsedUnit;
            }

            parsedUnit.UnitInfo = RemoveAllUnitInformation(parsedUnit.UnitInfo);

            parsedUnit.UnitInfo = UpdateInitialPositions(parsedUnit.UnitInfo);

            //This is the best place to determine the system before finding the unit, because
            //the subsequent unit part corrections might provoke some misunderstandings on this
            //front (e.g., CGS named compound divided into SI basic units).
            parsedUnit.UnitInfo.System = GetSystemFromUnitInfo(parsedUnit.UnitInfo).System;

            //This is also an excellent place to correct eventual system mismatches.
            //For example: N/pint (pint has to be converted to m3, the basic unit of the first
            //operand system, SI).
            parsedUnit.UnitInfo = CorrectDifferentSystemIssues(parsedUnit.UnitInfo);

            parsedUnit.UnitInfo = GetCompoundUnitFromParts
            (
                ImproveUnitParts(parsedUnit.UnitInfo)
            );
            
            parsedUnit.UnitInfo = UpdateMainUnitVariables(parsedUnit.UnitInfo);

            if (parsedUnit.UnitInfo.Unit == Units.None)
            {
                parsedUnit.UnitInfo.Error = new ErrorInfo(ErrorTypes.InvalidUnit);
            }
            else parsedUnit = AnalyseValidCompoundInfo(parsedUnit);

            return parsedUnit;
        }

        private static UnitInfo CorrectDifferentSystemIssues(UnitInfo unitInfo)
        {
            UnitSystems basicSystem = AllMetricEnglish[unitInfo.System];
            UnitInfo convertInfo = new UnitInfo(1m);
            unitInfo.Parts = unitInfo.Parts.OrderBy(x => unitInfo.InitialPositions[x]).ToList();

            for (int i = 1; i < unitInfo.Parts.Count; i++)
            {
                UnitPart part = unitInfo.Parts[i];
                UnitTypes type = GetTypeFromUnitPart(part);
                UnitSystems system = GetSystemFromUnit(part.Unit, true);
                bool englishConversion = false;

                if (ConvertPart(system, basicSystem, type) || (englishConversion = ConvertPartEnglish(unitInfo.System, GetSystemFromUnit(part.Unit))))
                {
                    UnitPart targetPart = GetTargetUnitPart
                    (
                        unitInfo, i, type, (englishConversion ? unitInfo.System : basicSystem)
                    );

                    if (targetPart == null || targetPart.Unit == part.Unit || GetTypeFromUnitPart(targetPart) != type)
                    {
                        continue;
                    }

                    unitInfo = UpdateNewUnitPart(unitInfo, part, targetPart);
                    i = i - 1;

                    convertInfo = ConvertUnitPartToTarget
                    (
                        convertInfo, part, targetPart
                    );

                    if (convertInfo.Error.Type != ErrorTypes.None)
                    {
                        unitInfo.Error = new ErrorInfo(convertInfo.Error.Type);
                        return unitInfo;
                    }
                }
            }

            return
            (
                convertInfo.Value == 1m ? unitInfo : 
                unitInfo * convertInfo
            );
        }

        private static UnitPart GetTargetUnitPart(UnitInfo unitInfo, int i, UnitTypes type, UnitSystems system)
        {
            for (int i2 = 0; i2 < i; i2++)
            {
                if (GetTypeFromUnitPart(unitInfo.Parts[i2], true) == type)
                {
                    //This part certainly belongs to the target system.
                    return new UnitPart(unitInfo.Parts[i2])
                    { 
                        Exponent = unitInfo.Parts[i].Exponent 
                    };
                }
            }

            return GetBasicUnitPartForTypeSystem
            (
                type, system, unitInfo.Parts[i].Exponent
            );
        }

        private static bool ConvertPartEnglish(UnitSystems system1, UnitSystems system2)
        {
            return 
            (
                (system1 == UnitSystems.USCS || system2 == UnitSystems.USCS) && system1 != system2
            );
        }

        private static bool ConvertPart(UnitSystems partSystem, UnitSystems basicSystem, UnitTypes partType)
        {
            if (NeutralTypes.Contains(partType)) return false;

            return
            (
                partSystem == UnitSystems.None || basicSystem == UnitSystems.None ?
                true : AllMetricEnglish[partSystem] != basicSystem
            );
        }

        private static UnitInfo UpdateNewUnitPart(UnitInfo unitInfo, UnitPart oldUnitPart, UnitPart newUnitPart)
        {
            int newPos = 0;
            var oldPos = unitInfo.InitialPositions.FirstOrDefault(x => x.Key == oldUnitPart);
            if (oldPos.Key != null)
            {
                newPos = oldPos.Value;
                Dictionary<UnitPart, int> allOccurrences = unitInfo.InitialPositions
                .Where(x => x.Value == oldPos.Value).ToDictionary(x => x.Key, x => x.Value);

                foreach (var item in allOccurrences)
                {
                    unitInfo.InitialPositions.Remove(item.Key);
                }
                
            }
            else if (unitInfo.InitialPositions.Count > 0)
            {
                newPos = unitInfo.InitialPositions.Max(x => x.Value);
            }

            if (!unitInfo.InitialPositions.ContainsKey(newUnitPart))
            {
                unitInfo.InitialPositions.Add(newUnitPart, newPos);
            }

            unitInfo.Parts.Remove(oldUnitPart);
            unitInfo.Parts.Add(new UnitPart(newUnitPart));

            return unitInfo;
        }

        private static UnitPart GetBasicUnitPartForTypeSystem(UnitTypes type, UnitSystems system, int exponent)
        {
            if (AllBasicUnits.ContainsKey(type) && AllBasicUnits[type].ContainsKey(system))
            {
                Units unit2 = AllBasicUnits[type][system].Unit;
                if (UnitIsCompound(unit2))
                {
                    //Remember that BasicUnit doesn't fully agree with the "basic unit" concept.
                    List<UnitPart> compoundParts = GetCompoundUnitParts(unit2, true);
                    if (compoundParts.Count == 1)
                    {
                        int sign = exponent / Math.Abs(exponent); //This is always required.

                        if (Math.Abs(exponent) > Math.Abs(compoundParts[0].Exponent))
                        {
                            //Condition to compensate situations like litre being a volume unit.
                            compoundParts[0].Exponent = exponent;
                        }
                        compoundParts[0].Exponent *= sign;

                        return compoundParts[0];
                    }
                }
                else
                {
                    return new UnitPart
                    (
                        AllBasicUnits[type][system].Unit,
                        AllBasicUnits[type][system].PrefixFactor,
                        exponent
                    );
                }
            }

            List<UnitPart> unitParts = GetBasicCompoundUnitParts(type, system);
            if (unitParts.Count == 1)
            {
                unitParts[0].Exponent = exponent;
                return unitParts[0];
            }

            return null;
        }

        //Making sure that the assumed-valid compound doesn't really hide invalid information.
        private static ParsedUnit AnalyseValidCompoundInfo(ParsedUnit parsedUnit)
        {
            if (parsedUnit.InputToParse == null || parsedUnit.ValidCompound.Length < 1)
            {
                //This part might not only be reached while parsing.
                return parsedUnit;
            }

            char[] valid = new char[parsedUnit.ValidCompound.Length];
            parsedUnit.ValidCompound.CopyTo(0, valid, 0, valid.Length);
            char[] toCheck = parsedUnit.InputToParse.Trim().Where(x => x != ' ').ToArray();

            if (valid.Length == toCheck.Length) return parsedUnit;

            decimal invalidCount = 0m;
            int i2 = -1;

            for (int i = 0; i < toCheck.Length; i++)
            {
                i2 = i2 + 1;
                if (i2 > valid.Length - 1) break;

                if (!CharAreEquivalent(valid[i2], toCheck[i]))
                {
                    if (!IgnoredUnitSymbols.Contains(toCheck[i].ToString()))
                    {
                        invalidCount = invalidCount + 1m;
                    }
                    i = i + 1;
                }
            }

            if (invalidCount >= 3m || invalidCount / valid.Length >= 0.25m)
            {
                parsedUnit.UnitInfo.Type = UnitTypes.None;
                parsedUnit.UnitInfo.Unit = Units.None;
            }

            return parsedUnit;
        }

        //Instrumental class whose sole purpose is easing the exponent parsing process.
        private class ParsedExponent
        {
            public string AfterString { get; set; }
            public int Exponent { get; set; }

            public ParsedExponent(string afterString, int exponent = 1)
            {
                AfterString = afterString;
                Exponent = exponent;
            }
        }
    }
}