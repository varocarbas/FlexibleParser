using System;
using System.Collections.Generic;

namespace FlexibleParser
{
    internal partial class TimeZonesInternal
    {
        private static int CompareSameTimezonesSameUTC(TimeZones first, TimeZones second)
        {
            dynamic[] firstTypes = new dynamic[]
            {
                first.Official, first.IANA, first.Conventional
            };
            dynamic[] secondTypes = new dynamic[]
            {
                second.Official, second.IANA, second.Conventional
            };

            for (int i = 0; i < firstTypes.Length; i++)
            {
                if (firstTypes[i].Count == 1 && firstTypes[i].Count == secondTypes[i].Count)
                {
                    return firstTypes[i][0].ToString().ComparedTo
                    (
                        secondTypes[i][0].ToString()
                    );
                }
            }

            return first.Windows.ToString().CompareTo
            (
                second.Windows.ToString()
            );
        }

        public static bool TimezonesAreEqual(TimeZones first, TimeZones second)
        {
            if (first == null || second == null)
            {
                return first == second;
            } 

            int temp = IANAMainsAreEqual(first, second);
            if (temp != 0) return (temp == 1);

            return 
            (
                first.Conventional == second.Conventional || first.Windows == second.Windows
            );
        }

        private static int IANAMainsAreEqual(TimeZones first, TimeZones second)
        {
            int temp = CheckIANAMain(first.IANA, second.IANA);
            if (temp != 0) return temp;

            return CheckIANAMain(first.Official, second.Official);
        }

        private static int CheckIANAMain(dynamic first, dynamic second)
        {
            if (first == null || second == null)
            {
                return
                (
                    first == second ? 0 : -1
                );
            }

            if (first.Count != second.Count)
            {
                return -1;
            }

            return
            (
                (first.Count == 1 && first[0] == second[0]) ? 1 : 0
            );
        }

        public static bool NoNullEquals(dynamic first, dynamic second)
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
