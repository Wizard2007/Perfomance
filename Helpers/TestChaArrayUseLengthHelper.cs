using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perfomance.Helpers
{
    public class TestChaArrayUseLengthHelper
    {
        private static DiagnosticHelper dh = new DiagnosticHelper();
        public static void TestChaArrayUseLength()
        {
            Random rnd = new Random();
            int maxLengthCh = 512 * 1024 * 1024;
            Console.WriteLine("Test char array (length):");
            char[] buffer = new char[maxLengthCh];
            char[] templateCh = new char[16] { '1', '2', '3', '4', '5', '6', '7', '8', '1', '2', '3', '4', '5', '6', '7', '8' };
            dh.StartWatch();
            int position = maxLengthCh;
            for (int i = 0; i < 200; i++)
            {
                position = maxLengthCh - templateCh.Length;
                while (position >= 0)
                {
                    rnd.Next(10);
                    Buffer.BlockCopy(templateCh, 0, buffer, position << 1, templateCh.Length << 1);
                    position -= templateCh.Length;
                }
            }
            dh.StoptWatch();
            templateCh = null;
            buffer = null;

            GarbachCollectorHelper.GBForceRun();
            Console.WriteLine(dh.GetMessage());
        }
    }
}
