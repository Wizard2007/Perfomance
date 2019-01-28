using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perfomance.Helpers
{
    public class Test2Helper
    {
        private static DiagnosticHelper dh = new DiagnosticHelper();
        public static void Test2(int n = Int32.MaxValue)
        {
            Console.WriteLine("--------------------------------------------- ");
            Int64 result = 0;
            Console.WriteLine(n);
            Console.WriteLine("Multiplay 2 ");
            dh.StartWatch();
            for(int i = 0; i <= n; i++)
            {
                result = i * 2;
            }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());

            Console.WriteLine("Subtract 2 ");
            dh.StartWatch();
            for (int i = 0; i <= n; i++)
            {
                result = i / 2;
            }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());

            Console.WriteLine("<< 2 ");
            dh.StartWatch();
            for (int i = 0; i <= n; i++)
            {
                result = i << 1;
            }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());

            Console.WriteLine(">> 2 ");
            dh.StartWatch();
            for (int i = 0; i <= n; i++)
            {
                result = i >> 1;
            }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
        }
    }
}
