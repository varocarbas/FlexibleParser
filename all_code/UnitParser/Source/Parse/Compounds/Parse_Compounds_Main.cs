using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlexibleParser
{
    public partial class UnitP
    {
        private static ParseInfo StartCompoundParse(ParseInfo parseInfo)
        {
            //ValidCompound isn't used when parsing individual units.
            parseInfo.ValidCompound = new StringBuilder();

            return StartCompoundAnalysis
            (
                ExtractUnitParts(parseInfo)
            );
        }

        private static ParseInfo ExtractUnitParts(ParseInfo parseInfo)
        {
            StringBuilder previous = new StringBuilder();
            string origInput = parseInfo.InputToParse;
            
            parseInfo = UnitInDenominator(parseInfo);
            
            //Both strings being different would mean that a number-only numerator was removed.
            //For example: the input string 1/m is converted into m, but UpdateUnitParts treats
            //it as m-1 because of isNumerator being false.
            bool isNumerator = (origInput == parseInfo.InputToParse);
            
            char symbol = ' ';
            char[] inputArray = parseInfo.InputToParse.ToArray();

            for (int i = 0; i < inputArray.Length; i++)
            {
                char bit = inputArray[i];

                if (IsCompoundDescriptive(bit, true) && !SkipCompoundDescriptive(i, inputArray, previous.ToString()))
                {
                    if (bit == '-')
                    {
                        if (MinusIsOK(inputArray, i))
                        {
                            previous.Append(bit);
                        }
                        continue;
                    }

                    parseInfo = UpdateUnitParts
                    (
                        parseInfo, previous, isNumerator, symbol
                    );

                    if (parseInfo.UnitInfo.Error.Type != ErrorTypes.None)
                    {
                        return parseInfo;
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
                previous.Length == 0 ? parseInfo :
                UpdateUnitParts(parseInfo, previous, isNumerator, symbol)
            );
        }

        private static bool SkipCompoundDescriptive(int i, char[] inputArray, string sofar)
        {
            if (inputArray[i].ToString().ToLower() == "x")
            {
                //By default, x is assumed to be a multiplication sign.
                sofar = sofar.ToLower();

                if (sofar.Length >= 2)
                {
                    string sofar2 = sofar.Substring(sofar.Length - 2, 2);
                    if (sofar2 == "ma" || sofar2 == "lu")
                    {
                        //It isn't a multiplication sign, but part of the string representations
                        //of maxwell or lux.
                        return true;
                    }
                }

                if (sofar.Length >= 1)
                {
                    string sofar2 = sofar.Substring(sofar.Length - 1, 1);
                    if (sofar2 == "m" || sofar2 == "l")
                    {
                        if (i == inputArray.Length - 1 || IsCompoundDescriptive(inputArray[i + 1]))
                        {
                            //It isn't a multiplication sign, but part of the symbols Mx or lx.
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        //When parsing a unit, only integer exponents are considered valid numbers (e.g., m2). Inverse units 
        //(e.g., 1/m) are the exception to this rule. This method corrects the input string to avoid ExtractUnitParts
        //to trigger an error. For example: 1/m*s being converted into m-1*s-1.
        private static ParseInfo UnitInDenominator(ParseInfo parseInfo)
        {
            foreach (var symbol in OperationSymbols[Operations.Division])
            {
                string[] tempArray = parseInfo.InputToParse.Split(symbol);
                if (tempArray.Length >= 2)
                {
                    UnitInfo tempInfo = ParseDouble(tempArray[0]);
                    if (tempInfo.Value != 0m && tempInfo.Error.Type == ErrorTypes.None)
                    {
                        parseInfo.UnitInfo = parseInfo.UnitInfo * tempInfo;
                        parseInfo.InputToParse = parseInfo.InputToParse.Substring
                        (
                            tempArray[0].Length + symbol.ToString().Length
                        );
                        return parseInfo;
                    }
                }
            }

            return parseInfo;
        }

        private static ParseInfo StartCompoundAnalysis(ParseInfo parseInfo)
        {
            if (parseInfo.UnitInfo.Error.Type != ErrorTypes.None)
            {
                return parseInfo;
            }
            if (parseInfo.ValidCompound == null)
            {
                parseInfo.ValidCompound = new StringBuilder();
            }

            parseInfo.UnitInfo = RemoveAllUnitInformation(parseInfo.UnitInfo);

            //Knowing the initial positions of all the unit parts is important because of the defining
            //"first element rules" idea which underlies this whole approach. Such a determination isn't
            //always straightforward due to the numerous unit part modifications.
            parseInfo.UnitInfo = UpdateInitialPositions(parseInfo.UnitInfo);

            //This is the best place to determine the system before finding the unit, because the subsequent
            //unit part corrections might provoke some misunderstandings on this front (e.g., CGS named 
            //compound divided into SI basic units).
            parseInfo.UnitInfo.System = GetSystemFromUnitInfo(parseInfo.UnitInfo);

            //This is also an excellent place to correct eventual system mismatches. For example: N/pint 
            //where pint has to be converted into m3, the SI (first operand system) basic unit for volume.
            parseInfo.UnitInfo = CorrectDifferentSystemIssues(parseInfo.UnitInfo);

            parseInfo.UnitInfo = ImproveUnitParts(parseInfo.UnitInfo);

            if (parseInfo.UnitInfo.Type == UnitTypes.None)
            {
                parseInfo.UnitInfo = GetUnitFromParts(parseInfo.UnitInfo);
            }
            
            parseInfo.UnitInfo = UpdateMainUnitVariables(parseInfo.UnitInfo);
            if (parseInfo.UnitInfo.Unit == Units.None)
            {
                parseInfo.UnitInfo.Error = new ErrorInfo(ErrorTypes.InvalidUnit);
            }
            else parseInfo = AnalyseValidCompoundInfo(parseInfo);

            return parseInfo;
        }

        private static UnitInfo CorrectDifferentSystemIssues(UnitInfo unitInfo)
        {
            UnitSystems basicSystem = AllMetricEnglish[unitInfo.System];
            UnitInfo convertInfo = new UnitInfo(1m);

            for (int i = 1; i < unitInfo.Parts.Count; i++)
            {
                UnitPart part = unitInfo.Parts[i];
                UnitTypes type = GetTypeFromUnitPart(part);
                UnitSystems system = GetSystemFromUnit(part.Unit, true);
                //There are two different scenarios where a conversion might occur: metric vs. English or Imperial vs. USCS.
                bool convertEnglish = false;

                if (PartNeedsConversion(system, basicSystem, type) || (convertEnglish = PartNeedsConversionEnglish(unitInfo.System, GetSystemFromUnit(part.Unit))))
                {
                    UnitPart targetPart = GetTargetUnitPart
                    (
                        unitInfo, part, type, 
                        (
                            convertEnglish ? unitInfo.System : basicSystem
                        )
                    );

                    if (targetPart == null || targetPart.Unit == part.Unit || GetTypeFromUnitPart(targetPart) != type)
                    {
                        continue;
                    }

                    UnitInfo tempInfo = AdaptUnitParts
                    (
                        new UnitInfo(1m) 
                        { 
                            Parts = new List<UnitPart>() { part, targetPart } 
                        }, 
                        0, 1
                    );

                    if (tempInfo == null) continue;
                    //AdaptUnitParts doesn't perform an actual conversion, just an adaptation to the target format.
                    //That's why it doesn't account for the target prefix, what explains the modification below.
                    tempInfo.Parts[0].Prefix = new Prefix(targetPart.Prefix);

                    unitInfo = UpdateNewUnitPart
                    (
                        unitInfo, unitInfo.Parts[i], 
                        tempInfo.Parts[0]
                    );
                    convertInfo = convertInfo * tempInfo;
                }
            }

            return unitInfo * convertInfo;
        }

        private static UnitPart GetTargetUnitPart(UnitInfo unitInfo, UnitPart part, UnitTypes partType, UnitSystems targetSystem)
        {
            foreach (UnitPart part2 in unitInfo.Parts)
            {
                if (part2.Unit == part.Unit) continue;

                if (GetSystemFromUnit(part2.Unit) == targetSystem && GetTypeFromUnitPart(part2, true) == partType)
                {
                    //Different unit part with the same type and the target system is good enough.
                    //For example: in kg/m*ft, m is a good target for ft.
                    return new UnitPart(part2) 
                    { 
                        //The exponent is removed because there was a non-exponent match.
                        //Cases like m3-litre never reach this point and are analysed below.
                        Exponent = 1
                    };
                }
            }

            return GetBasicUnitPartForTypeSystem(partType, targetSystem);
        }

        private static bool PartNeedsConversionEnglish(UnitSystems system1, UnitSystems system2)
        {
            return 
            (
                (system1 == UnitSystems.USCS || system2 == UnitSystems.USCS) && system1 != system2
            );
        }

        private static bool PartNeedsConversion(UnitSystems partSystem, UnitSystems basicSystem, UnitTypes partType)
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

        private static UnitPart GetBasicUnitPartForTypeSystem(UnitTypes type, UnitSystems system)
        {
            if (AllBasicUnits.ContainsKey(type) && AllBasicUnits[type].ContainsKey(system))
            {
                //The given type/system matches a basic unit. For example: length and SI matching metre.
                Units unit2 = AllBasicUnits[type][system].Unit;
                if (UnitIsNamedCompound(unit2))
                {
                    //Note that BasicUnit doesn't fully agree with the "basic unit" concept and that's why it might
                    //be a compound. 
                    IEnumerable<UnitPart> compoundUnitParts = GetBasicCompoundUnitParts(unit2, true);
                    if (compoundUnitParts.Count() == 1)
                    {
                        //Only 1-unit-part versions are relevant. 
                        //This condition should always be met as far as all the compound basic units are expected to
                        //have a 1-unit-part version. For example: energy defined as an energy unit part. 
                        //This theoretically-never-met condition accounts for eventual hardcoding misconducts, like 
                        //faulty population of AllCompounds.
                        return new UnitPart(compoundUnitParts.First());
                    }
                }
                else
                {
                    return new UnitPart
                    (
                        AllBasicUnits[type][system].Unit,
                        AllBasicUnits[type][system].PrefixFactor,
                        //Note that exponents aren't relevant to define basic units (= always 1).
                        //On the other hand, they might be formed by parts where the exponent is
                        //relevant; for example: Units.CubicMetre (1 part with exponent 3).
                        1 
                    );
                }
            }

            IEnumerable<UnitPart> unitParts = GetBasicCompoundUnitParts(type, system, true);
            return
            (
                unitParts.Count() != 1 ? null :
                //Type and system match a basic compound consisting in just one part (e.g., m2).
                new UnitPart(unitParts.First()) 
            );
        }

        //Making sure that the assumed-valid compound doesn't really hide invalid information.
        private static ParseInfo AnalyseValidCompoundInfo(ParseInfo parseInfo)
        {
            if (parseInfo.InputToParse == null || parseInfo.ValidCompound.Length < 1)
            {
                //This part might be reached when performing different actions than parsing; that's why
                //InputToParse or ValidCompound might have not be populated.
                return parseInfo;
            }

            char[] valid = new char[parseInfo.ValidCompound.Length];
            parseInfo.ValidCompound.CopyTo(0, valid, 0, valid.Length);
            char[] toCheck = parseInfo.InputToParse.Trim().Where(x => x != ' ').ToArray();

            if (valid.Length == toCheck.Length) return parseInfo;

            decimal invalidCount = 0m;
            int i2 = -1;

            for (int i = 0; i < toCheck.Length; i++)
            {
                i2 = i2 + 1;
                if (i2 > valid.Length - 1) break;

                if (!CharAreEquivalent(valid[i2], toCheck[i]))
                {
                    if (!wrongCharIsOK(toCheck[i]))
                    {
                        invalidCount = invalidCount + 1m;
                    }
                    i = i + 1;
                }
            }

            if (invalidCount >= 3m || invalidCount / valid.Length >= 0.25m)
            {
                parseInfo.UnitInfo.Type = UnitTypes.None;
                parseInfo.UnitInfo.Unit = Units.None;
            }

            return parseInfo;
        }
        
        private static bool CharAreEquivalent(char char1, char char2)
        {
            if (char1 == char2) return true;

            return (char1.ToString().ToLower() == char2.ToString().ToLower());
        }

        //Certain symbols aren't considered when post-analysing the validity of the input string.
        //For example: m//////////////s is considered fine.
        private static bool wrongCharIsOK(char charToCheck)
        {
            if (UnitParseIgnored.Contains(charToCheck.ToString()))
            {
                return true;
            }

            foreach (var item in OperationSymbols)
            {
                if (item.Value.Contains(charToCheck))
                {
                    return true;
                }
            }

            return false;
        }

        //Instrumental class whose sole purpose is easing the exponent parsing process.
        private class ParseExponent
        {
            public string AfterString { get; set; }
            public int Exponent { get; set; }

            public ParseExponent(string afterString, int exponent = 1)
            {
                AfterString = afterString;
                Exponent = exponent;
            }
        }
    }
}
