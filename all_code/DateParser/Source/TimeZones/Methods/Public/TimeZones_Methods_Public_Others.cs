using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FlexibleParser
{
    public partial class Offset
    {
        public static ReadOnlyCollection<Offset> GetAllValid()
        {
            List<Offset> outList = new List<Offset>();

            foreach (TimeZoneUTCEnum utc in Enum.GetValues(typeof(TimeZoneUTCEnum)))
            {
                outList.Add(new Offset(utc));
            }

            return outList.AsReadOnly();
        }

        public TimeZoneUTC ToUTC()
        {
            return TimeZoneUTCInternal.GetTimeZoneUTCFromDecimalOffset
            (
                this.DecimalOffset
            );
        }
    }

    public partial class HourMinute
    {
        public static ReadOnlyCollection<HourMinute> GetAllValid()
        {
            List<HourMinute> outList = new List<HourMinute>();

            foreach (TimeZoneUTCEnum utc in Enum.GetValues(typeof(TimeZoneUTCEnum)))
            {
                outList.Add(new Offset(utc).HourMinute);
            }

            return outList.AsReadOnly();
        }

        public decimal ToDecimal()
        {
            return HourMinuteInternal.GetDecimalFromHourMinute(this);
        }
    }
}
