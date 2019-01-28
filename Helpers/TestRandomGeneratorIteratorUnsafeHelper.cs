using Perfomance.RandomIterators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perfomance.Helpers
{
    public class TestRandomGeneratorIteratorUnsafeHelper
    {
        private static DiagnosticHelper dh = new DiagnosticHelper();
        public static void TestRandomGeneratorIteratorUnsafe()
        {
            int randomValue = 0;
            Console.WriteLine("Test Random generator Iterator Unsafe En: ");
            dh.StartWatch();
            RandomIteratorUsafeEn randomIteratorUsafeEn = new RandomIteratorUsafeEn(16, 1, 10);
            for (long i = 20000000000; i != 0; --i)
            {
                randomValue = randomIteratorUsafeEn.Next();
            }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());

            Console.WriteLine("Test Random generator Iterator Unsafe: ");
            dh.StartWatch();
            RandomIteratorUsafe randomIteratorUnsafe = new RandomIteratorUsafe(100000, 1, 10);
            for (long i = 0; i < 20000000000; i++)
            {
                randomValue = randomIteratorUnsafe.Next();
            }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
        }
    }
}
