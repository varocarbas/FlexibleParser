using System;
using System.Collections.Generic;
using System.Linq;
using FlexibleParser;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {

            //------ The base class is UnitP. There are multiple ways to instantiate a UnitP variable.

            PrintSampleItem("Inst1", new UnitP("1 N")); //Unit symbol. Caps does matter.
            PrintSampleItem("Inst2", new UnitP(1m, UnitSymbols.Newton));
            PrintSampleItem("Inst3", new UnitP(1m, "nEwTon")); //Unit secondary string representation. Caps doesn't matter.  
            PrintSampleItem("Inst4", new UnitP(1m, Units.Newton));

            //--- All the public classes support (un)equality comparisons accounting for their peculiaries.
            if (new UnitP("1 N") == new UnitP(1m, UnitSymbols.Newton) && new UnitP(1m, UnitSymbols.Newton) == new UnitP(1m, "nEwTon") && new UnitP(1m, "nEwTon") == new UnitP(1m, Units.Newton))
            {
                //This condition is true.
            }


            //------ UnitP variables can be seen as abstract concepts including many specific types.
            
            //--- Same type variables can be added/subtracted.
            PrintSampleItem("Add1", new UnitP("1 N") + new UnitP(1m, UnitSymbols.Newton)); //All of them have the same type: force.

            //--- Different type variables can be multiplied/divided, but only when the generated unit belongs to a supported type.
            PrintSampleItem("Mult1", new UnitP("1 N") * new UnitP("1 m")); //N*m = J, what is a supported type (energy).

            //--- Any operation outputting unsupported types triggers an error. 
            PrintSampleItem("Mult2", new UnitP("1 N") * new UnitP("1 m") * new UnitP("1 m")); //N*m2 doesn't match any valid type.


            //------ Multiplication/division with decimal/double values are also supported.

            PrintSampleItem("Mult3", new UnitP("1 N") * 1.23456); //Multiplication between UnitP and double variables.
            PrintSampleItem("Div1", new UnitP("1 N") / 7.891011m); //Division between UnitP and decimal variables.

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

            PrintSampleItem("Ind1", new UnitP("1 sec")); //Individual unit (i.e., 1 single unit part whose exponent is 1).
            PrintSampleItem("Comp1", new UnitP("1 m/sec")); //Compound (i.e., various parts or one with a different-than-1 exponent).
            PrintSampleItem("Comp2", new UnitP("1 N")); //Compound with an official name.

            //--- Compounds can be formed through string parsing or arithmetic operations.
            PrintSampleItem("Comp3", new UnitP("1 m") / new UnitP("1 s")); //Unit m/s created by dividing two UnitP variables.
            PrintSampleItem("Comp4", new UnitP("1 kg*m/s2")); //Unit N (= kg*m/s2) created via string parsing. 
            if (new UnitP("1 m/sec") == new UnitP("1 m") / new UnitP("1 s") && new UnitP("1 N") == new UnitP("1 kg*m/s2"))
            {
                //This condition is true.
            }

            //--- The unit parts are automatically populated when instantiating a valid UnitP variable.
            if (new UnitP("1 N").UnitParts.FirstOrDefault(x => !new UnitP("1 kg*m/s2").UnitParts.Contains(x)) == null)
            {
                //This condition is true.
            }


            //------ Format of input strings.

            //--- UnitP constructors without numeric inputs expect strings formed by number, blank space and unit.
            PrintSampleItem("Str1", new UnitP("1 m")); //1 metre (length).
            PrintSampleItem("Str2", new UnitP("1m")); //Error.

            //--- Multi-part strings are expected to be formed by units, multiplication/division symbols and exponents.
            PrintSampleItem("Str3", new UnitP("1 J/s")); //1 joule per second (power unit).
            PrintSampleItem("Str4", new UnitP("1 Jxs")); //1 joule second (angular momentum).
            PrintSampleItem("Str5", new UnitP("1 J⋅s2")); //1 joule square second (moment of inertia).
            PrintSampleItem("Str6", new UnitP("1 J÷s-2")); //1 joule square second (moment of inertia).

            //--- Only one division sign is expected. It separates the numerator and denominator parts.
            PrintSampleItem("Str7", new UnitP("1 J*J/s*J2*J-1*s*s-1")); //1 watt (power).
            PrintSampleItem("Str8", new UnitP("1 J*J/(s*J2*s)*J*s")); //Error. It is understood as J*J/(s*J2*s*J*s).

            //--- Not-supported-but-commonly-used characters are plainly ignored.
            PrintSampleItem("Str9", new UnitP(1m, "ft.")); //1 foot (length).
            PrintSampleItem("Str10", new UnitP(1m, "ft^2")); //1 square foot (area).
            PrintSampleItem("Str11", new UnitP(1m, "ft*(ft*ft)")); //1 cubic foot (volume).

            //--- Ideally, no blank spaces should be included. The parser can deal with them anyway.
            PrintSampleItem("Str12", new UnitP(1m, "AU/min")); //1 astronomical unit per minute (velocity).
            PrintSampleItem("Str13", new UnitP(1m, "A U/     min")); //1 astronomical unit per minute (velocity).


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
            PrintSampleItem("Err3", new UnitP("wrong"));
            PrintSampleItem("Err4", new UnitP("1 m", UnitP.ExceptionHandlingTypes.AlwaysTriggerException));
            PrintSampleItem
            (
                "Err5",  //No exception is triggered.
                new UnitP("wrong") * 
                new UnitP("1 m", UnitP.ExceptionHandlingTypes.AlwaysTriggerException)
            );

            try
            {
                PrintSampleItem
                (
                    "Err6", //An exception is triggered.
                    new UnitP("1 m", UnitP.ExceptionHandlingTypes.AlwaysTriggerException) *
                    new UnitP("wrong")
                );
            }
            catch 
            {
                Console.WriteLine("Err6 - Caught Exception.");
            }

            //--- When the first operand is a number, an exception is always triggered.
            try
            {
                //An exception is triggered.
                PrintSampleItem("Err7", 5.0 * new UnitP("wrong"));
            }
            catch 
            {
                Console.WriteLine("Err7 - Caught Exception.");
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
            PrintSampleItem("Pref3", new UnitP("1 kJ"));
            PrintSampleItem("Pref4", new UnitP("555 mJ"));
            PrintSampleItem("Pref5", new UnitP("0.00000001 MJ"));
            PrintSampleItem("Pref6", new UnitP("1 kJ") + new UnitP("555 mJ") + new UnitP("0.00000001 MJ"));

            //--- Prefix symbols are case sensitive, but string representations are not.
            PrintSampleItem("Pref7", new UnitP(1m, "Km")); //Error.
            PrintSampleItem("Pref8", new UnitP(1m, "mEGam")); //SI prefix mega + metre.

            //--- By default, prefixes can only be used with units which officially/commonly support them.
            PrintSampleItem("Pref9", new UnitP("1 Mft")); //Error because the unit foot doesn't support SI prefixes.
            PrintSampleItem("Pref10", new UnitP("1 Eim")); //Error because the unit metre doesn't support binary prefixes.

            //--- The default behaviour can be modified when instantiating the variable.
            PrintSampleItem("Pref11", new UnitP("1 Mft", PrefixUsageTypes.AllUnits)); //SI prefix mega + foot.
            PrintSampleItem("Pref12", new UnitP("1 Eim", PrefixUsageTypes.AllUnits)); //Binary prefix exbi + metre.

            //--- Same rules apply to officially-named compounds. Non-named compounds recognise prefixes, but don't use them.
            PrintSampleItem("Pref13", new UnitP("1 GN")); //SI prefix giga + newton.
            PrintSampleItem("Pref14", new UnitP(1m, SIPrefixSymbols.Giga + Units.MetrePerSecond)); //1000000*10^3 m/s.

            //--- The unit parts can also have prefixes, which might be compensated with the main prefix.
            PrintSampleItem("Pref15", new UnitP(1m, SIPrefixSymbols.Kilo + UnitSymbols.Newton)); //SI prefix kilo affecting the compound newton.
            PrintSampleItem("Pref16", new UnitP("1 Mg*m/s2")); //Parts of the compound newton (kg*m/s2) + SI prefix kilo (kilo-kg = Mg).
            if (new UnitP(1m, SIPrefixSymbols.Kilo + UnitSymbols.Newton) == new UnitP("1 Mg*m/s2"))
            {
                //This condition is true.
                //Note that both UnitP variables being equal implies identical prefixes.
            }


            //------ Systems of units.

            //--- The system is automatically determined at variable instantiation. Each unit can belong to just one system.
            PrintSampleItem("Sys1", new UnitP("1 m/s2")); //SI acceleration unit (m/s2).
            PrintSampleItem("Sys2", new UnitP("1 cm/s2")); //CGS acceleration unit (Gal).
            PrintSampleItem("Sys3", new UnitP(1m, UnitSymbols.ImperialCable + "/h2")); //Imperial acceleration unit (impcbl/h2). 
            PrintSampleItem("Sys4", new UnitP(1m, UnitSymbols.USCSCable + "/s2")); //USCS acceleration unit (usccbl/s2). 
            PrintSampleItem("Sys5", new UnitP(1m, "AU/min2")); //Acceleration unit not belonging to any system (AU/min2). 


            //------ Automatic unit conversions.

            //--- Automatic conversions (to the system of the first operand) happen in operations between many different-system units.
            PrintSampleItem("Conv1", new UnitP(1m, Units.Metre) * new UnitP("1 ft")); //After converting ft to metre, SI area unit m2.
            PrintSampleItem("Conv2", new UnitP("1 lbf") + new UnitP(5m, "N")); //After converting N to lbf, Imperial/USCS force unit lbf.

            //--- Same rules apply to compounds instantiated via string-parsing.
            PrintSampleItem("Conv3", new UnitP(1m, "m*lb/s2")); //After converting lb to kg, SI force unit N.
            PrintSampleItem("Conv4", new UnitP(1m, "surin3/in")); //After converting in to surin, USCS area unit surin2.


            //------ Numeric support.

            //--- UnitP variables support two different numeric types: decimal and double. 
            PrintSampleItem("Num1", new UnitP(1.23456m, "m")); //The UnitP constructor overloads only support decimal type.
            PrintSampleItem("Num2", new UnitP("1 ft") * 7.891011m); //Decimal variables can be used in multiplications/divisions.
            PrintSampleItem("Num3", new UnitP("1 s") * 1213141516.0); //Double variables can be used in multiplications/divisions.
            
            //--- All the numeric inputs are converted into decimal type. UnitPVariable.BaseTenExponent avoids eventual type-conversion overflow problems.          
            PrintSampleItem
            (
                //UnitP variable with a numerical value notably above decimal.MaxValue.
                "Num4", new UnitP(9999999999999999m, "YAU2", PrefixUsageTypes.AllUnits) 
                / new UnitP("0.000000000000001 yf", PrefixUsageTypes.AllUnits)
            );
            PrintSampleItem
            (
                //UnitP variable with a numerical value notably below decimal.MinValue.
                "Num5", 0.0000000000000000000000000000000000000000000000001 * 
                new UnitP(0.000000000000000000001m, "ym2") / new UnitP("999999999999999999999 Ym") 
            );


            //------ No unit, unitless & unnamed units.

            //--- No unit (Units.None).
            PrintSampleItem("No1", new UnitP(1m, Units.None)); //Units.None cannot be used as an input.
            PrintSampleItem("No2", new UnitP("1 wrong")); //Units.None is the unit associated with all the errors.

            //--- Unitless (Units.Unitless).
            PrintSampleItem("No3", new UnitP(1m, Units.Unitless)); //Units.Unitless can be used as an input.
            PrintSampleItem("No4", new UnitP("5 km") / new UnitP(1m, Units.Unitless)); //Units.Unitless can be used together with other valid units without triggering an error.
            PrintSampleItem("No5", new UnitP("1 ft/m")); //Units.Unitless is associated with the output of operations where all the units cancel each other (with or without automatic conversions).

            //--- Unnamed units (Units.Valid[system]Unit).
            PrintSampleItem("No6", new UnitP("1 cbl/s")); //All the parsed compounds not matching any named unit are automatically included in this category.
            PrintSampleItem("No7", new UnitP(1m, Units.ValidCGSUnit)); //Unnamed units cannot be used as inputs.


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
