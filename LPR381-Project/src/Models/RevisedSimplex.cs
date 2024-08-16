using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace LPR381_Project.src.Models
{
    internal class RevisedSimplex
    {
        private List<double[]> VariableCol;
        private List<double[]> ObjRow;
        private List<double[]> VariableRow;
        private List<double[]> RightSide;
        private int VarCount;

        public RevisedSimplex(int varCount, List<double[]> variableCol, List<double[]> objRow, List<double[]> variableRow, List<double[]> rightSide)
        {
            this.VariableCol = variableCol;
            this.ObjRow = objRow;
            this.VariableRow = variableRow;
            this.RightSide = rightSide;
            this.VarCount = varCount;
        }

        public void SolveRS()
        {
            double[,] data =
            {
                { 3, 2, 0, 0, 0, 0 },
                { 2, 1, 1, 0, 0, 100 },
                { 1, 1, 0, 1, 0, 80 },
                { 1, 0, 0, 0, 1, 40 }
            };

            Matrix<double> matrix = Matrix<double>.Build.DenseOfArray(data);

            List<double[]> vCol = new List<double[]>();

            for (int col = 0; col < data.GetLength(1); col++)
            {
                double[] columnData = new double[data.GetLength(0)];

                for (int row = 0; row < data.GetLength(0); row++)
                {
                    columnData[row] = data[row, col];
                }

                vCol.Add(columnData);
            }

            foreach (var colArray in vCol)
            {
                foreach (var value in colArray)
                {
                    Console.Write(value + " ");
                }
                Console.WriteLine();  
            }
        }
    }
}
