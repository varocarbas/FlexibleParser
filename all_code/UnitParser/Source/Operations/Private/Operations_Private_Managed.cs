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

        //This method should always be used when dealing with random UnitInfo variables because it accounts for all the
        //possible scenarios. On the other hand, with simple operations (e.g., random UnitInfo & numeric type) it might
        //be better to use PerformManagedOperationValues. 
        private static UnitInfo PerformManagedOperationUnits(UnitInfo firstInfo, UnitInfo secondInfo, Operations operation)
        {
            ErrorTypes errorType = GetOperationError
            (
                firstInfo, secondInfo, operation
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
            return PerformManagedOperationNormalisedValues
            (
                //After being normalised, the operands might require further modifications.
                firstInfo, GetOperandsAddition(firstInfo, secondInfo, operation), operation
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
                //For example: 5 and 6 from 5*10^2 and 6*10^7 cannot be added right away.
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
                //Having the same BaseTenExponent values means that the given operation can be performed right away.
                return unitInfos2;
            }

            int[] bigSmallI = 
            (
                unitInfos2[0].BaseTenExponent > unitInfos2[1].BaseTenExponent ?
                new int[] { 0, 1 } : new int[] { 1, 0 }
            );

            //Only the variable with the bigger value is modified. For example: 5*10^5 & 3*10^3 is converted
            //into 500*10^3 & 3*10^3 in order to allow the addition 500 + 3. 
            UnitInfo big2 = AdaptBiggerAdditionOperand(unitInfos2, bigSmallI, operation);
            if (big2.Error.Type != ErrorTypes.None)
            {
                return TooBigGapAddition(unitInfos2, bigSmallI, operation);
            }

            unitInfos2[bigSmallI[0]].Value = big2.Value;
            unitInfos2[bigSmallI[0]].BaseTenExponent = unitInfos2[bigSmallI[1]].BaseTenExponent;

            return unitInfos2;
        }

        //When adding/subtracting two numbers whose gap is bigger than the maximum decimal range, there
        //is no need to perform any operation (i.e., no change will be observed because of being outside
        //the maximum supported precision). This method takes care of these cases and returns the expected
        //output (i.e., biggest value).
        private static UnitInfo[] TooBigGapAddition(UnitInfo[] unitInfos2, int[] bigSmallI, Operations operation)
        {
            UnitInfo[] outInfos = new UnitInfo[] 
            {
                //First operand (i.e., one whose information defines the operation) together with the
                //numeric information (i.e., just Value and BaseTenExponent because both are normalised)
                //which is associated with the biggest one.
                new UnitInfo(unitInfos2[0])
                {
                    Value = unitInfos2[bigSmallI[0]].Value,
                    BaseTenExponent = unitInfos2[bigSmallI[0]].BaseTenExponent
                }
            };

            if (operation == Operations.Subtraction && bigSmallI[0] == 1)
            {
                outInfos[0].Value = -1m * outInfos[0].Value;
            }

            if (outInfos[0].Unit == Units.Unitless)
            {
                outInfos[0].Unit = unitInfos2[bigSmallI[1]].Unit;
            }

            return outInfos;
        }

        private static UnitInfo AdaptBiggerAdditionOperand(UnitInfo[] unitInfos2, int[] bigSmallI, Operations operation)
        {
            int gapExponent = unitInfos2[bigSmallI[0]].BaseTenExponent - unitInfos2[bigSmallI[1]].BaseTenExponent;
            if (gapExponent >= 27)
            {
                //The difference between both inputs is bigger than (or, at least, very close to) the maximum decimal value/precision;
                //what makes this situation calculation unworthy and the first operand to be returned as the result.
                //Note that the error below these lines is just an easy way to tell the calling function about this eventuality.
                return new UnitInfo(unitInfos2[0]) 
                { 
                    Error = new ErrorInfo(ErrorTypes.InvalidOperation) 
                };
            }

            //PerformManagedOperationValues is used to make sure that the resulting numeric information is stored
            //in Value (if possible).
            UnitInfo big2 = PerformManagedOperationValues
            (
                RaiseToIntegerExponent(10m, gapExponent), unitInfos2[bigSmallI[0]].Value, 
                Operations.Multiplication                
            );

            bool isWrong = 
            (
                big2.Error.Type != ErrorTypes.None || big2.BaseTenExponent != 0 ?
                
                //The value of the bigger input times 10^(gap between BaseTenExponent of inputs) is too big. 
                isWrong = true :
                
                //Overflow-check very unlikely to trigger an error. In fact, with properly normalised variables,
                //triggering an error would be plainly impossible.               
                AreAdditionFinalValuesWrong
                (
                    unitInfos2[0].Value, unitInfos2[1].Value, operation
                )
            );

            return
            (
                isWrong ?
                //This error is just an easy way to let the calling function know about the fact that no
                //calculation has been performed (too big gap). This isn't a properly-speaking error and
                //that's why it will not be notified to the user.
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

        private static bool AreAdditionFinalValuesWrong(decimal val1, decimal val2, Operations operation)
        {
            bool isWrong = false;

            try
            {
                val1 = val1 + val2 *
                (
                    operation == Operations.Addition ? 1 : -1
                );
            }
            catch { isWrong = true; }

            return isWrong;
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

        private static UnitInfo RaiseToIntegerExponent(decimal baseValue, int exponent)
        {
            return RaiseToIntegerExponent(new UnitInfo(baseValue), exponent);
        }

        private static UnitInfo RaiseToIntegerExponent(UnitInfo baseInfo, int exponent)
        {
            if (exponent <= 1 && exponent >= 0)
            {
                baseInfo.Value = (exponent == 0 ? 1m : baseInfo.Value);
                return baseInfo;
            }

            UnitInfo outInfo = new UnitInfo(baseInfo);

            for (int i = 1; i < Math.Abs(exponent); i++)
            {
                outInfo = PerformManagedOperationValues
                (
                    outInfo, baseInfo, Operations.Multiplication
                );
                if (outInfo.Error.Type != ErrorTypes.None) return outInfo;
            }

            return
            (
                exponent < 0 ?
                PerformManagedOperationValues(new UnitInfo(1m), outInfo, Operations.Division) :
                outInfo
            );
        }

        private static UnitInfo PerformManagedOperationNormalisedValues(UnitInfo outInfo, UnitInfo[] normalisedInfos, Operations operation)
        {
            return
            (
                normalisedInfos.Length == 1 ?
                //There is just one operand when the difference between both of them is too big.
                normalisedInfos[0] :
                PerformManagedOperationTwoOperands(outInfo, normalisedInfos, operation)
            );
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

        //This method might be used to perform full operations (not just being the last calculation step) instead
        //of the default one (PerformManagedOperationUnits) for simple cases. That is: ones not dealing with the
        //complex numeric reality (Value, Prefix and BaseTenExponent) which makes a pre-analysis required.
        //Note that, unlikely what happens with PerformMangedOperationUnits, the outputs of this method aren't
        //normalised (= primarily stored under Value), what is useful in certain contexts.
        //NOTE: this function assumes that both inputs are normalised, what means that no prefix information is expected.
        //It might also be used with non-normalised inputs, but their prefix information would be plainly ignored.
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
                    outInfo.Value += secondInfo0.Value;
                }
                else if (operation == Operations.Subtraction)
                {
                    outInfo.Value -= secondInfo.Value;
                }
                else
                {
                    //The reason for checking whether BaseTenExponent is inside/outside the int range before performing 
                    //the operation (rather than going ahead and eventually catching the resulting exception) isn't just
                    //being quicker, but also the only option in many situations. Note that an addition/subtraction between
                    //two int variables whose result is outside the int range might not trigger an exception (+ random 
                    //negative value as output).
                    if (VaryBaseTenExponent(outInfo, secondInfo0.BaseTenExponent, operation == Operations.Division).Error.Type != ErrorTypes.None)
                    {
                        return new UnitInfo(outInfo, ErrorTypes.InvalidOperation);
                    }

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
                isWrong || ((operation == Operations.Multiplication || operation == Operations.Division) && outInfo.Value == 0.0m) ?
                OperationValuesManageError(firstInfo0, secondInfo0, operation) : outInfo
            );
        }

        private static UnitInfo OperationValuesManageError(UnitInfo outInfo, UnitInfo secondInfo, Operations operation)
        {
            if (operation != Operations.Multiplication && operation != Operations.Division)
            {
                //This condition should never be true on account of the fact that the pre-modifications performed before
                //adding/subtracting should avoid erroneous situations.
                return new UnitInfo(outInfo, ErrorTypes.InvalidOperation);
            }

            UnitInfo secondInfo2 = ConvertValueToBaseTen(secondInfo.Value);
            outInfo = VaryBaseTenExponent(outInfo, secondInfo2.BaseTenExponent, operation == Operations.Division);
            if (Math.Abs(secondInfo2.Value) == 1m || outInfo.Error.Type != ErrorTypes.None) return outInfo;
            
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
                //lies within the 0.1-10.0 range.
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

        private static UnitInfo ConvertValueToBaseTen(decimal value)
        {
            value = Math.Abs(value);
            return FromValueToBaseTenExponent
            (
                 new UnitInfo(value), Math.Abs(value), false
            );
        }

        private static UnitInfo ConvertBaseTenToValue(UnitInfo unitInfo)
        {
            if (unitInfo.BaseTenExponent == 0) return unitInfo;

            UnitInfo outInfo = new UnitInfo(unitInfo);
            bool decrease = unitInfo.BaseTenExponent > 0;
            int sign = Math.Sign(outInfo.Value);
            decimal absValue = Math.Abs(outInfo.Value);

            while (outInfo.BaseTenExponent != 0m)
            {
                if (decrease)
                {
                    if (absValue >= MaxValueDec / 10m) break;
                    absValue *= 10m;
                    outInfo.BaseTenExponent -= 1;
                }
                else
                {
                    if (absValue <= MinValueDec * 10m) break;
                    absValue /= 10m;
                    outInfo.BaseTenExponent += 1;
                }
            }

            outInfo.Value = sign * absValue;

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
            if (outInfo.Value == 0m) return outInfo;

            outInfo = FromValueToBaseTenExponent
            (
                outInfo, outInfo.Value, false
            );

            return outInfo;
        }

        private static UnitInfo FromValueToBaseTenExponent(UnitInfo outInfo, decimal value, bool isPrefix)
        {
            if (value == 0m) return outInfo;

            decimal valueAbs = Math.Abs(value);
            bool decrease = (valueAbs > 1m);
            if (!isPrefix)
            {
                outInfo.Value = outInfo.Value / valueAbs;
            }

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
                    outInfo.BaseTenExponent += 1;
                }
                else
                {
                    value *= 10m;
                    outInfo.BaseTenExponent -= 1;
                }

                valueAbs = Math.Abs(value);
            }

            return outInfo;
        }

        //Method used to vary BaseTenExponent without provoking unhandled exceptions (i.e., bigger than int.MaxValue).
        private static UnitInfo VaryBaseTenExponent(UnitInfo info, int baseTenIncrease, bool isDivision = false)
        {
            long val1 = info.BaseTenExponent;
            long val2 = baseTenIncrease;

            if (isDivision)
            {
                //Converting a negative value into positive might provoke an overflow error for the int type
                //(e.g., Math.Abs(int.MinValue)). Converting both variables to long is a quick and effective
                //way to avoid this problem.
                val2 *= -1;
            }

            return
            (
                 ((val2 > 0 && val1 > int.MaxValue - val2) || (val2 < 0 && val1 < int.MinValue - val2)) ?
                new UnitInfo(info, ErrorTypes.NumericError) : new UnitInfo(info){ BaseTenExponent = (int)(val1 + val2) }
            );
        }
    }
}
