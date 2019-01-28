using Perfomance.RandomIterators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perfomance.Helpers
{
    public class TestRandomIteratorUsafeXorshiftHelper
    {
        private static DiagnosticHelper dh = new DiagnosticHelper();
        public static void TestRandomIteratorUsafeXorshift()
        {
            int randomValue = 0;


            Console.WriteLine("Test Random generator Iterator Unsafe Xor Shift En Pointer: ");
            dh.StartWatch();
            RandomIteratorUsafeXorshiftEn randomIteratorUsafeEn = new RandomIteratorUsafeXorshiftEn(1024 * 1024);
            ulong n = (ulong)20 * (ulong)1024 * (ulong)1024 * (ulong)1024;
            for (ulong i = n; i != 0; i--)
            {
                randomValue = randomIteratorUsafeEn.bNextPointer();
            }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());

            Console.WriteLine("Test Random generator Iterator Unsafe Xor Shift En: ");
            dh.StartWatch();
            RandomIteratorUsafeXorshift randomIteratorUnsafe = new RandomIteratorUsafeXorshift(1024 * 1024);
             n = (ulong)20 * (ulong)1024 * (ulong)1024 * (ulong)1024;
            for (ulong i = n; i != 0; i--)
            {
                randomValue = randomIteratorUnsafe.bNext();
            }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());



            Console.WriteLine("Test Random generator Iterator Unsafe Xor Shift En: ");
            randomIteratorUsafeEn = new RandomIteratorUsafeXorshiftEn(1024 * 1024);
            n = (ulong)1024 * (ulong)1024 * (ulong)1024 * (ulong)1024;

            Console.WriteLine("i++");
            dh.StartWatch();

            for (ulong i = 0; i < n; i++)
            {
            }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());

            Console.WriteLine("--i");
            dh.StartWatch();           
            
            for (ulong i = n; i !=0 ; --i)
            {
            }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());

            Console.WriteLine("i--");
            dh.StartWatch();
                        
            for (ulong i = n; i != 0; i--)
            {                
            }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());

            Console.WriteLine("++i");
            dh.StartWatch();

            
            for (ulong i = 0; i <n; ++i)
            {
            }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());




        }
    }
}
