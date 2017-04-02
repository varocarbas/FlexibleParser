# FlexibleParser        

[![DOI](https://zenodo.org/badge/DOI/10.5281/zenodo.192368.svg)](https://doi.org/10.5281/zenodo.192368) [![Build Status](https://travis-ci.org/varocarbas/FlexibleParser.svg?branch=master)](https://travis-ci.org/varocarbas/FlexibleParser)


FlexibleParser is a multi-purpose .NET parsing library based upon the following ideas:

- Intuitive, adaptable and easy to use.
- Pragmatic, but aiming for the maximum accuracy and correctness.
- Overall compatible and easily automatable. 
- Formed by independent DLLs managing specific situations.

## Parts

At the moment, FlexibleParser is formed by the following independent parts:

[![DOI](https://zenodo.org/badge/DOI/10.5281/zenodo.439724.svg)](https://doi.org/10.5281/zenodo.439724) [UnitParser](https://customsolvers.com/unit_parser/) ([last release](https://github.com/varocarbas/FlexibleParser/releases/tag/UnitParser_1.0.6301.23655), [readme file](https://github.com/varocarbas/FlexibleParser/blob/master/all_readme/UnitParser.md), [code analysis](https://varocarbas.com/unit_parser_code/))<br/>
It allows to easily deal with a wide variety of situations involving units of measurement.
Among its most salient features are: user-defined exception triggering and gracefully managing numeric values of any size.


[![DOI](https://zenodo.org/badge/DOI/10.5281/zenodo.192347.svg)](https://doi.org/10.5281/zenodo.192347) [NumberParser](https://customsolvers.com/number_parser/) ([last release](https://github.com/varocarbas/FlexibleParser/releases/tag/NumberParser_1.0.6174.19926), [readme file](https://github.com/varocarbas/FlexibleParser/blob/master/all_readme/NumberParser.md), [code analysis](https://varocarbas.com/number_parser_code/))<br/>It provides a common framework for all the .NET numeric types. Main features: exceptions managed internally; beyond-double-range support; custom mathematical functionalities.


## Authorship & Copyright

I, Alvaro Carballo Garcia (varocarbas), am the sole author of each single bit of this code.

Equivalently to what happens with all my other online contributions, this code can be considered public domain. For more information about my copyright/authorship attribution ideas, visit the corresponding pages of my sites:
- https://customsolvers.com/copyright/<br/> 
ES: https://customsolvers.com/copyright_es/
- https://varocarbas.com/copyright/<br/>
ES: https://varocarbas.com/copyright_es/