using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perfomance.Helpers
{
    public static class DataProducer
    {
        public static byte[][] ConvetStringListToBytes(string[] list)
        {
            int n = list.Length;
            byte[][] result = new byte[n][];
            for(int i = 0; i < n; ++i)
            {
                result[i] = Encoding.ASCII.GetBytes(list[i]);
            }
            return result;
        }
    }
}
