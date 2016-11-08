using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FlexibleParser
{
    internal partial class Operations
    {
        public static bool NumberXsAreEqualBasic(dynamic first, dynamic second)
        {
            if (first == null || second == null) return (first == second);
            if (first.Error != second.Error) return false;

            dynamic first2 = first;
            dynamic second2 = second;
            if (first.BaseTenExponent != 0 || second.BaseTenExponent != 0)
            {
                first2 = Operations.PassBaseTenToValue(first);
                second2 = Operations.PassBaseTenToValue(second);
            }

            return ValuesAreEqual(first2, second2);
        }

        public static bool NumberXsAreEqual(Number first, Number second)
        {
            return NumberXsAreEqualBasic(first, second);
        }

        public static bool NumberXsAreEqual(NumberD first, NumberD second)
        {
            return 
            (
                first.Type == second.Type && NumberXsAreEqualBasic(first, second)    
            );
        }

        public static bool NumberXsAreEqual(NumberO first, NumberO second)
        {
            return
            (
                NumberXsAreEqualBasic(first, second) && 
                OthersAreEqual(first.Others, second.Others)
            );
        }

        private static bool OthersAreEqual(ReadOnlyCollection<NumberD> first, ReadOnlyCollection<NumberD> second)
        {
            if (first.Count != second.Count) return false;

            foreach (var item1 in first)
            {
                var item2 = second.FirstOrDefault(x => x.Type == item1.Type);
                if (item2 == null) return false;
            }

            return true;
        }

        public static bool NumberXsAreEqual(NumberP first, NumberP second)
        {
            return
            (
                NumberXsAreEqualBasic(first, second) && 
                first.OriginalString == second.OriginalString &&
                ParseConfigsAreEqual(first.Config, second.Config)
            );
        }

        public static bool ParseConfigsAreEqual(ParseConfig first, ParseConfig second)
        {
            return
            (
                first.NumberStyle == second.NumberStyle && first.Target == second.Target &&
                first.Culture.LCID == second.Culture.LCID && first.ParseType == second.ParseType
            );
        }

        public static bool PolynomialsAreEqual(Polynomial first, Polynomial second)
        {
            return
            (
                first.Error == second.Error && NumberXsAreEqual(first.A, second.A) &&
                NumberXsAreEqual(first.B, second.B) && NumberXsAreEqual(first.C, second.C) 
            );
        }

        public static bool NoNullEquals(dynamic first, dynamic second)
        {
            return
            (
                object.Equals(first, null) ? 
                object.Equals(second, null) :
                first.Equals(second)
            );
        }

        private static bool ValuesAreEqual(dynamic first, dynamic second)
        {
            return
            (
                first.Value == second.Value &&
                (
                    ErrorInfoNumber.InputTypeIsValidNumeric(first.Value) == 
                    ErrorInfoNumber.InputTypeIsValidNumeric(second.Value) 
                )
                && first.BaseTenExponent == second.BaseTenExponent
            );
        }
    }
}
