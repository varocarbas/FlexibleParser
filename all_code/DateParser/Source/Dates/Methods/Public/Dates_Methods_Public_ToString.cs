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

            return DatesInternal.ToStringFinal
            (
                (
                    standardFormat.Patterns == null || standardFormat.Patterns.Length < 1 ?
                    this.Value.ToString(standardFormat.FormatProvider) :
                    this.Value.ToString(standardFormat.Patterns[0])
                ),
                this
            );
        }

        public string ToStringCustom(CustomDateTimeFormat customFormat)
        {
            return DatesInternal.ToStringFinal
            (
                (
                    customFormat == null ? ToString() :
                    DatesInternal.ToStringCustomFormat(this.Value, customFormat)
                ),
                this
            );
        }
    }
}
