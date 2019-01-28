using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perfomance.Helpers
{
    public class TestRandomGeneratorIteratorHelper
    {
        private static DiagnosticHelper dh = new DiagnosticHelper();
        public static void TestRandomGeneratorIterator()
        {
            int randomValue = 0;
            Console.WriteLine("Test Random generator Iterator: ");
            dh.StartWatch();
            RandomIterator randomIterator = new RandomIterator(10000, 1, 10);
            for (long i = 0; i < 20000000000; i++)
            {
                randomValue = randomIterator.Next();
            }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
        }
    }
}
