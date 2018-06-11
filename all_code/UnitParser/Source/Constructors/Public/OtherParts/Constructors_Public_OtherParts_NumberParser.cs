using System.Collections.Generic;

namespace FlexibleParser
{
    public partial class UnitP
    {
        ///<summary>
        ///<para>Initialises a new UnitP instance.</para>
        ///<para>Automatically assigned values:</para>
        ///<para>Unit = Units.Unitless</para>
        ///<para>Error.ExceptionHandling = ExceptionHandlingTypes.NeverTriggerException</para>
        ///<para>PrefixUsage = PrefixUsageTypes.DefaultUsage</para>
        ///</summary>
        ///<param name="numberX">NumberParser's Number, NumberD, NumberO or NumberP variable to be used.</param>
        public UnitP(dynamic numberX) 
        {
            UnitInfo unitInfo = OtherParts.GetUnitInfoFromNumberX(numberX);
            if (unitInfo.Error.Type == ErrorTypes.None)
            {
                unitInfo.Unit = Units.Unitless;
            }
            UnitPConstructor unitP2 = new UnitPConstructor("", unitInfo);

            Value = unitP2.Value;
            BaseTenExponent = unitP2.UnitInfo.BaseTenExponent;
            Unit = unitP2.UnitInfo.Unit;
            UnitType = unitP2.UnitType;
            UnitSystem = unitP2.UnitSystem;
            UnitPrefix = new Prefix(unitP2.UnitInfo.Prefix);
            UnitParts = unitP2.UnitInfo.Parts.AsReadOnly();
            UnitString = unitP2.UnitString;
            ValueAndUnitString = unitP2.ValueAndUnitString;
            //If applicable, this instantiation would trigger an exception right away.
            Error = new ErrorInfo(unitP2.ErrorType, unitP2.ExceptionHandling);
        }

        ///<summary>
        ///<para>Initialises a new UnitP instance.</para>
        ///</summary>
        ///<param name="numberX">NumberParser's Number, NumberD, NumberO or NumberP variable to be used.</param>
        ///<param name="unitString">String containing the unit information to be parsed.</param>
        ///<param name="exceptionHandling">Member of the ExceptionHandlingTypes enum to be used.</param>
        ///<param name="prefixUsage">Member of the PrefixUsageTypes enum to be used.</param>
        public UnitP
        (
            dynamic numberX, string unitString,
            ExceptionHandlingTypes exceptionHandling = ExceptionHandlingTypes.NeverTriggerException,
            PrefixUsageTypes prefixUsage = PrefixUsageTypes.DefaultUsage
        )
        {
            UnitPConstructor unitP2 = GetUnitP2
            (
                OtherParts.GetUnitInfoFromNumberX(numberX, exceptionHandling, prefixUsage), unitString
            );

            Value = unitP2.Value;
            BaseTenExponent = unitP2.UnitInfo.BaseTenExponent;
            Unit = unitP2.UnitInfo.Unit;
            UnitType = unitP2.UnitType;
            UnitSystem = unitP2.UnitSystem;
            UnitPrefix = new Prefix(unitP2.UnitInfo.Prefix);
            UnitParts = unitP2.UnitInfo.Parts.AsReadOnly();
            UnitString = unitP2.UnitString;
            ValueAndUnitString = unitP2.ValueAndUnitString;
            //If applicable, this instantiation would trigger an exception right away.
            Error = new ErrorInfo(unitP2.ErrorType, unitP2.ExceptionHandling);
        }

        ///<summary>
        ///<para>Initialises a new UnitP instance.</para>       
        ///</summary>
        ///<param name="numberX">NumberParser's Number, NumberD, NumberO or NumberP variable to be used.</param>
        ///<param name="unit">Member of the Units enum to be used.</param>
        ///<param name="prefix">Prefix variable whose information will be used.</param>
        ///<param name="exceptionHandling">Member of the ExceptionHandlingTypes enum to be used.</param>
        ///<param name="prefixUsage">Member of the PrefixUsageTypes enum to be used.</param>
        public UnitP
        (
            dynamic numberX, Units unit, Prefix prefix = null, 
            ExceptionHandlingTypes exceptionHandling = ExceptionHandlingTypes.NeverTriggerException, 
            PrefixUsageTypes prefixUsage = PrefixUsageTypes.DefaultUsage
        )
        {
            if (prefix == null) prefix = new Prefix();

            ErrorTypes errorType =
            (
                unit == Units.None || IsUnnamedUnit(unit) ?
                ErrorTypes.InvalidUnit : ErrorTypes.None
            );

            UnitInfo tempInfo = null;
            if (errorType == ErrorTypes.None)
            {
                tempInfo = OtherParts.GetUnitInfoFromNumberX
                (
                    numberX, ExceptionHandlingTypes.NeverTriggerException, prefix.PrefixUsage
                );

                if (tempInfo.Error.Type == ErrorTypes.None)
                {
                    //Getting the unit parts associated with the given unit.
                    tempInfo.Unit = unit;
                    tempInfo.Prefix = new Prefix(prefix);
                    tempInfo.Parts = GetPartsFromUnit(tempInfo).Parts;

                    if (tempInfo.Error.Type == ErrorTypes.None)
                    {
                        tempInfo = ImproveUnitInfo(tempInfo, false);
                    }
                }

                errorType = tempInfo.Error.Type;
            }

            if (errorType != ErrorTypes.None)
            {
                Value = 0m;
                BaseTenExponent = 0;
                UnitPrefix = new Prefix(prefix.PrefixUsage);
                UnitParts = new List<UnitPart>().AsReadOnly();
            }
            else
            {
                Value = tempInfo.Value;
                BaseTenExponent = tempInfo.BaseTenExponent;
                Unit = unit;
                UnitType = GetTypeFromUnit(Unit);
                UnitSystem = GetSystemFromUnit(Unit);
                UnitPrefix = new Prefix(prefix);
                UnitParts = tempInfo.Parts.AsReadOnly();
                UnitString = GetUnitString(tempInfo);
                OriginalUnitString = UnitString;
                ValueAndUnitString = Value.ToString() + " " + UnitString;
            }

            //If applicable, this instantiation would trigger an exception right away.
            Error = new ErrorInfo
            (
                errorType, ExceptionHandlingTypes.NeverTriggerException
            );
        }
    }
}
