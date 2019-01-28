using Perfomance.RandomIterators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perfomance.Helpers
{
    public class TestArrayCopyBlockCopyHelper
    {
        private static DiagnosticHelper dh = new DiagnosticHelper();
        public static void TestArrayCopyBlockCopy(int iterationCount = 1)
        {
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("Copy array using block copy ++:");
            Console.WriteLine("Generating data ... ");
            dh.StartWatch();
            int n = 1024 * 1024;
            RandomIteratorUsafeXorshiftEn rnd = new RandomIteratorUsafeXorshiftEn(1024 * 1024);
            rnd.Reset();
            byte[] destArray = new byte[n];
            byte[] sourceArra = new byte[256];
            Buffer.BlockCopy(rnd.values, 0, sourceArra, 0, 256);

            byte index = rnd.bNext();
            int position = 0;

            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());


            Console.WriteLine("Executing + ... ");

            dh.StartWatch();
            for (int k = iterationCount * 1024; k != 0; --k)
            {
                position = 0;
                //index = rnd.bNext();
                index = 1;
                while (position + index < n)                   
                {
                    Buffer.BlockCopy(sourceArra, 0, destArray, position, index);
                    index = rnd.bNext();
                    position += index;                    
                }
                
            }

            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
                        
            destArray = null;
            GarbachCollectorHelper.GBForceRun();
        }

        public static void TestArrayCopyBlockCopy1(int iterationCount = 1)
        {
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("Copy array using block copy -- :");
            Console.WriteLine("Generating data ... ");
            dh.StartWatch();
            int n = 1024 * 1024;
            RandomIteratorUsafeXorshiftEn rnd = new RandomIteratorUsafeXorshiftEn(1024 * 1024);
            rnd.Reset();
            byte[] destArray = new byte[n];
            byte[] sourceArra = new byte[256];
            Buffer.BlockCopy(rnd.values, 0, sourceArra, 0, 256);

            byte index = rnd.bNext();
            int position = 0;

            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());

            Console.WriteLine("Executing - > ... ");

            dh.StartWatch();

            for (int k = iterationCount * 1024; k > 0; --k)
            {
                position = n;
                //index = rnd.bNext();
                index = 1;
                position -= index;
                while (position > 0)
                {
                    Buffer.BlockCopy(sourceArra, 0, destArray, position, index);
                    index = rnd.bNext();
                    position -= index;
                }
            }

            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());



            destArray = null;
            GarbachCollectorHelper.GBForceRun();
        }
    }
}
