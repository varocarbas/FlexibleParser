using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    public partial class UnitP
    {

        //Contains the definitions of all the supported compounds, understood as units formed by other units
        //and/or variations (e.g., exponents different than 1) of them.
        //In order to be as efficient as possible, AllCompounds ignores the difference between dividable and 
        //non-dividable units. For example: N is formed by kg*m/s2, exactly what this collection expects; on the
        //other hand, lbf isn't formed by the expected lb*ft/s2. In any case, note that this "faulty" format is
        //only used internally, never shown to the user.
        //NOTE: the order of the compounds within each type does matter. The first position is reserved for the main
        //fully-expanded version (e.g., mass*length/time2 for force). In the second position, the compound basic 
        //units (e.g., energy) are expected to have their 1-part version (e.g., 1 energy part for energy).
        private static Dictionary<UnitTypes, Compound[]> AllCompounds = new Dictionary<UnitTypes, Compound[]>()
        {
            {
                UnitTypes.Area, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>() { new CompoundPart(UnitTypes.Length, 2) }
                    ),
                    new Compound
                    (
                        new List<CompoundPart>() { new CompoundPart(UnitTypes.Area) }
                    )
                }
            },
            {
                UnitTypes.Volume, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>() { new CompoundPart(UnitTypes.Length, 3) }
                    ),
                    new Compound
                    (
                        new List<CompoundPart>() { new CompoundPart(UnitTypes.Volume) }
                    )
                }
            },
            {
                UnitTypes.Velocity, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Length),
                            new CompoundPart(UnitTypes.Time, -1)
                        }
                    )
                }
            },
            {
                UnitTypes.Acceleration, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Length),
                            new CompoundPart(UnitTypes.Time, -2)
                        }
                    )
                }
            },
            {
                UnitTypes.Force, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Mass),
                            new CompoundPart(UnitTypes.Length),
                            new CompoundPart(UnitTypes.Time, -2)
                        }
                    ),
                    new Compound
                    (
                        new List<CompoundPart>() { new CompoundPart(UnitTypes.Force) }
                    )
                }
            }, 
            {
                UnitTypes.Energy, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Mass),
                            new CompoundPart(UnitTypes.Length, 2),
                            new CompoundPart(UnitTypes.Time, -2)
                        }
                    ),
                    new Compound
                    (
                        new List<CompoundPart>() { new CompoundPart(UnitTypes.Energy) }
                    )
                }
            },
            {
                UnitTypes.Power, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Mass),
                            new CompoundPart(UnitTypes.Length, 2),
                            new CompoundPart(UnitTypes.Time, -3)
                        }
                    ),
                    new Compound
                    (
                        new List<CompoundPart>() { new CompoundPart(UnitTypes.Power) }
                    )
                }
            },
            {
                UnitTypes.Pressure, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Mass),
                            new CompoundPart(UnitTypes.Length, -1),
                            new CompoundPart(UnitTypes.Time, -2)
                        }
                    )
                }
            },
            {
                UnitTypes.Frequency, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>() { new CompoundPart(UnitTypes.Time, -1) }
                    )
                }
            },
            {
                UnitTypes.ElectricCharge, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.ElectricCurrent),
                            new CompoundPart(UnitTypes.Time)
                        }
                    )
                }
            },
            {
                UnitTypes.ElectricVoltage, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Mass),
                            new CompoundPart(UnitTypes.Length, 2),
                            new CompoundPart(UnitTypes.Time, -3),
                            new CompoundPart(UnitTypes.ElectricCurrent, -1)
                        }
                    )
                }
            },
            {
                UnitTypes.ElectricResistance, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Mass),
                            new CompoundPart(UnitTypes.Length, 2),
                            new CompoundPart(UnitTypes.Time, -3),
                            new CompoundPart(UnitTypes.ElectricCurrent, -2),
                        }
                    )
                }
            },
            {
                UnitTypes.ElectricResistivity, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Mass),
                            new CompoundPart(UnitTypes.Length, 3),
                            new CompoundPart(UnitTypes.Time, -3),
                            new CompoundPart(UnitTypes.ElectricCurrent, -2),
                        }
                    )
                }
            },
            {
                UnitTypes.ElectricConductance, new Compound[]
                {
                    new Compound 
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.ElectricCurrent, 2),
                            new CompoundPart(UnitTypes.Time, 3),
                            new CompoundPart(UnitTypes.Mass, -1),
                            new CompoundPart(UnitTypes.Length, -2),
                        }
                    )
                }
            },
            {
                UnitTypes.ElectricConductivity, new Compound[]
                {
                    new Compound 
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.ElectricCurrent, 2),
                            new CompoundPart(UnitTypes.Time, 3),
                            new CompoundPart(UnitTypes.Mass, -1),
                            new CompoundPart(UnitTypes.Length, -3),
                        }
                    )
                }
            },
            {
                UnitTypes.ElectricCapacitance, new Compound[]
                {
                    new Compound 
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.ElectricCurrent, 2),
                            new CompoundPart(UnitTypes.Time, 4),
                            new CompoundPart(UnitTypes.Mass, -1),
                            new CompoundPart(UnitTypes.Length, -2)
                        }
                    )
                }
            },     
            {
                UnitTypes.ElectricInductance, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Mass),
                            new CompoundPart(UnitTypes.Length, 2),
                            new CompoundPart(UnitTypes.Time, -2),
                            new CompoundPart(UnitTypes.ElectricCurrent, -2)
                        }
                    )
                }
            },  
            {
                UnitTypes.ElectricDipoleMoment, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.ElectricCurrent),
                            new CompoundPart(UnitTypes.Time, 1),
                            new CompoundPart(UnitTypes.Length, 1)
                        }
                    )
                }
            },
            {
                UnitTypes.Wavenumber, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>() { new CompoundPart(UnitTypes.Length, -1) }
                    )
                }
            }, 
            {
                UnitTypes.Viscosity, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Mass),
                            new CompoundPart(UnitTypes.Length, -1),
                            new CompoundPart(UnitTypes.Time, -1)
                        }
                    )
                }
            }, 
            {
                UnitTypes.KinematicViscosity, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Length, 2),
                            new CompoundPart(UnitTypes.Time, -1)
                        }
                    )
                }
            }, 
            {
                UnitTypes.Momentum, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Mass),
                            new CompoundPart(UnitTypes.Length),
                            new CompoundPart(UnitTypes.Time, -1)
                        }
                    )
                }
            }, 
            {
                UnitTypes.AngularVelocity, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Angle),
                            new CompoundPart(UnitTypes.Time, -1)
                        }
                    )
                }
            },
            {
                UnitTypes.AngularAcceleration, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Angle),
                            new CompoundPart(UnitTypes.Time, -2)
                        }
                    )
                }
            },
            {
                UnitTypes.AngularMomentum, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Mass),
                            new CompoundPart(UnitTypes.Length, 2),
                            new CompoundPart(UnitTypes.Time, -1)
                        }
                    )
                }
            },
            {
                UnitTypes.MomentOfInertia, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Mass),
                            new CompoundPart(UnitTypes.Length, 2)
                        }
                    )
                }
            },
            {
                UnitTypes.SolidAngle, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Angle, 2)
                        }
                    )
                }
            },
            {
                UnitTypes.LuminousFlux, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.LuminousIntensity),
                            new CompoundPart(UnitTypes.SolidAngle)
                        }
                    )
                }
            },
            {
                UnitTypes.LuminousEnergy, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.LuminousIntensity),
                            new CompoundPart(UnitTypes.SolidAngle),
                            new CompoundPart(UnitTypes.Time)
                        }
                    )
                }
            },
            {
                UnitTypes.Luminance, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.LuminousIntensity),
                            new CompoundPart(UnitTypes.Length, -2)
                        }
                    )
                }
            },
            {
                UnitTypes.Illuminance, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.LuminousIntensity),
                            new CompoundPart(UnitTypes.SolidAngle),
                            new CompoundPart(UnitTypes.Length, -2)
                        }
                    )
                }
            },
            {
                UnitTypes.MagneticFlux, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Mass),
                            new CompoundPart(UnitTypes.Length, 2),
                            new CompoundPart(UnitTypes.Time, -2),
                            new CompoundPart(UnitTypes.ElectricCurrent, -1)
                        }
                    )
                }
            },
            {
                UnitTypes.MagneticFieldB, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Mass),
                            new CompoundPart(UnitTypes.Time, -2),
                            new CompoundPart(UnitTypes.ElectricCurrent, -1)
                        }
                    )
                }
            },
            {
                UnitTypes.MagneticFieldH, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.ElectricCurrent),
                            new CompoundPart(UnitTypes.Length, -1)
                        }
                    )
                }
            },
            {
                UnitTypes.AbsorbedDose, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Length, 2),
                            new CompoundPart(UnitTypes.Time, -2)
                        }
                    )
                }
            },
            {
                UnitTypes.AbsorbedDoseRate, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Length, 2),
                            new CompoundPart(UnitTypes.Time, -3)
                        }
                    )
                }
            },
            {
                UnitTypes.EquivalentDose, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Length, 2),
                            new CompoundPart(UnitTypes.Time, -2)
                        }
                    )
                }
            },
            {
                UnitTypes.Exposure, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.ElectricCurrent),
                            new CompoundPart(UnitTypes.Time),
                            new CompoundPart(UnitTypes.Mass, -1)
                        }
                    )
                }
            },
            {
                UnitTypes.SpecificEnergy, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Length, 2),
                            new CompoundPart(UnitTypes.Time, -2)
                        }
                    )
                }
            },
            {
                UnitTypes.CatalyticActivity, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.AmountOfSubstance),
                            new CompoundPart(UnitTypes.Time, -1)
                        }
                    )
                }
            },
            {
                UnitTypes.CatalyticActivityConcentration, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.AmountOfSubstance),
                            new CompoundPart(UnitTypes.Time, -1),
                            new CompoundPart(UnitTypes.Length, -3)
                        }
                    )
                }
            },
            {
                UnitTypes.Jerk, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Length),
                            new CompoundPart(UnitTypes.Time, -3)
                        }
                    )
                }
            },
            {
                UnitTypes.MassFlowRate, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Mass),
                            new CompoundPart(UnitTypes.Time, -1)
                        }
                    )
                }
            },
            {
                UnitTypes.Density, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Mass),
                            new CompoundPart(UnitTypes.Length, -3)
                        }
                    )
                }
            },
            {
                UnitTypes.AreaDensity, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Mass),
                            new CompoundPart(UnitTypes.Length, -2)
                        }
                    )
                }
            },
            {
                UnitTypes.EnergyDensity, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Mass),
                            new CompoundPart(UnitTypes.Length, -2)
                        }
                    )
                }
            },
            {
                UnitTypes.SpecificVolume, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Length, 3),
                            new CompoundPart(UnitTypes.Mass, -1)
                        }
                    )
                }
            },
            {
                UnitTypes.VolumetricFlowRate, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Length, 3),
                            new CompoundPart(UnitTypes.Time, -1)
                        }
                    )
                }
            },
            {
                UnitTypes.SurfaceTension, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Mass),
                            new CompoundPart(UnitTypes.Time, -2)
                        }
                    )
                }
            },
            {
                UnitTypes.SpecificWeight, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Mass),
                            new CompoundPart(UnitTypes.Time, -2),
                            new CompoundPart(UnitTypes.Length, -2)
                        }
                    )
                }
            },
            {
                UnitTypes.ThermalConductivity, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Mass),
                            new CompoundPart(UnitTypes.Length),
                            new CompoundPart(UnitTypes.Time, -3),
                            new CompoundPart(UnitTypes.Temperature, -1)
                        }
                    )
                }
            },
            {
                UnitTypes.ThermalConductance, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Mass),
                            new CompoundPart(UnitTypes.Length, 2),
                            new CompoundPart(UnitTypes.Time, -3),
                            new CompoundPart(UnitTypes.Temperature, -1)
                        }
                    )
                }
            },
            {
                UnitTypes.ThermalResistivity, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Time, 3),
                            new CompoundPart(UnitTypes.Temperature),
                            new CompoundPart(UnitTypes.Mass, -1),
                            new CompoundPart(UnitTypes.Length, -1)
                        }
                    )
                }
            },
            {
                UnitTypes.ThermalResistance, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Time, 3),
                            new CompoundPart(UnitTypes.Temperature),
                            new CompoundPart(UnitTypes.Mass, -1),
                            new CompoundPart(UnitTypes.Length, -2)
                        }
                    )
                }
            },
            {
                UnitTypes.HeatTransferCoefficient, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Mass),
                            new CompoundPart(UnitTypes.Time, -3),
                            new CompoundPart(UnitTypes.Temperature, -1)
                        }
                    )
                }
            },
            {
                UnitTypes.HeatFluxDensity, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Mass),
                            new CompoundPart(UnitTypes.Time, -3)
                        }
                    )
                }
            },
            {
                UnitTypes.Entropy, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Mass),
                            new CompoundPart(UnitTypes.Length, 2),
                            new CompoundPart(UnitTypes.Time, -2),
                            new CompoundPart(UnitTypes.Temperature, -1)
                        }
                    )
                }
            },
            {
                UnitTypes.ElectricFieldStrength, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Mass),
                            new CompoundPart(UnitTypes.Length),
                            new CompoundPart(UnitTypes.Time, -3),
                            new CompoundPart(UnitTypes.ElectricCurrent, -1)
                        }
                    )
                }
            },
            {
                UnitTypes.LinearElectricChargeDensity, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.ElectricCurrent),
                            new CompoundPart(UnitTypes.Time),
                            new CompoundPart(UnitTypes.Length, -1)
                        }
                    )
                }
            },
            {
                UnitTypes.SurfaceElectricChargeDensity, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.ElectricCurrent),
                            new CompoundPart(UnitTypes.Time),
                            new CompoundPart(UnitTypes.Length, -2)
                        }
                    )
                }
            },
            {
                UnitTypes.VolumeElectricChargeDensity, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.ElectricCurrent),
                            new CompoundPart(UnitTypes.Time),
                            new CompoundPart(UnitTypes.Length, -3)
                        }
                    )
                }
            },
            {
                UnitTypes.CurrentDensity, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.ElectricCurrent),
                            new CompoundPart(UnitTypes.Length, -2)
                        }
                    )
                }
            },
            {
                UnitTypes.Permittivity, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.ElectricCurrent, 2),
                            new CompoundPart(UnitTypes.Time, 4),
                            new CompoundPart(UnitTypes.Mass, -1),
                            new CompoundPart(UnitTypes.Length, -3)
                        }
                    )
                }
            },
            {
                UnitTypes.Permeability, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Mass),
                            new CompoundPart(UnitTypes.Length, 1),
                            new CompoundPart(UnitTypes.Time, -2),
                            new CompoundPart(UnitTypes.ElectricCurrent, -2)
                        }
                    )
                }
            },
            {
                UnitTypes.MolarEnergy, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Mass),
                            new CompoundPart(UnitTypes.Length, 2),
                            new CompoundPart(UnitTypes.Time, -2),
                            new CompoundPart(UnitTypes.AmountOfSubstance, -1)
                        }
                    )
                }
            },
            {
                UnitTypes.MolarEntropy, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Mass),
                            new CompoundPart(UnitTypes.Length, 2),
                            new CompoundPart(UnitTypes.Time, -2),
                            new CompoundPart(UnitTypes.AmountOfSubstance, -1),
                            new CompoundPart(UnitTypes.Temperature, -1),
                        }
                    )
                }
            },
            {
                UnitTypes.MolarVolume, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Length, 3),
                            new CompoundPart(UnitTypes.AmountOfSubstance, -1),
                        }
                    )
                }
            },
            {
                UnitTypes.MolarMass, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Mass),
                            new CompoundPart(UnitTypes.AmountOfSubstance, -1),
                        }
                    )
                }
            },
            {
                UnitTypes.MolarConcentration, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.AmountOfSubstance),
                            new CompoundPart(UnitTypes.Length, -3),
                        }
                    )
                }
            },
            {
                UnitTypes.MolalConcentration, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.AmountOfSubstance),
                            new CompoundPart(UnitTypes.Mass, -1),
                        }
                    )
                }
            },
            {
                UnitTypes.RadiantIntensity, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Mass),
                            new CompoundPart(UnitTypes.Length, 2),
                            new CompoundPart(UnitTypes.Time, -3),
                            new CompoundPart(UnitTypes.SolidAngle, -1)
                        }
                    )
                }
            },
            {
                UnitTypes.Radiance, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Mass),
                            new CompoundPart(UnitTypes.Time, -3),
                            new CompoundPart(UnitTypes.SolidAngle, -1)
                        }
                    )
                }
            },
            {
                UnitTypes.FuelEconomy, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Length, -2)
                        }
                    )
                }
            },
            {
                UnitTypes.SoundExposure, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Mass, 2),
                            new CompoundPart(UnitTypes.Length, -2),
                            new CompoundPart(UnitTypes.Time, -3)
                        }
                    )
                }
            },
            {
                UnitTypes.SoundImpedance, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Mass),
                            new CompoundPart(UnitTypes.Length, -4),
                            new CompoundPart(UnitTypes.Time, -1)
                        }
                    )
                }
            },
            {
                UnitTypes.RotationalStiffness, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Mass),
                            new CompoundPart(UnitTypes.Length, 2),
                            new CompoundPart(UnitTypes.Time, -2),
                            new CompoundPart(UnitTypes.Angle, -1),
                        }
                    )
                }
            },
            {
                UnitTypes.BitRate, new Compound[]
                {
                    new Compound
                    (
                        new List<CompoundPart>()
                        {
                            new CompoundPart(UnitTypes.Information),
                            new CompoundPart(UnitTypes.Time, -1),
                        }
                    )
                }
            }
        };

        //Contains all the named compounds defined by the basic units for the given type/system.
        //Example: Newton is formed by kg*m/s^2, the basic mass*length/time units in SI; that's why it belongs here.
        //NOTE: all these compounds are divided into their most basic constituent parts (againt what some non-SI units expect).
        private static Dictionary<UnitTypes, Dictionary<UnitSystems, Units>> AllBasicCompounds = new Dictionary<UnitTypes, Dictionary<UnitSystems, Units>>()
        {
           {
                UnitTypes.Area, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.SquareMetre },
                    { UnitSystems.CGS, Units.SquareCentimetre },
                    { UnitSystems.Imperial, Units.SquareFoot }
                }
            },
            {
                UnitTypes.Volume, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.CubicMetre },
                    { UnitSystems.CGS, Units.CubicCentimetre },
                    { UnitSystems.Imperial, Units.CubicFoot }
                }
            },
            {
                UnitTypes.Velocity, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.MetrePerSecond },
                    { UnitSystems.CGS, Units.CentimetrePerSecond },
                    { UnitSystems.Imperial, Units.FootPerSecond }
                }
            },
            {
                UnitTypes.Acceleration, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.MetrePerSquareSecond },
                    { UnitSystems.CGS, Units.Gal },
                    { UnitSystems.Imperial, Units.FootPerSquareSecond }
                }
            },
            {
                UnitTypes.Force, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.Newton },
                    { UnitSystems.CGS, Units.Dyne },
                    { UnitSystems.Imperial, Units.Poundal },
                }
            },
            {
                UnitTypes.Energy, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.Joule },
                    { UnitSystems.CGS, Units.Erg }
                }
            },
            {
                UnitTypes.Power, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.Watt },
                    { UnitSystems.CGS, Units.ErgPerSecond }
                }
            },
            {
                UnitTypes.Pressure, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.Pascal },
                    { UnitSystems.CGS, Units.Barye },
                }
            },
            {
                UnitTypes.Frequency, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.Hertz }
                }
            },
            {
                UnitTypes.ElectricCharge, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.Coulomb },
                }
            },
            {
                UnitTypes.ElectricCurrent, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.Ampere },
                }
            },
            {
                UnitTypes.ElectricVoltage, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.Volt },
                }
            },
            {
                UnitTypes.ElectricResistance, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.Ohm },
                }
            },
            {
                UnitTypes.ElectricResistivity, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.OhmMetre },
                }
            },
            {
                UnitTypes.ElectricConductance, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.Siemens }
                }
            },
            {
                UnitTypes.ElectricConductivity, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.SiemensPerMetre },
                }
            },
            {
                UnitTypes.ElectricCapacitance, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.Farad },
                }
            },
            {
                UnitTypes.ElectricInductance, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.Henry },
                }
            },
            {
                UnitTypes.ElectricDipoleMoment, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.CoulombMetre },
                }
            },
            {
                UnitTypes.Wavenumber, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.ReciprocalMetre },
                    { UnitSystems.CGS, Units.Kayser }
                }
            },
            {
                UnitTypes.Viscosity, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.PascalSecond },
                    { UnitSystems.CGS, Units.Poise }
                }
            },
            {
                UnitTypes.KinematicViscosity, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.SquareMetrePerSecond },
                    { UnitSystems.CGS, Units.Stokes }
                }
            },
            {
                UnitTypes.Momentum, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.NewtonSecond }
                }
            },
            {
                UnitTypes.AngularVelocity, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.RadianPerSecond }
                }
            },
            {
                UnitTypes.AngularAcceleration, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.RadianPerSquareSecond }
                }
            },
            {
                UnitTypes.AngularMomentum, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.JouleSecond }
                }
            },
            {
                UnitTypes.MomentOfInertia, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.KilogramSquareMetre }
                }
            },
            {
                UnitTypes.SolidAngle, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.Steradian }
                }
            },
            {
                UnitTypes.LuminousFlux, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.Lumen }
                }
            },
            {
                UnitTypes.LuminousEnergy, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.LumenSecond }
                }
            },
            {
                UnitTypes.Luminance, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.CandelaPerSquareMetre },
                    { UnitSystems.CGS, Units.Stilb },
                }
            },
            {
                UnitTypes.Illuminance, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.Lux },
                    { UnitSystems.CGS, Units.Phot },
                    { UnitSystems.Imperial, Units.FootCandle }
                }
            },
            {
                UnitTypes.MagneticFlux, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.Weber },
                }
            },
            {
                UnitTypes.MagneticFieldB, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.Tesla },
                }
            },
            {
                UnitTypes.AbsorbedDose, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.Gray },
                    { UnitSystems.CGS, Units.Rad }
                }
            },
            {
                UnitTypes.AbsorbedDoseRate, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.GrayPerSecond }
                }
            },
            {
                UnitTypes.EquivalentDose, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.Sievert },
                    { UnitSystems.CGS, Units.REM }
                }
            },
            {
                UnitTypes.Exposure, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.CoulombPerKilogram },
                    { UnitSystems.CGS, Units.Roentgen }
                }
            },
            {
                UnitTypes.CatalyticActivity, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.Katal }
                }
            },
            {
                UnitTypes.CatalyticActivityConcentration, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.KatalPerCubicMetre }
                }
            },
            {
                UnitTypes.Jerk, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.MetrePerCubicSecond }                  
                }
            },
            {
                UnitTypes.MassFlowRate, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.KilogramPerSecond }                  
                }
            },
            {
                UnitTypes.Density, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.KilogramPerCubicMetre }                  
                }
            },
            {
                UnitTypes.AreaDensity, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.KilogramPerSquareMetre }                  
                }
            },
            {
                UnitTypes.EnergyDensity, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.JoulePerCubicMetre }                  
                }
            },
            {
                UnitTypes.SpecificVolume, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.CubicMetrePerKilogram }                  
                }
            },
            {
                UnitTypes.VolumetricFlowRate, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.CubicMetrePerSecond }                  
                }
            },
            {
                UnitTypes.SurfaceTension, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.JoulePerSquareMetre }                  
                }
            },
            {
                UnitTypes.SpecificWeight, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.NewtonPerCubicMetre }                  
                }
            },
            {
                UnitTypes.ThermalConductivity, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.WattPerMetrePerKelvin }                  
                }
            },
            {
                UnitTypes.ThermalConductance, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.WattPerKelvin }                  
                }
            },
            {
                UnitTypes.ThermalResistivity, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.MetreKelvinPerWatt }                  
                }
            },
            {
                UnitTypes.ThermalResistance, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.KelvinPerWatt }                  
                }
            },
            {
                UnitTypes.HeatTransferCoefficient, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.WattPerSquareMetrePerKelvin }                  
                }
            },
            {
                UnitTypes.HeatFluxDensity, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.WattPerSquareMetre }                  
                }
            },
            {
                UnitTypes.Entropy, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.JoulePerKelvin }                  
                }
            },
            {
                UnitTypes.ElectricFieldStrength, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.NewtonPerCoulomb }                  
                }
            },
            {
                UnitTypes.LinearElectricChargeDensity, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.CoulombPerMetre }                  
                }
            },
            {
                UnitTypes.SurfaceElectricChargeDensity, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.CoulombPerSquareMetre }                  
                }
            },
            {
                UnitTypes.VolumeElectricChargeDensity, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.CoulombPerCubicMetre }                  
                }
            },
            {
                UnitTypes.CurrentDensity, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.AmperePerSquareMetre }                  
                }
            },
            {
                UnitTypes.Permittivity, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.FaradPerMetre }                  
                }
            },
            {
                UnitTypes.Permeability, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.HenryPerMetre }                  
                }
            },
            {
                UnitTypes.MolarEnergy, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.JoulePerMole }                  
                }
            },
            {
                UnitTypes.MolarEntropy, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.JoulePerMolePerKelvin }                  
                }
            },
            {
                UnitTypes.MolarVolume, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.CubicMetrePerMole }                  
                }
            },
            {
                UnitTypes.MolarMass, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.KilogramPerMole }                  
                }
            },
            {
                UnitTypes.MolarConcentration, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.MolePerCubicMetre }                  
                }
            },
            {
                UnitTypes.MolalConcentration, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.MolePerKilogram }                  
                }
            },
            {
                UnitTypes.RadiantIntensity, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.WattPerSteradian }                  
                }
            },
            {
                UnitTypes.Radiance, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.WattPerSteradianPerSquareMetre }                  
                }
            },
            {
                UnitTypes.FuelEconomy, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.InverseSquareMetre },                
                }
            },
            {
                UnitTypes.SoundExposure, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.SquarePascalSecond }                
                }
            },
            {
                UnitTypes.SoundImpedance, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.PascalSecondPerCubicMetre }                
                }
            },
            {
                UnitTypes.RotationalStiffness, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.NewtonMetrePerRadian }                
                }
            },
            {
                UnitTypes.BitRate, new Dictionary<UnitSystems, Units>()
                {
                    { UnitSystems.SI, Units.BitPerSecond },
                    { UnitSystems.Imperial, Units.BitPerSecond },
                    { UnitSystems.CGS, Units.BitPerSecond }                
                }
            }
        };

        //Roughly speaking, AllNonBasicCompounds is a container of somehow exceptional situations. Ideally,
        //AllCompounds (+ associated variables) should be enough to deal with this reality by its own. 
        //Some of these units (e.g., centimetre) shouldn't be matched when looking for valid compounds.
        //The reason for being here is defining its constituent parts (what cannot be done in AllCompounds).
        private static Units[] NonBasicCompoundsToSkip = new Units[]
        {
            Units.Centimetre
        };

        //Contains the definition (i.e., UnitPart[] containing their defining units) of all the supported named 
        //compounds except the ones defined by the given system basic units (included in AllBasicCompounds).
        private static Dictionary<Units, UnitPart[]> AllNonBasicCompounds = new Dictionary<Units, UnitPart[]>()
        {
            { 
                Units.Centimetre, //Not exactly a compound, but required for consistency reasons.
                new UnitPart[] { new UnitPart(Units.Metre, SIPrefixValues.Centi) } 
            },
            { 
                Units.SquareInch, 
                new UnitPart[] { new UnitPart(Units.Inch, 2) } 
            },
            { 
                Units.SquareRod, 
                new UnitPart[] { new UnitPart(Units.Rod, 2) } 
            },
            { 
                Units.SquarePerch, 
                new UnitPart[] { new UnitPart(Units.Perch, 2) } 
            },
            { 
                Units.SquarePole, 
                new UnitPart[] { new UnitPart(Units.Pole, 2) } 
            },
            { 
                Units.CubicInch, 
                new UnitPart[] { new UnitPart(Units.Inch, 3) } 
            },
            { 
                Units.InchPerSecond, 
                new UnitPart[] 
                { 
                    new UnitPart(Units.Inch),
                    new UnitPart(Units.Second, -1)
                } 
            },
            { 
                Units.Knot, 
                new UnitPart[] 
                { 
                    new UnitPart(Units.NauticalMile),
                    new UnitPart(Units.Hour, -1)
                } 
            },
            { 
                Units.KilometrePerHour, 
                new UnitPart[] 
                { 
                    new UnitPart(Units.Metre, SIPrefixValues.Kilo),
                    new UnitPart(Units.Hour, -1)
                } 
            },
            { 
                Units.MilePerHour, 
                new UnitPart[] 
                { 
                    new UnitPart(Units.Mile),
                    new UnitPart(Units.Hour, -1)
                } 
            },
            { 
                Units.InchPerSquareSecond, 
                new UnitPart[] 
                { 
                    new UnitPart(Units.Inch),
                    new UnitPart(Units.Second, -2)
                } 
            },
            { 
                Units.WattHour, 
                new UnitPart[] 
                { 
                    new UnitPart(Units.Metre, 2),
                    new UnitPart(Units.Gram, SIPrefixValues.Kilo),
                    new UnitPart(Units.Second, -3),
                    new UnitPart(Units.Hour)
                } 
            },
            { 
                Units.TechnicalAtmosphere, 
                new UnitPart[] 
                { 
                    new UnitPart(Units.Kilopond),
                    new UnitPart(Units.Metre, SIPrefixValues.Centi, -2)
                } 
            },
            { 
                Units.PoundforcePerSquareInch, 
                new UnitPart[] 
                { 
                    new UnitPart(Units.PoundForce),
                    new UnitPart(Units.Inch, -2)
                } 
            },
            { 
                Units.PoundforcePerSquareFoot, 
                new UnitPart[] 
                { 
                    new UnitPart(Units.PoundForce),
                    new UnitPart(Units.Foot, -2)
                } 
            },
            { 
                Units.KipPerSquareInch, 
                new UnitPart[] 
                { 
                    new UnitPart(Units.Kip),
                    new UnitPart(Units.Inch, -2)
                } 
            },
            { 
                Units.Barye, 
                new UnitPart[] 
                { 
                    new UnitPart(Units.Dyne),
                    new UnitPart(Units.Metre, SIPrefixValues.Centi, -2)
                } 
            },
            { 
                Units.RevolutionPerMinute, 
                new UnitPart[] 
                { 
                    new UnitPart(Units.Revolution),
                    new UnitPart(Units.Minute, -1)
                } 
            },
            { 
                Units.SquareDegree, 
                new UnitPart[] 
                { 
                    new UnitPart(Units.Degree, 2)
                } 
            },
            { 
                Units.Talbot, 
                new UnitPart[] 
                { 
                    new UnitPart(Units.Lumen),
                    new UnitPart(Units.Second)
                } 
            },
            { 
                Units.Nit, 
                new UnitPart[] 
                { 
                    new UnitPart(Units.Candela),
                    new UnitPart(Units.Metre, -2)
                } 
            },
            { 
                Units.MilePerGallon, 
                new UnitPart[] 
                { 
                    new UnitPart(Units.Mile),
                    new UnitPart(Units.Gallon, -1)
                } 
            },
            { 
                Units.AmpereHour, 
                new UnitPart[] 
                { 
                    new UnitPart(Units.Ampere),
                    new UnitPart(Units.Hour)
                } 
            }
        };

        //Classifies all the basic units on account of their types and systems.
        private static Dictionary<UnitTypes, Dictionary<UnitSystems, BasicUnit>> AllBasicUnits =
        new Dictionary<UnitTypes, Dictionary<UnitSystems, BasicUnit>>()
        {
            {
                UnitTypes.Length, new Dictionary<UnitSystems, BasicUnit>()
                {
                    { UnitSystems.SI, new BasicUnit(Units.Metre) },
                    { UnitSystems.CGS, new BasicUnit(Units.Metre, SIPrefixValues.Centi) },
                    { UnitSystems.Imperial, new BasicUnit(Units.Foot) }                    
                }
            },
            {
                UnitTypes.Area, new Dictionary<UnitSystems, BasicUnit>()
                {
                    { UnitSystems.SI, new BasicUnit(Units.SquareMetre) },
                    { UnitSystems.CGS, new BasicUnit(Units.SquareCentimetre) },
                    { UnitSystems.Imperial, new BasicUnit(Units.SquareFoot) }                    
                }
            },
            {
                UnitTypes.Volume, new Dictionary<UnitSystems, BasicUnit>()
                {
                    { UnitSystems.SI, new BasicUnit(Units.CubicMetre) },
                    { UnitSystems.CGS, new BasicUnit(Units.CubicCentimetre) },
                    { UnitSystems.Imperial, new BasicUnit(Units.CubicFoot) }                    
                }
            },
            {
                UnitTypes.Mass, new Dictionary<UnitSystems, BasicUnit>()
                {
                    { UnitSystems.SI, new BasicUnit(Units.Gram, SIPrefixValues.Kilo) },
                    { UnitSystems.CGS, new BasicUnit(Units.Gram) },
                    { UnitSystems.Imperial, new BasicUnit(Units.Pound) }
                }
            },
            {
                UnitTypes.Time, new Dictionary<UnitSystems, BasicUnit>()
                {
                    { UnitSystems.SI, new BasicUnit(Units.Second) },
                    { UnitSystems.CGS, new BasicUnit(Units.Second) },
                    { UnitSystems.Imperial, new BasicUnit(Units.Second) }
                }
            },
            {
                UnitTypes.Force, new Dictionary<UnitSystems, BasicUnit>()
                {
                    { UnitSystems.SI, new BasicUnit(Units.Newton) },
                    { UnitSystems.CGS, new BasicUnit(Units.Dyne) },
                    { UnitSystems.Imperial, new BasicUnit(Units.PoundForce) }
                }
            },
            {
                UnitTypes.Energy, new Dictionary<UnitSystems, BasicUnit>()
                {
                    { UnitSystems.SI, new BasicUnit(Units.Joule) },
                    { UnitSystems.CGS, new BasicUnit(Units.Erg) },
                    { UnitSystems.Imperial, new BasicUnit(Units.BritishThermalUnit) }
                }
            },
            {
                UnitTypes.Power, new Dictionary<UnitSystems, BasicUnit>()
                {
                    { UnitSystems.SI, new BasicUnit(Units.Watt) },
                    { UnitSystems.CGS, new BasicUnit(Units.ErgPerSecond) },
                    { UnitSystems.Imperial, new BasicUnit(Units.Horsepower) }
                }
            },
            {
                UnitTypes.Temperature, new Dictionary<UnitSystems, BasicUnit>()
                {
                    { UnitSystems.SI, new BasicUnit(Units.Kelvin) },
                    { UnitSystems.Imperial, new BasicUnit(Units.Fahrenheit) },
                    { UnitSystems.CGS, new BasicUnit(Units.Kelvin) }
                }
            },
            {
                UnitTypes.Angle, new Dictionary<UnitSystems, BasicUnit>()
                {
                    { UnitSystems.SI, new BasicUnit(Units.Radian) },
                    { UnitSystems.CGS, new BasicUnit(Units.Radian) },
                    { UnitSystems.Imperial, new BasicUnit(Units.Radian) }
                }
            },
            {
                UnitTypes.SolidAngle, new Dictionary<UnitSystems, BasicUnit>()
                {
                    { UnitSystems.SI, new BasicUnit(Units.Steradian) },
                    { UnitSystems.CGS, new BasicUnit(Units.Steradian) },
                    { UnitSystems.Imperial, new BasicUnit(Units.Steradian) }
                }
            },
            {
                UnitTypes.ElectricCurrent, new Dictionary<UnitSystems, BasicUnit>()
                {
                    { UnitSystems.SI, new BasicUnit(Units.Ampere) },
                    { UnitSystems.CGS, new BasicUnit(Units.Ampere) },
                    { UnitSystems.Imperial, new BasicUnit(Units.Ampere) }
                }
            },
            {
                UnitTypes.LuminousIntensity, new Dictionary<UnitSystems, BasicUnit>()
                {
                    { UnitSystems.SI, new BasicUnit(Units.Candela) },
                    { UnitSystems.CGS, new BasicUnit(Units.Candela) },
                    { UnitSystems.Imperial, new BasicUnit(Units.Candela) }
                }
            },
            {
                UnitTypes.AmountOfSubstance, new Dictionary<UnitSystems, BasicUnit>()
                {
                    { UnitSystems.SI, new BasicUnit(Units.Mole) },
                    { UnitSystems.CGS, new BasicUnit(Units.Mole) },
                    { UnitSystems.Imperial, new BasicUnit(Units.Mole) }
                }
            },
            {
                UnitTypes.Information, new Dictionary<UnitSystems, BasicUnit>()
                {
                    { UnitSystems.SI, new BasicUnit(Units.Bit) },
                    { UnitSystems.CGS, new BasicUnit(Units.Bit) },
                    { UnitSystems.Imperial, new BasicUnit(Units.Bit) }
                }
            }
        };

        private class CompoundPart
        {
            public UnitTypes Type { get; set; }
            public int Exponent { get; set; }

            public CompoundPart(UnitTypes type, int exponent = 1)
            {
                Type = type;
                Exponent = exponent;
            }

            public static bool operator ==(CompoundPart first, CompoundPart second)
            {
                return NoNullEquals(first, second);
            }

            public static bool operator !=(CompoundPart first, CompoundPart second)
            {
                return !NoNullEquals(first, second);
            }

            public bool Equals(CompoundPart other)
            {
                return
                (
                    object.Equals(other, null) ?
                    false :
                    CompoundPartsAreEqual(this, other)
                );
            }

            public override bool Equals(object obj)
            {
                return Equals(obj as CompoundPart);
            }

            public override int GetHashCode() { return 0; }
        }

        private class BasicUnit
        {
            public Units Unit;
            public decimal PrefixFactor;

            public BasicUnit(Units unit, decimal prefixFactor = 1m)
            {
                Unit = unit;
                PrefixFactor = prefixFactor;
            }
        }

        private class Compound
        {
            public List<CompoundPart> Parts { get; set; }

            public Compound(List<CompoundPart> parts)
            {
                Parts = new List<CompoundPart>(parts);
            }
        }
    }
}
