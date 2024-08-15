using System;
using System.Collections.Generic;
using System.Xml.Schema;
using LPR381_Project.src.Models.Tester;

namespace LPR381_Project.src.Models.Tester
{
    internal class KnapOrginal
    {
        public List<Item> myItems = new List<Item>();
        public void KnapSackRound(double maxWeight)
        {

            GetItems();
            SortRatios();
            BranchKnap(maxWeight);
            foreach (Item item in myItems)
            {
                Console.WriteLine($"{item.Name}: {Math.Round(item.Ratio,2)} {item.Chosen} {item.Subtract}");
            }
        }

        public void GetItems()
        {
            Item itemOne = new Item("x1", 1, 2, 11, 1, 0);
            Item itemTwo = new Item("x2", 1, 3, 8, 1, 0);
            Item itemThree = new Item("x3", 1, 3, 6, 1, 0);
            Item itemFour = new Item("x4", 1, 5, 14, 1, 0);
            Item itemFive = new Item("x5", 1, 2, 10, 1, 0);
            Item itemSix = new Item("x6", 1, 4, 10, 1, 0);

            myItems.Add(itemOne);
            myItems.Add(itemTwo);
            myItems.Add(itemThree);
            myItems.Add(itemFour);
            myItems.Add(itemFive);
            myItems.Add(itemSix);
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

        public void BranchKnap(double maxWeight)
        {
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