using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perfomance.Helpers
{
    public class TestArrayWithDictionaryHelper
    {
        private static DiagnosticHelper dh = new DiagnosticHelper();
        public static void TestArrayWithDictionary()
        {
            UInt64 counter = 0;
            int maxLengthCh = 512 * 1024 * 1024;
            int maxLengthCh2 = maxLengthCh << 1;
            Console.WriteLine("Test char array with dictionary :");
            char[] buffer = new char[maxLengthCh];
            char[][] templateCh = { " apple".ToCharArray(), " town".ToCharArray(), " array".ToCharArray(), " summer".ToCharArray(), " winter".ToCharArray(), " aplril".ToCharArray(), " customer".ToCharArray(), " support".ToCharArray(), " laptop".ToCharArray(), " cross-domain".ToCharArray() };
            IntPtr[] test = new IntPtr[9];

            int n = templateCh.Length;
            int[] sizes = new int[n];
            int[] sizes2 = new int[n];
            int sizeTmp = 0;
            for (int i = 0; i < n; i++)
            {
                sizeTmp = templateCh[i].Length;
                sizes[i] = sizeTmp;
                sizes2[i] = sizeTmp << 1;
            }
            dh.StartWatch();
            int position = maxLengthCh;
            int position2 = maxLengthCh << 2;
            int templateLenghtCh2 = templateCh.Length << 1;
            int randomIndex = 0;
            Random rnd1 = new Random();
            n--;
            for (int i = 0; i < 200; i++)
            {
                randomIndex = rnd1.Next(n);
                templateLenghtCh2 = sizes2[randomIndex];
                position2 = maxLengthCh2 - templateLenghtCh2;
                while (position2 >= 0)
                {
                    Buffer.BlockCopy(templateCh[randomIndex], 0, buffer, position2, templateLenghtCh2);
                    randomIndex = n;//rnd1.Next(n);
                    templateLenghtCh2 = sizes2[randomIndex];
                    position2 -= templateLenghtCh2;
                    //counter++;
                }
            }
            dh.StoptWatch();
            templateCh = null;
            buffer = null;
            templateCh = null;
            sizes = null;
            sizes2 = null;

            GarbachCollectorHelper.GBForceRun();
            Console.WriteLine(string.Format("Iteration count {0}", counter));
            Console.WriteLine(dh.GetMessage());
        }
    }
}
