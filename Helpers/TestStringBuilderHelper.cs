using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perfomance.Helpers
{
    public class TestStringBuilderHelper
    {
        private static DiagnosticHelper dh = new DiagnosticHelper();
        public static void TestStringBuilder()
        {
            int maxLength = 256 * 1024 * 1024;
            StringBuilder sb = new StringBuilder(maxLength, maxLength);
            Console.WriteLine("Test StringBuilder :");
            string templateSB = "1234567812345678";
            string templateTmp = "";
            dh.StartWatch();
            for (int i = 0; i < 400; i++)
            {
                while (sb.Length < maxLength)
                {
                    sb.Append(templateSB);
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
