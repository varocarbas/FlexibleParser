using System;
using System.Collections.Generic;

namespace FlexibleParser
{
    public partial class UnitP
    {
        ///<summary><para>Determines whether wrong inputs trigger an exception or not.</para></summary>
        public enum ExceptionHandlingTypes
        {
            ///<summary><para>Wrong inputs never trigger an exception. Similar to standard .NET TryParse methods.</para></summary>
            NeverTriggerException = 0,
            ///<summary><para>Wrong inputs always trigger an exception. Similar to standard .NET Parse methods.</para></summary>
            AlwaysTriggerException
        };

        ///<summary><para>All the supported error types.</para></summary>
        public enum ErrorTypes 
        {
            ///<summary><para>No error occurred.</para></summary>
            None = 0,
            
            ///<summary>
            ///<para>Associated with invalid operations between UnitP variables not provoked by numeric errors.</para>
            ///<para>Examples: addition of UnitP variables with different types; multiplication of UnitP variables outputing an unsupported unit.</para>
            ///</summary>
            InvalidOperation,
            ///<summary>
            ///<para>Associated with faulty instantiations of UnitP variables.</para>
            ///<para>Examples: relying on an unsupported input string; including prefixes not supported in that specific context.</para>
            ///</summary>
            InvalidUnit,
            ///<summary>
            ///<para>Associated with invalid operations provoked by numeric overflow or arithmetic errors.</para>
            ///<para>Examples: multiplication of too big values; division by zero.</para>
            ///</summary>
            NumericError,
            ///<summary>
            ///<para>Associated with errors triggered when parsing numeric inputs.</para>
            ///<para>Example: new UnitP("unit") rather than new UnitP("number unit").</para>
            ///</summary>
            NumericParsingError,
            ///<summary>
            ///<para>Associated with unit conversion errors .</para>
            ///<para>Example: UnitP("1 m/s").ConvertCurrentUnitTo("m/s2").</para>
            ///</summary>
            InvalidUnitConversion,
        }

        ///<summary><para>Contains the main information associated with errors and exceptions.</para></summary>
        public class ErrorInfo
        {
            ///<summary><para>Error type.</para></summary>
            public readonly ErrorTypes Type;
            ///<summary><para>Exception handling type.</para></summary>
            public readonly ExceptionHandlingTypes ExceptionHandling;
            ///<summary><para>Error message.</para></summary>
            public readonly string Message;

            ///<summary><para>Initialises a new instance of ErrorInfo.</para></summary>
            public ErrorInfo() { }

            ///<summary><para>Initialises a new instance of ErrorInfo.</para></summary>
            ///<param name="errorInfo">ErrorInfo variable whose information will be used.</param>
            public ErrorInfo(ErrorInfo errorInfo) 
            {
                if (errorInfo == null) errorInfo = new ErrorInfo();
                
                Type = errorInfo.Type;
                ExceptionHandling = errorInfo.ExceptionHandling;
                Message = errorInfo.Message;
            }

            ///<summary><para>Initialises a new instance of ErrorInfo.</para></summary>
            ///<param name="type">Error type.</param>
            ///<param name="exceptionHandling">Exception handling type.</param>
            public ErrorInfo(ErrorTypes type, ExceptionHandlingTypes exceptionHandling = ExceptionHandlingTypes.NeverTriggerException)
            {
                Type = type;
                ExceptionHandling = exceptionHandling;
                Message = GetMessage(type);

                if (type != ErrorTypes.None && ExceptionHandling == ExceptionHandlingTypes.AlwaysTriggerException)
                {
                    throw new Exception(Message);
                }  
            }

            private string GetMessage(ErrorTypes type)
            {
                string outString = "";

                if (type == ErrorTypes.InvalidOperation)
                {
                    outString = "Invalid Operation. Some operands are incompatible among them or refer to invalid units.";
                }
                else if (type == ErrorTypes.InvalidUnit)
                {
                    outString = "Invalid Input. The input string doesn't match any supported unit.";
                }
                else if (type == ErrorTypes.NumericError)
                {
                    outString = "Numeric Error. An invalid mathematical operation has been performed.";
                }
                else if (type == ErrorTypes.NumericParsingError)
                {
                    outString = "Numeric Parsing Error. The input doesn't match the expected number + space + unit format.";
                }
                else if (type == ErrorTypes.InvalidUnitConversion)
                {
                    outString = "Invalid Unit Conversion. The unit conversion cannot be performed with these inputs.";
                }

                return outString;
            }

            public static bool operator ==(ErrorInfo first, ErrorInfo second)
            {
                return NoNullEquals(first, second);
            }

            public static bool operator !=(ErrorInfo first, ErrorInfo second)
            {
                return !NoNullEquals(first, second);
            }

            public bool Equals(ErrorInfo other)
            {
                return
                (
                    object.Equals(other, null) ? false :
                    ErrorsAreEqual(this, other)
                );
            }

            public override bool Equals(object obj)
            {
                return Equals(obj as ErrorInfo);
            }

            public override int GetHashCode() { return 0; }
        }

        //Called before starting unit conversions triggered by public methods.
        private static ErrorTypes PrelimaryErrorCheckConversion(UnitP original, UnitInfo targetInfo)
        {
            ErrorTypes outError = ErrorTypes.None;

            if (original == null)
            {
                outError = ErrorTypes.InvalidUnit;
            }
            else if (original.Error.Type != ErrorTypes.None)
            {
                outError = original.Error.Type;
            }
            else if (targetInfo.Error.Type != ErrorTypes.None)
            {
                outError = targetInfo.Error.Type;
            }

            return outError;
        }

        //Called before starting any unit conversion.
        private static ErrorTypes GetConversionError(UnitInfo originalInfo, UnitInfo targetInfo)
        {
            ErrorTypes outError = ErrorTypes.None;

            if (originalInfo.Unit == Units.None || targetInfo.Unit == Units.None)
            {
                outError = ErrorTypes.InvalidUnit;
            }
            else if (originalInfo.Unit == Units.Unitless || targetInfo.Unit == Units.Unitless)
            {
                outError = ErrorTypes.InvalidUnitConversion;
            }
            else if (originalInfo.Error.Type != ErrorTypes.None)
            {
                outError = originalInfo.Error.Type;
            }
            else if (targetInfo.Error.Type != ErrorTypes.None)
            {
                outError = targetInfo.Error.Type;
            }
            else if (originalInfo.Type == UnitTypes.None || originalInfo.Type != targetInfo.Type)
            {
                outError = ErrorTypes.InvalidUnitConversion;
            }

            return outError;
        }

        //Called before starting unit-part conversions.
        private static ErrorTypes GetUnitPartConversionError(UnitPart originalPart, UnitPart targetPart)
        {
            ErrorTypes outError = ErrorTypes.None;

            if (GetTypeFromUnitPart(originalPart) != GetTypeFromUnitPart(targetPart))
            {
                outError = ErrorTypes.InvalidUnitConversion;
            }
            else if (IsUnnamedUnit(originalPart.Unit) || IsUnnamedUnit(targetPart.Unit))
            {
                //Finding an unnamed compound here would be certainly an error.
                outError = ErrorTypes.InvalidUnitConversion;
            }

            return outError;
        }

        //Called before performing any operation.
        private static ErrorTypes GetOperationError(UnitInfo unitInfo1, UnitInfo unitInfo2, Operations operation)
        {
            if (operation == Operations.None) return ErrorTypes.InvalidOperation;
            if (operation == Operations.Division && unitInfo2.Value == 0m)
            {
                return ErrorTypes.NumericError;
            }

            foreach (UnitInfo info in new UnitInfo[] { unitInfo1, unitInfo2 })
            {
                if (info.Error.Type != ErrorTypes.None)
                {
                    return info.Error.Type;
                }
            }

            return ErrorTypes.None;
        }

        //Called before performing unit-unit operations.
        private static ErrorTypes GetUnitOperationError(UnitP first, UnitP second, Operations operation)
        {
            return
            (
                first.Unit == Units.None || second.Unit == Units.None ?
                ErrorTypes.InvalidUnit : 
                GetOperationError
                (
                    new UnitInfo(first), new UnitInfo(second), operation
                )
            );
        }

        //Called before performing unit-value/value-unit operations.
        private static ErrorTypes GetUnitValueOperationError(UnitP unitP, UnitInfo firstInfo, UnitInfo secondInfo, Operations operation)
        {
            return
            (
                //unitP always stores the information of the unit operand.
                unitP.Unit == Units.None ? 
                ErrorTypes.InvalidUnit :
                GetOperationError
                (
                    firstInfo, secondInfo, operation
                )
            );
        }
    }
}