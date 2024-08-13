using System;
using System.Collections.Generic;
using System.IO;

namespace LPR381_Project.src.Utils
{
    internal class FileParser
    {
        public bool maximize;
        public bool minimize;
        public List<string> myLines;
        public string filePath;

        public FileParser(string filePath)
        {
            this.filePath = filePath;
            this.myLines = new List<string>();
        }


        public void ReadFile()
        {
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        myLines.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}
