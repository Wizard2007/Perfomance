using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perfomance.Helpers
{
    public class TestListVsArrayHelpercs
    {
        private static DiagnosticHelper dh = new DiagnosticHelper();
        public static void TestListVsArray(int n = Int32.MaxValue, int iteration = 100)
        {
            
            byte tmp = 0;
            byte[] arrayTest = new byte[n];

            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("Fill array : ");
            dh.StartWatch();
            for (int j = 0; j < iteration; j++)
                for (int i = 0; i < n; i++)
                {
                    arrayTest[i] = tmp;
                }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            arrayTest = null;
            Console.WriteLine("GB ");
            dh.StartWatch();
            GarbachCollectorHelper.GBForceRun();
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            


           
            List<byte> list = new List<byte>();

            Console.WriteLine("Fill list add ");
            dh.StartWatch();
            for (int j = 0; j < iteration; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    list.Add(tmp);
                }
                list.Clear();
            }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            list = null;
            Console.WriteLine("GB ");
            dh.StartWatch();
            GarbachCollectorHelper.GBForceRun();
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            


           
            Console.WriteLine("Fill list capasity ");
            List<byte> listCapasity = new List<byte>(n);
            dh.StartWatch();
            for (int j = 0; j < iteration; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    listCapasity.Add(tmp);
                }
                listCapasity.Clear();
            }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            listCapasity = null;
            Console.WriteLine("GB ");
            dh.StartWatch();
            GarbachCollectorHelper.GBForceRun();
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());



            Console.WriteLine("Fill list capasity [] ");
            List<byte> listCapasityIndex = new List<byte>(n);
            dh.StartWatch();
            for (int j = 0; j < iteration; j++)
                for (int i = 0; i < n; i++)
                {
                    listCapasityIndex[i]=(tmp);
                }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            listCapasityIndex = null;
            Console.WriteLine("GB ");
            dh.StartWatch();
            GarbachCollectorHelper.GBForceRun();
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());

        }
    }
}
