using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aplimat_core.utilities
{
    public class Utils
    {
        public static float ConstrainFloat(float val, float min, float max)
        {
            if (val <= min)
            {
                return min;
            }
            else if (val >= max)
            {
                return max;
            }
            else
            {
                return val;
            }
            
        }
    }
}
