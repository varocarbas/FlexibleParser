using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FlexibleParser
{
    ///<summary>
    ///<para>NumberP is the only NumberX class parsing string inputs.</para>
    ///<para>It is implicitly convertible to Number, NumberD, NumberO and string.</para>
    ///</summary>
    public partial class NumberP
    {
        ///<summary><para>Readonly primary value under the current conditions.</para></summary>
        public readonly dynamic Value;
        ///<summary><para>Readonly Base-ten exponent under the current conditions.</para></summary>
        public readonly int BaseTenExponent;
        ///<summary><para>Readonly string variable including the original input to be parsed.</para></summary>
        public readonly string OriginalString;
        ///<summary><para>Readonly ParseConfig variable defining the current parsing configuration.</para></summary>
        public readonly ParseConfig Config = new ParseConfig();
        ///<summary><para>Readonly member of the ErrorTypesNumber enum which best suits the current conditions.</para></summary>
        public readonly ErrorTypesNumber Error;

        ///<summary><para>Initialises a new NumberP instance.</para></summary>
        ///<param name="input">String variable whose contents will be parsed.</param>
        public NumberP(string input) : this (input, new ParseConfig()) { }

        ///<summary><para>Initialises a new NumberP instance.</para></summary>
        ///<param name="input">String variable whose contents will be parsed.</param>     
        ///<param name="config">ParseConfig variable whose information will be used.</param>   
        public NumberP(string input, ParseConfig config)
        {
            if (input == null || input.Trim().Length < 1)
            {
                Error = ErrorTypesNumber.InvalidInput;
                return;
            }

            OriginalString = input;
            Config = new ParseConfig(config);

            //NumberD is lighter than NumberP and contains all what matters here (i.e., dynamic Value and BaseTenExponent).
            NumberD tempVar = StartParse(new ParseInfo(this));

            if (tempVar.Error != ErrorTypesNumber.None)
            {
                Error = tempVar.Error;
            }
            else
            {
                BaseTenExponent = tempVar.BaseTenExponent;
                Value = tempVar.Value;
            }
        }

        ///<summary><para>Initialises a new NumberP instance.</para></summary>
        ///<param name="numberP">NumberP variable whose information will be used.</param>     
        public NumberP(NumberP numberP)
        {
            if (numberP == null)
            {
                Error = ErrorTypesNumber.InvalidInput;
            }
            else
            {
                BaseTenExponent = numberP.BaseTenExponent;
                Value = numberP.Value;
                OriginalString = numberP.OriginalString;
                Config = new ParseConfig(numberP.Config);
                Error = numberP.Error;
            }
        }

        ///<summary><para>Initialises a new NumberP instance.</para></summary>
        ///<param name="number">Number variable whose information will be used.</param>    
        public NumberP(Number number)
        {
            Number tempVar = Common.ExtractSameTypeNumberXInfo(number);

            if (tempVar.Error != ErrorTypesNumber.None)
            {
                Error = tempVar.Error;
            }
            else
            {
                BaseTenExponent = number.BaseTenExponent;
                Value = number.Value;
                Config = new ParseConfig(typeof(decimal));
            }
        }

        ///<summary><para>Initialises a new NumberP instance.</para></summary>
        ///<param name="numberD">NumberD variable whose information will be used.</param>   
        public NumberP(NumberD numberD)
        {
            NumberD tempVar = Common.ExtractSameTypeNumberXInfo(numberD);

            if (tempVar.Error != ErrorTypesNumber.None)
            {
                Error = tempVar.Error;
            }
            else
            {
                BaseTenExponent = numberD.BaseTenExponent;
                Value = numberD.Value;
                Config = new ParseConfig(numberD.Type);
            }
        }

        ///<summary><para>Initialises a new NumberP instance.</para></summary>
        ///<param name="numberO">NumberO variable whose information will be used.</param>   
        public NumberP(NumberO numberO)
        {
            Number tempVar = Common.ExtractSameTypeNumberXInfo(numberO);

            if (tempVar.Error != ErrorTypesNumber.None)
            {
                Error = tempVar.Error;
            }
            else
            {
                BaseTenExponent = numberO.BaseTenExponent;
                Value = numberO.Value;
                Config = new ParseConfig(typeof(decimal));
            }
        }

        internal NumberP(Number number, string originalString, ParseConfig config)
        {
            Number tempVar = Common.ExtractSameTypeNumberXInfo(number);

            if (tempVar.Error != ErrorTypesNumber.None)
            {
                Error = tempVar.Error;
            }
            else
            {
                BaseTenExponent = number.BaseTenExponent;
                Value = number.Value;
                OriginalString = originalString;
                Config = new ParseConfig(config);
            }
        }

        internal NumberP(dynamic value, int baseTenExponent)
        {
            BaseTenExponent = baseTenExponent;
            Value = value;
        }

        internal NumberP(ErrorTypesNumber error) { Error = error; }
    }

    ///<summary>
    ///<para>ParseConfig defines the way in which the NumberP parsing actions are being performed.</para>
    ///</summary>
    public class ParseConfig
    {
        ///<summary><para>CultureInfo variable to be used while parsing. Its default value is CultureInfo.InvariantCulture.</para></summary>
        public CultureInfo Culture { get; set; }
        ///<summary><para>Member of the NumberStyles enum to be used while parsing. Its default value is NumberStyles.Any.</para></summary>
        public NumberStyles NumberStyle { get; set; }
        ///<summary><para>Member of the ParseTypes enum defining the parsing type. Its default value is ParseTypes.ParseAll.</para></summary>
        public ParseTypes ParseType { get; set; }
        ///<summary><para>Numeric type targeted by the parsing actions. Its default value is decimal.</para></summary>        
        public readonly Type Target;

        ///<summary>
        ///<para>Outputs the information in all the public fields (one per line).</para>
        ///</summary>
        public override string ToString()
        {
            string output = "Culture: " + Culture.DisplayName + Environment.NewLine;
            
            output += "Style: " + NumberStyle.ToString() + Environment.NewLine;
            output += "ParseType: " + ParseType.ToString() + Environment.NewLine;
            output += "Target: " + (Target != null ? Target.ToString() : "nothing");

            return output;
        }

        ///<summary><para>Initialises a new ParseConfig instance.</para></summary> 
        public ParseConfig()
        {
            Culture = CultureInfo.InvariantCulture;
            NumberStyle = NumberStyles.Any;
            ParseType = ParseTypes.ParseAll;
            Target = typeof(decimal);
        }

        ///<summary><para>Initialises a new ParseConfig instance.</para></summary>
        ///<param name="target">Type variable defining the numeric type targeted by the parsing actions.</param>  
        public ParseConfig(Type target)
        {
            Culture = CultureInfo.InvariantCulture;
            NumberStyle = NumberStyles.Any;
            ParseType = ParseTypes.ParseAll;
            Target =
            (
                Basic.AllNumericTypes.Contains(target) ?
                target : typeof(decimal)
            );
        }

        ///<summary><para>Initialises a new ParseConfig instance.</para></summary>
        ///<param name="config">ParseConfig variable whose information will be used.</param>  
        public ParseConfig(ParseConfig config)
        {
            if (config == null)
            {
                config = new ParseConfig();
                Culture = config.Culture;
                NumberStyle = config.NumberStyle;
                ParseType = config.ParseType;
                Target = config.Target;
                return;
            }

            Culture = new CultureInfo
            (
                (
                    config.Culture != null ? config.Culture :
                    CultureInfo.InvariantCulture
                )
                .LCID
            );
            NumberStyle = config.NumberStyle;
            ParseType = config.ParseType;
            Target =
            (
                config.Target != null ? config.Target : typeof(decimal)
            );
        }

        public static bool operator ==(ParseConfig first, ParseConfig second)
        {
            return Operations.NoNullEquals(first, second);
        }

        public static bool operator !=(ParseConfig first, ParseConfig second)
        {
            return !Operations.NoNullEquals(first, second);
        }

        public bool Equals(ParseConfig other)
        {
            return
            (
                object.Equals(other, null) ? false :
                Operations.ParseConfigsAreEqual(this, other)
            );
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ParseConfig);
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}
