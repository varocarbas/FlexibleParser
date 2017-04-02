using System;
using System.Collections.Generic;

namespace FlexibleParser
{
    ///<summary><para>Contains all the versions of the supported systems of units.</para></summary>
    public enum UnitSystems
    {
        ///<summary><para>Refers to units not belonging to any supported system of units.</para></summary>
        None = 0,
        ///<summary><para>Refers to units belonging to the International System of Units.</para></summary>
        SI,
        ///<summary><para>Refers to units belonging to the British Imperial System of Units.</para></summary>                                
        Imperial,
        ///<summary><para>Refers to units belonging to the U.S. Customary System of Units.</para></summary>                        
        USCS,
        ///<summary><para>Refers to units belonging to both Imperial and USC systems.</para></summary>                        
        ImperialAndUSCS,
        ///<summary>
        ///<para>Refers to units belonging to the Centimetre-gram-second System of Units.</para>
        ///<para>Includes all the electric subsystems (e.g., CGS-Gaussian, CGS-ESU or CGS-EMU).</para>
        ///</summary>     
        CGS
    };

    ///<summary><para>Contains all the supported mathematical constants.</para></summary>
    public class MathematicalConstants
    {
        ///<summary><para>Decimal version of Math.PI</para></summary>
        public const decimal Pi = 3.141592653589793238462643383m;
        ///<summary><para>Decimal version of Math.E</para></summary>
        public const decimal EulerNumber = 2.718281828459045235360287471m;
    }

    ///<summary>
    ///<para>Contains all the supported physical constants.</para>
    ///<para>Source: 2014 CODATA recommended values.</para>
    ///</summary>
    public class PhysicalConstants
    {
        ///<summary><para>Speed of light in vacuum (c). 299792458 m/s.</para></summary>
        public const decimal SpeedOfLight = 299792458m;
        ///<summary><para>Gravitational constant (G). 6.67408*10^-11 N*m^2/kg^2.</para></summary>
        public const decimal GravitationalConstant = 6.67408E-11m;
        ///<summary><para>Elementary charge (e). 1.6021766208*10^-19 C.</para></summary>
        public const decimal ElementaryCharge = 1.6021766208E-19m;
        ///<summary><para>Avogadro constant (N_A). 6.022140857*10^23 1/mol.</para></summary>
        public const decimal AvogadroConstant = 6.022140857E23m;
        ///<summary><para>Molar gas constant (R). 8.3144598 J/(K*mol).</para></summary>
        public const decimal GasConstant = 8.3144598m;
        ///<summary><para>Standard gravity acceleration (g). 9.80665 m/s^2.</para></summary>
        public const decimal GravityAcceleration = 9.80665m;
        ///<summary><para>Standard atmosphere (atm). 101325 Pa.</para></summary>
        public const decimal StandardAtmosphere = 101325m;
        ///<summary><para>Electronvolt (eV). 1.6021766208*10^−19 J.</para></summary>
        public const decimal Electronvolt = 1.6021766208E-19m;
        ///<summary><para>Atomic mass constant (m_u). 1.66053904*10^-27 kg.</para></summary>
        public const decimal AtomicMassConstant = 1.66053904E-27m;
    }
}
