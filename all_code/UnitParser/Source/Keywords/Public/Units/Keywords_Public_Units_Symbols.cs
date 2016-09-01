using System;
using System.Collections.Generic;

namespace FlexibleParser
{
    ///<summary>
    ///<para>Contains the primary symbols, abbreviations and string representations of all the supported units.</para>
    ///<para>All these strings are case sensitive.</para>
    ///</summary>
    public class UnitSymbols
    {
        //--- Length
        ///<summary><para>Metre symbol. SI length unit.</para></summary>  
        public const string Metre = "m";
        ///<summary><para>Centimetre symbol. CGS length unit.</para></summary>  
        public const string Centimetre = "cm";
        ///<summary><para>Astronomical unit symbol. Length unit.</para></summary>           
        public const string AstronomicalUnit = "AU";
        ///<summary><para>Inch symbol. Imperial/USCS length unit.</para></summary>             
        public const string Inch = "in";
        ///<summary><para>Foot symbol. Imperial/USCS length unit.</para></summary>             
        public const string Foot = "ft";
        ///<summary><para>Yard symbol. Imperial/USCS length unit.</para></summary>               
        public const string Yard = "yd";
        ///<summary><para>International mile symbol. Imperial/USCS length unit.</para></summary>                          
        public const string Mile = "mi";
        ///<summary>
        ///<para>Nautical mile symbol. Length unit.</para>
        ///<para>The "nm" alternative cannot be supported due to its incompatibility with the length unit nanometre.</para>
        ///</summary>                   
        public const string NauticalMile = "M";
        ///<summary><para>Thou symbol. Imperial/USCS length unit.</para></summary>                  
        public const string Thou = "th";
        ///<summary><para>Fathom symbol. Imperial/USCS length unit.</para></summary>
        public const string Fathom = "ftm";
        ///<summary><para>Rod symbol. Imperial/USCS length unit.</para></summary>
        public const string Rod = "rd";
        ///<summary><para>Perch string representation. Imperial/USCS length unit.</para></summary>
        public const string Perch = "perch";
        ///<summary><para>Pole string representation. Imperial/USCS length unit.</para></summary>
        public const string Pole = "pole";
        ///<summary><para>Chain symbol. Imperial/USCS length unit.</para></summary>            
        public const string Chain = "ch";
        ///<summary><para>Furlong symbol. Imperial/USCS length unit.</par></summary>
        public const string Furlong = "fur";
        ///<summary><para>League symbol. Imperial/USCS length unit.</para></summary>
        public const string League = "lea";
        ///<summary><para>International cable symbol. Length unit.</para></summary>
        public const string Cable = "cbl";
        ///<summary><para>Imperial cable abbreviation. Length unit.</para></summary>
        public const string ImperialCable = "impcbl";
        ///<summary><para>USCS cable abbreviation. Length unit.</para></summary>
        public const string USCSCable = "usccbl";
        ///<summary><para>Link symbol. Imperial/USCS length unit.</para></summary>
        public const string Link = "li";
        ///<summary><para>Survey inch abbreviation. USCS length unit.</para></summary>
        public const string SurveyInch = "surin";
        ///<summary><para>Survey foot abbreviation. USCS length unit.</para></summary>
        public const string SurveyFoot = "surft";
        ///<summary><para>Survey yard abbreviation. USCS length unit.</para></summary>
        public const string SurveyYard = "suryd";
        ///<summary><para>Survey rod abbreviation. USCS length unit.</para></summary>
        public const string SurveyRod = "surrd";
        ///<summary><para>Survey chain abbreviation. USCS length unit.</para></summary>
        public const string SurveyChain = "surch";
        ///<summary><para>Survey mile abbreviation. USCS length unit.</para></summary>
        public const string SurveyMile = "surmi";
        ///<summary><para>Ångström symbol. Length unit.</para></summary>
        public const string Angstrom = "Å";
        ///<summary><para>Fermi symbol. Length unit.</para></summary>
        public const string Fermi = "f";
        ///<summary><para>Light symbol. Length unit.</para></summary>
        public const string LightYear = "ly";
        ///<summary><para>Parsec. Length unit.</para></summary>
        public const string Parsec = "pc";

        //--- Mass
        ///<summary><para>Gram symbol. SI mass unit.</para></summary>
        public const string Gram = "g";
        ///<summary><para>Metric ton symbol. Mass unit.</para></summary>
        public const string MetricTon = "t";
        ///<summary><para>Grain symbol. Imperial/USCS mass unit.</para></summary>
        public const string Grain = "gr";
        ///<summary><para>Drachm symbol. Imperial/USCS mass unit.</para></summary>
        public const string Drachm = "dr";
        ///<summary><para>Ounce symbol. Imperial/USCS mass unit.</para></summary>            
        public const string Ounce = "oz";
        ///<summary><para>Pound symbol. Imperial/USCS mass unit.</para>/</summary>
        public const string Pound = "lb";
        ///<summary><para>Stone symbol. Imperial/USCS mass unit.</para></summary>            
        public const string Stone = "st";
        ///<summary><para>Slug symbol. Imperial/USCS mass unit.</para></summary>                      
        public const string Slug = "sl";
        ///<summary><para>Long quarter symbol. Imperial mass unit.</para></summary>          
        public const string Quarter = "qr";
        ///<summary><para>Long quarter abbreviation. Imperial mass unit.</para></summary>         
        public const string LongQuarter = "impqr";
        ///<summary><para>Short quarter abbreviation. USCS mass unit.</para></summary>           
        public const string ShortQuarter = "uscqr";
        ///<summary><para>Imperial hundredweight symbol. Imperial mass unit.</para></summary>          
        public const string Hundredweight = "cwt";
        ///<summary><para>Long hundredweight abbreviation. Imperial mass unit.</para></summary>  
        public const string LongHundredweight = "impcwt";
        ///<summary><para>Short hundredweight abbreviation. USCS mass unit.</para></summary>
        public const string ShortHundredweight = "usccwt";
        ///<summary><para>Long ton symbol. Imperial mass unit.</para></summary>           
        public const string Ton = "tn";
        ///<summary><para>Long ton abbreviation. Imperial mass unit.</para></summary>           
        public const string LongTon = "imptn";
        ///<summary><para>Short ton abbreviation. USCS mass unit.</para></summary>   
        public const string ShortTon = "usctn";
        ///<summary><para>Carat symbol. Mass unit.</para></summary>  
        public const string Carat = "ct";
        ///<summary><para>Dalton symbol. Mass unit.</para></summary>  
        public const string Dalton = "Da";
        ///<summary><para>Unified atomic mass unit symbol. Mass unit.</para></summary>  
        public const string UnifiedAtomicMassUnit = "u";

        //--- Time
        ///<summary><para>Second symbol. SI time unit.</para></summary>  
        public const string Second = "s";
        ///<summary>
        ///<para>Minute. Time unit.</para>
        ///<para>The "m" alternative cannot be supported due to its incompatibility with the length unit metre.</para>
        ///</summary>  
        public const string Minute = "min";
        ///<summary><para>Hour symbol. Time unit.</para></summary>  
        public const string Hour = "h";
        ///<summary><para>Day symbol. Time unit.</para></summary>  
        public const string Day = "d";

        //--- Area
        ///<summary><para>Square metre symbol. SI area unit.</para></summary>  
        public const string SquareMetre = "m2";
        ///<summary><para>Square centimetre symbol. CGS area unit.</para></summary>  
        public const string SquareCentimetre = "cm2";
        ///<summary><para>Are symbol. Area unit.</para></summary>  
        public const string Are = "a";
        ///<summary><para>Square foot symbol. Imperial/USCS area unit.</para></summary>  
        public const string SquareFoot = "ft2";
        ///<summary><para>Square inch symbol. Imperial/USCS area unit.</para></summary>  
        public const string SquareInch = "in2";
        ///<summary><para>Square rod symbol. Imperial/USCS area unit.</para></summary>  
        public const string SquareRod = "rd2";
        ///<summary><para>Square perch string representation. Imperial/USCS area unit.</para></summary>  
        public const string SquarePerch = "perch2";
        ///<summary><para>Square pole string representation. Imperial/USCS area unit.</para></summary>  
        public const string SquarePole = "pole2";
        ///<summary><para>Rood astring representation. Imperial/USCS area unit.</para></summary>  
        public const string Rood = "rood";
        ///<summary><para>Acre symbol. Imperial/USCS area unit.</para></summary>  
        public const string Acre = "ac";
        ///<summary><para>Barn symbol. Area unit.</para></summary>  
        public const string Barn = "b";

        //--- Volume
        ///<summary><para>Cubic metre symbol. SI volume unit.</para></summary>  
        public const string CubicMetre = "m3";
        ///<summary><para>Cubic centimetre abbreviation. CGS volume unit.</para></summary>  
        public const string CubicCentimetre = "cc";
        ///<summary><para>Litre symbol. Volume unit.</para></summary>  
        public const string Litre = "L";
        ///<summary><para>Cubic foot symbol. Imperial/USCS volume unit.</para></summary>  
        public const string CubicFoot = "ft3";
        ///<summary><para>Cubic inch symbol. Imperial/USCS volume unit.</para></summary>  
        public const string CubicInch = "in3";
        ///<summary><para>Imperial fluid ounce symbol. Imperial volume unit.</para></summary>  
        public const string FluidOunce = "floz";
        ///<summary><para>Imperial fluid ounce abbreviation. Imperial volume unit.</para></summary>  
        public const string ImperialFluidOunce = "impfloz";
        ///<summary><para>USCS fluid ounce abbreviation. USCS volume unit.</para></summary>  
        public const string USCSFluidOunce = "uscfloz";
        ///<summary><para>Imperial gill symbol. Imperial volume unit.</para></summary>            
        public const string Gill = "gi";
        ///<summary><para>Imperial gill abbreviation. Imperial volume unit.</para></summary>            
        public const string ImperialGill = "impgi";
        ///<summary><para>USCS gill abbreviation. USCS volume unit.</para></summary>  
        public const string USCSGill = "uscgi";
        ///<summary><para>Imperial pint symbol. Imperial volume unit.</para></summary>             
        public const string Pint = "pt";
        ///<summary><para>Imperial pint abbreviation. Imperial volume unit.</para></summary>             
        public const string ImperialPint = "imppt";
        ///<summary><para>Liquid pint abbreviation. USCS volume unit.</para></summary>
        public const string LiquidPint = "liquidpt";
        ///<summary><para>Dry pint abbreviation. USCS volume unit.</para></summary>
        public const string DryPint = "drypt";
        ///<summary><para>Imperial quart symbol. Imperial volume unit.</para>/</summary>            
        public const string Quart = "qt";
        ///<summary><para>Imperial quart abbreviation. Imperial volume unit.</para>/</summary>            
        public const string ImperialQuart = "impqt";
        ///<summary><para>Liquid quart abbreviation. USCS volume unit.</para></summary>                        
        public const string LiquidQuart = "liquidqt";
        ///<summary><para>Dry quart abbreviation. USCS volume unit.</para></summary>            
        public const string DryQuart = "dryqt";
        ///<summary><para>Imperial gallon symbol. Imperial volume unit.</para></summary>             
        public const string Gallon = "gal";
        ///<summary><para>Imperial gallon abbreviation. Imperial volume unit.</para></summary>             
        public const string ImperialGallon = "impgal";
        ///<summary><para>Liquid gallon abbreviation. USCS volume unit.</para></summary>     
        public const string LiquidGallon = "liquidgal";
        ///<summary><para>Dry gallon abbreviation. USCS volume unit.</para></summary>           
        public const string DryGallon = "drygal";

        //--- Angle
        ///<summary><para>Radian symbol. SI angle unit.</para></summary> 
        public const string Radian = "rad";
        ///<summary><para>Degree symbol. Angle unit.</para></summary> 
        public const string Degree = "°";
        ///<summary><para>Arcminute symbol. Angle unit.</para></summary>             
        public const string Arcminute = "'";
        ///<summary><para>Arcsecond symbol. Angle unit.</para></summary>                         
        public const string Arcsecond = "''";
        ///<summary><para>Revolution abbreviation. Angle unit.</para></summary>             
        public const string Revolution = "rev";
        ///<summary><para>Gradian symbol. Angle unit.</para></summary>             
        public const string Gradian = "grad";
        ///<summary><para>Gon symbol. Angle unit.</para></summary>             
        public const string Gon = "gon";

        //--- Information
        ///<summary>
        ///<para>Bit string representation. Information unit.</para>
        ///<para>The "b" alternative cannot be supported due to its incompatibility with the area unit barn.</para>
        ///</summary>                         
        public const string Bit = "bit";
        ///<summary>
        ///<para>Byte string representation. Information unit.</para>
        ///<para>The "B" alternative cannot be supported due to its incompatibility with the logarithmic unit bel.</para>
        ///</summary>  
        public const string Byte = "byte";
        ///<summary><para>Nibble string representation. Information unit.</para></summary>  
        public const string Nibble = "nibble";
        ///<summary><para>Quartet string representation. Information unit.</para></summary>            
        public const string Quartet = "quartet";
        ///<summary><para>Octet string representation. Information unit.</para></summary>             
        public const string Octet = "octet";

        //--- Force
        ///<summary><para>Newton symbol. SI force unit.</para></summary>             
        public const string Newton = "N";
        ///<summary><para>Kilopond symbol. Force unit.</para></summary>   
        public const string Kilopond = "kp";
        ///<summary><para>Pound-force symbol. Imperial/USCS force unit.</para></summary>               
        public const string PoundForce = "lbf";
        ///<summary><para>Kip symbol. Force unit.</para></summary>               
        public const string Kip = "kip";
        ///<summary><para>Poundal symbol. Imperial/USCS force unit.</para></summary>                
        public const string Poundal = "pdl";
        ///<summary><para>Ounce-force symbol. Imperial/USCS force unit.</para></summary>                
        public const string OunceForce = "ozf";
        ///<summary><para>Dyne symbol. CGS Force unit.</para></summary>              
        public const string Dyne = "dyn";

        //--- Velocity
        ///<summary><para>Metre per second symbol. SI velocity unit.</para></summary>  
        public const string MetrePerSecond = "m/s";
        ///<summary><para>Centimetre per second symbol. CGS velocity unit.</para></summary>  
        public const string CentimetrePerSecond = "cm/s";
        ///<summary><para>Foot per second symbol. Imperial/USCS velocity unit.</para></summary>  
        public const string FootPerSecond = "ft/s";
        ///<summary><para>Inch per second symbol. Imperial/USCS velocity unit.</para></summary>  
        public const string InchPerSecond = "in/s";
        ///<summary><para>Knot symbol. Velocity unit.</para></summary>
        public const string Knot = "kn";
        ///<summary><para>Kilometre per hour abbreviation. Velocity unit.</para></summary>
        public const string KilometrePerHour = "kph";
        ///<summary><para>Mile per hour abbreviation. Velocity unit.</para></summary>
        public const string MilePerHour = "mph";

        //--- Acceleration
        ///<summary><para>Metre per square second symbol. SI acceleration unit.</para></summary>  
        public const string MetrePerSquareSecond = "m/s2";
        ///<summary><para>Gal symbol. CGS acceleration unit.</para></summary>
        public const string Gal = "Gal";
        ///<summary><para>Foot per square second symbol. Imperial/USCS acceleration unit.</para></summary>  
        public const string FootPerSquareSecond = "ft/s2";
        ///<summary><para>Inch per square second symbol. Imperial/USCS acceleration unit.</para></summary>  
        public const string InchPerSquareSecond = "in/s2";

        //--- Energy
        ///<summary><para>Joule symbol. SI energy unit.</para></summary>   
        public const string Joule = "J";
        ///<summary><para>Electronvolt symbol. Energy unit.</para></summary>            
        public const string Electronvolt = "eV";
        ///<summary><para>Watt hour abbreviation. Energy unit.</para></summary>   
        public const string WattHour = "Wh";
        ///<summary><para>IT British thermal unit symbol. Imperial/USCS energy unit.</para></summary>                
        public const string BritishThermalUnit = "BTU";
        ///<summary><para>Thermochemical British thermal unit abbreviation. Imperial/USCS energy unit.</para></summary>                
        public const string ThermochemicalBritishThermalUnit = "thBTU";
        ///<summary><para>IT calorie symbol. Energy unit.</para>/</summary>              
        public const string Calorie = "cal";
        ///<summary><para>Thermochemical calorie abbreviation. Energy unit.</para></summary>              
        public const string ThermochemicalCalorie = "thcal";
        ///<summary><para>Food calorie symbol. Energy unit.</para></summary>              
        public const string FoodCalorie = "kcal";
        ///<summary><para>Erg symbol. CGS energy unit.</para></summary>              
        public const string Erg = "erg";
        ///<summary><para>EC therm symbol. Energy unit.</para></summary>              
        public const string Therm = "thm";
        ///<summary><para>UK therm abbreviation. Energy unit.</para></summary>              
        public const string UKTherm = "ukthm";
        ///<summary><para>US therm abbreviation. Energy unit.</para></summary>              
        public const string USTherm = "usthm";

        //--- Power
        ///<summary><para>Watt symbol. SI power unit.</para></summary>              
        public const string Watt = "W";
        ///<summary><para>Erg per second symbol. CGS power unit.</para></summary>              
        public const string ErgPerSecond = "erg/s";
        ///<summary><para>Mechanical horsepower symbol. Imperial/USCS power unit.</para></summary>              
        public const string Horsepower = "hp";
        ///<summary><para>Metric horsepower abbreviation. Power unit.</para></summary>    
        public const string MetricHorsepower = "hpM";
        ///<summary>/<para>Boiler horsepower abbreviation. Power unit.</para></summary>    
        public const string BoilerHorsepower = "hpS";
        ///<summary><para>Electric horsepower abbreviation. Power unit.</para></summary>    
        public const string ElectricHorsepower = "hpE";
        ///<summary><para>Ton of refrigeration abbreviation. Power unit.</para></summary>              
        public const string TonOfRefrigeration = "TR";

        //--- Pressure
        ///<summary><para>Pascal symbol. SI pressure unit.</para></summary>    
        public const string Pascal = "Pa";
        ///<summary><para>Atmosphere symbol. Pressure unit.</para></summary>            
        public const string Atmosphere = "atm";
        ///<summary><para>Bar symbol. Pressure unit.</para></summary>            
        public const string Bar = "bar";
        ///<summary><para>Pound-force per square inch abbreviation. Imperial/USCS pressure unit.</para></summary>                 
        public const string PoundforcePerSquareInch = "psi";
        ///<summary><para>Pound-force per square foot abbreviation. Imperial/USCS pressure unit.</para></summary>                 
        public const string PoundforcePerSquareFoot = "psf";
        ///<summary><para>Millimetre of mercury symbol. Pressure unit.</para></summary>               
        public const string MillimetreMercury = "mmHg";
        ///<summary><para>Inch of mercury 32 °F abbreviation. Pressure unit.</para></summary>               
        public const string InchMercury32F = "inHg32";
        ///<summary><para>Inch of mercury 60 °F abbreviation. Pressure unit.</para></summary>               
        public const string InchMercury60F = "inHg60";
        ///<summary><para>Barye symbol. CGS pressure unit.</para></summary>               
        public const string Barye = "Ba";
        ///<summary><para>Torr symbol. Pressure unit.</para></summary>               
        public const string Torr = "Torr";
        ///<summary><para>Kip per square inch abbreviation. Pressure unit.</para></summary>               
        public const string KipPerSquareInch = "ksi";

        //--- Frequency
        ///<summary><para>Hertz symbol. SI frequency unit.</para></summary>    
        public const string Hertz = "Hz";
        ///<summary><para>Revolutions per minute abbreviation. Frequency unit.</para></summary>             
        public const string RevolutionsPerMinute = "rpm";
        ///<summary><para>Cycles per second abbreviation. Frequency unit.</para></summary>                
        public const string CyclesPerSecond = "cps";

        //--- Electric Charge
        ///<summary>/<para>Coulomb symbol. SI electric charge unit.</para></summary>
        public const string Coulomb = "C";
        ///<summary><para>Franklin symbol. CGS-Gaussian/CGS-ESU electric charge unit.</para></summary>
        public const string Franklin = "Fr";
        ///<summary><para>Statcoulomb symbol. CGS-Gaussian/CGS-ESU electric charge unit.</para></summary>
        public const string Statcoulomb = "statC";
        ///<summary><para>Electrostatic unit of charge abbreviation. CGS-Gaussian/CGS-ESU electric charge unit.</para></summary>
        public const string ESUOfCharge = "ESUcha";
        ///<summary>
        ///<para>Abcoulomb symbol. CGS-EMU electric charge unit.</para>
        ///<para>The "aC" alternative cannot be supported due to its incompatibility with the electric charge unit attocoulomb.</para>            
        ///</summary>
        public const string Abcoulomb = "abC";
        ///<summary><para>Electromagnetic unit of charge abbreviation. CGS-EMU electric charge unit.</para></summary>
        public const string EMUOfCharge = "EMUcha";

        //--- Electric Current
        ///<summary><para>Ampere symbol. SI electric current unit.</para></summary>
        public const string Ampere = "A";
        ///<summary><para>Statampere symbol. CGS-Gaussian/CGS-ESU electric current unit.</para></summary>
        public const string Statampere = "statA";
        ///<summary><para>Electrostatic unit of current abbreviation. CGS-Gaussian/CGS-ESU electric current unit.</para></summary>
        public const string ESUOfCurrent = "ESUcur";
        ///<summary>
        ///<para>Abampere symbol. CGS-EMU electric current unit.</para>
        ///<para>The "aA" alternative cannot be supported due to its incompatibility with the electric current unit attoampere.</para>            
        ///</summary>
        public const string Abampere = "abA";
        ///<summary><para>Electromagnetic unit of current abbreviation. CGS-EMU electric current unit.</para></summary>
        public const string EMUOfCurrent = "EMUcur";
        ///<summary><para>Biot symbol. CGS-EMU electric current unit.</para></summary>
        public const string Biot = "Bi";

        //--- Electric Voltage
        ///<summary><para>Volt symbol. SI electric voltage unit.</para></summary>
        public const string Volt = "V";
        ///<summary><para>Electrostatic unit of electric potential abbreviation. CGS-Gaussian/CGS-ESU electric voltage unit.</para></summary>
        public const string ESUOfElectricPotential = "ESUpot";
        ///<summary><para>Statvolt symbol. CGS-Gaussian/CGS-ESU electric voltage unit.</para></summary>
        public const string Statvolt = "statV";
        ///<summary><para>Electromagnetic unit of electric potential abbreviation. CGS-EMU electric voltage unit.</para></summary>
        public const string EMUOfElectricPotential = "EMUpot";
        ///<summary>
        ///<para>Abvolt symbol. CGS-EMU electric voltage unit.</para>
        ///<para>The "aV" alternative cannot be supported due to its incompatibility with the electric voltage unit attovolt.</para>            
        ///</summary>
        public const string Abvolt = "abV";

        //--- Electric Resistance 
        ///<summary><para>Ohm symbol. SI electric resistance unit.</para></summary>
        public const string Ohm = "Ω";
        ///<summary><para>Statohm symbol. CGS-Gaussian/CGS-ESU electric resistance unit.</para></summary>
        public const string Statohm = "statΩ";
        ///<summary><para>Electrostatic unit of resistance abbreviation. CGS-Gaussian/CGS-ESU electric resistance unit.</para></summary>
        public const string ESUOfResistance = "ESUres";
        ///<summary>
        ///<para>Abohm symbol. CGS-EMU electric resistance unit.</para>
        ///<para>The "aΩ" alternative cannot be supported due to its incompatibility with the electric resistance unit attoohm.</para>            
        ///</summary>
        public const string Abohm = "abΩ";
        ///<summary><para>Electromagnetic unit of resistance abbreviation. CGS-EMU electric resistance unit.</para></summary>
        public const string EMUOfResistance = "EMUres";

        //--- Electric Resistivity 
        ///<summary><para>Ohm metre symbol. SI electric resistivity unit.</para></summary>
        public const string OhmMetre = "Ω*m";

        //--- Electric Conductance
        ///<summary><para>Siemens symbol. SI electric conductance unit.</para></summary>
        public const string Siemens = "S";
        ///<summary><para>Mho symbol. SI electric conductance unit.</para></summary>
        public const string Mho = "℧";
        ///<summary><para>Gemmho string representation. Electric conductance unit.</para></summary>
        public const string Gemmho = "gemmho";
        ///<summary><para>Statsiemens symbol. CGS-Gaussian/CGS-ESU electric resistance unit.</para></summary>
        public const string Statsiemens = "statS";
        ///<summary><para>Statmho symbol. CGS-Gaussian/CGS-ESU electric resistance unit.</para></summary>
        public const string Statmho = "stat℧";
        ///<summary>
        ///<para>Absiemens symbol. CGS-EMU electric resistance unit.</para>
        ///<para>The "aS" alternative cannot be supported due to its incompatibility with the electric conductance unit attosiemens.</para>            
        ///</summary>
        public const string Absiemens = "abS";
        ///<summary>
        ///<para>Abmho symbol. CGS-EMU electric resistance unit.</para>
        ///<para>The "a℧" alternative cannot be supported due to its incompatibility with the electric conductance unit attomho.</para>            
        ///</summary>
        public const string Abmho = "ab℧";

        //--- Electric Conductivity
        ///<summary><para>Siemens per metre symbol. SI electric conductivity unit.</para></summary>
        public const string SiemensPerMetre = "S/m";
        
        //--- Electric Capacitance
        ///<summary><para>Farad symbol. SI electric capacitance unit.</para></summary>
        public const string Farad = "F";
        ///<summary><para>Statfarad symbol. CGS-Gaussian/CGS-ESU electric capacitance unit.</para></summary>
        public const string Statfarad = "statF";
        ///<summary><para>Electrostatic unit of capacitance abbreviation. CGS-Gaussian/CGS-ESU electric capacitance unit.</para></summary>
        public const string ESUOfCapacitance = "ESUcap";
        ///<summary>
        ///<para>Abfarad symbol. CGS-EMU electric capacitance unit.</para>
        ///<para>The "aF" alternative cannot be supported due to its incompatibility with the electric capacitance unit attofarad.</para>            
        ///</summary>
        public const string Abfarad = "abF";
        ///<summary><para>Electromagnetic unit of capacitance abbreviation. CGS-EMU electric capacitance unit.</para></summary>
        public const string EMUOfCapacitance = "EMUcap";

        //--- Electric Inductance
        ///<summary><para>Henry symbol. SI electric inductance unit.</para></summary>
        public const string Henry = "H";
        ///<summary><para>Stathenry symbol. CGS-Gaussian/CGS-ESU electric inductance unit.</para></summary>
        public const string Stathenry = "statH";
        ///<summary><para>Electrostatic unit of inductance abbreviation. CGS-Gaussian/CGS-ESU electric inductance unit.</para></summary>
        public const string ESUOfInductance = "ESUind";
        ///<summary>
        ///<para>Abhenry symbol. CGS-EMU electric inductance unit.</para>
        ///<para>The "aH" alternative cannot be supported due to its incompatibility with the electric inductance unit attohenry.</para>            
        ///</summary>
        public const string Abhenry = "abH";
        ///<summary><para>Electromagnetic unit of inductance abbreviation. CGS-EMU electric inductance unit.</para></summary>
        public const string EMUOfInductance = "EMUind";

        //--- Electric Dipole Moment
        ///<summary><para>Coulomb metre symbol. SI electric dipole moment unit.</para></summary>  
        public const string CoulombMetre = "C*m";
        ///<summary><para>Debye symbol. CGS-Gaussian electric dipole moment unit.</para></summary>
        public const string Debye = "D";

        //--- Temperature
        ///<summary><para>Kelvin symbol. SI temperature unit.</para></summary>
        public const string Kelvin = "K";
        ///<summary><para>Degree Celsius symbol. SI temperature unit.</para></summary>
        public const string Celsius = "°C";
        ///<summary><para>Degree Fahrenheit symbol. Imperial/USCS temperature unit.</para></summary>            
        public const string Fahrenheit = "°F";
        ///<summary><para>Degree Rankine symbol. Imperial/USCS temperature unit.</para></summary>   
        public const string Rankine = "°R";

        //--- Wavenumber
        ///<summary><para>Reciprocal metre symbol. SI wavenumber unit.</para></summary>  
        public const string ReciprocalMetre = "1/m";
        ///<summary>
        ///<para>Kayser string representation. CGS wavenumber unit.</para>
        ///<para>The "K" alternative cannot be supported due to its incompatibility with the temperature unit kelvin.</para>            
        ///</summary>
        public const string Kayser = "kayser";

        //--- Viscosity
        ///<summary><para>Pascal second symbol. SI viscosity unit.</para></summary>  
        public const string PascalSecond = "Pa*s";
        ///<summary><para>Poise symbol. CGS viscosity unit.</para></summary>
        public const string Poise = "P";

        //--- Kinematic Viscosity
        ///<summary><para>Square metre per second symbol. SI kinematic viscosity unit.</para></summary>  
        public const string SquareMetrePerSecond = "m2/s";
        ///<summary><para>Stokes symbol. CGS kinematic viscosity unit.</para></summary>
        public const string Stokes = "St";

        //--- Amount of Substance
        ///<summary><para>Mole symbol. SI amount of substance unit.</para></summary>                 
        public const string Mole = "mol";
        ///<summary><para>Pound-mole abbreviation. Amount of substance unit.</para></summary>
        public const string PoundMole = "lbmol";

        //--- Momentum
        ///<summary><para>Newton second symbol. SI momentum unit.</para></summary>  
        public const string NewtonSecond = "N*s";

        //--- Angular Velocity
        ///<summary><para>Radian per second symbol. SI angular velocity unit.</para></summary>  
        public const string RadianPerSecond = "rad/s";

        //--- Angular Acceleration
        ///<summary><para>Radian per square second symbol. SI angular acceleration unit.</para></summary>  
        public const string RadianPerSquareSecond = "rad/s2";

        //--- Angular Momentum
        ///<summary><para>Joule second symbol. SI angular momentum unit.</para></summary>  
        public const string JouleSecond = "J*s";

        //--- Moment of Inertia
        ///<summary><para>Kilogram square metre symbol. SI moment of inertia unit.</para></summary>  
        public const string KilogramSquareMetre = "kg*m2";

        //--- Solid Angle
        ///<summary><para>Steradian symbol. SI solid angle unit.</summary>                 
        public const string Steradian = "sr";
        ///<summary><para>Square degree abbreviation. Solid angle unit.</para></summary>                 
        public const string SquareDegree = "deg2";

        //--- Luminous Intensity
        ///<summary><para>Candela symbol. SI luminous intensity unit.</para></summary>                 
        public const string Candela = "cd";

        //--- Luminous Flux
        ///<summary><para>Lumen symbol. SI luminous flux unit.</para></summary>                 
        public const string Lumen = "lm";

        //--- Luminous Energy
        ///<summary><para>Lumen second symbol. Luminous energy unit.</para></summary>                 
        public const string LumenSecond = "lm*s";
        ///<summary>
        ///<para>Talbot string representation. Luminous energy unit.</para>
        ///<para>The "T" alternative cannot be supported due to its incompatibility with the magnetic field B unit tesla.</para>            
        ///</summary>                 
        public const string Talbot = "talbot";

        //--- Luminance
        ///<summary><para>Candela per square metre symbol. SI luminance unit.</para></summary>                 
        public const string CandelaPerSquareMetre = "cd/m2";
        ///<summary><para>Nit abbreviation. Luminance unit.</para></summary>                 
        public const string Nit = "nt";
        ///<summary><para>Stilb symbol. CGS luminance unit.</para></summary>                 
        public const string Stilb = "sb";

        //--- Illuminance
        ///<summary><para>Lux symbol. SI illuminance unit.</para></summary>                 
        public const string Lux = "lx";
        ///<summary><para>Phot symbol. CGS illuminance unit.</para></summary>                 
        public const string Phot = "ph";

        //--- Logarithmic
        ///<summary><para>Bel symbol. Logarithmic unit.</para></summary>                        
        public const string Bel = "B";
        ///<summary><para>Neper symbol. Logarithmic unit.</para></summary>      
        public const string Neper = "Np";

        //--- Magnetic Flux
        ///<summary><para>Weber symbol. SI magnetic flux unit.</para></summary>                         
        public const string Weber = "Wb";
        ///<summary>
        ///<para>Maxwell symbol. CGS-Gaussian/CGS-EMU magnetic flux unit.</para></summary>                         
        public const string Maxwell = "Mx";

        //--- Magnetic Field B
        ///<summary><para>Tesla symbol. SI magnetic field B unit.</para></summary> 
        public const string Tesla = "T";
        ///<summary><para>Gauss symbol. CGS-Gaussian/CGS-EMU magnetic field B unit.</para></summary>                         
        public const string Gauss = "G";

        //--- Magnetic Field H
        ///<summary><para>Ampere per metre symbol. SI magnetic field H unit.</para></summary>                 
        public const string AmperePerMetre = "A/m";
        ///<summary><para>Oersted symbol. CGS-Gaussian/CGS-EMU magnetic field H unit.</para></summary>                         
        public const string Oersted = "Oe";

        //--- Radioactivity
        ///<summary><para>Becquerel symbol. SI radioactivity unit.</para></summary> 
        public const string Becquerel = "Bq";
        ///<summary><para>Curie symbol. Radioactivity unit.</para></summary> 
        public const string Curie = "Ci";
        ///<summary><para>Disintegrations per second abbreviation. Radioactivity unit.</para></summary> 
        public const string DisintegrationsPerSecond = "dps";
        ///<summary><para>Disintegrations per minute abbreviation. Radioactivity unit.</para></summary> 
        public const string DisintegrationsPerMinute = "dpm";
        ///<summary><para>Rutherford symbol. Radioactivity unit.</para></summary> 
        public const string Rutherford = "Rd";

        //--- Absorbed Dose
        ///<summary><para>Gray symbol. SI absorbed dose unit.</para></summary> 
        public const string Gray = "Gy";
        ///<summary>
        ///<para>Rad string representation. CGS absorbed dose unit.</para>
        ///<para>The "rad" alternative cannot be supported due to its incompatibility with the angle unit radian.</para>            
        ///</summary> 
        public const string Rad = "Rad";

        //--- Equivalent Dose
        ///<summary><para>Sievert symbol. SI equivalent dose unit.</para></summary> 
        public const string Sievert = "Sv";
        ///<summary><para>Roentgen equivalent in man symbol. CGS equivalent dose unit.</para></summary> 
        public const string REM = "rem";

        //--- Catalytic Activity
        ///<summary><para>Katal symbol. SI catalytic activity unit.</para></summary> 
        public const string Katal = "kat";

        //--- Catalytic Activity Concentration
        ///<summary><para>Katal per cubic metre symbol. SI catalytic activity concentration unit.</para></summary> 
        public const string KatalPerCubicMetre = "kat/m3";

        //--- Jerk
        ///<summary><para>Metre per cubic second symbol. SI jerk unit.</para></summary> 
        public const string MetrePerCubicSecond = "m/s3";

        //--- Mass Flow Rate
        ///<summary><para>Kilogram per second symbol. SI mass flow rate unit.</para></summary> 
        public const string KilogramPerSecond = "kg/s";

        //--- Density
        ///<summary><para>Kilogram per cubic metre symbol. SI density unit.</para></summary> 
        public const string KilogramPerCubicMetre = "kg/m3";

        //--- Area Density
        ///<summary><para>Kilogram per square metre symbol. SI area density unit.</para></summary> 
        public const string KilogramPerSquareMetre = "kg/m2";

        //--- Specific Volume
        ///<summary><para>Cubic metre per kilogram symbol. SI specific volume unit.</para></summary> 
        public const string CubicMetrePerKilogram = "m3/kg";

        //--- Volumetric Flow Rate
        ///<summary><para>Cubic metre per second symbol. SI volumetric flow rate unit.</para></summary> 
        public const string CubicMetrePerSecond = "m3/s";

        //--- Surface Tension
        ///<summary><para>Joule per square metre symbol. SI surface tension unit.</para></summary> 
        public const string JoulePerSquareMetre = "J/m2";

        //--- Specific Weight
        ///<summary><para>Newton per cubic metre symbol. SI specific weight unit.</para></summary> 
        public const string NewtonPerCubicMetre = "N/m3";

        //--- Thermal Conductivity
        ///<summary><para>Watt per metre per kelvin symbol. SI thermal conductivity unit.</para></summary> 
        public const string WattPerMetrePerKelvin = "W/m*K";

        //--- Thermal Conductance
        ///<summary><para>Watt per kelvin symbol. SI thermal conductance unit.</para></summary> 
        public const string WattPerKelvin = "W/K";

        //--- Thermal Resistivity
        ///<summary><para>Metre kelvin per watt symbol. SI thermal resistivity unit.</para></summary> 
        public const string MetreKelvinPerWatt = "m*K/W";

        //--- Thermal Resistance
        ///<summary><para>Kelvin per watt symbol. SI thermal resistance unit.</para></summary> 
        public const string KelvinPerWatt = "K/W";

        //--- Heat Transfer Coefficient
        ///<summary><para>Watt per square metre per kelvin symbol. SI heat transfer coefficient unit.</para></summary> 
        public const string WattPerSquareMetrePerKelvin = "W/m2*K";

        //--- Heat Flux Density
        ///<summary><para>Watt per square metre symbol. SI heat flux density unit.</para></summary> 
        public const string WattPerSquareMetre = "W/m2";

        //--- Entropy
        ///<summary><para>Joule per kelvin symbol. SI entropy unit.</para></summary> 
        public const string JoulePerKelvin = "J/K";

        //--- Electric Field Strength
        ///<summary><para>Newton per coulomb symbol. SI Electric Field Strength unit.</para></summary> 
        public const string NewtonPerCoulomb = "N/C";

        //--- Linear Electric Charge Density
        ///<summary><para>Coulomb per metre symbol. SI linear electric charge density unit.</para></summary> 
        public const string CoulombPerMetre = "C/m";

        //--- Surface Electric Charge Density
        ///<summary><para>Coulomb per square metre symbol. SI surface electric charge density unit.</para></summary> 
        public const string CoulombPerSquareMetre = "C/m2";

        //--- Volume Electric Charge Density
        ///<summary><para>Coulomb per cubic metre symbol. SI volume electric charge density unit.</para></summary> 
        public const string CoulombPerCubicMetre = "C/m3";

        //--- Current Density
        ///<summary><para>Ampere per square metre symbol. SI current density unit.</para></summary> 
        public const string AmperePerSquareMetre = "A/m2";

        //--- Energy Density
        ///<summary><para>Joule per cubic metre symbol. SI energy density unit.</para></summary> 
        public const string JoulePerCubicMetre = "J/m3";

        //--- Permittivity
        ///<summary><para>Farad per metre symbol. SI permittivity unit.</para></summary> 
        public const string FaradPerMetre = "F/m";

        //--- Permeability
        ///<summary><para>Henry per metre symbol. SI permeability unit.</para></summary> 
        public const string HenryPerMetre = "H/m";

        //--- Molar Energy
        ///<summary><para>Joule per mole symbol. SI molar energy unit.</para></summary> 
        public const string JoulePerMole = "J/mol";

        //--- Molar Entropy
        ///<summary><para>Joule per mole per kelvin symbol. SI molar entropy unit.</para></summary> 
        public const string JoulePerMolePerKelvin = "J/mol*K";

        //--- Molar Volume
        ///<summary><para>Cubic metre per mole symbol. SI molar volume unit.</para></summary> 
        public const string CubicMetrePerMole = "m3/mol";

        //--- Molar Mass
        ///<summary><para>Kilogram per mole symbol. SI molar mass unit.</para></summary> 
        public const string KilogramPerMole = "kg/mol";

        //--- Molar Concentration
        ///<summary><para>Mole per cubic metre symbol. SI molar concentration unit.</para></summary> 
        public const string MolePerCubicMetre = "mol/m3";

        //--- Molal Concentration
        ///<summary><para>Mole per kilogram symbol. SI molal concentration unit.</para></summary> 
        public const string MolePerKilogram = "mol/kg";

        //--- Radiant Intensity
        ///<summary><para>Watt per steradian symbol. SI radiant intensity unit.</para></summary> 
        public const string WattPerSteradian = "W/sr";

        //--- Radiance
        ///<summary><para>Watt per steradian per square metre symbol. SI radiance unit.</para></summary> 
        public const string WattPerSteradianPerSquareMetre = "W/sr*m2";
        
        //--- Fuel Economomy
        ///<summary><para>Inverse square metre symbol. SI fuel economy unit.</para></summary> 
        public const string InverseSquareMetre = "1/m2";
        ///<summary><para>Imperial mile per gallon abbreviation. Imperial fuel economy unit.</para></summary> 
        public const string MilePerGallon = "mpg";
        ///<summary><para>Imperial mile per gallon abbreviation. Imperial fuel economy unit.</para></summary> 
        public const string ImperialMilePerGallon = "impmpg";
        ///<summary><para>USCS mile per gallon abbreviation. USCS fuel economy unit.</para></summary> 
        public const string USCSMilePerGallon = "uscmpg";
        ///<summary><para>Litre per 100 km abbreviation. USCS fuel economy unit.</para></summary> 
        public const string LitrePerHundredKilometres = "L100km";

        //--- Sound Exposure
        ///<summary><para>Square pascal second symbol. SI sound exposure unit.</para></summary> 
        public const string SquarePascalSecond = "Pa2*s";

        //--- Sound Impedance
        ///<summary><para>Pascal second per cubic metre symbol. SI sound impedance unit.</para></summary> 
        public const string PascalSecondPerCubicMetre = "Pa*s/m3";

        //--- Rotational Stiffness
        ///<summary><para>Newton metre per radian symbol. SI rotational stiffness unit.</para></summary> 
        public const string NewtonMetrePerRadian = "N*m/rad";

        //--- Bit Rate
        ///<summary><para>Bit per second symbol. Bit rate unit.</para></summary> 
        public const string BitPerSecond = "bit/s";

        //--- Symbol Rate
        ///<summary><para>Baud. Symbol rate unit.</para></summary> 
        public const string Baud = "Bd";
    }
}
