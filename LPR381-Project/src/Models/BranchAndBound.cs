using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPR381_Project.src.Models
{
    internal class BranchAndBound
    {
        public void SolveBB(float[,] data)
        {
            SimplexAlgo simplex = new SimplexAlgo();
            data = simplex.BranchAndBound();

            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    Console.Write(Math.Round(data[i, j],3) + "\t");
                }
                Console.WriteLine();
            }

            FindNonInt(data);
            List<int> tester = GetBasicVariables(data);
            foreach(int t in tester)
            {
                Console.WriteLine(t);
            }
        }

        public List<int> FindNonInt(float[,] data)
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

        public List<int> GetBasicVariables(float[,] arr)
        {
            List<int> basicVariables = new List<int>();
            List<int> nI = FindNonInt(arr);
            int countOnes = 0;

            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i,j] == 1)
                    {
                        countOnes++;
                    }
                }
            }
            return basicVariables;
        }

    }
}
