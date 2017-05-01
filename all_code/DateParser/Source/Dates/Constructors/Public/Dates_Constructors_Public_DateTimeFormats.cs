using System.Globalization;

namespace FlexibleParser
{
    public abstract partial class DateTimeFormat
    {
        private DateTimeFormatInfo _FormatProvider { get; set; }

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

        public DateTimeFormat() { }
    }

    public partial class CustomDateTimeFormat : DateTimeFormat
    {
        internal bool IgnoreIrrelevantCharacters { get; set; }
        private string _Pattern { get; set; }
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

        public CustomDateTimeFormat(string pattern) : this
        (
            pattern, CultureInfo.CurrentCulture.DateTimeFormat
        )
        { }

        public CustomDateTimeFormat(string pattern, DateTimeFormatInfo formatProvider) : this(pattern, formatProvider, false) { }

        public CustomDateTimeFormat(DateTimeParts[] patternParts) : this
        (
            GetPatternFromParts(patternParts), CultureInfo.CurrentCulture.DateTimeFormat, true
        )
        { }

        private CustomDateTimeFormat(string pattern, DateTimeFormatInfo formatProvider, bool ignoreIrrelevant)
        {
            Pattern = pattern;
            FormatProvider = formatProvider;
            IgnoreIrrelevantCharacters = ignoreIrrelevant;
        }

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
    }

    public partial class StandardDateTimeFormat : DateTimeFormat
    {
        public DateTimeStyles DateTimeStyle { get; set; }

        private string[] _Patterns { get; set; }
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

        public bool UseParseExact { get; set; }

        public StandardDateTimeFormat()
        {
            FormatProvider = (DateTimeFormatInfo)CultureInfo.CurrentCulture.DateTimeFormat.Clone();
        }

        public StandardDateTimeFormat(string pattern, bool useParseExact = false) : this
        (
            new string[] { pattern }, CultureInfo.CurrentCulture.DateTimeFormat, DateTimeStyles.None, useParseExact
        )
        { }

        public StandardDateTimeFormat(string[] patterns, bool useParseExact = false) : this
        (
            patterns, CultureInfo.CurrentCulture.DateTimeFormat, DateTimeStyles.None, useParseExact
        )
        { }

        public StandardDateTimeFormat(DateTimeFormatInfo formatProvider, bool useParseExact = false) : this
        (
            null, formatProvider, DateTimeStyles.None, useParseExact
        )
        { }

        public StandardDateTimeFormat(DateTimeStyles dateTimeStyle, bool useParseExact = false) : this
        (
            null, null, dateTimeStyle, useParseExact
        )
        { }

        public StandardDateTimeFormat(string pattern, DateTimeStyles dateTimeStyle, bool useParseExact = false) : this
        (
            new string[] { pattern }, CultureInfo.CurrentCulture.DateTimeFormat, dateTimeStyle, useParseExact
        )
        { }

        public StandardDateTimeFormat(string[] patterns, DateTimeStyles dateTimeStyle, bool useParseExact = false) : this
        (
            patterns, CultureInfo.CurrentCulture.DateTimeFormat, dateTimeStyle, useParseExact
        )
        { }

        public StandardDateTimeFormat
        (
            string[] patterns, DateTimeFormatInfo formatProvider, bool useParseExact = false
        )
        : this(patterns, formatProvider, DateTimeStyles.None, useParseExact) { }

        public StandardDateTimeFormat
        (
            DateTimeFormatInfo formatProvider, DateTimeStyles dateTimeStyle, bool useParseExact = false
        )
        : this(null, formatProvider, dateTimeStyle, useParseExact) { }

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
