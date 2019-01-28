using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using XorshiftDemo;

namespace Perfomance.Helpers
{
    public class TestRandomGeneratorRndXorshiftHelper
    {
        private static DiagnosticHelper dh = new DiagnosticHelper();
        public static void TestRandomGeneratorRndXorshift()
        {
            int randomValue = 0;
            Console.WriteLine("Test Random generator RndXorshift bool: ");

            XorshiftUnrolled64 rndXorshift = new XorshiftUnrolled64();
            byte[] buffer = new byte[1024 * 1024];
            char[] bufferChar = new char[1024 * 1024];
            GCHandle gchBuffer = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            GCHandle gchBufferChar = GCHandle.Alloc(bufferChar, GCHandleType.Pinned);
            IntPtr pAddr = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
            IntPtr pAddrChar = Marshal.UnsafeAddrOfPinnedArrayElement(bufferChar, 0);
            dh.StartWatch();
            unsafe
            {
                //fixed (byte* pStart = buffer)
                {
                    byte* pStart = (byte*)pAddr.ToPointer();
                    byte* pEnd = pStart + buffer.Length;
                    ulong* plStart = (ulong*)pStart;
                    ulong* plEnd = (ulong*)pEnd;
                    byte* pCurrent = pStart;
                    for (int i = 0; i < 20000; i++)
                    {
                        pCurrent = pStart;
                        //rndXorshift.NextBytes(buffer);
                        rndXorshift.FillBufferEx(plStart, plEnd);
                        /*
                        while (pCurrent <= pEnd)
                        {
                            //randomValue = *pCurrent & 100000;
                            randomValue = *pCurrent;
                            pCurrent++;
                        }
                        */
                    }
                }
            }
            dh.StoptWatch();
            if (gchBuffer.IsAllocated)
            {
                gchBuffer.Free();
            }

            Console.WriteLine(dh.GetMessage());
        }
    }
}
