﻿using System;
using System.Collections.Generic;

namespace LPR381_Project.src.Models
{
    internal class SimplexAlgo
    {
        public void Simplex(double[,] data, bool maximize)
        {
            double[,] nextTab = new double[data.GetLength(0), data.GetLength(1)];
            bool dualSimplex = LeastNegativeInRhs(data);
            bool maxProblem = maximize;

            PrintTableau(data);

            while (true)
            {
                if (dualSimplex)
                {
                    if (!LeastNegativeInRhs(data))
                    {
                        dualSimplex = false;
                        continue;
                    }

                    bool optimalCondition = maxProblem ? MostNegativeInObj(data) : MostPositiveInObj(data);

                    if (optimalCondition)
                    {
                        Console.WriteLine("Dual: Optimal solution reached.");
                        break;
                    }

                    int pivotRow = GetDualPivotRow(data);
                    int pivotCol = GetDualPivotCol(data, pivotRow);

                    if (pivotRow == -1 || pivotCol == -1)
                    {
                        Console.WriteLine("No valid pivot element found in Dual Simplex.");
                        break;
                    }

                    NormalizeTable(data, nextTab, pivotRow, pivotCol);
                }
                else
                {
                    bool optimal = maxProblem ? !MostNegativeInObj(data) : !MostPositiveInObj(data);
                    if (optimal)
                    {
                        Console.WriteLine("Optimal solution reached.");
                        break;
                    }

                    // Select pivot column based on problem type
                    int pivotCol = maxProblem ? GetPivotColMax(data) : GetPivotColMin(data);
                    int pivotRow = GetPivotRow(data, pivotCol);

                    if (pivotRow == -1 || pivotCol == -1)
                    {
                        Console.WriteLine("No valid pivot element found in Primal Simplex.");
                        break;
                    }

                    NormalizeTable(data, nextTab, pivotRow, pivotCol);
                }
                PrintTableau(nextTab);
                Array.Copy(nextTab, data, nextTab.Length);
            }
        }

        private bool MostNegativeInObj(double[,] data)
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

        private bool LeastNegativeInRhs(double[,] data)
        {
            for (int i = 1; i < data.GetLength(0); i++)
            {
                if (data[i, data.GetLength(1) - 1] < 0)
                {
                    return true;
                }
            }
            return false;
        }

        private bool MostPositiveInObj(double[,] data)
        {
            for (int i = 0; i < data.GetLength(1) - 1; i++)
            {
                if (data[0, i] > 0)
                {
                    return true;
                }
            }
            return false;
        }


        private int GetDualPivotRow(double[,] data)
        {
            //Get largest negative value in RHS
            int lastCol = data.GetLength(1) - 1;
            int pivotRow = -1;
            double mostNegativeValue = 0;

            for (int i = 1; i < data.GetLength(0); i++)
            {
                if (data[i, lastCol] < mostNegativeValue)
                {
                    mostNegativeValue = data[i, lastCol];
                    pivotRow = i;
                }
            }

            return pivotRow;
        }

        private int GetDualPivotCol(double[,] data, int pivotRow)
        {
            /*Get the pivot col by dividing the corresponding obj row with the negative values in the pivot row. Using the absolute value, 
             * we choose the number closesest to zer0
            */
            double smallestRatio = float.MaxValue;
            int pivotCol = -1;

            for (int i = 0; i < data.GetLength(1) - 1; i++)
            {
                if (data[pivotRow, i] < 0)
                {
                    double ratio = Math.Abs(data[0, i] / data[pivotRow, i]);

                    if (ratio < smallestRatio)
                    {
                        smallestRatio = ratio;
                        pivotCol = i;
                    }
                }
            }

            return pivotCol;
        }

        private int GetPivotColMax(double[,] data)
        {
            //For a max problem, get the biggest negative value in obj row
            double minValue = data[0, 0];
            int pivotCol = 0;

            for (int j = 1; j < data.GetLength(1) - 1; j++)
            {
                if (data[0, j] < minValue)
                {
                    minValue = data[0, j];
                    pivotCol = j;
                }
            }

            return pivotCol;
        }

        private int GetPivotColMin(double[,] data)
        {
            //For a max problem, get the biggest positive value in obj row
            double maxValue = data[0, 0];
            int pivotCol = 0;

            for (int j = 1; j < data.GetLength(1) - 1; j++)
            {
                if (data[0, j] > maxValue)
                {
                    maxValue = data[0, j];
                    pivotCol = j;
                }
            }

            return pivotCol;
        }

        private int GetPivotRow(double[,] data, int pivotCol)
        {
            //Here we ratio test by dividing the RHS values with the pivot col in order to get the pivot row.
            //The most positive value closest to zero is chosen
            List<double> ratioList = new List<double>();
            int lastIndex = data.GetLength(1) - 1;

            for (int j = 1; j < data.GetLength(0); j++)
            {
                if (data[j, pivotCol] > 0)
                {
                    double ratio = data[j, lastIndex] / data[j, pivotCol];
                    ratioList.Add(ratio);
                }
                else
                {
                    ratioList.Add(double.MaxValue);
                }
            }

            double minPivot = ratioList[0];
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

        private void NormalizeTable(double[,] data, double[,] nextTab, int pivotRow, int pivotCol)
        {
            double crossSection = data[pivotRow, pivotCol];
            //Here we use the crosssection to firstly normalize the pivotrow by dividing the crossection value in the old table with the new values in new table
            for (int j = 0; j < data.GetLength(1); j++)
            {
                nextTab[pivotRow, j] = data[pivotRow, j] / crossSection;
            }
            /*Normalize the rest of the table by subtracting each value in the old table except the pivot row values, with the product of the the value in the pivot col with the correspoding value in
             * pivot row in the new table
             */
            for (int i = 0; i < data.GetLength(0); i++)
            {
                if (i != pivotRow)
                {
                    for (int j = 0; j < data.GetLength(1); j++)
                    {
                        nextTab[i, j] = data[i, j] - (data[i, pivotCol] * nextTab[pivotRow, j]);
                    }
                }
            }
        }

        private void PrintTableau(double[,] nextTab)
        {
            for (int i = 0; i < nextTab.GetLength(0); i++)
            {
                for (int j = 0; j < nextTab.GetLength(1); j++)
                {
                    Console.Write("{0,8:F3}\t", nextTab[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public double[,] BranchAndBound(double[,] data, bool maximization)
        {
            double[,] nextTab = new double[data.GetLength(0), data.GetLength(1)];
            bool dualSimplex = true;
            bool maxProblem = maximization;

            while (true)
            {
                if (dualSimplex)
                {
                    if (!LeastNegativeInRhs(data))
                    {
                        dualSimplex = false;
                        continue;
                    }

                    bool optimalCondition = maxProblem ? MostNegativeInObj(data) : MostPositiveInObj(data);

                    if (optimalCondition)
                    {
                        break;
                    }

                    int pivotRow = GetDualPivotRow(data);
                    int pivotCol = GetDualPivotCol(data, pivotRow);

                    if (pivotRow == -1 || pivotCol == -1)
                    {
                        Console.WriteLine("No valid pivot element found in Dual");
                        break;
                    }

                    NormalizeTable(data, nextTab, pivotRow, pivotCol);
                }
                else
                {
                    bool optimal = maxProblem ? !MostNegativeInObj(data) : !MostPositiveInObj(data);
                    if (optimal)
                    {
                        break;
                    }

                    // Select pivot column based on problem type
                    int pivotCol = maxProblem ? GetPivotColMax(data) : GetPivotColMin(data);
                    int pivotRow = GetPivotRow(data, pivotCol);

                    if (pivotRow == -1 || pivotCol == -1)
                    {
                        Console.WriteLine("No valid pivot element found in Primal Simplex.");
                        break;
                    }

                    NormalizeTable(data, nextTab, pivotRow, pivotCol);
                }
                PrintTableau(nextTab);
                Array.Copy(nextTab, data, nextTab.Length);
            }
            return data;
        }
    }
}
