using System;

namespace Perfomance.Helpers
{
    public static class CopyExtensions
    {
        private const int Threshold = 128;

        private static readonly int PlatformWordSize = IntPtr.Size;
        private static readonly int PlatformWordSizeBits = PlatformWordSize * 8;

        public static void CopyMemoryCh(char[] src, int srcOff, char[] dst, int dstOff, int count)
        {
            if (src == null)
            {
                throw new ArgumentNullException("src");
            }

            if (dst == null)
            {
                throw new ArgumentNullException("dst");
            }

            if (srcOff < 0)
            {
                throw new ArgumentOutOfRangeException("srcOffset");
            }

            if (dstOff < 0)
            {
                throw new ArgumentOutOfRangeException("dstOffset");
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count");
            }

            if (count == 0)
            {
                return;
            }

            unsafe
            {
                fixed (char* srcPtr = &src[srcOff])
                fixed (char* dstPtr = &dst[dstOff])
                {
                    CopyMemoryCh(srcPtr, dstPtr, count);
                }
            }
        }

        public static void FastCopy(this byte[] src, int srcOffset, byte[] dst, int dstOffset, int count)
        {
            if (srcOffset < 0)
            {
                throw new ArgumentOutOfRangeException("srcOffset");
            }

            if (dst == null)
            {
                throw new ArgumentNullException("dst");
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count");
            }

            if (src.Length - srcOffset < count || dst.Length - dstOffset < count)
            {
                throw new ArgumentOutOfRangeException();
            }

#if UNITY
            Buffer.BlockCopy(src, srcOffset, dst, dstOffset, count);
#else
            if (count <= Threshold)
            {
                CopyMemory(src, srcOffset, dst, dstOffset, count);
            }
            else
            {
                Buffer.BlockCopy(src, srcOffset, dst, dstOffset, count);
            }
#endif
        }

        private static void CopyMemory(byte[] src, int srcOff, byte[] dst, int dstOff, int count)
        {
            if (src == null)
            {
                throw new ArgumentNullException("src");
            }

            if (dst == null)
            {
                throw new ArgumentNullException("dst");
            }

            if (srcOff < 0)
            {
                throw new ArgumentOutOfRangeException("srcOffset");
            }

            if (dstOff < 0)
            {
                throw new ArgumentOutOfRangeException("dstOffset");
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count");
            }

            if (count == 0)
            {
                return;
            }

            unsafe
            {
                fixed (byte* srcPtr = &src[srcOff])
                fixed (byte* dstPtr = &dst[dstOff])
                {
                    CopyMemory(srcPtr, dstPtr, count);
                }
            }
        }

        private static unsafe void CopyMemory(byte* srcPtr, byte* dstPtr, int count)
        {
            const int u32Size = sizeof(UInt32);
            const int u64Size = sizeof(UInt64);
            const int u128Size = sizeof(UInt64) * 2;

            byte* srcEndPtr = srcPtr + count;

            if (PlatformWordSize == u32Size)
            {
                // 32-bit
                while (srcPtr + u64Size <= srcEndPtr)
                {
                    *(UInt32*)dstPtr = *(UInt32*)srcPtr;
                    dstPtr += u32Size;
                    srcPtr += u32Size;
                    *(UInt32*)dstPtr = *(UInt32*)srcPtr;
                    dstPtr += u32Size;
                    srcPtr += u32Size;
                }
            }
            else if (PlatformWordSize == u64Size)
            {
                // 64-bit
                while (srcPtr + u128Size <= srcEndPtr)
                {
                    *(UInt64*)dstPtr = *(UInt64*)srcPtr;
                    dstPtr += u64Size;
                    srcPtr += u64Size;
                    *(UInt64*)dstPtr = *(UInt64*)srcPtr;
                    dstPtr += u64Size;
                    srcPtr += u64Size;
                }

                if (srcPtr + u64Size <= srcEndPtr)
                {
                    *(UInt64*)dstPtr ^= *(UInt64*)srcPtr;
                    dstPtr += u64Size;
                    srcPtr += u64Size;
                }
            }

            if (srcPtr + u32Size <= srcEndPtr)
            {
                *(UInt32*)dstPtr = *(UInt32*)srcPtr;
                dstPtr += u32Size;
                srcPtr += u32Size;
            }

            if (srcPtr + sizeof(UInt16) <= srcEndPtr)
            {
                *(UInt16*)dstPtr = *(UInt16*)srcPtr;
                dstPtr += sizeof(UInt16);
                srcPtr += sizeof(UInt16);
            }

            if (srcPtr + 1 <= srcEndPtr)
            {
                *dstPtr = *srcPtr;
            }
        }

        private static unsafe void CopyMemoryCh(char* srcPtr, char* dstPtr, int count)
        {
            char* srcEndPtr = srcPtr + count;
            while (srcPtr <= srcEndPtr)
            {
                *dstPtr = *(char*)srcPtr;
                dstPtr++;
                srcPtr++;
            }
        }
    }
}
