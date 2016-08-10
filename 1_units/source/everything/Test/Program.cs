using System;
using System.Collections.Generic;
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
            varN5 = 7.891011m / varN5; //Error because 1/N doesn't represent a supported type.


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
