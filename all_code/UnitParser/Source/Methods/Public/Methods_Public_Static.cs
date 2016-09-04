using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FlexibleParser
{
    public partial class UnitP
    {
        ///<summary><para>Converts the input unit into the target one.</para><para>Warning: different unit types will trigger an error.</para></summary>
        ///<param name="unitP">unitP variable containing the original unit information.</param>
        ///<param name="targetUnit">Target unit.</param>
        ///<param name="targetPrefix">Target unit prefix.</param>
        public static UnitP ConvertTo(UnitP unitP, Units targetUnit, Prefix targetPrefix = null)
        {
            return ConvertToCommon(unitP, targetUnit, targetPrefix);
        }

        ///<summary><para>Converts the current variable unit into the target one.</para><para>Warning: different unit types will trigger an error.</para></summary>
        ///<param name="unitP">unitP variable containing the unit information to be converted.</param>
        ///<param name="targetUnitString">Target unit string.</param>
        public static UnitP ConvertTo(UnitP unitP, string targetUnitString)
        {
            return ConvertToCommon(unitP, targetUnitString);
        }

        ///<summary><para>Returns the string representations associated with the input unit.</para></summary>
        ///<param name="unit">Unit.</param>  
        ///<param name="otherStringsToo">When true, all the supported string representations (case doesn't matter) other than symbols (case matters) are also included.</param>  
        public static ReadOnlyCollection<string> GetStringsForUnit(Units unit, bool otherStringsToo = false)
        {
            return GetStringsUnitCommon(unit, otherStringsToo).AsReadOnly();
        }

        ///<summary><para>Returns the string representations associated with the input unit type.</para></summary>
        ///<param name="unitType">Unit type.</param>  
        ///<param name="otherStringsToo">When true, all the supported string representations (case doesn't matter) other than symbols (case matters) are also included.</param>  
        public static ReadOnlyCollection<string> GetStringsForType(UnitTypes unitType, bool otherStringsToo = false)
        {
            return GetStringsTypeCommon(unitType, otherStringsToo).AsReadOnly();
        }

        ///<summary><para>Returns the string representations associated with the input unit type and system.</para></summary>
        ///<param name="unitType">Unit type.</param>  
        ///<param name="unitSystem">Unit system.</param>          
        ///<param name="otherStringsToo">When true, all the supported string representations (case doesn't matter) other than symbols (case matters) are also included.</param>  
        public static ReadOnlyCollection<string> GetStringsForTypeAndSystem(UnitTypes unitType, UnitSystems unitSystem, bool otherStringsToo = false)
        {
            return GetStringsTypeAndSystemCommon(unitType, unitSystem, otherStringsToo).AsReadOnly();
        }

        ///<summary><para>Returns the members of the Units enum which are associated with the input unit type.</para></summary>
        ///<param name="unitType">Unit type.</param>  
        public static ReadOnlyCollection<Units> GetUnitsForType(UnitTypes unitType)
        {
            return GetUnitsTypeCommon(unitType).AsReadOnly();
        }

        ///<summary><para>Returns the members of the Units enum which are associated with the input unit type and system.</para></summary>
        ///<param name="unitType">Unit type.</param>  
        ///<param name="unitSystem">Unit system.</param>  
        public static ReadOnlyCollection<Units> GetUnitsForTypeAndSystem(UnitTypes unitType, UnitSystems unitSystem)
        {
            return GetUnitsTypeAndSystemCommon(unitType, unitSystem).AsReadOnly();
        }

        ///<summary><para>Returns the type of the input unit.</para></summary>
        ///<param name="unit">Unit.</param>  
        public static UnitTypes GetUnitType(Units unit)
        {
            return GetTypeFromUnit(unit);
        }

        ///<summary><para>Returns the system of the input unit.</para></summary>
        ///<param name="unit">Unit.</param>  
        public static UnitSystems GetUnitSystem(Units unit)
        {
            return GetSystemFromUnit(unit, false, true);
        }

        ///<summary><para>Removes all the prefixes of the input UnitP variable.</para></summary>
        ///<param name="unitP">UnitP variable.</param>  
        public static UnitP RemoveGlobalPrefixesFromVariable(UnitP unitP)
        {
            return
            (
                new UnitP
                (
                    //Normalising means converting prefix and value into BaseTenExponent.
                    //In the final output, the BaseTenExponent will be minimal (within the
                    //value capabilities) because of the subsequent improvements.
                    NormaliseUnitInfo(new UnitInfo(unitP)), unitP, true
                )
            );
        }
    }
}
