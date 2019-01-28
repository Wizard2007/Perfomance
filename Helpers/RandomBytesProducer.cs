using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Perfomance.Helpers
{
    public static class RandomBytesProducer
    {
        public static void InitBytes()
        {            
            const int size = 256*256;
            var stringList = new string[size];
            byteListSizes = new byte[size];
            for (int i = 0; i < size; i++)
            {
                stringList[i] = (i+1).ToString();
                byteListSizes[i] = (byte)stringList[i].Length;
            }
            byteList = DataProducer.ConvetStringListToBytes(stringList);
        }
        
        public static byte[][] byteList { get; set; }
        public static byte[] byteListSizes { get; set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] GetByte(int index)
        {
            return byteList[index];
        }
    }
}
