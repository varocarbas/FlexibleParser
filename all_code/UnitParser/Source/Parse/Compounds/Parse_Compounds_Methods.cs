using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlexibleParser
{
    public partial class UnitP
    {
        //Returns the string representation of the unit associated with the given compound.
        //NOTE: this function assumes that a valid compound is already in place.
        private static string GetUnitStringCompound(UnitInfo unitInfo)
        {
            if (unitInfo.Unit == Units.None) return "None";
            else if (unitInfo.Unit == Units.Unitless) return "Unitless";

            string outUnitString = "";
            bool isNumerator = true;

            foreach (UnitPart unitPart in unitInfo.Parts)
            {
                if (isNumerator && unitPart.Exponent < 0)
                {
                    isNumerator = false;
                    if (outUnitString == "") outUnitString = "1";
                    outUnitString = outUnitString + "/";
                }
                else if (outUnitString != "") outUnitString = outUnitString + "*";

                string unitString = "";
                if (unitPart.Prefix.Symbol != "" && unitPart.Unit.ToString().ToLower().StartsWith(unitPart.Prefix.Name.ToLower()))
                {
                    unitString = unitPart.Prefix.Symbol;
                }
                unitString = AllUnitSymbols.First(x => x.Value == unitPart.Unit).Key;
                
                int exponent = Math.Abs(unitPart.Exponent);
                if (exponent != 1) unitString = unitString + exponent.ToString();

                outUnitString = outUnitString + unitString;
            }

            return outUnitString;
        }

        private static UnitInfo GetBasicCompoundType(UnitInfo unitInfo)
        {
            foreach (var allCompound in AllCompounds)
            {
                foreach (var compound in allCompound.Value)
                {
                    unitInfo = UnitPartsMatchCompound
                    (
                        unitInfo, compound.Parts, allCompound.Key
                    );

                    if (unitInfo.Type != UnitTypes.None)
                    {
                        return unitInfo;
                    }
                }
            }

            return unitInfo;
        }

        private static UnitInfo UnitPartsMatchCompound(UnitInfo unitInfo, List<CompoundPart> compoundParts, UnitTypes compoundType)
        {
            int count = 0;
            while (count < 2)
            {
                count = count + 1;
                List<UnitPart> unitParts = GetCompoundComparisonUnitParts(unitInfo, count);
                if (UnitPartsMatchCompoundParts(unitParts, compoundParts))
                {
                    unitInfo.Type = compoundType;
                    return unitInfo;
                }
                else if (count == 2 && unitParts.Count == 1 && unitParts[0].Exponent == 1)
                {
                    UnitTypes type = GetTypeFromUnit(unitParts[0].Unit);
                    if (type != UnitTypes.None)
                    {
                        //The modifications in GetCompoundComparisonUnitParts generated an individual unit.
                        //It might not be recognised anywhere else, so better taken care of it here.
                        unitInfo.Unit = DefaultUnnamedUnits[unitInfo.System];
                        unitInfo.Type = type;

                        return unitInfo;
                    }
                }
            }

            return unitInfo;
        }

        private static List<UnitPart> GetCompoundComparisonUnitParts(UnitInfo unitInfo, int type)
        {
            return 
            (
                type == 1 ?
                new List<UnitPart>(unitInfo.Parts) :
                GetUnitPartsForAnyUnit(unitInfo)
            );
        }

        private static List<UnitPart> GetUnitPartsForAnyUnit(UnitInfo unitInfo)
        {
            List<UnitPart> outParts = new List<UnitPart>();

            foreach (UnitPart part in unitInfo.Parts)
            {
                //Under these specific conditions, GetTypeFromUnit is good enough on account of the fact that the
                //exponent is irrelevant. 
                //For example: m3 wouldn't go through this part (type 1 match) and the exponent doesn't define litre.
                //Note that these parts aren't actually correct in many cases, just compatible with the AllCompounds format.
                UnitTypes type2 = GetTypeFromUnitPart(part, false, true);
                if (AllCompounds.ContainsKey(type2))
                {
                    outParts.AddRange
                    (
                        GetUnitPartsFromBasicCompound
                        (
                            AllCompounds[type2][0],
                            unitInfo.System, part.Exponent
                        )
                    );
                }
                else outParts.Add(new UnitPart(part));
                outParts = SimplifyCompoundComparisonUnitParts(outParts).Parts;
            }

            return outParts;
        }

        private static UnitInfo SimplifyCompoundComparisonUnitParts(List<UnitPart> unitParts, bool checkPrefixes = false)
        {
            UnitInfo outInfo = new UnitInfo(1m) 
            { 
                Parts = new List<UnitPart>(unitParts) 
            };

            for (int i = outInfo.Parts.Count - 1; i >= 0; i--)
            {
                for (int i2 = i - 1; i2 >= 0; i2--)
                {
                    UnitTypes type1 = GetTypeFromUnit(outInfo.Parts[i].Unit);
                    UnitTypes type2 = GetTypeFromUnit(outInfo.Parts[i2].Unit);
                    if (type1 == type2)
                    {
                        if (outInfo.Parts[i].Unit != outInfo.Parts[i2].Unit)
                        {
                            //This method is only called to perform basic unit matching; more specifically, finding
                            //the (dividable) compounds best matching the non-dividable ones. No direct conversions
                            //will be performed among the outputs of this function, that's why the exact units aren't
                            //that important. For example: when dealing with rood/rod, the only output which matters
                            //is the resulting type (i.e., length). It doesn't matter if it is rod or ft or other unit.
                            outInfo.Parts[i].Unit = outInfo.Parts[i2].Unit;
                        }
                        else if (checkPrefixes && (outInfo.Parts[i].Prefix.Factor != 1m || outInfo.Parts[i2].Prefix.Factor != 1m))
                        {
                            //Reaching here means that the returned information will be used in an intermediate conversion.
                            //In such a scenario, unit part prefix might become error sources.
                            outInfo *= RaiseToIntegerExponent
                            (
                                outInfo.Parts[i].Prefix.Factor,
                                outInfo.Parts[i].Exponent
                            );
                            outInfo.Parts[i].Prefix = new Prefix();

                            outInfo *= RaiseToIntegerExponent
                            (
                                outInfo.Parts[i2].Prefix.Factor,
                                outInfo.Parts[i2].Exponent
                            );
                            outInfo.Parts[i2].Prefix = new Prefix();
                        }

                        outInfo.Parts[i].Exponent += outInfo.Parts[i2].Exponent;

                        if (outInfo.Parts[i].Exponent == 0)
                        {
                            outInfo.Parts.RemoveAt(i);
                        }
                        outInfo.Parts.RemoveAt(i2);

                        if (outInfo.Parts.Count == 0 || i > outInfo.Parts.Count - 1)
                        {
                            i = outInfo.Parts.Count;
                            break;
                        }
                    }
                }
            }

            return outInfo;
        }

        private static bool UnitPartsMatchCompoundParts(List<UnitPart> unitParts, List<CompoundPart> compoundParts)
        {
            if (unitParts.Count != compoundParts.Count) return false;

            foreach (UnitPart part in unitParts)
            {
                UnitTypes type = GetTypeFromUnit(part.Unit);
                int exponent = part.Exponent;
                if (compoundParts.FirstOrDefault(x => x.Type == type && x.Exponent == exponent) == null)
                {
                    return false;
                }
            }
            return true;
        }

        private static ParseExponent AnalysePartExponent(string input)
        {
            input = input.Trim();
            char[] inputArray = input.ToArray();
            int i2 = 0;
            
            for (int i = input.Length - 1; i >= 0; i--)
            {
                if (!char.IsNumber(inputArray[i]) && inputArray[i] != '-')
                {
                    i2 = i;
                    break;
                }
            }

            ParseExponent outVar = new ParseExponent(input);
            int exponent = 1;
            if (int.TryParse(input.Substring(i2 + 1), out exponent))
            {
                //Only integer exponents are supported.
                outVar = new ParseExponent
                (
                    input.Substring(0, i2 + 1), exponent
                );
            }

            return outVar;
        }

        private static ParseInfo UpdateUnitParts(ParseInfo parseInfo, StringBuilder inputSB, bool isNumerator, char symbol)
        {
            if (inputSB.Length == 0) return parseInfo;
            string input = inputSB.ToString();

            ParseExponent exponent = AnalysePartExponent(input);
            if (!isNumerator) exponent.Exponent = -1 * exponent.Exponent;

            ParseInfo parseInfo2 = StartIndividualUnitParse
            (
                new ParseInfo
                (
                    0m, exponent.AfterString,
                    parseInfo.UnitInfo.Prefix.PrefixUsage
                )
            );

            return
            (
                parseInfo2.UnitInfo.Unit == Units.None ?
                new ParseInfo(parseInfo)
                { 
                    UnitInfo = new UnitInfo(parseInfo.UnitInfo, ErrorTypes.InvalidUnit)
                } :
                AddValidUnitPartInformation
                (
                    parseInfo, parseInfo2, symbol, exponent, input
                )
            );
        }

        private static ParseInfo AddValidUnitPartInformation(ParseInfo parseInfo, ParseInfo parseInfo2, char symbol, ParseExponent exponent, string input)
        {
            parseInfo.ValidCompound = AddInformationToValidCompound
            (
                parseInfo.ValidCompound, symbol, exponent, input
            );

            parseInfo.UnitInfo = AddNewUnitParts
            (
                parseInfo.UnitInfo, new List<UnitPart>() 
                {
                    new UnitPart
                    (
                        parseInfo2.UnitInfo.Unit, parseInfo2.UnitInfo.Prefix.Factor,
                        exponent.Exponent
                    )
                }
            );

            return parseInfo;
        }

        private static StringBuilder AddInformationToValidCompound(StringBuilder validCompound, char symbol, ParseExponent exponent, string input)
        {
            if (symbol != ' ') validCompound.Append(symbol);

            if (exponent.AfterString.Trim().Length > 0)
            {
                validCompound.Append(exponent.AfterString);
            }

            string exponent2 = input.Replace(exponent.AfterString, "").Trim();
            if (exponent2.Length > 0)
            {
                validCompound.Append(exponent2);
            }

            return validCompound;
        }

        private static bool StringCanBeCompound(string inputToParse)
        {
            return
            (
                inputToParse.Length >= 2 &&
                inputToParse.FirstOrDefault(x => IsCompoundDescriptive(x)) != '\0'
            );
        }

        private static bool IsCompoundDescriptive(char bit, bool ignoreNumbers = false)
        {
            return
            (
                (!ignoreNumbers && char.IsNumber(bit)) || bit == '-' ||
                OperationSymbols[Operations.Multiplication].Contains(bit) ||
                OperationSymbols[Operations.Division].Contains(bit)
            );
        }
    }
}
