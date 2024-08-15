using System;
using MathNet.Numerics.LinearAlgebra;

namespace LPR381_Project.src.Models
{
    internal class CuttingPlane
    {
        public double[,] SolveCuttingPlane(double[,] data, int xVar)
        {
            BranchAndBound bb = new BranchAndBound();

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

            int basicVColumn = bb.GetBasicVariables(data, xVar);
            int rowToSubtractFrom = originalRows;
            int rowToSubtract = bb.GetBranchRow(data, xVar);

            Vector<double> rowFrom = tester2.Row(rowToSubtract);
            Vector<double> rowSubtract = tester2.Row(rowToSubtractFrom);

            Vector<double> resultRow = (rowFrom - rowSubtract).Map(myValue => -Mod(myValue, 1));

            tester2.SetRow(rowToSubtractFrom, resultRow);

            tester2[rowToSubtractFrom, secondLastColIndex] = 1;

            Console.WriteLine(tester2.ToString());
            double[,] ekWeetNieMeerNie = tester2.ToArray();
            return ekWeetNieMeerNie;
        }

        public double Mod(double value, double divisor)
        {
            double result = value % divisor;
            if(result < 0)
            {
                result = result + divisor;
            }

            return result;
        }
    }
}
