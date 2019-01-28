using Perfomance.RandomIterators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perfomance.Helpers
{
    public static class TestRandomBytesProducerHelper
    {
        private static DiagnosticHelper dh = new DiagnosticHelper();

        public static void TestRandomBytesProducer()
        {
            RandomBytesProducer.InitBytes();
            dh.StartWatch();
            //ulong n = (ulong)20 * (ulong)1024 * (ulong)1024 * (ulong)1024;
            ulong n = (ulong)1024 * (ulong)1024 * (ulong)1024;
            byte[] tmp = null;
            RandomIteratorUsafeXorshiftEn randomIteratorUsafeXorshiftEn = new RandomIteratorUsafeXorshiftEn(1024 * 1024);
            for (ulong i = 0; i < n; i++)
            {
                tmp = RandomBytesProducer.GetByte(randomIteratorUsafeXorshiftEn.Next16());                
            }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
        }
    }
}
