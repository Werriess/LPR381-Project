using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;


namespace LPR381_Project.src.Models
{
    internal class BranchAndBound
    {
        public void SolveBB(double[,] data, int xVar, int counter)
        {
            SimplexAlgo sl = new SimplexAlgo();
            sl.Simplex(GreaterThanBranch(data, 6));
            sl.Simplex(LessThanBranch(data, 6));
            
        }

        public List<int> FindNonInt(double[,] data)
        {
            List<int> nonInt = new List<int>();
            for (int i = 1; i < data.GetLength(0); i++)
            {
                if (data[i, data.GetLength(1)-1] != Math.Floor(data[i, data.GetLength(1) - 1]))
                {
                    nonInt.Add(i);
                }
            }
            return nonInt;
        }

        public int GetBasicVariables(double[,] arr, int xVar)
        {
            int bVar = 0;
            List<int> nonIntRows = FindNonInt(arr);

            for (int i = 0; i < arr.GetLength(1); i++)
            {
                bool isBasicVariable = true;
                int oneCount = 0;

                foreach (int rowIndex in nonIntRows)
                {
                    if (arr[rowIndex, i] == 1)
                    {
                        oneCount++;
                    }
                    else if (arr[rowIndex, i] != 0)
                    {
                        isBasicVariable = false;
                        break;
                    }
                }

                // Only add if there is one 1 and the rest zero's
                //Only add xVariables that are non ints
                if (isBasicVariable && oneCount == 1 && i <= xVar)
                {
                    bVar = i;
                }
            }
            return bVar;
        }

        public int GetBranchRow(double[,] data, int xVar)
        {
            int bvCol = GetBasicVariables(data, xVar);
            int bvRow = 0;
            for(int i = 0; i < data.GetLength(0); i++)
            {
                if (data[i, bvCol] == 1)
                {
                    bvRow = i;
                }
            }
            return bvRow;
        }

        private double[,] LessThanBranch(double[,] data, int xVar)
        {
            int originalRows = data.GetLength(0);
            int cols = data.GetLength(1);
            int newRows = originalRows + 1;

            Matrix<double> tester2 = Matrix<double>.Build.Dense(newRows, cols + 1);

            for(int i = 0; i < originalRows;i++)
            {
                for(int j = 0;j < cols ;j++)
                {
                    tester2[i, j] = data[i, j];
                }
            }

            int lastColIndex = tester2.ColumnCount - 1;
            int secondLastColIndex = tester2.ColumnCount - 2;
            var tempColumn = tester2.Column(secondLastColIndex).ToArray();
            tester2.SetColumn(secondLastColIndex, tester2.Column(lastColIndex));
            tester2.SetColumn(lastColIndex, Vector<double>.Build.Dense(tempColumn));

            int basicVColumn = GetBasicVariables(data, xVar);

            tester2[originalRows, basicVColumn] += 1;
            tester2[originalRows, secondLastColIndex] += 1;

            int rowToSubtractFrom = originalRows;
            int rowToSubtract = GetBranchRow(data, xVar);

            Vector<double> rowFrom = tester2.Row(rowToSubtract);
            Vector<double> rowSubtract = tester2.Row(rowToSubtractFrom);

            Vector<double> resultRow = (rowFrom - rowSubtract) * -1;

            tester2.SetRow(rowToSubtractFrom, resultRow);

            Console.WriteLine(tester2.ToString());
            double[,] ekWeetNieMeerNie = tester2.ToArray();
            return ekWeetNieMeerNie;
        }

        public double[,] GreaterThanBranch(double[,] data, int xVar)
        {
            int originalRows = data.GetLength(0);
            int cols = data.GetLength(1);
            int newRows = originalRows + 1;

            Matrix<double> tester2 = Matrix<double>.Build.Dense(newRows, cols + 1);

            for (int i = 0; i < originalRows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    tester2[i, j] = data[i, j];
                }
            }

            int lastColIndex = tester2.ColumnCount - 1;
            int secondLastColIndex = tester2.ColumnCount - 2;
            var tempColumn = tester2.Column(secondLastColIndex).ToArray();
            tester2.SetColumn(secondLastColIndex, tester2.Column(lastColIndex));
            tester2.SetColumn(lastColIndex, Vector<double>.Build.Dense(tempColumn));

            int basicVColumn = GetBasicVariables(data, xVar);

            tester2[originalRows, basicVColumn] += 1;
            tester2[originalRows, secondLastColIndex] -= 1;
            tester2[originalRows, lastColIndex] += 1;

            int rowToSubtractFrom = originalRows;
            int rowToSubtract = GetBranchRow(data, xVar); 

            Vector<double> rowFrom = tester2.Row(rowToSubtract);
            Vector<double> rowSubtract = tester2.Row(rowToSubtractFrom); 

            Vector<double> resultRow = rowFrom - rowSubtract;

            tester2.SetRow(rowToSubtractFrom, resultRow);

            Console.WriteLine(tester2.ToString());
            double[,] ekWeetNieMeerNie = tester2.ToArray();
            return ekWeetNieMeerNie;
        }

    }
}
