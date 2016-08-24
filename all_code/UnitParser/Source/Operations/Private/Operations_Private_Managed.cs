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
        //BaseTenExponent and Prefix). In case of not having to worry about any of this, at least in one of the operands,
        //(e.g., operation with numeric-type variables) PerformManagedOperationValues might be used.
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
            //After being normalised, the operands might be modified further.
            UnitInfo[] normalised = GetOperandsAddition(firstInfo, secondInfo, operation);

            return PerformManagedOperationNormalisedValues
            (
                firstInfo, normalised, operation
            );
        }

        private static UnitInfo[] GetOperandsAddition(UnitInfo firstInfo, UnitInfo secondInfo, Operations operation)
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
                    },
                    operation
                );
            }

            return operands2;
        }
        
        private static UnitInfo[] AdaptNormalisedValuesForAddition(UnitInfo[] unitInfos2, Operations operation)
        {
            if (unitInfos2[0].BaseTenExponent == unitInfos2[1].BaseTenExponent)
            {
                //Being normalised implies no prefixes.
                return unitInfos2;
            }

            int[] bigSmallI = 
            (
                unitInfos2[0].BaseTenExponent > unitInfos2[1].BaseTenExponent ?
                new int[] { 0, 1 } : new int[] { 1, 0 }
            );

            //Only the variable with the bigger value would be modified. For example:
            //5*10^5 & 3*10^3 is converted into 500*10^3 & 3*10^3 to allow 500+3. 
            UnitInfo big2 = AdaptBiggerAdditionOperand(unitInfos2, bigSmallI, operation);
            if (big2.Error.Type != ErrorTypes.None)
            {
                return new UnitInfo[] { unitInfos2[0] };
            }

            unitInfos2[bigSmallI[0]].Value = big2.Value;
            unitInfos2[bigSmallI[0]].BaseTenExponent = unitInfos2[bigSmallI[1]].BaseTenExponent;

            return unitInfos2;
        }

        private static UnitInfo AdaptBiggerAdditionOperand(UnitInfo[] unitInfos2, int[] bigSmallI, Operations operation)
        {
            UnitInfo big2 = RaiseToIntegerExponent
            (
                10m,
                unitInfos2[bigSmallI[0]].BaseTenExponent - unitInfos2[bigSmallI[1]].BaseTenExponent
            ); 
            
            //Forces all the numeric information to be stored in Value (if possible).
            big2 = PerformManagedOperationValues
            (
                big2, unitInfos2[bigSmallI[0]].Value, Operations.Multiplication                
            );

            bool isWrong = false;
            if (big2.Error.Type != ErrorTypes.None || big2.BaseTenExponent != 0)
            {
                //After removing the small value exponent, the big one is still too big (> decimal.MaxValue). 
                //Under these conditions, the result is just the first operand (eventually converted).
                isWrong = true;
            }
            else
            {
                try
                {
                    //Overflow-check very unlikely to trigger an error. In fact, with properly normalised variables,
                    //triggering an error would be plainly impossible.
                    unitInfos2[0].Value = unitInfos2[0].Value + unitInfos2[1].Value *
                    (
                        operation == Operations.Addition ? 1 : -1
                    );
                }
                catch
                {
                    isWrong = true;
                }
            }

            return
            (
                isWrong ?
                new UnitInfo(unitInfos2[0])
                {
                    Error = new ErrorInfo(ErrorTypes.InvalidOperation)
                } :
                //Returning the new big value. For example: with 5*10^4 & 3*10^2, 500 would be returned.
                new UnitInfo(unitInfos2[bigSmallI[0]])
                {
                    Value = big2.Value
                }
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

        private static UnitInfo PerformManagedOperationNormalisedValues(UnitInfo outInfo, UnitInfo[] normalisedInfos, Operations operation)
        {
            outInfo =
            (
                normalisedInfos.Length == 1 ?
                //There is just one operand when the difference between both of them is too big.
                outInfo = normalisedInfos[0] :
                PerformManagedOperationTwoOperands(outInfo, normalisedInfos, operation)
            );

            return outInfo;
        }

        private static UnitInfo PerformManagedOperationTwoOperands(UnitInfo outInfo, UnitInfo[] normalisedInfos, Operations operation)
        {
            UnitInfo outInfoNormalised = PerformManagedOperationValues
            (
                normalisedInfos[0], normalisedInfos[1], operation
            );

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

        private static UnitInfo ConvertValueToBaseTen(decimal value)
        {
            UnitInfo outInfo = new UnitInfo(Math.Abs(value));
            if (outInfo.Value < 10m && outInfo.Value > 0.1m)
            {
                return outInfo;
            }

            bool reduceIt = (outInfo.Value >= 10);

            while (outInfo.Value >= 10m || outInfo.Value <= 0.1m)
            {
                if (reduceIt)
                {
                    outInfo.Value /= 10m;
                    outInfo.BaseTenExponent += 1;
                }
                else
                {
                    outInfo.Value *= 10m;
                    outInfo.BaseTenExponent -= 1;
                }
            }

            return outInfo;
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
                if (operation == Operations.Multiplication || operation == Operations.Division)
                {
                    //Dividing by zero scenarios are taken into account somewhere else.
                    return new UnitInfo(firstInfo) { Value = 0m };
                }
            }

            UnitInfo outInfo = new UnitInfo(firstInfo);
            UnitInfo firstInfo0 = new UnitInfo(firstInfo);
            UnitInfo secondInfo0 = new UnitInfo(secondInfo);

            bool isWrong = false;
            try
            {
                if (operation == Operations.Addition)
                {
                    outInfo.Value += secondInfo.Value;
                }
                else if (operation == Operations.Subtraction)
                {
                    outInfo.Value -= secondInfo.Value;
                }
                else
                {
                    if (operation == Operations.Multiplication)
                    {
                        outInfo.Value *= secondInfo.Value;
                        outInfo.BaseTenExponent += secondInfo.BaseTenExponent;
                    }
                    else if (operation == Operations.Division)
                    {
                        if (secondInfo.Value == 0m)
                        {
                            return
                            (
                                new UnitInfo(outInfo) 
                                { 
                                    Error = new ErrorInfo(ErrorTypes.NumericError) 
                                }
                            );
                        }
                        outInfo.Value /= secondInfo.Value;
                        outInfo.BaseTenExponent -= secondInfo.BaseTenExponent;
                    }
                }
            }
            catch { isWrong = true; }

            return
            (
                //An error might not be triggered despite of dealing with numbers outside decimal precision.
                //For example: 0.00000000000000000001m * 0.0000000000000000000001m can output 0m without triggering an error. 
                isWrong || outInfo.Value == 0.0m ?
                OperationValuesManageError(firstInfo0, secondInfo0, operation) :
                outInfo
            );
        }

        private static UnitInfo OperationValuesManageError(UnitInfo outInfo, UnitInfo secondInfo, Operations operation)
        {
            if (operation != Operations.Multiplication && operation != Operations.Division)
            {
                //This condition should never be true on account of the fact that the pre-modifications performed before
                //adding/subtracting should avoid erroneous situations.
                return outInfo;
            }

            UnitInfo secondInfo2 = ConvertValueToBaseTen(secondInfo.Value);
            outInfo.BaseTenExponent += 
            (
                (operation == Operations.Multiplication ? 1 : -1) * secondInfo2.BaseTenExponent
            );

            if (Math.Abs(secondInfo2.Value) == 1m) return outInfo;
            
            try
            {
                outInfo = PerformManagedOperationUnits
                (
                    outInfo, secondInfo2.Value, operation
                );
            }
            catch
            {
                //Very unlikely scenario on account of the fact that Math.Abs(secondInfo2.Value)
                //lies within the 0.1-1.0 range.
                outInfo = OperationValuesManageError
                (
                    new UnitInfo(outInfo)
                    {
                        Value = secondInfo2.Value,
                        BaseTenExponent = 0
                    },
                    new UnitInfo()
                    {
                        Value = outInfo.Value,
                        BaseTenExponent = outInfo.BaseTenExponent
                    },
                    operation
                );
            }

            return outInfo;
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
