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

                string unitString = unitPart.Prefix.Symbol + AllUnitSymbols.First(x => x.Value == unitPart.Unit).Key;
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
                    if (UnitPartsMatchCompoundParts(unitInfo.Parts, compound.Parts))
                    {
                        unitInfo.Type = allCompound.Key;
                        break;
                    }
                }
            }

            return unitInfo;
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
