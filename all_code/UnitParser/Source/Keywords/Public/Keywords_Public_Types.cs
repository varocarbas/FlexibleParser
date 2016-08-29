using System;
using System.Collections.Generic;

namespace FlexibleParser
{
    ///<summary><para>All the supported unit types.</para></summary>
    public enum UnitTypes
    {
        ///<summary><para>No unit type.</para></summary>
        None = 0,

        ///<summary><para>Associated with all the length units.</para></summary>
        Length,
        ///<summary><para>Associated with all the area units.</para></summary>
        Area,
        ///<summary><para>Associated with all the volume and capacity units.</para></summary>            
        Volume,
        ///<summary><para>Associated with all the time units.</para></summary>                       
        Time,
        ///<summary><para>Associated with all the velocity units.</para></summary>                                  
        Velocity,
        ///<summary><para>Associated with all the acceleration units.</para></summary>                                              
        Acceleration,
        ///<summary><para>Associated with all the mass units.</para></summary>                                            
        Mass,
        ///<summary><para>Associated with all the (plane) angle units.</para></summary>                                             
        Angle,
        ///<summary><para>Associated with all the information/data units.</para></summary>                                             
        Information,
        ///<summary><para>Associated with all the force/weight units.</para></summary>                                                        
        Force,
        ///<summary><para>Associated with all the energy units.</para></summary>                                                        
        Energy,
        ///<summary><para>Associated with all the power units.</para></summary>                                                     
        Power,
        ///<summary><para>Associated with all the pressure units.</para></summary>                                                  
        Pressure,
        ///<summary><para>Associated with all the frequency units.</para></summary>                                                       
        Frequency,
        ///<summary><para>Associated with all the electric charge units.</para></summary>                                                         
        ElectricCharge,
        ///<summary><para>Associated with all the electric voltage units.</para></summary>                                                     
        ElectricVoltage,
        ///<summary><para>Associated with all the electric current units.</para></summary>                                                               
        ElectricCurrent,
        ///<summary><para>Associated with all the electric resistance units.</para></summary>                                                                 
        ElectricResistance,
        ///<summary><para>Associated with all the electric resistivity units.</para></summary>                                                                 
        ElectricResistivity,             
        ///<summary><para>Associated with all the electric conductance units.</para></summary>                                                                
        ElectricConductance,
        ///<summary><para>Associated with all the electric conductivity units.</para></summary>                                                                 
        ElectricConductivity,  
        ///<summary><para>Associated with all the electric capacitance units.</para></summary>                                                                 
        ElectricCapacitance,
        ///<summary><para>Associated with all the electric inductance units.</para></summary>                                                                 
        ElectricInductance,
        ///<summary><para>Associated with all the electric dipole moment units.</para></summary>                                                                 
        ElectricDipoleMoment,
        ///<summary><para>Associated with all the temperature units.</para></summary>                                                                 
        Temperature,
        ///<summary><para>Associated with all the wavenumber units.</para></summary>                                                                 
        Wavenumber,
        ///<summary><para>Associated with all the (dynamic) viscosity units.</para></summary>                                                                 
        Viscosity,
        ///<summary><para>Associated with all the kinematic viscosity units.</para></summary>                                                                 
        KinematicViscosity,
        ///<summary><para>Associated with all the amount of substance units.</para></summary>                                                                 
        AmountOfSubstance,
        ///<summary><para>Associated with all the (linear) momentum units.</para></summary>                                                                 
        Momentum,
        ///<summary><para>Associated with all the angular velocity units.</para></summary>                                                                 
        AngularVelocity,
        ///<summary><para>Associated with all the angular acceleration units.</para></summary>                                                                 
        AngularAcceleration,
        ///<summary><para>Associated with all the angular momentum units.</para></summary>                                                                 
        AngularMomentum,
        ///<summary><para>Associated with all the moment of inertia units.</para></summary>                                                                 
        MomentOfInertia,
        ///<summary><para>Associated with all the solid angle units.</para></summary>                                                                 
        SolidAngle,
        ///<summary><para>Associated with all the luminous intensity units.</para></summary>                                                                 
        LuminousIntensity,
        ///<summary><para>Associated with all the luminous flux units.</para></summary>                                                                 
        LuminousFlux,
        ///<summary><para>Associated with all the luminous energy units.</para></summary>                                                                 
        LuminousEnergy,
        ///<summary><para>Associated with all the luminance units.</para></summary>  
        Luminance,
        ///<summary><para>Associated with all the illuminance units.</para></summary>  
        Illuminance,
        ///<summary><para>Associated with all the logarithmic units.</para></summary>  
        Logarithmic,
        ///<summary><para>Associated with all the magnetic flux units.</para></summary>  
        MagneticFlux,
        ///<summary><para>Associated with all the magnetic field B (or magnetic flux density) units.</para></summary>  
        MagneticFieldB,
        ///<summary><para>Associated with all the magnetic field H (or magnetic field strength) units.</para></summary>  
        MagneticFieldH,
        ///<summary><para>Associated with all the radioactivity (or radioactive decay) units.</para></summary>  
        Radioactivity,
        ///<summary><para>Associated with all the (ionising radiation) absorbed dose units.</para></summary>  
        AbsorbedDose,
        ///<summary><para>Associated with all the (ionising radiation) equivalent dose units.</para></summary>  
        EquivalentDose,
        ///<summary><para>Associated with all the catalytic activity units.</para></summary> 
        CatalyticActivity,
        ///<summary><para>Associated with all the catalytic activity concentration units.</para></summary> 
        CatalyticActivityConcentration,
        ///<summary><para>Associated with all the jerk units.</para></summary> 
        Jerk,
        ///<summary><para>Associated with all the mass flow rate units.</para></summary> 
        MassFlowRate,
        ///<summary><para>Associated with all the density units.</para></summary> 
        Density,
        ///<summary><para>Associated with all the area density units.</para></summary> 
        AreaDensity,
        ///<summary><para>Associated with all the energy density units.</para></summary> 
        EnergyDensity,
        ///<summary><para>Associated with all the specific energy units.</para></summary> 
        SpecificEnergy,
        ///<summary><para>Associated with all the specific volume units.</para></summary> 
        SpecificVolume,
        ///<summary><para>Associated with all the volumetric flow rate units.</para></summary> 
        VolumetricFlowRate,
        ///<summary><para>Associated with all the surface tension units.</para></summary> 
        SurfaceTension,
        ///<summary><para>Associated with all the specific weight units.</para></summary> 
        SpecificWeight,
        ///<summary><para>Associated with all the thermal conductivity units.</para></summary> 
        ThermalConductivity,
        ///<summary><para>Associated with all the thermal conductance units.</para></summary> 
        ThermalConductance,
        ///<summary><para>Associated with all the thermal resistivity units.</para></summary> 
        ThermalResistivity,
        ///<summary><para>Associated with all the thermal resistance units.</para></summary> 
        ThermalResistance,
        ///<summary><para>Associated with all the heat transfer coefficient units.</para></summary> 
        HeatTransferCoefficient,
        ///<summary><para>Associated with all the heat flux density units.</para></summary> 
        HeatFluxDensity,
        ///<summary><para>Associated with all the entropy units.</para></summary> 
        Entropy,
        ///<summary><para>Associated with all the electric field strength units.</para></summary> 
        ElectricFieldStrength,
        ///<summary><para>Associated with all the linear electric charge density units.</para></summary> 
        LinearElectricChargeDensity,
        ///<summary><para>Associated with all the surface electric charge density units.</para></summary> 
        SurfaceElectricChargeDensity,
        ///<summary><para>Associated with all the volume electric charge density units.</para></summary> 
        VolumeElectricChargeDensity,
        ///<summary><para>Associated with all the current density units.</para></summary> 
        CurrentDensity,
        ///<summary><para>Associated with all the permittivity units.</para></summary> 
        Permittivity,
        ///<summary><para>Associated with all the permeability units.</para></summary> 
        Permeability,
        ///<summary><para>Associated with all the molar concentration units.</para></summary> 
        MolarConcentration,
        ///<summary><para>Associated with all the molar energy units.</para></summary> 
        MolarEnergy,
        ///<summary><para>Associated with all the molar entropy units.</para></summary> 
        MolarEntropy,
        ///<summary><para>Associated with all the radiant intensity units.</para></summary> 
        RadiantIntensity,
        ///<summary><para>Associated with all the radiance units.</para></summary> 
        Radiance,
        ///<summary><para>Associated with all the fuel economy units.</para></summary> 
        FuelEconomy,
        ///<summary><para>Associated with all the sound exposure units.</para></summary> 
        SoundExposure,
        ///<summary><para>Associated with all the sound impedance units.</para></summary> 
        SoundImpedance,
        ///<summary><para>Associated with all the rotational stiffness units.</para></summary> 
        RotationalStiffness,
    };
}
