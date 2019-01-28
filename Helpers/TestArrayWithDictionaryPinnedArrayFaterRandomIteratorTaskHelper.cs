using Perfomance.RandomIterators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Perfomance.Helpers
{
    public static class TestArrayWithDictionaryPinnedArrayFaterRandomIteratorTaskHelper
    {
        private static DiagnosticHelper dh = new DiagnosticHelper();
        public static void TestArrayWithDictionaryPinnedArrayFaterRandomIteratorTask()
        {
            ThreadPool.SetMaxThreads(1024, 1024);
            Console.WriteLine("Test TestArrayWithDictionaryPinnedArrayFaterRandomIteratorTask in tasks :");
            dh.StartWatch();
            var t = Task.Run(
            async () =>
            {
                List<Task> tasks = new List<Task>();
                for (int i = 0; i < 1024; i++)
                {
                    tasks.Add(
                        Task.Factory.StartNew(() => { TestArrayWithDictionaryPinnedArrayFaterRandomIterator(1024 * 1024, 100); })
                        );
                }
                await Task.WhenAll(tasks);
            });
            t.Wait();
            dh.StoptWatch();
            Console.WriteLine(dh.GetMessage());
        }

        unsafe public static void TestArrayWithDictionaryPinnedArrayFaterRandomIterator(int maxLengthCh = 512 * 1024 * 1024, int iteration = 200)
        {


            int maxLengthCh2 = (int)maxLengthCh << 1;
            Console.WriteLine("Test char array with dictionary and faster random iterator :");
            char[] buffer = new char[maxLengthCh];
            RandomIteratorUsafeXorshift rnd = new RandomIteratorUsafeXorshift(256);
            GCHandle gchbuffers = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            IntPtr addrBuffer = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, (int)maxLengthCh - 1);
            char* pBuffer = (char*)addrBuffer.ToPointer();
            char* pCurrentBuffer = pBuffer;
            char* pBufferStart = (char*)Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
            char[][] templateCh = { " apple".ToCharArray(), " town".ToCharArray(), " array".ToCharArray(), " summer".ToCharArray(), " winter".ToCharArray(), " april".ToCharArray(), " customer".ToCharArray(), " support".ToCharArray(), " laptop".ToCharArray(), " cross-domain".ToCharArray() };

            int n = templateCh.Length;
            //GCHandle.Alloc(templateCh, GCHandleType.Pinned);
            GCHandle[] lines = new GCHandle[n];
            //GCHandle gchLines = GCHandle.Alloc(lines, GCHandleType.Pinned);
            char*[] pLines = new char*[n];
            //GCHandle gchpLines = GCHandle.Alloc(pLines, GCHandleType.Pinned);
            IntPtr[] linesPtr = new IntPtr[n];
            //GCHandle gchlinesPtr = GCHandle.Alloc(linesPtr, GCHandleType.Pinned);
            for (int i = 0; i < n; i++)
            {
                lines[i] = GCHandle.Alloc(templateCh[i], GCHandleType.Pinned);
                linesPtr[i] = Marshal.UnsafeAddrOfPinnedArrayElement(templateCh[i], 0);
                pLines[i] = (char*)linesPtr[i].ToPointer();
            }
            int[] sizes = new int[n];
            GCHandle gchsizes = GCHandle.Alloc(sizes, GCHandleType.Pinned);
            IntPtr addrSizes = Marshal.UnsafeAddrOfPinnedArrayElement(sizes, 0);
            int* pSize = (int*)addrSizes.ToPointer();

            int[] sizes2 = new int[n];
            GCHandle gchsizes2 = GCHandle.Alloc(sizes2, GCHandleType.Pinned);
            IntPtr addrSizes2 = Marshal.UnsafeAddrOfPinnedArrayElement(sizes2, 0);
            int* pSizes2 = (int*)addrSizes2.ToPointer();

            int sizeTmp = 0;
            for (int i = 0; i < n; i++)
            {
                sizeTmp = templateCh[i].Length;
                sizes[i] = sizeTmp;
                sizes2[i] = sizeTmp << 1;
            }

            int[] randomizeDictionary = new int[256 * 256];
            int[] randomizeDictionarySize = new int[256 * 256];
            int[] randomizeDictionarySize2 = new int[256 * 256];

            int k = 0;
            for (int j = 0; j < 256 * 256; j++)
            {
                randomizeDictionary[j] = k;
                k++;
                if (k == n)
                {
                    k = 0;
                }
            }
            k = 0;
            for (int j = 0; j < 256 * 256; j++)
            {
                randomizeDictionarySize[j] = (int)sizes[k];
                k++;
                if (k == n)
                {
                    k = 0;
                }
            }
            k = 0;
            for (int j = 0; j < 256 * 256; j++)
            {
                randomizeDictionarySize2[j] = (int)sizes2[k];
                k++;
                if (k == n)
                {
                    k = 0;
                }
            }
            //dh.StartWatch();
            int position = maxLengthCh;
            int position2 = maxLengthCh << 2;
            int templateLenghtCh = (int)templateCh.Length;
            int templateLenghtCh2 = (int)templateCh.Length << 1;
            int randomIndex = 0;
            char[] endline = Environment.NewLine.ToCharArray();
            GCHandle gchendline = GCHandle.Alloc(endline, GCHandleType.Pinned);
            char[] dot = new char[1] { '.' };
            GCHandle gchdot = GCHandle.Alloc(endline, GCHandleType.Pinned);
            byte[] rndInt = new byte[4] { rnd.bNext(), rnd.bNext(), rnd.bNext(), rnd.bNext() };
            GCHandle gchrndInt = GCHandle.Alloc(endline, GCHandleType.Pinned);
            byte rndIntIndex = 0;
            int minStrLength = 0;
            byte wordCount = 0;
            fixed (int* psize = randomizeDictionarySize, psize2 = randomizeDictionarySize2, pdic = randomizeDictionary)
            {
                for (int i = 0; i < iteration; i++)
                {
                    position2 = maxLengthCh2;


                    //position = maxLengthCh - templateLenghtCh;
                    AddEndLine();
                    while (position2 >= minStrLength)
                    {
                        wordCount = (byte)((rnd.bNext() >> 5));
                        wordCount++;
                        if (AddNewLine(psize, psize2, pdic, wordCount))
                        {
                            AddNumber();
                            AddEndLine();
                        }
                    }

                    /*
                    if(position2 < 0)
                    {
                        pCurrentBuffer = pCurrentBuffer + templateLenghtCh - 1 ;
                        while(pCurrentBuffer >= pBufferStart)
                        {
                            *pCurrentBuffer = '*';
                            pCurrentBuffer--;
                        }
                    }*/
                }
            }
            //dh.StoptWatch();
            templateCh = null;
            buffer = null;
            templateCh = null;
            sizes = null;
            sizes2 = null;
            if (gchrndInt.IsAllocated)
            {
                gchrndInt.Free();
            }
            if (gchdot.IsAllocated)
            {
                gchdot.Free();
            }
            if (gchendline.IsAllocated)
            {
                gchendline.Free();
            }

            if (gchsizes2.IsAllocated)
            {
                gchsizes2.Free();
            }

            if (gchsizes.IsAllocated)
            {
                gchsizes.Free();
            }

            if (gchbuffers.IsAllocated)
            {
                gchbuffers.Free();/*
            if (gchLines.IsAllocated)
                gchLines.Free();*//*
            if (gchpLines.IsAllocated)
                gchpLines.Free();*//*
            if (gchlinesPtr.IsAllocated)
                gchlinesPtr.Free();*/
            }

            for (int i = 0; i < n; i++)
            {
                if (lines[i].IsAllocated)
                {
                    lines[i].Free();
                }
            }
            GarbachCollectorHelper.GBForceRun();
            //Console.WriteLine(dh.GetMessage());
            return;

            unsafe bool AddNewLine(int* psize, int* psize2, int* pdic, byte count)
            {
                byte counter = 0;
                randomIndex = (rnd.bNext() << 8) + rnd.bNext();//--rnd1.Next(n);
                //

                templateLenghtCh2 = *(psize2 + randomIndex);
                position2 -= templateLenghtCh2;
                while (position2 >= minStrLength)
                {
                    Buffer.BlockCopy(templateCh[*(pdic + randomIndex)], 0, buffer, position2, templateLenghtCh2);
                    counter++;
                    if (counter >= count)
                    {
                        return true;
                    }
                    else
                    {
                        templateLenghtCh2 = *(psize2 + randomIndex);
                        position2 -= templateLenghtCh2;
                    }
                }
                return false;
            }

            bool AddEndLine()
            {

                if (position2 > 6)
                {
                    position2 -= 4;
                    Buffer.BlockCopy(endline, 0, buffer, position2, 4);
                    return true;
                }

                return false;
            }
            /*
            bool AddNumber()
            { 
                uint rndNumber = BitConverter.ToUInt32(rndInt, 0);
                if (rndNumber == 0)
                {
                    rndNumber++;
                }
                byte shift = rnd.bNext();
                rndInt[rndIntIndex] = shift;
                rndIntIndex++;
                if(rndIntIndex>=4)
                {
                    rndIntIndex = 0;
                }
                for(int i = 0; i < 4; i++)
                {
                    rndInt[i] = (byte) (rndInt[i] & shift);
                }
                char[] strNumber = rndNumber.ToString().ToCharArray();
                int length2 = strNumber.Length << 1;
                if (position2 > length2)
                {
                    position2 -= 2;
                    Buffer.BlockCopy(dot, 0, buffer, position2, 2);
                    position2 -= length2;
                    Buffer.BlockCopy(strNumber, 0, buffer, position2, length2);
                       
                    return true;
                }

                return false;
            }
            */

            bool AddNumber()
            {
                int rndNumber = rnd.Next();
                ;
                char[] strNumber = rndNumber.ToString().ToCharArray();
                int length2 = strNumber.Length << 1;
                if (position2 > length2)
                {
                    position2 -= 2;
                    Buffer.BlockCopy(dot, 0, buffer, position2, 2);
                    position2 -= length2;
                    Buffer.BlockCopy(strNumber, 0, buffer, position2, length2);

                    return true;
                }

                return false;
            }

        }
    }
}
