using System.Globalization;

namespace FlexibleParser
{
    ///<summary><para>Abstract class with two implementations: CustomDateTimeFormat and StandardDateTimeFormat.</para></summary>
    public abstract partial class DateTimeFormat
    {
        private DateTimeFormatInfo _FormatProvider { get; set; }

        ///<summary><para>DateTimeFormatInfo variable associated with the current instance.</para></summary>
        public DateTimeFormatInfo FormatProvider
        {
            get { return _FormatProvider; }
            set
            {
                _FormatProvider = (DateTimeFormatInfo)
                (
                    value == null ?
                    CultureInfo.CurrentCulture.DateTimeFormat :
                    value
                )
                .Clone();
            }
        }
    }

    ///<summary><para>Implementation of the DateTimeFormat class which deals with the custom approaches of this library.</para></summary>
    public partial class CustomDateTimeFormat : DateTimeFormat
    {
        internal bool IgnoreIrrelevantCharacters { get; set; }
        private string _Pattern { get; set; }

        ///<summary><para>Pattern determining how date/time information is stored in a string variable.</para></summary>
        public string Pattern
        {
            get { return _Pattern; }
            set
            {
                _Pattern = (value == null ? "" : value);
                Pattern2 = _Pattern.Trim().ToLower();
            }
        }
        internal string Pattern2 { get; set; }

        ///<summary><para>Initialises a new CustomDateTimeFormat instance.</para></summary>
        ///<param name="pattern">Pattern to use when extracting date/time information from strings.</param>
        public CustomDateTimeFormat(string pattern) : this
        (
            pattern, CultureInfo.CurrentCulture.DateTimeFormat
        )
        { }

        ///<summary><para>Initialises a new CustomDateTimeFormat instance.</para></summary>
        ///<param name="pattern">Pattern to use when extracting date/time information from strings.</param>
        ///<param name="formatProvider">DateTimeFormatInfo variable to be used.</param>
        public CustomDateTimeFormat(string pattern, DateTimeFormatInfo formatProvider) : this
        (
            pattern, formatProvider, false
        )
        { }

        ///<summary><para>Initialises a new CustomDateTimeFormat instance.</para></summary>
        ///<param name="patternParts">Date/time elements defining the separator-agnostic pattern to be used.</param>
        public CustomDateTimeFormat(DateTimeParts[] patternParts) : this
        (
            GetPatternFromParts(patternParts), CultureInfo.CurrentCulture.DateTimeFormat, true
        )
        { }

        ///<summary><para>Initialises a new CustomDateTimeFormat instance.</para></summary>
        ///<param name="customDateTimeFormat">CustomDateTimeFormat variable whose information will be used.</param>
        public CustomDateTimeFormat(CustomDateTimeFormat customDateTimeFormat)
        {
            if (customDateTimeFormat == null)
            {
                FormatProvider = CultureInfo.CurrentCulture.DateTimeFormat;
            }
            else
            {
                Pattern = customDateTimeFormat.Pattern;
                FormatProvider = customDateTimeFormat.FormatProvider;
                IgnoreIrrelevantCharacters = customDateTimeFormat.IgnoreIrrelevantCharacters;
            }
        }

        private CustomDateTimeFormat(string pattern, DateTimeFormatInfo formatProvider, bool ignoreIrrelevant)
        {
            Pattern = pattern;
            FormatProvider = formatProvider;
            IgnoreIrrelevantCharacters = ignoreIrrelevant;
        }
    }

    ///<summary>
    ///<para>Implementation of the DateTimeFormat class which deals with the default .NET approach.</para>
    ///<para>The public variables of this class emulate the arguments of the parsing methods of the .NET date variables.</para>
    ///</summary>
    public partial class StandardDateTimeFormat : DateTimeFormat
    {
        ///<summary><para>DateTimeStyles variable associated with the current instance.</para></summary>
        public DateTimeStyles DateTimeStyle { get; set; }

        private string[] _Patterns { get; set; }
        ///<summary><para>String patterns associated with the current instance.</para></summary>
        public string[] Patterns
        {
            get { return _Patterns; }
            set
            {
                _Patterns =
                (
                    value == null ? new string[0] :
                    (string[])value.Clone()
                );
            }
        }

        ///<summary><para>Flag which indicates the parsing behaviour of the current instance (i.e., Parse or ParseExact method).</para></summary>
        public bool UseParseExact { get; set; }

        ///<summary>
        ///<para>Initialises a new StandardTimeFormat instance.</para>
        ///<para>It takes all the information from CultureInfo.CurrentCulture.DateTimeFormat.</para>    
        ///</summary>
        public StandardDateTimeFormat()
        {
            FormatProvider = (DateTimeFormatInfo)CultureInfo.CurrentCulture.DateTimeFormat.Clone();
        }

        ///<summary><para>Initialises a new StandardTimeFormat instance.</para></summary>
        ///<param name="pattern">Pattern to be used.</param>
        ///<param name="useParseExact">Flag indicating whether it should behave as a \"ParseExact\" method or not.</param>
        public StandardDateTimeFormat(string pattern, bool useParseExact = false) : this
        (
            new string[] { pattern }, CultureInfo.CurrentCulture.DateTimeFormat, DateTimeStyles.None, useParseExact
        )
        { }


        ///<summary><para>Initialises a new StandardTimeFormat instance.</para></summary>
        ///<param name="patterns">Patterns to be used.</param>
        ///<param name="useParseExact">Flag indicating whether it should behave as a \"ParseExact\" method or not.</param>
        public StandardDateTimeFormat(string[] patterns, bool useParseExact = false) : this
        (
            patterns, CultureInfo.CurrentCulture.DateTimeFormat, DateTimeStyles.None, useParseExact
        )
        { }

        ///<summary><para>Initialises a new StandardTimeFormat instance.</para></summary>
        ///<param name="formatProvider">DateTimeFormatInfo variable to be used.</param>
        ///<param name="useParseExact">Flag indicating whether it should behave as a \"ParseExact\" method or not.</param>
        public StandardDateTimeFormat(DateTimeFormatInfo formatProvider, bool useParseExact = false) : this
        (
            null, formatProvider, DateTimeStyles.None, useParseExact
        )
        { }

        ///<summary><para>Initialises a new StandardTimeFormat instance.</para></summary>
        ///<param name="dateTimeStyle">DateTimeStyles variable to be used.</param>
        ///<param name="useParseExact">Flag indicating whether it should behave as a \"ParseExact\" method or not.</param>
        public StandardDateTimeFormat(DateTimeStyles dateTimeStyle, bool useParseExact = false) : this
        (
            null, null, dateTimeStyle, useParseExact
        )
        { }

        ///<summary><para>Initialises a new StandardTimeFormat instance.</para></summary>
        ///<param name="pattern">Pattern to be used.</param>
        ///<param name="dateTimeStyle">DateTimeStyles variable to be used.</param>       
        ///<param name="useParseExact">Flag indicating whether it should behave as a \"ParseExact\" method or not.</param>
        public StandardDateTimeFormat(string pattern, DateTimeStyles dateTimeStyle, bool useParseExact = false) : this
        (
            new string[] { pattern }, CultureInfo.CurrentCulture.DateTimeFormat, dateTimeStyle, useParseExact
        )
        { }

        ///<summary><para>Initialises a new StandardTimeFormat instance.</para></summary>
        ///<param name="patterns">Patterns to be used.</param>
        ///<param name="dateTimeStyle">DateTimeStyles variable to be used.</param>       
        ///<param name="useParseExact">Flag indicating whether it should behave as a \"ParseExact\" method or not.</param>
        public StandardDateTimeFormat(string[] patterns, DateTimeStyles dateTimeStyle, bool useParseExact = false) : this
        (
            patterns, CultureInfo.CurrentCulture.DateTimeFormat, dateTimeStyle, useParseExact
        )
        { }

        ///<summary><para>Initialises a new StandardTimeFormat instance.</para></summary>
        ///<param name="patterns">Patterns to be used.</param>
        ///<param name="formatProvider">DateTimeFormatInfo variable to be used.</param>
        ///<param name="useParseExact">Flag indicating whether it should behave as a \"ParseExact\" method or not.</param>
        public StandardDateTimeFormat
        (
            string[] patterns, DateTimeFormatInfo formatProvider, bool useParseExact = false
        )
        : this(patterns, formatProvider, DateTimeStyles.None, useParseExact) { }

        ///<summary><para>Initialises a new StandardTimeFormat instance.</para></summary>
        ///<param name="formatProvider">DateTimeFormatInfo variable to be used.</param>
        ///<param name="dateTimeStyle">DateTimeStyles variable to be used.</param> 
        ///<param name="useParseExact">Flag indicating whether it should behave as a \"ParseExact\" method or not.</param>
        public StandardDateTimeFormat
        (
            DateTimeFormatInfo formatProvider, DateTimeStyles dateTimeStyle, bool useParseExact = false
        )
        : this(null, formatProvider, dateTimeStyle, useParseExact) { }

        ///<summary><para>Initialises a new StandardTimeFormat instance.</para></summary>
        ///<param name="patterns">Patterns to be used.</param>
        ///<param name="formatProvider">DateTimeFormatInfo variable to be used.</param>
        ///<param name="dateTimeStyle">DateTimeStyles variable to be used.</param> 
        ///<param name="useParseExact">Flag indicating whether it should behave as a \"ParseExact\" method or not.</param>
        public StandardDateTimeFormat
        (
            string[] patterns, DateTimeFormatInfo formatProvider, DateTimeStyles dateTimeStyle, bool useParseExact
        )
        {
            Patterns = patterns;
            FormatProvider = formatProvider;
            DateTimeStyle = dateTimeStyle;
            UseParseExact = useParseExact;
        }

        ///<summary><para>Initialises a new StandardTimeFormat instance.</para></summary>
        ///<param name="standardDateTimeFormat">StandardDateTimeFormat variable whose information will be used.</param>
        public StandardDateTimeFormat(StandardDateTimeFormat standardDateTimeFormat)
        {
            if (standardDateTimeFormat == null)
            {
                FormatProvider = CultureInfo.CurrentCulture.DateTimeFormat;
                Patterns = null;
            }
            else
            {
                Patterns = standardDateTimeFormat.Patterns;
                FormatProvider = standardDateTimeFormat.FormatProvider;
                DateTimeStyle = standardDateTimeFormat.DateTimeStyle;
                UseParseExact = standardDateTimeFormat.UseParseExact;
            }
        }
    }
}
