using System;
using System.Globalization;

namespace FlexibleParser
{
    public partial class DateP
    {
        public override string ToString()
        {
            return ToStringStandard();
        }

        public string ToStringStandard()
        {
            return ToStringStandard(null);
        }

        public string ToStringStandard(StandardDateTimeFormat standardFormat)
        {
            if (standardFormat == null)
            {
                standardFormat = new StandardDateTimeFormat();
            }

            return ToStringFinal
            (
                standardFormat.Patterns == null || standardFormat.Patterns.Length < 1 ?
                this.Value.ToString(standardFormat.FormatProvider) :
                this.Value.ToString(standardFormat.Patterns[0])
            );
        }

        public string ToStringCustom(CustomDateTimeFormat customFormat)
        {
            return ToStringFinal
            (
                customFormat == null ? ToString() :
                DatesInternal.ToStringCustomFormat(this.Value, customFormat)
            );
        }

        private string ToStringFinal(string outputSofar)
        {
            if (this.Error != ErrorDateEnum.None)
            {
                return
                (
                    this.Error == ErrorDateEnum.InvalidInput ?
                    "Invalid input" : "Parse error"
                );
            }

            return outputSofar + 
            (
                this.TimeZoneOffset == null ? "" : " " + 
                OffsetInternal.OffsetToString(this.TimeZoneOffset, true)
            );
        }
    }
}
