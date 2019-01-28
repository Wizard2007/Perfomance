using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Perfomance.Helpers
{
    public class TestArrayWithDictionaryPinnedArrayHelper
    {
        private static DiagnosticHelper dh = new DiagnosticHelper();
        unsafe public static void TestArrayWithDictionaryPinnedArray()
        {
            int maxLengthCh = 512 * 1024 * 1024;
            int maxLengthCh2 = maxLengthCh << 1;
            Console.WriteLine("Test char array with dictionary :");
            char[] buffer = new char[maxLengthCh];

            GCHandle gchbuffers = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            IntPtr addrBuffer = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, maxLengthCh - 1);
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
                pCurrentBuffer = pBuffer - templateLenghtCh + 1;
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
            Console.WriteLine(dh.GetMessage());
        }
    }
}
