﻿using System;
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
        float eps = 0.3f;
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
            sampleMean /= num;
            return sampleMean; 
        }

        public float SampleDispersion()
        {
            float x = SampleMean();
            float sampleDispersion = 0;
            for (int i = 0; i < num; i++)
                sampleDispersion += (val[i] - x)*(val[i] - x);
            sampleDispersion /= num;
            return sampleDispersion;
        }

        public float SampleNDispersion()
        {
            float x = SampleMean();
            float sampleDispersion = 0;
            for (int i = 0; i < num; i++)
                sampleDispersion += (val[i] - x) * (val[i] - x);
            sampleDispersion /= (num - 1);
            return sampleDispersion;
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

        //public float SampleMatExpectation()
        //{

        //    return 1.0f;
        //}

        public float GetMaxPeriod()
        {
            return this.val[num - 1] + eps;
        }

        public float GetMinPeriod()
        {
            if (this.val[0] - eps > 0)
                return this.val[0] - eps;
            else
                return 0;
        }

    }
}
