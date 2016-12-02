using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    public partial class NumberO
    {
        private static List<NumberD> PopulateOthers(decimal value, int baseTenExponent, List<NumberD> others)
        {
            if (others == null) return new List<NumberD>();

            for (int i = others.Count - 1; i >= 0; i--)
            {
                if (others[i].Type == typeof(decimal) || others[i].Type == null)
                {
                    others.RemoveAt(i);
                }
                else
                {
                    others[i] = new NumberD(value, baseTenExponent, others[i].Type);
                }
            }

            return others;
        }

        private static List<NumberD> GetOtherVersions(Type[] types)
        {
            List<NumberD> list = new List<NumberD>();
            if (types == null || types.Length == 0) return list;

            foreach (Type type in types)
            {
                if (type != typeof(decimal) && Basic.AllNumericTypes.Contains(type))
                {
                    list.Add
                    (
                        new NumberD
                        (
                            Conversions.CastDynamicToType(0, type), 0, type
                        )
                    );
                }
            }

            return new List<NumberD>(list);
        }

        private static Type[] GetAssociatedTypes(OtherTypes otherType)
        {
            if (otherType == OtherTypes.AllTypes)
            {
                return Basic.AllNumericTypes;
            }
            else if (otherType == OtherTypes.MostCommonTypes)
            {
                return new Type[] 
                { 
                    typeof(decimal), typeof(double), typeof(long), typeof(int)
                };
            }
            else if (otherType == OtherTypes.DecimalTypes)
            {
                return new Type[] 
                { 
                    typeof(decimal), typeof(double), typeof(float)
                };
            }
            else if (otherType == OtherTypes.IntegerTypes)
            {
                return new Type[] 
                { 
                    typeof(long), typeof(ulong), typeof(int), typeof(uint), typeof(short), 
                    typeof(ushort), typeof(sbyte), typeof(byte), typeof(char) 
                };
            }
            else if (otherType == OtherTypes.SignedTypes)
            {
                return new Type[] 
                { 
                    typeof(decimal), typeof(double), typeof(float), typeof(long), typeof(int), 
                    typeof(short), typeof(sbyte)
                };
            }
            else if (otherType == OtherTypes.UnsignedTypes)
            {
                return new Type[] 
                { 
                    typeof(ulong), typeof(uint), typeof(ushort), typeof(byte), typeof(char) 
                };
            }
            else if (otherType == OtherTypes.BigTypes)
            {
                return new Type[] 
                { 
                    typeof(decimal), typeof(double), typeof(float), typeof(long), typeof(ulong),
                    typeof(int), typeof(uint)
                };
            }
            else if (otherType == OtherTypes.SmallTypes)
            {
                return new Type[]                 
                { 
                    typeof(short), typeof(ushort), typeof(sbyte), typeof(byte), typeof(char)
                };
            }

            return null;
        }
    }

    ///<summary><para>Determines the group of numeric types to be considered at NumberO instantiation.</para></summary>
    public enum OtherTypes
    {
        ///<summary><para>No types.</para></summary>
        None = 0,
        ///<summary><para>All the numeric types.</para></summary>
        AllTypes,
        ///<summary><para>Only the following types: decimal, double, long and int.</para></summary>
        MostCommonTypes,
        ///<summary><para>Only the following types: long, ulong, int, uint, short, ushort, char, sbyte and byte.</para></summary>      
        IntegerTypes,
        ///<summary><para>Only the following types: decimal, double and float.</para></summary>           
        DecimalTypes,
        ///<summary><para>Only the following types: decimal, double, float, long, int, short and sbyte.</para></summary>           
        SignedTypes,
        ///<summary><para>Only the following types: ulong, uint, ushort, byte and char.</para></summary>           
        UnsignedTypes,
        ///<summary><para>Only the following types: decimal, double, float, long, ulong, int and uint.</para></summary>                   
        BigTypes,
        ///<summary><para>Only the following types: short, ushort, sbyte, byte and char.</para></summary>
        SmallTypes
    }
}
