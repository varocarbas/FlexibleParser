﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FlexibleParser
{
    public partial class UnitP
    {
        ///<summary><para>Unit.</para></summary>
        public readonly Units Unit;
        ///<summary><para>Type of the unit.</para></summary>
        public readonly UnitTypes UnitType;
        ///<summary><para>System of the unit.</para></summary>
        public readonly UnitSystems UnitSystem;        
        ///<summary><para>Prefix of the unit (if applicable).</para></summary>
        public readonly Prefix UnitPrefix = new Prefix();
        ///<summary><para>Constituent parts of the unit.</para></summary>
        public ReadOnlyCollection<UnitPart> UnitParts;
        ///<summary><para>Input string to parse.</para></summary>
        public readonly string OriginalUnitString;
        ///<summary><para>Unit in string format.</para></summary>
        public readonly string UnitString;
        ///<summary><para>Unit and value in string format.</para></summary>
        public readonly string ValueAndUnitString;
        ///<summary><para>Base-10 exponent helping to deal with too small/big numbers.</para></summary>
        public readonly int BaseTenExponent;
        ///<summary><para>Error management.</para></summary>
        public readonly ErrorInfo Error;
        ///<summary><para>Main value associated with the unit.</para></summary>
        public decimal Value { get; set; }

        ///<summary><para>Initialises a new instance of UnitP.</para></summary>
        ///<param name="value">Numerical value associated with the given unit.</param>
        ///<param name="unitString">Unit information to be parsed.</param>
        ///<param name="exceptionHandling">Exception handling definition.</param>
        ///<param name="prefixUsage">Prefix usage definition.</param>
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
        ///<para>Initialises a new instance of UnitP.</para>
        ///</summary>
        ///<param name="valueAndUnit">String containing the value and unit information to be parsed. Expected format: number, separating space and unit.</param>
        ///<param name="exceptionHandling">Exception handling definition.</param>
        ///<param name="prefixUsage">Prefix usage definition.</param>       
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

        private UnitInfo ParseValueAndUnit(string valueAndUnit)
        {
            UnitInfo unitInfo = new UnitInfo();
            string[] parts = valueAndUnit.Trim().Split(' ');

            //Note that ParseDecimal can deal with any number (i.e., decimal, double or beyond double).
            if (parts.Length >= 2)
            {
                unitInfo = ParseDecimal(parts[0]);

                if (unitInfo.Error.Type == ErrorTypes.None)
                {
                    unitInfo.TempString = String.Join(" ", parts, 1, parts.Length - 1);
                }
            }
            else if (parts.Length == 1)
            {
                unitInfo = ParseDecimal(valueAndUnit);

                if (unitInfo.Error.Type == ErrorTypes.None)
                {
                    unitInfo.TempString = "Unitless";
                }
            }

            return unitInfo;
        }

        ///<summary>
        ///<para>Initialises a new instance of UnitP.</para>
        ///<para>Automatically assigned values:</para>
        ///<para>Unit = Units.Unitless</para>
        ///<para>Error.ExceptionHandling = ExceptionHandlingTypes.NeverTriggerException</para>
        ///<para>PrefixUsage = PrefixUsageTypes.DefaultUsage</para>
        ///</summary>
        ///<param name="value">Numerical value associated with the given unit.</param>
        public UnitP(decimal value = 0m) : this(value, Units.Unitless) { }

        ///<summary>
        ///<para>Initialises a new instance of UnitP.</para>
        ///<para>Automatically assigned values:</para>
        ///<para>Value = 1m</para>
        ///<para>Error.ExceptionHandling = ExceptionHandlingTypes.NeverTriggerException</para>
        ///<para>PrefixUsage = PrefixUsageTypes.DefaultUsage</para>
        ///</summary>
        ///<param name="unit">Unit.</param>
        public UnitP(Units unit) : this(1m, unit) { }

        ///<summary>
        ///<para>Initialises a new instance of UnitP.</para>
        ///</summary>
        ///<param name="unitP">unitP variable whose information will be used.</param> 
        ///<param name="prefixUsage">Prefix usage definition.</param> 
        ///<param name="exceptionHandling">Exception handling definition.</param> 
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
        ///<para>Initialises a new instance of UnitP.</para>
        ///</summary>
        ///<param name="value">Numerical value associated with the given unit.</param>
        ///<param name="unit">Unit.</param>
        ///<param name="prefix">Prefix associated with the unit.</param>
        ///<param name="exceptionHandling">Exception handling definition.</param>
        ///<param name="prefixUsage">Prefix usage definition.</param>     
        public UnitP
        (
            decimal value, Units unit, Prefix prefix, ExceptionHandlingTypes exceptionHandling, 
            PrefixUsageTypes prefixUsage
        )
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
                    //While getting the unit parts, some automatic conversions might have been performed
                    //and the associated values have to be taken into account.
                    tempInfo *= value;
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
        ///<para>Initialises a new instance of UnitP.</para>
        ///<para>Automatically assigned values:</para>
        ///<para>Error.ExceptionHandling = ExceptionHandlingTypes.NeverTriggerException</para>
        ///<para>PrefixUsage = PrefixUsageTypes.DefaultUsage</para>
        ///</summary>
        ///<param name="value">Numerical value associated with the given unit.</param>
        ///<param name="unitString">String containing the unit information to be parsed.</param>
        public UnitP(decimal value, string unitString) : this
        (
            value, unitString, ExceptionHandlingTypes.NeverTriggerException, 
            PrefixUsageTypes.DefaultUsage
        ) 
        { }

        ///<summary>
        ///<para>Initialises a new instance of UnitP.</para>
        ///<para>Automatically assigned values:</para>
        ///<para>PrefixUsage = PrefixUsageTypes.DefaultUsage</para>
        ///</summary>
        ///<param name="value">Numerical value associated with the given unit.</param>
        ///<param name="unitString">String containing the unit information to be parsed.</param>
        ///<param name="exceptionHandling">Exception handling definition.</param>
        public UnitP
        (
            decimal value, string unitString, ExceptionHandlingTypes exceptionHandling
        ) 
        : this (value, unitString, exceptionHandling, PrefixUsageTypes.DefaultUsage) { }

        ///<summary>
        ///<para>Initialises a new instance of UnitP.</para>
        ///<para>Automatically assigned values:</para>
        ///<para>Error.ExceptionHandling = ExceptionHandlingTypes.NeverTriggerException</para>
        ///</summary>
        ///<param name="value">Numerical value associated with the given unit.</param>
        ///<param name="unitString">String containing the unit information to be parsed.</param>
        ///<param name="prefixUsage">Prefix usage definition.</param>    
        public UnitP(decimal value, string unitString, PrefixUsageTypes prefixUsage) : this
        (
            value, unitString, ExceptionHandlingTypes.NeverTriggerException, prefixUsage
        ) 
        { }

        ///<summary>
        ///<para>Initialises a new instance of UnitP.</para>
        ///<para>Automatically assigned values:</para>
        ///<para>Error.ExceptionHandling = ExceptionHandlingTypes.NeverTriggerException</para>
        ///<para>PrefixUsage = PrefixUsageTypes.DefaultUsage</para>
        ///</summary>
        ///<param name="valueAndUnit">String containing the value and unit information to be parsed. Expected format: number, separating space and unit.</param>
        public UnitP(string valueAndUnit) : this
        (
            valueAndUnit, ExceptionHandlingTypes.NeverTriggerException, PrefixUsageTypes.DefaultUsage
        ) 
        { }

        ///<summary>
        ///<para>Initialises a new instance of UnitP.</para>
        ///<para>Automatically assigned values:</para>
        ///<para>PrefixUsage = PrefixUsageTypes.DefaultUsage</para>
        ///</summary>
        ///<param name="valueAndUnit">String containing the value and unit information to be parsed. Expected format: number, separating space and unit.</param>
        ///<param name="exceptionHandling">Exception handling definition.</param>           
        public UnitP(string valueAndUnit, ExceptionHandlingTypes exceptionHandling) : this
        (
            valueAndUnit, exceptionHandling, PrefixUsageTypes.DefaultUsage
        ) 
        { }

        ///<summary>
        ///<para>Initialises a new instance of UnitP.</para>
        ///<para>Automatically assigned values:</para>
        ///<para>Error.ExceptionHandling = ExceptionHandlingTypes.NeverTriggerException</para>
        ///</summary>
        ///<param name="valueAndUnit">String containing the value and unit information to be parsed. Expected format: number, separating space and unit.</param>
        ///<param name="prefixUsage">Prefix usage definition.</param>          
        public UnitP(string valueAndUnit, PrefixUsageTypes prefixUsage) : this
        (
            valueAndUnit, ExceptionHandlingTypes.NeverTriggerException, prefixUsage
        ) 
        { }

        ///<summary>
        ///<para>Initialises a new instance of UnitP.</para>
        ///<para>Automatically assigned values:</para>
        ///<para>Error.ExceptionHandling = ExceptionHandlingTypes.NeverTriggerException</para>
        ///<para>PrefixUsage = PrefixUsageTypes.DefaultUsage</para>
        ///</summary>
        ///<param name="value">Numerical value associated with the given unit.</param>
        ///<param name="unit">Unit.</param>
        ///<param name="prefix">Prefix associated with the unit.</param>
        public UnitP(decimal value, Units unit, Prefix prefix) : this
        (
            value, unit, prefix, ExceptionHandlingTypes.NeverTriggerException, 
            PrefixUsageTypes.DefaultUsage
        )
        { }

        ///<summary>
        ///<para>Initialises a new instance of UnitP.</para>
        ///<para>Automatically assigned values:</para>
        ///<para>Error.ExceptionHandling = ExceptionHandlingTypes.NeverTriggerException</para>
        ///<para>PrefixUsage = PrefixUsageTypes.DefaultUsage</para>
        ///</summary>
        ///<param name="value">Numerical value associated with the given unit.</param>
        ///<param name="unit">Unit.</param>
        ///<param name="prefixFactor">Factor defining the prefix associated with the unit (Prefix.Factor).</param>
        public UnitP(decimal value, Units unit, decimal prefixFactor) : this
        (
            value, unit, new Prefix(prefixFactor), ExceptionHandlingTypes.NeverTriggerException,
            PrefixUsageTypes.DefaultUsage
        )
        { }

        ///<summary>
        ///<para>Initialises a new instance of UnitP.</para>
        ///<para>Automatically assigned values:</para>
        ///<para>Error.ExceptionHandling = ExceptionHandlingTypes.NeverTriggerException</para>
        ///<para>PrefixUsage = PrefixUsageTypes.DefaultUsage</para>
        ///<para>Prefix.Factor = 1m</para>
        ///</summary>
        ///<param name="value">Numerical value associated with the given unit.</param>
        ///<param name="unit">Unit.</param>
        public UnitP(decimal value, Units unit) : this
        (
            value, unit, new Prefix(), ExceptionHandlingTypes.NeverTriggerException,
            PrefixUsageTypes.DefaultUsage
        )
        { }

        ///<summary>
        ///<para>Initialises a new instance of UnitP.</para>
        ///<para>Automatically assigned values:</para>
        ///<para>PrefixUsage = PrefixUsageTypes.DefaultUsage</para>
        ///</summary>
        ///<param name="value">Numerical value associated with the given unit.</param>
        ///<param name="unit">Unit.</param>
        ///<param name="prefix">Prefix associated with the unit.</param>
        ///<param name="exceptionHandling">Exception handling definition.</param> 
        public UnitP(decimal value, Units unit, Prefix prefix, ExceptionHandlingTypes exceptionHandling) : this
        (
            value, unit, prefix, exceptionHandling, PrefixUsageTypes.DefaultUsage
        )
        { }

        ///<summary>
        ///<para>Initialises a new instance of UnitP.</para>
        ///<para>Automatically assigned values:</para>
        ///<para>PrefixUsage = PrefixUsageTypes.DefaultUsage</para>
        ///</summary>
        ///<param name="value">Numerical value associated with the given unit.</param>
        ///<param name="unit">Unit.</param>
        ///<param name="prefixFactor">Factor defining the prefix associated with the unit (Prefix.Factor).</param>
        ///<param name="exceptionHandling">Exception handling definition.</param> 
        public UnitP(decimal value, Units unit, decimal prefixFactor, ExceptionHandlingTypes exceptionHandling) : this
        (
            value, unit, new Prefix(prefixFactor), exceptionHandling, 
            PrefixUsageTypes.DefaultUsage
        )
        { }

        ///<summary>
        ///<para>Initialises a new instance of UnitP.</para>
        ///<para>Automatically assigned values:</para>
        ///<para>PrefixUsage = PrefixUsageTypes.DefaultUsage</para>
        ///<para>Prefix.Factor = 1m</para>
        ///</summary>
        ///<param name="value">Numerical value associated with the given unit.</param>
        ///<param name="unit">Unit.</param>
        ///<param name="exceptionHandling">Exception handling definition.</param> 
        public UnitP(decimal value, Units unit, ExceptionHandlingTypes exceptionHandling) : this
        (
            value, unit, new Prefix(), exceptionHandling, PrefixUsageTypes.DefaultUsage
        )
        { }

        ///<summary>
        ///<para>Initialises a new instance of UnitP.</para>
        ///<para>Automatically assigned values:</para>
        ///<para>Error.ExceptionHandling = ExceptionHandlingTypes.NeverTriggerException</para>
        ///</summary>
        ///<param name="value">Numerical value associated with the given unit.</param>
        ///<param name="unit">Unit.</param>
        ///<param name="prefix">Prefix associated with the unit.</param>
        ///<param name="prefixUsage">Prefix usage definition.</param> 
        public UnitP(decimal value, Units unit, Prefix prefix, PrefixUsageTypes prefixUsage) : this
        (
            value, unit, prefix, ExceptionHandlingTypes.NeverTriggerException,
            prefixUsage
        )
        { }

        ///<summary>
        ///<para>Initialises a new instance of UnitP.</para>
        ///<para>Automatically assigned values:</para>
        ///<para>Error.ExceptionHandling = ExceptionHandlingTypes.NeverTriggerException</para>
        ///</summary>
        ///<param name="value">Numerical value associated with the given unit.</param>
        ///<param name="unit">Unit.</param>
        ///<param name="prefixFactor">Factor defining the prefix associated with the unit (Prefix.Factor).</param>
        ///<param name="prefixUsage">Prefix usage definition.</param> 
        public UnitP(decimal value, Units unit, decimal prefixFactor, PrefixUsageTypes prefixUsage) : this
        (
            value, unit, new Prefix(prefixFactor),
            ExceptionHandlingTypes.NeverTriggerException, prefixUsage
        )
        { }

        ///<summary>
        ///<para>Initialises a new instance of UnitP.</para>
        ///<para>Automatically assigned values:</para>
        ///<para>Error.ExceptionHandling = ExceptionHandlingTypes.NeverTriggerException</para>
        ///<para>Prefix.Factor = 1m</para>
        ///</summary>
        ///<param name="value">Numerical value associated with the given unit.</param>
        ///<param name="unit">Unit.</param>
        ///<param name="prefixUsage">Prefix usage definition.</param> 
        public UnitP(decimal value, Units unit, PrefixUsageTypes prefixUsage) : this
        (
            value, unit, new Prefix(), ExceptionHandlingTypes.NeverTriggerException, 
            prefixUsage
        )
        { }
    }
}