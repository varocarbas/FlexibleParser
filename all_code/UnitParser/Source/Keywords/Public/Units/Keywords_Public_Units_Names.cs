using System;
using System.Collections.Generic;

namespace FlexibleParser
{
    ///<summary><para>All the supported units.</para></summary>
    public enum Units
    {
        ///<summary><para>No supported unit.</para></summary>  
        None = 0,

        ///<summary><para>Unitless variable.</para></summary>  
        Unitless,

        ///<summary>
        ///<para>Valid SI unit not included elsewhere.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary>  
        ValidSIUnit,
        ///<summary>
        ///<para>Valid Imperial/USCS unit not included elsewhere.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>                       
        ///</summary>   
        ValidImperialUSCSUnit,
        ///<summary>
        ///<para>Valid Imperial unit not included elsewhere.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>                       
        ///</summary>   
        ValidImperialUnit,
        ///<summary>
        ///<para>Valid USCS unit not included elsewhere.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>                       
        ///</summary>   
        ValidUSCSUnit,
        ///<summary>
        ///<para>Valid CGS unit not included elsewhere.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary>   
        ValidCGSUnit,
        ///<summary>
        ///<para>Valid unit not included elsewhere.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary>    
        ValidUnit,

        //--- Length
        ///<summary>
        ///<para>Metre (m). SI length unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>            
        ///</summary>  
        Metre,
        ///<summary>
        ///<para>Centimetre (cm). CGS length unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary>  
        Centimetre,
        ///<summary>
        ///<para>Astronomical unit (AU). Length unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>
        ///</summary>           
        AstronomicalUnit,
        ///<summary>
        ///<para>Inch (in). Imperial/USCS length unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>            
        ///</summary>             
        Inch,
        ///<summary>
        ///<para>Foot (ft). Imperial/USCS length unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>
        ///</summary>             
        Foot,
        ///<summary>
        ///<para>Yard (yd). Imperial/USCS length unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>
        ///</summary>               
        Yard,
        ///<summary>
        ///<para>International mile (mi). Imperial/USCS length unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>            
        ///</summary>              
        Mile,
        ///<summary>
        ///<para>Nautical mile (M). Length unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>
        ///</summary>                   
        NauticalMile,
        ///<summary>
        ///<para>Thou (thou). Imperial/USCS length unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>
        ///</summary>                  
        Thou,
        ///<summary>
        ///<para>Mil (mil). Imperial/USCS length unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>
        ///</summary>                  
        Mil,
        ///<summary>
        ///<para>Fathom (fathom). Imperial/USCS length unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>            
        ///</summary>
        Fathom,
        ///<summary>
        ///<para>Rod (rd). Imperial/USCS length unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>            
        ///</summary>
        Rod,
        ///<summary>
        ///<para>Perch (perch). Imperial/USCS length unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>            
        ///</summary>
        Perch,
        ///<summary>
        ///<para>Pole (pole). Imperial/USCS length unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>            
        ///</summary>
        Pole,
        ///<summary>
        ///<para>Chain (ch). Imperial/USCS length unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>            
        ///</summary>            
        Chain,
        ///<summary>
        ///<para>Furlong (fur). Imperial/USCS length unit.</par>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>            
        ///</summary>
        Furlong,
        ///<summary>
        ///<para>Link (li). Imperial/USCS length unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>            
        ///</summary>
        Link,
        ///<summary>
        ///<para>U.S. survey inch (surin). USCS length unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>            
        ///</summary>
        SurveyInch,
        ///<summary>
        ///<para>U.S. survey foot (surft). USCS length unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>            
        ///</summary>
        SurveyFoot,
        ///<summary>
        ///<para>U.S. survey yard (suryd). USCS length unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>            
        ///</summary>
        SurveyYard,
        ///<summary>
        ///<para>U.S. survey rod (surrd). USCS length unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>            
        ///</summary>
        SurveyRod,
        ///<summary>
        ///<para>U.S. survey chain (surch). USCS length unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>            
        ///</summary>
        SurveyChain,
        ///<summary>
        ///<para>U.S. survey link (surli). USCS length unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>            
        ///</summary>
        SurveyLink,
        ///<summary>
        ///<para>U.S. survey mile (surmi). USCS length unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>            
        ///</summary>
        SurveyMile,
        ///<summary>
        ///<para>U.S. survey fathom (surfathom). USCS length unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>            
        ///</summary>
        SurveyFathom,
        ///<summary>
        ///<para>Ångström (Å). Length unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>            
        ///</summary>
        Angstrom,
        ///<summary>
        ///<para>Fermi (f). Length unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>            
        ///</summary>
        Fermi,
        ///<summary>
        ///<para>Light year (ly). Length unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>            
        ///</summary>
        LightYear,
        ///<summary>
        ///<para>Parsec (pc). Length unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>            
        ///</summary>
        Parsec,
        ///<summary>
        ///<para>Micron (μ). Length unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>            
        ///</summary>
        Micron,

        //--- Mass
        ///<summary>
        ///<para>Gram (g). SI mass unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>              
        ///</summary>
        Gram,
        ///<summary>
        ///<para>Metric ton (t). Mass unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>  
        ///</summary>
        MetricTon,
        ///<summary>
        ///<para>Grain (gr). Imperial/USCS mass unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>
        Grain,
        ///<summary>
        ///<para>Drachm (dr). Imperial/USCS mass unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>
        Drachm,
        ///<summary>
        ///<para>Ounce (oz). Imperial/USCS mass unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>            
        Ounce,
        ///<summary>
        ///<para>Pound (lb). Imperial/USCS mass unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>
        Pound,
        ///<summary>
        ///<para>Stone (st). Imperial/USCS mass unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>            
        Stone,
        ///<summary>
        ///<para>Slug (sl). Imperial/USCS mass unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>                      
        Slug,
        ///<summary>
        ///<para>Quarter (qr). Imperial mass unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>          
        Quarter,
        ///<summary>
        ///<para>Long quarter (impqr). Imperial mass unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>          
        LongQuarter,
        ///<summary>
        ///<para>Short quarter (uscqr). USCS mass unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>          
        ShortQuarter,
        ///<summary>
        ///<para>Hundredweight (cwt). Imperial mass unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>          
        Hundredweight,
        ///<summary>
        ///<para>Long hundredweight (impcwt). Imperial mass unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>
        LongHundredweight,
        ///<summary>
        ///<para>Short hundredweight (usccwt). USCS mass unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>
        ShortHundredweight,
        ///<summary>
        ///<para>Ton (tn). Imperial mass unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>            
        Ton,
        ///<summary>
        ///<para>Long ton (imptn). Imperial mass unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>            
        LongTon,
        ///<summary>
        ///<para>Short ton (usctn). USCS mass unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>  
        ShortTon,
        ///<summary>
        ///<para>Carat (ct). Mass unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>  
        Carat,
        ///<summary>
        ///<para>Dalton (Da). Mass unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>              
        ///</summary>  
        Dalton,
        ///<summary>
        ///<para>Unified atomic mass unit (u). Mass unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>              
        ///</summary>  
        UnifiedAtomicMassUnit,

        //--- Time
        ///<summary>
        ///<para>Second (s). SI time unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>              
        ///</summary>  
        Second,
        ///<summary>
        ///<para>Minute (min). Time unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>  
        ///</summary>  
        Minute,
        ///<summary>
        ///<para>Hour (h). Time unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>  
        ///</summary>  
        Hour,
        ///<summary>
        ///<para>Day (d). Time unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>  
        ///</summary>  
        Day,
        ///<summary>
        ///<para>Shake (shake). Time unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>  
        ///</summary>  
        Shake,

        //--- Area
        ///<summary>
        ///<para>Square metre (m2). SI area unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>                         
        ///</summary>                 
        SquareMetre,
        ///<summary>
        ///<para>Square centimetre (cm2). CGS area unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>                         
        ///</summary>                 
        SquareCentimetre,
        ///<summary>
        ///<para>Are (a). Area unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>  
        ///</summary>  
        Are,
        ///<summary>
        ///<para>Square inch (in2). Imperial/USCS area unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>                         
        ///</summary>                 
        SquareInch,
        ///<summary>
        ///<para>Square foot (ft2). Imperial/USCS area unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>                         
        ///</summary>                 
        SquareFoot,
        ///<summary>
        ///<para>Square rod (rd2). Imperial/USCS area unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>              
        ///</summary> 
        SquareRod,
        ///<summary>
        ///<para>Square perch (perch2). Imperial/USCS area unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>              
        ///</summary>  
        SquarePerch,
        ///<summary>
        ///<para>Square pole (pole2). Imperial/USCS area unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>              
        ///</summary>  
        SquarePole,
        ///<summary>
        ///<para>Rood (rood). Imperial/USCS area unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>  
        Rood,
        ///<summary>
        ///<para>Acre (ac). Imperial/USCS area unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>  
        Acre,
        ///<summary>
        ///<para>Barn (b). Area unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>              
        ///</summary>  
        Barn,
        ///<summary>
        ///<para>U.S. survey acre (surac). USCS area unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>  
        SurveyAcre,

        //--- Volume
        ///<summary>
        ///<para>Cubic metre (m3). SI volume unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>                         
        ///</summary>                 
        CubicMetre,
        ///<summary>
        ///<para>Cubic centimetre (cc). CGS volume unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>                         
        ///</summary>                 
        CubicCentimetre,
        ///<summary>
        ///<para>Litre (L). Volume unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>  
        ///</summary>  
        Litre,
        ///<summary>
        ///<para>Cubic foot (ft3). Imperial/USCS volume unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>                         
        ///</summary>                 
        CubicFoot,
        ///<summary>
        ///<para>Cubic inch (in3). Imperial/USCS volume unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>                         
        ///</summary>                 
        CubicInch,
        ///<summary>
        ///<para>Fluid ounce (floz). Imperial volume unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>  
        FluidOunce,
        ///<summary>
        ///<para>Imperial fluid ounce (impfloz). Imperial volume unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>  
        ImperialFluidOunce,
        ///<summary>
        ///<para>USCS fluid ounce (uscfloz). USCS volume unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>  
        USCSFluidOunce,
        ///<summary>
        ///<para>Gill (gi). Imperial volume unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>            
        Gill,
        ///<summary>
        ///<para>Imperial gill (impgi). Imperial volume unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>            
        ImperialGill,
        ///<summary>
        ///<para>USCS gill (uscgi). USCS volume unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>  
        USCSGill,
        ///<summary><para>Pint (pt). Imperial volume unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>             
        Pint,
        ///<summary><para>Imperial pint (imppt). Imperial volume unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>             
        ImperialPint,
        ///<summary><para>Liquid pint (liquidpt). USCS volume unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>
        LiquidPint,
        ///<summary>
        ///<para>Dry pint (drypt). USCS volume unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>
        DryPint,
        ///<summary>
        ///<para>Quart (qt). Imperial volume unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>            
        Quart,
        ///<summary>
        ///<para>Imperial quart (impqt). Imperial volume unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>            
        ImperialQuart,
        ///<summary>
        ///<para>Liquid quart (liquidqt). USCS volume unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>                        
        LiquidQuart,
        ///<summary>
        ///<para>Dry quart (dryqt). USCS volume unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>            
        DryQuart,
        ///<summary>
        ///<para>Gallon (gal). Imperial volume unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>             
        Gallon,
        ///<summary>
        ///<para>Imperial gallon (impgal). Imperial volume unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>             
        ImperialGallon,
        ///<summary>
        ///<para>Liquid gallon (liquidgal). USCS volume unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>     
        LiquidGallon,
        ///<summary>
        ///<para>Dry gallon (drygal). USCS volume unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>           
        DryGallon,

        //--- Angle
        ///<summary>
        ///<para>Radian (rad). SI angle unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>              
        ///</summary> 
        Radian,
        ///<summary>
        ///<para>Degree (°). Angle unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>  
        ///</summary> 
        Degree,
        ///<summary>
        ///<para>Arcminute ('). Angle unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>  
        ///</summary>             
        Arcminute,
        ///<summary>
        ///<para>Arcsecond (''). Angle unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>  
        ///</summary>                         
        Arcsecond,
        ///<summary>
        ///<para>Revolution (rev). Angle unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>             
        Revolution,
        ///<summary>
        ///<para>Gradian (grad). Angle unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>             
        Gradian,
        ///<summary>
        ///<para>Gon (gon). Angle unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>              
        ///</summary>             
        Gon,

        //--- Information
        ///<summary><para>Bit (bit). Information unit.</para></summary>                         
        Bit,
        ///<summary><para>Byte (byte). Information unit.</para></summary>   
        Byte,
        ///<summary><para>Nibble (nibble). Information unit.</para></summary>  
        Nibble,
        ///<summary><para>Quartet (quartet). Information unit.</para></summary>            
        Quartet,
        ///<summary><para>Octet (octet). Information unit.</para></summary>             
        Octet,

        //--- Force
        ///<summary>
        ///<para>Newton (N). SI force unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>                         
        ///</summary>             
        Newton,
        ///<summary>
        ///<para>Kilopond (kp). Force unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>                         
        ///</summary>   
        Kilopond,
        ///<summary>
        ///<para>Pound-force (lbf). Imperial/USCS force unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>                        
        ///</summary>               
        PoundForce,
        ///<summary>
        ///<para>Kip (kip). Force unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>                        
        ///</summary>               
        Kip,
        ///<summary>
        ///<para>Poundal (pdl). Imperial/USCS force unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>                          
        ///</summary>                
        Poundal,
        ///<summary>
        ///<para>Ounce-force (ozf). Imperial/USCS force unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>                          
        ///</summary>                
        OunceForce,
        ///<summary>
        ///<para>Dyne (dyn). CGS Force unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>                        
        ///</summary>              
        Dyne,

        //--- Velocity
        ///<summary>
        ///<para>Metre per second (m/s). SI velocity unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>                         
        ///</summary>                 
        MetrePerSecond,
        ///<summary>
        ///<para>Centimetre per second (cm/s). CGS velocity unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>                         
        ///</summary>                 
        CentimetrePerSecond,
        ///<summary>
        ///<para>Foot per second (ft/s). Imperial/USCS velocity unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>                         
        ///</summary>                 
        FootPerSecond,
        ///<summary>
        ///<para>Inch per second (in/s). Imperial/USCS velocity unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>                         
        ///</summary>                 
        InchPerSecond,
        ///<summary>
        ///<para>Knot (kn). Velocity unit.</para>
        ///<para>By default, SI/binary prefixes may not be used.</para>
        ///</summary>
        Knot,
        ///<summary>
        ///<para>Kilometre per hour (kph). Velocity unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para> 
        ///</summary>
        KilometrePerHour,
        ///<summary>
        ///<para>Mile per hour (mph). Velocity unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para> 
        ///</summary>
        MilePerHour,

        //--- Acceleration
        ///<summary>
        ///<para>Metre per square second (m/s2). SI acceleration unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>                         
        ///</summary>                 
        MetrePerSquareSecond,
        ///<summary>
        ///<para>Gal (Gal). CGS acceleration unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        Gal,
        ///<summary>
        ///<para>Foot per square second (ft/s2). Imperial/USCS acceleration unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>                         
        ///</summary>                 
        FootPerSquareSecond,
        ///<summary>
        ///<para>Inch per square second (in/s2). Imperial/USCS acceleration unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>                         
        ///</summary>                 
        InchPerSquareSecond,

        //--- Energy
        ///<summary>
        ///<para>Joule (J). SI energy unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>         
        ///</summary>   
        Joule,
        ///<summary>
        ///<para>Electronvolt (eV). Energy unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para> 
        ///</summary>            
        Electronvolt,
        ///<summary>
        ///<para>Watt hour (Wh). Energy unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>         
        ///</summary>   
        WattHour,
        ///<summary>
        ///<para>IT British thermal unit (BTU). Imperial/USCS energy unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>             
        ///</summary>                
        BritishThermalUnit,
        ///<summary>
        ///<para>Thermochemical British thermal unit (thBTU). Imperial/USCS energy unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>             
        ///</summary>                
        ThermochemicalBritishThermalUnit,
        ///<summary>
        ///<para>IT calorie (cal). Energy unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>            
        ///</summary>              
        Calorie,
        ///<summary>
        ///<para>Thermochemical calorie (thcal). Energy unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>            
        ///</summary>              
        ThermochemicalCalorie,
        ///<summary>
        ///<para>Food calorie (kcal). Energy unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>            
        ///</summary>              
        FoodCalorie,
        ///<summary>
        ///<para>Erg (erg). CGS energy unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>           
        ///</summary>              
        Erg,
        ///<summary>
        ///<para>EC therm (thm). Energy unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>           
        ///</summary>              
        Therm,
        ///<summary>
        ///<para>UK therm (ukthm). Energy unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>           
        ///</summary>              
        UKTherm,
        ///<summary>
        ///<para>US therm (usthm). Energy unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>           
        ///</summary>              
        USTherm,

        //--- Power
        ///<summary>
        ///<para>Watt (W). SI power unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>            
        ///</summary>              
        Watt,
        ///<summary>
        ///<para>Erg per second (erg/s). CGS power unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary>              
        ErgPerSecond,
        ///<summary>
        ///<para>Mechanical horsepower (hp). Imperial/USCS power unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>             
        ///</summary>              
        Horsepower,
        ///<summary>
        ///<para>Metric horsepower (hpM). Power unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para> 
        ///</summary>    
        MetricHorsepower,
        ///<summary>
        ///<para>Boiler horsepower (hpS). Power unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para> 
        ///</summary>    
        BoilerHorsepower,
        ///<summary>
        ///<para>Electric horsepower (hpE). Power unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para> 
        ///</summary>    
        ElectricHorsepower,
        ///<summary>
        ///<para>Ton of refrigeration (TR). Power unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>            
        ///</summary>              
        TonOfRefrigeration,

        //--- Pressure
        ///<summary>
        ///<para>Pascal (Pa). SI pressure unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>    
        Pascal,
        ///<summary>
        ///<para>Atmosphere (atm). Pressure unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para> 
        ///</summary>            
        Atmosphere,
        ///<summary>
        ///<para>Technical atmosphere (at). Pressure unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para> 
        ///</summary>            
        TechnicalAtmosphere,
        ///<summary>
        ///<para>Bar (bar). Pressure unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para> 
        ///</summary>            
        Bar,
        ///<summary>
        ///<para>Pound-force per square inch (psi). Imperial/USCS pressure unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>             
        ///</summary>                 
        PoundforcePerSquareInch,
        ///<summary>
        ///<para>Pound-force per square foot (psf). Imperial/USCS pressure unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>             
        ///</summary>                 
        PoundforcePerSquareFoot,
        ///<summary>
        ///<para>Millimetre of mercury (mmHg). Pressure unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>             
        ///</summary>               
        MillimetreOfMercury,
        ///<summary>
        ///<para>Inch of mercury 32 °F (inHg32). Pressure unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>             
        ///</summary>               
        InchOfMercury32F,
        ///<summary>
        ///<para>Inch of mercury 60 °F (inHg60). Pressure unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>             
        ///</summary>               
        InchOfMercury60F,
        ///<summary>
        ///<para>Barye (Ba). CGS pressure unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>               
        Barye,
        ///<summary>
        ///<para>Torr (Torr). Pressure unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>               
        Torr,
        ///<summary>
        ///<para>Kip per square inch (ksi). Pressure unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>             
        ///</summary>               
        KipPerSquareInch,

        //--- Frequency
        ///<summary>
        ///<para>Hertz (Hz). SI frequency unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para> 
        ///</summary>    
        Hertz,
        ///<summary>
        ///<para>Cycle per second (cps). Frequency unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>             
        ///</summary>                
        CyclePerSecond,

        //--- Electric Charge
        ///<summary>
        ///<para>Coulomb (C). SI electric charge unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        Coulomb,
        ///<summary>
        ///<para>AmpereHour (Ah). Electric charge unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        AmpereHour,
        ///<summary>
        ///<para>Franklin (Fr). CGS-Gaussian/CGS-ESU electric charge unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>            
        ///</summary>
        Franklin,
        ///<summary>
        ///<para>Statcoulomb (statC). CGS-Gaussian/CGS-ESU electric charge unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>                         
        ///</summary>
        Statcoulomb,
        ///<summary>
        ///<para>Electrostatic unit of charge (ESUcha). CGS-Gaussian/CGS-ESU electric charge unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        ESUOfCharge,
        ///<summary>
        ///<para>Abcoulomb (abC). CGS-EMU electric charge unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        Abcoulomb,
        ///<summary>
        ///<para>Electromagnetic unit of charge (EMUcha). CGS-EMU electric charge unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        EMUOfCharge,

        //--- Electric Current
        ///<summary>
        ///<para>Ampere (A). SI electric current unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        Ampere,
        ///<summary>
        ///<para>Statampere (statA). CGS-Gaussian/CGS-ESU electric current unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        Statampere,
        ///<summary>
        ///<para>Electrostatic unit of current (ESUcur). CGS-Gaussian/CGS-ESU electric current unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        ESUOfCurrent,
        ///<summary>
        ///<para>Abampere (abA). CGS-EMU electric current unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        Abampere,
        ///<summary>
        ///<para>Electromagnetic unit of current (EMUcur). CGS-EMU electric current unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>              
        ///</summary>
        EMUOfCurrent,
        ///<summary>
        ///<para>Biot (Bi). CGS-EMU electric current unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        Biot,

        //--- Electric Voltage
        ///<summary>
        ///<para>Volt (V). SI electric voltage unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        Volt,
        ///<summary>
        ///<para>Electrostatic unit of electric potential (ESUpot). CGS-Gaussian/CGS-ESU electric voltage unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>              
        ///</summary>
        ESUOfElectricPotential,
        ///<summary>
        ///<para>Statvolt (statV). CGS-Gaussian/CGS-ESU electric voltage unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        Statvolt,
        ///<summary>
        ///<para>Electromagnetic unit of electric potential (EMUpot). CGS-EMU electric voltage unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>              
        ///</summary>
        EMUOfElectricPotential,
        ///<summary>
        ///<para>Abvolt (abV). CGS-EMU electric voltage unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        Abvolt,

        //--- Electric Resistance 
        ///<summary>
        ///<para>Ohm (Ω). SI electric resistance unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        Ohm,
        ///<summary>
        ///<para>Statohm (statΩ). CGS-Gaussian/CGS-ESU electric resistance unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        Statohm,
        ///<summary>
        ///<para>Electrostatic unit of resistance (ESUres). CGS-Gaussian/CGS-ESU electric resistance unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        ESUOfResistance,
        ///<summary>
        ///<para>Abohm (abΩ). CGS-EMU electric resistance unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        Abohm,
        ///<summary>
        ///<para>Electromagnetic unit of resistance (EMUres). CGS-EMU electric resistance unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        EMUOfResistance,

        //--- Electric Resistivity 
        ///<summary>
        ///<para>Ohm metre (Ω*m). SI electric resistivity unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>  
        ///</summary>
        OhmMetre,

        //--- Electric Conductance
        ///<summary>
        ///<para>Siemens (S). SI electric conductance unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        Siemens,
        ///<summary>
        ///<para>Mho (℧). SI electric conductance unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        Mho,
        ///<summary>
        ///<para>Gemmho (gemmho). Electric conductance unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>             
        ///</summary>
        Gemmho,
        ///<summary>
        ///<para>Statsiemens (statS). CGS-Gaussian/CGS-ESU electric resistance unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        Statsiemens,
        ///<summary>
        ///<para>Statmho (stat℧). CGS-Gaussian/CGS-ESU electric resistance unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        Statmho,
        ///<summary>
        ///<para>Absiemens (abS). CGS-EMU electric resistance unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        Absiemens,
        ///<summary>
        ///<para>Abmho (ab℧). CGS-EMU electric resistance unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        Abmho,

        //--- Electric Conductivity
        ///<summary>
        ///<para>Siemens per metre (S/m). SI electric conductivity unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>  
        ///</summary>
        SiemensPerMetre,

        //--- Electric Capacitance
        ///<summary>
        ///<para>Farad (F). SI electric capacitance unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        Farad,
        ///<summary>
        ///<para>Statfarad (statF). CGS-Gaussian/CGS-ESU electric capacitance unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        Statfarad,
        ///<summary>
        ///<para>Electrostatic unit of capacitance (ESUcap). CGS-Gaussian/CGS-ESU electric capacitance unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        ESUOfCapacitance,
        ///<summary>
        ///<para>Abfarad (abF). CGS-EMU electric capacitance unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        Abfarad,
        ///<summary>
        ///<para>Electromagnetic unit of capacitance (EMUcap). CGS-EMU electric capacitance unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        EMUOfCapacitance,

        //--- Electric Inductance
        ///<summary>
        ///<para>Henry (H). SI electric inductance unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        Henry,
        ///<summary>
        ///<para>Stathenry (statH). CGS-Gaussian/CGS-ESU electric inductance unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        Stathenry,
        ///<summary>
        ///<para>Electrostatic unit of inductance (ESUind). CGS-Gaussian/CGS-ESU electric inductance unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        ESUOfInductance,
        ///<summary>
        ///<para>Abhenry (abH). CGS-EMU electric inductance unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        Abhenry,
        ///<summary>
        ///<para>Electromagnetic unit of inductance (EMUind). CGS-EMU electric inductance unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        EMUOfInductance,

        //--- Electric Dipole Moment
        ///<summary>
        ///<para>Coulomb metre (C*m). SI electric moment unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>                         
        ///</summary>                 
        CoulombMetre,
        ///<summary>
        ///<para>Debye (D). CGS-Gaussian electric dipole moment unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        Debye,

        //--- Temperature
        ///<summary>
        ///<para>Kelvin (K). SI temperature unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary>
        Kelvin,
        ///<summary>
        ///<para>Degree Celsius (°C). SI temperature unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>                     
        ///</summary>
        DegreeCelsius,
        ///<summary>
        ///<para>Degree Fahrenheit (°F). Imperial/USCS temperature unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>                        
        ///</summary>            
        DegreeFahrenheit,
        ///<summary>
        ///<para>Degree Rankine (°R). Imperial/USCS temperature unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>                         
        ///</summary>   
        DegreeRankine,

        //--- Wavenumber
        ///<summary>
        ///<para>Reciprocal metre (1/m). SI wavenumber unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>                         
        ///</summary>                 
        ReciprocalMetre,
        ///<summary>
        ///<para>Kayser (kayser). CGS wavenumber unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>                         
        ///</summary>
        Kayser,

        //--- Viscosity
        ///<summary>
        ///<para>Pascal second (Pa*s). SI viscosity unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>                         
        ///</summary>                 
        PascalSecond,
        ///<summary>
        ///<para>Poise (P). CGS viscosity unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>                         
        ///</summary>
        Poise,

        //--- Kinematic Viscosity
        ///<summary>
        ///<para>Square metre per second (m2/s). SI kinematic viscosity unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>                         
        ///</summary>                 
        SquareMetrePerSecond,
        ///<summary>
        ///<para>Stokes (St). CGS kinematic viscosity unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>                         
        ///</summary>
        Stokes,

        //--- Amount of Substance
        ///<summary>
        ///<para>Mole (mol). SI amount of substance unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>                         
        ///</summary>                 
        Mole,
        ///<summary>
        ///<para>Pound-mole (lbmol). Amount of substance unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>                         
        ///</summary>
        PoundMole,

        //--- Momentum
        ///<summary>
        ///<para>Newton second (N*s). SI momentum unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>                         
        ///</summary>                 
        NewtonSecond,

        //--- Angular Velocity
        ///<summary>
        ///<para>Radian per second (rad/s). SI angular velocity unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>                         
        ///</summary>                 
        RadianPerSecond,
        ///<summary>
        ///<para>Revolution per minute (rpm). Angular velocity unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>           
        ///</summary>             
        RevolutionPerMinute,

        //--- Angular Acceleration
        ///<summary>
        ///<para>Radian per square second (rad/s2). SI angular acceleration unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>                         
        ///</summary>                 
        RadianPerSquareSecond,

        //--- Angular Momentum
        ///<summary>
        ///<para>Joule second (J*s). SI angular momentum unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>                         
        ///</summary>                 
        JouleSecond,

        //--- Moment of Inertia
        ///<summary>
        ///<para>Kilogram square metre (kg*m2). SI moment of inertia unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>                         
        ///</summary>                 
        KilogramSquareMetre,

        //--- Solid Angle
        ///<summary>
        ///<para>Steradian (sr). SI solid angle unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>                         
        ///</summary>                 
        Steradian,
        ///<summary>
        ///<para>Square degree (deg2). Solid angle unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>                         
        ///</summary>                 
        SquareDegree,

        //--- Luminous Intensity
        ///<summary>
        ///<para>Candela (cd). SI luminous intensity unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>                         
        ///</summary>                 
        Candela,

        //--- Luminous Flux
        ///<summary>
        ///<para>Lumen (lm). SI luminous flux unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>                         
        ///</summary>                 
        Lumen,

        //--- Luminous Energy
        ///<summary>
        ///<para>Lumen second (lm*s). Luminous energy unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>                         
        ///</summary>                 
        LumenSecond,
        ///<summary>
        ///<para>Talbot (talbot). Luminous energy unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>                         
        ///</summary>                 
        Talbot,

        //--- Luminance
        ///<summary>
        ///<para>Candela per square metre (cd/m2). SI luminance unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>                         
        ///</summary>                 
        CandelaPerSquareMetre,
        ///<summary>
        ///<para>Nit (nt). Luminance unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>                         
        ///</summary>                 
        Nit,
        ///<summary>
        ///<para>Stilb (sb). CGS Luminance unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>                         
        ///</summary>                 
        Stilb,
        ///<summary>
        ///<para>Lambert (lambert). CGS Luminance unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>                         
        ///</summary>                 
        Lambert,
        ///<summary>
        ///<para>Foot-lambert (ftL). Imperial/USCS Luminance unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>                         
        ///</summary>                 
        FootLambert,

        //--- Illuminance
        ///<summary>
        ///<para>Lux (lx). SI illuminance unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>                         
        ///</summary>                 
        Lux,
        ///<summary>
        ///<para>Phot (ph). CGS illuminance unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>                         
        ///</summary>                 
        Phot,
        ///<summary>
        ///<para>Foot-candle (fc). Imperial/USCS illuminance unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>                         
        ///</summary>                 
        FootCandle,

        //--- Logarithmic
        ///<summary>
        ///<para>Bel (B). Logarithmic unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>                         
        ///</summary>                        
        Bel,
        ///<summary>
        ///<para>Neper (Np). Logarithmic unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>                         
        ///</summary>      
        Neper,

        //--- Magnetic Flux
        ///<summary>
        ///<para>Weber (Wb). SI magnetic flux unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>                         
        ///</summary>                         
        Weber,
        ///<summary>
        ///<para>Maxwell (Mx). CGS-Gaussian/CGS-EMU magnetic flux unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>                         
        ///</summary>                         
        Maxwell,

        //--- Magnetic Field B
        ///<summary>
        ///<para>Tesla (T). SI magnetic field B unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>                         
        ///</summary> 
        Tesla,
        ///<summary>
        ///<para>Gauss (G). CGS-Gaussian/CGS-EMU magnetic field B unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>                         
        ///</summary>                         
        Gauss,

        //--- Magnetic Field H
        ///<summary>
        ///<para>Ampere per metre (A/m). SI magnetic field H unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>                         
        ///</summary>                 
        AmperePerMetre,
        ///<summary>
        ///<para>Oersted (Oe). CGS-Gaussian/CGS-EMU magnetic field H unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>                         
        ///</summary>                         
        Oersted,

        //--- Radioactivity
        ///<summary>
        ///<para>Becquerel (Bq). SI radioactivity unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>                         
        ///</summary> 
        Becquerel,
        ///<summary>
        ///<para>Curie (Ci). Radioactivity unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>             
        ///</summary> 
        Curie,
        ///<summary>
        ///<para>Disintegrations per second (dps). Radioactivity unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>                         
        ///</summary> 
        DisintegrationsPerSecond,
        ///<summary>
        ///<para>Disintegrations per minute (dpm). Radioactivity unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>                         
        ///</summary> 
        DisintegrationsPerMinute,
        ///<summary>
        ///<para>Rutherford (Rd). Radioactivity unit.</para>
        ///<para>By default, SI/binary prefixes may not be used with this unit.</para>                         
        ///</summary> 
        Rutherford,

        //--- Absorbed Dose
        ///<summary>
        ///<para>Gray (Gy). SI absorbed dose unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>                         
        ///</summary> 
        Gray,
        ///<summary>
        ///<para>Rad (Rad). CGS absorbed dose unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>                         
        ///</summary> 
        Rad,

        //--- Absorbed Dose Rate
        ///<summary>
        ///<para>Gray per second (Gy/s). SI absorbed dose rate unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        GrayPerSecond,

        //--- Equivalent Dose
        ///<summary>
        ///<para>Sievert (Sv). SI equivalent dose unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>                         
        ///</summary> 
        Sievert,
        ///<summary>
        ///<para>Roentgen equivalent in man (rem). CGS equivalent dose unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>                         
        ///</summary> 
        REM,

        //--- Exposure
        ///<summary>
        ///<para>Coulomb per kilogram (C/kg). SI exposure unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        CoulombPerKilogram,
        ///<summary>
        ///<para>Roentgen (R). CGS exposure unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>                         
        ///</summary> 
        Roentgen,

        //--- Catalytic Activity
        ///<summary>
        ///<para>Katal (kat). SI catalytic activity unit.</para>
        ///<para>By default, binary prefixes may not be used with this unit.</para>            
        ///</summary> 
        Katal,

        //--- Catalytic Activity Concentration
        ///<summary>
        ///<para>Katal per cubic metre (kat/m3). SI catalytic activity concentration unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        KatalPerCubicMetre,

        //--- Jerk
        ///<summary>
        ///<para>Metre per cubic second (m/s3). SI jerk unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        MetrePerCubicSecond,

        //--- Mass Flow Rate
        ///<summary>
        ///<para>Kilogram per second (kg/s). SI mass flow rate unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        KilogramPerSecond,

        //--- Density
        ///<summary>
        ///<para>Kilogram per cubic metre (kg/m3). SI density unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        KilogramPerCubicMetre,

        //--- Area Density
        ///<summary>
        ///<para>Kilogram per square metre (kg/m2). SI area density unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        KilogramPerSquareMetre,

        //--- Energy Density
        ///<summary>
        ///<para>Joule per cubic metre (J/m3). SI energy density unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        JoulePerCubicMetre,

        //--- Specific Volume
        ///<summary>
        ///<para>Cubic metre per kilogram (m3/kg). SI specific volume unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        CubicMetrePerKilogram,

        //--- Volumetric Flow Rate
        ///<summary>
        ///<para>Cubic metre per second (m3/s). SI volumetric flow rate unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        CubicMetrePerSecond,

        //--- Surface Tension
        ///<summary>
        ///<para>Joule per square metre (J/m2). SI surface tension unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        JoulePerSquareMetre,

        //--- Specific Weight
        ///<summary>
        ///<para>Newton per cubic metre (N/m3). SI specific weight unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        NewtonPerCubicMetre,

        //--- Thermal Conductivity
        ///<summary>
        ///<para>Watt per metre per kelvin (W/m*K). SI thermal conductivity unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        WattPerMetrePerKelvin,

        //--- Thermal Conductance
        ///<summary>
        ///<para>Watt per kelvin (W/K). SI thermal conductance unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        WattPerKelvin,

        //--- Thermal Resistivity
        ///<summary>
        ///<para>Metre kelvin per watt (m*K/W). SI thermal resistivity unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        MetreKelvinPerWatt,

        //--- Thermal Resistance
        ///<summary>
        ///<para>Metre kelvin per watt (K/W). SI thermal resistance unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        KelvinPerWatt,

        //--- Heat Transfer Coefficient
        ///<summary>
        ///<para>Watt per square metre per kelvin (W/m2*K). SI heat transfer coefficient unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        WattPerSquareMetrePerKelvin,

        //--- Heat Flux Density
        ///<summary>
        ///<para>Watt per square metre (W/m2). SI heat flux density unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        WattPerSquareMetre,

        //--- Entropy
        ///<summary>
        ///<para>Joule per kelvin (J/K). SI entropy unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        JoulePerKelvin,

        //--- Electric Field Strength
        ///<summary>
        ///<para>Newton per coulomb (N/C). SI electric field strength unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        NewtonPerCoulomb,
        ///<summary>
        ///<para>Volt per metre (V/m). SI electric field strength unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        VoltPerMetre,

        //--- Linear Electric Charge Density
        ///<summary>
        ///<para>Coulomb per metre (C/m). SI linear electric charge density unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        CoulombPerMetre,

        //--- Surface Electric Charge Density
        ///<summary>
        ///<para>Coulomb per square metre (C/m2). SI surface electric charge density unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        CoulombPerSquareMetre,

        //--- Volume Electric Charge Density
        ///<summary>
        ///<para>Coulomb per cubic metre (C/m3). SI volume electric charge density unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        CoulombPerCubicMetre,

        //--- Current Density
        ///<summary>
        ///<para>Ampere per square metre (A/m2). SI current density unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        AmperePerSquareMetre,

        //--- Electromagnetic Permittivity
        ///<summary>
        ///<para>Farad per metre (F/m). SI electromagnetic permittivity unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        FaradPerMetre,

        //--- Electromagnetic Permeability
        ///<summary>
        ///<para>Henry per metre (H/m). SI electromagnetic permeability unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        HenryPerMetre,

        //--- Molar Energy
        ///<summary>
        ///<para>Joule per mole (J/mol). SI molar energy unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        JoulePerMole,

        //--- Molar Entropy
        ///<summary>
        ///<para>Joule per mole per kelvin (J/mol*K). SI molar entropy unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        JoulePerMolePerKelvin,

        //--- Molar Volume
        ///<summary>
        ///<para>Cubic metre per mole (m3/mol). SI molar volume unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        CubicMetrePerMole,

        //--- Molar Mass
        ///<summary>
        ///<para>Kilogram per mole (kg/mol). SI molar mass unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        KilogramPerMole,

        //--- Molar Concentration
        ///<summary>
        ///<para>Mole per cubic metre (mol/m3). SI molar concentration unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        MolePerCubicMetre,

        //--- Molal Concentration
        ///<summary>
        ///<para>Mole per kilogram (mol/kg). SI molal concentration unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        MolePerKilogram,

        //--- Radiant Intensity
        ///<summary>
        ///<para>Watt per steradian (W/sr). SI radiant intensity unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        WattPerSteradian,

        //--- Radiance
        ///<summary>
        ///<para>Watt per steradian per square metre (W/sr*m2). SI radiance unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        WattPerSteradianPerSquareMetre,

        //--- Fuel Economomy
        ///<summary>
        ///<para>Inverse square metre (1/m2). SI fuel economy unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        InverseSquareMetre,
        ///<summary>
        ///<para>Mile per gallon (mpg). Imperial fuel economy unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        MilePerGallon,
        ///<summary>
        ///<para>Imperial mile per gallon (impmpg). Imperial fuel economy unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        ImperialMilePerGallon,
        ///<summary>
        ///<para>USCS mile per gallon (uscmpg). USCS fuel economy unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>          
        ///</summary>     
        USCSMilePerGallon,
        ///<summary>
        ///<para>Kilometre per litre (km/L). Fuel economy unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>          
        ///</summary>     
        KilometrePerLitre,

        //--- Sound Exposure
        ///<summary>
        ///<para>Square pascal second (Pa2*s). SI sound exposure unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        SquarePascalSecond,

        //--- Sound Impedance
        ///<summary>
        ///<para>Pascal second per cubic metre (Pa*s/m3). SI sound impedance unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        PascalSecondPerCubicMetre,

        //--- Rotational Stiffness
        ///<summary>
        ///<para>Newton metre per radian (N*m/rad). SI rotational stiffness unit.</para>
        ///<para>No prefix may be used with this unit. This restriction doesn't apply to its constituent parts.</para>            
        ///</summary> 
        NewtonMetrePerRadian,

        //--- Bit Rate
        ///<summary><para>Bit per second (bit/s). Bit rate unit.</para></summary> 
        BitPerSecond,

        //--- Symbol Rate
        ///<summary><para>Baud (Bd). Symbol rate unit.</para></summary> 
        Baud,
    };
}
