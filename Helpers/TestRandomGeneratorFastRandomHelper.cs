using SharpNeatLib.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perfomance.Helpers
{
    public class TestRandomGeneratorFastRandomHelper
    {
        private static DiagnosticHelper dh = new DiagnosticHelper();
        public static void TestRandomGeneratorFastRandom()
        {
            byte[] randomValue = null;
            Console.WriteLine("Test Random generator Fast Random: ");
            dh.StartWatch();
            FastRandom fastRandom = new FastRandom();
            ulong n = (ulong)1024 * (ulong)1024 * (ulong)1024;
            for (ulong i = 0; i < n; i++)
            {
                randomValue = Encoding.ASCII.GetBytes(fastRandom.NextInt().ToString());
            }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
        }
    }
}
