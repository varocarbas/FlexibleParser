using System;
using System.Collections.Generic;

namespace FlexibleParser
{
    public partial class UnitP
    {
        //Relates the primary string representations (constants in the UnitSymbols class) with the corresponding
        //unit (element of the Units enum).
        private static Dictionary<string, Units> AllUnitSymbols = new Dictionary<string, Units>()
        {
            //--- Unitless
            { "unitless", Units.Unitless },

            //--- Length
            { UnitSymbols.Metre, Units.Metre },
            { UnitSymbols.Centimetre, Units.Centimetre },
            { UnitSymbols.AstronomicalUnit, Units.AstronomicalUnit },      
            { UnitSymbols.Inch, Units.Inch },           
            { UnitSymbols.Foot, Units.Foot },             
            { UnitSymbols.Yard, Units.Yard },            
            { UnitSymbols.Mile, Units.Mile },              
            { UnitSymbols.NauticalMile, Units.NauticalMile },                     
            { UnitSymbols.Thou, Units.Thou },                  
            { UnitSymbols.Fathom, Units.Fathom },  
            { UnitSymbols.Rod, Units.Rod },
            { UnitSymbols.Perch, Units.Perch },
            { UnitSymbols.Pole, Units.Pole },
            { UnitSymbols.Chain, Units.Chain },         
            { UnitSymbols.Furlong, Units.Furlong }, 
            { UnitSymbols.League, Units.League }, 
            { UnitSymbols.Cable, Units.Cable }, 
            { UnitSymbols.ImperialCable, Units.ImperialCable }, 
            { UnitSymbols.USCSCable, Units.USCSCable },
            { UnitSymbols.SurveyInch, Units.SurveyInch }, 
            { UnitSymbols.SurveyFoot, Units.SurveyFoot }, 
            { UnitSymbols.SurveyYard, Units.SurveyYard }, 
            { UnitSymbols.SurveyRod, Units.SurveyRod }, 
            { UnitSymbols.SurveyChain, Units.SurveyChain },  
            { UnitSymbols.SurveyMile, Units.SurveyMile }, 
            { UnitSymbols.Link, Units.Link }, 
            { UnitSymbols.Angstrom, Units.Angstrom }, 
            { UnitSymbols.Fermi, Units.Fermi }, 

            //--- Mass
            { UnitSymbols.Gram, Units.Gram }, 
            { UnitSymbols.MetricTon, Units.MetricTon }, 
            { UnitSymbols.Grain, Units.Grain }, 
            { UnitSymbols.Drachm, Units.Drachm },
            { UnitSymbols.Ounce, Units.Ounce },          
            { UnitSymbols.Pound, Units.Pound },  
            { UnitSymbols.Stone, Units.Stone },           
            { UnitSymbols.Slug, Units.Slug },      
            { UnitSymbols.Quarter, Units.Quarter },                
            { UnitSymbols.LongQuarter, Units.LongQuarter },          
            { UnitSymbols.ShortQuarter, Units.ShortQuarter },    
            { UnitSymbols.Hundredweight, Units.Hundredweight },        
            { UnitSymbols.LongHundredweight, Units.LongHundredweight },   
            { UnitSymbols.ShortHundredweight, Units.ShortHundredweight }, 
            { UnitSymbols.Ton, Units.Ton },   
            { UnitSymbols.LongTon, Units.LongTon },            
            { UnitSymbols.ShortTon, Units.ShortTon }, 
            { UnitSymbols.Carat, Units.Carat }, 
            { UnitSymbols.Dalton, Units.Dalton }, 
            { UnitSymbols.UnifiedAtomicMassUnit, Units.UnifiedAtomicMassUnit }, 

            //--- Time
            { UnitSymbols.Second, Units.Second }, 
            { UnitSymbols.Minute, Units.Minute },  
            { UnitSymbols.Hour, Units.Hour },    
            { UnitSymbols.Day, Units.Day },    

            //--- Area
            { UnitSymbols.SquareMetre, Units.SquareMetre }, 
            { UnitSymbols.SquareCentimetre, Units.SquareCentimetre }, 
            { UnitSymbols.Are, Units.Are }, 
            { UnitSymbols.SquareFoot, Units.SquareFoot },
            { UnitSymbols.SquareInch, Units.SquareInch },
            { UnitSymbols.SquareRod, Units.SquareRod }, 
            { UnitSymbols.SquarePerch, Units.SquarePerch }, 
            { UnitSymbols.SquarePole, Units.SquarePole },  
            { UnitSymbols.Rood, Units.Rood }, 
            { UnitSymbols.Acre, Units.Acre }, 
            { UnitSymbols.Barn, Units.Barn },  

            //--- Volume
            { UnitSymbols.CubicMetre, Units.CubicMetre }, 
            { UnitSymbols.CubicCentimetre, Units.CubicCentimetre }, 
            { UnitSymbols.Litre, Units.Litre }, 
            { UnitSymbols.CubicFoot, Units.CubicFoot },
            { UnitSymbols.CubicInch, Units.CubicInch },
            { UnitSymbols.FluidOunce, Units.FluidOunce },   
            { UnitSymbols.ImperialFluidOunce, Units.ImperialFluidOunce },  
            { UnitSymbols.USCSFluidOunce, Units.USCSFluidOunce },
            { UnitSymbols.Gill, Units.Gill },   
            { UnitSymbols.ImperialGill, Units.ImperialGill },          
            { UnitSymbols.USCSGill, Units.USCSGill }, 
            { UnitSymbols.Pint, Units.Pint },  
            { UnitSymbols.ImperialPint, Units.ImperialPint },             
            { UnitSymbols.LiquidPint, Units.LiquidPint },  
            { UnitSymbols.DryPint, Units.DryPint }, 
            { UnitSymbols.Quart, Units.Quart },  
            { UnitSymbols.ImperialQuart, Units.ImperialQuart },           
            { UnitSymbols.LiquidQuart, Units.LiquidQuart },                        
            { UnitSymbols.DryQuart, Units.DryQuart },  
            { UnitSymbols.Gallon, Units.Gallon },          
            { UnitSymbols.ImperialGallon, Units.ImperialGallon },            
            { UnitSymbols.LiquidGallon, Units.LiquidGallon },      
            { UnitSymbols.DryGallon, Units.DryGallon },           

            //--- Angle
            { UnitSymbols.Radian, Units.Radian }, 
            { UnitSymbols.Degree, Units.Degree }, 
            { UnitSymbols.Arcminute, Units.Arcminute },             
            { UnitSymbols.Arcsecond, Units.Arcsecond },                          
            { UnitSymbols.Revolution, Units.Revolution },             
            { UnitSymbols.Gradian, Units.Gradian },    
            { UnitSymbols.Gon, Units.Gon },
            
            //--- Information
            { UnitSymbols.Bit, Units.Bit },     
            { UnitSymbols.Byte, Units.Byte },  
            { UnitSymbols.Nibble, Units.Nibble },  
            { UnitSymbols.Quartet, Units.Quartet },          
            { UnitSymbols.Octet, Units.Octet },           

            //--- Force
            { UnitSymbols.Newton, Units.Newton },  
            { UnitSymbols.Kilopond, Units.Kilopond },   
            { UnitSymbols.PoundForce, Units.PoundForce },            
            { UnitSymbols.Kip, Units.Kip },              
            { UnitSymbols.Poundal, Units.Poundal },                   
            { UnitSymbols.OunceForce, Units.OunceForce },                
            { UnitSymbols.Dyne, Units.Dyne },            

            //--- Velocity
            { UnitSymbols.MetrePerSecond, Units.MetrePerSecond },           
            { UnitSymbols.CentimetrePerSecond, Units.CentimetrePerSecond },  
            { UnitSymbols.FootPerSecond, Units.FootPerSecond }, 
            { UnitSymbols.InchPerSecond, Units.InchPerSecond }, 
            { UnitSymbols.Knot, Units.Knot }, 
            { UnitSymbols.KilometrePerHour, Units.KilometrePerHour }, 
            { UnitSymbols.MilePerHour, Units.MilePerHour }, 

            //--- Acceleration
            { UnitSymbols.MetrePerSquareSecond, Units.MetrePerSquareSecond },
            { UnitSymbols.Gal, Units.Gal }, 
            { UnitSymbols.FootPerSquareSecond, Units.FootPerSquareSecond }, 
            { UnitSymbols.InchPerSquareSecond, Units.InchPerSquareSecond }, 

            //--- Energy
            { UnitSymbols.Joule, Units.Joule },   
            { UnitSymbols.Electronvolt, Units.Electronvolt },            
            { UnitSymbols.WattHour, Units.WattHour },       
            { UnitSymbols.BritishThermalUnit, Units.BritishThermalUnit },             
            { UnitSymbols.ThermochemicalBritishThermalUnit, Units.ThermochemicalBritishThermalUnit },               
            { UnitSymbols.Calorie, Units.Calorie },            
            { UnitSymbols.ThermochemicalCalorie, Units.ThermochemicalCalorie },                
            { UnitSymbols.FoodCalorie, Units.FoodCalorie },               
            { UnitSymbols.Erg, Units.Erg },                
            { UnitSymbols.Therm, Units.Therm },                  
            { UnitSymbols.UKTherm, Units.UKTherm },               
            { UnitSymbols.USTherm, Units.USTherm },              

            //--- Power
            { UnitSymbols.Watt, Units.Watt },          
            { UnitSymbols.ErgPerSecond, Units.ErgPerSecond },   
            { UnitSymbols.Horsepower, Units.Horsepower },               
            { UnitSymbols.MetricHorsepower, Units.MetricHorsepower },       
            { UnitSymbols.BoilerHorsepower, Units.BoilerHorsepower },     
            { UnitSymbols.ElectricHorsepower, Units.ElectricHorsepower },      
            { UnitSymbols.TonOfRefrigeration, Units.TonOfRefrigeration },             

            //--- Pressure
            { UnitSymbols.Pascal, Units.Pascal },    
            { UnitSymbols.Atmosphere, Units.Atmosphere },             
            { UnitSymbols.Bar, Units.Bar },             
            { UnitSymbols.PoundforcePerSquareInch, Units.PoundforcePerSquareInch },                   
            { UnitSymbols.MillimetreMercury, Units.MillimetreMercury },             
            { UnitSymbols.InchMercury32F, Units.InchMercury32F },              
            { UnitSymbols.InchMercury60F, Units.InchMercury60F },               
            { UnitSymbols.Barye, Units.Barye },              
            { UnitSymbols.Torr, Units.Torr },               
            { UnitSymbols.KipPerSquareInch, Units.KipPerSquareInch },                

            //--- Frequency
            { UnitSymbols.Hertz, Units.Hertz },      
            { UnitSymbols.RevolutionsPerMinute, Units.RevolutionsPerMinute },               
            { UnitSymbols.CyclesPerSecond, Units.CyclesPerSecond },              

            //--- Electric Charge
            { UnitSymbols.Coulomb, Units.Coulomb }, 
            { UnitSymbols.Franklin, Units.Franklin }, 
            { UnitSymbols.Statcoulomb, Units.Statcoulomb }, 
            { UnitSymbols.ESUOfCharge, Units.ESUOfCharge }, 
            { UnitSymbols.Abcoulomb, Units.Abcoulomb }, 
            { UnitSymbols.EMUOfCharge, Units.EMUOfCharge }, 

            //--- Electric Current
            { UnitSymbols.Ampere, Units.Ampere },  
            { UnitSymbols.Statampere, Units.Statampere }, 
            { UnitSymbols.ESUOfCurrent, Units.ESUOfCurrent }, 
            { UnitSymbols.Abampere, Units.Abampere }, 
            { UnitSymbols.EMUOfCurrent, Units.EMUOfCurrent }, 
            { UnitSymbols.Biot, Units.Biot }, 

            //--- Electric Voltage
            { UnitSymbols.Volt, Units.Volt },  
            { UnitSymbols.Statvolt, Units.Statvolt }, 
            { UnitSymbols.ESUOfElectricPotential, Units.ESUOfElectricPotential }, 
            { UnitSymbols.Abvolt, Units.Abvolt }, 
            { UnitSymbols.EMUOfElectricPotential, Units.EMUOfElectricPotential }, 

            //--- Electric Resistance 
            { UnitSymbols.Ohm, Units.Ohm },  
            { UnitSymbols.Statohm, Units.Statohm }, 
            { UnitSymbols.ESUOfResistance, Units.ESUOfResistance }, 
            { UnitSymbols.Abohm, Units.Abohm }, 
            { UnitSymbols.EMUOfResistance, Units.EMUOfResistance }, 
            
            //--- Electric Resistivity 
            { UnitSymbols.OhmMetre, Units.OhmMetre },  

            //--- Electric Conductance
            { UnitSymbols.Siemens, Units.Siemens },  
            { UnitSymbols.Mho, Units.Mho }, 
            { UnitSymbols.Gemmho, Units.Gemmho }, 
            { UnitSymbols.Statsiemens, Units.Statsiemens }, 
            { UnitSymbols.Statmho, Units.Statmho }, 
            { UnitSymbols.Absiemens, Units.Absiemens }, 
            { UnitSymbols.Abmho, Units.Abmho }, 

            //--- Electric Conductivity
            { UnitSymbols.SiemensPerMetre, Units.SiemensPerMetre },  

            //--- Electric Capacitance
            { UnitSymbols.Farad, Units.Farad },  
            { UnitSymbols.Statfarad, Units.Statfarad }, 
            { UnitSymbols.ESUOfCapacitance, Units.ESUOfCapacitance }, 
            { UnitSymbols.Abfarad, Units.Abfarad }, 
            { UnitSymbols.EMUOfCapacitance, Units.EMUOfCapacitance }, 

            //--- Electric Inductance
            { UnitSymbols.Henry, Units.Henry },  
            { UnitSymbols.Stathenry, Units.Stathenry }, 
            { UnitSymbols.ESUOfInductance, Units.ESUOfInductance }, 
            { UnitSymbols.Abhenry, Units.Abhenry }, 
            { UnitSymbols.EMUOfInductance, Units.EMUOfInductance }, 

            //--- Electric Dipole Moment
            { UnitSymbols.CoulombMetre, Units.CoulombMetre },  
            { UnitSymbols.Debye, Units.Debye },  

            //--- Temperature
            { UnitSymbols.Kelvin, Units.Kelvin },  
            { UnitSymbols.Celsius, Units.Celsius }, 
            { UnitSymbols.Fahrenheit, Units.Fahrenheit },            
            { UnitSymbols.Rankine, Units.Rankine },    

            //--- Wavenumber
            { UnitSymbols.ReciprocalMetre, Units.ReciprocalMetre }, 
            { UnitSymbols.Kayser, Units.Kayser },  

            //--- Viscosity
            { UnitSymbols.PascalSecond, Units.PascalSecond },  
            { UnitSymbols.Poise, Units.Poise },  

            //--- Kinematic Viscosity
            { UnitSymbols.SquareMetrePerSecond, Units.SquareMetrePerSecond },  
            { UnitSymbols.Stokes, Units.Stokes },  

            //--- Amount of Substance
            { UnitSymbols.Mole, Units.Mole },                 
            { UnitSymbols.PoundMole, Units.PoundMole }, 
            
            //--- Momentum
            { UnitSymbols.NewtonSecond, Units.NewtonSecond },   
            
            //--- Angular Velocity
            { UnitSymbols.RadianPerSecond, Units.RadianPerSecond },
            
            //--- Angular Acceleration
            { UnitSymbols.RadianPerSquareSecond, Units.RadianPerSquareSecond },
            
            //--- Angular Momentum
            { UnitSymbols.JouleSecond, Units.JouleSecond },
            
            //--- Moment of Inertia
            { UnitSymbols.KilogramSquareMetre, Units.KilogramSquareMetre },

            //--- Solid Angle
            { UnitSymbols.Steradian, Units.Steradian },                
            { UnitSymbols.SquareDegree, Units.SquareDegree },                 

            //--- Luminous Intensity
            { UnitSymbols.Candela, Units.Candela },                

            //--- Luminous Flux
            { UnitSymbols.Lumen, Units.Lumen },                  

            //--- Luminous Energy
            { UnitSymbols.LumenSecond, Units.LumenSecond },   
            { UnitSymbols.Talbot, Units.Talbot }, 

            //--- Luminance
            { UnitSymbols.CandelaPerSquareMetre, Units.CandelaPerSquareMetre },                
            { UnitSymbols.Nit, Units.Nit },
            { UnitSymbols.Stilb, Units.Stilb },

            //--- Illuminance
            { UnitSymbols.Lux, Units.Lux },               
            { UnitSymbols.Phot, Units.Phot },   

            //--- Logarithmic
            { UnitSymbols.Bel, Units.Bel },                       
            { UnitSymbols.Neper, Units.Neper }, 

            //--- Magnetic Flux
            { UnitSymbols.Weber, Units.Weber },                        
            { UnitSymbols.Maxwell, Units.Maxwell },                          

            //--- Magnetic Field B
            { UnitSymbols.Tesla, Units.Tesla },
            { UnitSymbols.Gauss, Units.Gauss },                      

            //--- Magnetic Field H
            { UnitSymbols.AmperePerMetre, Units.AmperePerMetre }, 
            { UnitSymbols.Oersted, Units.Oersted },                       

            //--- Radioactivity
            { UnitSymbols.Becquerel, Units.Becquerel }, 
            { UnitSymbols.Curie, Units.Curie },
            { UnitSymbols.DisintegrationsPerSecond, Units.DisintegrationsPerSecond },
            { UnitSymbols.DisintegrationsPerMinute, Units.DisintegrationsPerMinute },
            { UnitSymbols.Rutherford, Units.Rutherford },

            //--- Absorbed Dose
            { UnitSymbols.Gray, Units.Gray },
            { UnitSymbols.Rad, Units.Rad },

            //--- Equivalent Dose
            { UnitSymbols.Sievert, Units.Sievert },
            { UnitSymbols.REM, Units.REM },

            //--- Catalytic Activity
            { UnitSymbols.Katal, Units.Katal }, 

            //--- Catalytic Activity Concentration
            { UnitSymbols.KatalPerCubicMetre, Units.KatalPerCubicMetre }, 

            //--- Jerk
            { UnitSymbols.MetrePerCubicSecond, Units.MetrePerCubicSecond },

            //--- Mass Flow Rate
            { UnitSymbols.KilogramPerSecond, Units.KilogramPerSecond },

            //--- Density
            { UnitSymbols.KilogramPerCubicMetre, Units.KilogramPerCubicMetre },

            //--- Area Density
            { UnitSymbols.KilogramPerSquareMetre, Units.KilogramPerSquareMetre },

            //--- Energy Density
            { UnitSymbols.JoulePerCubicMetre, Units.JoulePerCubicMetre },

            //--- Specific Volume
            { UnitSymbols.CubicMetrePerKilogram, Units.CubicMetrePerKilogram },

            //--- Volumetric Flow Rate
            { UnitSymbols.CubicMetrePerSecond, Units.CubicMetrePerSecond },

            //--- Surface Tension
            { UnitSymbols.JoulePerSquareMetre, Units.JoulePerSquareMetre },

            //--- Specific Weight
            { UnitSymbols.NewtonPerCubicMetre, Units.NewtonPerCubicMetre },

            //--- Thermal Conductivity
            { UnitSymbols.WattPerMetrePerKelvin, Units.WattPerMetrePerKelvin },

            //--- Thermal Conductance
            { UnitSymbols.WattPerKelvin, Units.WattPerKelvin },

            //--- Thermal Resistivity
            { UnitSymbols.MetreKelvinPerWatt, Units.MetreKelvinPerWatt },

            //--- Thermal Resistance
            { UnitSymbols.KelvinPerWatt, Units.KelvinPerWatt },

            //--- Heat Transfer Coefficient
            { UnitSymbols.WattPerSquareMetrePerKelvin, Units.WattPerSquareMetrePerKelvin },

            //--- Heat Flux Density
            { UnitSymbols.WattPerSquareMetre, Units.WattPerSquareMetre },

            //--- Entropy
            { UnitSymbols.JoulePerKelvin, Units.JoulePerKelvin },

            //--- Electric Field Strength
            { UnitSymbols.NewtonPerCoulomb, Units.NewtonPerCoulomb },

            //--- Linear Electric Charge Density
            { UnitSymbols.CoulombPerMetre, Units.CoulombPerMetre },

            //--- Surface Electric Charge Density
            { UnitSymbols.CoulombPerSquareMetre, Units.CoulombPerSquareMetre },

            //--- Volume Electric Charge Density
            { UnitSymbols.CoulombPerCubicMetre, Units.CoulombPerCubicMetre },

            //--- Current Density
            { UnitSymbols.AmperePerSquareMetre, Units.AmperePerSquareMetre },

            //--- Permittivity
            { UnitSymbols.FaradPerMetre, Units.FaradPerMetre },

            //--- Permeability
            { UnitSymbols.HenryPerMetre, Units.HenryPerMetre },

            //--- Molar Entropy
            { UnitSymbols.JoulePerMolePerKelvin, Units.JoulePerMolePerKelvin },

            //--- Molar Concentration
            { UnitSymbols.MolePerCubicMetre, Units.MolePerCubicMetre },

            //--- Radiant Intensity
            { UnitSymbols.WattPerSteradian, Units.WattPerSteradian },

            //--- Radiance
            { UnitSymbols.WattPerSteradianPerSquareMetre, Units.WattPerSteradianPerSquareMetre },

            //--- Fuel Economy
            { UnitSymbols.InverseSquareMetre, Units.InverseSquareMetre },
            { UnitSymbols.MilePerGallon, Units.MilePerGallon },
            { UnitSymbols.ImperialMilePerGallon, Units.ImperialMilePerGallon },
            { UnitSymbols.USCSMilePerGallon, Units.USCSMilePerGallon },

            //--- Sound Exposure
            { UnitSymbols.SquarePascalSecond, Units.SquarePascalSecond },

            //--- Sound Impedance
            { UnitSymbols.PascalSecondPerCubicMetre, Units.PascalSecondPerCubicMetre },

            //--- Rotational Stiffness
            { UnitSymbols.NewtonMetrePerRadian, Units.NewtonMetrePerRadian }
        };

        //Contains all the units outside the SI-prefix-supporting systems (i.e., UnitSystems.SI & UnitSystems.CGS)
        //which do support SI prefixes by default.
        private static Units[] AllOtherSIPrefixUnits = new Units[]
        {
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
        };

        //English-system units which are identical in both Imperial and USCS.
        private static Units[] AllImperialAndUSCSUnits = new Units[]
        {
            //--- Length
            Units.Foot, Units.Yard, Units.Mile, Units.Thou, Units.Fathom,
            Units.Rod, Units.Perch, Units.Pole, Units.Chain, Units.Furlong,
            Units.League, Units.Link, 

            //--- Mass
            Units.Grain, Units.Drachm, Units.Ounce, Units.Pound, Units.Stone,
            Units.Slug, 

            //--- Area
            Units.SquareInch, Units.SquareFoot, Units.SquareRod, Units.SquarePerch,
            Units.SquarePole, Units.Rood, Units.Acre,

            //--- Volume
            Units.CubicInch, Units.CubicFoot,
            
            //--- Force
            Units.PoundForce, Units.Poundal, Units.OunceForce,
                        
            //--- Velocity
            Units.FootPerSecond, Units.InchPerSecond, 
                        
            //--- Acceleration
            Units.FootPerSquareSecond, Units.InchPerSquareSecond, 
                        
            //--- Energy
            Units.BritishThermalUnit, Units.ThermochemicalBritishThermalUnit, 
                        
            //--- Power
            Units.Horsepower, 
                        
            //--- Pressure
            Units.PoundforcePerSquareInch, 
                        
            //--- Temperature
            Units.Fahrenheit, Units.Rankine, 
        };
    }
}
