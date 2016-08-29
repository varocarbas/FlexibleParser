using System;
using System.Collections.Generic;

namespace FlexibleParser
{
    public partial class UnitP
    {
        //This method improves values which have likely been affected by the precision of the calculations.
        //For example: 1.2999999999999 is likely to actually be 1.3.
        private static decimal ImproveFinalValue(decimal value)
        {
            if (value == 0m) return value;
            decimal sign = value / Math.Abs(value);
            decimal absValue = Math.Abs(value);
            decimal maxGapVal = 0.0000001m;
            int maxGapDigits = 6;
            
            UnitInfo infoUp = GetTargetRoundedValue(absValue, RoundType.MidpointAwayFromZero);
            if (infoUp.Value > absValue && infoUp.BaseTenExponent >= maxGapDigits && infoUp.Value - absValue <= maxGapVal)
            {
                //Performs improvements like converting 0.23459999999999999999 into 0.2346.
                value = sign * infoUp.Value;
            }
            else
            {
                UnitInfo infoDown = GetTargetRoundedValue(absValue, RoundType.MidpointToZero);
                if (infoDown.Value < absValue && infoDown.BaseTenExponent >= maxGapDigits && infoDown.Value - absValue <= maxGapVal)
                {
                    //Performs improvements like converting 0.23450000000000001 into 0.2345.
                    value = sign * infoDown.Value;
                }
            }

            return value;
        }

        private static UnitInfo GetTargetRoundedValue(decimal value, RoundType roundType)
        {
            //UnitInfo has a perfect format to store the returned two values (decimal & integer).
            UnitInfo outInfo = new UnitInfo
            (
                RoundExact(value, 1, roundType)
            );

            //Loop iterating through all the digits (up to the maximum decimal precision) and looking
            //for situations with many consecutive irrelevant (i.e., no effect on rounding) digits.
            bool started = false;
            for (int i = 2; i < 27; i++)
            {
                decimal tempVal = RoundExact(value, i, roundType);
                
                if (!started && tempVal == outInfo.Value) started = true;
                else if (started)
                {
                    if (tempVal != outInfo.Value) return outInfo;
                    outInfo.BaseTenExponent += 1;
                }

                outInfo.Value = tempVal;
            }

            return outInfo;
        }
    }
}
