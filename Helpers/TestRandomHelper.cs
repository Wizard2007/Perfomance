using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perfomance.Helpers
{
    public class TestRandomHelper
    {
        private static DiagnosticHelper dh = new DiagnosticHelper();
        public static void TestRandom()
        {            
            Random rnd = new Random();
            Console.WriteLine("Test Random generator : ");
            dh.StartWatch();
            for (long i = 0; i < 20000000000; i++)
            {
                rnd.Next(1, 10);
            }
            dh.StoptWatch();
            GarbachCollectorHelper.GBForceRun();
            Console.WriteLine(dh.GetMessage());
        }
    }
}
