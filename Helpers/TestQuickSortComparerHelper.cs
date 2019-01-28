using Perfomance.RandomIterators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perfomance.Helpers
{
    public class TestQuickSortComparerHelper
    {
        private static DiagnosticHelper dh = new DiagnosticHelper();

        public static void TestSort(int n)
        {
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("Test Comparer Sort");
            int[] array = new int[n];
            Console.WriteLine("Generate data");
            dh.StartWatch();
            RandomIteratorUsafeXorshiftEn rnd = new RandomIteratorUsafeXorshiftEn(n + 1);
            for (int i = 0; i < n; i++)
            {
                array[i] = rnd.Next16();
            }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            Console.WriteLine("Sort");
            dh.StartWatch();
            Array.Sort(array, new CustomIntComparer());
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
            dh.StartWatch();
            RandomIteratorUsafeXorshiftEn rnd = new RandomIteratorUsafeXorshiftEn(n + 1);
            for (int i = 0; i < n; i++)
            {
                array[i] = rnd.Next16();
            }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            Console.WriteLine("Sort");
            dh.StartWatch();
            Quicksort(array, 0, n - 1, new CustomIntComparer());
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
            dh.StartWatch();
            RandomIteratorUsafeXorshiftEn rnd = new RandomIteratorUsafeXorshiftEn(n + 1);
            for (int i = 0; i < n; i++)
            {
                array[i] = rnd.Next16();
            }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            Console.WriteLine("Sort");
            dh.StartWatch();
            QuicksortTaskLimited(array, 0, n - 1, limit, new CustomIntComparer());
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
            dh.StartWatch();
            RandomIteratorUsafeXorshiftEn rnd = new RandomIteratorUsafeXorshiftEn(n + 1);
            for (int i = 0; i < n; i++)
            {
                array[i] = rnd.Next16();
            }
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            Console.WriteLine("Sort");
            dh.StartWatch();
            QuicksortTask1(array, 0, n - 1, new CustomIntComparer());

            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
            array = null;
            GarbachCollectorHelper.GBForceRun();
        }
        public static void Quicksort(int[] elements, int left, int right, IComparer<int> _compare)
        {
            int i = left, j = right;
            int pivot = elements[(left + right) / 2];
            int tmp = 0;
            while (i <= j)
            {
                while (_compare.Compare( elements[i], pivot) < 0)
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

            if (left < j)
            {
                Quicksort(elements, left, j, _compare);
            }

            if (i < right)
            {
                Quicksort(elements, i, right, _compare);
            }
        }

        public static void QuicksortTaskLimited(int[] elements, int left, int right, int limit, IComparer<int> _compare)
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

        public static void QuicksortTask1(int[] elements, int left, int right, IComparer<int> _compare)
        {
            int i = left, j = right;
            int pivot = elements[(left + right) / 2];
            //int tmp = 0;
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

    public class CustomIntComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return x.CompareTo(y);            
        }
    }
}
