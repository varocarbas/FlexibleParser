using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    public partial class UnitP
    {
        //Main classiffication for all the units (type, system and conversion factor).
        //This dictionary represents an easily-modifiable container of well-structured unit information.
        //After creating more specific/efficient collections from this informations, GetAllMain() deletes it.
        private static Dictionary<UnitTypes, Dictionary<UnitSystems, Dictionary<Units, decimal>>> AllUnits = 
        new Dictionary<UnitTypes, Dictionary<UnitSystems, Dictionary<Units, decimal>>>()
        {
             { 
                UnitTypes.None, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    { UnitSystems.SI, new Dictionary<Units, decimal>() { { Units.ValidSIUnit, 1m } } },
                    { UnitSystems.Imperial, new Dictionary<Units, decimal>() 
                      { 
                        { Units.ValidImperialUSCSUnit, 1m }, { Units.ValidImperialUnit, 1m } 
                      } 
                    },
                    { UnitSystems.USCS, new Dictionary<Units, decimal>() { { Units.ValidUSCSUnit, 1m } } }, 
                    { UnitSystems.CGS, new Dictionary<Units, decimal>() { { Units.ValidCGSUnit, 1m } } },
                    { UnitSystems.None, new Dictionary<Units, decimal>() { { Units.ValidUnit, 1m }, { Units.Unitless, 1m} } },
                }
            },
            { 
                UnitTypes.Length, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    { 
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Metre, UnitConversionFactors.Metre }
                        }
                    },
                    { 
                        UnitSystems.CGS, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Centimetre, UnitConversionFactors.Centimetre }
                        }
                    },
                    { 
                        UnitSystems.Imperial, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Foot, UnitConversionFactors.Foot }, 
                            { Units.Thou, UnitConversionFactors.Thou }, 
                            { Units.Inch, UnitConversionFactors.Inch },  
                            { Units.Yard, UnitConversionFactors.Yard }, 
                            { Units.Fathom, UnitConversionFactors.Fathom }, 
                            { Units.Rod, UnitConversionFactors.Rod }, 
                            { Units.Perch, UnitConversionFactors.Perch }, 
                            { Units.Pole, UnitConversionFactors.Pole }, 
                            { Units.Chain, UnitConversionFactors.Chain }, 
                            { Units.Furlong, UnitConversionFactors.Furlong }, 
                            { Units.Mile, UnitConversionFactors.Mile },
                            { Units.League, UnitConversionFactors.League },
                            { Units.Link, UnitConversionFactors.Link },
                            { Units.ImperialCable, UnitConversionFactors.ImperialCable },
                            { Units.Cable, UnitConversionFactors.Cable }
                        }
                    },
                    { 
                        UnitSystems.USCS, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.USCSCable, UnitConversionFactors.USCSCable },
                            { Units.SurveyInch, UnitConversionFactors.SurveyInch }, 
                            { Units.SurveyFoot, UnitConversionFactors.SurveyFoot }, 
                            { Units.SurveyYard, UnitConversionFactors.SurveyYard }, 
                            { Units.SurveyRod, UnitConversionFactors.SurveyRod }, 
                            { Units.SurveyChain, UnitConversionFactors.SurveyChain }, 
                            { Units.SurveyLink, UnitConversionFactors.SurveyLink }, 
                            { Units.SurveyMile, UnitConversionFactors.SurveyMile },
                            { Units.SurveyFathom, UnitConversionFactors.SurveyFathom } 
                        }
                    },
                    { 
                        UnitSystems.None, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.AstronomicalUnit, UnitConversionFactors.AstronomicalUnit },
                            { Units.NauticalMile, UnitConversionFactors.NauticalMile },
                            { Units.Angstrom, UnitConversionFactors.Angstrom },
                            { Units.Fermi, UnitConversionFactors.Fermi },
                            { Units.LightYear, UnitConversionFactors.LightYear },
                            { Units.Parsec, UnitConversionFactors.Parsec },
                            { Units.Micron, UnitConversionFactors.Micron }
                        }
                    }
                }
            },
            { 
                UnitTypes.Mass, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    { 
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Gram, UnitConversionFactors.Gram }
                        }
                    },
                    { 
                        UnitSystems.Imperial, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Pound, UnitConversionFactors.Pound }, 
                            { Units.Grain, UnitConversionFactors.Grain }, 
                            { Units.Drachm, UnitConversionFactors.Drachm }, 
                            { Units.Ounce, UnitConversionFactors.Ounce }, 
                            { Units.Stone, UnitConversionFactors.Stone }, 
                            { Units.Slug, UnitConversionFactors.Slug },
                            { Units.Quarter, UnitConversionFactors.Quarter },
                            { Units.LongQuarter, UnitConversionFactors.LongQuarter },
                            { Units.Hundredweight, UnitConversionFactors.Hundredweight },
                            { Units.LongHundredweight, UnitConversionFactors.LongHundredweight },
                            { Units.Ton, UnitConversionFactors.Ton },
                            { Units.LongTon, UnitConversionFactors.LongTon }
                        }
                    },
                    { 
                        UnitSystems.USCS, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.ShortQuarter, UnitConversionFactors.ShortQuarter },
                            { Units.ShortHundredweight, UnitConversionFactors.ShortHundredweight }, 
                            { Units.ShortTon, UnitConversionFactors.ShortTon }
                        }
                    },
                    {
                        UnitSystems.None,
                        new Dictionary<Units, decimal>()
                        {
                            { Units.MetricTon, UnitConversionFactors.MetricTon },
                            { Units.Dalton, UnitConversionFactors.Dalton },
                            { Units.UnifiedAtomicMassUnit, UnitConversionFactors.UnifiedAtomicMassUnit },
                            { Units.Carat, UnitConversionFactors.Carat }
                        }
                    }
                }
            },
            { 
                UnitTypes.Time, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    { 
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Second, UnitConversionFactors.Second }
                        }
                    },
                    { 
                        UnitSystems.None, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Minute, UnitConversionFactors.Minute }, 
                            { Units.Hour, UnitConversionFactors.Hour },
                            { Units.Day, UnitConversionFactors.Day },
                            { Units.Shake, UnitConversionFactors.Shake }
                        }
                    }
                }
            },
            { 
                UnitTypes.Area, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                   {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.SquareMetre, UnitConversionFactors.SquareMetre }
                        }
                    },
                    {
                        UnitSystems.CGS, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.SquareCentimetre, UnitConversionFactors.SquareCentimetre }
                        }
                    },
                    {
                        UnitSystems.Imperial, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.SquareFoot, UnitConversionFactors.SquareFoot },
                            { Units.SquareInch, UnitConversionFactors.SquareInch },
                            { Units.SquareRod, UnitConversionFactors.SquareRod },
                            { Units.SquarePerch, UnitConversionFactors.SquarePerch },
                            { Units.SquarePole, UnitConversionFactors.SquarePole }, 
                            { Units.Rood, UnitConversionFactors.Rood }, 
                            { Units.Acre, UnitConversionFactors.Acre }
                        }
                    },
                    {
                        UnitSystems.USCS, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.SurveyAcre, UnitConversionFactors.SurveyAcre }
                        }
                    },
                   {
                        UnitSystems.None, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Are, UnitConversionFactors.Are },
                            { Units.Barn, UnitConversionFactors.Barn }
                        }
                    }
                }
            },
            { 
                UnitTypes.Volume, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.CubicMetre, UnitConversionFactors.CubicMetre }
                        }
                    },
                    {
                        UnitSystems.CGS, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.CubicCentimetre, UnitConversionFactors.CubicCentimetre }
                        }
                    },
                    {
                        UnitSystems.Imperial, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.CubicFoot, UnitConversionFactors.CubicFoot },
                            { Units.CubicInch, UnitConversionFactors.CubicInch },
                            { Units.FluidOunce, UnitConversionFactors.FluidOunce },
                            { Units.ImperialFluidOunce, UnitConversionFactors.ImperialFluidOunce },
                            { Units.Gill, UnitConversionFactors.Gill },
                            { Units.ImperialGill, UnitConversionFactors.ImperialGill },
                            { Units.Pint, UnitConversionFactors.Pint },
                            { Units.ImperialPint, UnitConversionFactors.ImperialPint },
                            { Units.Quart, UnitConversionFactors.Quart },
                            { Units.ImperialQuart, UnitConversionFactors.ImperialQuart },
                            { Units.Gallon, UnitConversionFactors.Gallon },
                            { Units.ImperialGallon, UnitConversionFactors.ImperialGallon }
                        }
                    },
                    {
                        UnitSystems.USCS, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.USCSFluidOunce, UnitConversionFactors.USCSFluidOunce },
                            { Units.USCSGill, UnitConversionFactors.USCSGill },
                            { Units.LiquidPint, UnitConversionFactors.LiquidPint },
                            { Units.DryPint, UnitConversionFactors.DryPint },
                            { Units.LiquidQuart, UnitConversionFactors.LiquidQuart },
                            { Units.DryQuart, UnitConversionFactors.DryQuart },
                            { Units.LiquidGallon, UnitConversionFactors.LiquidGallon },
                            { Units.DryGallon, UnitConversionFactors.DryGallon }
                        }
                    },
                    {
                        UnitSystems.None, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Litre, UnitConversionFactors.Litre }
                        }
                    },
                }
            },
            { 
                UnitTypes.Angle, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Radian, UnitConversionFactors.Radian }
                        }
                    },
                    {
                        UnitSystems.None, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Degree, UnitConversionFactors.Degree }, 
                            { Units.Arcminute, UnitConversionFactors.Arcminute }, 
                            { Units.Arcsecond, UnitConversionFactors.Arcsecond },
                            { Units.Revolution, UnitConversionFactors.Revolution },
                            { Units.Gradian, UnitConversionFactors.Gradian },
                            { Units.Gon, UnitConversionFactors.Gon }
                        }
                    }
                }
            },
            { 
                UnitTypes.Information, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.None, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Bit, UnitConversionFactors.Bit }, 
                            { Units.Byte, UnitConversionFactors.Byte },
                            { Units.Nibble, UnitConversionFactors.Nibble }, 
                            { Units.Quartet, UnitConversionFactors.Quartet },
                            { Units.Octet, UnitConversionFactors.Octet }
                        }
                    }
                }
            },
            { 
                UnitTypes.Force, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Newton, UnitConversionFactors.Newton }
                        }
                    },
                    {
                        UnitSystems.Imperial, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.PoundForce, UnitConversionFactors.PoundForce },
                            { Units.Poundal, UnitConversionFactors.Poundal },
                            { Units.OunceForce, UnitConversionFactors.OunceForce },
                        }
                    },
                    {
                        UnitSystems.CGS, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Dyne, UnitConversionFactors.Dyne }
                        }
                    },
                    {
                        UnitSystems.None, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Kilopond, UnitConversionFactors.Kilopond },
                            { Units.Kip, UnitConversionFactors.Kip }
                        }
                    }
                }
            },
            { 
                UnitTypes.Velocity, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.MetrePerSecond, UnitConversionFactors.MetrePerSecond }
                        }
                    },
                    {
                        UnitSystems.CGS, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.CentimetrePerSecond, UnitConversionFactors.CentimetrePerSecond }
                        }
                    },
                    {
                        UnitSystems.Imperial, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.FootPerSecond, UnitConversionFactors.FootPerSecond },
                            { Units.InchPerSecond, UnitConversionFactors.InchPerSecond }
                        }
                    },
                    {
                        UnitSystems.None, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Knot, UnitConversionFactors.Knot },
                            { Units.KilometrePerHour, UnitConversionFactors.KilometrePerHour },
                            { Units.MilePerHour, UnitConversionFactors.MilePerHour }
                        }
                    }
                }
            },
            { 
                UnitTypes.Acceleration, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.MetrePerSquareSecond, UnitConversionFactors.MetrePerSquareSecond }
                        }
                    },
                    {
                        UnitSystems.CGS, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Gal, UnitConversionFactors.Gal }
                        }
                    },
                    {
                        UnitSystems.Imperial, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.FootPerSquareSecond, UnitConversionFactors.FootPerSquareSecond },
                            { Units.InchPerSquareSecond, UnitConversionFactors.InchPerSquareSecond }
                        }
                    },
                }
            },
            { 
                UnitTypes.Energy, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Joule, UnitConversionFactors.Joule }
                        }
                    },
                    {
                        UnitSystems.Imperial, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.BritishThermalUnit, UnitConversionFactors.BritishThermalUnit },
                            { Units.ThermochemicalBritishThermalUnit, UnitConversionFactors.ThermochemicalBritishThermalUnit }
                        }
                    },
                    {
                        UnitSystems.CGS, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Erg, UnitConversionFactors.Erg }
                        }
                    },
                    {
                        UnitSystems.None, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Electronvolt, UnitConversionFactors.Electronvolt },
                            { Units.WattHour, UnitConversionFactors.WattHour }, 
                            { Units.Calorie, UnitConversionFactors.Calorie },
                            { Units.ThermochemicalCalorie, UnitConversionFactors.ThermochemicalBritishThermalUnit },
                            { Units.FoodCalorie, UnitConversionFactors.FoodCalorie },
                            { Units.Therm, UnitConversionFactors.Therm },
                            { Units.UKTherm, UnitConversionFactors.UKTherm },
                            { Units.USTherm, UnitConversionFactors.USTherm }                       
                        }
                    }
                }
            },
            { 
                UnitTypes.Power, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Watt, UnitConversionFactors.Watt }
                        }
                    },
                    {
                        UnitSystems.CGS, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.ErgPerSecond, UnitConversionFactors.ErgPerSecond }
                        }
                    },
                    {
                        UnitSystems.Imperial, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Horsepower, UnitConversionFactors.Horsepower }
                        }
                    },
                    {
                        UnitSystems.None, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.MetricHorsepower, UnitConversionFactors.MetricHorsepower },
                            { Units.ElectricHorsepower, UnitConversionFactors.ElectricHorsepower },
                            { Units.BoilerHorsepower, UnitConversionFactors.BoilerHorsepower },
                            { Units.TonOfRefrigeration, UnitConversionFactors.TonOfRefrigeration }
                        }
                    }
                }
            },
            { 
                UnitTypes.Pressure, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Pascal, UnitConversionFactors.Pascal }
                        }
                    },
                    {
                        UnitSystems.CGS, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Barye, UnitConversionFactors.Barye }
                        }
                    },
                    {
                        UnitSystems.Imperial, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.PoundforcePerSquareInch, UnitConversionFactors.PoundforcePerSquareInch },
                            { Units.PoundforcePerSquareFoot, UnitConversionFactors.PoundforcePerSquareFoot }
                        }
                    },
                    {
                        UnitSystems.None, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Bar, UnitConversionFactors.Bar }, 
                            { Units.Atmosphere, UnitConversionFactors.Atmosphere },
                            { Units.TechnicalAtmosphere, UnitConversionFactors.TechnicalAtmosphere },
                            { Units.MillimetreOfMercury, UnitConversionFactors.MillimetreOfMercury },
                            { Units.InchOfMercury32F, UnitConversionFactors.InchOfMercury32F },
                            { Units.InchOfMercury60F, UnitConversionFactors.InchOfMercury60F },
                            { Units.Torr, UnitConversionFactors.Torr },
                            { Units.KipPerSquareInch, UnitConversionFactors.KipPerSquareInch }
                        }
                    }
                }
            },
            { 
                UnitTypes.Frequency, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Hertz, UnitConversionFactors.Hertz }
                        }
                    },
                    {
                        UnitSystems.None, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.CyclesPerSecond, UnitConversionFactors.CyclesPerSecond }, 
                            { Units.RevolutionsPerMinute, UnitConversionFactors.RevolutionsPerMinute }
                         }
                    }
                }
            },
            { 
                UnitTypes.ElectricCharge, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Coulomb, UnitConversionFactors.Coulomb },
                            { Units.AmpereHour, UnitConversionFactors.AmpereHour }
                        }
                    },
                    {
                        UnitSystems.CGS, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Franklin, UnitConversionFactors.Franklin },
                            { Units.Statcoulomb, UnitConversionFactors.Statcoulomb },
                            { Units.ESUOfCharge, UnitConversionFactors.ESUOfCharge },
                            { Units.Abcoulomb, UnitConversionFactors.Abcoulomb },
                            { Units.EMUOfCharge, UnitConversionFactors.EMUOfCharge }
                        }
                    }
                }
            },
            { 
                UnitTypes.ElectricCurrent, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Ampere, UnitConversionFactors.Ampere }
                        }
                    },
                    {
                        UnitSystems.CGS, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Statampere, UnitConversionFactors.Statampere },
                            { Units.ESUOfCurrent, UnitConversionFactors.ESUOfCurrent },
                            { Units.Abampere, UnitConversionFactors.Abampere },
                            { Units.Biot, UnitConversionFactors.Biot },
                            { Units.EMUOfCurrent, UnitConversionFactors.EMUOfCurrent }
                        }
                    }
                }
            },
            { 
                UnitTypes.ElectricVoltage, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Volt, UnitConversionFactors.Volt }
                        }
                    },
                    {
                        UnitSystems.CGS, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Statvolt, UnitConversionFactors.Statvolt },
                            { Units.ESUOfElectricPotential, UnitConversionFactors.ESUOfElectricPotential },
                            { Units.Abvolt, UnitConversionFactors.Abvolt },
                            { Units.EMUOfElectricPotential, UnitConversionFactors.EMUOfElectricPotential }

                        }
                    }
                }
            },
            { 
                UnitTypes.ElectricResistance, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Ohm, UnitConversionFactors.Ohm }
                        }
                    },
                    {
                        UnitSystems.CGS,
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Statohm, UnitConversionFactors.Statohm },
                            { Units.ESUOfResistance, UnitConversionFactors.ESUOfResistance },
                            { Units.Abohm, UnitConversionFactors.Abohm },
                            { Units.EMUOfResistance, UnitConversionFactors.EMUOfResistance }
                        }
                    }
                }
            },
            { 
                UnitTypes.ElectricResistivity, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.OhmMetre, UnitConversionFactors.OhmMetre }
                        }
                    },
                }
            },
            { 
                UnitTypes.ElectricConductance, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Siemens, UnitConversionFactors.Siemens },
                            { Units.Mho, UnitConversionFactors.Mho },
                        }
                    },
                    {
                        UnitSystems.CGS,
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Statmho, UnitConversionFactors.Statmho },
                            { Units.Statsiemens, UnitConversionFactors.Statsiemens },
                            { Units.Abmho, UnitConversionFactors.Abmho },
                            { Units.Absiemens, UnitConversionFactors.Absiemens }
                        }
                    },
                    {
                        UnitSystems.None, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Gemmho, UnitConversionFactors.Gemmho }
                        }
                    }
                }
            },
            { 
                UnitTypes.ElectricConductivity, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.SiemensPerMetre, UnitConversionFactors.SiemensPerMetre }
                        }
                    },
                }
            },
            { 
                UnitTypes.ElectricCapacitance, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Farad, UnitConversionFactors.Farad }
                        }
                    },
                    {
                        UnitSystems.CGS,  
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Statfarad, UnitConversionFactors.Statfarad },
                            { Units.ESUOfCapacitance, UnitConversionFactors.ESUOfCapacitance },
                            { Units.Abfarad, UnitConversionFactors.Abfarad },
                            { Units.EMUOfCapacitance, UnitConversionFactors.EMUOfCapacitance }
                        }
                    }
                }
            },
            { 
                UnitTypes.ElectricInductance, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Henry, UnitConversionFactors.Henry }
                        }
                    },
                    {
                        UnitSystems.CGS,
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Stathenry, UnitConversionFactors.Stathenry },
                            { Units.ESUOfInductance, UnitConversionFactors.ESUOfInductance },
                            { Units.Abhenry, UnitConversionFactors.Abhenry },
                            { Units.EMUOfInductance, UnitConversionFactors.EMUOfInductance }
                        }
                    }
                }
            },
            { 
                UnitTypes.ElectricDipoleMoment, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI,
                        new Dictionary<Units, decimal>()
                        {
                            { Units.CoulombMetre, UnitConversionFactors.CoulombMetre }
                        }
                    },
                    {
                        UnitSystems.CGS,
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Debye, UnitConversionFactors.Debye }
                        }
                    }
                }
            },
            { 
                UnitTypes.Temperature, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            //Mere Placeholders. Temperature conversions are managed through a function.
                            { Units.Kelvin, 1.0m },
                            { Units.Celsius, 1.0m } 
                        }
                    },
                    {
                        UnitSystems.Imperial, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Fahrenheit, 1.0m },
                            { Units.Rankine, 1.0m }
                        }
                    }
                }
            },
            { 
                UnitTypes.Wavenumber, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                   {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.ReciprocalMetre, UnitConversionFactors.ReciprocalMetre }
                        }
                    },
                    {
                        UnitSystems.CGS, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Kayser, UnitConversionFactors.Kayser }
                        }
                    }
                }
            },
            { 
                UnitTypes.Viscosity, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.PascalSecond, UnitConversionFactors.PascalSecond }
                        }
                    },
                    {
                        UnitSystems.CGS, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Poise, UnitConversionFactors.Poise }
                        }
                    }
                }
            },
            { 
                UnitTypes.KinematicViscosity, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.SquareMetrePerSecond, UnitConversionFactors.SquareMetrePerSecond }
                        }
                    },
                    {
                        UnitSystems.CGS, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Stokes, UnitConversionFactors.Stokes }
                        }
                    }
                }
            },
            { 
                UnitTypes.AmountOfSubstance, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Mole, UnitConversionFactors.Mole }
                        }
                    },
                    {
                        UnitSystems.None, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.PoundMole, UnitConversionFactors.PoundMole }
                        }
                    }
                }
            },
            { 
                UnitTypes.Momentum, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.NewtonSecond, UnitConversionFactors.NewtonSecond }
                        }
                    }
                }
            },
            { 
                UnitTypes.AngularVelocity, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.RadianPerSecond, UnitConversionFactors.RadianPerSecond }
                        }
                    }
                }
            },
            { 
                UnitTypes.AngularAcceleration, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.RadianPerSquareSecond, UnitConversionFactors.RadianPerSquareSecond }
                        }
                    }
                }
            },
            { 
                UnitTypes.AngularMomentum, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.JouleSecond, UnitConversionFactors.JouleSecond }
                        }
                    }
                }
            },
            { 
                UnitTypes.MomentOfInertia, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.KilogramSquareMetre, UnitConversionFactors.KilogramSquareMetre }
                        }
                    }
                }
            },
            { 
                UnitTypes.SolidAngle, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Steradian, UnitConversionFactors.Steradian }
                        }
                    },
                    {
                        UnitSystems.None, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.SquareDegree, UnitConversionFactors.SquareDegree }
                        }
                    }
                }
            },
            { 
                UnitTypes.LuminousIntensity, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Candela, UnitConversionFactors.Candela }
                        }
                    }
                }
            },
            { 
                UnitTypes.LuminousFlux, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Lumen, UnitConversionFactors.Lumen }
                        }
                    }
                }
            },
            { 
                UnitTypes.LuminousEnergy, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.LumenSecond, UnitConversionFactors.LumenSecond }
                        }
                    },
                    {
                        UnitSystems.None, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Talbot, UnitConversionFactors.Talbot }
                        }
                    }
                }
            },
            { 
                UnitTypes.Luminance, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.CandelaPerSquareMetre, UnitConversionFactors.CandelaPerSquareMetre }
                        }
                    },
                    {
                        UnitSystems.CGS, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Stilb, UnitConversionFactors.Stilb },
                            { Units.Lambert, UnitConversionFactors.Lambert }
                        }
                    },
                    {
                        UnitSystems.Imperial, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.FootLambert, UnitConversionFactors.FootLambert }
                        }
                    },
                    {
                        UnitSystems.None, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Nit, UnitConversionFactors.Nit }
                        }
                    }
                }
            },
            { 
                UnitTypes.Illuminance, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Lux, UnitConversionFactors.Lux }
                        }
                    },
                    {
                        UnitSystems.Imperial, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.FootCandle, UnitConversionFactors.FootCandle }
                        }
                    },
                    {
                        UnitSystems.None, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Phot, UnitConversionFactors.Phot }
                        }
                    }
                }
            },
            { 
                UnitTypes.Logarithmic, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.None, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Bel, UnitConversionFactors.Bel },
                            { Units.Neper, UnitConversionFactors.Neper }
                        }
                    }
                }
            },
            { 
                UnitTypes.MagneticFlux, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Weber, UnitConversionFactors.Weber }
                        }
                    },
                    {
                        UnitSystems.CGS, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Maxwell, UnitConversionFactors.Maxwell }
                        }
                    }
                }
            },
            { 
                UnitTypes.MagneticFieldB, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Tesla, UnitConversionFactors.Tesla }
                        }
                    },
                    {
                        UnitSystems.CGS, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Gauss, UnitConversionFactors.Gauss }
                        }
                    }
                }
            },
            { 
                UnitTypes.MagneticFieldH, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.AmperePerMetre, UnitConversionFactors.AmperePerMetre }
                        }
                    },
                    {
                        UnitSystems.CGS, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Oersted, UnitConversionFactors.Oersted }
                        }
                    }
                }
            },
            { 
                UnitTypes.Radioactivity, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Becquerel, UnitConversionFactors.Becquerel }
                        }
                    },
                    {
                        UnitSystems.None, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Curie, UnitConversionFactors.Curie },
                            { Units.DisintegrationsPerMinute, UnitConversionFactors.DisintegrationsPerMinute },
                            { Units.DisintegrationsPerSecond, UnitConversionFactors.DisintegrationsPerSecond },
                            { Units.Rutherford, UnitConversionFactors.Rutherford }
                        }
                    }
                }
            },
            { 
                UnitTypes.AbsorbedDose, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Gray, UnitConversionFactors.Gray }
                        }
                    },
                    {
                        UnitSystems.CGS, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Rad, UnitConversionFactors.Rad }
                        }
                    }
                }
            },
            { 
                UnitTypes.AbsorbedDoseRate, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.GrayPerSecond, UnitConversionFactors.GrayPerSecond }
                        }
                    },
                }
            },
            { 
                UnitTypes.EquivalentDose, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Sievert, UnitConversionFactors.Sievert }
                        }
                    },
                    {
                        UnitSystems.CGS, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.REM, UnitConversionFactors.REM }
                        }
                    }
                }
            },
            { 
                UnitTypes.Exposure, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.CoulombPerKilogram, UnitConversionFactors.CoulombPerKilogram }
                        }
                    },
                    {
                        UnitSystems.CGS, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Roentgen, UnitConversionFactors.Roentgen }
                        }
                    }
                }
            },
            { 
                UnitTypes.CatalyticActivity, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Katal, UnitConversionFactors.Katal }
                        }
                    }
                }
            },
            { 
                UnitTypes.CatalyticActivityConcentration, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.KatalPerCubicMetre, UnitConversionFactors.KatalPerCubicMetre }
                        }
                    }
                }
            },
            { 
                UnitTypes.Jerk, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.MetrePerCubicSecond, UnitConversionFactors.MetrePerCubicSecond }
                        }
                    }
                }
            },
            { 
                UnitTypes.MassFlowRate, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.KilogramPerSecond, UnitConversionFactors.KilogramPerSecond }
                        }
                    }
                }
            },
            { 
                UnitTypes.Density, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.KilogramPerCubicMetre, UnitConversionFactors.KilogramPerCubicMetre }
                        }
                    }
                }
            },
            { 
                UnitTypes.AreaDensity, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.KilogramPerSquareMetre, UnitConversionFactors.KilogramPerSquareMetre }
                        }
                    }
                }
            },
            { 
                UnitTypes.EnergyDensity, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.JoulePerCubicMetre, UnitConversionFactors.JoulePerCubicMetre }
                        }
                    }
                }
            },
            { 
                UnitTypes.SpecificVolume, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.CubicMetrePerKilogram, UnitConversionFactors.CubicMetrePerKilogram }
                        }
                    }
                }
            },
            { 
                UnitTypes.VolumetricFlowRate, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.CubicMetrePerSecond, UnitConversionFactors.CubicMetrePerSecond }
                        }
                    }
                }
            },
            { 
                UnitTypes.SurfaceTension, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.JoulePerSquareMetre, UnitConversionFactors.JoulePerSquareMetre }
                        }
                    }
                }
            },
            { 
                UnitTypes.SpecificWeight, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.NewtonPerCubicMetre, UnitConversionFactors.NewtonPerCubicMetre }
                        }
                    }
                }
            },
            { 
                UnitTypes.ThermalConductivity, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.WattPerMetrePerKelvin, UnitConversionFactors.WattPerMetrePerKelvin }
                        }
                    }
                }
            },
            { 
                UnitTypes.ThermalConductance, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.WattPerKelvin, UnitConversionFactors.WattPerKelvin }
                        }
                    }
                }
            },
            { 
                UnitTypes.ThermalResistivity, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.MetreKelvinPerWatt, UnitConversionFactors.MetreKelvinPerWatt }
                        }
                    }
                }
            },
            { 
                UnitTypes.ThermalResistance, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.KelvinPerWatt, UnitConversionFactors.KelvinPerWatt }
                        }
                    }
                }
            },
            { 
                UnitTypes.HeatTransferCoefficient, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.WattPerSquareMetrePerKelvin, UnitConversionFactors.WattPerSquareMetrePerKelvin }
                        }
                    }
                }
            },
            { 
                UnitTypes.HeatFluxDensity, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.WattPerSquareMetre, UnitConversionFactors.WattPerSquareMetre }
                        }
                    }
                }
            },
            { 
                UnitTypes.Entropy, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.JoulePerKelvin, UnitConversionFactors.JoulePerKelvin }
                        }
                    }
                }
            },
            { 
                UnitTypes.ElectricFieldStrength, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.NewtonPerCoulomb, UnitConversionFactors.NewtonPerCoulomb }
                        }
                    }
                }
            },
            { 
                UnitTypes.LinearElectricChargeDensity, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.CoulombPerMetre, UnitConversionFactors.CoulombPerMetre }
                        }
                    }
                }
            },
            { 
                UnitTypes.SurfaceElectricChargeDensity, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.CoulombPerSquareMetre, UnitConversionFactors.CoulombPerSquareMetre }
                        }
                    }
                }
            },
            { 
                UnitTypes.VolumeElectricChargeDensity, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.CoulombPerCubicMetre, UnitConversionFactors.CoulombPerCubicMetre }
                        }
                    }
                }
            },
            { 
                UnitTypes.CurrentDensity, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.AmperePerSquareMetre, UnitConversionFactors.AmperePerSquareMetre }
                        }
                    }
                }
            },
            { 
                UnitTypes.Permittivity, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.FaradPerMetre, UnitConversionFactors.FaradPerMetre }
                        }
                    }
                }
            },
            { 
                UnitTypes.Permeability, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.HenryPerMetre, UnitConversionFactors.HenryPerMetre }
                        }
                    }
                }
            },
            { 
                UnitTypes.MolarEnergy, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.JoulePerMole, UnitConversionFactors.JoulePerMole }
                        }
                    }
                }
            },
            { 
                UnitTypes.MolarEntropy, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.JoulePerMolePerKelvin, UnitConversionFactors.JoulePerMolePerKelvin }
                        }
                    }
                }
            },
            { 
                UnitTypes.MolarVolume, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.CubicMetrePerMole, UnitConversionFactors.CubicMetrePerMole }
                        }
                    }
                }
            },
            { 
                UnitTypes.MolarMass, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.KilogramPerMole, UnitConversionFactors.KilogramPerMole }
                        }
                    }
                }
            },
            { 
                UnitTypes.MolarConcentration, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.MolePerCubicMetre, UnitConversionFactors.MolePerCubicMetre }
                        }
                    }
                }
            },
            { 
                UnitTypes.MolalConcentration, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.MolePerKilogram, UnitConversionFactors.MolePerKilogram }
                        }
                    }
                }
            },
            { 
                UnitTypes.RadiantIntensity, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.WattPerSteradian, UnitConversionFactors.WattPerSteradian }
                        }
                    }
                }
            },
            { 
                UnitTypes.Radiance, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.WattPerSteradianPerSquareMetre, UnitConversionFactors.WattPerSteradianPerSquareMetre }
                        }
                    }
                }
            },
            { 
                UnitTypes.FuelEconomy, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.InverseSquareMetre, UnitConversionFactors.InverseSquareMetre }
                        }
                    },
                    {
                        UnitSystems.Imperial, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.MilePerGallon, UnitConversionFactors.MilePerGallon },
                            { Units.ImperialMilePerGallon, UnitConversionFactors.ImperialMilePerGallon }
                        }
                    },
                    {
                        UnitSystems.USCS, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.USCSMilePerGallon, UnitConversionFactors.USCSMilePerGallon }
                        }
                    }
                }
            },
            { 
                UnitTypes.SoundExposure, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.SquarePascalSecond, UnitConversionFactors.SquarePascalSecond }
                        }
                    }
                }
            },
            { 
                UnitTypes.SoundImpedance, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.PascalSecondPerCubicMetre, UnitConversionFactors.PascalSecondPerCubicMetre }
                        }
                    }
                }
            },
            { 
                UnitTypes.RotationalStiffness, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.SI, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.NewtonMetrePerRadian, UnitConversionFactors.NewtonMetrePerRadian }
                        }
                    }
                }
            },
            { 
                UnitTypes.BitRate, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.None, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.BitPerSecond, UnitConversionFactors.BitPerSecond }
                        }
                    }
                }
            },
            { 
                UnitTypes.SymbolRate, 
                new Dictionary<UnitSystems, Dictionary<Units, decimal>>()
                {
                    {
                        UnitSystems.None, 
                        new Dictionary<Units, decimal>()
                        {
                            { Units.Baud, UnitConversionFactors.Baud }
                        }
                    }
                }
            }
        }; 

        //Dictionary mostly meant to deal with the Imperial/USCS peculiar relationship.
        //Imperial is used for all the calculations and USCS just for information-displaying purposes
        private static Dictionary<UnitSystems, UnitSystems> AllBasicSystems = 
        new Dictionary<UnitSystems, UnitSystems>()
        {
            { UnitSystems.SI, UnitSystems.SI },
            { UnitSystems.CGS, UnitSystems.CGS },
            { UnitSystems.Imperial, UnitSystems.Imperial },
            { UnitSystems.USCS, UnitSystems.Imperial },
            { UnitSystems.ImperialAndUSCS, UnitSystems.Imperial },
            
            { UnitSystems.None, UnitSystems.None } //Each scenario has to be accounted.
        };

        //Some times, all what matters is knowing whether the system is metric (SI) or English (Imperial)
        private static Dictionary<UnitSystems, UnitSystems> AllMetricEnglish = 
        new Dictionary<UnitSystems, UnitSystems>()
        {
            { UnitSystems.SI, UnitSystems.SI },
            { UnitSystems.CGS, UnitSystems.SI },
            { UnitSystems.Imperial, UnitSystems.Imperial },
            { UnitSystems.USCS, UnitSystems.Imperial },
            { UnitSystems.ImperialAndUSCS, UnitSystems.Imperial },

            { UnitSystems.None, UnitSystems.None } //Each scenario has to be accounted for.
        };

        //Relates all the unnamed units with their associated systems.
        //There are many units which don't fit any Units enum case, what this unnamed category addresses.
        //That is: placeholders avoiding a huge (and not too logical) hardcoding effort.
        private static Dictionary<UnitSystems, Units> DefaultUnnamedUnits = 
        AllUnits[UnitTypes.None].ToDictionary(x => x.Key, x => x.Value.First().Key);    

        //Relates all the units with their respective types.
        //The call to GetALLMain() also populates all the collections below this line.
        private static Dictionary<Units, UnitTypes> AllUnitTypes = GetAllMain();

        //Relates all the units with their respective systems.
        private static Dictionary<Units, UnitSystems> AllUnitSystems;

        //Relates all the units with their respective conversion factors.
        private static Dictionary<Units, decimal> AllUnitConversionFactors;

        //Includes all the supported unit string representations where case doesn't matter.
        private static Dictionary<string, Units> AllUnitStrings;

        //Includes secondary symbols for some units (case does matter).
        private static Dictionary<string, Units> AllUnitSymbols2;

        //Some conversion factors are too small/big for decimal type.
        private static Dictionary<decimal, UnitInfo> AllBeyondDecimalConversionFactors;
    }
}
