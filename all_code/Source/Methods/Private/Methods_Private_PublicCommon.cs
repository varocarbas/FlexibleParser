using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    public partial class UnitP
    {
        private static List<Units> GetUnitsTypeCommon(UnitTypes type)
        {
            return
            (
                type == UnitTypes.None ? new List<Units>() :
                AllUnitConversionFactors.Where
                (
                    x => GetTypeFromUnit(x.Key) == type
                )
                .Select(x => x.Key).ToList()
            );
        }

        private static List<Units> GetUnitsTypeAndSystemCommon(UnitTypes type, UnitSystems system)
        {
            return 
            (
                type == UnitTypes.None ? new List<Units>() :
                AllUnitConversionFactors.Where
                (
                    x => GetTypeFromUnit(x.Key) == type &&
                    UnitBelongsToSystem(x.Key, system)
                )
                .Select(x => x.Key).ToList()
            );
        }

        private static bool UnitBelongsToSystem(Units unit, UnitSystems targetSystem)
        {
            UnitSystems system = GetSystemFromUnit(unit);

            return
            (
                system == targetSystem ? true :
                (
                    AllMetricEnglish[targetSystem] == UnitSystems.Imperial &&
                    AllMetricEnglish[system] == UnitSystems.Imperial &&
                    AllImperialAndUSCSUnits.Contains(unit)
                )
            );
        }

        private static List<string> GetStringsUnitCommon(Units unit, bool otherStringsToo)
        {
            return
            (
                unit == Units.None || unit == Units.Unitless || IsUnnamedUnit(unit) ?
                new List<string>() :
                GetUnitStringsCommon
                (
                    GetAllStringsSpecific
                    (
                        GetAllStrings(otherStringsToo), InputTypes.Unit, unit
                    )
                )
                .Distinct().ToList()
            );
        }

        private static List<string> GetStringsTypeCommon(UnitTypes type, bool otherStringsToo)
        {
            return
            (
                type == UnitTypes.None ? new List<string>() :
                GetUnitStringsCommon
                (
                    GetAllStringsSpecific
                    (
                        GetAllStrings(otherStringsToo), InputTypes.Type, Units.None, type
                    )
                )
            );
        }

        private static List<string> GetStringsTypeAndSystemCommon(UnitTypes type, UnitSystems system, bool otherStringsToo)
        {
            return
            (
                type == UnitTypes.None || system == UnitSystems.None ? 
                new List<string>() :
                GetUnitStringsCommon
                (
                    GetAllStringsSpecific
                    (
                        GetAllStrings(otherStringsToo),
                        InputTypes.TypeAndSystem, 
                        Units.None, type, system
                    )
                )
            );
        }

        private static IEnumerable<KeyValuePair<string, Units>> GetAllStrings(bool otherStringsToo)
        {
            //Symbols (case matters).
            List<KeyValuePair<string, Units>> allStrings = 
            AllUnitSymbols.Select(x => x).ToList();
            allStrings.AddRange(AllUnitSymbols2.Select(x => x));

            if (otherStringsToo)
            {
                //Further Strings (case doesn't matter).
                allStrings.AddRange(AllUnitStrings.Select(x => x));
            }

            return allStrings.OrderBy(x => x.Value);
        }

        private static IEnumerable<KeyValuePair<string, Units>> GetAllStringsSpecific
        (
            IEnumerable<KeyValuePair<string, Units>> allSymbols, InputTypes inputType,
            Units unit = Units.None, UnitTypes type = UnitTypes.None,
            UnitSystems system = UnitSystems.None
        )
        {
            if (inputType == InputTypes.Unit)
            {
                return allSymbols.Where(x => x.Value == unit);
            }
            else if (inputType == InputTypes.Type)
            {
                return allSymbols.Where(x => GetTypeFromUnit(x.Value) == type);
            }
            else if (inputType == InputTypes.TypeAndSystem)
            {
                return
                (
                    allSymbols.Where
                    (
                        x => GetTypeFromUnit(x.Value) == type &&
                        UnitBelongsToSystem(x.Value, system)
                    )
                );
            }

            return allSymbols;
        }

        private static List<string> GetUnitStringsCommon(IEnumerable<KeyValuePair<string, Units>> allStrings, string prefixAbbrev = "")
        {
            List<string> outList = new List<string>();

            foreach (var item in allStrings)
            {
                string item2 = item.Key;

                if (prefixAbbrev != "" && !AllUnitStrings.ContainsKey(item.Key))
                {
                    item2 = prefixAbbrev + item2;
                }

                outList.Add(item2);
            }

            return outList;
        }

        private static UnitP ConvertToCommon(UnitP original, string unitString)
        {
            return ConvertToCommon
            (
                original, StartUnitParse(new ParseInfo(1m, unitString)).UnitInfo
            );
        }

        private static UnitP ConvertToCommon(UnitP original, UnitInfo targetInfo)
        {
            ErrorTypes error = PrelimaryErrorCheckConversion(original, targetInfo);
            if (error != ErrorTypes.None) 
            {
                return new UnitP(original, error);
            }

            UnitInfo originalInfo = new UnitInfo(original);
            UnitInfo infoResult = ConvertUnit(originalInfo, targetInfo, false);

            return
            (
                infoResult.Error.Type != ErrorTypes.None ?
                new UnitP(original, infoResult.Error.Type) :
                new UnitP
                (
                    infoResult, original,
                    original.OriginalUnitString + " => " + GetUnitString(infoResult)
                )
            );
        }
    }

    internal enum InputTypes { Unit, Type, TypeAndSystem, System }
}
