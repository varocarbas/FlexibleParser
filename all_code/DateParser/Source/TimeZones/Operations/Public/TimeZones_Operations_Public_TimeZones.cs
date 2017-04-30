using System;
using System.Collections.Generic;

namespace FlexibleParser
{
    public partial class TimeZones
    {
        ///<summary><para>Compares the current instance against another TimeZones one.</para></summary>
        ///<param name="other">The other TimeZones instance.</param>
        public int CompareTo(TimeZones other)
        {
            return Common.PerformComparison(this, other, typeof(TimeZones));
        }

        ///<summary><para>Outputs an error or "UTC [offset]".</para> </summary>
        public override string ToString()
        {
            return TimeZonesInternal.TimeZoneTypeToString(this);
        }

        ///<summary><para>Creates a new TimeZones instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">String input.</param>
        public static implicit operator TimeZones(string input)
        {
            return new TimeZones(input);
        }

        ///<summary><para>Creates a new TimeZones instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimezoneOfficial input.</param>
        public static implicit operator TimeZones(TimeZoneOfficial input)
        {
            return new TimeZones(input);
        }

        ///<summary><para>Creates a new TimeZones instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimezoneOfficialEnum input.</param>
        public static implicit operator TimeZones(TimeZoneOfficialEnum input)
        {
            return new TimeZones(input);
        }

        ///<summary><para>Creates a new TimeZones instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimezoneIANA input.</param>
        public static implicit operator TimeZones(TimeZoneIANA input)
        {
            return new TimeZones(input);
        }

        ///<summary><para>Creates a new TimeZones instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimezoneIANAEnum input.</param>
        public static implicit operator TimeZones(TimeZoneIANAEnum input)
        {
            return new TimeZones(input);
        }

        ///<summary><para>Creates a new TimeZones instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimezoneConventional input.</param>
        public static implicit operator TimeZones(TimeZoneConventional input)
        {
            return new TimeZones(input);
        }

        ///<summary><para>Creates a new TimeZones instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimezoneConventionalEnum input.</param>
        public static implicit operator TimeZones(TimeZoneConventionalEnum input)
        {
            return new TimeZones(input);
        }

        ///<summary><para>Creates a new TimeZones instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimezoneWindows input.</param>
        public static implicit operator TimeZones(TimeZoneWindows input)
        {
            return new TimeZones(input);
        }

        ///<summary><para>Creates a new TimeZones instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimezoneWindows input.</param>
        public static implicit operator TimeZones(TimeZoneWindowsEnum input)
        {
            return new TimeZones(input);
        }

        ///<summary><para>Creates a new TimeZones instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimezoneUTC input.</param>
        public static implicit operator TimeZones(TimeZoneUTC input)
        {
            return new TimeZones(input);
        }

        ///<summary><para>Creates a new TimeZones instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimezoneUTCEnum input.</param>
        public static implicit operator TimeZones(TimeZoneUTCEnum input)
        {
            return new TimeZones(input);
        }

        ///<summary><para>Creates a new TimeZones instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimezoneMilitary input.</param>
        public static implicit operator TimeZones(TimeZoneMilitary input)
        {
            return new TimeZones(input);
        }

        ///<summary><para>Creates a new TimeZones instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimezoneMilitaryEnum input.</param>
        public static implicit operator TimeZones(TimeZoneMilitaryEnum input)
        {
            return new TimeZones(input);
        }

        ///<summary><para>Creates a new TimeZones instance by relying on the most adequate constructor.</para></summary>
        ///<param name="input">TimeZoneInfo input.</param>
        public static implicit operator TimeZones(TimeZoneInfo input)
        {
            return new TimeZones(input);
        }

        ///<summary><para>Determines whether two TimeZones variables are equal.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator ==(TimeZones first, TimeZones second)
        {
            return Common.NoNullEquals(first, second, typeof(TimeZones));
        }

        ///<summary><para>Determines whether two TimeZones variables are different.</para></summary>
        ///<param name="first">First operand.</param>
        ///<param name="second">Second operand.</param>
        public static bool operator !=(TimeZones first, TimeZones second)
        {
            return !Common.NoNullEquals(first, second, typeof(TimeZones));
        }

        ///<summary><para>Determines whether the current TimeZones variable is equal to other one.</para></summary>
        ///<param name="other">Other variable.</param>
        public bool Equals(TimeZones other)
        {
            return
            (
                object.Equals(other, null) ? false :
                TimeZonesInternal.TimezonesAreEqual(this, other)
            );
        }

        ///<summary><para>Determines whether the current TimeZones variable is equal to other one.</para></summary>
        ///<param name="obj">Other variable.</param>
        public override bool Equals(object obj)
        {
            return Equals(obj as TimeZones);
        }

        ///<summary><para>Returns the hash code for this TimeZones variable.</para></summary>
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
