# UnitParser 

[NuGet package](https://www.nuget.org/packages/UnitParser/) -- [UnitParser.dll](https://github.com/varocarbas/FlexibleParser/tree/master/all_binaries)

##Introduction
The namespace is ```FlexibleParser``` and the main class ```UnitP```. It can be instantiated in many different ways.

```C#
//1 N.
UnitP unitP = new UnitP("1 N"); 

//1 N.
unitP = new UnitP(1m, UnitSymbols.Newton);

//1 N.
unitP = new UnitP(1m, "nEwTon");

//1 N.
unitP = new UnitP(1m, Units.Newton);
```

```UnitP``` can be seen as an abstract concept including many specific types ([full list](https://github.com/varocarbas/FlexibleParser/blob/master/all_code/UnitParser/Source/Keywords/Public/Keywords_Public_Types.cs)). Same-type variables can be added/subtracted. Different-type variables can be multiplied/divided, but only in case of generating a valid-type output.

```C#
//2 N.
unitP = new UnitP("1 N") + new UnitP(1m, Units.Newton);

//1 J.
unitP = new UnitP("1 N") * new UnitP("1 m");

//Error not triggering an exception. 
//The output unit N*m^2 doesn't match any supported type.
unitP = new UnitP("1 N") * new UnitP("1 m") * new UnitP("1 m"); 
```

###Main Variable Information
UnitP variables are defined according to various ```readonly``` fields populated at instantiation.

```Unit``` - Corresponding [Units](https://github.com/varocarbas/FlexibleParser/blob/master/all_code/UnitParser/Source/Keywords/Public/Units/Keywords_Public_Units_Names.cs) member.<br>
```UnitType``` - Corresponding [UnitTypes](https://github.com/varocarbas/FlexibleParser/blob/master/all_code/UnitParser/Source/Keywords/Public/Keywords_Public_Types.cs) member.<br>
```UnitSystem``` - Corresponding [UnitSystems](https://github.com/varocarbas/FlexibleParser/blob/master/all_code/UnitParser/Source/Keywords/Public/Keywords_Public_Miscellaneous.cs) member.<br>
```UnitParts``` - Defining parts of the given unit.<br>
```UnitPrefix``` - [Supported prefix](https://github.com/varocarbas/FlexibleParser/blob/master/all_code/UnitParser/Source/Keywords/Public/Keywords_Public_Prefixes.cs) affecting all the unit parts.<br>
```BaseTenExponent``` - Base-ten exponent used when dealing with too small/big values.<br>
```Error``` - Variable storing all the error- and exception-related information.

##General Rules

All the functionalities are based upon the following ideas:
- In case of incompatibility, the first element is always preferred.
- By default, the formally-correct alternative is preferred. Some required modifications might be performed.
- By default, all the errors are managed internally.

```C#
//1.3048 m.
unitP = new UnitP("1 m") + new UnitP("1 ft"); 

//Error not triggering an exception. 
//The parser expects "km" or a full-name-based version like "KiLom".
unitP = new UnitP("1 Km"); 

//999999.999999900000 * 10^19 YSt.
unitP = 999999999999999999999999999999999999.9 * new UnitP("9999999999999 St"); 
```

##Unit String Parsing Format

The unit string parsing part is quite flexible, but there are some basic rules.
- When number and unit are included in the same string, a separating blank space is always required.
- String multi-part units are expected to be exclusively formed by units, multiplication/division signs and exponents.
- Only one division sign is expected. The parser understands that all what lies before/after it is the numerator/denominator.

```C#
//Error not triggering an exception. 
//The parser expects "1 m" or any other version including a separating blank space.
unitP = new UnitP("1m"); 

//1 W.
unitP = new UnitP("1 J*J/s*J2*J-1*s*s-1");

//Error not triggering an exception. 
//The parser understands "J*J/(s*J2*s*J*s)", what doesn't represent a supported type.
unitP = new UnitP("1 J*J/(s*J2*s)*J*s");
```

##Numeric Support
Formally, two numeric types are supported: ```decimal```, almost everywhere; and ```double```, only in multiplication/division with ```UnitP``` variables. Practically, ```UnitP``` variables implement a mixed system delivering ```decimal``` precision and beyond-```double```-range support. 

```C#
//7.81011 ft.
unitP = new UnitP("1 ft") * 7.891011m;

//1213141516 s.
unitP = new UnitP("1 s") * 1213141516.0;

//0.0003094346047382564187537561*10^-752 ym.
unitP = 0.0000000000000000000000000000000000000000000000001 * new UnitP(0.000000000000000000001m, "ym2") / 
new UnitP("999999999999999999999 Ym") / double.MaxValue / double.MaxValue; 
```

##Further Code Samples
The [test application](https://github.com/varocarbas/FlexibleParser/blob/master/all_code/Test/Program.cs) includes a relevant number of descriptive code samples. 

## Authorship & Copyright

I, Alvaro Carballo Garcia (varocarbas), am the sole author of each single bit of this code.

Equivalently to what happens with all my other online contributions, this code can be considered public domain. For more information about my copyright/authorship attribution ideas, visit the corresponding pages of my sites:
- http://customsolvers.com/en/pages/company/legal/copyright/<br/> 
ES: http://customsolvers.com/es/paginas/empresa/legal/copyright/
- http://varocarbas.com/copyright/<br/>ES: http://varocarbas.com/copyright_es/
