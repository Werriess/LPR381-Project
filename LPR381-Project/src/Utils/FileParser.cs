using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LPR381_Project.src.Utils
{
    internal class FileParser
    {
        public List<string> data = new List<string>();
        public void ReadFile()
        {
            try
            {
                StreamReader reader = new StreamReader("Test.txt");
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    data.Add(line);
                }

                string[,] problem = { { data[0] }, { data[1] } };

                for (int i = 0; i < problem.GetLength(0); i++)
                {
                    for (int j = 0; j < problem.GetLength(1); j++)
                    {
                        Console.WriteLine(problem[i, j]);
                    }
                }
                reader.Close();                 
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

    }
}
