using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perfomance.Helpers
{
    public class TestCharArrayHelper
    {
        private static DiagnosticHelper dh = new DiagnosticHelper();
        public static void TestCharArray()
        {
            Random rnd = new Random();
            int maxLengthCh = 512 * 1024 * 1024;
            int maxLengthCh2 = (512 * 1024 * 1024) << 1;
            Console.WriteLine("Test char array :");
            char[] buffer = new char[maxLengthCh];
            char[] templateCh = new char[16] { '1', '2', '3', '4', '5', '6', '7', '8', '1', '2', '3', '4', '5', '6', '7', '8' };
            dh.StartWatch();
            int position = maxLengthCh;
            int position2 = maxLengthCh << 2;
            int templateLenghtCh2 = templateCh.Length << 1;
            for (int i = 0; i < 200; i++)
            {
                position2 = maxLengthCh2 - templateLenghtCh2;
                while (position2 >= 0)
                {
                    rnd.Next(10);

                    Buffer.BlockCopy(templateCh, 0, buffer, position2, templateLenghtCh2);
                    position2 -= templateLenghtCh2;
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
