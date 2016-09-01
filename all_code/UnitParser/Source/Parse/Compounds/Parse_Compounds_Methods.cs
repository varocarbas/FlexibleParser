﻿using System;
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
                    if (UnitPartsMatchCompound(unitInfo, compound.Parts))
                    {
                        unitInfo.Type = allCompound.Key;
                        return unitInfo;
                    }
                }
            }

            return unitInfo;
        }

        private static bool UnitPartsMatchCompound(UnitInfo unitInfo, List<CompoundPart> compoundParts)
        {
            int count = 0;
            while (count < 2)
            {
                count = count + 1;
                if (UnitPartsMatchCompoundParts(GetCompoundComparisonUnitParts(unitInfo, count), compoundParts))
                {
                    return true;
                }
            }

            return false;
        }

        private static List<UnitPart> GetCompoundComparisonUnitParts(UnitInfo unitInfo, int type)
        {
            List<UnitPart> outParts = new List<UnitPart>();

            if (type == 1)
            {
                return unitInfo.Parts;
            }
            else
            {
                foreach (UnitPart part in unitInfo.Parts)
                {
                    //Under these specific conditions, GetTypeFromUnit is good enough on account of the fact that the
                    //exponent is irrelevant. 
                    //For example: m3 wouldn't go through this part (type 1 match) and the exponent doesn't define litre.
                    //Note that these parts aren't actually correct in many cases, just compatible with the AllCompounds format.
                    UnitTypes type2 = GetTypeFromUnit(part.Unit);
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
                        outParts = SimplifyCompoundComparisonUnitParts(outParts);
                    }
                    else outParts.Add(new UnitPart(part));
                }
            }

            return outParts;
        }

        private static List<UnitPart> SimplifyCompoundComparisonUnitParts(List<UnitPart> outParts)
        {
            for (int i = outParts.Count - 1; i >= 0; i--)
            {
                for (int i2 = i - 1; i2 >= 0; i2--)
                {
                    if (outParts[i].Unit == outParts[i2].Unit && outParts[i].Prefix == outParts[i2].Prefix)
                    {
                        //The scenario with different prefixes doesn't need to be considered because same-type
                        //basic units always have the same prefixes. And this is precisely what this method is
                        //about: simplifying basic units to eventually match a compound.
                        outParts[i].Exponent += outParts[i2].Exponent;

                        if (outParts[i].Exponent == 0)
                        {
                            outParts.RemoveAt(i);
                        }
                        outParts.RemoveAt(i2);

                        if (outParts.Count == 0 || i > outParts.Count - 1)
                        {
                            i = outParts.Count;
                            break;
                        }
                    }
                }
            }

            return outParts;
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
