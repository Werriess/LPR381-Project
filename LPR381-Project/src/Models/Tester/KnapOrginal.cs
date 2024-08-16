using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Schema;
using LPR381_Project.src.Models.Tester;

namespace LPR381_Project.src.Models.Tester
{
    internal class KnapOrginal
    {
        public List<Item> myItems = new List<Item>();
        public void KnapSackRound(double maxWeight, int counter, int amountVar, double[,] data)
        {
            GetItems(data, amountVar);
            double[,] ds = MainStructure(amountVar);

            for (int i = 0; i < ds.GetLength(0); i++)
            {
                for (int j = 0; j < ds.GetLength(1); j++)
                {
                    Console.WriteLine(ds[i, j]);
                }
            }
            
         
            BranchKnap(maxWeight, counter);
            

           
            foreach (Item item in myItems)
            {
                Console.WriteLine($"{item.Name}: {Math.Round(item.Ratio,2)} {item.Chosen} {item.Subtract}");
            }
        }

        public double[,] MainStructure(int amountVar)
        {
            double[,] letSee = new double[3, amountVar];

            for (int j = 0; j < amountVar; j++)
            {
                if (j < myItems.Count)
                {
                    letSee[0, j] = myItems[j].Ratio;
                    letSee[1, j] = myItems[j].Chosen;
                    letSee[2, j] = myItems[j].Subtract;
                }
            }
            return letSee;
        }

        public void GetItems(double[,] data, int count)
        {
            for (int i = 0; i <= count; i++)
            {
                Item item = new Item($"x{i + 1}", 1, data[0, i], data[1, i], 1, 0);
                myItems.Add(item);
            }
        }

        public void BranchOnInclude(string include, double maxWeight)
        {
            for (int i = 0; i < myItems.Count; i++)
            {
                if(myItems[i].Name == include)
                {
                    myItems[i].Chosen = 1;
                }
            }
            SubtractWeights(maxWeight);
  
        }

        public void BranchOnExclude(string exclude, double maxWeight)
        {
            for (int i = 0; i < myItems.Count; i++)
            {
                if (myItems[i].Name == exclude)
                {
                    myItems[i].Chosen = 0;
                }
            }

            SubtractWeights(maxWeight);

        }

        public string BranchOn()
        {
            string branch = string.Empty;
            for (int i = 0; i < myItems.Count; i++)
            {
                if (myItems[i].Subtract < 0)
                {
                    branch = myItems[i].Name;
                    break;
                }
            }
            return branch;
        }

        public void BranchKnap(double maxWeight, int counter)
        {
            SortRatios();
            string variable = BranchOn();
            BranchOnInclude(variable, maxWeight);
            BranchOnExclude(variable, maxWeight);
            
        }

        public void SubtractWeights(double maxWeight)
        {
            for (int i = 0; i < myItems.Count; i++)
            {
                if (myItems[i].Chosen == 1)
                {
                    maxWeight -= myItems[i].Weight;
                    myItems[i].Subtract = maxWeight;
                }
            }
        }

        public void SortRatios()
        {
            myItems.Sort((x, y) => y.Ratio.CompareTo(x.Ratio));
        }
    }
}