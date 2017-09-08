using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleParser
{
    public partial class NumberO
    {
        private static List<NumberD> PopulateOthers(decimal value, int baseTenExponent, IEnumerable<Type> types)
        {
            List<NumberD> outList = new List<NumberD>();
            if (types == null) return outList;

            foreach (Type type in types)
            {
                outList.Add
                (
                    new NumberD(value, baseTenExponent, type)
                );
            }

            return outList;
        }

        private static IEnumerable<Type> CheckOtherTypes(IEnumerable<Type> types)
        {
            if (types == null || types.Count() == 0) yield break;

            foreach (Type type in types)
            {
                if (type != null && type != typeof(decimal) && Basic.AllNumericTypes.Contains(type))
                {
                    yield return type;
                }
            }
        }

        private static IEnumerable<Type> GetAssociatedTypes(OtherTypes otherType)
        {
            Type[] types = null;

            if (otherType == OtherTypes.AllTypes)
            {
                types = (Type[])Basic.AllNumericTypes.Clone();
            }
            else if (otherType == OtherTypes.MostCommonTypes)
            {
                types = new Type[] 
                { 
                    typeof(decimal), typeof(double), typeof(long), typeof(int)
                };
            }
            else if (otherType == OtherTypes.DecimalTypes)
            {
                types = new Type[] 
                { 
                    typeof(decimal), typeof(double), typeof(float)
                };
            }
            else if (otherType == OtherTypes.IntegerTypes)
            {
                types = new Type[] 
                { 
                    typeof(long), typeof(ulong), typeof(int), typeof(uint), typeof(short), 
                    typeof(ushort), typeof(sbyte), typeof(byte), typeof(char) 
                };
            }
            else if (otherType == OtherTypes.SignedTypes)
            {
                types = new Type[] 
                { 
                    typeof(decimal), typeof(double), typeof(float), typeof(long), typeof(int), 
                    typeof(short), typeof(sbyte)
                };
            }
            else if (otherType == OtherTypes.UnsignedTypes)
            {
                types = new Type[] 
                { 
                    typeof(ulong), typeof(uint), typeof(ushort), typeof(byte), typeof(char) 
                };
            }
            else if (otherType == OtherTypes.BigTypes)
            {
                types = new Type[] 
                { 
                    typeof(decimal), typeof(double), typeof(float), typeof(long), typeof(ulong),
                    typeof(int), typeof(uint)
                };
            }
            else if (otherType == OtherTypes.SmallTypes)
            {
                types = new Type[]                 
                { 
                    typeof(short), typeof(ushort), typeof(sbyte), typeof(byte), typeof(char)
                };
            }

            if (types == null) yield break;

            foreach (Type type in types)
            {
                yield return type;
            }
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
