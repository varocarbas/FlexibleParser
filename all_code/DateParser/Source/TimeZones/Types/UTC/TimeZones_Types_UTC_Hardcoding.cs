using System;
using System.Linq;
using System.Collections.Generic;

namespace FlexibleParser
{
    public enum TimeZoneUTCEnum
    {
        None = 0,
        Minus_12 = -12, Minus_11 = -11, Minus_10 = -10, Minus_9_30 = -20, Minus_9 = -9,
        Minus_8 = -8, Minus_7 = -7, Minus_6 = -6, Minus_5 = -5, Minus_4 = -4, 
        Minus_3_30 = -21, Minus_3 = -3, Minus_2_30 = -22, Minus_2 = -2, Minus_1 = -1,
        UTC = -23, //0 is reserved for the default item (None).
        Plus_1 = 1, Plus_2 = 2, Plus_3 = 3, Plus_3_30 = -24,
        Plus_4 = 4, Plus_4_30 = -25, Plus_5 = 5, Plus_5_30 = -26, Plus_5_45 = -27,
        Plus_6 = 6, Plus_6_30 = -28, Plus_7 = 7, Plus_8 = 8, Plus_8_30 = -29,
        Plus_8_45 = -30, Plus_9 = 9, Plus_9_30 = -31, Plus_10 = 10, Plus_10_30 = -32,
        Plus_11 = 11, Plus_12 = 12, Plus_12_45 = -33, Plus_13 = 13, Plus_13_45 = -34,
        Plus_14 = 14
    }

    internal partial class TimeZoneUTCInternal
    {
        //The integers assigned to most of the members of the TimeZoneUTCEnum enum are already defining the associated offset.
        //The dictionary EnumToDecimalSpecial accounts for all the cases which cannot be defined in that way.
        private static Dictionary<TimeZoneUTCEnum, decimal> EnumToDecimalSpecial = new Dictionary<TimeZoneUTCEnum, decimal>()
        {
            { TimeZoneUTCEnum.Minus_9_30, -9.5m }, { TimeZoneUTCEnum.Minus_3_30, -3.5m }, 
            { TimeZoneUTCEnum.Minus_2_30, -2.5m }, { TimeZoneUTCEnum.UTC, 0m },
            { TimeZoneUTCEnum.Plus_3_30, 3.5m }, { TimeZoneUTCEnum.Plus_4_30, 4.5m }, 
            { TimeZoneUTCEnum.Plus_5_30, 5.5m }, { TimeZoneUTCEnum.Plus_5_45, 5.75m }, 
            { TimeZoneUTCEnum.Plus_6_30, 6.5m }, { TimeZoneUTCEnum.Plus_8_30, 8.5m }, 
            { TimeZoneUTCEnum.Plus_8_45, 8.75m }, { TimeZoneUTCEnum.Plus_9_30, 9.5m }, 
            { TimeZoneUTCEnum.Plus_10_30, 10.5m }, { TimeZoneUTCEnum.Plus_12_45, 12.75m }, 
            { TimeZoneUTCEnum.Plus_13_45, 13.75m }
        };

        public static decimal GetDecimalOffsetFromEnum(TimeZoneUTCEnum utc)
        {
            decimal offset = (int)utc;

            return
            (
                offset <= -20 ? EnumToDecimalSpecial[utc] : offset
            );
        }
    }
}
