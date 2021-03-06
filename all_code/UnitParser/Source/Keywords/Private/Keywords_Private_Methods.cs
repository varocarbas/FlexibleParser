﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    public partial class UnitP
    {
        //Extracts all the AllUnits information and stores it in more-usable specific collections.
        private static Dictionary<Units, UnitTypes> GetAllMain()
        {
            Dictionary<Units, UnitTypes> outDict = new Dictionary<Units, UnitTypes>();
            AllUnitConversionFactors = new Dictionary<Units, decimal>();
            AllUnitSystems = new Dictionary<Units, UnitSystems>();
            AllUnitStrings = new Dictionary<string, Units>();

            foreach (var item in AllUnits)
            {
                foreach (var item2 in item.Value)
                {
                    foreach (var item3 in item2.Value)
                    {
                        outDict.Add(item3.Key, item.Key);
                        AllUnitSystems.Add(item3.Key, item2.Key);
                        AllUnitConversionFactors.Add(item3.Key, item3.Value);

                        if (!IsUnnamedUnit(item3.Key) && StoreUnitNameIsOK(item3.Key))
                        {
                            string unitName = item3.Key.ToString().ToLower();
                            if (!AllUnitStrings.ContainsKey(unitName))
                            {
                                AllUnitStrings.Add(unitName, item3.Key);
                            }
                        }
                    }
                }
            }

            PopulateAllUnitStrings();
            PopulateUnitSymbols2();
            PopulateBeyondDecimalConversionFactors();

            AllUnits = null;

            return outDict;
        }

        private static void PopulateBeyondDecimalConversionFactors()
        {
            AllBeyondDecimalConversionFactors = new Dictionary<decimal, UnitInfo>();

            //Barn (b). Actual conversion factor 1E-28m.
            AllBeyondDecimalConversionFactors.Add
            (
                UnitConversionFactors.Barn, new UnitInfo()
                {
                    Value = 1, 
                    BaseTenExponent = -28
                }
            );

            //Debye (D). Actual conversion factor 3.33564095E-30m.
            AllBeyondDecimalConversionFactors.Add
            (
                UnitConversionFactors.Debye, new UnitInfo()
                {
                    Value = 3.33564095m,
                    BaseTenExponent = -30
                }
            );
        }

        //All the unit names are stored in AllUnitStrings, whose elements are treated case insensitively.
        //Not storing one of them would mean that all the associated string representations are always
        //treated case-sensitively to avoid confusions.
        private static bool StoreUnitNameIsOK(Units unit)
        {
            return
            (
                unit == Units.Rad //rad/radian.
                || unit == Units.Rutherford //rutherford/rod.
                || unit == Units.Gal //gal/gallon.
                ? false : true
            );
        }

        //Stores additional symbols (case does matter) for some units.
        private static void PopulateUnitSymbols2()
        {
            AllUnitSymbols2 = new Dictionary<string, Units>();

            AllUnitSymbols2.Add("nmi", Units.NauticalMile);
            AllUnitSymbols2.Add("ftm", Units.Fathom);
            AllUnitSymbols2.Add("th", Units.Thou);
            AllUnitSymbols2.Add("lnk", Units.Link);
            AllUnitSymbols2.Add("fm", Units.Fermi);
            AllUnitSymbols2.Add("psc", Units.Parsec);
            AllUnitSymbols2.Add("l", Units.Litre);
            AllUnitSymbols2.Add("p", Units.Pint);
            AllUnitSymbols2.Add("impp", Units.ImperialPint);
            AllUnitSymbols2.Add("uscp", Units.LiquidPint);
            AddToAllUnitStrings("liquidp", Units.LiquidPint);
            AddToAllUnitStrings("dryp", Units.DryPint);
            AllUnitSymbols2.Add("cm3", Units.CubicCentimetre);
            AllUnitSymbols2.Add("lbm", Units.Pound);
            AllUnitSymbols2.Add("car", Units.Carat);
            AllUnitSymbols2.Add("kgf", Units.Kilopond);
            AllUnitSymbols2.Add("r", Units.Revolution);
            AllUnitSymbols2.Add("stC", Units.Statcoulomb);
            AllUnitSymbols2.Add("stA", Units.Statampere);
            AllUnitSymbols2.Add("stV", Units.Statvolt);
            AllUnitSymbols2.Add("stΩ", Units.Statohm);
            AllUnitSymbols2.Add("stS", Units.Statsiemens);
            AllUnitSymbols2.Add("st℧", Units.Statmho);
            AllUnitSymbols2.Add("stF", Units.Statfarad);
            AllUnitSymbols2.Add("stH", Units.Stathenry);
        }

        //Populates all the unit string representations which aren't symbols (case doesn't matter).
        private static void PopulateAllUnitStrings()
        {
            Units unit = Units.Unitless;
            AddToAllUnitStrings("ul", unit);

            unit = Units.Metre;
            AddToAllUnitStrings("meter", unit);
            AddToAllUnitStrings("mtr", unit);

            unit = Units.Centimetre;
            AddToAllUnitStrings("centimeter", unit);

            unit = Units.AstronomicalUnit;
            AddToAllUnitStrings("ua", unit);

            unit = Units.SurveyInch;
            AddToAllUnitStrings("usin", unit);

            unit = Units.SurveyFoot;
            AddToAllUnitStrings("usft", unit);

            unit = Units.SurveyYard;
            AddToAllUnitStrings("usyd", unit);

            unit = Units.SurveyRod;
            AddToAllUnitStrings("usrd", unit);

            unit = Units.SurveyChain;
            AddToAllUnitStrings("usch", unit);

            unit = Units.SurveyLink;
            AddToAllUnitStrings("usli", unit);

            unit = Units.SurveyMile;
            AddToAllUnitStrings("usmi", unit);

            unit = Units.SurveyFathom;
            AddToAllUnitStrings("usfathom", unit);

            unit = Units.MetricTon;
            AddToAllUnitStrings("tonne", unit);

            unit = Units.Drachm;
            AddToAllUnitStrings("dram", unit);

            unit = Units.LongQuarter;
            AddToAllUnitStrings("longqr", unit);
            AddToAllUnitStrings("ukqr", unit);

            unit = Units.ShortQuarter;
            AddToAllUnitStrings("shortqr", unit);
            AddToAllUnitStrings("usqr", unit);

            unit = Units.LongHundredweight;
            AddToAllUnitStrings("longcwt", unit);
            AddToAllUnitStrings("ukcwt", unit);

            unit = Units.ShortHundredweight;
            AddToAllUnitStrings("shortcwt", unit);
            AddToAllUnitStrings("uscwt", unit);

            //Plural support is automatically added to all the string representations (e.g., the ones added here), 
            //but not to the symbols. For example: the aforementioned mtr reference already includes mtrs.
            unit = Units.Gram;
            AddToAllUnitStrings("gs", unit);

            unit = Units.Pound;
            AddToAllUnitStrings("lbs", unit);

            unit = Units.LongTon;
            AddToAllUnitStrings("longtn", unit);
            AddToAllUnitStrings("uktn", unit);

            unit = Units.ShortTon;
            AddToAllUnitStrings("shorttn", unit);
            AddToAllUnitStrings("ustn", unit);

            unit = Units.UnifiedAtomicMassUnit;
            AddToAllUnitStrings("amu", unit);

            unit = Units.Second;
            AddToAllUnitStrings("sec", unit);

            unit = Units.Hour;
            AddToAllUnitStrings("hr", unit);

            unit = Units.SquareMetre;
            AddToAllUnitStrings("squaremeter", unit);
            AddSqCuToAllUnitStrings(new string[] { "m" }, unit);
            
            unit = Units.SquareCentimetre;
            AddToAllUnitStrings("squarecentimeter", unit);
            AddSqCuToAllUnitStrings(new string[] { "cm" }, unit);

            unit = Units.SquareFoot;
            AddSqCuToAllUnitStrings(new string[] { "ft" }, unit);

            unit = Units.SquareInch;
            AddSqCuToAllUnitStrings(new string[] { "in" }, unit);

            unit = Units.SquareRod;
            AddSqCuToAllUnitStrings(new string[] { "rd" }, unit);

            unit = Units.SquarePerch;
            AddSqCuToAllUnitStrings(new string[] { "perch" }, unit);

            unit = Units.SquarePole;
            AddSqCuToAllUnitStrings(new string[] { "pole" }, unit);

            unit = Units.CubicMetre;
            AddToAllUnitStrings("cubicmeter", unit);
            AddSqCuToAllUnitStrings(new string[] { "m" }, unit, false);

            unit = Units.CubicCentimetre;
            AddToAllUnitStrings("cubiccentimeter", unit);
            AddSqCuToAllUnitStrings(new string[] { "cm" }, unit, false);

            unit = Units.CubicFoot;
            AddSqCuToAllUnitStrings(new string[] { "ft" }, unit, false);

            unit = Units.CubicInch;
            AddSqCuToAllUnitStrings(new string[] { "in" }, unit, false);

            unit = Units.Litre;
            AddToAllUnitStrings("liter", unit);
            AddToAllUnitStrings("ltr", unit);

            unit = Units.ImperialFluidOunce;
            AddToAllUnitStrings("ukfloz", unit);

            unit = Units.USCSFluidOunce;
            AddToAllUnitStrings("usfloz", unit);

            unit = Units.ImperialGill;
            AddToAllUnitStrings("ukgi", unit);

            unit = Units.USCSGill;
            AddToAllUnitStrings("usgi", unit);

            unit = Units.ImperialPint;
            AddToAllUnitStrings("ukpt", unit);
            AddToAllUnitStrings("ukp", unit);

            unit = Units.LiquidPint;
            AddToAllUnitStrings("uspt", unit);
            AddToAllUnitStrings("usp", unit);

            unit = Units.ImperialQuart;
            AddToAllUnitStrings("ukqt", unit);

            unit = Units.LiquidQuart;
            AddToAllUnitStrings("usqt", unit);

            unit = Units.ImperialGallon;
            AddToAllUnitStrings("ukgal", unit);

            unit = Units.LiquidGallon;
            AddToAllUnitStrings("usgal", unit);

            unit = Units.Degree;
            AddToAllUnitStrings("deg", unit);

            unit = Units.Arcsecond;
            AddToAllUnitStrings("arcsec", unit);

            unit = Units.Arcminute;
            AddToAllUnitStrings("arcmin", unit);

            unit = Units.BritishThermalUnit;
            AddToAllUnitStrings("btu", unit);

            unit = Units.ThermochemicalBritishThermalUnit;
            AddToAllUnitStrings("thbtu", unit);

            unit = Units.Therm;
            AddToAllUnitStrings("ecthm", unit);

            unit = Units.TonOfRefrigeration;
            AddToAllUnitStrings("tr", unit);
            AddToAllUnitStrings("rt", unit);

            unit = Units.DegreeCelsius;
            AddToAllUnitStrings("degC", unit);

            unit = Units.DegreeFahrenheit;
            AddToAllUnitStrings("degF", unit);

            unit = Units.DegreeRankine;
            AddToAllUnitStrings("degR", unit);

            unit = Units.ReciprocalMetre;
            AddToAllUnitStrings("inversemetre", unit);
            AddToAllUnitStrings("inversemeter", unit);
            AddToAllUnitStrings("reciprocalmeter", unit);

            unit = Units.InverseSquareMetre;
            AddToAllUnitStrings("reciprocalsquaremetre", unit);
            AddToAllUnitStrings("inversesquaremeter", unit);
            AddToAllUnitStrings("reciprocalsquaremeter", unit);

            unit = Units.ImperialMilePerGallon;
            AddToAllUnitStrings("ukmpg", unit);

            unit = Units.USCSMilePerGallon;
            AddToAllUnitStrings("usmpg", unit);

            unit = Units.BitPerSecond;
            AddToAllUnitStrings("bps", unit);
        }

        private static void AddSqCuToAllUnitStrings(string[] symbols, Units unit, bool square = true)
        {
            string addition = (square ? "sq" : "cu");

            foreach (string symbol in symbols)
            {
                AddToAllUnitStrings(addition + symbol, unit); 
            }
        }

        private static void AddToAllUnitStrings(string item, Units unit)
        {
            if (!AllUnitStrings.ContainsKey(item))
            {
                AllUnitStrings.Add(item, unit);
            }
        }
    }
}
