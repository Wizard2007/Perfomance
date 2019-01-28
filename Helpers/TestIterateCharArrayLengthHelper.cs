using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Perfomance.Helpers
{

    public class TestIterateCharArrayLengthHelper
    {
        [DllImport("msvcrt.dll", EntryPoint = "memcpy", CallingConvention = CallingConvention.Cdecl, SetLastError = false), SuppressUnmanagedCodeSecurity]
        public static unsafe extern void* CopyMemory(void* dest, void* src, ulong count);

        private static DiagnosticHelper dh = new DiagnosticHelper();
        public static void TestIterateCharArrayLength()
        {
            Console.WriteLine("Test iterate char array length: ");
            char tmpChar = ' ';
            char[] templateChar = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            dh.StartWatch();
            for (long i = 0; i < 20000000000; i++)
            {
                for (int j = 0; j < templateChar.Length; j++)
                {
                    tmpChar = templateChar[j];
                }
            }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
        }
    }
}
