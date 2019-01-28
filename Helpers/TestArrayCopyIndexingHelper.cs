using Perfomance.RandomIterators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perfomance.Helpers
{
    public class TestArrayCopyIndexingHelper
    {
        private static DiagnosticHelper dh = new DiagnosticHelper();
        public static void TestArrayCopyIndexing(int iterationCount  = 1)
        {
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("Copy array using for(i++) :");
            Console.WriteLine("Generating data ... ");
            dh.StartWatch();
            int n = 1024 * 1024 * 1024;
            RandomIteratorUsafeXorshiftEn rnd = new RandomIteratorUsafeXorshiftEn(1024 * 1024);
            rnd.Reset();
            byte[] destArray = new byte[1024 * 1024 * 1024];
            byte[] sourceArra = new byte[256];
            Buffer.BlockCopy(rnd.values, 0, sourceArra, 0, 256);

            byte index = rnd.bNext();
            int position = 0;

            Console.WriteLine("Executing ++i ... ");

            dh.StartWatch();

            for (int k = 0; k < iterationCount; ++k)
            {
                position = 0;
                while (position + index < n)
                {
                    for (byte j = 0; j < index; ++j)
                    {
                        destArray[position] = sourceArra[j];
                        ++position;
                    }
                    index = rnd.bNext();
                }
            }

            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());

            destArray = null;
            GarbachCollectorHelper.GBForceRun();
        }

        public static void TestArrayCopyIndexing1(int iterationCount = 1)
        {
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("Copy array using for(i++) :");
            Console.WriteLine("Generating data ... ");
            dh.StartWatch();
            int n = 1024 * 1024 * 1024;
            RandomIteratorUsafeXorshiftEn rnd = new RandomIteratorUsafeXorshiftEn(1024 * 1024);
            rnd.Reset();
            byte[] destArray = new byte[1024 * 1024 * 1024];
            byte[] sourceArra = new byte[256];
            Buffer.BlockCopy(rnd.values, 0, sourceArra, 0, 256);

            byte index = rnd.bNext();
            int position = 0;

            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());

            Console.WriteLine("Executing --i > ... ");


            dh.StartWatch();

            for (int k = iterationCount; k > 0; --k)
            {
                position = n;
                while (position - index >= 0)
                {
                    for (byte j = index; j > 0; --j)
                    {
                        --position;
                        destArray[position] = sourceArra[j];
                    }
                    index = rnd.bNext();
                }
            }

            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());

            destArray = null;
            GarbachCollectorHelper.GBForceRun();
        }

        public static void TestArrayCopyIndexing2(int iterationCount = 1)
        {
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("Copy array using for(--i != 0 ) :");
            Console.WriteLine("Generating data ... ");
            dh.StartWatch();
            int n = 1024 * 1024 * 1024;
            RandomIteratorUsafeXorshiftEn rnd = new RandomIteratorUsafeXorshiftEn(1024 * 1024);
            rnd.Reset();
            byte[] destArray = new byte[1024 * 1024 * 1024];
            byte[] sourceArra = new byte[256];
            Buffer.BlockCopy(rnd.values, 0, sourceArra, 0, 256);

            byte index = rnd.bNext();
            int position = 0;

            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());

            Console.WriteLine("Executing --i != ... ");


            dh.StartWatch();

            for (int k = iterationCount; k != 0; --k)
            {
                position = n;
                while (position - index >= 0)
                {
                    for (byte j = index; j != 0; --j)
                    {
                        --position;
                        destArray[position] = sourceArra[j];
                    }
                    index = rnd.bNext();
                }
            }

            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());           

            destArray = null;
            GarbachCollectorHelper.GBForceRun();
        }
    }
}
