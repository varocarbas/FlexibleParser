using System.Collections.Generic;

namespace FlexibleParser
{
    ///<summary><para>All the UTC timezones.</para></summary>
    public enum TimeZoneUTCEnum
    {
        ///<summary><para>None.</para></summary>
        None = 0,
        ///<summary><para>UTC -12:00.</para></summary>
        Minus_12 = -12,
        ///<summary><para>UTC -11:00.</para></summary>
        Minus_11 = -11,
        ///<summary><para>UTC -10:00.</para></summary>
        Minus_10 = -10,
        ///<summary><para>UTC -09:30.</para></summary>
        Minus_9_30 = -20,
        ///<summary><para>UTC -09:00.</para></summary>
        Minus_9 = -9,
        ///<summary><para>UTC -08:00.</para></summary>
        Minus_8 = -8,
        ///<summary><para>UTC -07:00.</para></summary>
        Minus_7 = -7,
        ///<summary><para>UTC -06:00.</para></summary>
        Minus_6 = -6,
        ///<summary><para>UTC -05:00.</para></summary>
        Minus_5 = -5,
        ///<summary><para>UTC -04:00.</para></summary>
        Minus_4 = -4,
        ///<summary><para>UTC -03:30.</para></summary>
        Minus_3_30 = -21,
        ///<summary><para>UTC -03:00.</para></summary>
        Minus_3 = -3,
        ///<summary><para>UTC -02:30.</para></summary>
        Minus_2_30 = -22,
        ///<summary><para>UTC -02:00.</para></summary>
        Minus_2 = -2,
        ///<summary><para>UTC -01:00.</para></summary>
        Minus_1 = -1,
        ///<summary><para>UTC +00:00.</para></summary>
        UTC = -23, //0 is reserved for the default item (None).
        ///<summary><para>UTC +01:00.</para></summary>
        Plus_1 = 1,
        ///<summary><para>UTC +02:00.</para></summary>
        Plus_2 = 2,
        ///<summary><para>UTC +03:00.</para></summary>
        Plus_3 = 3,
        ///<summary><para>UTC +03:30.</para></summary>
        Plus_3_30 = -24,
        ///<summary><para>UTC +04:00.</para></summary>
        Plus_4 = 4,
        ///<summary><para>UTC +04:30.</para></summary>
        Plus_4_30 = -25,
        ///<summary><para>UTC +05:00.</para></summary>
        Plus_5 = 5,
        ///<summary><para>UTC +05:30.</para></summary>
        Plus_5_30 = -26,
        ///<summary><para>UTC +05:45.</para></summary>
        Plus_5_45 = -27,
        ///<summary><para>UTC +06:00.</para></summary>
        Plus_6 = 6,
        ///<summary><para>UTC +06:30.</para></summary>
        Plus_6_30 = -28,
        ///<summary><para>UTC +07:00.</para></summary>
        Plus_7 = 7,
        ///<summary><para>UTC +08:00.</para></summary>
        Plus_8 = 8,
        ///<summary><para>UTC +08:30.</para></summary>
        Plus_8_30 = -29,
        ///<summary><para>UTC +08:45.</para></summary>
        Plus_8_45 = -30,
        ///<summary><para>UTC +09:00.</para></summary>
        Plus_9 = 9,
        ///<summary><para>UTC +09:30.</para></summary>
        Plus_9_30 = -31,
        ///<summary><para>UTC +10:00.</para></summary>
        Plus_10 = 10,
        ///<summary><para>UTC +10:30.</para></summary>
        Plus_10_30 = -32,
        ///<summary><para>UTC +11:00.</para></summary>
        Plus_11 = 11,
        ///<summary><para>UTC +12:00.</para></summary>
        Plus_12 = 12,
        ///<summary><para>UTC +12:45.</para></summary>
        Plus_12_45 = -33,
        ///<summary><para>UTC +13:00.</para></summary>
        Plus_13 = 13,
        ///<summary><para>UTC +13:45.</para></summary>
        Plus_13_45 = -34,
        ///<summary><para>UTC +14:00.</para></summary>
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
