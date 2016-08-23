using System;
using System.Collections.Generic;

namespace FlexibleParser
{
    public partial class UnitP
    {
        private static UnitInfo PerformManagedOperationUnits(UnitInfo firstInfo, decimal second, Operations operation)
        {
            return PerformManagedOperationUnits
            (
                firstInfo, new UnitInfo(second), operation
            );
        }

        private static UnitInfo PerformManagedOperationUnits(decimal first, UnitInfo secondInfo, Operations operation)
        {
            return PerformManagedOperationUnits
            (
                new UnitInfo(first), secondInfo, operation
            );
        }

        //This method is much more comprehensive than the alternative for values (PerformManagedOperationValues),
        //because it assumes any scenario involving two units (understood as UnitInfo variables which might have Value, 
        //BaseTenExponent and Prefix). In case of not having to worry about any of this, even just for one of the operands,
        //PerformManagedOperationValues might be used (e.g., in any operation involving plain numbers).
        private static UnitInfo PerformManagedOperationUnits(UnitInfo firstInfo, UnitInfo secondInfo, Operations operation)
        {
            ErrorTypes errorType = GetOperationError
            (
                new UnitInfo[] { firstInfo, secondInfo }, operation
            );
            if (errorType != ErrorTypes.None)
            {
                return new UnitInfo(firstInfo) 
                {
                    Error = new ErrorInfo(errorType) 
                };
            }

            return
            (
                operation == Operations.Addition || operation == Operations.Subtraction ?
                PerformManagedOperationAddition(firstInfo, secondInfo, operation) :
                PerformManagedOperationMultiplication(firstInfo, secondInfo, operation)
            );
        }

        private static UnitInfo PerformManagedOperationAddition(UnitInfo firstInfo, UnitInfo secondInfo, Operations operation)
        {   
            //Additionally to being normalised, the operands have to pass through further checks.
            UnitInfo[] normalised = GetOperandsAddition(firstInfo, secondInfo);

            return PerformManagedOperationNormalisedValues
            (
                firstInfo, normalised, operation
            );
        }

        private static UnitInfo[] GetOperandsAddition(UnitInfo firstInfo, UnitInfo secondInfo)
        {
            UnitInfo[] operands2 = new UnitInfo[] 
            {
                new UnitInfo(firstInfo), new UnitInfo(secondInfo)
            };

            if (operands2[0].BaseTenExponent != operands2[1].BaseTenExponent || operands2[0].Prefix.Factor != operands2[1].Prefix.Factor)
            {
                //The addition/subtraction might not be performed right away even with normalised values.
                //The base numbers of 5*10^2 and 6*10^7  (i.e., 5 & 6) might not be added right away; and
                //this is precisely what normalised units are (i.e., value * 10^exp).
                operands2 = AdaptNormalisedValuesForAddition
                (
                    new UnitInfo[] 
                    { 
                        NormaliseUnitInfo(operands2[0]), 
                        NormaliseUnitInfo(operands2[1])
                    }
                );
            }

            return operands2;
        }
        
        private static UnitInfo[] AdaptNormalisedValuesForAddition(UnitInfo[] unitInfos2)
        {
            if (unitInfos2[0].BaseTenExponent == unitInfos2[1].BaseTenExponent)
            {
                return unitInfos2;
            }

            int[] bigSmallI = 
            (
                unitInfos2[0].BaseTenExponent > unitInfos2[1].BaseTenExponent ?
                new int[] { 0, 1 } : new int[] { 1, 0 }
            );

            UnitInfo big2 = AdaptBiggerAdditionOperand(unitInfos2, bigSmallI);
            if (big2.Error.Type != ErrorTypes.None)
            {
                return new UnitInfo[] { unitInfos2[0] };
            }

            unitInfos2[bigSmallI[0]].Value = big2.Value;
            unitInfos2[bigSmallI[0]].BaseTenExponent = unitInfos2[bigSmallI[1]].BaseTenExponent;

            return unitInfos2;
        }

        private static UnitInfo AdaptBiggerAdditionOperand(UnitInfo[] unitInfos2, int[] bigSmallI)
        {
            UnitInfo big2 = RaiseToIntegerExponent
            (
                10m, 
                unitInfos2[bigSmallI[0]].BaseTenExponent - unitInfos2[bigSmallI[1]].BaseTenExponent
            );
            
            if (big2.Error.Type != ErrorTypes.None || big2.BaseTenExponent != 0)
            {
                //After removing the small value exponent, the big one is still too big (> decimal.MaxValue). 
                //Under these conditions, the result is just the first operand (eventually converted).
                return new UnitInfo(unitInfos2[0])
                {
                    Error = new ErrorInfo(ErrorTypes.InvalidOperation)
                };
            }

            return PerformManagedOperationValues
            (
                big2, unitInfos2[bigSmallI[0]], Operations.Multiplication
            );
        }

        private static UnitInfo PerformManagedOperationMultiplication(UnitInfo firstInfo, UnitInfo secondInfo, Operations operation)
        {
            return PerformManagedOperationNormalisedValues
            (
                firstInfo, new UnitInfo[] 
                { 
                    NormaliseUnitInfo(firstInfo), 
                    NormaliseUnitInfo(secondInfo) 
                },
                operation
            );
        }

        private static UnitInfo PerformManagedOperationNormalisedValues(UnitInfo outInfo, UnitInfo[] normalised, Operations operation)
        {
            outInfo =
            (
                normalised.Length == 1 ?
                //There is just one operand when the difference between both of them is too big.
                outInfo = normalised[0] :
                PerformManagedOperationTwoOperands(outInfo, normalised, operation)
            );

            return outInfo;
        }

        private static UnitInfo PerformManagedOperationTwoOperands(UnitInfo outInfo, UnitInfo[] normalised, Operations operation)
        {
            UnitInfo outInfoNormalised = PerformManagedOperationValues(normalised[0], normalised[1], operation);

            if (outInfo.Error.Type != ErrorTypes.None)
            {
                return new UnitInfo(outInfo)
                {
                    Error = new ErrorInfo(ErrorTypes.NumericError)
                };
            }

            outInfo.BaseTenExponent = outInfoNormalised.BaseTenExponent;
            outInfo.Value = outInfoNormalised.Value;
            //Normalised means no prefixes.
            outInfo.Prefix = new Prefix(outInfo.Prefix.PrefixUsage); 

            return outInfo;
        }

        private static int GetExponentFromValue(decimal value)
        {
            int exponent = 0;
            value = Math.Abs(value);
            if (value < 10m && value > 0.1m) return exponent;

            bool reduceIt = (value >= 10);

            while (value >= 10m || value <= 0.1m)
            {
                if (reduceIt)
                {
                    value = value / 10m;
                    exponent = exponent + 1;
                }
                else
                {
                    value = value * 10m;
                    exponent = exponent - 1;
                }
            }

            return exponent;
        }

        private static UnitInfo PerformManagedOperationValues(decimal firstValue, decimal secondValue, Operations operation)
        {
            return PerformManagedOperationValues
            (
                new UnitInfo(firstValue), new UnitInfo(secondValue), operation
            );
        }

        private static UnitInfo PerformManagedOperationValues(UnitInfo firstInfo, decimal secondValue, Operations operation)
        {
            return PerformManagedOperationValues
            (
                firstInfo, new UnitInfo(secondValue), operation
            );
        }

        private static UnitInfo PerformManagedOperationValues(UnitInfo firstInfo, UnitInfo secondInfo, Operations operation)
        {
            if (firstInfo.Value == 0m || secondInfo.Value == 0m)
            {
                //Dividing by zero scenarios are taken into account somewhere else.
                return new UnitInfo(firstInfo) { Value = 0m };
            }

            UnitInfo firstInfo0 = new UnitInfo(firstInfo);
            UnitInfo secondInfo0 = new UnitInfo(secondInfo);

            bool manageError = false;
            try
            {
                if (operation == Operations.Addition)
                {
                    firstInfo.Value += secondInfo.Value;
                }
                else if (operation == Operations.Subtraction)
                {
                    firstInfo.Value -= secondInfo.Value;
                }
                else
                {
                    if (operation == Operations.Multiplication)
                    {
                        firstInfo.Value *= secondInfo.Value;
                        firstInfo.BaseTenExponent += secondInfo.BaseTenExponent;
                    }
                    else if (operation == Operations.Division)
                    {
                        if (secondInfo.Value == 0m)
                        {
                            firstInfo.Error = new ErrorInfo(ErrorTypes.NumericError);
                            return firstInfo;
                        }

                        firstInfo.Value /= secondInfo.Value;
                        firstInfo.BaseTenExponent -= secondInfo.BaseTenExponent;
                    }
                }
            }
            catch { manageError = true; }

            //An error might not be triggered despite of dealing with numbers outside decimal precision.
            //For example: 0.00000000000000000001m * 0.0000000000000000000001m can output 0m without triggering an error. 
            if (!manageError && firstInfo.Value == 0.0m) manageError = true;

            if (manageError) firstInfo = ManageErrorValues(firstInfo0, secondInfo0, operation);

            return firstInfo;
        }

        private static UnitInfo ManageErrorValues(UnitInfo firstInfo, UnitInfo secondInfo, Operations operation)
        {
            if (operation != Operations.Multiplication && operation != Operations.Division)
            {
                //In principle, this condition should never be true.
                return firstInfo;
            }

            int secondExponent = GetExponentFromValue(secondInfo.Value);
            firstInfo.BaseTenExponent += 
            (
                (operation == Operations.Multiplication ? 1 : -1 ) * secondExponent
            );

            UnitInfo remInfo = PerformManagedOperationValues
            (
                secondInfo.Value,
                RaiseToIntegerExponent(10, secondExponent).Value,
                Operations.Division
            );


            if (Math.Abs(remInfo.Value) != 1m)
            {
                firstInfo = PerformManagedOperationUnits
                (
                    firstInfo.Value, remInfo, Operations.Multiplication
                ); 
            }

            return firstInfo;
        }

        private static UnitInfo NormaliseUnitInfo(UnitInfo unitInfo)
        {
            if (unitInfo.Value == 0 && unitInfo.Prefix.Factor == 1m)
            {
                return unitInfo;
            }

            UnitInfo outInfo = new UnitInfo(unitInfo);

            if (outInfo.Prefix.Factor != 1)
            {
                outInfo = FromValueToBaseTenExponent
                (
                    outInfo, outInfo.Prefix.Factor, true
                );
                outInfo.Prefix = new Prefix(outInfo.Prefix.PrefixUsage);
            }
            
            if (outInfo.Value == 0) return outInfo;

            outInfo = FromValueToBaseTenExponent
            (
                outInfo, outInfo.Value, false
            );

            return outInfo;
        }

        private static UnitInfo FromValueToBaseTenExponent(UnitInfo outInfo, decimal value, bool isPrefix)
        {
            decimal valueAbs = Math.Abs(value);
            bool decrease = (valueAbs > 1m);
            if (!isPrefix) outInfo.Value = outInfo.Value / valueAbs;

            while (valueAbs != 1m)
            {
                if ((valueAbs < 10m && valueAbs > 1m) || (valueAbs > 0.1m && valueAbs < 1m))
                {
                    if (!isPrefix) outInfo.Value = value;
                    else
                    {
                        outInfo = PerformManagedOperationValues
                        (
                            outInfo, value, Operations.Multiplication
                        );
                    }

                    return outInfo;
                }

                if (decrease)
                {
                    value /= 10m;
                    outInfo.BaseTenExponent = outInfo.BaseTenExponent + 1;
                }
                else
                {
                    value *= 10m;
                    outInfo.BaseTenExponent = outInfo.BaseTenExponent - 1;
                }

                valueAbs = Math.Abs(value);
            }

            return outInfo;
        }
    }
}
