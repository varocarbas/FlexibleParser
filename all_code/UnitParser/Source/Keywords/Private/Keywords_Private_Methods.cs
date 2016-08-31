using System;
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

        //All the unit names are stored in AllUnitStrings, whose elements are always case insensitive.
        //Not storing one of them means that the string representations of the given unit have always to
        //be treated case-sensitively.
        private static bool StoreUnitNameIsOK(Units unit)
        {
            return
            (
                //The rad unit cannot be represented as "rad" to avoid confusions with radians. 
                unit == Units.Rad ? 
                false : true
            );
        }

        //Populates additional symbols (i.e., unit string representations where case does matter) 
        //for some units.
        private static void PopulateUnitSymbols2()
        {
            AllUnitSymbols2 = new Dictionary<string, Units>();
            
            AllUnitSymbols2.Add("nmi", Units.NauticalMile);
            AllUnitSymbols2.Add("fath", Units.Fathom);
            AllUnitSymbols2.Add("mil", Units.Thou);
            AllUnitSymbols2.Add("lnk", Units.Link);
            AllUnitSymbols2.Add("fm", Units.Fermi);
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
            AddToAllUnitStrings("km/h", Units.KilometrePerHour);
            AddToAllUnitStrings("M/h", Units.MilePerHour);
            AllUnitSymbols2.Add("mi/h", Units.MilePerHour);
            AllUnitSymbols2.Add("lbf/in2", Units.PoundforcePerSquareInch);
            AllUnitSymbols2.Add("Btu", Units.BritishThermalUnit);
            AllUnitSymbols2.Add("thBtu", Units.ThermochemicalBritishThermalUnit);
            AllUnitSymbols2.Add("stC", Units.Statcoulomb);
            AllUnitSymbols2.Add("stA", Units.Statampere);
            AllUnitSymbols2.Add("stV", Units.Statvolt);
            AllUnitSymbols2.Add("stΩ", Units.Statohm);
            AllUnitSymbols2.Add("stS", Units.Statsiemens);
            AllUnitSymbols2.Add("st℧", Units.Statmho);
            AllUnitSymbols2.Add("stF", Units.Statfarad);
            AllUnitSymbols2.Add("stH", Units.Stathenry);
            AllUnitSymbols2.Add("mi/gal", Units.MilePerGallon);
            AllUnitSymbols2.Add("ukmpg", Units.ImperialMilePerGallon);
            AllUnitSymbols2.Add("usmpg", Units.USCSMilePerGallon);
        }

        //Populates all the unit string representations which aren't symbols. That is: when dealing
        //with them, case doesn't matter.
        private static void PopulateAllUnitStrings()
        {
            Units unit = Units.Metre;
            AddToAllUnitStrings("meter", unit);
            AddToAllUnitStrings("mtr", unit);

            unit = Units.AstronomicalUnit;
            AddToAllUnitStrings("ua", unit);

            unit = Units.ImperialCable;
            AddToAllUnitStrings("ukcbl", unit);

            unit = Units.USCSCable;
            AddToAllUnitStrings("uscbl", unit);

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

            unit = Units.SurveyMile;
            AddToAllUnitStrings("usmi", unit);

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

            unit = Units.LongTon;
            AddToAllUnitStrings("longtn", unit);
            AddToAllUnitStrings("uktn", unit);

            unit = Units.ShortTon;
            AddToAllUnitStrings("shorttn", unit);
            AddToAllUnitStrings("ustn", unit);

            unit = Units.Second;
            AddToAllUnitStrings("sec", unit);

            unit = Units.Hour;
            AddToAllUnitStrings("hr", unit);

            unit = Units.SquareMetre;
            AddSqCuToAllUnitStrings(new string[] { "m" }, unit);
            
            unit = Units.SquareCentimetre;
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
            AddSqCuToAllUnitStrings(new string[] { "m" }, unit, false);

            unit = Units.CubicCentimetre;
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

            unit = Units.TonOfRefrigeration;
            AddToAllUnitStrings("tr", unit);
            AddToAllUnitStrings("rt", unit);
            
            unit = Units.Celsius;
            AddToAllUnitStrings("degC", unit);

            unit = Units.Fahrenheit;
            AddToAllUnitStrings("degF", unit);

            unit = Units.Rankine;
            AddToAllUnitStrings("degR", unit);
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
