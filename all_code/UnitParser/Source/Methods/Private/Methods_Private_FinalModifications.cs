using System;
using System.Collections.Generic;

namespace FlexibleParser
{
    public partial class UnitP
    {
        private static UnitInfo ImproveUnitInfo(UnitInfo unitInfo, bool noPrefixImprovement)
        {
            if (unitInfo.Parts.Count == 0)
            {
                if (unitInfo.Prefix.Factor != 1m)
                {
                    unitInfo = NormaliseUnitInfo(unitInfo);
                }

                unitInfo.Unit = Units.Unitless;
                unitInfo.Prefix = new Prefix(1m, unitInfo.Prefix.PrefixUsage);
            }
            else if (Math.Abs(unitInfo.Value) < 1 && unitInfo.Prefix.Factor > 1)
            {
                unitInfo.Value = unitInfo.Value * unitInfo.Prefix.Factor;
                unitInfo.Prefix = new Prefix(unitInfo.Prefix.PrefixUsage);
            }

            if (!noPrefixImprovement)
            {
                unitInfo = ImprovePrefixes(unitInfo);
            }

            return ReduceBigValueExp(unitInfo);
        }

        private static UnitInfo ReduceBigValueExp(UnitInfo unitInfo)
        {
            if (unitInfo.BaseTenExponent == 0) return unitInfo;

            decimal maxVal = 1000000m;
            decimal minVal = 0.0001m;

            int sign = Math.Sign(unitInfo.Value);
            decimal absValue = Math.Abs(unitInfo.Value);

            if (unitInfo.BaseTenExponent > 0)
            {
                while (unitInfo.BaseTenExponent > 0 && absValue <= maxVal / 10)
                {
                    unitInfo.BaseTenExponent -= 1;
                    absValue *= 10;
                }
            }
            else
            {
                while (unitInfo.BaseTenExponent < 0 && absValue >= minVal * 10)
                {
                    unitInfo.BaseTenExponent += 1;
                    absValue /= 10;
                }
            }

            unitInfo.Value = sign * absValue;

            return unitInfo;
        }

        private static UnitInfo ImprovePrefixes(UnitInfo unitInfo)
        {
            if (unitInfo.Unit == Units.Unitless)
            {
                return NormaliseUnitInfo(unitInfo);
            }

            decimal absValue = Math.Abs(unitInfo.Value);
            bool valueIsOK = (absValue >= 0.001m && absValue <= 1000m);

            if (valueIsOK && unitInfo.BaseTenExponent == 0 && unitInfo.Prefix.Factor == 1m)
            {
                return unitInfo;
            }

            PrefixTypes prefixType =
            (
                unitInfo.Prefix.Type != PrefixTypes.None ?
                unitInfo.Prefix.Type : PrefixTypes.SI
            );

            bool prefixIsOK = PrefixCanBeUsedWithUnit(unitInfo, prefixType);

            if (!prefixIsOK || !valueIsOK || unitInfo.BaseTenExponent != 0)
            {
                unitInfo = NormaliseUnitInfo(unitInfo);

                if (prefixIsOK)
                {
                    unitInfo = GetBestPrefixForTarget
                    (
                        unitInfo, unitInfo.BaseTenExponent,
                        prefixType, true
                    );
                }
            }

            return CompensateBaseTenExponentWithPrefix(unitInfo);
        }

        private static UnitInfo CompensateBaseTenExponentWithPrefix(UnitInfo unitInfo)
        {
            if (unitInfo.BaseTenExponent == 0 || unitInfo.Prefix.Factor == 1m) return unitInfo;

            UnitInfo tempInfo = NormaliseUnitInfo
            (
                new UnitInfo(unitInfo) { Value = 1m }
            );

            tempInfo = GetBestPrefixForTarget
            (
                tempInfo, tempInfo.BaseTenExponent,
                unitInfo.Prefix.Type, true
            );

            unitInfo = new UnitInfo(unitInfo)
            {
                BaseTenExponent = tempInfo.BaseTenExponent,
                Prefix = new Prefix(tempInfo.Prefix),
                Value = unitInfo.Value
            };

            return PerformManagedOperationValues
            (
                unitInfo, tempInfo = new UnitInfo(tempInfo)
                {
                    BaseTenExponent = 0,
                    Prefix = new Prefix()
                },
                Operations.Multiplication
            );
        }
    }
}
