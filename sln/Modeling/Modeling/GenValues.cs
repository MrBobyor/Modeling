using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modeling
{
    public class GenValues : ConditionalFunc
    {
        public Queue<float> queue = new Queue<float>();
        public float sigma;
        public int num;
        public float[] val;
        Random rnd = new Random();

        public float GetRandomValue()
        {
            return (float)rnd.NextDouble();
        }

        public GenValues(float s, int n)
        {
            sigma = s;
            num = n;
            val = new float[num];
        }

        public void GenVal(float u)
        {
            if (u < 0 || u > 1)
                return;
            queue.Enqueue(reverseFunctionDisribution2(u, sigma));
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
                for (int j = 0; j < k; j++)
                    if (val[j] > val[j + 1])
                    {
                        float tmp = val[j];
                        val[j] = val[j + 1];
                        val[j + 1] = tmp;
                    }
        }

        public float SampleMean()
        {
            float sampleMean = 0;
            for(int i = 0; i < num; i++)
                sampleMean += val[i];
            return (1/num) * sampleMean; 
        }

        public float SampleDispersion()
        {
            float x = SampleMean();
            float sampleDispersion = 0;
            for (int i = 0; i < num; i++)
                sampleDispersion += (float)(Math.Pow((val[i] - x), 2));
            return (1 / num) * sampleDispersion;
        }

        public float SampleScope()
        {
            float sampleScope = 0;
            sampleScope = val[num - 1] - val[0];
            return sampleScope;
        }

        public float SampleMedian()
        {
            //если одно значение?
            float sampleMedian = 0;
            if (num % 2 != 0)
                sampleMedian = val[num / 2];
            else
                sampleMedian = (val[(num - 1) / 2] + val[(num - 3) / 2]) / 2;
            return sampleMedian;
        }
    }
}
