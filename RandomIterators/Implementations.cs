// This code is provided under the MIT license. Originally by Roman Starkov.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace XorshiftDemo
{
    abstract class Xorshift : Random
    {
        protected uint _x = 123456789;
        protected uint _y = 362436069;
        protected uint _z = 521288629;
        protected uint _w = 88675123;

        public abstract int FillBufferMultipleRequired { get; }
        protected abstract void FillBuffer(byte[] buf, int offset, int offsetEnd);

        private Queue<byte> _bytes = new Queue<byte>();

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void NextBytes(byte[] buffer)
        {
            int offset = 0;
            while (_bytes.Any() && offset < buffer.Length)
                buffer[offset++] = _bytes.Dequeue();

            int length = ((buffer.Length - offset) / FillBufferMultipleRequired) * FillBufferMultipleRequired;
            if (length > 0)
                FillBuffer(buffer, offset, offset + length);

            offset += length;
            while (offset < buffer.Length)
            {
                if (_bytes.Count == 0)
                {
                    uint t = _x ^ (_x << 11);
                    _x = _y; _y = _z; _z = _w;
                    _w = _w ^ (_w >> 19) ^ (t ^ (t >> 8));
                    _bytes.Enqueue((byte) (_w & 0xFF));
                    _bytes.Enqueue((byte) ((_w >> 8) & 0xFF));
                    _bytes.Enqueue((byte) ((_w >> 16) & 0xFF));
                    _bytes.Enqueue((byte) ((_w >> 24) & 0xFF));
                }
                buffer[offset++] = _bytes.Dequeue();
            }
        }
    }

    sealed class XorshiftSafe : Xorshift
    {
        public override int FillBufferMultipleRequired { get { return 4; } }

        protected override void FillBuffer(byte[] buf, int offset, int offsetEnd)
        {
            while (offset < offsetEnd)
            {
                uint t = _x ^ (_x << 11);
                _x = _y; _y = _z; _z = _w;
                _w = _w ^ (_w >> 19) ^ (t ^ (t >> 8));
                buf[offset++] = (byte) (_w & 0xFF);
                buf[offset++] = (byte) ((_w >> 8) & 0xFF);
                buf[offset++] = (byte) ((_w >> 16) & 0xFF);
                buf[offset++] = (byte) ((_w >> 24) & 0xFF);
            }
        }
    }

    sealed class XorshiftSafeLocals : Xorshift
    {
        public override int FillBufferMultipleRequired { get { return 4; } }

        protected override void FillBuffer(byte[] buf, int offset, int offsetEnd)
        {
            uint x = _x, y = _y, z = _z, w = _w;
            while (offset < offsetEnd)
            {
                uint t = x ^ (x << 11);
                x = y; y = z; z = w;
                w = w ^ (w >> 19) ^ (t ^ (t >> 8));
                buf[offset++] = (byte) (w & 0xFF);
                buf[offset++] = (byte) ((w >> 8) & 0xFF);
                buf[offset++] = (byte) ((w >> 16) & 0xFF);
                buf[offset++] = (byte) ((w >> 24) & 0xFF);
            }
            _x = x; _y = y; _z = z; _w = w;
        }
    }

    sealed class XorshiftUnsafeSilly : Xorshift
    {
        public override int FillBufferMultipleRequired { get { return 4; } }

        protected unsafe override void FillBuffer(byte[] buf, int offset, int offsetEnd)
        {
            fixed (byte* pbytes = buf)
            {
                byte* pbuf = pbytes + offset;
                byte* pend = pbytes + offsetEnd;
                while (pbuf < pend)
                {
                    uint t = _x ^ (_x << 11);
                    _x = _y; _y = _z; _z = _w;
                    _w = _w ^ (_w >> 19) ^ (t ^ (t >> 8));
                    *(pbuf++) = (byte) (_w & 0xFF);
                    *(pbuf++) = (byte) ((_w >> 8) & 0xFF);
                    *(pbuf++) = (byte) ((_w >> 16) & 0xFF);
                    *(pbuf++) = (byte) ((_w >> 24) & 0xFF);
                }
            }
        }
    }

    sealed class XorshiftUnsafe : Xorshift
    {
        public override int FillBufferMultipleRequired { get { return 4; } }

        protected unsafe override void FillBuffer(byte[] buf, int offset, int offsetEnd)
        {
            fixed (byte* pbytes = buf)
            {
                uint* pbuf = (uint*) (pbytes + offset);
                uint* pend = (uint*) (pbytes + offsetEnd);
                while (pbuf < pend)
                {
                    uint t = _x ^ (_x << 11);
                    _x = _y; _y = _z; _z = _w;
                    _w = _w ^ (_w >> 19) ^ (t ^ (t >> 8));
                    *(pbuf++) = _w;
                }
            }
        }
    }

    sealed class XorshiftUnsafeLocals : Xorshift
    {
        public override int FillBufferMultipleRequired { get { return 4; } }

        protected unsafe override void FillBuffer(byte[] buf, int offset, int offsetEnd)
        {
            uint x = _x, y = _y, z = _z, w = _w;
            fixed (byte* pbytes = buf)
            {
                uint* pbuf = (uint*) (pbytes + offset);
                uint* pend = (uint*) (pbytes + offsetEnd);
                while (pbuf < pend)
                {
                    uint t = x ^ (x << 11);
                    x = y; y = z; z = w;
                    w = w ^ (w >> 19) ^ (t ^ (t >> 8));
                    *(pbuf++) = w;
                }
            }
            _x = x; _y = y; _z = z; _w = w;
        }
    }

    sealed class XorshiftUnrolled2Step1Locals : Xorshift
    {
        public override int FillBufferMultipleRequired { get { return 8; } }

        protected unsafe override void FillBuffer(byte[] buf, int offset, int offsetEnd)
        {
            uint x = _x, y = _y, z = _z, w = _w;
            fixed (byte* pbytes = buf)
            {
                uint* pbuf = (uint*) (pbytes + offset);
                uint* pend = (uint*) (pbytes + offsetEnd);
                while (pbuf < pend)
                {
                    uint t = x ^ (x << 11);
                    x = y; y = z; z = w;
                    w = w ^ (w >> 19) ^ (t ^ (t >> 8));
                    *(pbuf++) = w;

                    t = x ^ (x << 11);
                    x = y; y = z; z = w;
                    w = w ^ (w >> 19) ^ (t ^ (t >> 8));
                    *(pbuf++) = w;
                }
            }
            _x = x; _y = y; _z = z; _w = w;
        }
    }

    sealed class XorshiftUnrolled2Step2Locals : Xorshift
    {
        public override int FillBufferMultipleRequired { get { return 8; } }

        protected unsafe override void FillBuffer(byte[] buf, int offset, int offsetEnd)
        {
            uint x = _x, y = _y, z = _z, w = _w;
            fixed (byte* pbytes = buf)
            {
                uint* pbuf = (uint*) (pbytes + offset);
                uint* pend = (uint*) (pbytes + offsetEnd);
                while (pbuf < pend)
                {
                    uint tx = x ^ (x << 11);
                    uint ty = y ^ (y << 11);

                    y = z; z = w;
                    w = w ^ (w >> 19) ^ (tx ^ (tx >> 8));
                    *(pbuf++) = w;

                    x = y; y = z; z = w;
                    w = w ^ (w >> 19) ^ (ty ^ (ty >> 8));
                    *(pbuf++) = w;
                }
            }
            _x = x; _y = y; _z = z; _w = w;
        }
    }

    sealed class XorshiftUnrolled2Step3Locals : Xorshift
    {
        public override int FillBufferMultipleRequired { get { return 8; } }

        protected unsafe override void FillBuffer(byte[] buf, int offset, int offsetEnd)
        {
            uint x = _x, y = _y, z = _z, w = _w;
            fixed (byte* pbytes = buf)
            {
                uint* pbuf = (uint*) (pbytes + offset);
                uint* pend = (uint*) (pbytes + offsetEnd);
                while (pbuf < pend)
                {
                    uint tx = x ^ (x << 11);
                    uint ty = y ^ (y << 11);
                    x = z;
                    y = w;
                    *(pbuf++) = z = w ^ (w >> 19) ^ (tx ^ (tx >> 8));
                    *(pbuf++) = w = z ^ (z >> 19) ^ (ty ^ (ty >> 8));
                }
            }
            _x = x; _y = y; _z = z; _w = w;
        }
    }

    sealed class XorshiftUnrolled4 : Xorshift
    {
        public override int FillBufferMultipleRequired { get { return 16; } }

        protected unsafe override void FillBuffer(byte[] buf, int offset, int offsetEnd)
        {
            fixed (byte* pbytes = buf)
            {
                uint* pbuf = (uint*) (pbytes + offset);
                uint* pend = (uint*) (pbytes + offsetEnd);
                while (pbuf < pend)
                {
                    uint tx = _x ^ (_x << 11);
                    uint ty = _y ^ (_y << 11);
                    uint tz = _z ^ (_z << 11);
                    uint tw = _w ^ (_w << 11);
                    *(pbuf++) = _x = _w ^ (_w >> 19) ^ (tx ^ (tx >> 8));
                    *(pbuf++) = _y = _x ^ (_x >> 19) ^ (ty ^ (ty >> 8));
                    *(pbuf++) = _z = _y ^ (_y >> 19) ^ (tz ^ (tz >> 8));
                    *(pbuf++) = _w = _z ^ (_z >> 19) ^ (tw ^ (tw >> 8));
                }
            }
        }
    }

    sealed class XorshiftUnrolled4Locals : Xorshift
    {
        public override int FillBufferMultipleRequired { get { return 16; } }

        protected unsafe override void FillBuffer(byte[] buf, int offset, int offsetEnd)
        {
            uint x = _x, y = _y, z = _z, w = _w;
            fixed (byte* pbytes = buf)
            {
                uint* pbuf = (uint*) (pbytes + offset);
                uint* pend = (uint*) (pbytes + offsetEnd);
                while (pbuf < pend)
                {
                    uint tx = x ^ (x << 11);
                    uint ty = y ^ (y << 11);
                    uint tz = z ^ (z << 11);
                    uint tw = w ^ (w << 11);
                    *(pbuf++) = x = w ^ (w >> 19) ^ (tx ^ (tx >> 8));
                    *(pbuf++) = y = x ^ (x >> 19) ^ (ty ^ (ty >> 8));
                    *(pbuf++) = z = y ^ (y >> 19) ^ (tz ^ (tz >> 8));
                    *(pbuf++) = w = z ^ (z >> 19) ^ (tw ^ (tw >> 8));
                }
            }
            _x = x; _y = y; _z = z; _w = w;
        }
    }

    sealed class XorshiftUnrolled4_Slower1 : Xorshift
    {
        public override int FillBufferMultipleRequired { get { return 16; } }

        protected unsafe override void FillBuffer(byte[] buf, int offset, int offsetEnd)
        {
            uint x = _x, y = _y, z = _z, w = _w;
            fixed (byte* pbytes = buf)
            {
                uint* pbuf = (uint*) (pbytes + offset);
                uint* pend = (uint*) (pbytes + offsetEnd);
                while (pbuf < pend)
                {
                    uint tx = x ^ (x << 11); tx ^= tx >> 8;
                    uint ty = y ^ (y << 11); ty ^= ty >> 8;
                    uint tz = z ^ (z << 11); tz ^= tz >> 8;
                    uint tw = w ^ (w << 11); tw ^= tw >> 8;
                    *(pbuf++) = x = w ^ (w >> 19) ^ tx;
                    *(pbuf++) = y = x ^ (x >> 19) ^ ty;
                    *(pbuf++) = z = y ^ (y >> 19) ^ tz;
                    *(pbuf++) = w = z ^ (z >> 19) ^ tw;
                }
            }
            _x = x; _y = y; _z = z; _w = w;
        }
    }

    sealed class XorshiftUnrolled4_Slower2 : Xorshift
    {
        public override int FillBufferMultipleRequired { get { return 16; } }

        protected unsafe override void FillBuffer(byte[] buf, int offset, int offsetEnd)
        {
            uint x = _x, y = _y, z = _z, w = _w;
            fixed (byte* pbytes = buf)
            {
                uint* pbuf = (uint*) (pbytes + offset);
                uint* pend = (uint*) (pbytes + offsetEnd);
                while (pbuf < pend)
                {
                    uint tx = x ^ (x << 11);
                    uint ty = y ^ (y << 11);
                    uint tz = z ^ (z << 11);
                    uint tw = w ^ (w << 11);
                    *(pbuf) = x = w ^ (w >> 19) ^ (tx ^ (tx >> 8));
                    *(pbuf + 1) = y = x ^ (x >> 19) ^ (ty ^ (ty >> 8));
                    *(pbuf + 2) = z = y ^ (y >> 19) ^ (tz ^ (tz >> 8));
                    *(pbuf + 3) = w = z ^ (z >> 19) ^ (tw ^ (tw >> 8));
                    pbuf += 4;
                }
            }
            _x = x; _y = y; _z = z; _w = w;
        }
    }

    sealed class XorshiftUnrolled64 : Xorshift
    {
        private new ulong _x = 123456789;
        private new ulong _y = 362436069;
        private new ulong _z = 521288629;
        private new ulong _w = 88675123;

        
        public override int FillBufferMultipleRequired {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return 32; }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected unsafe override void FillBuffer(byte[] buf, int offset, int offsetEnd)
        {
            ulong x = _x, y = _y, z = _z, w = _w;
            fixed (byte* pbytes = buf)
            {
                ulong* pbuf = (ulong*) (pbytes + offset);
                ulong* pend = (ulong*) (pbytes + offsetEnd);
                while (pbuf < pend)
                {
                    ulong tx = x ^ (x << 11);
                    ulong ty = y ^ (y << 11);
                    ulong tz = z ^ (z << 11);
                    ulong tw = w ^ (w << 11);
                    *(pbuf++) = x = w ^ (w >> 19) ^ (tx ^ (tx >> 8));
                    *(pbuf++) = y = x ^ (x >> 19) ^ (ty ^ (ty >> 8));
                    *(pbuf++) = z = y ^ (y >> 19) ^ (tz ^ (tz >> 8));
                    *(pbuf++) = w = z ^ (z >> 19) ^ (tw ^ (tw >> 8));
                }
            }
            _x = x; _y = y; _z = z; _w = w;
        }

        public unsafe void FillBufferEx(ulong* pbytes, ulong* pend)
        {
            ulong x = _x, y = _y, z = _z, w = _w;            
            
            ulong* pbuf = pbytes;                
            while (pbuf < pend)
            {
                ulong tx = x ^ (x << 11);
                ulong ty = y ^ (y << 11);
                ulong tz = z ^ (z << 11);
                ulong tw = w ^ (w << 11);
                *(pbuf++) = x = w ^ (w >> 19) ^ (tx ^ (tx >> 8));
                *(pbuf++) = y = x ^ (x >> 19) ^ (ty ^ (ty >> 8));
                *(pbuf++) = z = y ^ (y >> 19) ^ (tz ^ (tz >> 8));
                *(pbuf++) = w = z ^ (z >> 19) ^ (tw ^ (tw >> 8));
            }
            
            _x = x;
            _y = y;
            _z = z;
            _w = w;
        }
    }
}
