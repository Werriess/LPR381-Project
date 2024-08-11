using System;
using System.Collections.Generic;
using System.Security.Authentication;

namespace LPR381_Project.src.Models
{
    internal class SimplexAlgo
    {
        public void Simplex()
        {
            List<float> ratioList = new List<float>();

            bool run = true;
            bool negativeInObjective = true;

            float[,] data = {
                { -2f, -3f, -3f, -5f, -2f, -4f, 0f, 0f, 0f, 0f },
                { 11f, 8f, 6f, 14f, 10f, 10f, 1f, 0f, 0f, 40f },
                { 5f, 8f, 7f, 14f, 11f, 13f, 0f,1f, 0f, 20f },
                { 9f, 6f, 4f, 10f, 4f, 10f, 0f, 0f, 1f, 4f }
            };

            float[,] tableIteration = new float[data.GetLength(0), data.GetLength(1)];

            while (run)
            {
                // Checking if there are negative values in the objective function row
                negativeInObjective = false;
                for (int i = 0; i < data.GetLength(1) - 1; i++)
                {
                    if (data[0, i] < 0)
                    {
                        negativeInObjective = true;
                        break;
                    }
                }

                if (!negativeInObjective)
                {
                    Console.WriteLine("Optimal solution reached.");
                    break;
                }

                ratioList.Clear();

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

                int lastIndex = data.GetLength(1) - 1;

                for (int j = 1; j < data.GetLength(0); j++)
                {
                    if (data[j, pivotCol] > 0) // Avoiding division by zero
                    {
                        float ratio = (float)data[j, lastIndex] / data[j, pivotCol];
                        ratioList.Add(ratio);
                    }
                    else
                    {
                        ratioList.Add(float.MaxValue); // Infeasibility
                    }
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
                        data[j, k] = result;
                    }
                }

                for (int i = 0; i < pivotRow + 1; i++)
                {
                    for (int j = 0; j < data.GetLength(1); j++)
                    {
                        tableIteration[i, j] = data[i, j] - (data[i, pivotCol] * data[pivotRow + 1, j]);
                    }
                }

                for (int i = pivotRow + 1; i < data.GetLength(0); i++)
                {
                    for (int j = 0; j < data.GetLength(1); j++)
                    {
                        tableIteration[i, j] = data[i, j];
                    }
                }

                for (int i = 0; i < tableIteration.GetLength(0); i++)
                {
                    for (int j = 0; j < tableIteration.GetLength(1); j++)
                    {
                        Console.Write(Math.Round(tableIteration[i, j],2) + "\t");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();

                Array.Copy(tableIteration, data, tableIteration.Length);
            }
        }
    }
}
