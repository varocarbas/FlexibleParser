using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FlexibleParser
{
    public partial class UnitP
    {
        ///<summary><para>Converts the current unit into the target one. Different unit types will trigger an error.</para></summary>
        ///<param name="targetUnit">Conversion target unit.</param>
        ///<param name="targetPrefix">Prefix of the conversion target unit.</param>
        public UnitP ConvertCurrentUnitTo(Units targetUnit, Prefix targetPrefix = null)
        {
            return ConvertToCommon(this, targetUnit, targetPrefix);
        }

        ///<summary><para>Converts the current unit into the target one. Different unit types will trigger an error.</para></summary>
        ///<param name="targetUnitString">String representation of the conversion target unit.</param>
        public UnitP ConvertCurrentUnitTo(string targetUnitString)
        {
            return ConvertToCommon(this, targetUnitString);
        }

        ///<summary><para>Returns the string representations associated with the current unit.</para></summary>
        ///<param name="otherStringsToo">When true, all the supported string representations (case doesn't matter) other than symbols (case matters) are also included.</param>  
        public ReadOnlyCollection<string> GetStringsForCurrentUnit(bool otherStringsToo = false)
        {
            return GetStringsUnitCommon(Unit, otherStringsToo).AsReadOnly();
        }

        ///<summary><para>Returns the string representations associated with the current unit type.</para></summary>
        ///<param name="otherStringsToo">When true, all the supported string representations (case doesn't matter) other than symbols (case matters) are also included.</param>        
        public ReadOnlyCollection<string> GetStringsForCurrentType(bool otherStringsToo = false)
        {
            return GetStringsTypeCommon(UnitType, otherStringsToo).AsReadOnly();
        }

        ///<summary><para>Returns the string representations associated with the current unit type and system.</para></summary>
        ///<param name="otherStringsToo">When true, all the supported string representations (case doesn't matter) other than symbols (case matters) are also included.</param>  
        public ReadOnlyCollection<string> GetStringsForCurrentTypeAndSystem(bool otherStringsToo = false)
        {
            return GetStringsTypeAndSystemCommon(UnitType, UnitSystem, otherStringsToo).AsReadOnly();
        }

        ///<summary><para>Returns the members of the Units enum which are associated with the current unit type.</para></summary> 
        public ReadOnlyCollection<Units> GetUnitsForCurrentType()
        {
            return GetUnitsTypeCommon(UnitType).AsReadOnly();
        }

        ///<summary><para>Returns the members of the Units enum which are associated with the current unit type and system.</para></summary>
        public ReadOnlyCollection<Units> GetUnitsForCurrentTypeAndSystem()
        {
            return GetUnitsTypeAndSystemCommon(UnitType, UnitSystem).AsReadOnly();
        }

        ///<summary><para>Returns the type of the current unit.</para></summary>
        public UnitTypes GetCurrentUnitType()
        {
            return GetTypeFromUnit(Unit);
        }

        ///<summary><para>Returns the system of the current unit.</para></summary>  
        public UnitSystems GetCurrentUnitSystem()
        {
            return GetSystemFromUnit(Unit, false, true);
        }

        ///<summary><para>Removes the global prefix of the current UnitP variable.</para></summary>  
        public UnitP RemoveCurrentGlobalPrefix()
        {
            return 
            (
                new UnitP
                (
                    //Normalising means converting prefix and value into BaseTenExponent.
                    //In the final output, the BaseTenExponent will be minimal (within the
                    //value capabilities) because of the subsequent improvements.
                    NormaliseUnitInfo(new UnitInfo(this)), this, true
                )
            );
        }

        ///<summary><para>Transfers all the base-ten exponent information to the Value field (if possible on account of the decimal type range limits).</para></summary>  
        public UnitP RemoveCurrentBaseTen()
        {
            UnitInfo tempInfo = ConvertBaseTenToValue(new UnitInfo(this));
            
            return new UnitP(this, tempInfo.Value, tempInfo.BaseTenExponent);
        }
    }
}
