using Perfomance.RandomIterators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Perfomance.Helpers
{
    public class TestBytesGeneratorHelper
    {
        private static DiagnosticHelper dh = new DiagnosticHelper();
        unsafe public static void TestBytesGenerator(int maxLengthCh = 512 * 1024 * 1024, int iteration = 200)
        {
            RandomIteratorUsafeXorshiftEn rnd = new RandomIteratorUsafeXorshiftEn(1024 * 1024);
            RandomBytesProducer.InitBytes();
            //-------
            int maxLengthCh2 = (int)maxLengthCh << 1;
            Console.WriteLine("Test Bytes Generator :");
            byte[] buffer = new byte[maxLengthCh];


            GCHandle gchbuffers = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            IntPtr addrBuffer = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, (int)maxLengthCh - 1);
            char* pBuffer = (char*)addrBuffer.ToPointer();
            char* pCurrentBuffer = pBuffer;
            char* pBufferStart = (char*)Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);

            string[] _templateCh = new string[10]
                {
                    " apple"
                    , " town"
                    , " array"
                    , " summer"
                    , " winter"
                    , " april"
                    , " customer"
                    , " support"
                    , " laptop"
                    , " cross-domain"
                };

            byte[][] templateCh = DataProducer.ConvetStringListToBytes(_templateCh);
            int n = templateCh.Length;
            
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

            dh.StartWatch();
            int position = maxLengthCh;
            int templateLenghtCh = (int)templateCh.Length;
            int randomIndex = 0;

            byte[] endline = Encoding.ASCII.GetBytes(Environment.NewLine);

            GCHandle gchendline = GCHandle.Alloc(endline, GCHandleType.Pinned);

            byte[] dot = Encoding.ASCII.GetBytes(".");

            GCHandle gchdot = GCHandle.Alloc(endline, GCHandleType.Pinned);
            GCHandle gchrndInt = GCHandle.Alloc(endline, GCHandleType.Pinned);
            
            int minStrLength = 0;
            
            fixed (int* psize = randomizeDictionarySize, psize2 = randomizeDictionarySize2, pdic = randomizeDictionary)
            {
                for (int i = 0; i < iteration; i++)
                {                    
                    position = maxLengthCh;
                    AddEndLine();
                    while (position >= minStrLength)
                    {
                        if (AddNewLine(psize, psize2, pdic))
                        {
                            AddNumber();
                            AddEndLine();
                        }
                    }

                    /*
                    if(position < 0)
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
            dh.StoptWatch();
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
                gchbuffers.Free();
            }

            GarbachCollectorHelper.GBForceRun();
            Console.WriteLine(dh.GetMessage());


            unsafe bool AddNewLine(int* psize, int* psize2, int* pdic)
            {                
                randomIndex = rnd.Next16();
                bool result = false;               

                templateLenghtCh = *(psize + randomIndex);
                position -= templateLenghtCh;
                while (position >= minStrLength)
                {
                    Buffer.BlockCopy(templateCh[*(pdic + randomIndex)], 0, buffer, position, templateLenghtCh);
                    result = true;
                    if (rnd.NextBool())                    
                    {
                        templateLenghtCh = *(psize + randomIndex);
                        position -= templateLenghtCh;
                    }
                    else
                    {
                        break;
                    }
                }
                return result;
            }

            bool AddEndLine()
            {
                if (position > 3)
                {
                    position -= 2;
                    Buffer.BlockCopy(endline, 0, buffer, position, 2);
                    return true;
                }

                return false;
            }

            bool AddNumber()
            {
                int rndNumber = rnd.Next16();
                byte[] strNumber = RandomBytesProducer.byteList[rndNumber];
                int length = RandomBytesProducer.byteListSizes[rndNumber];
                if (position > length)
                {
                    position -= 1;
                    Buffer.BlockCopy(dot, 0, buffer, position, 1);
                    position -= length;
                    Buffer.BlockCopy(strNumber, 0, buffer, position, length);

                    return true;
                }

                return false;
            }

        }
        /*
        {
            int maxLengthCh = 512 * 1024 * 1024;
            int maxLengthCh2 = maxLengthCh << 1;
            Console.WriteLine("Test Bytes Generator :");
            char[] buffer = new char[maxLengthCh];                        

            string[] templateCh = new string [10]
                {
                    " apple"
                    , " town"
                    , " array"
                    , " summer"
                    , " winter"
                    , " april"
                    , " customer"
                    , " support"
                    , " laptop"
                    , " cross-domain"
                };
            RandomIteratorUsafeXorshiftEn randomIteratorUsafeXorshiftEn = new RandomIteratorUsafeXorshiftEn(1024 * 1024);
            RandomBytesProducer.InitBytes();

            int n = templateCh.Length;
            //GCHandle gchLines = GCHandle.Alloc(lines, GCHandleType.Pinned);
            char*[] pLines = new char*[n];
            //GCHandle gchpLines = GCHandle.Alloc(pLines, GCHandleType.Pinned);
            IntPtr[] linesPtr = new IntPtr[n];

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
            dh.StartWatch();
            int position = maxLengthCh;
            int position2 = maxLengthCh << 2;
            int templateLenghtCh = templateCh.Length;
            int templateLenghtCh2 = templateCh.Length << 1;
            int randomIndex = 0;
            n--;//--
            for (int i = 0; i < 200; i++)
            {
                randomIndex = n;//--rnd1.Next(n);
                templateLenghtCh = *(pSize + randomIndex);
                //
                templateLenghtCh2 = *(pSizes2 + randomIndex);
                //position = maxLengthCh - templateLenghtCh;
                position2 = maxLengthCh2 - templateLenghtCh2;
                while (position2 >= 0)
                {
                    Buffer.BlockCopy(templateCh[randomIndex], 0, buffer, position2, templateLenghtCh2);
                    //Buffer.MemoryCopy(pLines[randomIndex], pCurrentBuffer, templateLenghtCh2, templateLenghtCh2);
                    //--randomIndex = rnd1.Next(n);
                    templateLenghtCh2 = *(pSizes2 + randomIndex);
                    //templateLenghtCh = *(pSize + randomIndex);
                    position2 -= templateLenghtCh2;
                    //position -= templateLenghtCh;
                    //pCurrentBuffer = pCurrentBuffer - templateLenghtCh;
                }                
                //if(position2 < 0)
                //{
                //    pCurrentBuffer = pCurrentBuffer + templateLenghtCh - 1 ;
                //    while(pCurrentBuffer >= pBufferStart)
                //    {
                //        *pCurrentBuffer = '*';
                //        pCurrentBuffer--;
                //    }
                //}
            }
            dh.StoptWatch();
            templateCh = null;
            buffer = null;
            templateCh = null;
            sizes = null;
            sizes2 = null;
            if (gchsizes2.IsAllocated)
            {
                gchsizes2.Free();
            }

            if (gchsizes.IsAllocated)
            {
                gchsizes.Free();
            }
            GarbachCollectorHelper.GBForceRun();
            Console.WriteLine(dh.GetMessage());
        }*/
    }
}
