using System;
using System.Collections.Generic;
using FlexibleParser;
using System.Globalization;

namespace Test
{
    public class NumberParser
    {
        public static void StartTest()
        {
            Console.WriteLine("-------------- NumberParser --------------");
            Console.WriteLine();
            
            //------ There are 4 main classes (NumberX) which take care of different actions.
            
            //--- Number is the simplest and lightest one. 
            PrintSampleItem("Ini1", new Number(123.45678945699m));
            PrintSampleItem("Ini2", new Number(decimal.MaxValue, int.MaxValue));

            //--- NumberD is the simplest version supporting any numeric type. 
            PrintSampleItem("Ini3", new NumberD(1.5634456f));
            PrintSampleItem("Ini4", new NumberD(555555555555555555, typeof(long)));

            //--- NumberO can deal with different numeric types at the same time. 
            PrintSampleItem("Ini5", new NumberO(1567894563321.5634456m, OtherTypes.IntegerTypes));
            PrintSampleItem("Ini6", new NumberO(new NumberD(53264485), new Type[] { typeof(short), typeof(double) }));

            //--- NumberP is the only one extracting numeric information from strings. 
            PrintSampleItem("Ini7", new NumberP("12555555.2", new ParseConfig(typeof(byte))));
            PrintSampleItem("Ini8", new NumberP("1 00 00 000", new ParseConfig() { ParseType = ParseTypes.ParseThousandsStrict }));


            //------ All the NumberX classes support various implicit conversions. 

            //--- All of them are implicitly convertible between each other. 
            PrintSampleItem("Imp1", (Number)new NumberD(1234556789));
            PrintSampleItem("Imp2", (NumberO)new NumberP("123456.77"));

            //--- All of them are also implicitly convertible to either numeric types (Number/NumberD/NumberO) or strings (NumberP). 
            PrintSampleItem("Imp3", (NumberD)1234556789);
            PrintSampleItem("Imp4", (NumberP)"555e123456");


            //------ All the NumberX classes support the most common operators. 

            //--- Arithmetic operators.
            PrintSampleItem("Op1", new Number(1.233333658789m) + (Number)0.0000000012);
            PrintSampleItem("Op2", new Number(1000m) * (Number)new NumberD(55555555555555555555.55555) / (Number)new NumberP("1e-350"));

            //--- Comparison operators.
            PrintSampleItem("Op3", (new Number(555m, -3) == ((Number)new NumberP("0.555")) ? new Number(1m) : new Number()));
            PrintSampleItem("Op4", (new Number(123.5m) <= ((Number)new NumberD(123)) ? new Number(1m) : new Number()));


            //------ Math2 contains NumberX-adapted versions of all the .NET System.Math methods. 

            //--- The expectations of the corresponding native method have to be met, otherwise an error would be triggered.
            PrintSampleItem("Math1", Math2.Max(999, 1.234)); //Valid scenario for Math.Max (int implicitly convertible to double).
            PrintSampleItem("Math2", Math2.Pow(new NumberD(5555555555, 500), 5.3)); //Error. 5555555555*10^500 is outside the Math.Pow supported range. 

            //--- If the target range is met, using the expected format isn't always required.
            PrintSampleItem("Math3", Math2.Log(new NumberD(20m, 3))); //No error despite calling Math.Log with decimal when it expects double.
            PrintSampleItem("Math4", Math2.Sin(new NumberD('e'))); //No error despite calling Math.Sin with char when it expects double.


            //------ Math2 also includes other mathematical methods which I developed completely from scratch. 

            //--- The PowDecimal/SqrtDecimal algorithms only rely on the decimal type and are more precise than the native versions.
            //--- You can find more information about these algorithms in https://varocarbas.com/fractional_exponentiation/.
            PrintSampleItem("Math5", Math2.PowDecimal(new Number(0.0000000000000001m), 1.234567895m));
            PrintSampleItem("Math6", Math2.SqrtDecimal(new Number(9999999999999999999, 500)));

            //-- RoundExact/TruncateExact can deal with multiple rounding/truncating scenarios which aren't supported by the native methods.
            PrintSampleItem("Math7", Math2.RoundExact(new Number(124555897.5500008m), 4, RoundType.AlwaysToEven, RoundSeparator.BeforeDecimalSeparator));
            PrintSampleItem("Math8", Math2.TruncateExact(new Number(5.123999m), 3));

            //-- GetPolynomialFit/ApplyPolynomialFit allow to perform regression analysis (2nd degree polynomial fits created via least squares). 
            PrintSampleItem("Math9", Math2.ApplyPolynomialFit(Math2.GetPolynomialFit(new NumberD[] { 1, 2, 4 }, new NumberD[] { 1, 4, 16 }), 3));

            //-- Factorial calculates the factorial of any integer number up to 100000. 
            PrintSampleItem("Math10", Math2.Factorial(new NumberD(25)));


            //------ Other FlexibleParser parts.
            //All the FlexibleParser parts are independent among each other and only the corresponding DLL file needs to be referred.
            //On the other, codes relying on various parts can take advantage of certain compatibility among their main classes. 

            //--- UnitParser.
            PrintSampleItem("UP1", new Number(new UnitP("12.3 MabA")));
            PrintSampleItem("UP2", new NumberD(new UnitP(0.01m, SIPrefixes.Micro.ToString() + Units.Second.ToString())));
            PrintSampleItem("UP3", new NumberP(new UnitP("Error")));
           
            
            Console.WriteLine();
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();
            Console.ReadLine();
        }

        private static void PrintSampleItem(string sampleId, dynamic numberX)
        {
            Console.WriteLine
            (
                sampleId + " -- "
                + (numberX == null ? " " : numberX.GetType().ToString().Replace("FlexibleParser.", ""))
                //Each ToString() method outputs what the given NumberX needs.
                + " - " + numberX.ToString()
                + Environment.NewLine
            );
        }
    }
}