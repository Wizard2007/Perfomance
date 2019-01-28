using System;

namespace Perfomance.Helpers
{
    public class TestGetLengthHelper
    {
        private static DiagnosticHelper dh = new DiagnosticHelper();
        public static void TestGetLength()
        {
            Console.WriteLine("Test get length: ");
            int length = 0;
            dh.StartWatch();
            for (long i = 0; i < 20000000000; i++)
            {
                length = "fffff".Length;
            }
            dh.StoptWatch();

            Console.WriteLine(dh.GetMessage());
        }
    }
}
