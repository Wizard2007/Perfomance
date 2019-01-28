using Perfomance.Helpers;
using Perfomance.RandomIterators;
using SharpNeatLib.Maths;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XorshiftDemo;

namespace Perfomance
{
    

    //https://limbioliong.wordpress.com/2011/06/21/passing-multi-dimensional-managed-array-to-c-part-1/
    internal class Program
    {
        private static DiagnosticHelper dh = new DiagnosticHelper();
        private static void Main(string[] args)
        {
            unsafe
            {
                byte[] test = new byte[16] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
                fixed (byte* start = &test[0])
                {
                    short* t = (short*)start;
                    Console.WriteLine(*t);
                    t++;
                    Console.WriteLine(*t);
                    t++;
                    Console.WriteLine(*t);
                    t++;
                    Console.WriteLine(*t);
                    t++;
                    Console.WriteLine(*t);
                    t++;
                }
            }
            //TestRandomGeneratorRndXorshiftByteHelper.TestRandomGeneratorRndXorshiftByte();
            
            ThreadPool.SetMaxThreads(1024, 1024);

            TestQuickSortComparerPointerHelper.TestQuickSort(Int32.MaxValue / 50);
            TestQuickSortHelper.TestQuickSort(Int32.MaxValue / 50);

            TestQuickSortHelper.TestSort(Int32.MaxValue / 5);
            // TestQuickSortHelper.TestQuickSortTask(Int32.MaxValue / 5);
            TestQuickSortHelper.TestQuickSortTaskLimited(Int32.MaxValue / 5, 10000000);

            

            //TestQuickSortPointerHelper.TestQuickSort(1000000);            
            // TestQuickSortComparerHelper.TestQuickSortTask(Int32.MaxValue / 5);
            TestQuickSortComparerDelegateHelper.TestQuickSortTaskLimited(Int32.MaxValue / 5, 10000000);
            TestQuickSortComparerDelegateHelper.TestSort(Int32.MaxValue / 5);
            TestQuickSortComparerDelegateHelper.TestQuickSort(Int32.MaxValue / 5);           
            
            
            
            
            
            
            TestAccesToVariablesHelper.TestAccesToVariables(Int32.MaxValue / 3);
            TestListVsArrayHelpercs.TestListVsArray(Int32.MaxValue / 4);
            //Test2Helper.Test2();
            try
            {

                TestArrayAccessHelper.IterateArray();

                string[] list = new string[5]
                    {
                        " 1111" + Environment.NewLine
                        , " 2222" + Environment.NewLine
                        , " 3333" + Environment.NewLine
                        , " 4444" + Environment.NewLine
                        , " 5555" + Environment.NewLine
                    };

                var result = DataProducer.ConvetStringListToBytes(list);
                RandomBytesProducer.InitBytes();
                TestRandomIteratorUsafeXorshiftHelper.TestRandomIteratorUsafeXorshift();

                TestArrayCopyBlockCopyHelper.TestArrayCopyBlockCopy(100);
                TestArrayCopyBlockCopyHelper.TestArrayCopyBlockCopy1(100);
                TestArrayCopyFixedHelper.TestArrayCopyFixed(100);
                TestArrayCopyIndexingHelper.TestArrayCopyIndexing(100);
                TestArrayCopyIndexingHelper.TestArrayCopyIndexing1(100);
                TestArrayCopyIndexingHelper.TestArrayCopyIndexing2(100);

                TestBytesGeneratorStrongInlineHelper.TestBytesGeneratorStrongInline();
                TestBytesGeneratorInlineHelper.TestBytesGeneratorInline();
                TestBytesGeneratorHelper.TestBytesGenerator();
                TestRandomGeneratorFastRandomHelper.TestRandomGeneratorFastRandom();
                TestRandomBytesProducerHelper.TestRandomBytesProducer();


                //
                TestArrayWithDictionaryPinnedArrayFaterRandomIteratorTaskHelper.
                TestArrayWithDictionaryPinnedArrayFaterRandomIterator();
                TestArrayWithDictionaryPinnedArrayHelper.
                TestArrayWithDictionaryPinnedArray();
                TestArrayWithDictionaryUseLengthHelper.
                TestArrayWithDictionaryUseLength();
                TestArrayWithDictionaryHelper.
                TestArrayWithDictionary();
                TestRandomGeneratorRndXorshiftHelper.
                TestRandomGeneratorRndXorshift();
                TestRandomGeneratorRndXorshiftByteHelper.
                TestRandomGeneratorRndXorshiftByte();
                TestRandomGeneratorFastRandomHelper.
                TestRandomGeneratorFastRandom();
                TestRandomGeneratorIteratorUnsafeHelper.
                TestRandomGeneratorIteratorUnsafe();
                TestRandomHelper.
                TestRandom();
                TestRandomGeneratorIteratorHelper.
                TestRandomGeneratorIterator();

                TestArrayWithDictionaryPinnedArrayHelper.
                TestArrayWithDictionaryPinnedArray();
                TestArrayWithDictionaryHelper.
                TestArrayWithDictionary();
                TestArrayWithDictionaryUseLengthHelper.
                TestArrayWithDictionaryUseLength();
                TestCharArrayCopyExtensionsHelper.
                TestCharArrayCopyExtensions();
                TestCharArrayHelper.TestCharArray();
                TestChaArrayUseLengthHelper.
                TestChaArrayUseLength();
                TestStringBuilderHelper.
                TestStringBuilder();
                TestStringBuilderWithCharArrayHelper.
                TestStringBuilderWithCharArray();
                TestIterateStringLengthHelper.
                TestIterateStringLength();
                TestIterateCharArrayLengthHelper.
                TestIterateCharArrayLength();
                TestGetLengthHelper.
                TestGetLength();


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}