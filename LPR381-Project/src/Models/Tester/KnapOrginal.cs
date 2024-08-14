using System;
using System.Collections.Generic;
using System.Xml.Schema;
using LPR381_Project.src.Models.Tester;

namespace LPR381_Project.src.Models.Tester
{
    internal class KnapOrginal
    {
        public List<Item> myItems = new List<Item>();
        public void KnapSackRound(float maxWeight)
        {

            GetItems();
            SortRatios();
            SubtractWeights(maxWeight);
            BranchKnap(maxWeight);
            foreach (Item item in myItems)
            {
                Console.WriteLine($"{item.Name}: {Math.Round(item.Ratio,2)} {item.Chosen} {item.Subtract}");
            }
        }

        public void GetItems()
        {
            Item itemOne = new Item("x1", 1, 2f, 11f, 1, 0f);
            Item itemTwo = new Item("x2", 1, 3f, 8f, 1, 0f);
            Item itemThree = new Item("x3", 1, 3f, 6f, 1, 0f);
            Item itemFour = new Item("x4", 1, 5f, 14f, 1, 0f);
            Item itemFive = new Item("x5", 1, 2f, 10f, 1, 0f);
            Item itemSix = new Item("x6", 1, 4f, 10f, 1, 0f);

            myItems.Add(itemOne);
            myItems.Add(itemTwo);
            myItems.Add(itemThree);
            myItems.Add(itemFour);
            myItems.Add(itemFive);
            myItems.Add(itemSix);
        }

        public void BranchOnInclude(string include)
        {
            for (int i = 0; i < myItems.Count; i++)
            {
                if(myItems[i].Name == include)
                {
                    myItems[i].Chosen = 1;
                }
            }
        }

        public void BranchOnExclude(string exclude)
        {
            for (int i = 0; i < myItems.Count; i++)
            {
                if (myItems[i].Name == exclude)
                {
                    myItems[i].Chosen = 0;
                }
            }
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

        public void BranchKnap(float maxWeight)
        {
            string variable = BranchOn();
            BranchOnInclude(variable);
            BranchOnExclude(variable);
            SubtractWeights(maxWeight);

        }

        public void SubtractWeights(float maxWeight)
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