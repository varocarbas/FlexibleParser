namespace FlexibleParser
{
    public partial class DateP
    {
        ///<summary><para>Returns the string representation of the current instance.</para></summary>
        public override string ToString()
        {
            return ToStringStandard();
        }

        ///<summary><para>Returns the string representation of the current instance.</para></summary>
        public string ToStringStandard()
        {
            return ToStringStandard(null);
        }

        ///<summary><para>Returns the string representation of the current instance.</para></summary>
        ///<param name="standardFormat">StandardDateTimeFormat variable to be used.</param>
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
                    Value.ToString(standardFormat.FormatProvider) :
                    Value.ToString(standardFormat.Patterns[0])
                ),
                this
            );
        }

        ///<summary><para>Returns the string representation of the current instance.</para></summary>
        ///<param name="customFormat">CustomDateTimeFormat variable to be used.</param>
        public string ToStringCustom(CustomDateTimeFormat customFormat)
        {
            return DatesInternal.ToStringFinal
            (
                (
                    customFormat == null ? ToString() :
                    DatesInternal.ToStringCustomFormat(Value, customFormat)
                ),
                this
            );
        }
    }
}
