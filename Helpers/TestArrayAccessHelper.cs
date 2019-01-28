using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perfomance.Helpers
{
    public class TestArrayAccessHelper
    {
        private static DiagnosticHelper dh = new DiagnosticHelper();
        unsafe public static void IterateArray(int n = 1024*1024, int iteration = 100*1024)
        {
            Console.WriteLine("----------------------------------------------------------");
            
            int value = 0;
            byte[] array = null;
            int n1 = n;
            n1--;
            Console.WriteLine("Generate array");
            dh.StartWatch();
            array = new byte[n];
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            Console.WriteLine("Iterate array foreach ");
           
            dh.StartWatch();
            for (int j = iteration; j != 0; j--)
            {
                foreach (int i in array)
                {
                    value = i;
                }
            }
            dh.StoptWatch();
            array = null;
            GarbachCollectorHelper.GBForceRun();
            Console.WriteLine(dh.GetMessage());

            Console.WriteLine("Generate array");
            dh.StartWatch();
            array = new byte[n];
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            Console.WriteLine("Iterate array for (i++)");

            dh.StartWatch();
            for (int j = iteration; j != 0; j--)
            {
                for (int i = 0; i < n ; i++)
                {
                    value = array[i];
                }
            }
            dh.StoptWatch();
            array = null;
            GarbachCollectorHelper.GBForceRun();
            Console.WriteLine(dh.GetMessage());

            Console.WriteLine("Generate array");
            dh.StartWatch();
            array = new byte[n];
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            Console.WriteLine("Iterate array for (++i)");

            dh.StartWatch();
            for (int j = iteration; j != 0; j--)
            {
                for (int i = 0; i < n; ++i)
                {
                    value = array[i];
                }
            }
            dh.StoptWatch();
            array = null;
            GarbachCollectorHelper.GBForceRun();
            Console.WriteLine(dh.GetMessage());


            Console.WriteLine("Generate array");
            dh.StartWatch();
            array = new byte[n];
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            Console.WriteLine("Iterate array for (i--)");
            
            dh.StartWatch();
            for (int j = iteration; j != 0; j--)
            {
                for (int i = n - 1; i >= 0; i--)
                {
                    value = array[i];
                }
            }
            dh.StoptWatch();
            array = null;
            GarbachCollectorHelper.GBForceRun();
            Console.WriteLine(dh.GetMessage());

            Console.WriteLine("Generate array");
            dh.StartWatch();
            array = new byte[n];
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            Console.WriteLine("Iterate array for (--i)");

            dh.StartWatch();
            for (int j = iteration; j != 0; j--)
            {
                for (int i = n - 1; i >= 0; --i)
                {
                    value = array[i];
                }
            }
            dh.StoptWatch();
            array = null;
            GarbachCollectorHelper.GBForceRun();
            Console.WriteLine(dh.GetMessage());

            Console.WriteLine("Generate array");
            dh.StartWatch();
            array = new byte[n];
            array[n - 1] = 1;
            array[n - 2] = 2;
            array[0] = 255;
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());

            Console.WriteLine("Iterate array by pointer < --i");
            dh.StartWatch();
            fixed (byte* start = &array[0])
            {
                for (int j = iteration; j != 0; j--)
                {

                    byte* end = start + n ;
                   
                    do
                    {                        
                        value = *(--end);                       
                    }
                    while (start < end);
                }
            }
            dh.StoptWatch();
            array = null;

            GarbachCollectorHelper.GBForceRun();
            Console.WriteLine(dh.GetMessage());

            Console.WriteLine("Generate array");
            dh.StartWatch();
            array = new byte[n];
            array[n - 1] = 1;
            array[n - 2] = 2;
            array[0] = 255;
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());

            Console.WriteLine("Iterate array by pointer <= --i");
            dh.StartWatch();
            fixed (byte* start = &array[0])
            {
                for (int j = iteration; j != 0; j--)
                {

                    byte* end = start + n;
                    --end;
                    do
                    {
                        
                        value = *(end);
                        --end;
                    }
                    while (start <= end);
                }
            }
            dh.StoptWatch();
            array = null;

            GarbachCollectorHelper.GBForceRun();
            Console.WriteLine(dh.GetMessage());

            Console.WriteLine("Generate array");
            dh.StartWatch();
            array = new byte[n];
            array[n - 1] = 1;
            array[n - 2] = 2;
            array[0] = 255;
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());

            Console.WriteLine("Iterate array by pointer < i--");
            dh.StartWatch();
            fixed (byte* start = &array[0])
            {
                for (int j = iteration; j != 0; j--)
                {

                    byte* end = start + n;
                   
                    do
                    {
                        end--;
                        value = *(end);
                    }
                    while (start < end);
                }
            }
            dh.StoptWatch();
            array = null;

            GarbachCollectorHelper.GBForceRun();
            Console.WriteLine(dh.GetMessage());

            Console.WriteLine("Generate array");
            dh.StartWatch();
            array = new byte[n];
            array[n - 1] = 1;
            array[n - 2] = 2;
            array[0] = 255;
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());

            Console.WriteLine("Iterate array by pointer <= i--");
            dh.StartWatch();
            fixed (byte* start = &array[0])
            {
                for (int j = iteration; j != 0; j--)
                {

                    byte* end = start + n;
                    end--;
                    do
                    {

                        value = *(end);
                        end--;
                    }
                    while (start <= end);
                }
            }
            dh.StoptWatch();
            array = null;

            GarbachCollectorHelper.GBForceRun();
            Console.WriteLine(dh.GetMessage());
        }
    }
}
