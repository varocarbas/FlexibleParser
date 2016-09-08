using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FlexibleParser
{
    public partial class UnitP
    {
        private static UnitInfo ParseDecimal(string stringToParse)
        {
            decimal value = 0m;

            if (decimal.TryParse(stringToParse, NumberStyles.Any, CultureInfo.InvariantCulture, out value))
            {
                //In some cases, decimal.TryParse might consider valid numbers beyond the actual scope of
                //decimal type. For example: 0.00000000000000000000000000000001m assumed to be zero.
                if (value != 0m) return new UnitInfo(value);
            }

            return ParseDouble(stringToParse);
        }

        private static UnitInfo ParseDouble(string stringToParse)
        {
            double valueDouble = 0.0;

            return
            (
                double.TryParse(stringToParse, NumberStyles.Any, CultureInfo.InvariantCulture, out valueDouble) ?
                ConvertDoubleToDecimal(valueDouble) : GetInfoBeyondDouble(stringToParse)
            );
        }

        //This method complements .NET numeric-type parsing methods beyond the double range (i.e., > 1e300).
        private static UnitInfo GetInfoBeyondDouble(string stringToParse)
        {
            stringToParse = stringToParse.ToLower();
            UnitInfo errorInfo = new UnitInfo
            (
                new UnitInfo(), ErrorTypes.NumericError
            );

            //The double-parsing .NET format is expected. That is: only integer exponents after a
            //letter "e", what indicates before-e * 10^after-e.
            if (stringToParse.Contains("e"))
            {
                string[] temp = stringToParse.Split('e');
                if (temp.Length == 2)
                {
                    UnitInfo outInfo = ParseDouble(temp[0]);
                    if (outInfo.Error.Type != ErrorTypes.None)
                    {
                        return errorInfo;
                    }

                    int expInt = 0;
                    if (int.TryParse(temp[1], out expInt))
                    {
                        outInfo.BaseTenExponent += expInt;
                        return outInfo;
                    }
                }
            }
            else
            {
                if (stringToParse.Length < 300)
                {
                    double tempDoub = 0.0;
                    if (double.TryParse(stringToParse, out tempDoub))
                    {
                        return ConvertDoubleToDecimal(tempDoub);
                    }
                }
                else
                {
                    double startNumber = 0.0;
                    if (double.TryParse(stringToParse.Substring(0, 299), out startNumber))
                    {
                        string remString = stringToParse.Substring(299);
                        if (remString.FirstOrDefault(x => !char.IsDigit(x) && x != ',') != '\0')
                        {
                            //Finding a decimal separator here is considered an error because it wouldn't
                            //be too logical (300 digits before the decimal separator!). Mainly by bearing
                            //in mind the exponential alternative above.
                            return errorInfo;
                        }
                        UnitInfo outInfo = ConvertDoubleToDecimal(startNumber);
                        outInfo.BaseTenExponent += GetBeyondDoubleCharacterCount
                        (
                            stringToParse.Substring(299)
                        );
                        return outInfo;
                    }
                }
            }

            return errorInfo;
        }

        private static int GetBeyondDoubleCharacterCount(string remString)
        {
            int outCount = 0;

            try
            {
                foreach (char item in remString.ToCharArray())
                {
                    if (!char.IsDigit(item) && item != ',')
                    {
                        return 0;
                    }
                    outCount = outCount + 1;
                }
            }
            catch
            {
                //The really unlikely scenario of hitting int.MaxValue.
                outCount = 0;
            }

            return outCount;
        }
    }
}
