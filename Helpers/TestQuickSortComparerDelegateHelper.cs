using Perfomance.RandomIterators;
using SharpNeatLib.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perfomance.Helpers
{
    public class TestQuickSortComparerDelegateHelper
    {
        public delegate int CustomCompareDelegate(int x, int y);

        private static DiagnosticHelper dh = new DiagnosticHelper();

        public static void TestSort(int n)
        {
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("Test Comparer Sort");
            int[] array = new int[n];
            Console.WriteLine("Generate data");
            
            RandomIteratorUsafeXorshiftEn rnd1 = new RandomIteratorUsafeXorshiftEn(n + 1);
            FastRandom rnd = new FastRandom();
            Random random = new Random();
            
            dh.StartWatch();
            for (int i = 0; i < n; i++)
            {
                array[i] = rnd.Next();
            }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            /*
            byte[] byteArray = new byte[256];
            dh.StartWatch();
            for (int i = 0; i < n; i++)
            {
                array[i] = byteArray[rnd1.bNext()];
            }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());

            dh.StartWatch();
            for (int i = 0; i < n; i++)
            {
                array[i] = rnd1.Next16();
            }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            */
            Console.WriteLine("Sort");
            dh.StartWatch();            
            Array.Sort(array, new CustomIntComparerD());
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            array = null;
            GarbachCollectorHelper.GBForceRun();
        }
        public static void TestQuickSort(int n)
        {
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("Test Comparer  Quick Sort");
            int[] array = new int[n];
            Console.WriteLine("Generate data");
            RandomIteratorUsafeXorshiftEn rnd1 = new RandomIteratorUsafeXorshiftEn(n + 1);
            FastRandom rnd = new FastRandom();
            dh.StartWatch();
            for (int i = 0; i < n; i++)
            {
                array[i] = rnd.Next();
            }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            Console.WriteLine("Sort");
            dh.StartWatch();
            var c = new CustomIntComparerD();
            Quicksort(array, 0, n - 1, c.Compare);
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            array = null;
            GarbachCollectorHelper.GBForceRun();
        }

        public static void TestQuickSortTaskLimited(int n, int limit)
        {
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("Test Comparer  Quick Sort Limited ");
            int[] array = new int[n];
            Console.WriteLine("Generate data");
            RandomIteratorUsafeXorshiftEn rnd1 = new RandomIteratorUsafeXorshiftEn(n + 1);
            FastRandom rnd = new FastRandom();
            dh.StartWatch();
            for (int i = 0; i < n; i++)
            {
                array[i] = rnd.Next();
            }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            Console.WriteLine("Sort");
            dh.StartWatch();
            QuicksortTaskLimited(array, 0, n - 1, limit, (new CustomIntComparerD()).Compare);
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            array = null;
            GarbachCollectorHelper.GBForceRun();
        }
        public static void TestQuickSortTask(int n)
        {
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("Test Comparer  Quick Sort Task");
            int[] array = new int[n];
            Console.WriteLine("Generate data");
            RandomIteratorUsafeXorshiftEn rnd1 = new RandomIteratorUsafeXorshiftEn(n + 1);
            FastRandom rnd = new FastRandom();
            dh.StartWatch();
            for (int i = 0; i < n; i++)
            {
                array[i] = rnd.Next();
            }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            Console.WriteLine("Sort");
            dh.StartWatch();
            QuicksortTask1(array, 0, n - 1, (new CustomIntComparerD()).Compare);

            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            array = null;
            GarbachCollectorHelper.GBForceRun();
        }
        public static void Quicksort(int[] elements, int left, int right, CustomCompareDelegate _compare)
        {
            int i = left, j = right;
            int pivot = elements[(left + right) / 2];
            int tmp = 0;
            while (i <= j)
            {
                while (_compare( elements[i], pivot) < 0)
                {
                    i++;
                }

                while (_compare(elements[j], pivot) > 0)
                {
                    j--;
                }

                if (i <= j)
                {
                    // Swap
                    tmp = elements[i];
                    elements[i] = elements[j];
                    elements[j] = tmp;

                    i++;
                    j--;
                }
            }

            if (left < j)
            {
                Quicksort(elements, left, j, _compare);
            }

            if (i < right)
            {
                Quicksort(elements, i, right, _compare);
            }
        }

        public static void QuicksortTaskLimited(int[] elements, int left, int right, int limit, CustomCompareDelegate _compare)
        {
            int i = left, j = right;
            int pivot = elements[(left + right) / 2];
            int tmp = 0;
            while (i <= j)
            {
                while (_compare(elements[i], pivot) < 0)
                {
                    i++;
                }

                while (_compare(elements[j], pivot) > 0)
                {
                    j--;
                }

                if (i <= j)
                {
                    // Swap
                    tmp = elements[i];
                    elements[i] = elements[j];
                    elements[j] = tmp;

                    i++;
                    j--;
                }
            }

            // Recursive calls
            List<Task> tasks = new List<Task>();

            if (left < j)
            {

                if (j - left < limit)
                {
                    Quicksort(elements, left, j, _compare);
                }
                else
                {
                    //Console.WriteLine(j - left);
                    tasks.Add(
                         Task.Factory.StartNew(() => { QuicksortTaskLimited(elements, left, j, limit,_compare); })
                         );
                }
            }

            if (i < right)
            {

                if (right - i < limit)
                {
                    Quicksort(elements, i, right,_compare);
                }
                else
                {
                    //Console.WriteLine(right - i);
                    tasks.Add(
                        Task.Factory.StartNew(() => { QuicksortTaskLimited(elements, i, right, limit, _compare); })
                        );
                }
            }
            Task.WaitAll(tasks.ToArray());
        }

        public static void QuicksortTask(int[] elements, int left, int right, IComparer<int> _compare)
        {
            int i = left, j = right;
            int pivot = elements[(left + right) / 2];
            int tmp = 0;
            while (i <= j)
            {
                while (_compare.Compare(elements[i], pivot) < 0)
                {
                    i++;
                }

                while (_compare.Compare(elements[j], pivot) > 0)
                {
                    j--;
                }

                if (i <= j)
                {
                    // Swap
                    tmp = elements[i];
                    elements[i] = elements[j];
                    elements[j] = tmp;

                    i++;
                    j--;
                }
            }

            // Recursive calls
            List<Task> tasks = new List<Task>();

            if (left < j)
            {
                tasks.Add(
                    Task.Factory.StartNew(() => { QuicksortTask(elements, left, j, _compare); })
                    );
            }

            if (i < right)
            {
                tasks.Add(
                    Task.Factory.StartNew(() => { QuicksortTask(elements, i, right, _compare); })
                    );
            }
            Task.WaitAll(tasks.ToArray());
        }

        public static void QuicksortTask1(int[] elements, int left, int right, CustomCompareDelegate _compare)
        {
            int i = left, j = right;
            int pivot = elements[(left + right) / 2];
            //int tmp = 0;
            while (i <= j)
            {
                while (_compare(elements[i], pivot) < 0)
                {
                    i++;
                }

                while (_compare(elements[j], pivot) > 0)
                {
                    j--;
                }

                if (i <= j)
                {
                    // Swap
                    int tmp = elements[i];
                    elements[i] = elements[j];
                    elements[j] = tmp;

                    i++;
                    j--;
                }
            }
            List<Task> tasks = new List<Task>(2);
            if (left < j)
            {
                tasks.Add(
                Task.Factory.StartNew(() =>
                { QuicksortTask1(elements, left, j, _compare); }));
            }

            if (i < right)
            {
                tasks.Add(
                Task.Factory.StartNew(() =>
                { QuicksortTask1(elements, i, right, _compare); }));
            }
            Task.WaitAll(tasks.ToArray());
        }
    }

    public class CustomIntComparerD: IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return x.CompareTo(y);
        }
    }
}
