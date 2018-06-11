using System;
using System.Linq;

namespace FlexibleParser
{
    internal partial class Operations
    {
        public static NumberP PerformArithmeticOperation(NumberP first, NumberP second, ExistingOperations operation, bool baseTenToValue = true)
        {
            return new NumberP
            (
                PerformArithmeticOperation
                (
                    new Number(first), new Number(second), operation
                ),
                GetOperationString(first, second, operation, first.Config.Culture), first.Config
            );
        }

        public static NumberO PerformArithmeticOperation(NumberO first, NumberO second, ExistingOperations operation, bool baseTenToValue = true)
        {
            return new NumberO
            (
                PerformArithmeticOperation
                (
                    new Number(first), new Number(second), operation
                ),
                first.Others.Select(x => x.Type).ToArray()
            );
        }

        public static NumberD PerformArithmeticOperation(NumberD first, NumberD second, ExistingOperations operation, bool baseTenToValue = true)
        {
            Number number = PerformArithmeticOperation
            (
                new Number(first), new Number(second), operation
            );

            return
            (
                number.Error != ErrorTypesNumber.None ? new NumberD(number.Error) :
                new NumberD
                (
                    number.Value, number.BaseTenExponent, first.Type
                )
            );
        }

        public static Number PerformArithmeticOperation(Number first, decimal second, ExistingOperations operation, bool baseTenToValue = true)
        {
            return PerformArithmeticOperation
            (
                first, new Number(second), operation
            );
        }

        public static Number MultiplyInternal(Number first, Number second)
        {
            return PerformArithmeticOperation
            (
                first, second, ExistingOperations.Multiplication, false
            );
        }

        public static NumberD MultiplyInternal(NumberD first, NumberD second)
        {
            return PerformArithmeticOperation
            (
                first, second, ExistingOperations.Multiplication, false
            );
        }

        public static Number DivideInternal(Number first, Number second)
        {
            return PerformArithmeticOperation
            (
                first, second, ExistingOperations.Division, false
            );
        }

        public static NumberD DivideInternal(NumberD first, NumberD second)
        {
            return PerformArithmeticOperation
            (
                first, second, ExistingOperations.Division, false
            );
        }

        public static Number AddInternal(Number first, Number second)
        {
            return PerformArithmeticOperation
            (
                first, second, ExistingOperations.Addition, false
            );
        }

        public static NumberD AddInternal(NumberD first, NumberD second)
        {
            return PerformArithmeticOperation
            (
                first, second, ExistingOperations.Addition, false
            );
        }

        public static Number PerformArithmeticOperation(Number first, Number second, ExistingOperations operation, bool baseTenToValue = true)
        {
            ErrorTypesNumber error = ErrorInfoNumber.GetOperationError
            (
                first, second, operation
            );
            if (error != ErrorTypesNumber.None) return new Number(error);

            Number outNumber = 
            (
                operation == ExistingOperations.Addition || operation == ExistingOperations.Subtraction ?
                PerformManagedOperationAddition
                (
                    new Number(first), new Number(second), operation
                ) : 
                PerformManagedOperationMultiplication
                (
                    new Number(first), new Number(second), operation
                )
            );

            return 
            (
                baseTenToValue ? PassBaseTenToValue(outNumber, true) : outNumber
            );
        }

        private static Number PerformManagedOperationAddition(Number first, Number second, ExistingOperations operation)
        {
            return PerformManagedOperationNormalisedValues
            (
                first,
                //In addition/subtraction, the normalised operands might require further modifications. That's why
                //calling a specific method rather than the generic NormaliseNumber.
                GetOperandsAddition(first, second, operation),
                operation
            );
        }

        private static Number[] GetOperandsAddition(Number first, Number second, ExistingOperations operation)
        {
            Number[] operands = new Number[] 
            { 
                NormaliseNumber(first), NormaliseNumber(second) 
            };

            return
            (
                operands[0].BaseTenExponent != operands[1].BaseTenExponent ?
                //The addition/subtraction might not be performed right away even with normalised values.
                AdaptNormalisedValuesForAddition(operands, operation) : operands
            );
        }

        private static Number[] AdaptNormalisedValuesForAddition(Number[] operands, ExistingOperations operation)
        {
            if (operands[0].BaseTenExponent == operands[1].BaseTenExponent)
            {
                //Having the same BaseTenExponent means that the given operation can be performed right away.
                return operands;
            }

            int[] bigSmallI =
            (
                operands[0].BaseTenExponent > operands[1].BaseTenExponent ?
                new int[] { 0, 1 } : new int[] { 1, 0 }
            );

            //Only the variable with the bigger value is modified. For example: 5*10^5 & 3*10^3 is converted
            //into 500*10^3 & 3*10^3 in order to allow the addition 500 + 3. 
            Number big2 = AdaptBiggerAdditionOperand(operands, bigSmallI, operation);
            if (big2.Error != ErrorTypesNumber.None)
            {
                return TooBigGapAddition(operands, bigSmallI, operation);
            }

            operands[bigSmallI[0]].Value = big2.Value;
            operands[bigSmallI[0]].BaseTenExponent = operands[bigSmallI[1]].BaseTenExponent;

            return operands;
        }

        //When adding/subtracting two numbers whose gap is bigger than the maximum decimal precision, there
        //is no need to perform any operation (no change will be observed anyway). This method takes care 
        //of these cases and returns the expected output (i.e., biggest value).
        private static Number[] TooBigGapAddition(Number[] operands, int[] bigSmallI, ExistingOperations operation)
        {
            Number[] outOperands = new Number[] 
            {
                //First operand together with the numeric information (i.e., Value and BaseTenExponent) which
                //is associated with the biggest one.
                new Number(operands[0])
                {
                    Value = operands[bigSmallI[0]].Value,
                    BaseTenExponent = operands[bigSmallI[0]].BaseTenExponent
                }
            };

            if (operation == ExistingOperations.Subtraction && bigSmallI[0] == 1)
            {
                outOperands[0].Value = -1m * outOperands[0].Value;
            }

            return outOperands;
        }

        private static Number AdaptBiggerAdditionOperand(Number[] operands, int[] bigSmallI, ExistingOperations operation)
        {
            int gapExponent = operands[bigSmallI[0]].BaseTenExponent - operands[bigSmallI[1]].BaseTenExponent;
            if (gapExponent >= 27)
            {
                //The difference between both inputs is bigger than (or, at least, very close to) the maximum decimal value/precision;
                //what makes this situation calculation unworthy and the first operand to be returned as the result.
                //Note that the error below these lines is just an easy way to tell the calling function about this eventuality.
                return new Number
                (
                    ErrorTypesNumber.NumericOverflow
                );
            }

            //PerformArithmeticOperationValues is used to make sure that the resulting numeric information is stored
            //in Value (if possible).
            Number big2 = PerformArithmeticOperationValues
            (
                Math2.PowSqrtInternal(new Number(10m), gapExponent, false),
                operands[bigSmallI[0]].Value, ExistingOperations.Multiplication
            );

            bool isWrong =
            (
                big2.Error != ErrorTypesNumber.None || big2.BaseTenExponent != 0 ?

                //The value of the bigger input times 10^(gap between BaseTenExponent of inputs) is too big. 
                isWrong = true :

                //Overflow-check very unlikely to trigger an error.              
                AreAdditionFinalValuesWrong
                (
                    operands[0].Value, operands[1].Value, operation
                )
            );

            return
            (
                isWrong ?
                //This error is just an easy way to let the calling function know about the fact that no
                //calculation has been performed (too big gap). This isn't a properly-speaking error and
                //that's why it will not be notified to the user.
                new Number(ErrorTypesNumber.InvalidInput) :
                //Returning the new big value. For example: with 5*10^4 & 3*10^2, 500 would be returned.
                new Number(operands[bigSmallI[0]])
                {
                    Value = big2.Value
                }
            );
        }

        private static bool AreAdditionFinalValuesWrong(decimal val1, decimal val2, ExistingOperations operation)
        {
            bool isWrong = false;

            try
            {
                val1 = val1 + val2 *
                (
                    operation == ExistingOperations.Addition ? 1 : -1
                );
            }
            catch { isWrong = true; }

            return isWrong;
        }

        private static Number PerformManagedOperationMultiplication(Number first, Number second, ExistingOperations operation)
        {
            return PerformManagedOperationNormalisedValues
            (
                first, new Number[] 
                { 
                    NormaliseNumber(first), NormaliseNumber(second) 
                },
                operation
            );
        }

        private static Number PerformManagedOperationNormalisedValues(Number outInfo, Number[] normalised, ExistingOperations operation)
        {
            return
            (
                normalised.Length == 1 ?
                //There is just one operand when the difference between both of them is too big.
                outInfo = normalised[0] :
                PerformManagedOperationTwoOperands(outInfo, normalised, operation)
            );
        }

        private static Number PerformManagedOperationTwoOperands(Number outNumber, Number[] normalised, ExistingOperations operation)
        {
            if (outNumber.Error != ErrorTypesNumber.None) return new Number(outNumber);

            Number tempNumber = PerformArithmeticOperationValues
            (
                normalised[0], normalised[1], operation
            );
            if (tempNumber.Error != ErrorTypesNumber.None) return tempNumber;


            outNumber.BaseTenExponent = tempNumber.BaseTenExponent;
            outNumber.Value = tempNumber.Value;

            return outNumber;
        }

        private static Number PerformArithmeticOperationValues(Number first, Number second, ExistingOperations operation)
        {
            if (first.Value == 0m || second.Value == 0m)
            {
                if (operation == ExistingOperations.Multiplication || operation == ExistingOperations.Division)
                {
                    //Dividing by zero scenarios are taken care of somewhere else.
                    return new Number(first) { Value = 0m };
                }
            }

            Number output = new Number(first);
            Number first2 = new Number(first);
            Number second2 = new Number(second);

            bool isWrong = false;
            try
            {
                if (operation == ExistingOperations.Addition)
                {
                    output.Value += second2.Value;
                }
                else if (operation == ExistingOperations.Subtraction)
                {
                    output.Value -= second2.Value;
                }
                else
                {
                    //The reason for checking whether BaseTenExponent is inside/outside the int range before performing 
                    //the operation (rather than going ahead and eventually catching the resulting exception) isn't just
                    //being quicker, but also the only option in many situations. Note that an addition/subtraction between
                    //two int variables whose result is outside the int range might not trigger an exception (+ random 
                    //negative value as output).
                    Number tempVar = Operations.VaryBaseTenExponent
                    (
                        output, second.BaseTenExponent, 
                        operation == ExistingOperations.Division
                    );
                    if (tempVar.Error != ErrorTypesNumber.None) return tempVar;

                    if (operation == ExistingOperations.Multiplication)
                    {
                        output.Value *= second2.Value;
                        output.BaseTenExponent += second2.BaseTenExponent;
                    }
                    else if (operation == ExistingOperations.Division)
                    {
                        if (second.Value == 0m)
                        {
                            return new Number(ErrorTypesNumber.InvalidOperation);
                        }
                        output.Value /= second2.Value;
                        output.BaseTenExponent -= second2.BaseTenExponent;
                    }
                }
            }
            catch { isWrong = true; }

            return
            (
                //An exception might not be triggered despite of dealing with numbers outside decimal precision.
                //For example: 0.00000000000000000001m * 0.0000000000000000000001m can plainly output 0m. 
                isWrong || ((operation == ExistingOperations.Multiplication || operation == ExistingOperations.Division) && output.Value == 0.0m) ?
                OperationValuesManageError(first2, second2, operation) : output
            );
        }

        private static Number OperationValuesManageError(Number outNumber, Number second, ExistingOperations operation)
        {
            Number tempVar = OperationValuesManageErrorPreAnalysis(outNumber, second, operation);
            if (tempVar != null) return tempVar;

            Number second2 = Math.Abs(second.Value);
            second2 = FromValueToBaseTenExponent(second2.Value, second2.BaseTenExponent);

            outNumber = Operations.VaryBaseTenExponent
            (
                outNumber, (operation == ExistingOperations.Multiplication ? 1 : -1) * second2.BaseTenExponent
            );

            if (Math.Abs(second2.Value) == 1m) return outNumber;

            try
            {
                outNumber = PerformArithmeticOperation(outNumber, second2.Value, operation);
            }
            catch
            {
                outNumber = OperationValuesManageError
                (
                    new Number(outNumber)
                    {
                        Value = second2.Value,
                        BaseTenExponent = 0
                    },
                    new Number()
                    {
                        Value = outNumber.Value,
                        BaseTenExponent = outNumber.BaseTenExponent
                    },
                    operation
                );
            }

            return outNumber;
        }

        private static Number OperationValuesManageErrorPreAnalysis(Number outNumber, Number second, ExistingOperations operation)
        {
            if (operation != ExistingOperations.Multiplication && operation != ExistingOperations.Division)
            {
                //This condition should never be true on account of the fact that the pre-modifications performed before
                //adding/subtracting should avoid erroneous situations.
                return new Number(ErrorTypesNumber.NumericOverflow);
            }

            //Accounting for some limit cases which might reach this point and provoke and infinite set of recursive calls.
            if (operation == ExistingOperations.Multiplication && Math.Abs(outNumber.BaseTenExponent + second.BaseTenExponent) == int.MaxValue)
            {
                return new Number(ErrorTypesNumber.NumericOverflow);
            }
            if (operation == ExistingOperations.Division && (outNumber.BaseTenExponent - second.BaseTenExponent) == int.MinValue)
            {
                return new Number(ErrorTypesNumber.NumericOverflow);
            }

            return null;
        }

        //This method is called when performing an arithmetic operation between two variables of a random
        //numeric type (i.e., dynamic numeric variable) and makes sure that the output type matches the 
        //one of the inputs. Note that, in some cases, an operation between two variables of the same type
        //might output a different one (e.g., sbyte * sbyte = int).
        //This method assumes that both inputs are non-null and belong to the same numeric type.
        internal static dynamic PerformArithmeticOperationDynamicVariables(dynamic var1, dynamic var2, ExistingOperations operation)
        {
            return Conversions.CastDynamicToType
            (
                PerformArithmeticOperationDynamicVariablesValues
                (
                    var1, var2, operation
                ), 
                var1.GetType()
            );
        }

        private static dynamic PerformArithmeticOperationDynamicVariablesValues(dynamic var1, dynamic var2, ExistingOperations operation)
        {
            if (operation == ExistingOperations.Multiplication)
            {
                return var1 * var2;
            }
            else if (operation == ExistingOperations.Division)
            {
                return var1 / var2;
            }
            else if (operation == ExistingOperations.Addition)
            {
                return var1 + var2;
            }
            else if (operation == ExistingOperations.Subtraction)
            {
                return var1 - var2;
            }

            return null;
        }

        internal static NumberD PassBaseTenToValue(NumberD input, bool showUser = false)
        {
            NumberD output = new NumberD(input);
            if (input.BaseTenExponent == 0) return output;

            output = Operations.AbsInternal(output);
            if (output.Error != ErrorTypesNumber.None)
            {
                //Although it is extremely unlikely, calculating the absolute value might provoke to go beyond the
                //maximum supported range (i.e., BaseTenExponent outside the int range).
                return new NumberD(ErrorTypesNumber.InvalidOperation);
            }

            if (showUser && output.Value < 1)
            {
                //The opposite to passing to value, but what is expected anyway.
                return NormaliseNumber(input);
            }

            bool decrease = input.BaseTenExponent > 0;
            dynamic sign = PerformArithmeticOperationDynamicVariables
            (
                input.Value, output.Value, ExistingOperations.Division
            );
            dynamic variation = Conversions.CastDynamicToType(10, output.Type);
            dynamic one = Conversions.CastDynamicToType(1, output.Type);

            while (output.BaseTenExponent != 0)
            {
                if (decrease)
                {
                    if (output.Value > Basic.AllNumberMinMaxPositives[output.Type][1] / variation)
                    {
                        break;
                    }
                    output.Value = PerformArithmeticOperationDynamicVariables
                    (
                        output.Value, variation, ExistingOperations.Multiplication
                    );
                    output.BaseTenExponent -= 1;
                }
                else
                {
                    if ((showUser && output.Value / variation < one) || output.Value < Basic.AllNumberMinMaxPositives[output.Type][0] * variation)
                    {
                        break;
                    }
                    output.Value = PerformArithmeticOperationDynamicVariables
                    (
                        output.Value, variation, ExistingOperations.Division
                    );
                    output.BaseTenExponent += 1;
                }
            }

            output.Value = PerformArithmeticOperationDynamicVariables
            (
                output.Value, sign, ExistingOperations.Multiplication
            );

            return output;
        }

        internal static Number PassBaseTenToValue(Number input, bool showUser = false)
        {
            Number output = new Number(input);
            if (input.BaseTenExponent == 0) return output;

            //No error is possible because Math.Abs(decimal.MinValue) is equal to decimal.MaxValue.
            decimal absValue = Math.Abs(output.Value);

            if (showUser && absValue < 1m)
            {
                //The opposite to passing to value, but what is expected anyway.
                return NormaliseNumber(output);
            }

            bool decrease = output.BaseTenExponent > 0;
            decimal sign = output.Value / absValue;
            output.Value = absValue;

            while (output.BaseTenExponent != 0)
            {
                if (decrease)
                {
                    if (output.Value > Basic.AllNumberMinMaxPositives[typeof(decimal)][1] / 10m)
                    {
                        break;
                    }
                    output.Value *= 10m;
                    output.BaseTenExponent -= 1;
                }
                else
                {
                    if ((showUser && output.Value / 10m < 1m) || output.Value < Basic.AllNumberMinMaxPositives[typeof(decimal)][0] * 10m)
                    {
                        break;
                    }
                    output.Value /= 10m;
                    output.BaseTenExponent += 1;
                }
            }

            output.Value *= sign;

            return output;
        }

        internal static NumberD NormaliseNumber(NumberD numberD)
        {
            if (numberD.BaseTenExponent == int.MaxValue)
            {
                return new NumberD(numberD);
            }

            Number tempVar = new Number(numberD);
            tempVar = FromValueToBaseTenExponent
            (
                tempVar.Value, tempVar.BaseTenExponent, 
                Basic.AllDecimalTypes.Contains(numberD.Type)
            );

            return new NumberD
            (
                tempVar.Value, tempVar.BaseTenExponent, numberD.Type
            );
        }

        internal static Number NormaliseNumber(Number number)
        {
            Number outNumber = new Number(number);
            if (outNumber.BaseTenExponent == int.MaxValue) return outNumber;

            return FromValueToBaseTenExponent
            (
                outNumber.Value, outNumber.BaseTenExponent
            );
        }

        private static Number FromValueToBaseTenExponent(decimal value, int baseTenExponent, bool decimals = true)
        {
            Number outNumber = new Number(value, baseTenExponent);
            if (value == 0m) return outNumber;

            decimal valueAbs = Math.Abs(value);
            bool decrease = (valueAbs > 1m);

            while (valueAbs != 1m && ((decrease && outNumber.BaseTenExponent <= int.MaxValue - 1) || (!decrease && outNumber.BaseTenExponent >= int.MinValue + 1)))
            {
                if ((decrease && valueAbs < 10m) || (!decrease && valueAbs >= 1m))
                {
                    return outNumber;
                }

                value = (decrease ? value /= 10m : value *= 10m);
                if (!decimals && Math.Floor(value) != value)
                {
                    return outNumber;
                }

                outNumber.Value = value;
                outNumber.BaseTenExponent += (decrease ? 1 : -1);

                valueAbs = Math.Abs(value);
            }

            return outNumber;
        }
    }
}