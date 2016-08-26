using System;
using System.Collections.Generic;

namespace FlexibleParser
{
    ///<summary><para>Conversion factors relating each unit with the reference for the given type.</para></summary>
    public class UnitConversionFactors
    {
        //--- Length
        ///<summary>
        ///<para>Metre (m) conversion factor. SI length unit.</para>
        ///<para>Reference point for all the length units.</para>
        ///</summary>
        public const decimal Metre = 1m;
        ///<summary><para>Centimetre (cm) conversion factor. CGS length unit.</para></summary>
        public const decimal Centimetre = 0.01m;
        ///<summary><para>Astronomical unit (AU) conversion factor. Length unit.</para></summary>
        public const decimal AstronomicalUnit = 149597870700m;
        ///<summary><para>Foot (ft) conversion factor. Imperial/USCS length unit.</para></summary>
        public const decimal Foot = 0.3048m;
        ///<summary><para>Inch (in) conversion factor. Imperial/USCS length unit.</para></summary>
        public const decimal Inch = 0.0254m;
        ///<summary><para>Yard (yd) conversion factor. Imperial/USCS length unit.</para></summary>
        public const decimal Yard = 0.9144m;
        ///<summary><para>International mile (mi) conversion factor. Imperial/USCS length unit.</para></summary>
        public const decimal Mile = 1609.344m;
        ///<summary><para>Nautical Mile (M) conversion factor. Length unit.</para></summary>
        public const decimal NauticalMile = 1852m;
        ///<summary><para>Thou (th) conversion factor. Imperial/USCS length unit.</para></summary>    
        public const decimal Thou = 0.0000254m;
        ///<summary><para>Fathom (ftm) conversion factor. Imperial/USCS length unit.</para></summary>
        public const decimal Fathom = 1.8288m;
        ///<summary><para>Rod (rd) conversion factor. Imperial/USCS length unit.</para></summary>
        public const decimal Rod = 5.0292m;
        ///<summary><para>Perch (perch) conversion factor. Imperial/USCS length unit.</para></summary>
        public const decimal Perch = 5.0292m;
        ///<summary><para>Pole (pole) conversion factor. Imperial/USCS length unit.</para></summary>
        public const decimal Pole = 5.0292m;
        ///<summary><para>Chain (ch) conversion factor. Imperial/USCS length unit.</para></summary>
        public const decimal Chain = 20.1168m;
        ///<summary><para>Furlong (fur) conversion factor. Imperial/USCS length unit.</para></summary>
        public const decimal Furlong = 201.168m;
        ///<summary><para>League (lea) conversion factor. Imperial/USCS length unit.</para></summary>
        public const decimal League = 4828.032m;
        ///<summary><para>International cable (cbl) conversion factor. Length unit.</para></summary>
        public const decimal Cable = 185.2m;
        ///<summary><para>Imperial cable (impcbl) conversion factor. Imperial length unit.</para></summary>
        public const decimal ImperialCable = 185.3184m;
        ///<summary><para>USCS cable (usccbl) conversion factor. USCS length unit.</para></summary>
        public const decimal USCSCable = 219.456m;
        ///<summary><para>Link (li) conversion factor. Imperial/USCS length unit.</para></summary>
        public const decimal Link = 0.201168m;
        ///<summary><para>Survey inch (surin) conversion factor. USCS length unit.</para></summary>
        public const decimal SurveyInch = 0.0254000508001016002032004064m;
        ///<summary><para>Survey foot (surft) conversion factor. USCS length unit.</para></summary>
        public const decimal SurveyFoot = 0.3048006096012192024384048768m;
        ///<summary><para>Survey yard (suryd) conversion factor. USCS length unit.</para></summary>
        public const decimal SurveyYard = 0.9144018288036576073152146304m;
        ///<summary><para>Survey rod (surrd) conversion factor. USCS length unit.</para></summary>
        public const decimal SurveyRod = 5.0292100584201168402336804672m;
        ///<summary><para>Survey chain (surch) conversion factor. USCS length unit.</para></summary>
        public const decimal SurveyChain = 20.116840233680467360934721869m;
        ///<summary><para>Survey mile (surmi) conversion factor. USCS length unit.</para></summary>
        public const decimal SurveyMile = 1609.3472186944373888747777495m;
        ///<summary><para>Ångström (Å) conversion factor. Length unit.</para></summary>
        public const decimal Angstrom = 1E-10m;
        ///<summary><para>Fermi (f) conversion factor. Length unit.</para></summary>
        public const decimal Fermi = 1E-15m;

        //--- Mass
        ///<summary>
        ///<para>Gram (g) conversion factor. SI mass unit.</para>
        ///<para>Reference point for all the mass units.</para>
        ///</summary>
        public const decimal Gram = 1m;
        ///<summary><para>Metric Ton (t) conversion factor. Mass unit.</para></summary>
        public const decimal MetricTon = 1000000m;
        ///<summary><para>Grain (gr) conversion factor. Imperial/USCS mass unit.</para></summary>
        public const decimal Grain = 0.06479891m;
        ///<summary><para>Drachm (dr) conversion factor. Imperial/USCS mass unit.</para></summary>
        public const decimal Drachm = 1.7718451953125m;
        ///<summary><para>Ounce (oz) conversion factor. Imperial/USCS mass unit.</para></summary>
        public const decimal Ounce = 28.349523125m;
        ///<summary><para>Pound (lb) conversion factor. Imperial/USCS mass unit.</para></summary>
        public const decimal Pound = 453.59237m;
        ///<summary><para>Stone (st) conversion factor. Imperial/USCS mass unit.</para></summary>
        public const decimal Stone = 6350.29318m;
        ///<summary><para>Slug (sl) conversion factor. Imperial/USCS mass unit.</para></summary>
        public const decimal Slug = 14593.9029372m;
        ///<summary><para>Long quarter (qr). Imperial mass unit.</para></summary>
        public const decimal Quarter = 12700.58636m;
        ///<summary><para>Long quarter (impqr) conversion factor. Imperial length unit.</para></summary>
        public const decimal LongQuarter = 12700.58636m;
        ///<summary><para>Short quarter (uscqr) conversion factor. USCS length unit.</para></summary>
        public const decimal ShortQuarter = 11339.80925m;
        ///<summary><para>Long Hundredweight (cwt) conversion factor. Imperial mass unit.</para></summary>
        public const decimal Hundredweight = 50802.34544m;
        ///<summary><para>Long Hundredweight (impcwt) conversion factor. Imperial mass unit.</para></summary>
        public const decimal LongHundredweight = 50802.34544m;
        ///<summary><para>Short Hundredweight (usccwt) conversion factor. USCS mass unit.</para></summary>
        public const decimal ShortHundredweight = 45359.237m;
        ///<summary><para>Long Ton (tn) conversion factor. Imperial mass unit.</para></summary>
        public const decimal Ton = 1016046.9088m;
        ///<summary><para>Long Ton (imptn) conversion factor. Imperial mass unit.</para></summary>
        public const decimal LongTon = 1016046.9088m;
        ///<summary><para>Short Ton (usctn) conversion factor. USCS mass unit.</para></summary>
        public const decimal ShortTon = 907184.74m;
        ///<summary><para>Carat (ct) conversion factor. Mass unit.</para></summary>  
        public const decimal Carat = 0.2m;
        ///<summary><para>Dalton (Da) conversion factor. Mass unit.</para></summary>  
        public const decimal Dalton = 1.66053904E-24m; //PhysicalConstants.AtomicMassConstant * 1000m;
        ///<summary><para>Unified atomic mass unit (u) conversion factor. Mass unit.</para></summary>  
        public const decimal UnifiedAtomicMassUnit = 1.66053904E-24m; //PhysicalConstants.AtomicMassConstant * 1000m;

        //--- Time
        ///<summary>
        ///<para>Second (s) conversion factor. SI time unit.</para>
        ///<para>Reference point for all the time units.</para>
        ///</summary>
        public const decimal Second = 1m;
        ///<summary><para>Minute (min) conversion factor. Time unit.</para></summary>
        public const decimal Minute = 60m;
        ///<summary><para>Hour (h) conversion factor. Time unit.</para></summary>
        public const decimal Hour = 3600m;
        ///<summary><para>Day (d) conversion factor. Time unit.</para></summary>
        public const decimal Day = 86400m;

        //--- Area
        ///<summary>
        ///<para>Square metre (m2) conversion factor. SI area unit.</para>
        ///<para>Reference point for all the area units.</para>
        ///</summary>
        public const decimal SquareMetre = 1m;
        ///<summary><para>Square centimetre (cm2) conversion factor. CGS area unit.</para></summary>
        public const decimal SquareCentimetre = 0.0001m;
        ///<summary><para>Are (a) conversion factor. Area unit.</para></summary>
        public const decimal Are = 100m;
        ///<summary><para>Square foot (ft2) conversion factor. Imperial/USCS area unit.</para></summary>
        public const decimal SquareFoot = 0.09290304m;
        ///<summary><para>Square inch (in2) conversion factor. Imperial/USCS area unit.</para></summary>
        public const decimal SquareInch = 0.00064516m;
        ///<summary><para>Square rod (rod2) conversion factor. Imperial/USCS area unit.</para></summary>
        public const decimal SquareRod = 25.29285264m;
        ///<summary><para>Square perch (perch2) conversion factor. Imperial/USCS area unit.</para></summary>
        public const decimal SquarePerch = 25.29285264m;
        ///<summary><para>Square pole (pole2) conversion factor. Imperial/USCS area unit.</para></summary>
        public const decimal SquarePole = 25.29285264m;
        ///<summary><para>Rood (rood) conversion factor. Imperial/USCS area unit.</para></summary>
        public const decimal Rood = 1011.7141056m;
        ///<summary><para>Acre (ac) conversion factor. Imperial/USCS area unit.</para></summary>
        public const decimal Acre = 4046.8564224m;
        ///<summary>
        ///<para>Barn (b) conversion factor. Area unit.</para>
        ///<para>The decimal type cannot deal directly with the actual conversion factor (1E-28m).</para>
        ///</summary>
        public const decimal Barn = -1m;

        //--- Volume
        ///<summary>
        ///<para>Cubic metre (m3) conversion factor. SI volume unit.</para>
        ///<para>Reference point for all the volume units.</para>
        ///</summary>
        public const decimal CubicMetre = 1m;
        ///<summary><para>Cubic centimetre (cm3) conversion factor. CGS Volume unit.</para></summary>
        public const decimal CubicCentimetre = 0.000001m;
        ///<summary><para>Litre (L) conversion factor. Volume unit.</para></summary>
        public const decimal Litre = 0.001m;
        ///<summary><para>Cubic foot (ft3) conversion factor. Imperial/USCS Volume unit.</para></summary>
        public const decimal CubicFoot = 0.028316846592m;
        ///<summary><para>Cubic inch (in3) conversion factor. Imperial/USCS Volume unit.</para></summary>
        public const decimal CubicInch = 0.000016387064m;
        ///<summary><para>Imperial fluid ounce (floz) conversion factor. Imperial volume unit.</para></summary>
        public const decimal FluidOunce = 0.0000284130625m;
        ///<summary><para>Imperial fluid ounce (impfloz) conversion factor. Imperial volume unit.</para></summary>
        public const decimal ImperialFluidOunce = 0.0000284130625m;
        ///<summary><para>USCS fluid ounce (uscfloz) conversion factor. USCS volume unit.</para></summary>
        public const decimal USCSFluidOunce = 0.0000295735295625m;
        ///<summary><para>Imperial gill (gi) conversion factor. Imperial volume unit.</para></summary>
        public const decimal Gill = 0.0001420653125m;
        ///<summary><para>Imperial gill (impgi) conversion factor. Imperial volume unit.</para></summary>
        public const decimal ImperialGill = 0.0001420653125m;
        ///<summary><para>USCS gill (uscgi) conversion factor. USCS volume unit.</para></summary>
        public const decimal USCSGill = 0.00011829411825m;
        ///<summary><para>Imperial pint (pt) conversion factor. Imperial volume unit.</para></summary>
        public const decimal Pint = 0.00056826125m;
        ///<summary><para>Imperial pint (imppt) conversion factor. Imperial volume unit.</para></summary>
        public const decimal ImperialPint = 0.00056826125m;
        ///<summary><para>Liquid pint (liquidpt) conversion factor. USCS volume unit.</para></summary>
        public const decimal LiquidPint = 0.000473176473m;
        ///<summary><para>Dry pint (drypt) conversion factor. USCS volume unit.</para></summary>
        public const decimal DryPint = 0.0005506104713575m;
        ///<summary><para>Imperial quart (qt) conversion factor. Imperial volume unit.</para></summary>
        public const decimal Quart = 0.0011365225m;
        ///<summary><para>Imperial quart (impqt) conversion factor. Imperial volume unit.</para></summary>
        public const decimal ImperialQuart = 0.0011365225m;
        ///<summary><para>Liquid quart (liquidqt) conversion factor. USCS volume unit.</para></summary>
        public const decimal LiquidQuart = 0.000946352946m;
        ///<summary><para>Dry quart (dryqt) conversion factor. USCS volume unit.</para></summary>
        public const decimal DryQuart = 0.001101220942715m;
        ///<summary><para>Imperial gallon (gal) conversion factor. Imperial volume unit.</para></summary>
        public const decimal Gallon = 0.00454609m;
        ///<summary><para>Imperial gallon (impgal) conversion factor. Imperial volume unit.</para></summary>
        public const decimal ImperialGallon = 0.00454609m;
        ///<summary><para>Liquid gallon (liquidgal) conversion factor. USCS volume unit.</para></summary>
        public const decimal LiquidGallon = 0.003785411784m;
        ///<summary><para>Dry gallon (drygal) conversion factor. USCS volume unit.</para></summary>
        public const decimal DryGallon = 0.00440488377086m;

        //--- Angle
        ///<summary>
        ///<para>Radian (rad) conversion factor. SI angle unit.</para>
        ///<para>Reference point for all the angle units.</para>
        ///</summary>
        public const decimal Radian = 1m;
        ///<summary><para>Degree (°) conversion factor. Angle unit.</para></summary>
        public const decimal Degree = 0.0174532925199432957692369077m; //MathematicalConstants.Pi / 180m;
        ///<summary><para>Revolution (rev) conversion factor. Angle unit.</para></summary>
        public const decimal Revolution = 6.283185307179586476925286766m; //2m * MathematicalConstants.Pi;
        ///<summary><para>Arcminute (') conversion factor. Angle unit.</para></summary>
        public const decimal Arcminute = 0.0002908882086657215961539485m; //MathematicalConstants.Pi / 10800m;
        ///<summary><para>Arcsecond ('') conversion factor. Angle unit.</para></summary>
        public const decimal Arcsecond = 0.0000048481368110953599358991m; //MathematicalConstants.Pi / 648000m;
        ///<summary><para>Gradian (grad) conversion factor. Angle unit.</para></summary>
        public const decimal Gradian = 0.0157079632679489661923132169m; //MathematicalConstants.Pi / 200m;
        ///<summary><para>Gon (gon) conversion factor. Angle unit.</para></summary>
        public const decimal Gon = 0.0157079632679489661923132169m; //MathematicalConstants.Pi / 200m;

        //--- Information
        ///<summary>
        ///<para>Bit (bit) conversion factor. Information unit.</para>
        ///<para>Reference point for all the information units.</para>
        ///</summary>
        public const decimal Bit = 1m;
        ///<summary><para>Nibble (nibble) conversion factor. Information unit.</para></summary>
        public const decimal Nibble = 4m;
        ///<summary><para>Quartet (quartet) conversion factor. Information unit.</para></summary>
        public const decimal Quartet = 4m;
        ///<summary><para>Byte (byte) conversion factor. Information unit.</para></summary>
        public const decimal Byte = 8m;
        ///<summary><para>Octet (octet) conversion factor. Information unit.</para></summary>
        public const decimal Octet = 8m;

        //--- Force
        ///<summary>
        ///<para>Newton (N) conversion factor. SI force unit.</para>
        ///<para>Reference point for all the force units.</para>
        ///</summary>
        public const decimal Newton = 1m;
        ///<summary><para>Kilopond (kp) conversion factor. Force unit.</para></summary>
        public const decimal Kilopond = 9.80665m; //PhysicalConstants.GravityAcceleration;
        ///<summary><para>Pound-force (lbf) conversion factor. Imperial/USCS force unit.</para></summary>
        public const decimal PoundForce = 4.4482216152605m;
        ///<summary><para>Kip (kip) conversion factor. Force unit.</para></summary>
        public const decimal Kip = 4448.2216152605m;
        ///<summary><para>Poundal (pdl) conversion factor. Imperial/USCS force unit.</para></summary>
        public const decimal Poundal = 0.138254954376m;
        ///<summary><para>Ounce-force (ozf) conversion factor. Imperial/USCS force unit.</para></summary>
        public const decimal OunceForce = 0.27801385095378125m;
        ///<summary><para>Dyne (dyn) conversion factor. Force unit.</para></summary>
        public const decimal Dyne = 1E-5m;

        //--- Velocity
        ///<summary>
        ///<para>Metre per second (m/s) conversion factor. SI velocity unit.</para>
        ///<para>Reference point for all the velocity units.</para>
        ///</summary>
        public const decimal MetrePerSecond = 1m;
        ///<summary><para>Centimetre per second (cm/s) conversion factor. CGS velocity unit.</para></summary>
        public const decimal CentimetrePerSecond = 0.01m;
        ///<summary><para>Foot per second (ft/s) conversion factor. Imperial/USCS velocity unit.</para></summary>
        public const decimal FootPerSecond = 0.3048m;
        ///<summary><para>Inch per second (in/s) conversion factor. Imperial/USCS velocity unit.</para></summary>
        public const decimal InchPerSecond = 0.0254m;
        ///<summary><para>Kilometre per hour (kph) conversion factor. Velocity unit.</para></summary>
        public const decimal KilometrePerHour = 0.2777777777777777777777777778m; //1000m / Hour;
        ///<summary><para>Knot (kn) conversion factor. Velocity unit.</para></summary>
        public const decimal Knot = 0.5144444444444444444444444445m; //1.852m * KilometrePerHour;
        ///<summary><para>Mile per hour (mph) conversion factor. Velocity unit.</para></summary>
        public const decimal MilePerHour = 0.44704m; //Mile / Hour;

        //--- Acceleration
        ///<summary>
        ///<para>Metre per square second (m/s2) conversion factor. SI acceleration unit.</para>
        ///<para>Reference point for all the acceleration units.</para>
        ///</summary>
        public const decimal MetrePerSquareSecond = 1m;
        ///<summary><para>Gal (Gal) conversion factor. CGS acceleration unit.</para></summary>
        public const decimal Gal = 0.01m;
        ///<summary><para>Foot per square second (ft/s2) conversion factor. Imperial/USCS acceleration unit.</para></summary>
        public const decimal FootPerSquareSecond = 0.3048m;
        ///<summary><para>Inch per square second (in/s2) conversion factor. Imperial/USCS acceleration unit.</para></summary>
        public const decimal InchPerSquareSecond = 0.0254m;

        //--- Energy
        ///<summary>
        ///<para>Joule (J) conversion factor. SI energy unit.</para>
        ///<para>Reference point for all the energy units.</para>
        ///</summary>
        public const decimal Joule = 1m;
        ///<summary><para>Electronvolt (eV) conversion factor. Energy unit.</para></summary>
        public const decimal Electronvolt = 1.6021766208E-19m; //PhysicalConstants.Electronvolt;
        ///<summary><para>Watt hour (Wh) conversion factor. Energy unit.</para></summary>
        public const decimal WattHour = 3600;
        ///<summary><para>IT calorie (cal) conversion factor. Energy unit.</para></summary>
        public const decimal Calorie = 4.1868m;
        ///<summary><para>Thermochemical calorie (thcal) conversion factor. Energy unit.</para></summary>
        public const decimal ThermochemicalCalorie = 4.184m;
        ///<summary><para>Food calorie (kcal) conversion factor. Energy unit.</para></summary>
        public const decimal FoodCalorie = 4186.8m;
        ///<summary><para>IT British thermal unit (BTU) conversion factor. Imperial/USCS energy unit.</para></summary>
        public const decimal BritishThermalUnit = 1055.05585262m;
        ///<summary><para>Thermochemical British thermal unit (thBTU) conversion factor. Imperial/USCS energy unit.</para></summary>
        public const decimal ThermochemicalBritishThermalUnit = 1054.3502644888888888888888889m; //BritishThermalUnit * ThermochemicalCalorie / Calorie;            
        ///<summary><para>Erg (erg) conversion factor. CGS Energy unit.</para></summary>
        public const decimal Erg = 1E-7m;
        ///<summary><para>EC therm (thm) conversion factor. Energy unit.</para></summary>              
        public const decimal Therm = 105505600;
        ///<summary><para>UK therm (ukthm) conversion factor. Energy unit.</para></summary>              
        public const decimal UKTherm = 105505585.257348m;
        ///<summary><para>US therm (usthm) conversion factor. Energy unit.</para></summary>              
        public const decimal USTherm = 105480400m;

        //--- Power
        ///<summary>
        ///<para>Watt (W) conversion factor. SI power unit.</para>
        ///<para>Reference point for all the power units.</para>
        ///</summary>
        public const decimal Watt = 1m;
        ///<summary><para>Erg per second (erg/s) conversion factor. CGS power unit.</para></summary>
        public const decimal ErgPerSecond = 1E-7m;
        ///<summary><para>Mechanical horsepower (hp) conversion factor. Imperial/USCS power unit.</para></summary>
        public const decimal Horsepower = 745.69987158227m;
        ///<summary><para>Metric horsepower (hpM) conversion factor. Power unit.</para></summary>
        public const decimal MetricHorsepower = 735.49875m;
        ///<summary><para>Boiler horsepower (hpS) conversion factor. Power unit.</para></summary>
        public const decimal BoilerHorsepower = 9809.5m;
        ///<summary><para>Electric horsepower (hpE) conversion factor. Power unit.</para></summary>
        public const decimal ElectricHorsepower = 746m;
        ///<summary><para>Ton of Refrigeration (TR) conversion factor. Power unit.</para></summary>
        public const decimal TonOfRefrigeration = 3516.8528420667m;

        //--- Pressure
        ///<summary>
        ///<para>Pascal (Pa) conversion factor. SI pressure unit.</para>
        ///<para>Reference point for all the pressure units.</para>
        ///</summary>
        public const decimal Pascal = 1m;
        ///<summary><para>Atmosphere (atm) conversion factor. Pressure unit.</para></summary>
        public const decimal Atmosphere = 101325m; //PhysicalConstants.StandardAtmosphere;
        ///<summary><para>Bar (bar) conversion factor. Pressure unit.</para></summary>
        public const decimal Bar = 100000;
        ///<summary><para>Pound-force per square inch (psi) conversion factor. Imperial/USCS pressure unit.</para></summary>
        public const decimal PoundforcePerSquareInch = 6894.7572931683613367226734453m; //UnitConversionFactors.PoundForce / UnitConversionFactors.Inch / UnitConversionFactors.Inch;
        ///<summary><para>Millimetre of Mercury (mmHg) conversion factor. Pressure unit.</para></summary>
        public const decimal MillimetreMercury = 133.322387415m;
        ///<summary><para>Inch of Mercury at 32 °F (inHg32) conversion factor. Pressure unit.</para></summary>
        public const decimal InchMercury32F = 3386.38m;
        ///<summary><para>Inch of Mercury at 60 °F (inHg60) conversion factor. Pressure unit.</para></summary>
        public const decimal InchMercury60F = 3376.85m;
        ///<summary><para>Barye (Ba) conversion factor. CGS pressure unit.</para></summary>
        public const decimal Barye = 0.1m;
        ///<summary><para>Torr (Torr) conversion factor. Pressure unit.</para></summary>
        public const decimal Torr = 133.32236842105263157894736842m; //PhysicalConstants.StandardAtmosphere / 760m;
        ///<summary><para>Kip per square inch (ksi) conversion factor. Pressure unit.</para></summary>
        public const decimal KipPerSquareInch = 6894757.2931683613367226734453m;

        //--- Frequency
        ///<summary>
        ///<para>Hertz (Hz) conversion factor. SI frequency unit.</para>
        ///<para>Reference point for all the frequency units.</para>
        ///</summary>
        public const decimal Hertz = 1m;
        ///<summary><para>Revolutions per minute (rpm) conversion factor. Frequency unit.</para></summary>
        public const decimal RevolutionsPerMinute = 1m / 60m;
        ///<summary><para>Cycles per Second (cps) conversion factor. Frequency unit.</para></summary>
        public const decimal CyclesPerSecond = 1m;

        //--- Electric Charge
        ///<summary>
        ///<para>Coulomb (C) conversion factor. SI electric charge unit.</para>
        ///<para>Reference point for all the electric charge units.</para>
        ///</summary>
        public const decimal Coulomb = 1m;
        ///<summary><para>Franklin (Fr) conversion ratio. CGS-Gaussian/CGS-ESU electric charge unit.</para></summary>
        public const decimal Franklin = 3.335640951982E-10m;
        ///<summary><para>Statcoulomb (statC) conversion ratio. CGS-Gaussian/CGS-ESU electric charge unit.</para></summary>
        public const decimal Statcoulomb = 3.335640951982E-10m;
        ///<summary><para>Electrostatic unit of charge (ESUcha) conversion ratio. CGS-Gaussian/CGS-ESU electric charge unit.</para></summary>
        public const decimal ESUOfCharge = 3.335640951982E-10m;
        ///<summary><para>Abcoulomb (abC) conversion ratio. CGS-EMU electric charge unit.</para></summary>
        public const decimal Abcoulomb = 10m;
        ///<summary><para>Electromagnetic unit of charge (EMUcha) conversion ratio. CGS-EMU electric charge unit.</para></summary>
        public const decimal EMUOfCharge = 10m;

        //--- Electric Current
        ///<summary>
        ///<para>Ampere (A) conversion factor. SI electric current unit.</para>
        ///<para>Reference point for all the electric current units.</para>
        ///</summary>
        public const decimal Ampere = 1m;
        ///<summary><para>Statampere (statA) conversion factor. CGS-Gaussian/CGS-ESU electric current unit.</para></summary>
        public const decimal Statampere = 3.335640951982E-10m;
        ///<summary><para>Electrostatic unit of current (ESUcur) conversion factor. CGS-Gaussian/CGS-ESU electric current unit.</para></summary>
        public const decimal ESUOfCurrent = 3.335640951982E-10m;
        ///<summary><para>Abampere (abA) conversion factor. CGS-EMU electric current unit.</para></summary>
        public const decimal Abampere = 3.335640951982E-10m;
        ///<summary><para>Biot (Bi) conversion factor. CGS-EMU electric current unit.</para></summary>
        public const decimal Biot = 10m;
        ///<summary><para>Electromagnetic unit of current (EMUcur) conversion factor. CGS-EMU electric current unit.</para></summary>
        public const decimal EMUOfCurrent = 10m;

        //--- Electric Voltage
        ///<summary>
        ///<para>Volt (V) conversion factor. SI electric voltage unit.</para>
        ///<para>Reference point for all the electric voltage units.</para>
        ///</summary>
        public const decimal Volt = 1m;
        ///<summary><para>Statvolt (statV) conversion factor. CGS-Gaussian/CGS-ESU voltage unit.</para></summary>
        public const decimal Statvolt = 299.792458m; //PhysicalConstants.SpeedOfLight / 1E6m;
        ///<summary><para>Electrostatic unit of electric potential (ESUpot) conversion factor. CGS-Gaussian/CGS-ESU voltage unit.</para></summary>
        public const decimal ESUOfElectricPotential = 299.792458m; //PhysicalConstants.SpeedOfLight / 1E6m;
        ///<summary><para>Abvolt (abV) conversion factor. CGS-EMU voltage unit.</para></summary>
        public const decimal Abvolt = 1E-8m;
        ///<summary><para>Electromagnetic unit of electric potential (EMUpot) conversion factor. CGS-EMU voltage unit.</para></summary>
        public const decimal EMUOfElectricPotential = 1E-8m;

        //--- Electric Resistance
        ///<summary>
        ///<para>Ohm (Ω) conversion factor. SI electric resistance unit.</para>
        ///<para>Reference point for all the electric resistance units.</para>
        ///</summary>
        public const decimal Ohm = 1m;
        ///<summary><para>Statohm (statΩ) conversion factor. CGS-Gaussian/CGS-ESU electric resistance unit.</para></summary>
        public const decimal Statohm = 8.987551787E11m;
        ///<summary><para>Electrostatic unit of resistance (ESUres) conversion factor. CGS-Gaussian/CGS-ESU electric resistance unit.</para></summary>
        public const decimal ESUOfResistance = 8.987551787E11m;
        ///<summary><para>Abohm (abΩ) conversion factor. CGS-EMU electric resistance unit.</para></summary>
        public const decimal Abohm = 1E-9m;
        ///<summary><para>Electromagnetic unit of resistance (EMUres) conversion factor. CGS-EMU electric resistance unit.</para></summary>
        public const decimal EMUOfResistance = 1E-9m;

        //--- Electric Conductance
        ///<summary>
        ///<para>Siemens (S) conversion factor. SI electric conductance unit.</para>
        ///<para>Reference point for all the electric conductance units.</para>
        ///</summary>
        public const decimal Siemens = 1m;
        ///<summary><para>Mho (℧) conversion factor. SI electric conductance unit.</para></summary>
        public const decimal Mho = 1m;
        ///<summary><para>Gemmho (gemmho) conversion factor. SI electric conductance unit.</para><para>Reference point for all the electric conductance units.</para></summary>
        public const decimal Gemmho = 0.000001m;
        ///<summary><para>Statsiemens (statS) conversion factor. CGS-Gaussian/CGS-ESU electric conductance unit.</para></summary>
        public const decimal Statsiemens = 1.1126500560536184E-12m; //1E5m / PhysicalConstants.SpeedOfLight / PhysicalConstants.SpeedOfLight
        ///<summary><para>Statmho (stat℧) conversion factor. CGS-Gaussian/CGS-ESU electric conductance unit.</para></summary>
        public const decimal Statmho = 1.1126500560536184E-12m; //1E5m / PhysicalConstants.SpeedOfLight / PhysicalConstants.SpeedOfLight
        ///<summary><para>Absiemens (abS) conversion factor. CGS-EMU electric conductance unit.</para></summary>
        public const decimal Absiemens = 1E9m;
        ///<summary><para>Abmho (ab℧) conversion factor. CGS-EMU electric conductance unit.</para></summary>
        public const decimal Abmho = 1E9m;

        //--- Electric Capacitance
        ///<summary><para>Farad (F) conversion factor. SI electric capacitance unit.</para><para>Reference point for all the electric capacitance units.</para></summary>
        public const decimal Farad = 1m;
        ///<summary><para>Statfarad (statF) conversion factor. CGS-Gaussian/CGS-ESU electric capacitance unit.</para></summary>
        public const decimal Statfarad = 1.1126500560536184E-12m; //1E5m / PhysicalConstants.SpeedOfLight / PhysicalConstants.SpeedOfLight
        ///<summary><para>Electrostatic unit of capacitance (ESUcap) conversion factor. CGS-Gaussian/CGS-ESU electric capacitance unit.</para></summary>
        public const decimal ESUOfCapacitance = 1.1126500560536184E-12m; //1E5m / PhysicalConstants.SpeedOfLight / PhysicalConstants.SpeedOfLight
        ///<summary><para>Abfarad (abF) conversion factor. CGS-EMU electric capacitance unit.</para></summary>
        public const decimal Abfarad = 1E9m;
        ///<summary><para>Electromagnetic unit of capacitance (EMUcap) conversion factor. CGS-EMU electric resistance unit.</para></summary>
        public const decimal EMUOfCapacitance = 1E9m;

        //--- Electric Inductance
        ///<summary><para>Henry (H) conversion factor. SI electric inductance unit.</para><para>Reference point for all the electric inductance units.</para></summary>
        public const decimal Henry = 1m;
        ///<summary><para>Stathenry (statH). CGS-Gaussian/CGS-ESU electric inductance unit.</para></summary>
        public const decimal Stathenry = 8.987551787E11m;
        ///<summary><para>Electrostatic unit of inductance (ESUind) conversion factor. CGS-Gaussian/CGS-ESU electric inductance unit.</para></summary>
        public const decimal ESUOfInductance = 8.987551787E11m;
        ///<summary><para>Abhenry (abH). CGS-EMU electric inductance unit.</para></summary>
        public const decimal Abhenry = 1e-9m;
        ///<summary><para>Electromagnetic unit of inductance (EMUind) conversion factor. CGS-EMU electric inductance unit.</para></summary>
        public const decimal EMUOfInductance = 1E-9m;

        //--- Electric Dipole Moment
        ///<summary>
        ///<para>Coulomb Metre (C*m) conversion factor. SI electric dipole unit.</para>
        ///<para>Reference point for all the electric dipole moment units.</para>
        ///</summary>   
        public const decimal CoulombMetre = 1m;
        ///<summary>
        ///<para>Debye (D). CGS-Gaussian electric dipole moment unit.</para>
        ///<para>The decimal type cannot deal directly with the actual conversion factor (3.33564095E-30m).</para>  
        ///</summary>
        public const decimal Debye = -2m;

        //--- Wavenumber
        ///<summary>
        ///<para>Reciprocal metre (1/m) conversion factor. SI wavenumber unit.</para>
        ///<para>Reference point for all the wavenumber units.</para>
        ///</summary>   
        public const decimal ReciprocalMetre = 1m;
        ///<summary><para>Kayser (kayser) conversion factor. CGS wavenumber unit.</para></summary>
        public const decimal Kayser = 100m;

        //--- Viscosity
        ///<summary>
        ///<para>Pascal second (Pa*s) conversion factor. SI viscosity unit.</para>
        ///<para>Reference point for all the viscosity units.</para>
        ///</summary>   
        public const decimal PascalSecond = 1m;
        ///<summary><para>Poise (P) conversion factor. CGS viscosity unit.</para></summary>
        public const decimal Poise = 0.1m;

        //--- Kinematic Viscosity
        ///<summary>
        ///<para>Square metre per second (m2/s) conversion factor. SI kinematic viscosity unit.</para>
        ///<para>Reference point for all the kinematic viscosity units.</para>
        ///</summary>   
        public const decimal SquareMetrePerSecond = 1m;
        ///<summary><para>Stokes (St) conversion factor. CGS kinematic viscosity unit.</para></summary>
        public const decimal Stokes = 0.0001m;

        //--- Amount of Substance
        ///<summary>
        ///<para>Mole (mol) conversion factor. SI amount of substance unit.</para>
        ///<para>Reference point for all the amount of substance units.</para>
        ///</summary>   
        public const decimal Mole = 1m;
        ///<summary><para>Pound-mole (lbmol) conversion factor. Amount of substance unit.</para></summary>
        public const decimal PoundMole = 453.59237m;

        //--- Momentum
        ///<summary>
        ///<para>Newton second (N*s) conversion factor.</para>
        ///<para>Reference point for all the momentum units.</para>
        ///</summary>    
        public const decimal NewtonSecond = 1m;

        //--- Angular Velocity
        ///<summary>
        ///<para>Radian per second (rad/s) conversion factor.</para>
        ///<para>Reference point for all the angular velocity units.</para>
        ///</summary>  
        public const decimal RadianPerSecond = 1m;

        //--- Angular Acceleration
        ///<summary>
        ///<para>Radian per square second (rad/s2) conversion factor.</para>
        ///<para>Reference point for all the angular acceleration units.</para>
        ///</summary>  
        public const decimal RadianPerSquareSecond = 1m;

        //--- Angular Momentum
        ///<summary>
        ///<para>Joule second (J*s) conversion factor.</para>
        ///<para>Reference point for all the angular momentum units.</para>
        ///</summary>  
        public const decimal JouleSecond = 1m;

        //--- Moment of Inertia
        ///<summary>
        ///<para>Kilogram square metre (kg*m2) conversion factor.</para>
        ///<para>Reference point for all the moment of inertia units.</para>
        ///</summary>  
        public const decimal KilogramSquareMetre = 1m;

        //--- Solid Angle
        ///<summary>
        ///<para>Steradian (sr) conversion factor. SI solid angle unit.</para>
        ///<para>Reference point for all the solid angle units.</para>
        ///</summary>                 
        public const decimal Steradian = 1m;
        ///<summary><para>Square degree (deg2) conversion factor. Solid angle unit.</para></summary>                 
        public const decimal SquareDegree = 0.0003046174197867085993467435m; //MathematicalConstants.Pi * MathematicalConstants.Pi / 32400;

        //--- Luminous Intensity
        ///<summary>
        ///<para>Candela (cd) conversion factor. SI luminous intensity unit.</para>
        ///<para>Reference point for all the luminous intensity units.</para>
        ///</summary>                 
        public const decimal Candela = 1m;

        //--- Luminous Flux
        ///<summary>
        ///<para>Lumen (lm) conversion factor. SI luminous flux unit.</para>
        ///<para>Reference point for all the luminous flux units.</para>
        ///</summary>                 
        public const decimal Lumen = 1m;

        //--- Luminous Energy
        ///<summary>
        ///<para>Lumen second (lm*s) conversion factor. Luminous energy unit.</para>
        ///<para>Reference point for all the luminous energy units.</para>
        ///</summary>                 
        public const decimal LumenSecond = 1m;
        ///<summary>
        ///<para>Talbot (talbot) conversion factor. Luminous energy unit.</para>
        ///<para>Reference point for all the luminous energy units.</para>
        ///</summary>                 
        public const decimal Talbot = 1m;

        //--- Luminance
        ///<summary>
        ///<para>Candela per square metre (cd/m2) conversion factor.</para>
        ///<para>Reference point for all the luminance units.</para>
        ///</summary>                 
        public const decimal CandelaPerSquareMetre = 1m;
        ///<summary><para>Nit (nt) conversion factor.</para></summary>                 
        public const decimal Nit = 1m;
        ///<summary><para>Stilb (sb) conversion factor.</para></summary>                 
        public const decimal Stilb = 1E4m;

        //--- Illuminance
        ///<summary>
        ///<para>Lux (lx) conversion factor. SI illuminance unit.</para>
        ///<para>Reference point for all the illuminance units.</para>
        ///</summary>                 
        public const decimal Lux = 1m;
        ///<summary><para>Phot (ph) conversion factor. CGS illuminance unit.</para></summary>                 
        public const decimal Phot = 1E4m;

        //--- Logarithmic
        ///<summary>
        ///<para>Bel (B) conversion factor. Logarithmic unit.</para>
        ///<para>Reference point for all the logarithmic units.</para>
        ///</summary>                        
        public const decimal Bel = 1m;
        ///<summary><para>Neper (Np) conversion factor. Logarithmic unit.</para></summary>      
        public const decimal Neper = 0.8685889638065035m; //20.0 / Math.Log(10)

        //--- Magnetic Flux
        ///<summary>
        ///<para>Weber (Wb) conversion factor. SI magnetic flux unit.</para>
        ///<para>Reference point for all the magnetic flux units.</para>
        ///</summary>
        public const decimal Weber = 1m;
        ///<summary><para>Maxwell (Mx).</para><para>CGS-Gaussian/CGS-EMU magnetic flux unit.</para></summary>                         
        public const decimal Maxwell = 1E-8m;

        //--- Magnetic Field B
        ///<summary>
        ///<para>Tesla (T) conversion factor. SI magnetic field B unit.</para>
        ///<para>Reference point for all the magnetic field B units.</para>
        ///</summary>
        public const decimal Tesla = 1m;
        ///<summary><para>Gauss (G). CGS-Gaussian/CGS-EMU magnetic field B unit.</para></summary>                         
        public const decimal Gauss = 1E-4m;

        //--- Magnetic Field H
        ///<summary>
        ///<para>Ampere per metre (A/m) conversion factor. SI magnetic field H unit.</para>
        ///<para>Reference point for all the magnetic field H units.</para>
        ///</summary> 
        public const decimal AmperePerMetre = 1m;
        ///<summary><para>Oersted (Oe) conversion factor. CGS-Gaussian/CGS-EMU magnetic field H unit.</para></summary>                         
        public const decimal Oersted = 79.57747154594m;

        //--- Radioactivity
        ///<summary>
        ///<para>Becquerel (Bq) conversion factor. SI radioactivity unit.</para>
        ///<para>Reference point for all the radioactivity units.</para>
        ///</summary>
        public const decimal Becquerel = 1m;
        ///<summary><para>Curie (Ci) conversion factor. Radioactivity unit.</para></summary> 
        public const decimal Curie = 3.7E10m;
        ///<summary><para>Disintegrations per second (dps) conversion factor. Radioactivity unit.</para></summary> 
        public const decimal DisintegrationsPerSecond = 1m;
        ///<summary><para>Disintegrations per minute (dpm) conversion factor. Radioactivity unit.</para></summary> 
        public const decimal DisintegrationsPerMinute = 0.0166666666666666666666666667m;
        ///<summary><para>Rutherford (Rd) conversion factor. Radioactivity unit.</para></summary> 
        public const decimal Rutherford = 1E6m;

        //--- Absorbed Dose
        ///<summary>
        ///<para>Gray (Gy) conversion factor. SI absorbed dose unit.</para>
        ///<para>Reference point for all the absorbed dose units.</para>
        ///</summary> 
        public const decimal Gray = 1m;
        ///<summary><para>Rad (Rad) conversion factor. CGS absorbed dose unit.</para></summary> 
        public const decimal Rad = 0.01m;

        //--- Equivalent Dose
        ///<summary>
        ///<para>Sievert (Sv) conversion factor. SI equivalent dose unit.</para>
        ///<para>Reference point for all the equivalent dose units.</para>
        ///</summary> 
        public const decimal Sievert = 1m;
        ///<summary><para>Roentgen equivalent in man (rem). CGS equivalent dose unit.</para></summary> 
        public const decimal REM = 0.01m;

        //--- Catalytic Activity
        ///<summary>
        ///<para>Katal (kat) conversion factor. SI catalytic activity unit.</para>
        ///<para>Reference point for all the catalytic activity units.</para>
        ///</summary> 
        public const decimal Katal = 1m;

        //--- Jerk
        ///<summary>
        ///<para>Metre per cubic second (m/s3). SI jerk unit.</para>
        ///<para>Reference point for all the jerk units.</para>
        ///</summary> 
        public const decimal MetrePerCubicSecond = 1m;

        //--- Mass Flow Rate
        ///<summary>
        ///<para>Kilogram per second (kg/s). SI mass flow rate unit.</para>
        ///<para>Reference point for all the mass flow rate units.</para>          
        ///</summary> 
        public const decimal KilogramPerSecond = 1m;

        //--- Density
        ///<summary>
        ///<para>Kilogram per cubic metre (kg/m3). SI density unit.</para>
        ///<para>Reference point for all the density units.</para>            
        ///</summary> 
        public const decimal KilogramPerCubicMetre = 1m;

        //--- Specific Volume
        ///<summary>
        ///<para>Cubic metre per kilogram (m3/kg). SI specific volume unit.</para>
        ///<para>Reference point for all the specific volume units.</para>            
        ///</summary> 
        public const decimal CubicMetrePerKilogram = 1m;

        //--- Volumetric Flow Rate
        ///<summary>
        ///<para>Cubic metre per second (m3/s). SI volumetric flow rate unit.</para>
        ///<para>Reference point for all the volumetric flow rate units.</para>                
        ///</summary> 
        public const decimal CubicMetrePerSecond = 1m;

        //--- Surface Tension
        ///<summary>
        ///<para>Joule per square metre (J/m2). SI surface tension unit.</para>
        ///<para>Reference point for all the surface tension units.</para>            
        ///</summary> 
        public const decimal JoulePerSquareMetre = 1m;

        //--- Specific Weight
        ///<summary>
        ///<para>Newton per cubic metre (N/m3). SI specific weight unit.</para>
        ///<para>Reference point for all the specific weight units.</para>            
        ///</summary> 
        public const decimal NewtonPerCubicMetre = 1m;

        //--- Thermal Conductivity
        ///<summary>
        ///<para>Watt per metre per kelvin (W/m*K). SI thermal conductivity unit.</para>
        ///<para>Reference point for all the thermal conductivity units.</para>            
        ///</summary> 
        public const decimal WattPerMetrePerKelvin = 1m;

        //--- Thermal Conductance
        ///<summary>
        ///<para>Watt per kelvin (W/K). SI thermal conductance unit.</para>
        ///<para>Reference point for all the thermal conductance units.</para>            
        ///</summary> 
        public const decimal WattPerKelvin = 1m;

        //--- Thermal Resistivity
        ///<summary>
        ///<para>Metre kelvin per watt (m*K/W). SI thermal resistivity unit.</para>
        ///<para>Reference point for all the thermal resistivity units.</para>             
        ///</summary> 
        public const decimal MetreKelvinPerWatt = 1m;

        //--- Thermal Resistance
        ///<summary>
        ///<para>Kelvin per watt (K/W). SI thermal resistance unit.</para>
        ///<para>Reference point for all the thermal resistance units.</para>             
        ///</summary> 
        public const decimal KelvinPerWatt = 1m;

        //--- Heat Transfer Coefficient
        ///<summary>
        ///<para>Watt per square metre per kelvin (W/m2*K). SI heat transfer coefficient unit.</para>
        ///<para>Reference point for all the heat transfer coefficient units.</para>             
        ///</summary> 
        public const decimal WattPerSquareMetrePerKelvin = 1m;

        //--- Heat Flux Density
        ///<summary>
        ///<para>Watt per square metre (W/m2). SI heat flux density unit.</para>
        ///<para>Reference point for all the heat flux density units.</para>             
        ///</summary> 
        public const decimal WattPerSquareMetre = 1m;

        //--- Entropy
        ///<summary>
        ///<para>Joule per kelvin (J/K). SI entropy unit.</para>
        ///<para>Reference point for all the entropy units.</para>  
        /// </summary> 
        public const decimal JoulePerKelvin = 1m;

        //--- Electric Field Strength
        ///<summary>
        ///<para>Newton per coulomb (N/C). SI Electric Field Strength unit.</para>
        ///<para>Reference point for all the Electric Field Strength units.</para>              
        ///</summary> 
        public const decimal NewtonPerCoulomb = 1m;

        //--- Linear Electric Charge Density
        ///<summary>
        ///<para>Coulomb per metre (C/m). SI linear electric charge density unit.</para>
        ///<para>Reference point for all the linear electric charge units.</para>              
        ///</summary> 
        public const decimal CoulombPerMetre = 1m;

        //--- Surface Electric Charge Density
        ///<summary>
        ///<para>Coulomb per square metre (C/m2). SI surface electric charge density unit.</para>
        ///<para>Reference point for all the surface electric charge units.</para>              
        ///</summary> 
        public const decimal CoulombPerSquareMetre = 1m;

        //--- Volume Electric Charge Density
        ///<summary>
        ///<para>Coulomb per cubic metre (C/m3). SI volume electric charge density unit.</para>
        ///<para>Reference point for all the volume electric charge density units.</para>              
        ///</summary> 
        public const decimal CoulombPerCubicMetre = 1m;

        //--- Permittivity
        ///<summary>
        ///<para>Farad per metre (F/m). SI permittivity unit.</para>
        ///<para>Reference point for all the permittivity units.</para>              
        ///</summary> 
        public const decimal FaradPerMetre = 1m;

        //--- Permeability
        ///<summary>
        ///<para>Henry per metre (H/m). SI permeability unit.</para>
        ///<para>Reference point for all the permeability units.</para>              
        ///</summary> 
        public const decimal HenryPerMetre = 1m;

        //--- Molar Energy
        ///<summary>
        ///<para>Joule per mole (J/mol). SI molar energy unit.</para>
        ///<para>Reference point for all the molar energy units.</para>              
        ///</summary> 
        public const decimal JoulePerMole = 1m;

        //--- Molar Entropy
        ///<summary>
        ///<para>Joule per mole per kelvin (J/mol*K). SI molar entropy unit.</para>
        ///<para>Reference point for all the molar entropy units.</para>              
        ///</summary> 
        public const decimal JoulePerMolePerKelvin = 1m;

        //--- Radiant Intensity
        ///<summary>
        ///<para>Watt per steradian (W/sr). SI radiant intensity unit.</para>
        ///<para>Reference point for all the radiant intensity units.</para>              
        ///</summary> 
        public const decimal WattPerSteradian = 1m;

        //--- Radiance
        ///<summary>
        ///<para>Watt per steradian per square metre (W/sr*m2). SI radiance unit.</para>
        ///<para>Reference point for all the radiance units.</para>              
        ///</summary> 
        public const decimal WattPerSteradianPerSquareMetre = 1m;
    }
}
