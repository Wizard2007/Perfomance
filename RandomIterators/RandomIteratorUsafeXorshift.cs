using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using XorshiftDemo;

namespace Perfomance.RandomIterators
{
    unsafe public class RandomIteratorUsafeXorshift : IRandomIterator, IDisposable
    {
        #region Unsafe Values fileds 
        private GCHandle gchValues;            // Handle to GCHandle object used to pin the I/O buffer in memory.
        private byte* pValues;              // Pointer to the buffer used to perform I/O.
        private byte* pCurrent;
        private byte* pEnd;
        IntPtr pAddr;
        #endregion
        private byte[] values;
        private int length;
        private XorshiftUnrolled64 rndXorshiftUnrolled64;

        unsafe public RandomIteratorUsafeXorshift(int capasity)
        {
            length = capasity;
            values = new byte[length];
            PinBuffer(values);
            rndXorshiftUnrolled64  = new XorshiftUnrolled64();
            
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        unsafe public byte bNext()
        {
            if (pCurrent > pEnd)
            {
                pCurrent = pValues;
                pCurrent--;
                Reset();
            }           
            return *(pCurrent++);
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        unsafe public void Reset()
        {
            rndXorshiftUnrolled64.NextBytes(values);
        }
        #region Unsafe functions 
        public void UnpinValues()
        {
            // This function unpins the buffer and needs to be called before a new buffer is pinned or
            // when disposing of this object.  It does not need to be called directly since the code in Dispose
            // or PinBuffer will automatically call this function.
            if (gchValues.IsAllocated)
                gchValues.Free();
        }

        public void PinBuffer(Array Buffer)
        {
            // This function must be called to pin the buffer in memory before any file I/O is done.
            // This shows how to pin a buffer in memory for an extended period of time without using
            // the "Fixed" statement.  Pinning a buffer in memory can take some cycles, so this technique
            // is helpful when doing quite a bit of file I/O.
            //
            // Make sure we don't leak memory if this function was called before and the UnPinBuffer was not called.
            UnpinValues();
            gchValues = GCHandle.Alloc(Buffer, GCHandleType.Pinned);
            pAddr = Marshal.UnsafeAddrOfPinnedArrayElement(Buffer, 0);
            // pValues is the pointer used for all of the I/O functions in this class.
            pValues = (byte*)pAddr.ToPointer();           
            pEnd = pValues + length;
            pCurrent = pEnd;
            pCurrent++;
        }
        #endregion 
        #region IDisposable Support
        private bool disposedValue = false; // Для определения избыточных вызовов

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: освободить управляемое состояние (управляемые объекты).
                }
                UnpinValues();
                // TODO: освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить ниже метод завершения.

                // TODO: задать большим полям значение NULL.
                values = null;
                disposedValue = true;
            }
        }

        // TODO: переопределить метод завершения, только если Dispose(bool disposing) выше включает код для освобождения неуправляемых ресурсов.
         ~RandomIteratorUsafeXorshift() {
           // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
           Dispose(false);
        }

        // Этот код добавлен для правильной реализации шаблона высвобождаемого класса.
        public void Dispose()
        {
            // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
            Dispose(true);
            // TODO: раскомментировать следующую строку, если метод завершения переопределен выше.
            GC.SuppressFinalize(this);
        }

        public int Next()
        {
            return rndXorshiftUnrolled64.Next();
        }
        #endregion
    }

    unsafe public class RandomIteratorUsafeXorshiftEn : IRandomIterator, IDisposable
    {
        #region Unsafe Values fileds 
        private GCHandle gchValues;            // Handle to GCHandle object used to pin the I/O buffer in memory.
        private byte* pValues;              // Pointer to the buffer used to perform I/O.
        private byte* pCurrent;
        private byte* pEnd;
        private int index = -1;
        private int index16 = -1;
        private int index32 = -1;
        private int indexBool = -1;
        IntPtr pAddr;
        #endregion
        public byte[] values;
        private int length;
        private int length16;
        private int length32;

        private XorshiftUnrolled64 rndXorshiftUnrolled64;

        unsafe public RandomIteratorUsafeXorshiftEn(int capasity)
        {
            length = capasity;
            length16 = length - 2;
            length16 = length - 4;

            values = new byte[length];
            PinBuffer(values);
            rndXorshiftUnrolled64 = new XorshiftUnrolled64();
            Reset();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        unsafe public int Next16()
        {
            
            ++index16;
            if (index16 > length16 )
            {
                Reset();
                index16 = 0;
            }
            return BitConverter.ToUInt16(values, index16);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        unsafe public Int32 Next32()
        {

            ++index32;
            if (index32 > length32)
            {
                Reset();
                index32 = 0;
            }
            //return values[index32] + values[index32 + 1] << 8 + values[index32 + 2] << 16 + values[index32 + 3] << 24;
            return BitConverter.ToInt32(values, index32);
        }

        uint bitMask = 1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool NextBool()
        {

            if (bitMask == 1)
            {
                indexBool++;
                if (indexBool >= length)
                {
                    Reset();
                    indexBool = 0;
                }
                bitMask = 0x80;
                return (values[indexBool] & bitMask) == 0;
            }

            return (values[indexBool] & (bitMask >>= 1)) == 0;
        }

        //https://blogs.msdn.microsoft.com/davidnotario/2004/11/01/jit-optimizations-inlining-ii/
        //https://msdn.microsoft.com/en-us/library/ms973852.aspx
        //https://www.dotnetperls.com/aggressiveinlining
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        unsafe public byte bNext()            
        {
            index++;
            if (index < length)
            {
                
            }
            else
            {
                Reset();
                index = 0;
            }
            return values[index];
            /*
            if (pCurrent != pEnd)
            {                
            }
            else
            {                
                Reset();
                pCurrent = pValues;                
            }
            return *(++pCurrent);
            */
            
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        unsafe public byte bNextPointer()
        {
 
            if (pCurrent > pValues)
            {
                
            }
            else
            {                
                Reset();                
                pCurrent = pEnd;
            }
            return *(pCurrent--);
        }
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        unsafe public void Reset()
        {
            rndXorshiftUnrolled64.NextBytes(values);
        }
        #region Unsafe functions 
        public void UnpinValues()
        {
            // This function unpins the buffer and needs to be called before a new buffer is pinned or
            // when disposing of this object.  It does not need to be called directly since the code in Dispose
            // or PinBuffer will automatically call this function.
            if (gchValues.IsAllocated)
                gchValues.Free();
        }

        public void PinBuffer(Array Buffer)
        {
            // This function must be called to pin the buffer in memory before any file I/O is done.
            // This shows how to pin a buffer in memory for an extended period of time without using
            // the "Fixed" statement.  Pinning a buffer in memory can take some cycles, so this technique
            // is helpful when doing quite a bit of file I/O.
            //
            // Make sure we don't leak memory if this function was called before and the UnPinBuffer was not called.
            UnpinValues();
            gchValues = GCHandle.Alloc(Buffer, GCHandleType.Pinned);
            pAddr = Marshal.UnsafeAddrOfPinnedArrayElement(Buffer, 0);
            // pValues is the pointer used for all of the I/O functions in this class.
            pValues = (byte*)pAddr.ToPointer();            
            pEnd = pValues + length + 1;
            --pValues;
            pCurrent = pEnd;
        }
        #endregion 
        #region IDisposable Support
        private bool disposedValue = false; // Для определения избыточных вызовов

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: освободить управляемое состояние (управляемые объекты).
                }
                UnpinValues();
                // TODO: освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить ниже метод завершения.

                // TODO: задать большим полям значение NULL.
                values = null;
                disposedValue = true;
            }
        }

        // TODO: переопределить метод завершения, только если Dispose(bool disposing) выше включает код для освобождения неуправляемых ресурсов.
        ~RandomIteratorUsafeXorshiftEn()
        {
            // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
            Dispose(false);
        }

        // Этот код добавлен для правильной реализации шаблона высвобождаемого класса.
        public void Dispose()
        {
            // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
            Dispose(true);
            // TODO: раскомментировать следующую строку, если метод завершения переопределен выше.
            GC.SuppressFinalize(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Next()
        {
            return rndXorshiftUnrolled64.Next();
        }
        #endregion
    }
}
