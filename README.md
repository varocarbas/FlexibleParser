
# FlexibleParser        

[![Build Status](https://travis-ci.org/varocarbas/FlexibleParser.svg?branch=master)](https://travis-ci.org/varocarbas/FlexibleParser)

FlexibleParser is a multi-purpose .NET parsing library based upon the following ideas:

- Intuitive, adaptable and easy to use.
- Pragmatic, but aiming for the maximum accuracy and correctness.
- Overall compatible and easily automatable. 
- Formed by independent DLLs managing specific situations.

## Parts

At the moment, FlexibleParser is formed by the following independent parts:

[UnitParser](https://github.com/varocarbas/FlexibleParser/blob/master/all_readme/UnitParser.md) ([C# code](https://github.com/varocarbas/FlexibleParser/tree/master/all_code/UnitParser/Source), [all_binaries](https://github.com/varocarbas/FlexibleParser/tree/master/all_binaries)/UnitParser.dll and https://github.com/varocarbas/FlexibleParser/blob/master/all_comments/UnitParser.XML with the IntelliSense information). It allows to easily deal with a wide variety of situations involving units of measurement.
Among its most salient features are: user-defined exception triggering and gracefully managing numeric values of any size.


[NumberParser](https://github.com/varocarbas/FlexibleParser/blob/master/all_readme/NumberParser.md) ([C# code](https://github.com/varocarbas/FlexibleParser/tree/master/all_code/NumberParser/Source), [all_binaries](https://github.com/varocarbas/FlexibleParser/tree/master/all_binaries)/NumberParser.dll and https://github.com/varocarbas/FlexibleParser/blob/master/all_comments/NumberParser.XML with the IntelliSense information). It provides a common framework for all the .NET numeric types. Main features: exceptions managed internally; beyond-double-range support; custom mathematical functionalities.


## Authorship & Copyright

I, Alvaro Carballo Garcia (varocarbas), am the sole author of each single bit of this code.

Equivalently to what happens with all my other online contributions, this code can be considered public domain. For more information about my copyright/authorship attribution ideas, visit the corresponding pages of my sites:
- http://customsolvers.com/en/pages/company/legal/copyright/<br/> 
ES: http://customsolvers.com/es/paginas/empresa/legal/copyright/
- http://varocarbas.com/copyright/<br/>ES: http://varocarbas.com/copyright_es/
