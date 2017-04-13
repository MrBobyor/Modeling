using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modeling
{
    public class ConditionalFunc
    {
        // for experiment 2
        public float functionDisribution2(float y, float sigma)
        {
            if (y < 0)
                return 0;
            //return (float)(y * Math.Exp(-Math.Pow(y, 2) / 2 * Math.Pow(sigma, 2)) / Math.Pow(sigma, 2));
            return (float)(1 - Math.Exp(- Math.Pow(y, 2) / (2 * Math.Pow(sigma, 2))));
        }

        public float reverseFunctionDisribution2(float functionDisributionValue2, float sigma)
        {
            //return (float)Math.Sqrt(-2 * Math.Pow(sigma, 2) * Math.Log(1 - functionDisributionValue2));
            return (float)(Math.Sqrt(2 * Math.Pow(sigma, 2) * Math.Log(1 / (1 - functionDisributionValue2))));
        }
    }
}
