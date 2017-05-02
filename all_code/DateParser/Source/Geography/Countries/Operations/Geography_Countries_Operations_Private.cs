using System;
using System.Collections.Generic;

namespace FlexibleParser
{
    internal partial class CountryInternal
    {
        public static string GetNameFromEnum(CountryEnum value)
        {
            string name = value.ToString();
            name = name.Replace("_", " ");

            return name;
        }
    }
}
