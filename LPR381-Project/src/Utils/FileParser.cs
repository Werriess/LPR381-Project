using System;
using System.Collections.Generic;
using System.IO;

namespace LPR381_Project.src.Utils
{
    internal class FileParser
    {
        public List<string> data = new List<string>();
        public List<string[]> splitData = new List<string[]>();
        public bool maximise = false;
        public Dictionary<int, int> slack = new Dictionary<int, int>();
        public float[,] tableau;

        public void ReadFile()
        {
            try
            {
                using (StreamReader reader = new StreamReader("Test.txt"))
                {
                    string line;
                    int lineIndex = 0;
                    while ((line = reader.ReadLine()) != null)
                    {
                        data.Add(line);
                        if (line.Contains("max"))
                        {
                            maximise = true;
                        }
                        if (line.Contains("<="))
                        {
                            slack[lineIndex] = 1; 
                        }
                        lineIndex++;
                    }
                }

                foreach (var s in slack)
                {
                    int index = s.Key;
                    data[index] = data[index].Replace("<=", "+1 <=");
                }

                string[] separator = { "+", "<=", "max" };

                for (int i = 0; i < data.Count; i++)
                {
                    string[] splitLine = data[i].Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    splitData.Add(splitLine);
                }

                Console.WriteLine(data[0]);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}
