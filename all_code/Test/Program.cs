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

            UnitP varN1 = new UnitP("1 N"); //Unit symbol. Caps does matter.
            UnitP varN2 = new UnitP(1m, UnitSymbols.Newton);
            UnitP varN3 = new UnitP(1m, "nEwTon"); //Unit secondary string representation. Caps doesn't matter.  
            UnitP varN4 = new UnitP(1m, Units.Newton);

            //--- All the public classes support (un)equality checks.
            if (varN1 == varN2 && varN2 == varN3 && varN2 == varN4)
            {
                //This condition is true.
            }


            //------ UnitP variables can be seen as abstract concepts including many specific types.
            
            //--- Same type variables can be added/subtracted.
            UnitP varN5 = varN1 + varN2; //All of them have the same type: force.

            //--- Different type variables can be multiplied/divided, but only when the generated unit belongs to a supported type.
            UnitP varM1 = new UnitP("1 m");
            UnitP varJ1 = varN1 * varM1; //N*m = J, what is a supported type (energy).

            //--- Any operation outputting unsupported types triggers an error. 
            UnitP varError1 = varN1 * varM1 * varM1;// N*m2 doesn't match any valid type.


            //------ Multiplication/division with decimal/double values are also supported.
            
            varN5 = varN5 * 1.23456;
            varN5 = varN5 / 7.891011m;

            //--- Dividing a number by a UnitP variable does affect the given unit.
            try
            {
                varN5 = 7.891011m / varN5; 
            }
            catch
            {
                //Error because 1/N doesn't represent a supported type.
                //The reasons for the exception are explained below.
            }


            //------ Compounds, unit parts and individual units.

            UnitP varInd1 = new UnitP("1 sec"); //Individual unit (i.e., 1 single unit part whose exponent is 1).
            UnitP varComp1 = new UnitP("1 m/sec"); //Compound (i.e., various parts or one with a different-than-1 exponent).
            UnitP varNamedComp1 = new UnitP("1 N"); //Compound with an official name.

            //--- Compounds can be formed through string parsing or arithmetic operations.
            UnitP varComp1V2 = new UnitP("1 m") / new UnitP("1 s");
            UnitP varNamedComp1V2 = new UnitP("1 kg*m/s2"); 
            if (varComp1 == varComp1V2 && varNamedComp1 == varNamedComp1V2)
            {
                //This condition is true.
            }

            //--- The unit parts are automatically populated when instantiating a valid UnitP variable.
            if (varNamedComp1.UnitParts.FirstOrDefault(x => !varNamedComp1V2.UnitParts.Contains(x)) == null)
            {
                //This condition is true.
            }


            //------ Errors and exceptions.

            //--- All the error information is stored in the readonly variable Error.
            if (varComp1.Error.Type == UnitP.ErrorTypes.None)
            {
                //This condition is true.
            }
            if (varError1.Error.Type != UnitP.ErrorTypes.None)
            {
                //This condition is true.
            }

            //--- By default, errors don't trigger exceptions.
            UnitP varError2 = new UnitP("1 wrong"); //No exception is triggered.

            //--- The default behaviour can be modified when instantiating the variable.
            try
            {
                varError2 = new UnitP("1 wrong", UnitP.ExceptionHandlingTypes.AlwaysTriggerException);
            }
            catch //An exception is triggered.
            { }

            //--- In case of incompatibility, the configuration of the first operand is applied.
            UnitP varNoException1 = new UnitP("1 wrong");
            UnitP varException1 = new UnitP("1 m", UnitP.ExceptionHandlingTypes.AlwaysTriggerException);
            UnitP wrongOperation1 = varNoException1 * varException1; //No exception is triggered.
            try
            {
                UnitP wrongOperation2 = varException1 * varNoException1;
            }
            catch //An exception is triggered.
            { }

            //--- When the first operand is a number, an exception is always triggered.
            try
            {
                UnitP wrongOperation2V2 = 5.0 * varNoException1;
            }
            catch //An exception is triggered.
            { }


            //------ Unit prefixes.

            //--- Two types of prefixes are supported: SI and binary.
            UnitP varPrefix1 = new UnitP(1m, "km"); //SI prefix kilo + metre.
            UnitP varPrefix2 = new UnitP("1 Kibit"); //Binary prefix Kibi + bit.

            //--- All the prefix-related information is stored in the UnitPrefix variable.
            UnitP varPrefix1OtherUnit = new UnitP(1m, "ks");
            if (varPrefix1.UnitPrefix == varPrefix1OtherUnit.UnitPrefix)
            {
                //This condition is true.
            }

            //--- Prefixes are automatically updated/simplified in any operation.
            UnitP varEnergy1 = new UnitP("1 kJ");
            UnitP varEnergy2 = new UnitP("555 mJ");
            UnitP varEnergy3 = new UnitP("0.00000001 MJ");
            UnitP varEnergyResult = varEnergy1 + varEnergy2 + varEnergy3;

            //--- Prefix symbols are case sensitive, but string representations are not.
            UnitP varPrefix3 = new UnitP(1m, "Km"); //Error.
            UnitP varPrefix4 = new UnitP(1m, "mEGam"); //SI prefix mega + metre.

            //--- By default, prefixes can only be used with units which officially/commonly support them.
            UnitP varPrefix5 = new UnitP("1 Mft"); //Error because the unit foot doesn't support SI prefixes.
            UnitP varPrefix6 = new UnitP("1 Eim"); //Error because the unit metre doesn't support binary prefixes.

            //--- The default behaviour can be modified when instantiating the variable.
            UnitP varPrefix7 = new UnitP("1 Mft", PrefixUsageTypes.AllUnits); //SI prefix mega + foot.
            UnitP varPrefix8 = new UnitP("1 Eim", PrefixUsageTypes.AllUnits); //Binary prefix exbi + metre.

            //--- Same rules apply to officially-named compounds. Non-named compounds recognise prefixes, but don't use them.
            UnitP varPrefix9 = new UnitP("1 GN"); //SI prefix giga + newton.
            UnitP varPrefix10 = new UnitP(1m, SIPrefixSymbols.Giga + Units.MetrePerSecond); //1000000*10^3 m/s.

            //--- The unit parts can also have prefixes, which might be compensated with the main prefix.
            UnitP varPrefix11 = new UnitP(1m, SIPrefixSymbols.Kilo + UnitSymbols.Newton); //SI prefix kilo affecting the compound newton.
            UnitP varPrefix12 = new UnitP("1 Mg*m/s2"); //Parts of the compound newton (kg*m/s2) + SI prefix kilo (kilo-kg = Mg).
            if (varPrefix11 == varPrefix12)
            {
                //This condition is true.
                //Note that both UnitP variables being equal implies identical prefixes.
            }

            //------ MORE TO BE WRITTEN SOON!


            //----- Printing all the supported named units.
            PrintAllNamedUnits();
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
