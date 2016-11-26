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
                //Bear in mind that double.TryParse might misunderstand very small numbers, that's why 0.0 is treated as an error.
                //GetInfoBeyondDouble will undoubtedly determine whether it is a real zero or not.
                double.TryParse(stringToParse, NumberStyles.Any, CultureInfo.InvariantCulture, out valueDouble) && valueDouble != 0.0 ?
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
                    if (temp[0].Contains("e")) return errorInfo;
                    
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
                        
                        int beyondCount = GetBeyondDoubleCharacterCount(stringToParse.Substring(299));
                        if (beyondCount < 0) return errorInfo;

                        //Accounting for the differences 0.001/1000 -> 10^-3/10^3.
                        int sign = (Math.Abs(startNumber) < 1.0 ? -1 : 1);
                        if (startNumber == 0.0 && sign == -1)
                        {
                            //This code accounts for situations like 0.00000[...]00001 where, for the aforementioned double.TryParse, startNumber is zero.
                            bool found = false;
                            int length2 = (remString.Length > 299 ? 299 : remString.Length);
                            for (int i = 0; i < remString.Length; i++)
                            {
                                if (remString[i] != '0' && remString[i] != ',')
                                {
                                    //The default interpretation is initial_part*10^remString.Length (up to the maximum length natively supported by double). 
                                    //For example, 0.0000012345[...]4565879561424 is correctly understood as 0.0000012345*10^length-after-[...]. In this specific 
                                    //situation (i.e., initial_part understood as zero), some digits after [...] have to also be considered to form initial_part. 
                                    //Thus, startNumber is being redefined as all the digits (up to the maximum length natively supported by double) after the 
                                    //first non-zero one; and beyondCount (i.e., the associated 10-base exponent) such that it also includes all the digits since the start.
                                    found = true;
                                    startNumber = double.Parse(remString.Substring(i, length2 - i));
                                    beyondCount = 297 + length2;
                                    break;
                                }
                            }

                            if (!found) return new UnitInfo(0m);
                        }

                        return VaryBaseTenExponent
                        (
                            ConvertDoubleToDecimal(startNumber), 
                            sign * beyondCount
                        );
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
                outCount = -1;
            }

            return outCount;
        }
    }
}
