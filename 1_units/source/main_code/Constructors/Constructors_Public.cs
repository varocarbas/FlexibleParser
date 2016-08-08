using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

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
        public readonly int BigNumberExponent;
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
            BigNumberExponent = unitP2.UnitInfo.BigNumberExponent;
            Unit = unitP2.UnitInfo.Unit;
            UnitType = unitP2.UnitType;
            UnitSystem = unitP2.UnitSystem;
            UnitPrefix = new Prefix(unitP2.UnitInfo.Prefix);
            UnitParts = unitP2.UnitInfo.Parts.AsReadOnly();
            UnitString = unitP2.UnitString;
            ValueAndUnitString = unitP2.ValueAndUnitString;

            Error =
            (
                unitP2.ErrorType != ErrorTypes.None ?
                //If applicable, this instantiation would trigger an exception right away.
                new ErrorInfo(unitP2.ErrorType, unitP2.ExceptionHandling) :
                new ErrorInfo()
            );
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
                valueAndUnit == null || !valueAndUnit.Contains(" ") || valueAndUnit.Trim().Length < 2 ?
                ErrorTypes.NumericParsingError : ErrorTypes.None
            );

            decimal value = 0m;
            string unit = "";
            int bigNumberExponent = 0;
            if (parsingError == ErrorTypes.None)
            {
                UnitInfo tempInfo = ParseValueAndUnit(valueAndUnit);
                value = tempInfo.Value;
                unit = tempInfo.TempString;
                bigNumberExponent = tempInfo.BigNumberExponent;
            }

            UnitPConstructor unitP2 = GetUnitP2(value, unit, exceptionHandling, prefixUsage);

            OriginalUnitString = unitP2.OriginalUnitString;
            Value = unitP2.Value;
            BigNumberExponent = unitP2.UnitInfo.BigNumberExponent + bigNumberExponent;
            Unit = unitP2.UnitInfo.Unit;
            UnitType = unitP2.UnitType;
            UnitSystem = unitP2.UnitSystem;
            UnitPrefix = new Prefix(unitP2.UnitInfo.Prefix.Factor, prefixUsage);
            UnitParts = unitP2.UnitInfo.Parts.AsReadOnly();
            UnitString = unitP2.UnitString;
            ValueAndUnitString = unitP2.ValueAndUnitString;

            Error =
            (
                parsingError != ErrorTypes.None || unitP2.ErrorType != ErrorTypes.None ?
                new ErrorInfo
                (
                    //If applicable, this instantiation would trigger an exception right away.
                    (parsingError != ErrorTypes.None ? parsingError : unitP2.ErrorType),
                    unitP2.ExceptionHandling
                ) 
                : new ErrorInfo()
            );
        }

        private UnitInfo ParseValueAndUnit(string valueAndUnit)
        {
            UnitInfo unitInfo = new UnitInfo();
            string[] parts = valueAndUnit.Split(' ');

            if (parts.Length >= 2)
            {
                decimal value = 0m;
                double valueDouble = 0.0;
                bool isOK = false;
                if (decimal.TryParse(parts[0], NumberStyles.Any, CultureInfo.InvariantCulture, out value))
                {
                    isOK = true;
                    unitInfo.Value = value;
                }
                else if (double.TryParse(parts[0], NumberStyles.Any, CultureInfo.InvariantCulture, out valueDouble))
                {
                    isOK = true;
                    unitInfo = ConvertDoubleToDecimal(valueDouble);
                }

                if (isOK)
                {
                    unitInfo.TempString = String.Join(" ", parts, 1, parts.Length - 1);
                }
            }

            return unitInfo;
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
            Value = value;
            Unit = unit;
            UnitType = GetTypeFromUnit(Unit);
            UnitSystem = GetSystemFromUnit(Unit);
            UnitPrefix = new Prefix(prefix);
            UnitInfo tempInfo = new UnitInfo(Value, Unit, UnitPrefix);
            UnitParts = GetPartsFromUnit(tempInfo).Parts.AsReadOnly();
            UnitString = GetUnitString(tempInfo);
            OriginalUnitString = UnitString;
            ValueAndUnitString = Value.ToString() + 
            (
                BigNumberExponent != 0 ? "*10^" + BigNumberExponent.ToString() : ""
            );
            ValueAndUnitString = ValueAndUnitString + " [" + UnitString + "]";

            Error =
            (
                unit == Units.None || unit == Units.Unitless || AllUnnamedUnits.ContainsValue(unit) ?
                //If applicable, this instantiation would trigger an exception right away.
                new ErrorInfo(ErrorTypes.InvalidUnit, exceptionHandling) :
                new ErrorInfo()
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
        : this(value, unitString, exceptionHandling, PrefixUsageTypes.DefaultUsage) { }

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