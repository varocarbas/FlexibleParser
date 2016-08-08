using System;
using System.Collections.Generic;
using FlexibleParser;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintAllNamedUnits();

            //More relevant sample codes soon!

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
