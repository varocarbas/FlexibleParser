using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    ///<summary><para>Contains all the supported error types.</para></summary>
    public enum ErrorTypesNumber
    {
        ///<summary><para>No error.</para></summary>
        None = 0,
        ///<summary><para>Error provoked by not matching the expected input format (e.g., null string).</para></summary>
        InvalidInput,
        ///<summary><para>Error provoked by not matching the expected conditions of certain mathematical operation (e.g., division by zero).</para></summary>
        InvalidOperation,
        ///<summary><para>Error provoked by not matching the expected conditions of the corresponding native System.Math method.</para></summary>
        NativeMethodError,
        ///<summary><para>Error provoked by a calculation outputting a value outside the supported range (i.e., BaseTenExponent outside the int range).</para></summary>
        NumericOverflow,
        ///<summary><para>Error provoked by a string not containing valid numeric information under the given conditions.</para></summary>
        ParseError
    }

    internal class ErrorInfoNumber
    {
        internal static dynamic GetNumberXError(Type type, ErrorTypesNumber error)
        {
            if (type == typeof(Number))
            {
                return new Number(error);
            }
            else if (type == typeof(NumberD))
            {
                return new NumberD(error);
            }
            else if (type == typeof(NumberO))
            {
                return new NumberO(error);
            }
            else if (type == typeof(NumberP))
            {
                return new NumberP(error);
            }

            return null;
        }

        internal static ErrorTypesNumber ApplyPolynomialFitError(Polynomial coefficients, Number xValue)
        {
            ErrorTypesNumber error = ErrorTypesNumber.None;

            if (coefficients == null || xValue == null) error = ErrorTypesNumber.InvalidInput;
            if (coefficients.Error != ErrorTypesNumber.None) error = coefficients.Error;
            if (xValue.Error != ErrorTypesNumber.None) error = xValue.Error;

            return error;
        }

        internal static ErrorTypesNumber GetPolynomialFitError(NumberD[] xValues, NumberD[] yValues)
        {
            ErrorTypesNumber error = ErrorTypesNumber.None;

            if (xValues == null || xValues.Length == 0 || yValues == null || yValues.Length == 0)
            {
                error = ErrorTypesNumber.InvalidInput;
            }
            else if (xValues.Length != yValues.Length)
            {
                error = ErrorTypesNumber.InvalidOperation;
            }

            return error;
        }

        internal static ErrorTypesNumber GetFactorialError(NumberD n)
        {
            ErrorTypesNumber error =
            (
                n.Value < 0 ? ErrorTypesNumber.InvalidOperation :
                ErrorInfoNumber.GetOperandError(n, typeof(long))
            );
            if (error != ErrorTypesNumber.None) return error;

            return
            (
                n >= 100000 ? ErrorTypesNumber.InvalidOperation : ErrorTypesNumber.None
            );
        }

        internal static ErrorTypesNumber GetPowTruncateError(Number number)
        {
            return
            (
                number == null || number.GetType() != typeof(Number) ?
                ErrorTypesNumber.InvalidInput : ErrorTypesNumber.None
            );
        }

        internal static ErrorTypesNumber GetOperationError(dynamic first, dynamic second, ExistingOperations operation)
        {
            ErrorTypesNumber error = GetOperandsError(first, second);
            if(error != ErrorTypesNumber.None) return error;

            return
            (
                (operation == ExistingOperations.Division || operation == ExistingOperations.Modulo) && 
                Conversions.ConvertAnyValueToDecimal(second.Value).Value == 0m ?
                ErrorTypesNumber.InvalidOperation : ErrorTypesNumber.None
            );
        }

        internal static ErrorTypesNumber GetOperandsError(dynamic first, dynamic second, Type target = null)
        {
            ErrorTypesNumber error = GetOperandError(first, target);

            return
            (
                error == ErrorTypesNumber.None ? GetOperandError(second, target) : error
            );
        }

        internal static ErrorTypesNumber GetOperandError(dynamic input, Type target = null)
        {
            if (input == null || !Basic.AllNumberClassTypes.Contains((Type)input.GetType()) || input.Value == null)
            {
                return ErrorTypesNumber.InvalidInput;
            }
            if (input.Error != ErrorTypesNumber.None) return input.Error;

            Type type = input.Value.GetType();
            if (type == typeof(double) || type == typeof(float))
            {
                ErrorTypesNumber error = GetFloatingTypeError(input.Value);
                if (error != ErrorTypesNumber.None) return ErrorTypesNumber.InvalidInput;
            }

            return
            (
                target != null && !Basic.InputInsideTypeRange(input, target) ?
                ErrorTypesNumber.NativeMethodError : ErrorTypesNumber.None
            );
        }

        internal static ErrorTypesNumber GetConvertToAnyError(Number number, Type target)
        {
            if (number == null || !Basic.AllNumericTypes.Contains(target))
            {
                return ErrorTypesNumber.InvalidInput;
            }

            return number.Error;
        }

        internal static ErrorTypesNumber GetFloatingTypeError(dynamic value)
        {
            if (value == null) return ErrorTypesNumber.InvalidInput;
            else
            {
                Type type = value.GetType();
                if (type != typeof(double) && type != typeof(float))
                {
                    return ErrorTypesNumber.InvalidInput;
                }
                else if (type == typeof(double))
                {
                    if (double.IsInfinity(value) || double.IsNaN(value))
                    {
                        return ErrorTypesNumber.InvalidInput;
                    }
                }
                else if (float.IsInfinity(value) || float.IsNaN(value))
                {
                    return ErrorTypesNumber.InvalidInput;
                }
            }

            return ErrorTypesNumber.None;
        }

        internal static Type InputTypeIsValidNumericOrNumberX(dynamic input)
        {
            return InputTypeIsOK
            (
                input, InputType.NumericAndNumberClass
            );
        }

        internal static Type InputTypeIsValidNumeric(dynamic input)
        {
            return InputTypeIsOK
            (
                input, InputType.Numeric
            );
        }

        private static Type InputTypeIsOK(dynamic input, InputType inputType)
        {
            Type type = (input == null ? null : input.GetType());
            if (type == null) return null;

            Type[] targets = null;
            if (inputType == InputType.NumberClass)
            {
                targets = Basic.AllNumberClassTypes;
            }
            else
            {
                if (type == typeof(double) || type == typeof(float))
                {
                    if (GetFloatingTypeError(input) != ErrorTypesNumber.None)
                    {
                        return null;
                    }
                }

                targets = Basic.AllNumericTypes;
                if (inputType == InputType.NumericAndNumberClass)
                {
                    targets = targets.Concat(Basic.AllNumberClassTypes).ToArray();
                }
            }

            return (targets.Contains(type) ? type : null);
        }

        internal enum InputType { Numeric, NumberClass, NumericAndNumberClass }
    }
}
