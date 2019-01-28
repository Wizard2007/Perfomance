using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Perfomance.RandomIterators;

namespace Perfomance
{
    public class RandomIterator : IRandomIterator
    {
        private int[] values;
        private int length;
        private int currentIndex = -1;
        private int lower;
        private int upper;
        private Random rnd = new Random();
        private void InitValues()
        {
            for(int i = 0; i< length; i++)
            {
                values[i] = rnd.Next(lower, upper);
            }
        }
        public RandomIterator(int capasity, int lower, int upper)
        {
            length = capasity;
            values = new int[length];
            this.lower = lower;
            this.upper = upper;
            InitValues();
        }
        public int Next()
        {
            currentIndex++;
            if (currentIndex>=length)
            {
                currentIndex = 0;
                Reset();
            }            
            return values[currentIndex];
        }

        public void Reset()
        {
            int shift = rnd.Next(lower, upper);
            for (int i = 0; i < length; i++)
            {
                values[i] = (values[i] + shift) >> 1;
            }
        }

        public byte bNext()
        {
            throw new NotImplementedException();
        }
    }
}
