using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    public partial class UnitP
    {
        //Relates the SI prefixes (SIPrefixes enum) with their values (constants in the SIPrefixValues class).
        internal static Dictionary<SIPrefixes, decimal> AllSIPrefixes = 
        new Dictionary<SIPrefixes, decimal>()
        {
            { SIPrefixes.Yotta, SIPrefixValues.Yotta }, 
            { SIPrefixes.Zetta, SIPrefixValues.Zetta },
            { SIPrefixes.Exa, SIPrefixValues.Exa }, 
            { SIPrefixes.Peta, SIPrefixValues.Peta },
            { SIPrefixes.Tera, SIPrefixValues.Tera }, 
            { SIPrefixes.Giga, SIPrefixValues.Giga },
            { SIPrefixes.Mega, SIPrefixValues.Mega }, 
            { SIPrefixes.Kilo, SIPrefixValues.Kilo },
            { SIPrefixes.Hecto, SIPrefixValues.Hecto }, 
            { SIPrefixes.Deca, SIPrefixValues.Deca },
            { SIPrefixes.Deci, SIPrefixValues.Deci }, 
            { SIPrefixes.Centi, SIPrefixValues.Centi },
            { SIPrefixes.Milli, SIPrefixValues.Milli }, 
            { SIPrefixes.Micro, SIPrefixValues.Micro },
            { SIPrefixes.Nano, SIPrefixValues.Nano }, 
            { SIPrefixes.Pico, SIPrefixValues.Pico },
            { SIPrefixes.Femto, SIPrefixValues.Femto }, 
            { SIPrefixes.Atto, SIPrefixValues.Atto },
            { SIPrefixes.Zepto, SIPrefixValues.Zepto }, 
            { SIPrefixes.Yocto, SIPrefixValues.Yocto }
        };

        //Relates the SI prefix strings (SIPrefixSymbols enum) with their values (constants in the SIPrefixValues class).
        internal static Dictionary<string, decimal> AllSIPrefixSymbols = 
        new Dictionary<string, decimal>()
        {
            { SIPrefixSymbols.Yotta, SIPrefixValues.Yotta }, 
            { SIPrefixSymbols.Zetta, SIPrefixValues.Zetta },
            { SIPrefixSymbols.Exa, SIPrefixValues.Exa }, 
            { SIPrefixSymbols.Peta, SIPrefixValues.Peta },
            { SIPrefixSymbols.Tera, SIPrefixValues.Tera }, 
            { SIPrefixSymbols.Giga, SIPrefixValues.Giga },
            { SIPrefixSymbols.Mega, SIPrefixValues.Mega }, 
            { SIPrefixSymbols.Kilo, SIPrefixValues.Kilo },
            { SIPrefixSymbols.Hecto, SIPrefixValues.Hecto }, 
            { SIPrefixSymbols.Deca, SIPrefixValues.Deca },
            { SIPrefixSymbols.Deci, SIPrefixValues.Deci }, 
            { SIPrefixSymbols.Centi, SIPrefixValues.Centi },
            { SIPrefixSymbols.Milli, SIPrefixValues.Milli }, 
            { SIPrefixSymbols.Micro, SIPrefixValues.Micro },
            { SIPrefixSymbols.Nano, SIPrefixValues.Nano }, 
            { SIPrefixSymbols.Pico, SIPrefixValues.Pico },
            { SIPrefixSymbols.Femto, SIPrefixValues.Femto }, 
            { SIPrefixSymbols.Atto, SIPrefixValues.Atto },
            { SIPrefixSymbols.Zepto, SIPrefixValues.Zepto }, 
            { SIPrefixSymbols.Yocto, SIPrefixValues.Yocto }
        };

        //Relates the binary prefixes (BinaryPrefixes enum) with their values (constants in the BinaryPrefixValues class).
        internal static Dictionary<BinaryPrefixes, decimal> AllBinaryPrefixes = 
        new Dictionary<BinaryPrefixes, decimal>()
        {
            { BinaryPrefixes.Kibi, BinaryPrefixValues.Kibi }, 
            { BinaryPrefixes.Mebi, BinaryPrefixValues.Mebi },
            { BinaryPrefixes.Gibi, BinaryPrefixValues.Gibi }, 
            { BinaryPrefixes.Tebi, BinaryPrefixValues.Tebi },
            { BinaryPrefixes.Pebi, BinaryPrefixValues.Pebi }, 
            { BinaryPrefixes.Exbi, BinaryPrefixValues.Exbi },
            { BinaryPrefixes.Zebi, BinaryPrefixValues.Zebi }, 
            { BinaryPrefixes.Yobi, BinaryPrefixValues.Yobi }
        };

        //Relates the binary prefix strings (BinaryPrefixSymbols enum) with their values (constants in the BinaryPrefixValues class).
        internal static Dictionary<string, decimal> AllBinaryPrefixSymbols = 
        new Dictionary<string, decimal>()
        {
            { BinaryPrefixSymbols.Kibi, BinaryPrefixValues.Kibi }, 
            { BinaryPrefixSymbols.Mebi, BinaryPrefixValues.Mebi },
            { BinaryPrefixSymbols.Gibi, BinaryPrefixValues.Gibi }, 
            { BinaryPrefixSymbols.Tebi, BinaryPrefixValues.Tebi },
            { BinaryPrefixSymbols.Pebi, BinaryPrefixValues.Pebi }, 
            { BinaryPrefixSymbols.Exbi, BinaryPrefixValues.Exbi },
            { BinaryPrefixSymbols.Zebi, BinaryPrefixValues.Zebi }, 
            { BinaryPrefixSymbols.Yobi, BinaryPrefixValues.Yobi }
        };

        private static Dictionary<string, string> AllSIPrefixNames = 
        AllSIPrefixSymbols.ToDictionary(x => x.Key, x => AllSIPrefixes.First(y => y.Value == x.Value).Key.ToString().ToLower());

        private static IEnumerable<decimal> BigSIPrefixValues = 
        AllSIPrefixes.Where(x => x.Value > 1m).Select(x => x.Value).OrderByDescending(x => x);

        private static IEnumerable<decimal> SmallSIPrefixValues =
        AllSIPrefixes.Where(x => x.Value < 1m).Select(x => x.Value).OrderBy(x => x);

        private static Dictionary<string, string> AllBinaryPrefixNames =
        AllBinaryPrefixSymbols.ToDictionary(x => x.Key, x => AllBinaryPrefixes.First(y => y.Value == x.Value).Key.ToString().ToLower());

        private static IEnumerable<decimal> BigBinaryPrefixValues =
        AllBinaryPrefixes.Where(x => x.Value > 1m).Select(x => x.Value).OrderByDescending(x => x);

        private static IEnumerable<decimal> SmallBinaryPrefixValues =
        AllBinaryPrefixes.Where(x => x.Value < 1m).Select(x => x.Value).OrderBy(x => x);

        //Contains all the units outside the SI-prefix-supporting systems (i.e., UnitSystems.SI & UnitSystems.CGS)
        //which do support SI prefixes by default.
        private static Units[] AllOtherSIPrefixUnits = new Units[]
        {
            //--- Length
            Units.Parsec,

            //--- Mass
            Units.MetricTon, Units.Dalton, Units.UnifiedAtomicMassUnit,
            
            //--- Area
            Units.Are, Units.Barn,
            
            //--- Volume
            Units.Litre,
            
            //--- Information
            Units.Bit, Units.Byte, Units.Nibble, Units.Quartet, Units.Octet,
            
            //--- Energy
            Units.Electronvolt, Units.WattHour, Units.Calorie, Units.ThermochemicalCalorie,
            Units.FoodCalorie,
            
            //--- Pressure
            Units.Bar, Units.Torr,
            
            //--- Logarithmic
            Units.Bel, Units.Neper,
            
            //--- Radioactivity
            Units.Curie,
            
            //--- Bit Rate
            Units.BitPerSecond,

            //--- Symbol Rate
            Units.Baud
        };

        //By default, global prefixes aren't used with compounds to avoid misunderstandings.
        //For example: 1000 m2 converted into k m2 confused as km2.
        //This collection includes the only compounds which might use prefixes.
        private static Units[] AllCompoundsUsingPrefixes = new Units[]
        {
            //--- Acceleration
             Units.Gal,
 
             //--- Force
             Units.Newton, Units.Dyne, 
             
             //--- Energy
             Units.Joule, Units.Erg, Units.WattHour,
             
             //--- Power
             Units.Watt,

             //--- Pressure
             Units.Pascal, Units.Barye,
             
             //--- Frequency
             Units.Hertz,

             //--- Electric Charge
             Units.Coulomb, 

             //--- Electric Current
             Units.Ampere, 

             //--- Electric Voltage
             Units.Volt, 

             //--- Electric Resitance
             Units.Ohm, 

             //--- Electric Conductance
             Units.Siemens,

             //--- Electric Capacitance
             Units.Farad,

             //--- Electric Inductance
             Units.Henry,

             //--- Wavenumber
            Units.Kayser,

            //--- Viscosity
            Units.Poise,

            //--- Kinematic Viscosity
            Units.Stokes,

            //--- Luminous Flux
            Units.Lumen,

            //--- Luminous Energy
            Units.Talbot,

            //--- Luminance
            Units.Stilb,

            //--- Illuminance
            Units.Lux, Units.Phot,

            //--- Magnetic Flux
            Units.Weber,

            //--- Magnetic Field B
            Units.Tesla,

            //--- Absorbed Dose
            Units.Gray, Units.Rad,

            //--- Equivalent Dose
            Units.Sievert, Units.REM,
            
            //--- Catalytic Activity
            Units.Katal,
            
            //--- Bit Rate
            Units.BitPerSecond        
        };

        //Includes all the unit types which support binary prefixes by default.
        private static UnitTypes[] AllBinaryPrefixTypes = new UnitTypes[]
        {
            UnitTypes.Information, UnitTypes.BitRate, UnitTypes.SymbolRate
        };
    }
}
