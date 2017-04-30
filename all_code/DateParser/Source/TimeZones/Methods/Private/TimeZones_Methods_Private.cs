using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FlexibleParser
{
    public partial class TimeZones
    {
        internal static dynamic FromOfficialCommon(TimeZoneOfficial official, Type mainType)
        {
            return FromTypeInternal(official, mainType);
        }

        internal static dynamic FromOfficialOnlyEnumCommon(TimeZoneOfficial official, Type mainType)
        {
            return FromTypeOnlyEnumInternal
            (
                official, mainType, !OnlyOneItemIsReturned(mainType)
            );
        }

        internal static dynamic FromIANACommon(TimeZoneIANA iana, Type mainType)
        {
            return FromTypeInternal(iana, mainType);
        }

        internal static dynamic FromIANAOnlyEnumCommon(TimeZoneIANA iana, Type mainType)
        {
            return FromTypeOnlyEnumInternal
            (
                iana, mainType, !OnlyOneItemIsReturned(mainType)
            );
        }

        internal static dynamic FromConventionalCommon(TimeZoneConventional conventional, Type mainType)
        {
            return FromTypeInternal(conventional, mainType);
        }

        internal static dynamic FromConventionalOnlyEnumCommon(TimeZoneConventional conventional, Type mainType)
        {
            return FromTypeOnlyEnumInternal
            (
                conventional, mainType, !OnlyOneItemIsReturned(mainType)
            );
        }

        internal static dynamic FromUTCCommon(TimeZoneUTC utc, Type mainType)
        {
            return FromTypeInternal(utc, mainType);
        }

        internal static dynamic FromUTCOnlyEnumCommon(TimeZoneUTC utc, Type mainType)
        {
            return FromTypeOnlyEnumInternal
            (
                utc, mainType, !OnlyOneItemIsReturned(mainType)
            );
        }

        internal static dynamic FromWindowsCommon(TimeZoneWindows windows, Type mainType)
        {
            return FromTypeInternal(windows, mainType);
        }

        internal static dynamic FromWindowsOnlyEnumCommon(TimeZoneWindows windows, Type mainType)
        {
            return FromTypeOnlyEnumInternal
            (
                windows, mainType, !OnlyOneItemIsReturned(mainType)
            );
        }

        internal static dynamic FromMilitaryCommon(TimeZoneMilitary military, Type mainType)
        {
            return FromTypeInternal(military, mainType);
        }

        internal static dynamic FromMilitaryOnlyEnumCommon(TimeZoneMilitary military, Type mainType)
        {
            return FromTypeOnlyEnumInternal
            (
                military, mainType, !OnlyOneItemIsReturned(mainType)
            );
        }

        private static bool OnlyOneItemIsReturned(Type mainType)
        {
            TimeZonesInternal.BasicTypes basicType = TimeZonesInternal.GetBasicType(mainType);
            
            return
            (
                basicType != TimeZonesInternal.BasicTypes.Official &&
                basicType != TimeZonesInternal.BasicTypes.IANA &&
                basicType != TimeZonesInternal.BasicTypes.Conventional
            );
        }

        private static dynamic FromTypeInternal(dynamic input, Type mainType)
        {
            dynamic output = StartMainTypeList(mainType);

            bool justOneItem = OnlyOneItemIsReturned(mainType);

            foreach (dynamic item in FromTypeOnlyEnumInternal(input, mainType))
            {
                dynamic item2 = InstantiateMainType(item, mainType);
                if (justOneItem) return item2;

                output.Add(item2);
            }

            return output.AsReadOnly();
        }

        private static dynamic StartMainTypeList(Type mainType)
        {
            if (mainType == typeof(TimeZoneOfficial))
            {
                return new List<TimeZoneOfficial>();
            }
            else if (mainType == typeof(TimeZoneIANA))
            {
                return new List<TimeZoneIANA>();
            }
            else if (mainType == typeof(TimeZoneConventional))
            {
                return new List<TimeZoneConventional>();
            }
            else if (mainType == typeof(TimeZoneUTC))
            {
                return new List<TimeZoneUTC>();
            }
            else if (mainType == typeof(TimeZoneWindows))
            {
                return new List<TimeZoneWindows>();
            }
            else if (mainType == typeof(TimeZoneMilitary))
            {
                return new List<TimeZoneMilitary>();
            }

            return null;
        }

        private static dynamic InstantiateMainType(dynamic input, Type mainType)
        {
            if (mainType == typeof(TimeZoneOfficial))
            {
                return new TimeZoneOfficial((TimeZoneOfficialEnum)input);
            }
            else if (mainType == typeof(TimeZoneIANA))
            {
                return new TimeZoneIANA((TimeZoneIANAEnum)input);
            }
            else if (mainType == typeof(TimeZoneConventional))
            {
                return new TimeZoneConventional((TimeZoneConventionalEnum)input);
            }
            else if (mainType == typeof(TimeZoneUTC))
            {
                return new TimeZoneUTC((TimeZoneUTCEnum)input);
            }
            else if (mainType == typeof(TimeZoneWindows))
            {
                return new TimeZoneWindows((TimeZoneWindowsEnum)input);
            }
            else if (mainType == typeof(TimeZoneMilitary))
            {
                return new TimeZoneMilitary((TimeZoneMilitaryEnum)input);
            }

            return null;
        }

        private static dynamic FromTypeOnlyEnumInternal(dynamic input, Type mainType, bool returnReadOnly = false)
        {
            dynamic output = GetEnumItems(input, mainType);

            return
            (
                returnReadOnly ? output.AsReadOnly() : output
            );
        }

        private static dynamic GetEnumItems(dynamic input, Type mainType)
        {
            TimeZonesMainMap output = null;
            dynamic value = input.Value;
            Type type = value.GetType();

            if (type == typeof(TimeZoneOfficialEnum))
            {
                output = TimeZonesInternal.AllTimezonesInternal.First
                (
                    x => x.OfficialTimeZones.Contains((TimeZoneOfficialEnum)value)
                );
            }
            else if (type == typeof(TimeZoneIANAEnum))
            {
                output = TimeZonesInternal.AllTimezonesInternal.First
                (
                    x => x.IANATimeZones.Contains((TimeZoneIANAEnum)value)
                );
            }
            else if (type == typeof(TimeZoneConventionalEnum))
            {
                output = TimeZonesInternal.AllTimezonesInternal.First
                (
                    x => x.ConventionalTimeZones.Contains((TimeZoneConventionalEnum)value)
                );
            }
            else if (type == typeof(TimeZoneUTCEnum))
            {
                output = TimeZonesInternal.AllTimezonesInternal.First
                (
                    x => x.UTCTimeZone == (TimeZoneUTCEnum)value
                );
            }
            else if (type == typeof(TimeZoneWindowsEnum))
            {
                output = TimeZonesInternal.AllTimezonesInternal.First
                (
                    x => x.WindowsTimeZone == (TimeZoneWindowsEnum)value
                );
            }
            else if (type == typeof(TimeZoneMilitaryEnum))
            {
                output = TimeZonesInternal.AllTimezonesInternal.First
                (
                    x => x.MilitaryTimeZone == (TimeZoneMilitaryEnum)value
                );
            }

            return GetEnumItemsType(output, mainType);
        }

        private static dynamic GetEnumItemsType(dynamic output, Type mainType)
        {
            if (mainType == typeof(TimeZoneOfficial))
            {
                return
                (
                    (IEnumerable<TimeZoneOfficialEnum>)
                    output.OfficialTimezones
                )
                .ToList();
            }
            else if (mainType == typeof(TimeZoneIANA))
            {
                return 
                (
                    (IEnumerable<TimeZoneIANAEnum>)
                    output.IANATimeZones
                )
                .ToList();
            }
            else if (mainType == typeof(TimeZoneConventional))
            {
                return
                (
                    (IEnumerable<TimeZoneConventionalEnum>)
                    output.ConventionalTimezones
                )
                .ToList();
            }
            else if (mainType == typeof(TimeZoneUTC))
            {
                return new List<TimeZoneUTCEnum>() 
                {
                    output.UTCTimeZone
                };
            }
            else if (mainType == typeof(TimeZoneWindows))
            {
                return new List<TimeZoneWindowsEnum>() 
                {
                    output.WindowsTimeZone
                };
            }
            else if (mainType == typeof(TimeZoneMilitary))
            {
                return new List<TimeZoneMilitaryEnum>() 
                {
                    output.MilitaryTimeZone
                };
            }

            return null;
        }
    }
}
