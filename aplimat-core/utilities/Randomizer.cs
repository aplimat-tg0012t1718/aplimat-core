using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aplimat_labs.Utilities
{
    public class Randomizer
    {
        private double min, max;
        private Random random;

        public Randomizer(int _min, int _max)
        {
            this.min = _min;
            this.max = _max + 1;

            random = new Random();
        }
        public double GenerateDouble()
        {
            return random.NextDouble() * (min - max) + min;
        }



        public int Generate()
        {
            return (int)random.Next((int)min, (int)max);
        }
    }
}
