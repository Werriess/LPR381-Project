using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LPR381_Project.src.Utils
{
    internal class FileParser
    {
        public List<string> data = new List<string>();
        public List<string[]> splitData = new List<string[]>();
        public int[,] tableu;

        public void ReadFile()
        {
            try
            {
                using (StreamReader reader = new StreamReader("Test.txt"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        data.Add(line);
                    }
                }

                string[] separator = { "max", "+", "<=" };

                for (int i = 0; i < data.Count; i++)
                {
                    string[] splitLine = data[i].Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    splitData.Add(splitLine);
                }

                int rows = splitData.Count;
                int cols = splitData.Max(row => row.Length);
                tableu = new int[rows, cols];


                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < splitData[i].Length; j++)
                    {
                        if (int.TryParse(splitData[i][j], out int value))
                        {
                            tableu[i, j] = value;
                        }
                        else
                        {
                            tableu[i, j] = 0;
                        }
                    }
                }

                Console.WriteLine("Tableu Array:");
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        Console.Write(tableu[i, j] + " ");
                    }
                    Console.WriteLine();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}
