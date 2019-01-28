using Perfomance.RandomIterators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perfomance.Helpers
{
    class TestArrayCopyFixedHelper
    {
        private static DiagnosticHelper dh = new DiagnosticHelper();
        unsafe public static void TestArrayCopyFixed(int iterationCount = 1)
        {
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("Copy array using pointer copy ++:");
            Console.WriteLine("Generating data ... ");
            dh.StartWatch();
            int n = 1024 * 1024 * 1024;
            RandomIteratorUsafeXorshiftEn rnd = new RandomIteratorUsafeXorshiftEn(1024 * 1024);
            rnd.Reset();
            byte[] destArray = new byte[1024 * 1024 * 1024];
            byte[] sourceArray = new byte[256];
            Buffer.BlockCopy(rnd.values, 0, sourceArray, 0, 256);

            byte index = rnd.bNext();
            byte* endDest = null;
            byte* endSrc = null;
            byte* currentSrc = null;
            byte* currentDest = null;


            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());


            Console.WriteLine("Executing + ... ");


            dh.StartWatch();
            for (int k = iterationCount; k != 0; --k)
            {
                endDest = null;
                currentSrc = null;
                currentDest = null;
                index = rnd.bNext();
                fixed (byte* startDest = &destArray[0], startSource = &sourceArray[0])
                {
                    endDest = startDest + n - 1;
                    endSrc = startSource + index;

                    currentDest = startDest;
                    do
                    {
                        currentSrc = startSource;
                        endSrc = startSource + index;
                        while (currentSrc <= endSrc)
                        {
                            *currentDest = *currentSrc;
                            ++currentDest;
                            ++currentSrc;
                        }

                        index = rnd.bNext();
                    }
                    while ((currentDest + index) <= endDest);
                }

            }

            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());

            destArray = null;
            GarbachCollectorHelper.GBForceRun();
        }

        unsafe public static void TestArrayCopyFixed1(int iterationCount = 1)
        {
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("Copy array using pointer copy ++:");
            Console.WriteLine("Generating data ... ");
            dh.StartWatch();
            int n = 1024 * 1024 * 1024;
            RandomIteratorUsafeXorshiftEn rnd = new RandomIteratorUsafeXorshiftEn(1024 * 1024);
            rnd.Reset();
            byte[] destArray = new byte[1024 * 1024 * 1024];
            byte[] sourceArray = new byte[256];
            Buffer.BlockCopy(rnd.values, 0, sourceArray, 0, 256);

            byte index = rnd.bNext();
            byte* endDest = null;
            byte* endSrc = null;
            byte* currentSrc = null;
            byte* currentDest = null;


            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());


            Console.WriteLine("Executing + ... ");


            dh.StartWatch();
            for (int k = iterationCount; k != 0; --k)
            {
                endDest = null;
                currentSrc = null;
                currentDest = null;
                index = rnd.bNext();
                fixed (byte* startDest = &destArray[0], startSource = &sourceArray[0])
                {
                    endDest = startDest + n - 1;
                    endSrc = startSource + index;

                    currentDest = startDest;
                    do
                    {
                        currentSrc = startSource;
                        endSrc = startSource + index;
                        while (currentSrc <= endSrc)
                        {
                            *currentDest = *currentSrc;
                            ++currentDest;
                            ++currentSrc;
                        }

                        index = rnd.bNext();
                    }
                    while ((currentDest + index) <= endDest);
                }

            }

            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());

            destArray = null;
            GarbachCollectorHelper.GBForceRun();
        }
    }
}
