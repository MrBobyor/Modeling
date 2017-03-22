using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modeling
{
    public class ConditionalFunc
    {
        public Queue<float> queue = new Queue<float>();
        public float sigma;
        public int num;
        public float[] val;
        Random rnd = new Random();

        public ConditionalFunc(float s, int n)
        {
            sigma = s;
            num = n;
            val = new float[num];
        }

        public float GetRandomValue()
        {
            return (float)rnd.NextDouble();
        }

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


        public void SetVal(float u)
        {
            if (u < 0 || u > 1)
                return;
            queue.Enqueue(reverseFunctionDisribution(u, sigma));
        }

        public void GetVal()
        {
            int i = 0;
            while (queue.Count != 0)
                {
                    val[i] = queue.Dequeue();
                    i++;
                }

            for (int k = num - 1; k >= 0; k--)
            {
                for (int j = 0; j < k; j++)
                {
                    if (val[j] > val[j + 1])
                    {
                        float tmp = val[j];
                        val[j] = val[j + 1];
                        val[j + 1] = tmp;
                    }
                }
            }
        }
    }
}
