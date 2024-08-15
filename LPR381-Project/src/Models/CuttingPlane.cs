using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPR381_Project.src.Models
{
    internal class CuttingPlane
    {
        public void SolveCuttingPlane()
        {
            SimplexAlgo s = new SimplexAlgo();
            double[,] data = s.BranchAndBound();

            int originalRows = data.GetLength(0);
            int cols = data.GetLength(1);
            int newRows = originalRows + 1;

            double[,] newArray = new double[newRows, cols];

            for (int i = 0; i < originalRows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    newArray[i, j] = data[i, j];
                }
            }

            for (int i = 0; i < newArray.GetLength(1); i++)
            {
                newArray[newRows-1,i] = -data[1, i] % 1;
            }

            for(int i = 0; i < newArray.GetLength(0); i++)
            {
                for(int j = 0; j < newArray.GetLength(1); j++)
                {
                    Console.Write(Math.Round(newArray[i, j],3) + "\t");
                }
                Console.WriteLine();
            }
        }
    }
}
