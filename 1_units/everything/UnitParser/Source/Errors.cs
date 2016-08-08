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

        ///<summary><para>Stores all the information and performs all the actions related to UnitP error and exception management.</para></summary>
        public class ErrorInfo
        {
            public readonly ErrorTypes Type;
            public readonly ExceptionHandlingTypes ExceptionHandling;
            public readonly string Message;

            public ErrorInfo() { }

            public ErrorInfo(ErrorInfo errorInfo) 
            {
                if (errorInfo == null) errorInfo = new ErrorInfo();
                
                Type = errorInfo.Type;
                ExceptionHandling = errorInfo.ExceptionHandling;
                Message = errorInfo.Message;
            }

            public ErrorInfo(ErrorTypes type, ExceptionHandlingTypes exceptionHandling = ExceptionHandlingTypes.NeverTriggerException)
            {
                Type = type;
                ExceptionHandling = exceptionHandling;
                Message = GetMessage(type);

                if (ExceptionHandling == ExceptionHandlingTypes.AlwaysTriggerException)
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
    }
}