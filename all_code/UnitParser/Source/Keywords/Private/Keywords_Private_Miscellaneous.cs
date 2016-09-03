using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    public partial class UnitP
    {
        //While determining the system from the contituent parts, some units might be misinterpreted. 
        //For example: in ft/s, assuming SI (because of s) would be wrong. This collection avoids these
        //problems by including types to be ignored in these analyses (i.e., neutral types).
        private static UnitTypes[] NeutralTypes = new UnitTypes[]
        {
            UnitTypes.Time, UnitTypes.Angle, UnitTypes.SolidAngle,
            UnitTypes.ElectricCurrent, UnitTypes.AmountOfSubstance,
            UnitTypes.Temperature
        };

        //List of types whose conversion requires more than just applying a conversion factor.
        private static UnitTypes[] SpecialConversionTypes = new UnitTypes[]
        {
            UnitTypes.Temperature
        };

        //Class storing all the unit-related information. 
        //It includes the main numeric variables (Value, Prefix.Factor & BaseTenExponent) and, consequently,
        //is also the managed operations (i.e., error-free-ly dealing with numbers of any size) basic class.
        private class UnitInfo
        {
            public decimal Value { get; set; }
            public Units Unit { get; set; }
            public UnitSystems System { get; set; }
            public UnitTypes Type { get; set; }
            public Prefix Prefix { get; set; }
            public List<UnitPart> Parts { get; set; }
            public ErrorInfo Error { get; set; }
            public string TempString { get; set; }
            public int BaseTenExponent { get; set; }
            //Collection storing the positions of the unit parts as input by the user. 
            public Dictionary<UnitPart, int> InitialPositions { get; set; }

            public UnitInfo() : this(0m) { }

            public UnitInfo(Units unit, decimal prefixFactor) : this(0m, unit, new Prefix(prefixFactor)) { }
            
            public UnitInfo(decimal value, Units unit, Prefix prefix, bool getParts = true)
            {
                PopulateVariables
                (
                    value, unit, prefix, (getParts ? null : new List<UnitPart>()),
                    new Dictionary<UnitPart, int>(), 0, UnitTypes.None, UnitSystems.None, null
                );
            }

            public UnitInfo(UnitInfo unitInfo, ErrorTypes error = ErrorTypes.None)
            {
                if (unitInfo == null) unitInfo = new UnitInfo();

                PopulateVariables
                (
                    unitInfo.Value, unitInfo.Unit, unitInfo.Prefix, 
                    unitInfo.Parts, GetInitialPositions(unitInfo.Parts),
                    unitInfo.BaseTenExponent, unitInfo.Type, unitInfo.System,
                    (error != ErrorTypes.None ? new ErrorInfo(error) : unitInfo.Error)
                );
            }

            public UnitInfo(UnitP unitP)
            {
                if (unitP == null)
                {
                    Prefix = new Prefix();
                    Parts = new List<UnitPart>();
                    Error = new ErrorInfo();
                    return;
                }

                List<UnitPart> unitParts = new List<UnitPart>();
                if (unitP.UnitParts != null && unitP.UnitParts.Count > 0)
                {
                    unitParts = new List<UnitPart>(unitP.UnitParts.ToList());
                }

                PopulateVariables
                (
                    unitP.Value, unitP.Unit, unitP.UnitPrefix, unitParts,
                    GetInitialPositions(unitParts), unitP.BaseTenExponent,
                    unitP.UnitType, unitP.UnitSystem, unitP.Error
                );
            }

            public UnitInfo(decimal value, int bigNumberExponent = 0)
            {
                PopulateVariables
                (
                    value, Units.None, new Prefix(), null, null, BaseTenExponent
                );
            }

            private void PopulateVariables
            (
                decimal value, Units unit, Prefix prefix, List<UnitPart> parts,
                Dictionary<UnitPart, int> initialPositions, int bigNumberExponent = 0,
                UnitTypes type = UnitTypes.None, UnitSystems system = UnitSystems.None, 
                ErrorInfo errorInfo = null
            )
            {
                Value = value;
                BaseTenExponent = bigNumberExponent;
                Unit = unit;
                Prefix = new Prefix(prefix);
                if (parts == null)
                {
                    Parts = new List<UnitPart>();
                    Parts = new List<UnitPart>
                    (
                        GetUnitParts(this).Parts
                    );
                    InitialPositions = GetInitialPositions(Parts);
                }
                else
                {
                    InitialPositions = new Dictionary<UnitPart, int>(initialPositions);
                    Parts = new List<UnitPart>(parts);
                }
                System = system;
                Type = type;
                TempString = "";
                Error = new ErrorInfo(errorInfo);
            }

            public static UnitInfo operator +(UnitInfo first, UnitInfo second)
            {
                return PerformManagedOperationUnits(first, second, Operations.Addition);
            }

            public static UnitInfo operator +(UnitInfo first, decimal second)
            {
                return PerformManagedOperationUnits(first, second, Operations.Addition);
            }

            public static UnitInfo operator +(decimal first, UnitInfo second)
            {
                return PerformManagedOperationUnits(first, second, Operations.Addition);
            }

            public static UnitInfo operator -(UnitInfo first, UnitInfo second)
            {
                return PerformManagedOperationUnits(first, second, Operations.Subtraction);
            }

            public static UnitInfo operator -(UnitInfo first, decimal second)
            {
                return PerformManagedOperationUnits(first, second, Operations.Subtraction);
            }

            public static UnitInfo operator -(decimal first, UnitInfo second)
            {
                return PerformManagedOperationUnits(first, second, Operations.Subtraction);
            }

            public static UnitInfo operator *(UnitInfo first, UnitInfo second)
            {
                return PerformManagedOperationUnits(first, second, Operations.Multiplication);
            }

            public static UnitInfo operator *(UnitInfo first, decimal second)
            {
                return PerformManagedOperationUnits(first, second, Operations.Multiplication);
            }

            public static UnitInfo operator *(decimal first, UnitInfo second)
            {
                return PerformManagedOperationUnits(first, second, Operations.Multiplication);
            }

            public static UnitInfo operator /(UnitInfo first, UnitInfo second)
            {
                return PerformManagedOperationUnits(first, second, Operations.Division);
            }

            public static UnitInfo operator /(UnitInfo first, decimal second)
            {
                return PerformManagedOperationUnits(first, second, Operations.Division);
            }

            public static UnitInfo operator /(decimal first, UnitInfo second)
            {
                return PerformManagedOperationUnits(first, second, Operations.Division);
            }
        }

        //Characters which are ignored while parsing units. For example: m or m. or (m) are identical.
        //NOTE: the decimal separator characters would have to be removed from this list in order to
        //start supporting decimal exponents.
        private static string[] UnitParseIgnored = new string[]
        {
            ".", ",", ":", ";", "_", "^", "+", "#", "(", ")", "[", "]", 
            "{", "}", "=", "!", "?", "@", "&"
        };
    }
}
