using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FlexibleParser
{
    public partial class UnitP
    {
        private static bool CharAreEquivalent(char char1, char char2)
        {
            if (char1 == char2) return true;

            return (char1.ToString().ToLower() == char2.ToString().ToLower());
        }
    }
}
