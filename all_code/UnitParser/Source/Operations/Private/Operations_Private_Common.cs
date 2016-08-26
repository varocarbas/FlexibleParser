using System;
using System.Collections.Generic;

namespace FlexibleParser
{
    public partial class UnitP
    {
        private static string GetOperationString(UnitP first, UnitP second, Operations operation)
        {
            return ConcatenateOperationString
            (
                GetUnitPString(first), GetUnitPString(second), OperationSymbols[operation][0]
            );
        }

        private static string GetOperationString(UnitP first, decimal second, Operations operation)
        {
            return ConcatenateOperationString
            (
                GetUnitPString(first), second.ToString(), OperationSymbols[operation][0]
            );
        }

        private static string GetOperationString(UnitP first, double second, Operations operation)
        {
            return ConcatenateOperationString
            (
                GetUnitPString(first), second.ToString(), OperationSymbols[operation][0]
            );
        }

        private static string GetOperationString(decimal first, UnitP second, Operations operation)
        {
            return ConcatenateOperationString
            (
                first.ToString(), GetUnitPString(second), OperationSymbols[operation][0]
            );
        }

        private static string GetOperationString(double first, UnitP second, Operations operation)
        {
            return ConcatenateOperationString
            (
                first.ToString(), GetUnitPString(second), OperationSymbols[operation][0]
            );
        }

        private static string GetUnitPString(UnitP unitP)
        {
            return unitP.OriginalUnitString;
        }

        private static string ConcatenateOperationString(string first, string second, char operation)
        {
            return (first + " " + operation + " " + second); 
        }

        //NOTE: the order within each char[] collection does matter. The first element will be treated as the default
        //symbol for the given operation (e.g., used when creating a string including that operation).
        private static Dictionary<Operations, char[]> OperationSymbols = new Dictionary<Operations, char[]>()
        {
            { 
                Operations.Addition, new char[] { '+', '➕' } 
            },
            { 
                Operations.Subtraction, new char[] { '-', '−', '—' } 
            },            
            { 
                Operations.Multiplication, new char[] { '*', 'x', 'X', '×', '⊗', '⋅', '·' } 
            },
            { 
                Operations.Division, new char[] { '/', '∕', '⁄', '÷', '|', '\\' }
            }
        };

        private enum Operations
        {
            None = 0,
            
            Addition,
            Subtraction,
            Multiplication,
            Division
        };

        private const double MaxValue = 79228162514264337593543950335.0; //Decimal.MaxValue actual value.
        private const double MinValue = 0.0000000000000000000000000001; //Decimal precision lowest limit.
    }
}
