using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FlexibleParser
{
    public partial class UnitP
    {
        ///<summary><para>Member of the Units enum which best suits the current conditions.</para></summary>
        public readonly Units Unit;
        ///<summary><para>Member of the UnitTypes enum which best suits the current conditions.</para></summary>
        public readonly UnitTypes UnitType;
        ///<summary><para>Member of the UnitSystems enum which best suits the current conditions.</para></summary>
        public readonly UnitSystems UnitSystem;        
        ///<summary><para>Prefix information affecting all the unit parts.</para></summary>
        public readonly Prefix UnitPrefix = new Prefix();
        ///<summary><para>List containing the basic unit parts which define the current unit.</para></summary>
        public ReadOnlyCollection<UnitPart> UnitParts;
        ///<summary><para>String variable including the unit information which was input at variable instantiation.</para></summary>
        public readonly string OriginalUnitString;
        ///<summary><para>String variable containing the symbol(s) best describing the current unit.</para></summary>
        public readonly string UnitString;
        ///<summary><para>String variable including both numeric and unit information associated with the current conditions.</para></summary>
        public readonly string ValueAndUnitString;
        ///<summary><para>Base-ten exponent used when dealing with too small/big numeric values.</para></summary>
        public readonly int BaseTenExponent;
        ///<summary><para>ErrorInfo variable containing all the error- and exception-related information.</para></summary>
        public readonly ErrorInfo Error;
        ///<summary><para>Decimal variable storing the primary numeric information under the current conditions.</para></summary>
        public decimal Value { get; set; }

        ///<summary><para>Initialises a new UnitP instance.</para></summary>
        ///<param name="value">Numeric value to be used.</param>
        ///<param name="unitString">Unit information to be parsed.</param>
        ///<param name="exceptionHandling">Member of the ExceptionHandlingTypes enum to be used.</param>
        ///<param name="prefixUsage">Member of the PrefixUsageTypes enum to be used.</param>
        public UnitP(decimal value, string unitString, ExceptionHandlingTypes exceptionHandling, PrefixUsageTypes prefixUsage)
        {
            UnitPConstructor unitP2 = GetUnitP2(value, unitString, exceptionHandling, prefixUsage);
            
            OriginalUnitString = unitP2.OriginalUnitString;
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
        ///<param name="valueAndUnit">String containing the value and unit information to be parsed.</param>
        ///<param name="exceptionHandling">Member of the ExceptionHandlingTypes enum to be used.</param>
        ///<param name="prefixUsage">Member of the PrefixUsageTypes enum to be used.</param>       
        public UnitP(string valueAndUnit, ExceptionHandlingTypes exceptionHandling, PrefixUsageTypes prefixUsage)
        {
            ErrorTypes parsingError = 
            (
                valueAndUnit == null ? ErrorTypes.NumericParsingError : ErrorTypes.None
            );

            UnitInfo unitInfo = new UnitInfo(0m, exceptionHandling, prefixUsage);
            string unitString = "";
            if (parsingError == ErrorTypes.None)
            {
                UnitInfo tempInfo = ParseValueAndUnit(valueAndUnit);
                if (tempInfo.Error.Type == ErrorTypes.None)
                {
                    unitString = tempInfo.TempString;
                    unitInfo.Value = tempInfo.Value;
                    unitInfo.BaseTenExponent = tempInfo.BaseTenExponent;
                }
                else parsingError = tempInfo.Error.Type;
            }

            if (parsingError != ErrorTypes.None && !valueAndUnit.Contains(" "))
            {
                //valueAndUnit is assumed to only contain unit information.
                parsingError = ErrorTypes.None;
                unitInfo.Value = 1m;
                unitString = valueAndUnit;
            }

            UnitPConstructor unitP2 = GetUnitP2(unitInfo, unitString);

            OriginalUnitString = unitP2.OriginalUnitString;
            Value = unitP2.Value;
            BaseTenExponent = unitP2.UnitInfo.BaseTenExponent;
            Unit = unitP2.UnitInfo.Unit;
            UnitType = unitP2.UnitType;
            UnitSystem = unitP2.UnitSystem;
            UnitPrefix = new Prefix(unitP2.UnitInfo.Prefix.Factor, prefixUsage);
            UnitParts = unitP2.UnitInfo.Parts.AsReadOnly();
            UnitString = unitP2.UnitString;
            ValueAndUnitString = unitP2.ValueAndUnitString;
            //If applicable, this instantiation would trigger an exception right away.
            Error = new ErrorInfo
            (
                (parsingError != ErrorTypes.None ? parsingError : unitP2.ErrorType),
                unitP2.ExceptionHandling
            );
        }

        ///<summary>
        ///<para>Initialises a new UnitP instance.</para>
        ///<para>Automatically assigned values:</para>
        ///<para>Unit = Units.Unitless</para>
        ///<para>Error.ExceptionHandling = ExceptionHandlingTypes.NeverTriggerException</para>
        ///<para>PrefixUsage = PrefixUsageTypes.DefaultUsage</para>
        ///</summary>
        ///<param name="value">Numeric value to be used.</param>
        public UnitP(decimal value = 1m) : this(value, Units.Unitless) { }

        ///<summary>
        ///<para>Initialises a new UnitP instance.</para>
        ///<para>Automatically assigned values:</para>
        ///<para>Value = 1m</para>
        ///<para>Error.ExceptionHandling = ExceptionHandlingTypes.NeverTriggerException</para>
        ///<para>PrefixUsage = PrefixUsageTypes.DefaultUsage</para>
        ///</summary>
        ///<param name="unit">Member of the Units enum to be used.</param>
        public UnitP(Units unit) : this(1m, unit) { }

        ///<summary>
        ///<para>Initialises a new UnitP instance.</para>
        ///</summary>
        ///<param name="unitP">unitP variable whose information will be used.</param>
        public UnitP(UnitP unitP) 
        {
            Value = unitP.Value;
            BaseTenExponent = unitP.BaseTenExponent;
            Unit = unitP.Unit;
            UnitType = unitP.UnitType;
            UnitSystem = unitP.UnitSystem;
            UnitPrefix = new Prefix(unitP.UnitPrefix);
            UnitParts = new List<UnitPart>(unitP.UnitParts.ToList()).AsReadOnly();
            UnitString = unitP.UnitString;
            OriginalUnitString = unitP.OriginalUnitString;
            ValueAndUnitString = unitP.ValueAndUnitString;
            Error = new ErrorInfo(unitP.Error);
        }

        ///<summary>
        ///<para>Initialises a new UnitP instance.</para>
        ///</summary>
        ///<param name="value">Numeric value to be used.</param>
        ///<param name="unit">Member of the Units enum to be used.</param>
        ///<param name="prefix">Prefix variable whose information will be used.</param>
        ///<param name="exceptionHandling">Member of the ExceptionHandlingTypes enum to be used.</param> 
        public UnitP(decimal value, Units unit, Prefix prefix, ExceptionHandlingTypes exceptionHandling)
        {
            ErrorTypes errorType = 
            (
                unit == Units.None || IsUnnamedUnit(unit) ?
                ErrorTypes.InvalidUnit : ErrorTypes.None
            );

            UnitInfo tempInfo = null;
            if (errorType == ErrorTypes.None)
            {
                //Getting the unit parts associated with the given unit.
                tempInfo = new UnitInfo(value, unit, prefix);

                if (tempInfo.Error.Type == ErrorTypes.None)
                {
                    tempInfo = ImproveUnitInfo(tempInfo, false);
                }
                else errorType = tempInfo.Error.Type;
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
                errorType, exceptionHandling
            );
        }

        ///<summary>
        ///<para>Initialises a new UnitP instance.</para>
        ///<para>Automatically assigned values:</para>
        ///<para>Error.ExceptionHandling = ExceptionHandlingTypes.NeverTriggerException</para>
        ///<para>PrefixUsage = PrefixUsageTypes.DefaultUsage</para>
        ///</summary>
        ///<param name="value">Numeric value to be used.</param>
        ///<param name="unitString">String containing the unit information to be parsed.</param>
        public UnitP(decimal value, string unitString) : this
        (
            value, unitString, ExceptionHandlingTypes.NeverTriggerException, 
            PrefixUsageTypes.DefaultUsage
        ) 
        { }

        ///<summary>
        ///<para>Initialises a new UnitP instance.</para>
        ///<para>Automatically assigned values:</para>
        ///<para>PrefixUsage = PrefixUsageTypes.DefaultUsage</para>
        ///</summary>
        ///<param name="value">Numeric value to be used.</param>
        ///<param name="unitString">String containing the unit information to be parsed.</param>
        ///<param name="exceptionHandling">Member of the ExceptionHandlingTypes enum to be used.</param>
        public UnitP
        (
            decimal value, string unitString, ExceptionHandlingTypes exceptionHandling
        ) 
        : this (value, unitString, exceptionHandling, PrefixUsageTypes.DefaultUsage) { }

        ///<summary>
        ///<para>Initialises a new UnitP instance.</para>
        ///<para>Automatically assigned values:</para>
        ///<para>Error.ExceptionHandling = ExceptionHandlingTypes.NeverTriggerException</para>
        ///</summary>
        ///<param name="value">Numeric value to be used.</param>
        ///<param name="unitString">String containing the unit information to be parsed.</param>
        ///<param name="prefixUsage">Member of the PrefixUsageTypes enum to be used.</param>    
        public UnitP(decimal value, string unitString, PrefixUsageTypes prefixUsage) : this
        (
            value, unitString, ExceptionHandlingTypes.NeverTriggerException, prefixUsage
        ) 
        { }

        ///<summary>
        ///<para>Initialises a new UnitP instance.</para>
        ///<para>Automatically assigned values:</para>
        ///<para>Error.ExceptionHandling = ExceptionHandlingTypes.NeverTriggerException</para>
        ///<para>PrefixUsage = PrefixUsageTypes.DefaultUsage</para>
        ///</summary>
        ///<param name="valueAndUnit">String containing the value and unit information to be parsed.</param>
        public UnitP(string valueAndUnit) : this
        (
            valueAndUnit, ExceptionHandlingTypes.NeverTriggerException, PrefixUsageTypes.DefaultUsage
        ) 
        { }

        ///<summary>
        ///<para>Initialises a new UnitP instance.</para>
        ///<para>Automatically assigned values:</para>
        ///<para>PrefixUsage = PrefixUsageTypes.DefaultUsage</para>
        ///</summary>
        ///<param name="valueAndUnit">String containing the value and unit information to be parsed.</param>
        ///<param name="exceptionHandling">Member of the ExceptionHandlingTypes enum to be used.</param>           
        public UnitP(string valueAndUnit, ExceptionHandlingTypes exceptionHandling) : this
        (
            valueAndUnit, exceptionHandling, PrefixUsageTypes.DefaultUsage
        ) 
        { }

        ///<summary>
        ///<para>Initialises a new UnitP instance.</para>
        ///<para>Automatically assigned values:</para>
        ///<para>Error.ExceptionHandling = ExceptionHandlingTypes.NeverTriggerException</para>
        ///</summary>
        ///<param name="valueAndUnit">String containing the value and unit information to be parsed.</param>
        ///<param name="prefixUsage">Member of the PrefixUsageTypes enum to be used.</param>          
        public UnitP(string valueAndUnit, PrefixUsageTypes prefixUsage) : this
        (
            valueAndUnit, ExceptionHandlingTypes.NeverTriggerException, prefixUsage
        ) 
        { }

        ///<summary>
        ///<para>Initialises a new UnitP instance.</para>
        ///<para>Automatically assigned values:</para>
        ///<para>Error.ExceptionHandling = ExceptionHandlingTypes.NeverTriggerException</para>
        ///</summary>
        ///<param name="value">Numeric value to be used.</param>
        ///<param name="unit">Member of the Units enum to be used.</param>
        ///<param name="prefix">Prefix variable whose information will be used.</param>
        public UnitP(decimal value, Units unit, Prefix prefix) : this
        (
            value, unit, prefix, ExceptionHandlingTypes.NeverTriggerException
        )
        { }

        ///<summary>
        ///<para>Initialises a new UnitP instance.</para>
        ///<para>Automatically assigned values:</para>
        ///<para>Error.ExceptionHandling = ExceptionHandlingTypes.NeverTriggerException</para>
        ///<para>PrefixUsage = PrefixUsageTypes.DefaultUsage</para>
        ///</summary>
        ///<param name="value">Numeric value to be used.</param>
        ///<param name="unit">Member of the Units enum to be used.</param>
        public UnitP(decimal value, Units unit) : this
        (
            value, unit, new Prefix(), ExceptionHandlingTypes.NeverTriggerException
        )
        { }

        ///<summary>
        ///<para>Initialises a new UnitP instance.</para>
        ///<para>Automatically assigned values:</para>
        ///<para>PrefixUsage = PrefixUsageTypes.DefaultUsage</para>
        ///</summary>
        ///<param name="value">Numeric value to be used.</param>
        ///<param name="unit">Member of the Units enum to be used.</param>
        ///<param name="exceptionHandling">Member of the ExceptionHandlingTypes enum to be used.</param> 
        public UnitP(decimal value, Units unit, ExceptionHandlingTypes exceptionHandling) : this
        (
            value, unit, new Prefix(), exceptionHandling
        )
        { }

        ///<summary>
        ///<para>Initialises a new UnitP instance.</para>
        ///<para>Automatically assigned values:</para>
        ///<para>Error.ExceptionHandling = ExceptionHandlingTypes.NeverTriggerException</para>
        ///</summary>
        ///<param name="value">Numeric value to be used.</param>
        ///<param name="unit">Member of the Units enum to be used.</param>
        ///<param name="prefixUsage">Member of the PrefixUsageTypes enum to be used.</param> 
        public UnitP(decimal value, Units unit, PrefixUsageTypes prefixUsage) : this
        (
            value, unit, new Prefix(), ExceptionHandlingTypes.NeverTriggerException
        )
        { }
    }
}