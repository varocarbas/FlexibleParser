using System;
using System.Collections.Generic;

namespace FlexibleParser
{
    ///<summary><para>Usage types of unit prefixes.</para></summary>
    public enum PrefixUsageTypes
    {
        ///<summary><para>Prefixes can only be used together with units which commonly/officially support them.</para></summary>
        DefaultUsage = 0,
        ///<summary><para>Prefixes can be used with any individual unit or named compound.</para></summary>
        AllUnits,
    };

    ///<summary><para>Types of unit prefixes.</para></summary>
    public enum PrefixTypes
    {
        ///<summary><para>No unit prefix.</para>
        None = 0,

        ///<summary>
        ///<para>Refers to all the International System of Units prefixes.</para>
        ///<para>By default, these prefixes may only be used with SI, CGS or related units.</para>
        ///</summary>
        SI,
        ///<summary>
        ///<para>Refers to all the binary prefixes.</para>
        ///<para>By default, these prefixes may only be used with information or related units.</para>
        ///</summary>
        Binary
    }

    ///<summary><para>All the International System of Units prefixes.</para></summary>
    public enum SIPrefixes
    {
        ///<summary><para>No SI prefix.</para></summary>
        None = 0,

        ///<summary><para>Yotta (Y). 10^24.</para></summary>
        Yotta,
        ///<summary><para>Zetta (Z). 10^21.</para></summary>
        Zetta,
        ///<summary><para>Exa (E). 10^18.</para></summary>
        Exa,
        ///<summary><para>Peta (P). 10^15.</para></summary>
        Peta,
        ///<summary><para>Tera (T). 10^12.</para></summary>
        Tera,
        ///<summary><para>Giga (G). 10^9.</para></summary>
        Giga,
        ///<summary><para>Mega (M). 10^6.</para></summary>
        Mega,
        ///<summary><para>Kilo (k). 10^3.</para></summary>
        Kilo,
        ///<summary><para>Hecto (h). 10^2.</para></summary>
        Hecto,
        ///<summary><para>Deca (da). 10.</para></summary>
        Deca,
        ///<summary><para>Deci (d). 10^-1.</para></summary>
        Deci,
        ///<summary><para>Centi (c). 10^-2.</para></summary>            
        Centi,
        ///<summary><para>Milli (m). 10^-3.</para></summary>            
        Milli,
        ///<summary><para>Micro (μ). 10^-6.</para></summary>            
        Micro,
        ///<summary><para>Nano (n). 10^-9.</para></summary>           
        Nano,
        ///<summary><para>Pico (p). 10^-12.</para></summary>         
        Pico,
        ///<summary><para>Femto (f). 10^-15.</para></summary>        
        Femto,
        ///<summary><para>Atto (a). 10^-18.</para></summary>         
        Atto,
        ///<summary><para>Zepto (z). 10^-21.</para></summary>            
        Zepto,
        ///<summary><para>Yocto (y). 10^-24.</para></summary>            
        Yocto
    };

    ///<summary>
    ///<para>Contains the official symbols of all the SI prefixes.</para>
    ///<para>All the analyses involving these strings treat them as case sensitive.</para>
    ///</summary>
    public class SIPrefixSymbols
    {
        ///<summary><para>Yotta symbol. 10^24.</para></summary>
        public const string Yotta = "Y";
        ///<summary><para>Zetta symbol. 10^21.</para></summary>
        public const string Zetta = "Z";
        ///<summary><para>Exa symbol. 10^18.</para></summary>
        public const string Exa = "E";
        ///<summary><para>Peta symbol. 10^15.</para></summary>
        public const string Peta = "P";
        ///<summary><para>Tera symbol. 10^12.</para></summary>
        public const string Tera = "T";
        ///<summary><para>Giga symbol. 10^9.</para></summary>
        public const string Giga = "G";
        ///<summary><para>Mega symbol. 10^6.</para></summary>
        public const string Mega = "M";
        ///<summary><para>Kilo symbol. 10^3.</para></summary>
        public const string Kilo = "k";
        ///<summary><para>Hecto symbol. 10^2.</para></summary>
        public const string Hecto = "h";
        ///<summary><para>Deca symbol. 10.</para></summary>
        public const string Deca = "da";
        ///<summary><para>Deci symbol. 10^-1.</para></summary>
        public const string Deci = "d";
        ///<summary><para>Centi symbol. 10^-2.</para></summary>            
        public const string Centi = "c";
        ///<summary><para>Milli symbol. 10^-3.</para></summary>            
        public const string Milli = "m";
        ///<summary><para>Micro symbol. 10^-6.</para></summary>            
        public const string Micro = "μ";
        ///<summary><para>Nano symbol. 10^-9.</para></summary>           
        public const string Nano = "n";
        ///<summary><para>Pico symbol. 10^-12.</para></summary>         
        public const string Pico = "p";
        ///<summary><para>Femto symbol. 10^-15.</para></summary>        
        public const string Femto = "f";
        ///<summary><para>Atto symbol. 10^-18.</para></summary>         
        public const string Atto = "a";
        ///<summary><para>Zepto symbol. 10^-21.</para></summary>            
        public const string Zepto = "z";
        ///<summary><para>Yocto symbol. 10^-24.</para></summary>            
        public const string Yocto = "y";
    }

    ///<summary><para>Values of all the International System of Units prefixes.</para></summary>
    public class SIPrefixValues
    {
        ///<summary><para>Yotta (Y) value.</para></summary>
        public const decimal Yotta = 1E24m;
        ///<summary><para>Zetta (Z) value.</para></summary>
        public const decimal Zetta = 1E21m;
        ///<summary><para>Exa (E) value.</para></summary>
        public const decimal Exa = 1E18m;
        ///<summary><para>Peta (P) value.</para></summary>
        public const decimal Peta = 1E15m;
        ///<summary><para>Tera (T) value.</para></summary>
        public const decimal Tera = 1E12m;
        ///<summary><para>Giga (G) value.</para></summary>
        public const decimal Giga = 1E9m;
        ///<summary><para>Mega (M) value.</para></summary>
        public const decimal Mega = 1E6m;
        ///<summary><para>Kilo (k) value.</para></summary>
        public const decimal Kilo = 1E3m;
        ///<summary><para>Hecto (h) value.</para></summary>
        public const decimal Hecto = 1E2m;
        ///<summary><para>Deca (da) value.</para></summary>
        public const decimal Deca = 10m;
        ///<summary><para>Deci (d) value.</para></summary>
        public const decimal Deci = 1E-1m;
        ///<summary><para>Centi (c) value.</para></summary>
        public const decimal Centi = 1E-2m;
        ///<summary><para>Milli (m) value.</para></summary>
        public const decimal Milli = 1E-3m;
        ///<summary><para>Micro (μ) value.</para></summary>
        public const decimal Micro = 1E-6m;
        ///<summary><para>Nano (n) value.</para></summary>
        public const decimal Nano = 1E-9m;
        ///<summary><para>Pico (p) value.</para></summary>
        public const decimal Pico = 1E-12m;
        ///<summary><para>Femto (f) value.</para></summary>
        public const decimal Femto = 1E-15m;
        ///<summary><para>Atto (a) value.</para></summary>
        public const decimal Atto = 1E-18m;
        ///<summary><para>Zepto (z) value.</para></summary>
        public const decimal Zepto = 1E-21m;
        ///<summary><para>Yocto (y) value.</para></summary>
        public const decimal Yocto = 1E-24m;
    }

    ///<summary>
    ///<para>Contains the official symbols of all the binary prefixes.</para>
    ///<para>All the analyses involving these strings treat them as case sensitive.</para>
    ///</summary>
    public class BinaryPrefixSymbols
    {
        ///<summary><para>Yobi symbol. 2^80.</para></summary>
        public const string Yobi = "Yi";
        ///<summary><para>Zebi symbol. 2^70.</para></summary>           
        public const string Zebi = "Zi";
        ///<summary><para>Exbi symbol. 2^60.</para></summary>                     
        public const string Exbi = "Ei";
        ///<summary><para>Pebi symbol. 2^50.</para></summary>                               
        public const string Pebi = "Pi";
        ///<summary><para>Tebi symbol. 2^40.</para></summary>                                
        public const string Tebi = "Ti";
        ///<summary><para>Gibi symbol. 2^30.</para></summary>                                            
        public const string Gibi = "Gi";
        ///<summary><para>Mebi symbol. 2^20.</para></summary>                                           
        public const string Mebi = "Mi";
        ///<summary><para>Kibi symbol. 2^10.</para></summary>                                           
        public const string Kibi = "Ki";
    };

    ///<summary><para>All the binary prefixes.</para></summary>
    public enum BinaryPrefixes
    {
        ///<summary><para>No binary prefix.</para></summary>
        None = 0,

        ///<summary><para>Yobi (Yi). 2^80.</para></summary>
        Yobi,
        ///<summary><para>Zebi (Zi). 2^70.</para></summary>           
        Zebi,
        ///<summary><para>Exbi (Ei). 2^60.</para></summary>                     
        Exbi,
        ///<summary><para>Pebi (Pi). 2^50.</para></summary>                               
        Pebi,
        ///<summary><para>Tebi (Ti). 2^40.</para></summary>                                
        Tebi,
        ///<summary><para>Gibi (Gi). 2^30.</para></summary>                                            
        Gibi,
        ///<summary><para>Mebi (Mi). 2^20.</para></summary>                                           
        Mebi,
        ///<summary><para>Kibi (Ki). 2^10.</para></summary>                                           
        Kibi
    };

    ///<summary><para>Values of all the binary prefixes.</para></summary>
    public class BinaryPrefixValues
    {
        ///<summary><para>Yobi (Yi) value.</para></summary>
        public const decimal Yobi = 1208925819614629174706176m;
        ///<summary><para>Zebi (Zi) value.</para></summary>
        public const decimal Zebi = 1180591620717411303424m;
        ///<summary><para>Exbi (Ei) value.</para></summary>
        public const decimal Exbi = 1152921504606846976m;
        ///<summary><para>Pebi (Pi) value.</para></summary>
        public const decimal Pebi = 1125899906842624m;
        ///<summary><para>Tebi (Ti) value.</para></summary>
        public const decimal Tebi = 1099511627776m;
        ///<summary><para>Gibi (Gi) value.</para></summary>
        public const decimal Gibi = 1073741824m;
        ///<summary><para>Mebi (Mi) value.</para></summary>
        public const decimal Mebi = 1048576m;
        ///<summary><para>Kibi (Ki) value.</para></summary>
        public const decimal Kibi = 1024m;
    }
}
