using System;
using System.Linq;
using System.Collections.Generic;

namespace FlexibleParser
{
    internal partial class TimeZonesInternal
    {
        public static string TimeZoneTypeToString(dynamic timeZone)
        {
            if (timeZone == null || timeZone.Error != ErrorTimeZoneEnum.None)
            {
                return "Invalid timezone";
            }

            string utc = "UTC " + OffsetInternal.OffsetToString(timeZone.Offset);
            if (timeZone.GetType() == typeof(TimeZoneUTC) || timeZone.GetType() == typeof(TimeZones))
            {
                return utc;
            }

            string output = timeZone.Name;
            if (timeZone.Abbreviation != null)
            {
                output += " (" + timeZone.Abbreviation + ")";
            }

            return output + " -- " + utc;
        }

        //Both arguments of GetTimeZoneTypeFromEnum are expected to be valid: a valid type of a timezone enum and
        //a non-null (but perhaps "None") item of that enum. 
        internal static dynamic GetTimeZoneClassFromEnum(dynamic enumItem, Type type)
        {
            if (EnumIsNothing(enumItem)) return null;

            if (type == typeof(TimeZoneOfficialEnum))
            {
                return new TimeZoneOfficial
                (
                    (TimeZoneOfficialEnum)enumItem
                );
            }
            else if (type == typeof(TimeZoneConventionalEnum))
            {
                return new TimeZoneConventional
                (
                    (TimeZoneConventionalEnum)enumItem
                );
            }
            else if (type == typeof(TimeZoneIANAEnum))
            {
                return new TimeZoneIANA
                (
                    (TimeZoneIANAEnum)enumItem
                );
            }
            else if (type == typeof(TimeZoneUTCEnum))
            {
                return new TimeZoneUTC
                (
                    (TimeZoneUTCEnum)enumItem
                );
            }
            else if (type == typeof(TimeZoneWindowsEnum))
            {
                return new TimeZoneWindows
                (
                    (TimeZoneWindowsEnum)enumItem
                );
            }
            else if (type == typeof(TimeZoneMilitaryEnum))
            {
                return new TimeZoneMilitary
                (
                    (TimeZoneMilitaryEnum)enumItem
                );
            }

            return null;
        }

        //Both arguments of GetEnumItemOffset are expected to be valid members of the corresponding 
        //types (i.e., different than "None" items of main-type-timezone enums).
        internal static Offset GetEnumItemOffset(dynamic input, Type type)
        {
            TimeZoneUTCEnum utc = TimeZoneUTCEnum.None;

            if (type == typeof(TimeZoneUTCEnum))
            {
                utc = input;
            }
            else
            {
                var temp = GetAssociatedMapItem(input, type);
                if (temp != null) utc = temp.UTCTimeZone;
            }

            return
            (
                utc == TimeZoneUTCEnum.None ? null : new Offset(utc)
            );
        }

        internal static dynamic GetFirstTypeItemFromMap(TimeZonesMainMap mapItem, Type type)
        {
            if (mapItem == null) return null;

            if (type == typeof(TimeZoneOfficialEnum))
            {
                return mapItem.OfficialTimeZones.First();
            }
            else if (type == typeof(TimeZoneIANAEnum))
            {
                return mapItem.IANATimeZones.First();
            }
            else if (type == typeof(TimeZoneConventionalEnum))
            {
                return mapItem.ConventionalTimeZones.First();
            }
            else if (type == typeof(TimeZoneUTCEnum))
            {
                return mapItem.UTCTimeZone;
            }
            else if (type == typeof(TimeZoneWindowsEnum))
            {
                return mapItem.WindowsTimeZone;
            }
            else if (type == typeof(TimeZoneMilitaryEnum))
            {
                return mapItem.MilitaryTimeZone;
            }

            return null;
        }

        //Both arguments of GetSpecificTypeFromMapItem are expected to be valid members of the corresponding 
        //types (i.e., valid map and type of one of the timezone enums).
        internal static dynamic GetSpecificTypeFromMapItem(TimeZonesMainMap map, Type target)
        {
            if (target == typeof(TimeZoneOfficialEnum))
            {
                return map.OfficialTimeZones;
            }
            else if (target == typeof(TimeZoneIANAEnum))
            {
                return map.IANATimeZones;
            }
            else if (target == typeof(TimeZoneConventionalEnum))
            {
                return map.ConventionalTimeZones;
            }
            else if (target == typeof(TimeZoneUTCEnum))
            {
                return map.UTCTimeZone;
            }
            else if (target == typeof(TimeZoneWindowsEnum))
            {
                return map.WindowsTimeZone;
            }
            else if (target == typeof(TimeZoneMilitaryEnum))
            {
                return map.MilitaryTimeZone;
            }

            return null;
        }

        //Both arguments of GetAssociatedMapItem are expected to be valid members of the corresponding 
        //types (i.e., different than "None" items of main-type-timezone enums).
        internal static TimeZonesMainMap GetAssociatedMapItem(dynamic input, Type type)
        {
            IEnumerable<TimeZonesMainMap> temp = GetAssociatedMapItems(input, type);

            return
            (
                temp == null || temp.Count() == 0 ? null : temp.First()
            );
        }

        //Both arguments of GetAssociatedMapItems are expected to be valid members of the corresponding 
        //types (i.e., different than "None" items of main-type-timezone enums).
        internal static IEnumerable<TimeZonesMainMap> GetAssociatedMapItems(dynamic input, Type type)
        {
            if (type == typeof(TimeZoneOfficialEnum))
            {
                return AllTimezonesInternal.Where
                (
                    x => x.OfficialTimeZones.Any(y => y == input)
                );
            }
            else if (type == typeof(TimeZoneIANAEnum))
            {
                return AllTimezonesInternal.Where
                (
                    x => x.IANATimeZones.Any(y => y == input)
                );
            }
            else if (type == typeof(TimeZoneConventionalEnum))
            {
                return AllTimezonesInternal.Where
                (
                    x => x.ConventionalTimeZones.Any(y => y == input)
                );
            }
            else if (type == typeof(TimeZoneUTCEnum))
            {
                return AllTimezonesInternal.Where
                (
                    x => x.UTCTimeZone == input
                );
            }
            else if (type == typeof(TimeZoneWindowsEnum))
            {
                return AllTimezonesInternal.Where
                (
                    x => x.WindowsTimeZone == input
                );
            }
            else if (type == typeof(TimeZoneMilitaryEnum))
            {
                return AllTimezonesInternal.Where
                (
                    x => x.MilitaryTimeZone == input
                );
            }

            return null;
        }

        //Both arguments of GetEnumItemName are expected to be valid members of the 
        //expected types (i.e., different than "None" items of main-type-timezone enums).
        internal static string GetEnumItemName(dynamic input, Type type)
        {
            return CorrectEnumString(input.ToString(), type);
        }

        //Both arguments of GetEnumItemAbbreviation are expected to be valid members of the 
        //expected types (i.e., different than "None" items of main-type-timezone enums).
        internal static string GetEnumItemAbbreviation(dynamic input, Type type)
        {
            if (type == typeof(TimeZoneOfficialEnum))
            {
                return TimeZoneOfficial.GetAbbreviationFromOfficial(input);
            }
            else if (type == typeof(TimeZoneConventionalEnum))
            {
                return
                (
                    TimeZoneConventionalInternal.TimeZoneConventionalAbbreviations.ContainsKey(input) ?
                    TimeZoneConventionalInternal.TimeZoneConventionalAbbreviations[input] : null
                );
            }
            else if (type == typeof(TimeZoneMilitaryEnum))
            {
                return TimeZoneMilitaryInternal.GetAbbreviationFromMilitaryTimeZone
                (
                    (TimeZoneMilitaryEnum)input
                );
            }

            return null;
        }

        private static TemporaryVariables GetGlobalValuesCommon(TimeZonesMainMap match, Type type)
        {
            TemporaryVariables outVars = new TemporaryVariables();

            if (type == typeof(TimeZoneOfficial))
            {
                outVars.Vars.Add
                (
                    new List<TimeZoneOfficial>() { match.OfficialTimeZones.First() }.AsReadOnly()
                );
            }
            else
            {
                outVars.Vars.Add
                (
                    new List<TimeZoneOfficial>
                    (
                        match.OfficialTimeZones.OrderBy(x => x.ToString()).Select(x => new TimeZoneOfficial(x))
                    )
                    .AsReadOnly()
                );
            }

            if (type == typeof(TimeZoneIANA))
            {
                outVars.Vars.Add
                (
                    new List<TimeZoneIANA>() { match.IANATimeZones.First() }.AsReadOnly()
                );
            }
            else
            {
                outVars.Vars.Add
                (
                    new List<TimeZoneIANA>
                    (
                        match.IANATimeZones.OrderBy(x => x.ToString()).Select(x => new TimeZoneIANA(x))
                    )
                    .AsReadOnly()
                );
            }

            if (type == typeof(TimeZoneConventionalEnum))
            {
                outVars.Vars.Add
                (
                    new List<TimeZoneConventionalEnum>() { match.ConventionalTimeZones.First() }.AsReadOnly()
                );
            }
            else
            {
                outVars.Vars.Add
                (
                    new List<TimeZoneConventional>
                    (
                        match.ConventionalTimeZones.OrderBy(x => x.ToString()).Select(x => new TimeZoneConventional(x))
                    )
                    .AsReadOnly()
                );
            }

            outVars.Vars.Add(match.WindowsTimeZone);
            outVars.Vars.Add(match.UTCTimeZone);
            outVars.Vars.Add(match.MilitaryTimeZone);

            return outVars;
        }

        internal static bool EnumIsNothing(dynamic enumItem)
        {
            if (enumItem == null) return true;

            return EnumIsNothing
            (
                enumItem, enumItem.GetType()
            );
        }

        private static bool EnumIsNothing(dynamic enumItem, Type type)
        {
            return
            (
                enumItem == null || !TimeZoneEnumTypes.Contains(type) ? 
                true : NoneItems[type] == enumItem
            );
        }

        internal static BasicTypes GetBasicType(dynamic item)
        {
            return 
            (
                item == null ? BasicTypes.None :
                GetBasicType(item.GetType())
            );
        }

        internal static BasicTypes GetBasicType(Type type)
        {
            string typeName = type.ToString().ToLower();

            if (typeName.Contains("official")) return BasicTypes.Official;
            else if (typeName.Contains("iana")) return BasicTypes.IANA;
            else if (typeName.Contains("conventional")) return BasicTypes.Conventional;
            else if (typeName.Contains("utc")) return BasicTypes.UTC;
            else if (typeName.Contains("windows")) return BasicTypes.Windows;
            else if (typeName.Contains("military")) return BasicTypes.Military;

            return BasicTypes.None;
        }

        internal enum BasicTypes 
        {
            None = 0,
            Official, IANA, Conventional,
            UTC, Windows, Military
        }
    }
}
