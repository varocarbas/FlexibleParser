using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FlexibleParser
{
    internal partial class TimeZonesInternal
    {
        //Most of Official/IANA timezones are associated with specific countries, an issue which isn't being checked
        //while getting them. This method removes all these not-exactly-correct items.
        public static dynamic RemoveOtherCountryItems(dynamic input, Type type, CountryEnum[] targets)
        {
            if (type == typeof(TimeZoneIANAEnum))
            {
                return RemoveIANAOtherCountries
                (
                    ((ReadOnlyCollection<TimeZoneIANAEnum>)input).ToList(), targets
                )
                .AsReadOnly();
            }
            else if (type == typeof(TimeZoneOfficialEnum))
            {
                return RemoveOfficialOtherCountries
                (
                    ((ReadOnlyCollection<TimeZoneOfficialEnum>)input).ToList(), targets
                )
                .AsReadOnly();
            }

            return input;
        }

        private static List<TimeZoneIANAEnum> RemoveIANAOtherCountries(List<TimeZoneIANAEnum> enums, CountryEnum[] targets)
        {
            for (int i = enums.Count - 1; i >= 0; i--)
            {
                TimeZoneIANAEnum iana = enums[i];
                if (!TimeZoneIANAInternal.TimeZoneIANACountries.ContainsKey(iana))
                {
                    continue;
                }

                if (targets.Intersect(TimeZoneIANAInternal.TimeZoneIANACountries[iana]).Count() == 0)
                {
                    enums.RemoveAt(i);
                }
            }

            return enums;
        }

        private static List<TimeZoneOfficialEnum> RemoveOfficialOtherCountries(List<TimeZoneOfficialEnum> enums, CountryEnum[] targets)
        {
            for (int i = enums.Count - 1; i >= 0; i--)
            {
                TimeZoneOfficialEnum official = enums[i];
                var temp = TimeZonesCountryInternal.CountryOfficials.Where
                (
                    x => x.Value.FirstOrDefault
                    (
                        y => y.Key.Value == official || y.Value.Value == official
                    )
                    .Value != null
                )
                .Select(x => x.Key).ToList();

                if (targets.Intersect(temp).Count() == 0)
                {
                    enums.RemoveAt(i);
                }
            }

            return enums;
        }


        public static CountryEnum[] GetAssociatedCountries(dynamic input, Type type)
        {
            if (type == typeof(TimeZoneIANAEnum))
            {
                return TimeZoneIANAInternal.TimeZoneIANACountries[(TimeZoneIANAEnum)input];
            }
            else if (type == typeof(TimeZoneOfficialEnum))
            {
                TimeZoneOfficial input2 = (TimeZoneOfficial)input;
                var temp = TimeZonesCountryInternal.CountryOfficials.Where
                (
                    x => x.Value.FirstOrDefault
                    (
                        y => y.Key == input2 || y.Value == input2
                    )
                    .Value != null
                )
                .Select(x => x.Key).ToArray();

                return (temp.Count() == 0 ? null : temp);
            }

            return null;
        }

        //The argument of GetGlobalValues is expected to be a valid member of one of the valid types 
        //(i.e., different than "None" items of main-type-timezone enums).
        private static TemporaryVariables GetGlobalValues(dynamic input)
        {
            Type type = input.GetType();

            //"CURIOUS" BEHAVIOUR: at least under the current conditions (i.e., VS 2012 & .NET 4.0), the apparently-redundant
            //cast to IEnumerable<TimezonesOfficialMapping> is required because input is a dynamic variable!
            //Otherwise, this code would crash at runtime because of not being able to recognise the type of matches,
            //even though GetAssociatedMapItems is expressly returning it!
            //After using dynamic quite a lot in NumberParser and in this code, I have got kind of used to this and
            //other peculiarities. In any case, I do think that this is a faulty behaviour and that's why I submitted
            //a ticket in the corresponding open .NET GitHub repository which was... well, go there to see what happened. 
            var matches = (IEnumerable<TimeZonesMainMap>)GetAssociatedMapItems(input, type);

            return
            (
                matches.Count() < 1 ? new TemporaryVariables(input, type) : GetGlobalValuesValid(input, type, matches)
            );
        }

        private static TemporaryVariables GetGlobalValuesValid(dynamic input, Type type, IEnumerable<TimeZonesMainMap> matches)
        {
            return GetGlobalValuesCommon
            (
                matches.First(), type
            );
        }

        //The argument of GetGlobalValues is expected to be a valid member of one of the valid types 
        //(i.e., different than "None" items of main-type-timezone enums).
        private static TemporaryVariables GetGlobalValuesSpecific(dynamic input, Type type, Type target)
        {
            if (type != target)
            {
                input = AdaptItemToTarget(input, type, target);
                type = target;
            }

            return new TemporaryVariables
            (
                new List<dynamic>()
                {
                    GetEnumItemName(input, type),
                    GetEnumItemAbbreviation(input, type),
                    GetEnumItemOffset(input, type), input
                }
            );
        }

        //This method tries to account for the very common scenario where the returned timezone doesn't match the
        //the expected one.
        private static dynamic AdaptItemToTarget(dynamic input, Type type, Type target)
        {
            TimeZonesMainMap map = GetAssociatedMapItem(input, type);
            
            var temp = GetFirstTypeItemFromMap
            (
                map, target
            );

            return
            (
                EnumIsNothing(temp) ? NoneItems[target] : temp
            );
        }

        public static TemporaryVariables GetGlobalValuesInternal(dynamic input, Type type)
        {
            if (type == typeof(TimeZoneOfficialEnum))
            {
                return GetGlobalValues((TimeZoneOfficialEnum)input);
            }
            else if (type == typeof(TimeZoneIANAEnum))
            {
                return GetGlobalValues((TimeZoneIANAEnum)input);
            }
            else if (type == typeof(TimeZoneConventionalEnum))
            {
                return GetGlobalValues((TimeZoneConventionalEnum)input);
            }
            else if (type == typeof(TimeZoneWindowsEnum))
            {
                return GetGlobalValues((TimeZoneWindowsEnum)input);
            }
            else if (type == typeof(TimeZoneUTCEnum))
            {
                return GetGlobalValues((TimeZoneUTCEnum)input);
            }
            else if (type == typeof(TimeZoneMilitaryEnum))
            {
                return GetGlobalValues((TimeZoneMilitaryEnum)input);
            }

            return null;
        }
    }
}
