using Perfomance.RandomIterators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perfomance.Helpers
{
    public class TestQuickSortPointerHelper
    {
        private static DiagnosticHelper dh = new DiagnosticHelper();

        public static void TestSort(int n)
        {
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("Test Pointer Sort  ");
            int[] array = new int[n];
            Console.WriteLine("Generate data");
            dh.StartWatch();
            RandomIteratorUsafeXorshiftEn rnd = new RandomIteratorUsafeXorshiftEn(n + 1);
            for (int i = 0; i < n; i++)
            {
                array[i] = rnd.Next32();
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
        unsafe public static void TestQuickSort(int n)
        {
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("Test Pointer Quick Sort");
            int[] array = new int[n];
            Console.WriteLine("Generate data");
            dh.StartWatch();
            RandomIteratorUsafeXorshiftEn rnd = new RandomIteratorUsafeXorshiftEn(n + 1);
            for (int i = 0; i < n; i++)
            {
                array[i] = rnd.Next32();
            }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            Console.WriteLine("Sort");
            dh.StartWatch();
            fixed(int* start = &array[0], end = &array[n-1])
            {
                Quicksort(start, end);
            }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            array = null;
            GarbachCollectorHelper.GBForceRun();
        }

        public static void TestQuickSortTaskLimited(int n, int limit)
        {
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("Test Pointer Quick Sort Limited ");
            int[] array = new int[n];
            Console.WriteLine("Generate data");
            dh.StartWatch();
            RandomIteratorUsafeXorshiftEn rnd = new RandomIteratorUsafeXorshiftEn(n + 1);
            for (int i = 0; i < n; i++)
            {
                array[i] = rnd.Next32();
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
            Console.WriteLine("Test Pointer Quick Sort Task");
            int[] array = new int[n];
            Console.WriteLine("Generate data");
            dh.StartWatch();
            RandomIteratorUsafeXorshiftEn rnd = new RandomIteratorUsafeXorshiftEn(n + 1);
            for (int i = 0; i < n; i++)
            {
                array[i] = rnd.Next32();
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
        unsafe public static void Quicksort(int* left, int* right)
        {
            //Console.WriteLine("step : ");
            //Console.WriteLine((ulong)left);
            //Console.WriteLine((ulong)right);


            int* i = left, j = right;
            UInt64 x = (((UInt64)left + (UInt64)right + 4) / 2);
            int* z = (int*)x;
            int pivot = *z;
            int tmp = 0;
            while (i <= j)
            {
                while (*i < pivot)
                {
                    i++;
                }

                while (*j > pivot)
                {
                    j--;
                }

                if (i < j)
                {
                    // Swap
                    tmp = *i;
                    *i = *j;
                    *j = tmp;

                    i++;
                    j--;
                }
            }

            if (left < j)
            {
                Quicksort( left, j);
            }

            if (i < right)
            {
                Quicksort(i, right);
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
                    //Quicksort(elements, left, j);
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
                    //Quicksort(elements, i, right);
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
                Task.Factory.StartNew(() =>
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
