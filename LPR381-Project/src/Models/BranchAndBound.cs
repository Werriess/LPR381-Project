using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;


namespace LPR381_Project.src.Models
{
    internal class BranchAndBound
    {
        public void SolveBB(double[,] data)
        {
            LessThanBranch();
            GreaterThanBranch();
            
            List<int> ints = FindNonInt(data);

            foreach(int i in ints)
            {
                Console.WriteLine(i);
            }

            List<int>bv = GetBasicVariables(data);
            foreach(int i in bv)
            {
                Console.WriteLine(i);
            }

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

        public List<int> GetBasicVariables(double[,] arr)
        {
            List<int> bVariables = new List<int>();
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
                if (isBasicVariable && oneCount == 1)
                {
                    bVariables.Add(i);
                }
            }
            return bVariables;
        }



        public void LessThanBranch()
        {
            SimplexAlgo s = new SimplexAlgo();
            double[,] data = s.BranchAndBound();

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

            tester2[originalRows, 4] += 1;
            tester2[originalRows, secondLastColIndex] += 1;
            Console.WriteLine(tester2.ToString());

            int rowToSubtractFrom = originalRows;
            int rowToSubtract = 1;

            Vector<double> rowFrom = tester2.Row(rowToSubtract);
            Vector<double> rowSubtract = tester2.Row(rowToSubtractFrom);

            Vector<double> resultRow = (rowFrom - rowSubtract) * -1;

            tester2.SetRow(rowToSubtractFrom, resultRow);

            Console.WriteLine(tester2.ToString());

                
        }

        public void GreaterThanBranch()
        {
            SimplexAlgo s = new SimplexAlgo();
            double[,] data = s.BranchAndBound();

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

            Console.WriteLine(tester2.ToString());

            tester2[originalRows, 4] += 1;
            tester2[originalRows, secondLastColIndex] -= 1;
            tester2[originalRows, lastColIndex] += 1;

            Console.WriteLine(tester2.ToString());

            int rowToSubtractFrom = originalRows;
            int rowToSubtract = 1;

            Vector<double> rowFrom = tester2.Row(rowToSubtract);
            Vector<double> rowSubtract = tester2.Row(rowToSubtractFrom); 

            Vector<double> resultRow = rowFrom - rowSubtract;

            tester2.SetRow(rowToSubtractFrom, resultRow);

            Console.WriteLine(tester2.ToString());
        }

    }
}
