using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    public partial class Country
    {
        private static string GetNameFromEnum(CountryEnum value)
        {
            string name = value.ToString();
            name = name.Replace("_", " ");

            return name;
        }
    }
}
