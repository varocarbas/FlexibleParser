﻿using System;
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
                    ((ReadOnlyCollection<TimeZoneIANA>)input).ToList(), targets
                )
                .AsReadOnly();
            }
            else if (type == typeof(TimeZoneOfficialEnum))
            {
                return RemoveOfficialOtherCountries
                (
                    ((ReadOnlyCollection<TimeZoneOfficial>)input).ToList(), targets
                )
                .AsReadOnly();
            }

            return input;
        }

        private static List<TimeZoneIANA> RemoveIANAOtherCountries(List<TimeZoneIANA> items, CountryEnum[] targets)
        {
            for (int i = items.Count - 1; i >= 0; i--)
            {
                TimeZoneIANAEnum item2 = items[i].Value;
                if (!TimeZoneIANAInternal.TimeZoneIANACountries.ContainsKey(item2))
                {
                    continue;
                }

                if(targets.Intersect(TimeZoneIANAInternal.TimeZoneIANACountries[item2]).Count() == 0)
                {
                    items.RemoveAt(i);
                }
            }

            return items;
        }

        private static List<TimeZoneOfficial> RemoveOfficialOtherCountries(List<TimeZoneOfficial> items, CountryEnum[] targets)
        {
            for (int i = items.Count - 1; i >= 0; i--)
            {
                var temp = TimeZonesCountryInternal.CountryOfficials.Where
                (
                    x => x.Value.FirstOrDefault
                    (
                        y => y.Key.Value == items[i] || y.Value.Value == items[i]
                    )
                    .Value != null
                )
                .Select(x => x.Key);

                if (targets.Intersect(temp).Count() == 0)
                {
                    items.RemoveAt(i);
                }
            }

            return items;
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
                matches.Count() < 1 ? new TemporaryVariables(input, type) :
                GetGlobalValuesCommon(matches.First())
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
