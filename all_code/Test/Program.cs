﻿using System;
using System.Collections.Generic;
using System.Linq;
using FlexibleParser;

namespace Test
{
    class Program
    {
        private static void Main(string[] args)
        {
            //------ The base class is UnitP. There are multiple ways to instantiate a UnitP variable.
            PrintSampleItem("Inst1", new UnitP("1 N")); //Unit symbol. Case does matter.
            PrintSampleItem("Inst2", new UnitP(1m, UnitSymbols.Newton));
            PrintSampleItem("Inst3", new UnitP(1m, "nEwTon")); //Unit secondary string representation. Case doesn't matter.  
            PrintSampleItem("Inst4", new UnitP(Units.Newton)); //Value assumed to be 1.
            PrintSampleItem("Inst5", new UnitP()); //Value and unit assumed to be 1 and unitless, respectively.


            //--- All the public classes support (un)equality comparisons accounting for their more relevant variables.
            if (new UnitP("1 N") == new UnitP(1m, UnitSymbols.Newton) && new UnitP(1m, UnitSymbols.Newton) == new UnitP(1m, "nEwTon") && new UnitP(1m, "nEwTon") == new UnitP(Units.Newton))
            {
                //This condition is true.
            }

            //------ UnitP variables can be seen as abstract concepts including many specific types.
            
            //--- Same type variables can be added/subtracted.
            PrintSampleItem("Add1", new UnitP("1 N") + new UnitP(1m, UnitSymbols.Newton)); //Both variables have the same type (force).

            //--- Different type variables can be multiplied/divided, but only when the resulting output belongs to a supported type.
            PrintSampleItem("Mult1", new UnitP("1 N") * new UnitP("m")); //N*m = J, what is a supported type (energy).

            //--- Any operation outputting unsupported types triggers an error. 
            PrintSampleItem("Mult2", new UnitP("1 N") * new UnitP("1 m") * new UnitP("1 m")); //N*m2 doesn't match any valid type.


            //------ Multiplication/division with decimal/double values is also supported.

            PrintSampleItem("Mult3", new UnitP("1 N") * 1.23456); //Multiplication involving UnitP and double variables.
            PrintSampleItem("Div1", new UnitP("1 N") / 7.891011m); //Division involving UnitP and decimal variables.

            //--- Dividing a number by a UnitP variable does affect the given unit.
            try
            {
                PrintSampleItem("Div2", 7.891011m / new UnitP("1 N"));
            }
            catch
            {
                //Error because 1/N doesn't represent a supported type.
                //The reasons for the exception are explained below.
                Console.WriteLine("Div2 - Caught Exception.");
            }


            //------ Compounds, unit parts and individual units.

            PrintSampleItem("Ind1", new UnitP("1 sec")); //s, a valid SI time unit. It is an individual unit (i.e., 1 single unit part whose exponent is 1).
            PrintSampleItem("Comp1", new UnitP("1 m/s")); //m/s, a valid SI velocity unit. It is a compound (i.e., various parts or one with a different-than-1 exponent).
            PrintSampleItem("Comp2", new UnitP("1 N")); //N, a valid SI force unit. It is a compound with an official name.

            //--- Compounds can be formed through string parsing or arithmetic operations.
            PrintSampleItem("Comp3", new UnitP("1 m") / new UnitP("s")); //m/s, a valid SI velocity unit, created by dividing two UnitP variables.
            PrintSampleItem("Comp4", new UnitP("kg*m/s2")); //N (= kg*m/s2), a valid SI force unit, created via string parsing. 
            if (new UnitP("1 m/sec") == new UnitP("1 m") / new UnitP("s") && new UnitP("1 N") == new UnitP("1 kg*m/s2"))
            {
                //This condition is true.
            }

            //--- It is recommendable to create compounds via strings rather than operations.
            PrintSampleItem("Comp5", new UnitP("1 m") / new UnitP("1 s2")); //Error because s2 doesn't represent a valid type (type check for each UnitP variable).
            PrintSampleItem("Comp6", new UnitP("m/s2")); //m/s2, a valid SI acceleration unit (one type check after all the unit operations/simplifications were performed).

            //--- The unit parts are automatically populated when instantiating a valid UnitP variable.
            if (new UnitP("1 N").UnitParts.FirstOrDefault(x => !new UnitP("1 kg*m/s2").UnitParts.Contains(x)) == null)
            {
                //This condition is true.
            }

            //--- When various units have the same constituent parts, the string-based recognition might not match the input unit.
            PrintSampleItem("Comp7", new UnitP("V/m")); //Understood as NewtonPerCoulomb. Although V/m is a valid SI electric field strength unit, N/C has the same parts and is the default unit under these conditions. 
            PrintSampleItem("Comp8", new UnitP(Units.VoltPerMetre)); //Understood as VoltPerMetre. There is no string parsing/unit-part analysis and, consequently, no possible confusion. 

            //------ Format of input string units.

            //--- UnitP constructors without numeric inputs expect strings formed by number (it might be missing), blank space and unit.
            PrintSampleItem("Str1", new UnitP("10 m")); //10 metre (length).
            PrintSampleItem("Str2", new UnitP("1m")); //Error.
            PrintSampleItem("Str3", new UnitP("m")); //1 metre (length).

            //--- Multi-part strings are expected to be formed by units, multiplication/division symbols and integer exponents.
            PrintSampleItem("Str4", new UnitP("1 J/s")); //1 joule per second (power unit).
            PrintSampleItem("Str5", new UnitP("1 Jxs")); //1 joule second (angular momentum).
            PrintSampleItem("Str6", new UnitP("1 J⋅s2")); //1 kilogram square metre (moment of inertia).
            PrintSampleItem("Str7", new UnitP("J÷s-2")); //1 kilogram square metre (moment of inertia).

            //--- Only one division sign is expected. It separates the numerator and denominator parts.
            PrintSampleItem("Str8", new UnitP("1 J*J/s*J2*J-1*s*s-1")); //1 watt (power).
            PrintSampleItem("Str9", new UnitP("J*J/(s*J2*s)*J*s")); //Error. It is understood as J*J/(s*J2*s*J*s).

            //--- Not-supported-but-commonly-used characters are plainly ignored.
            PrintSampleItem("Str10", new UnitP(1m, "ft.")); //1 foot (length).
            PrintSampleItem("Str11", new UnitP(1m, "ft^2")); //1 square foot (area).
            PrintSampleItem("Str12", new UnitP(1m, "ft*(ft*ft)")); //1 cubic foot (volume).

            //--- Ideally, no blank spaces should be included. The parser can deal with them anyway.
            PrintSampleItem("Str13", new UnitP(1m, "AU/min")); //1 astronomical unit per minute (velocity).
            PrintSampleItem("Str14", new UnitP(1m, "A U/     min")); //1 astronomical unit per minute (velocity).


            //------ Format of input string numbers.

            //--- The used culture is always CultureInfo.InvariantCulture (e.g., "." as decimal separator). 
            PrintSampleItem("StrNum1", new UnitP("1.1 m")); //Always 1.1 m, independently upon the applicable culture.
            PrintSampleItem("StrNum2", new UnitP("1,1 s")); //Always 11 s, independently upon the applicable culture.

            //--- The differences between double/decimal types are managed internally.
            PrintSampleItem("StrNum3", new UnitP("1.0000000000000001 ft")); //1.0000000000000001 ft.
            PrintSampleItem("StrNum4", new UnitP("1000000000000000000000000000000000000000000000000000000000000 mi")); //1000000*10^54 mi.

            //--- It is also possible to input beyond-double numbers via strings. The exponential format follows the .NET rules.
            PrintSampleItem("StrNum5", new UnitP("9999.99999E1000")); //999999.99*10^998 unitless.
            PrintSampleItem("StrNum6", new UnitP("1234E1.5")); //Error. Equivalently to what happens with .NET numeric parsing, only integer exponents are supported.


            //------ Errors and exceptions.

            //--- All the error information is stored under UnitPVariable.Error.
            if (new UnitP("1 m/s").Error.Type == UnitP.ErrorTypes.None)
            {
                //This condition is true.
            }
            if (new UnitP("wrong").Error.Type != UnitP.ErrorTypes.None)
            {
                //This condition is true.
            }

            //--- By default, errors don't trigger exceptions.
            PrintSampleItem("Err1", new UnitP("wrong")); //No exception is triggered.

            //--- The default behaviour can be modified when instantiating the variable.
            try
            {
                PrintSampleItem("Err2", new UnitP("wrong", UnitP.ExceptionHandlingTypes.AlwaysTriggerException));
            }
            catch 
            {
                //An exception is triggered.
                Console.WriteLine("Err2 - Caught Exception.");
            }

            //--- In case of incompatibility, the configuration of the first operand is applied.
            PrintSampleItem
            (
                "Err3",  //No exception is triggered.
                new UnitP("wrong") * 
                new UnitP("1 m", UnitP.ExceptionHandlingTypes.AlwaysTriggerException)
            );

            try
            {
                PrintSampleItem
                (
                    "Err4", //An exception is triggered.
                    new UnitP("1 m", UnitP.ExceptionHandlingTypes.AlwaysTriggerException) *
                    new UnitP("wrong")
                );
            }
            catch 
            {
                Console.WriteLine("Err4 - Caught Exception.");
            }

            //--- When the first operand is a number, an exception is always triggered.
            try
            {
                //An exception is triggered.
                PrintSampleItem("Err5", 5.0 * new UnitP("wrong"));
            }
            catch 
            {
                Console.WriteLine("Err5 - Caught Exception.");
            }


            //------ Unit prefixes.

            //--- Two types of prefixes are supported: SI and binary.
            PrintSampleItem("Pref1", new UnitP(1m, "km")); //SI prefix kilo + metre.
            PrintSampleItem("Pref2", new UnitP("1 Kibit")); //Binary prefix Kibi + bit.

            //--- All the prefix-related information is stored under UnitPVariable.UnitPrefix.
            if (new UnitP(1m, "km").UnitPrefix == new UnitP(1m, "ks").UnitPrefix)
            {
                //This condition is true.
            }

            //--- Prefixes are automatically updated/simplified in any operation.
            PrintSampleItem("Pref3", new UnitP("1 kJ")); //1 kJ.
            PrintSampleItem("Pref4", new UnitP("555 mJ")); //555 mJ.
            PrintSampleItem("Pref5", new UnitP("0.00000001 MJ")); //0.01 J.
            PrintSampleItem("Pref6", new UnitP("1 kJ") + new UnitP("555 mJ") + new UnitP("0.00000001 MJ")); //1.000565 kJ.

            //--- Prefix symbols are case sensitive, but string representations are not.
            PrintSampleItem("Pref7", new UnitP(1m, "Km")); //Error.
            PrintSampleItem("Pref8", new UnitP(1m, "mEGam")); //SI prefix mega + metre.

            //--- By default, prefixes can only be used with units which officially/commonly support them.
            PrintSampleItem("Pref9", new UnitP("1 Mft")); //Error because the unit foot doesn't support SI prefixes.
            PrintSampleItem("Pref10", new UnitP("Eim")); //Error because the unit metre doesn't support binary prefixes.

            //--- The default behaviour can be modified when instantiating the variable.
            PrintSampleItem("Pref11", new UnitP("Mft", PrefixUsageTypes.AllUnits)); //SI prefix mega + foot.
            PrintSampleItem("Pref12", new UnitP("1 Eim", PrefixUsageTypes.AllUnits)); //Binary prefix exbi + metre.

            //--- Same rules apply to officially-named compounds. Non-named compounds recognise prefixes, but don't use them.
            PrintSampleItem("Pref13", new UnitP("1 GN")); //SI prefix giga + newton.
            PrintSampleItem("Pref14", new UnitP(1m, SIPrefixSymbols.Giga + Units.MetrePerSecond)); //1000000*10^3 m/s.

            //--- In certain situations, PrefixUsageTypes.AllUnits doesn't allow to use prefixes.
            PrintSampleItem("Pref15", new UnitP(1m, SIPrefixSymbols.Giga + Units.MetrePerSecond, PrefixUsageTypes.AllUnits)); //Compounds with no official name don't support prefixes.
            PrintSampleItem("Pref16", new UnitP("100000000000 unitless", PrefixUsageTypes.AllUnits)); //Unitless variables don't support prefixes.

            //--- The unit parts can also have prefixes, which might be compensated with the main prefix.
            PrintSampleItem("Pref17", new UnitP(1m, SIPrefixSymbols.Kilo + UnitSymbols.Newton)); //kN. SI prefix kilo directly affecting the unit newton.
            PrintSampleItem("Pref18", new UnitP("1 Mg*m/s2")); //kN. SI prefix kilo indirectly affecting newton, via one of its constituent parts (kilo-kg = Mg).
            if (new UnitP(1m, SIPrefixSymbols.Kilo + UnitSymbols.Newton) == new UnitP("1 Mg*m/s2"))
            {
                //This condition is true.
                //Note that both UnitP variables being equal implies identical prefixes.
            }


            //------ Systems of units.

            //--- The system is automatically determined at variable instantiation. Each unit can belong to just one system.
            PrintSampleItem("Sys1", new UnitP(Units.MetrePerSquareSecond)); //SI acceleration unit (m/s2).
            PrintSampleItem("Sys2", new UnitP("cm/s2")); //CGS acceleration unit (Gal).
            PrintSampleItem("Sys3", new UnitP(1m, UnitSymbols.Rod + "/h2")); //Imperial acceleration unit (rd/h2). 
            PrintSampleItem("Sys4", new UnitP(1m, UnitSymbols.SurveyRod + "/s2")); //USCS acceleration unit (surrd/s2). 
            PrintSampleItem("Sys5", new UnitP(1m, "AU/min2")); //Acceleration unit not belonging to any system (AU/min2). 


            //------ Automatic unit conversions.

            //--- Automatic conversions (to the system of the first operand) happen in operations between a big proportion of different-system units.
            PrintSampleItem("Conv1", new UnitP(1m, Units.Metre) * new UnitP("1 ft")); //After converting ft to metre, SI area unit m2.
            PrintSampleItem("Conv2", new UnitP(Units.PoundForce) + new UnitP(5m, "N")); //After converting N to lbf, Imperial/USCS force unit lbf.

            //--- Same rules apply to compounds whose unit parts belong to different systems.
            PrintSampleItem("Conv3", new UnitP(1m, "m*lb/s2")); //After converting lb to kg, SI force unit N.
            PrintSampleItem("Conv4", new UnitP(1m, "surin3/in")); //After converting in to surin, USCS area unit surin2.


            //------ Numeric support.

            //--- UnitP variables support two different numeric types: decimal and double. 
            PrintSampleItem("Num1", new UnitP(1.23456m, "m")); //The UnitP constructor overloads only support decimal type.
            PrintSampleItem("Num2", new UnitP("ft") * 7.891011m); //Decimal variables can be used in multiplications/divisions.
            PrintSampleItem("Num3", new UnitP("s") * 1213141516.0); //Double variables can be used in multiplications/divisions.
            
            //--- All the numeric inputs are converted into decimal type. UnitPVariable.BaseTenExponent avoids eventual type-conversion overflow problems.          
            PrintSampleItem
            (
                //UnitP variable with a Numeric value notably above decimal.MaxValue.
                "Num4", new UnitP(9999999999999999m, "YAU2", PrefixUsageTypes.AllUnits) 
                / new UnitP("0.000000000000001 yf", PrefixUsageTypes.AllUnits)
            );
            PrintSampleItem
            (
                //UnitP variable with a Numeric value notably below decimal.MinValue.
                "Num5", 0.0000000000000000000000000000000000000000000000001 * 
                new UnitP(0.000000000000000000001m, "ym2") / new UnitP("999999999999999999999 Ym") 
            );


            //------ No unit, unitless & unnamed units.

            //--- No unit (Units.None).
            PrintSampleItem("No1", new UnitP(1m, Units.None)); //Units.None cannot be used as an input.
            PrintSampleItem("No2", new UnitP("1 wrong")); //Units.None is the unit associated with all the errors.

            //--- Unitless (Units.Unitless).
            PrintSampleItem("No3", new UnitP(1m, Units.Unitless)); //Units.Unitless can be used as an input.
            PrintSampleItem("No4", new UnitP("5e1234")); //Units.Unitless is the unit for purely-numeric calculations.
            PrintSampleItem("No5", new UnitP("5 km") / new UnitP(1m, Units.Unitless)); //Units.Unitless can be used together with other valid units without triggering an error.
            PrintSampleItem("No6", new UnitP("1 ft/m")); //Units.Unitless is associated with the output of operations where all the units cancel each other (with or without automatic conversions).

            //--- Unnamed units (Units.Valid[system]Unit).
            PrintSampleItem("No7", new UnitP("yd/s")); //All the parsed compounds not matching any named unit are automatically included in this category.
            PrintSampleItem("No8", new UnitP(1m, Units.ValidCGSUnit)); //Error. Unnamed units cannot be used as inputs.


            //------ Public functions.

            //--- All the functions have static/UnitP and non-static/UnitPVariable versions.
            Console.WriteLine
            (
                "Func1 - " + Units.Abampere.ToString() + " -> " + 
                string.Join(",", UnitP.GetStringsForUnit(Units.Abampere, true)) //Static method returning all the string representations associated with the input unit.
            );
            Console.WriteLine
            (
                "Func2 - " + Units.Abampere.ToString() + " -> " + 
                string.Join(",", new UnitP(1m, Units.Abampere).GetStringsForCurrentUnit(true)) //Non-static method returning all the string representations associated with the current unit.
            );

            //--- The most relevant function is the one performing unit conversions. 
            PrintSampleItem("Func3", UnitP.ConvertTo(new UnitP("1 m"), Units.Foot)); //Static version of the unit conversion method.
            PrintSampleItem("Func4", new UnitP("1 m/s").ConvertCurrentUnitTo("ft/h")); //Non-static version of the unit conversion method.
            PrintSampleItem("Func5", new UnitP("1 m").ConvertCurrentUnitTo(Units.Gram)); //Error. No conversion is possible between different-type units.


            //-------------------------------------------------------------

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Do you want to print all the named units? Y/N");
            string input = Console.ReadLine();

            if (input.ToLower() == "y")
            {
                //--- Printing all the supported named units out.
                PrintAllNamedUnits();
                Console.Read();
            }
        }

        private static void PrintSampleItem(string sampleId, UnitP unitP)
        {
            Console.WriteLine
            (
                sampleId + " - " +
                (
                    unitP.Error.Type != UnitP.ErrorTypes.None ?
                    "Error. " + unitP.Error.Message :
                    unitP.ValueAndUnitString + " (" +
                    unitP.Unit.ToString() + ", " +
                    unitP.UnitType.ToString() + ", " + 
                    unitP.UnitSystem.ToString() + ")."
                )
            );
        }

        //This method prints the main string representations and some basic information for all the named units.
        //In any case, note that UnitParser supports a wide range of variations which aren't referred here.
        //Examples: plurals of string representation other than symbols or ignoring certain invalid characters
        //(e.g., blank spaces or conventionally-used characters like "^").
        //Additionally, bear in mind that these are just the members of the Units enum, a small fraction of 
        //all the units supported by UnitParser. Any unit belonging to a supported type (UnitTypes enum) which 
        //is formed by the combination of one or more named units (Units enum) is also supported. For example,
        //the named unit Units.Foot can be part of many other unnamed units like ft/h (velocity), rood*ft (volume)
        //or tn*ft/s2 (force).
        private static void PrintAllNamedUnits()
        {
            foreach (Units unit in Enum.GetValues(typeof(Units)))
            {
                if (unit == Units.None || unit == Units.Unitless) continue;

                UnitTypes type = UnitP.GetUnitType(unit);
                UnitSystems system = UnitP.GetUnitSystem(unit);

                if (type == UnitTypes.None) continue;

                Console.WriteLine("Unit: " + unit.ToString());
                Console.WriteLine("Type: " + type.ToString());
                Console.WriteLine("System: " + system.ToString());

                string representations = "";
                foreach (string representation in UnitP.GetStringsForUnit(unit, true))
                {
                    if (representations != "") representations += ", ";
                    representations += representation;
                }

                Console.WriteLine("Representations: " + representations);
                Console.WriteLine();
            }
        }
    }
}