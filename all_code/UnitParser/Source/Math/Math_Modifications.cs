using System;
using System.Collections.Generic;

namespace FlexibleParser
{
    public partial class UnitP
    {
        //This method improves values which have likely been affected by the precision of the calculations.
        //For example: 1.2999999999999 actually being 1.3.
        private static decimal ImproveFinalValue(decimal value)
        {
            if (value == 0m) return value;
            decimal sign = value / Math.Abs(value);
            decimal absValue = Math.Abs(value);
            int minGapDigits = 6;
            
            UnitInfo infoUp = GetTargetRoundedValue(absValue, RoundType.MidpointAwayFromZero);
            if (infoUp.Value > absValue && infoUp.BaseTenExponent >= minGapDigits)
            {
                //Performs improvements like converting 0.23459999999999999999 into 0.2346.
                value = sign * infoUp.Value;
            }
            else
            {
                UnitInfo infoDown = GetTargetRoundedValue(absValue, RoundType.MidpointToZero);
                if (infoDown.Value < absValue && infoDown.BaseTenExponent >= minGapDigits)
                {
                    //Performs improvements like converting 0.23450000000000004 into 0.2345.
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
            int startCount = 0;
            int startTarget = 4;
            for (int i = 2; i < 27; i++)
            {
                decimal tempVal = RoundExact(value, i, roundType);

                if (!started)
                {
                    if (tempVal == outInfo.Value)
                    {
                        startCount += 1;
                        if (startCount == startTarget) started = true;
                    }
                    else
                    {
                        //Starting the analysis of consecutive irrelevant digits right away might be counter-producing.
                        //Once the process is started, any exception (i.e., a non-irrelevant digit) would provoke the
                        //analysis to immediately fail. That's why better delaying the analysis start until seeing some
                        //consecutive digits (i.e., higher chances of finding what is expected).
                        startCount = 0;
                    }
                }
                else if (started)
                {
                    if (tempVal != outInfo.Value) return outInfo;
                    outInfo.BaseTenExponent += 1;
                }

                outInfo.Value = tempVal;
            }

            return outInfo;
        }

        private static UnitInfo ConvertDoubleToDecimal(double valueDouble)
        {
            if (valueDouble == 0.0) return new UnitInfo();

            UnitInfo outInfo = new UnitInfo();
            if (Math.Abs(valueDouble) < MinValue)
            {
                while (Math.Abs(valueDouble) < MinValue)
                {
                    outInfo.BaseTenExponent = outInfo.BaseTenExponent - 1;
                    valueDouble = valueDouble * 10.0;
                }
            }
            else if (Math.Abs(valueDouble) > MaxValue)
            {
                while (Math.Abs(valueDouble) > MaxValue)
                {
                    outInfo.BaseTenExponent = outInfo.BaseTenExponent + 1;
                    valueDouble = valueDouble / 10.0;
                }
            }

            outInfo.Value = Convert.ToDecimal(valueDouble);

            //Numbers always rely on ExceptionHandlingTypes.AlwaysTriggerException.
            outInfo.Error = new ErrorInfo
            (
                ErrorTypes.None, ExceptionHandlingTypes.AlwaysTriggerException
            );

            return outInfo;
        }
    }
}
