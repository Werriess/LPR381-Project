using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPR381_Project.src.Models
{
    internal class SimplexAlgo
    {
        public void Simplex()
        {
            List<float> ratioList = new List<float>();

            float[,] data = {
                { -2, -3, -3, -5, -2, -4, 0, 0 },
                { 11, 8, 6, 14, 10, 10, 1, 40 },
                { 5, 8, 7, 14, 11, 13, 1, 20 },
                { 9, 6, 4, 10, 4, 10, 1, 4 }
            };

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
                    float result = (float)data[j, k] / crossSection;
                    Console.WriteLine($"Row {j + 1}, Col {k}: {result}");
                    data[j, k] = (int)result;
                }
            }

            for (int i = 0; i < pivotRow; i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    data[i, j] = data[i, j] - (data[i, pivotCol] * data[pivotRow,j]);


                };
            }

            for (int i = 0; i < data.GetLength(0); i++)
            {
                for(int j = 0;j < data.GetLength(1); j++)
                {
                    Console.Write(data[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
    }
}
