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
            bool inNumerator = true;

            foreach (UnitPart unitPart in unitInfo.Parts)
            {
                if (inNumerator && unitPart.Exponent < 0)
                {
                    inNumerator = false;
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

        private static ParsedExponent GetCompoundExponent(string input)
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

            ParsedExponent outVar = new ParsedExponent(input);
            if (i2 >= 0 && i2 <= input.Length - 1)
            {
                int exponent = 1;
                if (int.TryParse(input.Substring(i2 + 1), out exponent))
                {
                    outVar = new ParsedExponent
                    (
                        input.Substring(0, i2 + 1), 
                        exponent
                    );
                }
            }

            return outVar;
        }

        private static ParsedUnit UpdateUnitParts(ParsedUnit parsedUnit, StringBuilder inputSB, bool isNumerator, char symbol)
        {
            string input = inputSB.ToString();

            ParsedExponent exponent = GetCompoundExponent(input);
            if (!isNumerator) exponent.Exponent = -1 * exponent.Exponent;

            ParsedUnit parsedUnit2 = StartIndividualUnitParse
            (
                new ParsedUnit
                (
                    0m, exponent.AfterString,
                    parsedUnit.UnitInfo.Prefix.PrefixUsage
                )
            );

            if (parsedUnit2.UnitInfo.Unit == Units.None)
            {
                parsedUnit.UnitInfo.Error = new ErrorInfo(ErrorTypes.InvalidUnit);
                return parsedUnit; 
            }

            parsedUnit = AddInformationToValidUnitPart
            (
                parsedUnit, symbol, exponent, input
            );

            parsedUnit.UnitInfo.Parts.Add
            (
                new UnitPart
                (
                    parsedUnit2.UnitInfo.Unit, parsedUnit2.UnitInfo.Prefix.Factor,
                    exponent.Exponent
                )
            );

            return parsedUnit;
        }

        private static ParsedUnit AddInformationToValidUnitPart(ParsedUnit parsedUnit, char symbol, ParsedExponent exponent, string input)
        {
            if (symbol != ' ') parsedUnit.ValidCompound.Append(symbol);

            if (exponent.AfterString.Trim().Length > 0)
            {
                parsedUnit.ValidCompound.Append(exponent.AfterString);
            }

            string exponent2 = input.Replace(exponent.AfterString, "").Trim();
            if (exponent2.Length > 0)
            {
                parsedUnit.ValidCompound.Append(exponent2);
            }

            return parsedUnit;
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
