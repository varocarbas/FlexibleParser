using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    ///<summary>
    ///<para>NumberD extends the limited decimal-only range of Number by supporting all the numeric types.</para>
    ///<para>It is implicitly convertible to Number, NumberO, NumberP and all the numeric types.</para>
    ///</summary>
    public partial class NumberD
    {
        private dynamic _Value;
        private Type _Type;
        ///<summary><para>Numeric variable storing the primary value.</para></summary>
        public dynamic Value
        {
            get { return _Value; }
            set
            {
                Type type = ErrorInfoNumber.InputTypeIsValidNumeric(value);

                if (type == null) _Value = null;
                else
                {
                    _Value = value;
                    if (_Value == 0) BaseTenExponent = 0;
                }

                if(Type != type) Type = type;
            }
        }  
        ///<summary><para>Base-ten exponent complementing the primary value.</para></summary>
        public int BaseTenExponent { get; set; }
        ///<summary><para>Numeric type of the Value property.</para></summary>
        public Type Type
        {
            get { return _Type; }
            set
            {
                if (Value != null && value != null)
                {
                    if (Value.GetType() == value) _Type = value;
                    else
                    {
                        NumberD tempVar = new NumberD(Value, BaseTenExponent, value, false);

                        if (tempVar.Error == ErrorTypesNumber.None)
                        {
                            _Type = value;
                            BaseTenExponent = tempVar.BaseTenExponent;
                            Value = tempVar.Value;
                        }
                        //else -> The new type is wrong and can be safely ignored.
                    }
                }
            }
        }  
        ///<summary><para>Readonly member of the ErrorTypesNumber enum which best suits the current conditions.</para></summary>
        public readonly ErrorTypesNumber Error;

        ///<summary><para>Initialises a new NumberD instance.</para></summary>
        ///<param name="type">Type to be assigned to the dynamic Value property. Only numeric types are valid.</param>
        public NumberD(Type type)
        {
            Value = Basic.GetNumberSpecificType(0, type);
            Type = type;
        }

        ///<summary><para>Initialises a new NumberD instance.</para></summary>
        ///<param name="value">Main value to be used. Only numeric variables are valid.</param>
        ///<param name="baseTenExponent">Base-ten exponent to be used.</param>
        public NumberD(dynamic value, int baseTenExponent)
        {
            Type type = ErrorInfoNumber.InputTypeIsValidNumeric(value);

            if (type == null)
            {
                Error = ErrorTypesNumber.InvalidInput;
            }
            else
            {
                //To avoid problems with the automatic actions triggered by some setters, it is better 
                //to always assign values in this order (i.e., first BaseTenExponent, then Value and 
                //finally Type).
                BaseTenExponent = baseTenExponent;
                Value = value;
                Type = type;
            }
        }

        ///<summary><para>Initialises a new NumberD instance.</para></summary>
        ///<param name="value">Main value to be used. Only numeric variables are valid.</param>
        ///<param name="type">Type to be assigned to the dynamic Value property. Only numeric types are valid.</param>
        public NumberD(dynamic value, Type type)
        {
            NumberD numberD = ExtractValueAndTypeInfo(value, 0, type);

            if (numberD.Error != ErrorTypesNumber.None)
            {
                Error = numberD.Error;
            }
            else
            {
                BaseTenExponent = numberD.BaseTenExponent;
                Value = numberD.Value;
                Type = type;
            }
        }

        ///<summary><para>Initialises a new NumberD instance.</para></summary>
        ///<param name="value">Main value to be used. Only numeric variables are valid.</param>
        ///<param name="baseTenExponent">Base-ten exponent to be used.</param>
        ///<param name="type">Type to be assigned to the dynamic Value property. Only numeric types are valid.</param>
        public NumberD(dynamic value, int baseTenExponent, Type type)
        {
            NumberD numberD = ExtractValueAndTypeInfo
            (
                value, baseTenExponent, type
            );

            if (numberD.Error != ErrorTypesNumber.None)
            {
                Error = numberD.Error;
            }
            else
            {
                BaseTenExponent = numberD.BaseTenExponent;
                Value = numberD.Value;
                Type = type;
            }
        }

        ///<summary><para>Initialises a new NumberD instance.</para></summary>
        ///<param name="input">Variable whose information will be used. Only NumberD, Number, NumberO, NumberP, numeric and UnitParser's UnitP variables are valid.</param>
        public NumberD(dynamic input)
        {
            Type type = ErrorInfoNumber.InputTypeIsValidNumericOrNumberX(input);

            if (type == null)
            {
                Number number = OtherParts.GetNumberFromUnitP(input);

                if (number.Error == ErrorTypesNumber.None)
                {
                    BaseTenExponent = number.BaseTenExponent;
                    Value = number.Value;
                    Type = typeof(decimal);
                }
                else Error = ErrorTypesNumber.InvalidInput;
            }
            else
            {
                if (Basic.AllNumberClassTypes.Contains(type))
                {
                    BaseTenExponent = input.BaseTenExponent;
                    Value = input.Value;
                    Type =
                    (
                        type == typeof(NumberD) ? input.Type : Value.GetType()
                    );
                    Error = input.Error;
                }
                else
                {
                    Value = input;
                    if (Value != null) Type = Value.GetType();
                }
            }
        }

        internal NumberD(decimal value)
        {
            Value = value;
            Type = typeof(decimal);
        }
       
        internal NumberD(double value)
        {
            Value = value;
            Type = typeof(double);
        }

        internal NumberD(float value)
        {
            Value = value;
            Type = typeof(float);
        }

        internal NumberD(long value)
        {
            Value = value;
            Type = typeof(long);
        }

        internal NumberD(ulong value)
        {
            Value = value;
            Type = typeof(ulong);
        }

        internal NumberD(int value)
        {
            Value = value;
            Type = typeof(int);
        }

        internal NumberD(uint value)
        {
            Value = value;
            Type = typeof(uint);
        }

        internal NumberD(short value)
        {
            Value = value;
            Type = typeof(short);
        }

        internal NumberD(ushort value)
        {
            Value = value;
            Type = typeof(ushort);
        }

        internal NumberD(byte value)
        {
            Value = value;
            Type = typeof(byte);
        }

        internal NumberD(sbyte value)
        {
            Value = value;
            Type = typeof(sbyte);
        }

        internal NumberD(char value)
        {
            Value = value;
            Type = typeof(char);
        }

        internal NumberD() { }

        internal NumberD(ErrorTypesNumber error) { Error = error; }

        private NumberD(dynamic value, int baseTenExponent, Type type, bool assignType)
        {
            NumberD numberD = ExtractValueAndTypeInfo
            (
                value, baseTenExponent, type
            );

            if (numberD.Error != ErrorTypesNumber.None)
            {
                Error = numberD.Error;
            }
            else
            {
                BaseTenExponent = numberD.BaseTenExponent;
                Value = numberD.Value;
                if (assignType)
                {
                    //Condition to avoid an infinite loop when calling this constructor from the Type property setter.
                    Type = type;
                }
            }
        }

        private static NumberD ExtractValueAndTypeInfo(dynamic value, int baseTenExponent, Type type)
        {
            Type typeValue = ErrorInfoNumber.InputTypeIsValidNumeric(value);
            if (typeValue == null)
            {
                return new NumberD(ErrorTypesNumber.InvalidInput);
            }

            return
            (
                typeValue == type ? new NumberD(value, baseTenExponent) :
                Operations.VaryBaseTenExponent
                (
                    Conversions.ConvertNumberToAny
                    (
                        Conversions.ConvertAnyValueToDecimal(value), type
                    ),
                    baseTenExponent
                )
            );
        }
    }
}
