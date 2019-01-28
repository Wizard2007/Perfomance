using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perfomance.RandomIterators
{
    //https://stackoverflow.com/questions/1790776/fast-random-generator
    //http://roman.st/Article/Faster-Marsaglia-Xorshift-pseudo-random-generator-in-unsafe-C
    //http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/emt.html

    //http://roman.st/Article/Faster-Marsaglia-Xorshift-pseudo-random-generator-in-unsafe-C
    public interface IRandomIterator
    {
        int Next();
        byte bNext();
        void Reset();
    }
}
