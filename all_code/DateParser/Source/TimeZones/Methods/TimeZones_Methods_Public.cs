using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FlexibleParser
{
    public partial class Offset
    {
        ///<summary><para>Returns an Offset list with all the valid timezone offsets.</para></summary>
        public static ReadOnlyCollection<Offset> GetAllValid()
        {
            List<Offset> outList = new List<Offset>();

            foreach (TimeZoneUTCEnum utc in Enum.GetValues(typeof(TimeZoneUTCEnum)))
            {
                outList.Add(new Offset(utc));
            }

            return outList.AsReadOnly();
        }

        ///<summary><para>Returns the TimeZoneUTC variable associated with the current instance.</para></summary>
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
        ///<summary><para>Returns a HourMinute list with all the valid timezone offsets.</para></summary>
        public static ReadOnlyCollection<HourMinute> GetAllValid()
        {
            List<HourMinute> outList = new List<HourMinute>();

            foreach (TimeZoneUTCEnum utc in Enum.GetValues(typeof(TimeZoneUTCEnum)))
            {
                outList.Add(new Offset(utc).HourMinute);
            }

            return outList.AsReadOnly();
        }

        ///<summary><para>Returns the decimal version of the offset associated with the current instance.</para></summary>
        public decimal ToDecimal()
        {
            return HourMinuteInternal.GetDecimalFromHourMinute(this);
        }
    }
}
