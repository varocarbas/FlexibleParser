using System.Collections.ObjectModel;

namespace FlexibleParser
{
    public partial class UnitP
    {
        ///<summary><para>Converts the input unit into the target one. Different unit types will trigger an error.</para></summary>
        ///<param name="unitP">unitP variable whose unit will be converted.</param>
        ///<param name="targetUnit">Conversion target unit.</param>
        ///<param name="targetPrefix">Prefix of the conversion target unit.</param>
        public static UnitP ConvertTo(UnitP unitP, Units targetUnit, Prefix targetPrefix = null)
        {
            return ConvertToCommon(unitP, targetUnit, targetPrefix);
        }

        ///<summary><para>Converts the input unit into the target one. Different unit types will trigger an error.</para></summary>
        ///<param name="unitP">unitP variable whose unit will be converted.</param>
        ///<param name="targetUnitString">String representation of the conversion target unit.</param>
        public static UnitP ConvertTo(UnitP unitP, string targetUnitString)
        {
            return ConvertToCommon(unitP, targetUnitString);
        }

        ///<summary><para>Returns the string representations associated with the input unit.</para></summary>
        ///<param name="unit">Unit whose string representations will be returned.</param>  
        ///<param name="otherStringsToo">When true, all the supported string representations (case doesn't matter) other than symbols (case matters) are also included.</param>  
        public static ReadOnlyCollection<string> GetStringsForUnit(Units unit, bool otherStringsToo = false)
        {
            return GetStringsUnitCommon(unit, otherStringsToo).AsReadOnly();
        }

        ///<summary><para>Returns the string representations associated with the input unit type.</para></summary>
        ///<param name="unitType">Type of the unit string representations to be returned.</param>  
        ///<param name="otherStringsToo">When true, all the supported string representations (case doesn't matter) other than symbols (case matters) are also included.</param>  
        public static ReadOnlyCollection<string> GetStringsForType(UnitTypes unitType, bool otherStringsToo = false)
        {
            return GetStringsTypeCommon(unitType, otherStringsToo).AsReadOnly();
        }

        ///<summary><para>Returns the string representations associated with the input unit type and system.</para></summary>
        ///<param name="unitType">Type of the unit string representations to be returned.</param>  
        ///<param name="unitSystem">System of the unit string representations to be returned.</param>          
        ///<param name="otherStringsToo">When true, all the supported string representations (case doesn't matter) other than symbols (case matters) are also included.</param>  
        public static ReadOnlyCollection<string> GetStringsForTypeAndSystem(UnitTypes unitType, UnitSystems unitSystem, bool otherStringsToo = false)
        {
            return GetStringsTypeAndSystemCommon(unitType, unitSystem, otherStringsToo).AsReadOnly();
        }

        ///<summary><para>Returns the members of the Units enum which are associated with the input unit type.</para></summary>
        ///<param name="unitType">Type of the units to be returned.</param>  
        public static ReadOnlyCollection<Units> GetUnitsForType(UnitTypes unitType)
        {
            return GetUnitsTypeCommon(unitType).AsReadOnly();
        }

        ///<summary><para>Returns the members of the Units enum which are associated with the input unit type and system.</para></summary>
        ///<param name="unitType">Type of the units to be returned.</param>  
        ///<param name="unitSystem">System of the units to be returned.</param>  
        public static ReadOnlyCollection<Units> GetUnitsForTypeAndSystem(UnitTypes unitType, UnitSystems unitSystem)
        {
            return GetUnitsTypeAndSystemCommon(unitType, unitSystem).AsReadOnly();
        }

        ///<summary><para>Returns the member of the UnitTypes enum which is associated with the input unit.</para></summary>
        ///<param name="unit">Unit whose type will be returned.</param>  
        public static UnitTypes GetUnitType(Units unit)
        {
            return GetTypeFromUnit(unit);
        }

        ///<summary><para>Returns the member of the UnitSystems enum which is associated with the input unit.</para></summary>
        ///<param name="unit">Unit whose system will be returned.</param>  
        public static UnitSystems GetUnitSystem(Units unit)
        {
            return GetSystemFromUnit(unit, false, true);
        }

        ///<summary><para>Removes the global prefix of the input UnitP variable.</para></summary>
        ///<param name="unitP">UnitP variable whose prefix will be removed.</param>  
        public static UnitP RemoveGlobalPrefix(UnitP unitP)
        {
            return new UnitP
            (
                NormaliseUnitInfo(new UnitInfo(unitP)), unitP, true
            );
        }

        ///<summary><para>Transfers all the base-ten exponent information to the Value field (if possible).</para></summary>  
        ///<param name="unitP">UnitP variable whose base-ten exponent will be removed.</param>  
        public static UnitP RemoveBaseTen(UnitP unitP)
        {
            UnitInfo tempInfo = ConvertBaseTenToValue(new UnitInfo(unitP));

            return new UnitP(unitP, tempInfo.Value, tempInfo.BaseTenExponent);
        }
    }
}
