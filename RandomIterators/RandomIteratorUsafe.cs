using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Perfomance.RandomIterators
{
    unsafe public class RandomIteratorUsafe : IRandomIterator, IDisposable
    {
        #region Unsafe Values fileds 
        private GCHandle gchValues;            // Handle to GCHandle object used to pin the I/O buffer in memory.
        private int* pValues;              // Pointer to the buffer used to perform I/O.
        private int* pCurrent;
        private int* pEnd;
        IntPtr pAddr;
        #endregion
        private int[] values;
        private int length;
        private int currentIndex = -1;
        private int lower;
        private int upper;
        private Random rnd = new Random();
        unsafe private void InitValues()
        {
            int* startPtr = pValues;
            
            while(startPtr <= pEnd)
            {
                *startPtr = rnd.Next(lower, upper);
                startPtr++;
            }
        }
        unsafe public RandomIteratorUsafe(int capasity, int lower, int upper)
        {
            length = capasity;
            values = new int[length];
            PinBuffer(values);
            this.lower = lower;
            this.upper = upper;
            InitValues();
        }
        unsafe public int Next()
        {
            if (pCurrent > pEnd)
            {
                pCurrent = pValues;
                pCurrent--;
                Reset();
            }           
            return *(pCurrent++);
        }

        unsafe public void Reset()
        {
            int shift = rnd.Next(lower, upper);
            int* startPtr = pValues;
            while (startPtr <= pEnd)
            {
                *startPtr = (*startPtr + shift)>>1;
                startPtr++;
            }
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
            pValues = (int*)pAddr.ToPointer();
            pCurrent = pValues;
            pCurrent--;
            pEnd = pValues + length;
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
         ~RandomIteratorUsafe() {
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

        public byte bNext()
        {
            throw new NotImplementedException();
        }
        #endregion
    }

    unsafe public class RandomIteratorUsafeEn : IRandomIterator, IDisposable
    {
        #region Unsafe Values fileds 
        private GCHandle gchValues;            // Handle to GCHandle object used to pin the I/O buffer in memory.
        private int* pValues;              // Pointer to the buffer used to perform I/O.
        private int* pCurrent;
        private int* pEnd;
        IntPtr pAddr;
        #endregion
        private int[] values;
        private int length;
        private int currentIndex = -1;
        private int lower;
        private int upper;
        private Random rnd = new Random();
        unsafe private void InitValues()
        {
            int* startPtr = pValues;
            do
            {
                *startPtr = rnd.Next(lower, upper);
                ++startPtr;
            }
            while (startPtr != pEnd);
        }
        unsafe public RandomIteratorUsafeEn(int capasity, int lower, int upper)
        {
            length = capasity;
            values = new int[length];
            PinBuffer(values);
            this.lower = lower;
            this.upper = upper;
            InitValues();
        }
        unsafe public int Next()
        {
            if (pCurrent != pEnd)
            {
                return *(++pCurrent);
            }
            else
            {
                pCurrent = pValues;                
                Reset();
                return *(pCurrent);
            }
        }

        unsafe public void Reset()
        {
            int shift = rnd.Next(lower, upper);
            int* startPtr = pValues;
            do
            {
                *startPtr = (*startPtr + shift) >> 1;
                ++startPtr;
            }
            while (startPtr != pEnd) ;
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
            pValues = (int*)pAddr.ToPointer();
            pCurrent = pValues;
            --pCurrent;
            pEnd = pValues + length + 1;
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
        ~RandomIteratorUsafeEn()
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

        public byte bNext()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
