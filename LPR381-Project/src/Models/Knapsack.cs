using System;
using System.Collections.Generic;

namespace LPR381_Project.src.Models
{
    internal class Knapsack
    {
        public void KnapsackTest()
        {
            float[,] data = {
                { 2f, 3f, 3f, 5f, 2f, 4f },
                { 11f, 8f, 6f, 14f, 10f, 10f },
            };

            float[,] sack = new float[2, data.GetLength(1)];

            int maxWeight = 40;
            List<float> ratioTest = new List<float>();

            for (int j = 0; j < data.GetLength(1); j++)
            {
                float result = data[0, j] / data[1, j];
                ratioTest.Add(result);
                sack[1,j] = result;
            }

            for (int i = 0; i < data.GetLength(1); i++)
            {
                sack[0, i] = i+1;
            }

            for (int j = 0; j < sack.GetLength(0); j++)
            {
                for (int k = 0; k < sack.GetLength(1); k++)
                {
                    Console.WriteLine(sack[j,k]);
                }
            }
            
        }
    }
}
