using System;
using System.Collections.Generic;

namespace FlexibleParser
{
    public partial class UnitP
    {
        ///<summary>
        ///<para>Adds two UnitP variables by giving preference to the configuration of the first operand.</para>
        ///<para>Different unit types will trigger an error.</para>        
        ///</summary>
        ///<param name="first">Augend. In case of incompatibilities, its configuration would prevail.</param>
        ///<param name="second">Addend.</param>
        public static UnitP operator +(UnitP first, UnitP second)
        {
            return PerformUnitOperation
            (
                first, second, Operations.Addition,
                GetOperationString(first, second, Operations.Addition)
            );
        }

        ///<summary>
        ///<para>Subtracts two UnitP variables by giving preference to the configuration of the first operand.</para>
        ///<para>Different unit types will trigger an error.</para>       
        ///</summary>
        ///<param name="first">Minuend. In case of incompatibilities, its configuration would prevail.</param>
        ///<param name="second">Subtrahend.</param>
        public static UnitP operator -(UnitP first, UnitP second)
        {
            return PerformUnitOperation
            (
                first, second, Operations.Subtraction,
                GetOperationString(first, second, Operations.Subtraction)
            );
        }

        ///<summary>
        ///<para>Multiplies two UnitP variables by giving preference to the configuration of the first operand.</para>
        ///<para>Different unit types will trigger an error.</para>     
        ///</summary>
        ///<param name="first">Multiplicand. In case of incompatibilities, its configuration would prevail.</param>
        ///<param name="second">Multiplier.</param>
        public static UnitP operator *(UnitP first, UnitP second)
        {
            return PerformUnitOperation
            (
                first, second, Operations.Multiplication,
                GetOperationString(first, second, Operations.Multiplication)                
            );
        }

        ///<summary>
        ///<para>Multiplies the value of a UnitP variable by a decimal one.</para>
        ///<para>Eventual errors will be managed as defined in first.ExceptionHandling.</para>        
        ///</summary>
        ///<param name="first">Multiplicand.</param>
        ///<param name="second">Multiplier.</param>
        public static UnitP operator *(UnitP first, decimal second)
        {
            return PerformUnitOperation
            (
                first, second, Operations.Multiplication, 
                GetOperationString(first, second, Operations.Multiplication)
            );
        }

        ///<summary>
        ///<para>Multiplies a decimal variable by the value of a UnitP one.</para>
        ///<para>Eventual errors will be managed as defined by the decimal type.</para>        
        ///</summary>
        ///<param name="first">Multiplicand.</param>
        ///<param name="second">Multiplier.</param>
        public static UnitP operator *(decimal first, UnitP second)
        {
            return PerformUnitOperation
            (
                first, second, Operations.Multiplication,
                GetOperationString(first, second, Operations.Multiplication)
            );
        }

        ///<summary>
        ///<para>Multiplies the value of a UnitP variable by a double one.</para>
        ///<para>Eventual errors will be managed as defined in first.ExceptionHandling.</para>   
        ///<para>Eventual double to decimal conversion errors will be managed internally.</para> 
        ///</summary>
        ///<param name="first">Multiplicand.</param>
        ///<param name="second">Multiplier.</param>
        public static UnitP operator *(UnitP first, double second)
        {
            return PerformUnitOperation
            (
                first, second, Operations.Multiplication,
                GetOperationString(first, second, Operations.Multiplication)
            );
        }

        ///<summary>
        ///<para>Multiplies a double variable by the value of a UnitP one.</para>
        ///<para>Eventual errors will be managed as defined by the double type.</para>   
        ///<para>Eventual double to decimal conversion errors will be managed internally.</para> 
        ///</summary>
        ///<param name="first">Multiplicand.</param>
        ///<param name="second">Multiplier.</param>
        public static UnitP operator *(double first, UnitP second)
        {
            return PerformUnitOperation
            (
                first, second, Operations.Multiplication,
                GetOperationString(first, second, Operations.Multiplication)
            );
        }

        ///<summary>
        ///<para>Divides two UnitP variables by giving preference to the configuration of the first operand.</para>
        ///<para>Different unit types will trigger an error.</para>        
        ///</summary>
        ///<param name="first">Dividend. In case of incompatibilities, its configuration would prevail.</param>
        ///<param name="second">Divisor.</param>
        public static UnitP operator /(UnitP first, UnitP second)
        {
            return PerformUnitOperation
            (
                first, second, Operations.Division,
                GetOperationString(first, second, Operations.Division)
            );
        }

        ///<summary>
        ///<para>Divides the value of a UnitP variable by a decimal one.</para>
        ///<para>Eventual errors will be managed as defined in first.ExceptionHandling.</para>      
        ///</summary>
        ///<param name="first">Dividend.</param>
        ///<param name="second">Divisor.</param>
        public static UnitP operator /(UnitP first, decimal second)
        {
            return PerformUnitOperation
            (
                first, second, Operations.Division,
                GetOperationString(first, second, Operations.Division)
            );
        }

        ///<summary>
        ///<para>Divides a decimal variable by the value of a UnitP one.</para>
        ///<para>Eventual errors will be managed as defined by the decimal type.</para>            
        ///</summary>
        ///<param name="first">Multiplicand.</param>
        ///<param name="second">Multiplier.</param>
        public static UnitP operator /(decimal first, UnitP second)
        {
            return PerformUnitOperation
            (
                first, second, Operations.Division, 
                GetOperationString(first, second, Operations.Division)
            );
        }

        ///<summary>
        ///<para>Divides the value of a UnitP variable by a double one.</para>
        ///<para>Eventual errors will be managed as defined in first.ExceptionHandling.</para>   
        ///<para>Eventual double to decimal conversion errors will be managed internally.</para> 
        ///</summary>
        ///<param name="first">Multiplicand.</param>
        ///<param name="second">Multiplier.</param>
        public static UnitP operator /(UnitP first, double second)
        {
            return PerformUnitOperation
            (
                first, second, Operations.Division, 
                GetOperationString(first, second, Operations.Division)
            );
        }

        ///<summary>
        ///<para>Divides a double variable by the value of a UnitP one.</para>
        ///<para>Eventual errors will be managed as defined by the double type.</para>   
        ///<para>Eventual double to decimal conversion errors will be managed internally.</para> 
        ///</summary>
        ///<param name="first">Dividend.</param>
        ///<param name="second">Divisor.</param>
        public static UnitP operator /(double first, UnitP second)
        {
            return PerformUnitOperation
            (
                first, second, Operations.Division, 
                GetOperationString(first, second, Operations.Division)
            );
        }

        public static bool operator ==(UnitP first, UnitP second)
        {
            return NoNullEquals(first, second);
        }

        public static bool operator !=(UnitP first, UnitP second)
        {
            return !NoNullEquals(first, second);
        }

        public bool Equals(UnitP other)
        {
            return
            (
                object.Equals(other, null) ? false : UnitPVarsAreEqual(this, other)
            );
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as UnitP);
        }

        public override int GetHashCode() 
        { 
            return 0; 
        }
    }
}
