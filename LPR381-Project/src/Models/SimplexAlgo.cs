using System;
using System.Collections.Generic;

namespace LPR381_Project.src.Models
{
    internal class SimplexAlgo
    {
        public void Simplex()
        {
            float[,] data = {
                { -2f, -3f, -3f, -5f, -2f, -4f, 0f, 0f, 0f, 0f },
                { 11f, 8f, 6f, 14f, 10f, 10f, 1f, 0f, 0f, 40f },
                { 5f, 8f, 7f, 14f, 11f, 13f, 0f,1f, 0f, 20f },
                { 9f, 6f, 4f, 10f, 4f, 10f, 0f, 0f, 1f, 4f }
            };

            float[,] nextTab = new float[data.GetLength(0), data.GetLength(1)];


            while (true)
            {
                if (!MostNegativeInObj(data))
                {
                    Console.WriteLine("Optimal solution reached.");
                    break;
                }

                if(!LeastNegativeInRhs(data))
                {
                    Console.WriteLine("Infeasible");
                }

                GetDualPivotRow(data);

                int pivotCol = GetPivotCol(data);
                int pivotRow = GetPivotRow(data, pivotCol);

                NormalizeTable(data, nextTab, pivotRow, pivotCol);

                PrintTableau(nextTab);

                Array.Copy(nextTab, data, nextTab.Length);
            }
        }

        private bool MostNegativeInObj(float[,] data)
        {
            for (int i = 0; i < data.GetLength(1) - 1; i++)
            {
                if (data[0, i] < 0)
                {
                    return true;
                }
            }
            return false;
        }

        private bool LeastNegativeInRhs(float[,] data)
        {
            for(int i = 0; i < data.GetLength(0); i++)
            {
                if (data[i,data.GetLength(1) -1] < 0)
                {
                    return true;
                }
            }
            return false; 
        }

        private int GetDualPivotRow(float[,] data)
        {
            int lastCol= data.GetLength(1) -1; 
            int pivotRow = -1;
            float notNull = 0;

            for (int i = 1; i < data.GetLength(0); i++)
            {
                if (data[i, lastCol] < notNull)
                {
                    notNull = data[i, lastCol];
                    pivotRow = i;
                }
            }

            return pivotRow;
        }


        private int GetPivotCol(float[,] data)
        {
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

            return pivotCol;
        }

        private int GetPivotRow(float[,] data, int pivotCol)
        {
            List<float> ratioList = new List<float>();
            int lastIndex = data.GetLength(1) - 1;

            for (int j = 1; j < data.GetLength(0); j++)
            {
                if (data[j, pivotCol] > 0)
                {
                    float ratio = data[j, lastIndex] / data[j, pivotCol];
                    ratioList.Add(ratio);
                }
                else
                {
                    ratioList.Add(float.MaxValue);
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

            return pivotRow + 1;
        }

        private void NormalizeTable(float[,] data, float[,] tableIteration, int pivotRow, int pivotCol)
        {
            float crossSection = data[pivotRow, pivotCol];

            // Normalize the pivot row
            for (int j = 0; j < data.GetLength(1); j++)
            {
                data[pivotRow, j] /= crossSection;
            }

            for (int i = 0; i < data.GetLength(0); i++)
            {
                if (i != pivotRow)
                {
                    for (int j = 0; j < data.GetLength(1); j++)
                    {
                        tableIteration[i, j] = data[i, j] - (data[i, pivotCol] * data[pivotRow, j]);
                    }
                }
            }

            for (int j = 0; j < data.GetLength(1); j++)
            {
                tableIteration[pivotRow, j] = data[pivotRow, j];
            }
        }

        private void PrintTableau(float[,] nextTab)
        {
            for (int i = 0; i < nextTab.GetLength(0); i++)
            {
                for (int j = 0; j < nextTab.GetLength(1); j++)
                {
                    Console.Write(Math.Round(nextTab[i, j], 2) + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
