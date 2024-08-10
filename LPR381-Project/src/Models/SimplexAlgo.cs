using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LPR381_Project.src.Models
{
    internal class SimplexAlgo
    {
        public void Simplex()
        {
            List<float> ratioList = new List<float>();
            List<float> test = new List<float>();

            float[,] data = {
                { -2f, -3f, -3f, -5f, -2f, -4f, 0f, 0f, 0f, 0f },
                { 11f, 8f, 6f, 14f, 10f, 10f, 1f, 0f, 0f, 40f },
                { 5f, 8f, 7f, 14f, 11f, 13f, 0f,1f, 0f, 20f },
                { 9f, 6f, 4f, 10f, 4f, 10f, 0f, 0f, 1f, 4f }
            };

            float[,] tableIteration = new float[data.GetLength(0), data.GetLength(1)];

            float minValue = data[0, 0];
            int pivotCol = 0;

            for (int j = 1; j < data.GetLength(1); j++)
            {
                if (data[0, j] < minValue)
                {
                    minValue = data[0, j];
                    pivotCol = j;
                }
            }

            Console.WriteLine($"Minimum value in row 0: {minValue}");
            Console.WriteLine($"Column index of minimum value: {pivotCol}");
            Console.WriteLine();

            int lastIndex = data.GetLength(1) - 1;

            for (int j = 1; j < data.GetLength(0); j++)
            {
                float ratio = (float)data[j, lastIndex] / data[j, pivotCol];
                ratioList.Add(ratio);
                Console.WriteLine($"Ratio test {j}: {ratio}");
            }

            float minPivot = ratioList[0];
            int pivotRow = 0;

            for (int j = 1; j < ratioList.Count; j++)
            {
                if (ratioList[j] < minPivot)
                {
                    minPivot = ratioList[j];
                    pivotRow = j;
                }
            }

            float crossSection = data[pivotRow + 1, pivotCol];

            for (int j = pivotRow + 1; j < data.GetLength(0); j++)
            {
                for (int k = 0; k < data.GetLength(1); k++)
                {
                    float result = data[j, k] / crossSection;
                    data[j,k] = result;
                    Console.WriteLine($"Row {j + 1}, Col {k}: {result}");
                }
            }


            for (int i = 0; i < pivotRow+1; i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    tableIteration[i,j] = data[i, j] - (data[i, pivotCol] * data[pivotRow+1,j]);
                    
                };
            }

            for (int i = pivotRow+1; i < data.GetLength(0); i++)
            {
                for (int j = 0;j < data.GetLength(1); j++)
                {
                    tableIteration[i, j] = data[i, j];
                }
            }

            for (int i = 0; i < tableIteration.GetLength(0); i++)
            {
                for (int j = 0; j < tableIteration.GetLength(1); j++)
                {
                    Console.Write(tableIteration[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
    }
}
