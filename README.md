# FlexibleParser        

[![DOI](https://zenodo.org/badge/DOI/10.5281/zenodo.803400.svg)](https://doi.org/10.5281/zenodo.803400) 

FlexibleParser is a group of multi-purpose .NET parsing libraries based upon the following ideas:

- Intuitive, adaptable and easy to use.
- Pragmatic, but aiming for the maximum accuracy and correctness.
- Overall compatible and easily automatable. 
- Formed by independent DLLs managing specific situations.

Not sure how to use FlexibleParser outside Windows? Take a look at [this repository](https://github.com/varocarbas/FlexibleParser_NonWindows).

## Parts

At the moment, FlexibleParser is formed by the following independent parts:

[![DOI](https://zenodo.org/badge/DOI/10.5281/zenodo.803378.svg)](https://doi.org/10.5281/zenodo.803378) [UnitParser](https://customsolvers.com/unit_parser/) ([last release](https://customsolvers.com/downloads/flexible_parser/unit_parser/), [readme file](https://customsolvers.com/downloads/flexible_parser/unit_parser/UnitParser.pdf), [code analysis](https://varocarbas.com/unit_parser_code/), [article](https://www.codeproject.com/Articles/1211504/UnitParser), [web API](http://unitparser.eu-west-2.elasticbeanstalk.com/))<br/>
It allows to easily deal with a wide variety of situations involving units of measurement.
Among its most salient features are: user-defined exception triggering and gracefully managing numeric values of any size.


[![DOI](https://zenodo.org/badge/DOI/10.5281/zenodo.887593.svg)](https://doi.org/10.5281/zenodo.887593) [NumberParser](https://customsolvers.com/number_parser/) ([last release](https://customsolvers.com/downloads/flexible_parser/number_parser/), [readme file](https://customsolvers.com/downloads/flexible_parser/number_parser/NumberParser.pdf), [code analysis](https://varocarbas.com/number_parser_code/), [article](https://www.codeproject.com/Articles/1216825/NumberParser))<br/>
It provides a common framework for all the .NET numeric types. Main features: exceptions managed internally; beyond-double-range support; custom mathematical functionalities.


[![DOI](https://zenodo.org/badge/DOI/10.5281/zenodo.803399.svg)](https://doi.org/10.5281/zenodo.803399) [DateParser](https://customsolvers.com/date_parser/) ([last release](https://customsolvers.com/downloads/flexible_parser/date_parser/), [readme file](https://customsolvers.com/downloads/flexible_parser/date_parser/DateParser.pdf), [code analysis](https://varocarbas.com/date_parser_code/))<br/>
It enhances the default .NET date/time support, mostly via improving the usability of its main type and accounting for a big amount of additional time zone information.


## Authorship & Copyright

I, Alvaro Carballo Garcia (varocarbas), am the sole author of each single bit of this code.

Equivalently to what happens with all my other online contributions, this code can be considered public domain. For more information about my copyright/authorship attribution ideas, visit [https://customsolvers.com/copyright/](https://customsolvers.com/copyright/).