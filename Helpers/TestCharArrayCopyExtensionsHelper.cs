using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perfomance.Helpers
{
    public class TestCharArrayCopyExtensionsHelper
    {
        private static DiagnosticHelper dh = new DiagnosticHelper();
        public static void TestCharArrayCopyExtensions()
        {
            Random rnd = new Random();
            int maxLengthCh = 512 * 1024 * 1024;
            int maxLengthCh2 = maxLengthCh << 1;
            Console.WriteLine("Test char array CopyExtensions:");
            char[] buffer = new char[maxLengthCh];

            char[] templateCh = new char[16] { '1', '2', '3', '4', '5', '6', '7', '8', '1', '2', '3', '4', '5', '6', '7', '8' };
            dh.StartWatch();

            {
                int position = maxLengthCh;
                int position2 = maxLengthCh << 2;
                int templateLenghtCh = templateCh.Length;
                for (int i = 0; i < 200; i++)
                {
                    position = maxLengthCh - templateLenghtCh;
                    while (position >= 0)
                    {
                        rnd.Next(10);
                        CopyExtensions.CopyMemoryCh(templateCh, 0, buffer, position, templateLenghtCh);
                        //Buffer.BlockCopy(templateCh, 0, buffer, position2, templateLenghtCh);
                        position -= templateLenghtCh;
                    }
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
