using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    public partial class UnitP
    {
        private static void InitialisePrefixNames(PrefixTypes prefixType)
        {
            if (prefixType == PrefixTypes.SI)
            {
                if (AllSIPrefixNames == null)
                {
                    AllSIPrefixNames = AllSIPrefixSymbols.ToDictionary
                    (
                        x => x.Key, x => AllSIPrefixes.First(y => y.Value == x.Value)
                        .Key.ToString().ToLower()
                    );
                }
            }
            else if (prefixType == PrefixTypes.Binary)
            {
                if (AllBinaryPrefixNames == null)
                {
                    AllBinaryPrefixNames = AllBinaryPrefixSymbols.ToDictionary
                    (
                        x => x.Key, x => AllBinaryPrefixes.First(y => y.Value == x.Value)
                        .Key.ToString().ToLower()
                    );
                }
            }
        }

        private static void InitialiseBigSmallPrefixValues(PrefixTypes prefixType)
        {
            if (prefixType == PrefixTypes.SI)
            {
                if (BigSIPrefixValues == null)
                {
                    BigSIPrefixValues = AllSIPrefixes.Where(x => x.Value > 1m)
                    .Select(x => x.Value).OrderByDescending(x => x);
                }
                if (SmallSIPrefixValues == null)
                {
                    SmallSIPrefixValues = AllSIPrefixes.Where(x => x.Value < 1m)
                    .Select(x => x.Value).OrderBy(x => x);
                }
            }
            else if(prefixType == PrefixTypes.Binary)
            {
                if (BigBinaryPrefixValues == null)
                {
                    BigBinaryPrefixValues = AllBinaryPrefixes.Where(x => x.Value > 1m)
                    .Select(x => x.Value).OrderByDescending(x => x);
                }
                if (SmallSIPrefixValues == null)
                {
                    SmallBinaryPrefixValues = AllBinaryPrefixes.Where(x => x.Value < 1m)
                    .Select(x => x.Value).OrderBy(x => x);
                }
            }
        }
    }
}
