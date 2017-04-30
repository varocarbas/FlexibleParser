using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    public abstract class TimeZoneType
    {
        public readonly string Name;
        public readonly string Abbreviation;
        public readonly Offset Offset;
        public readonly dynamic Value;
        public readonly TimeZoneInfo TimeZoneInfo;
        public readonly ErrorTimeZoneEnum Error;

        public TimeZoneType(string input, Type type)
        {
            TemporaryVariables temp = TimeZonesInternal.GetGlobalValuesFromString(input, type);
            if (temp == null || temp.Vars[3].GetType() != type)
            {
                Error = ErrorTimeZoneEnum.InvalidTimeZone;
                return;
            }

            int i = 0;
            Name = temp.Vars[i];
            i++;
            Abbreviation = temp.Vars[i];
            i++;
            Offset = temp.Vars[i];
            i++;
            Value = temp.Vars[i];
            TimeZoneInfo = TimeZoneTypeInternal.GetTimeZoneInfo(Value);

            if (Offset == null)
            {
                Error = ErrorTimeZoneEnum.InvalidTimeZone;
            }
        }

        internal TimeZoneType(TimeZoneInfo timeZoneInfo, Type type)
        {
            TimeZoneWindowsEnum windows = TimeZoneWindowsInternal.GetEnumFromTimeZoneInfo(timeZoneInfo);

            Name = TimeZonesInternal.AllNames[windows];
            Abbreviation = TimeZonesInternal.GetEnumItemAbbreviation
            (
                windows, typeof(TimeZoneWindowsEnum)
            );
            Offset = TimeZonesInternal.GetEnumItemOffset
            (
                windows, typeof(TimeZoneWindowsEnum)
            );

            if (type == typeof(TimeZoneWindowsEnum))
            {
                Value = windows;
            }
            else
            {
                var map = TimeZonesInternal.GetAssociatedMapItem
                (
                    windows, typeof(TimeZoneWindowsEnum)
                );
                if (map == null) return;

                Value = TimeZonesInternal.GetSpecificTypeFromMapItem(map, type);
            }

            TimeZoneInfo = timeZoneInfo;

            if (Offset == null)
            {
                Error = ErrorTimeZoneEnum.InvalidTimeZone;
            }
        }

        internal TimeZoneType(string name, string abbreviation, Offset offset, dynamic value)
        {
            Name = name;
            Abbreviation = abbreviation;
            Offset = offset;
            Value = value;
            TimeZoneInfo = TimeZoneTypeInternal.GetTimeZoneInfo(Value);

            if (Offset == null)
            {
                Error = ErrorTimeZoneEnum.InvalidTimeZone;
            }
        }
    }
}
