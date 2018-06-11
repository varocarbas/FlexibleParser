namespace FlexibleParser
{
    public partial class Number
    {
        ///<summary><para>Initialises a new Number instance.</para></summary>
        ///<param name="unitP">UnitParser's UnitP variable to be used.</param>
        public Number(dynamic unitP)
        {
            Number number = OtherParts.GetNumberFromUnitP(unitP);

            BaseTenExponent = number.BaseTenExponent;
            Value = number.Value;
            Error = number.Error;
        }
    }

    public partial class NumberO
    {
        ///<summary><para>Initialises a new NumberO instance.</para></summary>
        ///<param name="unitP">UnitParser's UnitP variable to be used.</param>
        public NumberO(dynamic unitP)
        {
            Number number = OtherParts.GetNumberFromUnitP(unitP);

            _OtherTypes = GetAssociatedTypes(OtherTypes.AllTypes);
            BaseTenExponent = number.BaseTenExponent;
            Value = number.Value;
            Error = number.Error;
        }
    }

    public partial class NumberP
    {
        ///<summary><para>Initialises a new NumberP instance.</para></summary>
        ///<param name="unitP">UnitParser's UnitP variable to be used.</param>
        public NumberP(dynamic unitP)
        {
            Number number = OtherParts.GetNumberFromUnitP(unitP);

            BaseTenExponent = number.BaseTenExponent;
            Value = number.Value;
            Error = number.Error;
        }
    }
}
