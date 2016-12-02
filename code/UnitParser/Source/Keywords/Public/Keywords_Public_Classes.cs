using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace FlexibleParser
{
    ///<summary><para>Contains the main information associated with the constituent parts of each unit.</para></summary>
    public class UnitPart
    {
        ///<summary><para>Unit associated with the current part.</para></summary>
        public Units Unit { get; set; }
        ///<summary><para>Prefix information associated with the current part.</para></summary>
        public Prefix Prefix { get; set; }
        ///<summary><para>Exponent associated with the current part.</para></summary>
        public int Exponent { get; set; }

        ///<summary><para>Initialises a new UnitPart instance.</para></summary>
        ///<param name="unit">Member of the Units enum to be used.</param>
        ///<param name="prefix">Prefix variable whose information will be used.</param>
        ///<param name="exponent">Integer exponent to be used.</param>
        public UnitPart(Units unit, Prefix prefix, int exponent = 1)
        {
            Unit = unit;
            Prefix = new Prefix(prefix);
            Exponent = exponent;
        }

        ///<summary><para>Initialises a new UnitPart instance.</para></summary>
        ///<param name="unit">Member of the Units enum to be used.</param>
        ///<param name="exponent">Integer exponent to be used.</param>
        public UnitPart(Units unit, int exponent = 1)
        {
            Unit = unit;
            Prefix = new Prefix(1m);
            Exponent = exponent;
        }

        ///<summary><para>Initialises a new UnitPart instance.</para></summary>
        ///<param name="unitPart">UnitPart variable whose information will be used.</param>
        public UnitPart(UnitPart unitPart)
        {
            if (unitPart == null) unitPart = new UnitPart(Units.None);

            Unit = unitPart.Unit;
            Prefix = new Prefix(unitPart.Prefix);
            Exponent = unitPart.Exponent;
        }

        internal UnitPart(Units unit, decimal prefixFactor, int exponent = 1)
        : this(unit, new Prefix(prefixFactor), exponent) { }

        public static bool operator ==(UnitPart first, UnitPart second)
        {
            return UnitP.NoNullEquals(first, second);
        }

        public static bool operator !=(UnitPart first, UnitPart second)
        {
            return !UnitP.NoNullEquals(first, second);
        }

        public bool Equals(UnitPart other)
        {
            return
            (
                object.Equals(other, null) ?
                false :
                UnitP.UnitPartsAreEqual(this, other)
            );
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as UnitPart);
        }

        public override int GetHashCode() { return 0; }
    }

    ///<summary><para>Contains the main information associated with unit prefixes.</para></summary>
    public class Prefix
    {
        ///<summary><para>Name of the unit prefix.</para></summary>
        public readonly string Name = "None";
        ///<summary><para>Symbol of the unit prefix.</para></summary>
        public readonly string Symbol = "";
        ///<summary><para>Multiplying factor associated with the unit prefix.</para></summary>
        public readonly decimal Factor = 1m;
        ///<summary><para>Type of the unit prefix.</para></summary>
        public readonly PrefixTypes Type;
        ///<summary><para>Usage conditions of the unit prefix.</para></summary>
        public readonly PrefixUsageTypes PrefixUsage;

        ///<summary><para>Initialises a new Prefix instance.</para></summary>
        public Prefix() { }

        ///<summary><para>Initialises a new Prefix instance.</para></summary>
        ///<param name="prefixUsage">Member of the PrefixUsageTypes enum to be used.</param>
        public Prefix(PrefixUsageTypes prefixUsage) { PrefixUsage = prefixUsage; }

        ///<summary><para>Initialises a new Prefix instance.</para></summary>
        ///<param name="factor">Multiplying factor to be used.</param>
        ///<param name="prefixUsage">Member of the PrefixUsageTypes enum to be used.</param>
        public Prefix(decimal factor, PrefixUsageTypes prefixUsage = PrefixUsageTypes.DefaultUsage)
        {
            Factor = factor;
            PrefixUsage = prefixUsage;
            Type = GetType(Factor, "");

            if (Type != PrefixTypes.None)
            {
                Factor = factor;
                Name = GetName(Type, Factor);
                Symbol =
                (
                    Type == PrefixTypes.SI ?
                    UnitP.AllSIPrefixSymbols :
                    UnitP.AllBinaryPrefixSymbols
                )
                .First(x => x.Value == Factor).Key;
            }
            else Factor = 1m;
        }

        ///<summary><para>Initialises a new Prefix instance.</para></summary>
        ///<param name="symbol">Symbol (case does matter) defining the current prefix.</param>
        ///<param name="prefixUsage">Member of the PrefixUsageTypes enum to be used.</param>
        public Prefix(string symbol, PrefixUsageTypes prefixUsage = PrefixUsageTypes.DefaultUsage)
        {
            PrefixUsage = prefixUsage;
            Type = GetType(1m, symbol);

            if (Type != PrefixTypes.None)
            {
                Symbol = symbol;
                Factor =
                (
                    Type == PrefixTypes.SI ?
                    UnitP.AllSIPrefixSymbols[symbol] :
                    UnitP.AllBinaryPrefixSymbols[symbol]
                );
                Name = GetName(Type, Factor);
            }
            else Factor = 1m;
        }

        ///<summary><para>Initialises a new Prefix instance.</para></summary>
        ///<param name="prefix">Prefix variable whose information will be used.</param>
        public Prefix(Prefix prefix)
        {
            if (prefix == null) prefix = new Prefix();

            Name = prefix.Name;
            Symbol = prefix.Symbol;
            Factor = prefix.Factor;
            Type = prefix.Type;
            PrefixUsage = prefix.PrefixUsage;
        }

        private static PrefixTypes GetType(decimal factor, string symbol)
        {
            PrefixTypes outType = PrefixTypes.None;
            if (factor == 1m && symbol == "") return outType;

            if (factor != 1m)
            {
                if (UnitP.AllSIPrefixes.ContainsValue(factor))
                {
                    outType = PrefixTypes.SI;
                }
                else if (UnitP.AllBinaryPrefixes.ContainsValue(factor))
                {
                    outType = PrefixTypes.Binary;
                }
            }
            else
            {
                if (UnitP.AllSIPrefixSymbols.ContainsKey(symbol))
                {
                    outType = PrefixTypes.SI;
                }
                else if (UnitP.AllBinaryPrefixSymbols.ContainsKey(symbol))
                {
                    outType = PrefixTypes.Binary;
                }
            }

            return outType;
        }

        private static string GetName(PrefixTypes type, decimal factor)
        {
            return
            (
                type == PrefixTypes.SI ?
                UnitP.AllSIPrefixes.First(x => x.Value == factor).Key.ToString() :
                UnitP.AllBinaryPrefixes.First(x => x.Value == factor).Key.ToString()
            );
        }

        public static bool operator ==(Prefix first, Prefix second)
        {
            return UnitP.NoNullEquals(first, second);
        }

        public static bool operator !=(Prefix first, Prefix second)
        {
            return !UnitP.NoNullEquals(first, second);
        }

        public bool Equals(Prefix other)
        {
            return
            (
                object.Equals(other, null) ?
                false :
                UnitP.PrefixesAreEqual(this, other)
            );
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Prefix);
        }

        public override int GetHashCode() { return 0; }
    }
}
