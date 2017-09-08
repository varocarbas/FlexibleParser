using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FlexibleParser
{
    public partial class NumberP
    {
        private static NumberD StartParse(ParseInfo info)
        {
            ParseInfo info2 = new ParseInfo(info);
            info2.OriginalString = RemoveValidRedundant
            (
                info2.OriginalString, info2.Config.Culture
            );

            //The blank spaces are supported as thousands separators in any case (i.e., independently upon the ParseType
            //value). But they have to be replace with standard separators to avoid parsing problems (the native parse
            //methods don't support them).
            if (info2.OriginalString.Contains(" "))
            {
                string[] thousands = GetCultureFeature
                (
                    CultureFeatures.ThousandSeparator, info2.Config.Culture
                );

                if (thousands.Length > 0)
                {
                    info2.OriginalString = string.Join
                    (
                        thousands[0], info2.OriginalString.Split(' ')
                    );
                }
            }

            if 
            (
                info2.Config.ParseType == ParseTypes.ParseThousandsStrict ||
                info2.Config.ParseType == ParseTypes.ParseOnlyTargetAndThousandsStrict
            )
            {
                //Method determining whether the thousands separators are valid or not.
                info2 = AnalyseThousands(info2);
                if (info2.Number.Error != ErrorTypesNumber.None)
                {
                    return new NumberD(info2.Number.Error);
                }
            }

            NumberD outNumberD =
            (
                info2.Config.Target == typeof(decimal) ? (NumberD)ParseDecimalMain
                (
                    info2.OriginalString, info2.Config.Culture, info2.Config.NumberStyle
                )
                : ParseToSpecificType(info2.OriginalString, info2.Config)
            );

            return outNumberD;
        }

        //Removing all the symbols associated with the current culture which, although technically valid, might
        //provoke some problems under certain parsing conditions.
        private static string RemoveValidRedundant(string input, CultureInfo culture)
        {
            CultureFeatures[] features = new CultureFeatures[]
            {
                CultureFeatures.CurrencySymbol, CultureFeatures.PercentageSymbol
            };

            string output = input;

            foreach (CultureFeatures feature in features)
            {
                foreach (string item in GetCultureFeature(feature, culture))
                {
                    output = output.Replace(item, "");
                }
            }

            return output;
        }

        private static ParseInfo AnalyseThousands(ParseInfo info)
        {   
            ParseInfo error = new ParseInfo(info, ErrorTypesNumber.ParseError);
            string[] separators = GetCultureFeature(CultureFeatures.ThousandSeparator, info.Config.Culture);
            if (separators.Length == 0) return error;

            //Removing the decimal part (+ making sure that it doesn't include any thousands separator) if existing.
            string string2 = RemoveDecimalsForThousands
            (
                info.OriginalString, info.Config.Culture, separators
            );

            string[] groups = string2.Split(separators, StringSplitOptions.None);
            if (groups.Length < 2) return error;

            int count = 0;
            while (count < 3)
            {
                count++;
                if (ThousandsAreOK(groups, count)) return info;
            }

            return error;
        }

        private static string RemoveDecimalsForThousands(string input, CultureInfo culture, string[] separators)
        {
            string[] decimals = GetCultureFeature(CultureFeatures.DecimalSeparator, culture);
            string[] tempVar = input.Split(decimals, StringSplitOptions.None);
            if (tempVar.Length != 2) return (tempVar.Length > 2 ? "" : input);

            int tempInt = 0;
            return
            (
                tempVar[1].FirstOrDefault(x => x == ' ' || separators.Contains(x.ToString())) != '\0' ||
                !int.TryParse(tempVar[1], out tempInt) ? "" : tempVar[0] 
            );
        }

        private static bool ThousandsAreOK(string[] groups, int type)
        {
            int target = 3; //Standard -> groups of 3.
            if (type == 3)
            {
                //Chinese -> groups of 4.
                target = 4;
            }

            //The last group (i = 0) doesn't need to be analysed (undefined size for all the types).
            for (int i = groups.Length - 1; i > 0; i--)
            {
                if (target != 0 && groups[i].Length != target) return false;

                if (type == 2)
                {
                    //Indian -> first group of 3; the remaining groups of 2.
                    target = 2;
                }
            }

            return true;
        }

        private static NumberD ParseToSpecificType(string input, ParseConfig config)
        {
            if (config.Target == typeof(decimal))
            {
                decimal value = 0m;
                if (decimal.TryParse(input, config.NumberStyle, config.Culture, out value))
                {
                    return new NumberD(value);
                }
            }
            else if (config.Target == typeof(double))
            {
                double value = 0.0;
                if (double.TryParse(input, config.NumberStyle, config.Culture, out value))
                {
                    return new NumberD(value);
                }
            }
            else if (config.Target == typeof(float))
            {
                float value = 0.0F;
                if (float.TryParse(input, config.NumberStyle, config.Culture, out value))
                {
                    return new NumberD(value);
                }
            }
            else if (config.Target == typeof(long))
            {
                long value = 0;
                if (long.TryParse(input, config.NumberStyle, config.Culture, out value))
                {
                    return new NumberD(value);
                }
            }
            else if (config.Target == typeof(ulong))
            {
                ulong value = 0;
                if (ulong.TryParse(input, config.NumberStyle, config.Culture, out value))
                {
                    return new NumberD(value);
                }
            }
            else if (config.Target == typeof(int))
            {
                int value = 0;
                if (int.TryParse(input, config.NumberStyle, config.Culture, out value))
                {
                    return new NumberD(value);
                }
            }
            else if (config.Target == typeof(uint))
            {
                uint value = 0;
                if (uint.TryParse(input, config.NumberStyle, config.Culture, out value))
                {
                    return new NumberD(value);
                }
            }
            else if (config.Target == typeof(short))
            {
                short value = 0;
                if (short.TryParse(input, config.NumberStyle, config.Culture, out value))
                {
                    return new NumberD(value);
                }
            }
            else if (config.Target == typeof(ushort))
            {
                ushort value = 0;
                if (ushort.TryParse(input, config.NumberStyle, config.Culture, out value))
                {
                    return new NumberD(value);
                }
            }
            else if (config.Target == typeof(sbyte))
            {
                sbyte value = 0;
                if (sbyte.TryParse(input, config.NumberStyle, config.Culture, out value))
                {
                    return new NumberD(value);
                }
            }
            else if (config.Target == typeof(byte))
            {
                byte value = 0;
                if (byte.TryParse(input, config.NumberStyle, config.Culture, out value))
                {
                    return new NumberD(value);
                }
            }
            else if (config.Target == typeof(char))
            {
                char value = '\0';
                if (char.TryParse(input, out value))
                {
                    return new NumberD(value);
                }
            }

            return 
            (
                config.ParseType == ParseTypes.ParseAll || config.ParseType == ParseTypes.ParseThousandsStrict ?
                //The input string cannot be directly parsed to the target type.
                //Under these conditions, there is a second chance: relying on ParseAnyMain which parses any string
                //fitting the Number range (+ the more permissive parsing rules of this library) and adapts its output
                //to the target type.
                ParseAnyMain(input, config) :
                new NumberD(ErrorTypesNumber.NativeMethodError)
            );
        }

        internal class ParseInfo
        {
            public string OriginalString { get; set; }
            public Number Number { get; set; }
            public ParseConfig Config { get; set; }

            public ParseInfo(NumberP numberP)
            {
                OriginalString = numberP.OriginalString;
                Config = new ParseConfig(numberP.Config);
                Number = new Number();
            }

            public ParseInfo(ParseInfo info) : this(info, ErrorTypesNumber.None) { }
            
            public ParseInfo(ParseInfo info, ErrorTypesNumber error)
            {
                OriginalString = info.OriginalString;
                Config = new ParseConfig(info.Config);
                Number = new Number(error);
            }
        }
    }

    ///<summary><para>Determines the main rules to be applied when parsing the string input at NumberP instantiation.</para></summary>
    public enum ParseTypes
    {
        ///<summary><para>All the strings are parsed without any restriction.</para></summary>
        ParseAll = 0,
        ///<summary><para>Only strings which fit within the range of the target type are acceptable.</para></summary>
        ParseOnlyTarget,
        ///<summary>
        ///<para>Invalid thousands separators trigger an error. Any valid configuration, supported by the given culture or not, is acceptable.</para>
        ///<para>Supported configurations for thousands separators: standard (groups of 3), Indian (first group of 3 and then groups of 2) and Chinese (groups of 4). Additionally to the group separators for the given culture, blank spaces are also supported.</para>        
        ///</summary> 
        ParseThousandsStrict,
        ///<summary>
        ///<para>Only strings which fit within the range of the target type and include valid thousands separators (any configuration) are acceptable.</para>
        ///<para>Supported configurations for thousands separators: standard (groups of 3), Indian (first group of 3 and then groups of 2) and Chinese (groups of 4). Additionally to the group separators for the given culture, blank spaces are also supported.</para>        
        ///</summary> 
        ParseOnlyTargetAndThousandsStrict
    }
}
