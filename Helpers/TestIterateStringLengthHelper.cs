using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perfomance.Helpers
{
    public class TestIterateStringLengthHelper
    {
        private static DiagnosticHelper dh = new DiagnosticHelper();
        public static void TestIterateStringLength()
        {
            Console.WriteLine("Test iterate string length: ");
            char tmp = ' ';
            string template = "1234567890";
            dh.StartWatch();
            for (long i = 0; i < 20000000000; i++)
            {
                for (int j = 0; j < template.Length; j++)
                {
                    tmp = template[j];
                }
            }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
        }
    }
}
