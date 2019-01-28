using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Perfomance.Helpers
{
    class TestClass
    {
        public byte[] classArray;
    }
    public class TestAccesToVariablesHelper
    {
        private static DiagnosticHelper dh = new DiagnosticHelper();

        private static byte[] externalArray;

        private static TestClass testClass = new TestClass();

        public static byte[] propArray { get; set; }
        public static byte[] inlineArray {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get; set; }
        public static void TestAccesToVariables(int n = Int32.MaxValue, int iteration = 100)
        {
            
            byte tmp = 0;
            byte[] internalArray = new byte[n];

            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("Access to internal array ");
            dh.StartWatch();
            for(int j= 0;j<iteration;j++)
                for (int i = 0; i < n; i++)
                {
                    tmp = internalArray[i];
                }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            internalArray = null;
            Console.WriteLine("GB ");
            dh.StartWatch();
            GarbachCollectorHelper.GBForceRun();
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            


            
            byte[] externalArray = new byte[n];
            
            Console.WriteLine("Access to external array ");
            dh.StartWatch();
            for (int j = 0; j < iteration; j++)
                for (int i = 0; i < n; i++)
                {
                    tmp = externalArray[i];
                }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            externalArray = null;
            Console.WriteLine("GB ");
            dh.StartWatch();
            GarbachCollectorHelper.GBForceRun();
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            
            

            
            Console.WriteLine("Access to class array ");
            testClass.classArray = new byte[n];
            dh.StartWatch();
            for (int j = 0; j < iteration; j++)
                for (int i = 0; i < n; i++)
                {
                    tmp = testClass.classArray[i];
                }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            testClass.classArray = null;
            Console.WriteLine("GB ");
            dh.StartWatch();
            GarbachCollectorHelper.GBForceRun();
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            

            
            Console.WriteLine("Access to prop array ");
            propArray = new byte[n];
            dh.StartWatch();
            for (int j = 0; j < iteration; j++)
                for (int i = 0; i < n; i++)
                {
                    tmp = propArray[i];
                }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            testClass.classArray = null;
            Console.WriteLine("GB ");
            dh.StartWatch();
            GarbachCollectorHelper.GBForceRun();
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            

            
            Console.WriteLine("Access to inline array ");
            inlineArray = new byte[n];
            dh.StartWatch();
            for (int j = 0; j < iteration; j++)
                for (int i = 0; i < n; i++)
                {
                    tmp = inlineArray[i];
                }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            testClass.classArray = null;
            Console.WriteLine("GB ");
            dh.StartWatch();
            GarbachCollectorHelper.GBForceRun();
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            
        }
    }
}
