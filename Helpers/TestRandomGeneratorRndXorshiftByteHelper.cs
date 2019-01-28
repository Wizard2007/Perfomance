using SharpNeatLib.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XorshiftDemo;

namespace Perfomance.Helpers
{
    public class TestRandomGeneratorRndXorshiftByteHelper
    {
        private static DiagnosticHelper dh = new DiagnosticHelper();
        public static void TestRandomGeneratorRndXorshiftByte()
        {
            int randomValue = 0;
            Console.WriteLine("Test RndXorshift generator bytes: ");
            dh.StartWatch();
            XorshiftUnrolled64 rndXorshift = new XorshiftUnrolled64();
            byte[] buffer = new byte[1024 * 1024];
            //GCHandle gchBuffer = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            for (int i = 0; i < 20000; i++)
            {
                rndXorshift.NextBytes(buffer);
                /*
                foreach (var value in buffer)
                {
                    randomValue = value;
                }
                */
            }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());

            Console.WriteLine("Test Fast Random generator bytes: ");
            dh.StartWatch();
            FastRandom fastRandom = new FastRandom();
            byte[] buffer1 = new byte[1024 * 1024];
            //GCHandle gchBuffer = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            for (int i = 0; i < 20000; i++)
            {
                fastRandom.NextBytes(buffer1);
                /*
                foreach (var value in buffer)
                {
                    randomValue = value;
                }
                */
            }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());

            Console.WriteLine("Test Random generator bytes: ");
            Random random = new Random();
            byte[] buffer2 = new byte[1024 * 1024];
            //GCHandle gchBuffer = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            for (int i = 0; i < 20000; i++)
            {
                random.NextBytes(buffer2);
                /*
                foreach (var value in buffer)
                {
                    randomValue = value;
                }
                */
            }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());

        }
    }
}
