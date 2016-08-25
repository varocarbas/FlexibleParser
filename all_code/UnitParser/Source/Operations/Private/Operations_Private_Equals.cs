using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    public partial class UnitP
    {
        private static bool UnitPVarsAreEqual(UnitP first, UnitP second)
        {
            return
            (
                UnitPNonUnitInfoAreEqual(first, second) &&
                UnitPUnitInfoAreEqual(first, second)
            );
        }

        private static bool UnitPNonUnitInfoAreEqual(UnitP first, UnitP second)
        {
            return (first.Error == second.Error);
        }

        private static bool UnitPUnitInfoAreEqual(UnitP first, UnitP second)
        {
            return UnitInfosAreEqual
            (
                new UnitInfo(first.Value, first.Unit, first.UnitPrefix),
                new UnitInfo(second.Value, second.Unit, second.UnitPrefix)
            );
        }

        private static bool NoNullEquals(UnitP first, UnitP second)
        {
            return
            (
                object.Equals(first, null) ? 
                object.Equals(second, null) :
                first.Equals(second)
            );
        }

        private static bool UnitInfosAreEqual(UnitInfo firstInfo, UnitInfo secondInfo, bool ignoreValues = false)
        {
            if (firstInfo.Error.Type != ErrorTypes.None || secondInfo.Error.Type != ErrorTypes.None)
            {
                return firstInfo.Error.Type == secondInfo.Error.Type;
            }

            UnitInfo firstInfo2 = NormaliseUnitInfo(firstInfo);
            UnitInfo secondInfo2 = NormaliseUnitInfo(secondInfo);
            
            return
            (
                (ignoreValues || UnitInfoValuesAreEqual(firstInfo2, secondInfo2)) &&
                UnitInfoUnitsAreEqual(firstInfo2, secondInfo2)
            );
        }

        //This method assumes that both inputs are normalised.
        private static bool UnitInfoValuesAreEqual(UnitInfo firstInfo, UnitInfo secondInfo)
        {
            return 
            (
                firstInfo.BaseTenExponent == secondInfo.BaseTenExponent &&
                firstInfo.Value == secondInfo.Value
            );
        }

        private static bool UnitInfoUnitsAreEqual(UnitInfo firstUnit, UnitInfo secondUnit)
        {
            return UnitPartListsAreEqual(firstUnit.Parts, secondUnit.Parts);
        }

        //This method expects fully expanded/simplified unit parts.
        private static bool UnitPartListsAreEqual(List<UnitPart> firstParts, List<UnitPart> secondParts)
        {
            if (firstParts.Count != secondParts.Count) return false;

            foreach (UnitPart firstPart in firstParts)
            {
                if (secondParts.FirstOrDefault(x => x == firstPart) == null)
                {
                    return false;
                }
            }

            return true;
        }

        internal static bool PrefixesAreEqual(Prefix first, Prefix second)
        {
            return 
            (
                first.Type == second.Type && 
                first.Factor == second.Factor
            );
        }

        internal static bool NoNullEquals(Prefix first, Prefix second)
        {
            return
            (
                object.Equals(first, null) ? 
                object.Equals(second, null) :
                first.Equals(second)
            );
        }

        internal static bool UnitPartsAreEqual(UnitPart first, UnitPart second)
        {
            return
            (
                first.Exponent == second.Exponent &&
                first.Unit == second.Unit && 
                first.Prefix.Factor == second.Prefix.Factor
            );
        }

        internal static bool NoNullEquals(UnitPart first, UnitPart second)
        {
            return
            (
                object.Equals(first, null) ? 
                object.Equals(second, null) :
                first.Equals(second)
            );
        }

        private static bool ErrorsAreEqual(ErrorInfo first, ErrorInfo second)
        {
            return
            (
                first.ExceptionHandling == second.ExceptionHandling &&
                first.Type == second.Type
            );
        }

        private static bool NoNullEquals(ErrorInfo first, ErrorInfo second)
        {
            return
            (
                object.Equals(first, null) ? 
                object.Equals(second, null) :
                first.Equals(second)
            );
        }

        private static bool CompoundPartsAreEqual(CompoundPart first, CompoundPart second)
        {
            return
            (
                first.Exponent == second.Exponent && first.Type == second.Type
            );
        }

        private static bool NoNullEquals(CompoundPart first, CompoundPart second)
        {
            return
            (
                object.Equals(first, null) ? 
                object.Equals(second, null) :
                first.Equals(second)
            );
        }
    }
}
