using System;

namespace FlexibleParser
{
    ///<summary><para>Abstract class with six implementations: TimeZoneOfficial, TimeZoneIANA, TimeZoneConventional, TimeZoneUTC, TimeZoneWindows, TimeZoneMilitary.</para></summary>
    public abstract class TimeZoneType
    {
        ///<summary><para>Name of the timezone.</para></summary>
        public readonly string Name;
        ///<summary><para>Official abbreviation of the timezone.</para></summary>
        public readonly string Abbreviation;
        ///<summary><para>Offset variable associated with the timezone.</para></summary>
        public readonly Offset Offset;
        ///<summary><para>Enum variable associated with the timezone.</para></summary>
        public readonly dynamic Value;
        ///<summary><para>TimeZoneInfo variable associated with the timezone.</para></summary>
        public readonly TimeZoneInfo TimeZoneInfo;
        ///<summary><para>Error associated with the current instance.</para></summary>
        public readonly ErrorTimeZoneEnum Error;

        ///<summary><para>Initialises a new TimeZoneType instance.</para></summary>
        ///<param name="input">Timezone-related information to be parsed.</param>
        ///<param name="type">Type associated with the current timezone.</param>
        public TimeZoneType(string input, Type type)
        {
            TimeZonesInternal.StartTimezones();

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
            TimeZonesInternal.StartTimezones();

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

        internal TimeZoneType(dynamic enumItem, Type type)
        {
            if (TimeZonesInternal.EnumIsNothing(enumItem))
            {
                Value = enumItem;
                Error = ErrorTimeZoneEnum.InvalidTimeZone;
                return;
            }

            Name = TimeZonesInternal.AllNames[enumItem];
            Abbreviation = TimeZonesInternal.GetEnumItemAbbreviation
            (
                enumItem, type
            );
            Offset = TimeZonesInternal.GetEnumItemOffset
            (
                enumItem, type
            );
            Value = enumItem;
            TimeZoneInfo = TimeZoneTypeInternal.GetTimeZoneInfo(Value);

            if (Offset == null)
            {
                Error = ErrorTimeZoneEnum.InvalidTimeZone;
            }
        }
    }
}
