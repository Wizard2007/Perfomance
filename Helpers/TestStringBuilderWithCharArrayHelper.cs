using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perfomance.Helpers
{
    public class TestStringBuilderWithCharArrayHelper
    {
        private static DiagnosticHelper dh = new DiagnosticHelper();
        public static void TestStringBuilderWithCharArray()
        {
            Random rnd = new Random();
            int maxLength = 256 * 1024 * 1024;
            StringBuilder sb = new StringBuilder(maxLength, maxLength);
            Console.WriteLine("Test stringBuilder char array :");
            char[] templateCh = new char[16] { '1', '2', '3', '4', '5', '6', '7', '8', '1', '2', '3', '4', '5', '6', '7', '8' };
            string templateTmp = "";
            dh.StartWatch();
            for (int i = 0; i < 400; i++)
            {
                while (sb.Length < maxLength)
                {
                    rnd.Next(10);
                    sb.Append(templateCh);
                }
                templateTmp = sb.ToString();
                templateTmp = "";
                sb.Clear();
            }
            sb = null;
            dh.StoptWatch();

            GarbachCollectorHelper.GBForceRun();
            Console.WriteLine(dh.GetMessage());
        }
    }
}
