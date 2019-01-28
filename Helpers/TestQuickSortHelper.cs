using Perfomance.RandomIterators;
using SharpNeatLib.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perfomance.Helpers
{
    public static class TestQuickSortHelper
    {
        private static DiagnosticHelper dh = new DiagnosticHelper();

        public static void TestSort(int n)
        {
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("Test Sort");
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
            Array.Sort(array);
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            array = null;
            GarbachCollectorHelper.GBForceRun();
        }
        public static void TestQuickSort(int n)
        {
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("Test Quick Sort");
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
            Quicksort(array, 0, n - 1);
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            array = null;
            GarbachCollectorHelper.GBForceRun();
        }

        public static void TestQuickSortTaskLimited(int n, int limit)
        {
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("Test Quick Sort Limited ");
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
            QuicksortTaskLimited(array, 0, n - 1, limit);
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            array = null;
            GarbachCollectorHelper.GBForceRun();
        }
        public static void TestQuickSortTask(int n)
        {
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("Test Quick Sort Task");
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
            QuicksortTask1(array, 0, n - 1);

            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            array = null;
            GarbachCollectorHelper.GBForceRun();
        }
        public static void Quicksort(int[] elements, int left, int right)
        {
            int i = left, j = right;
            int pivot = elements[(left + right) / 2];
            int tmp = 0;
            while (i <= j)
            {
                while (elements[i] < pivot)
                {
                    i++;
                }

                while (elements[j] > pivot)
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
                Quicksort(elements, left, j);
            }

            if (i < right)
            {
                Quicksort(elements, i, right);
            }
         }       

        public static void QuicksortTaskLimited(int[] elements, int left, int right, int limit)
        {
            int i = left, j = right;
            int pivot = elements[(left + right) / 2];
            int tmp = 0;
            while (i <= j)
            {
                while (elements[i] < pivot)
                {
                    i++;
                }

                while (elements[j] > pivot)
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
                    Quicksort(elements, left, j);
                }
                else
                {
                    //Console.WriteLine(j - left);
                   tasks.Add(
                        Task.Factory.StartNew(() => { QuicksortTaskLimited(elements, left, j, limit); })
                        );
                }
            }

            if (i < right)
            {
                
                if (right - i < limit)
                {
                    Quicksort(elements, i, right);
                }
                else
                {
                    //Console.WriteLine(right - i);
                    tasks.Add(
                        Task.Factory.StartNew(() => { QuicksortTaskLimited(elements, i, right, limit); })
                        );
                }
            }
            Task.WaitAll(tasks.ToArray());
        }

        public static void QuicksortTask(int[] elements, int left, int right)
        {
            int i = left, j = right;
            int pivot = elements[(left + right) / 2];
            int tmp = 0;
            while (i <= j)
            {
                while (elements[i] < pivot)
                {
                    i++;
                }

                while (elements[j] > pivot)
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
                    Task.Factory.StartNew(() => { QuicksortTask(elements, left, j); })
                    );
            }

            if (i < right)
            {
                tasks.Add(
                    Task.Factory.StartNew(() => { QuicksortTask(elements, i, right); })
                    );
            }
            Task.WaitAll(tasks.ToArray());
        }

        public static void QuicksortTask1(int[] elements, int left, int right)
        {
            int i = left, j = right;
            int pivot = elements[(left + right) / 2];
            //int tmp = 0;
            while (i <= j)
            {
                while (elements[i] < pivot)
                {
                    i++;
                }

                while (elements[j] > pivot)
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
                Task.Factory.StartNew( () =>
                { QuicksortTask1(elements, left, j); }));
            }

            if (i < right)
            {
                tasks.Add(
                Task.Factory.StartNew(() =>
               { QuicksortTask1(elements, i, right); }));
            }
            Task.WaitAll(tasks.ToArray());
        }
    }
}
