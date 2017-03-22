using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modeling
{
    public class ConditionalFunc
    {
        public float functionDisribution(float y, float sigma)
        {
            if (y < 0)
                return 0;
            return (float)(y * Math.Exp(-Math.Pow(y, 2) / 2 * Math.Pow(sigma, 2)) / Math.Pow(sigma, 2));
        }

        public float reverseFunctionDisribution(float functionDisributionValue, float sigma)
        {
            return (float)Math.Sqrt(-2 * Math.Pow(sigma, 2) * Math.Log(1 - functionDisributionValue));
        }
    }
}
