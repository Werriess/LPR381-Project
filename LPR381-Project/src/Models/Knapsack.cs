using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPR_Project_381
{
    public class Knapsack
    {
        public static int SolveKnapsack(int weight, List<ObjVariables> items)
        {
            Queue<Table> queue = new Queue<Table>();
            List<Table> allTables = new List<Table>();  
            Table root = new Table(-1, 0, 0);  
            queue.Enqueue(root);
            allTables.Add(root); 
            int maxValue = 0;
            Table optimalTable = null; 

            while (queue.Count > 0)
            {
                Table currentTable = queue.Dequeue();

                if (currentTable.Level == items.Count - 1)
                    continue;

                // Branch: Include the next item
                Table nextTableAdd = new Table(currentTable.Level + 1, currentTable.Value, currentTable.Weight);
                nextTableAdd.Decisions.AddRange(currentTable.Decisions);
                nextTableAdd.Decisions.Add(1); 

                nextTableAdd.Weight += items[nextTableAdd.Level].Weight;
                nextTableAdd.Value += items[nextTableAdd.Level].Value;

                if (nextTableAdd.Weight <= weight)
                {
                    if (nextTableAdd.Value > maxValue)
                    {
                        maxValue = nextTableAdd.Value;
                        optimalTable = nextTableAdd;// Store the optimal table
                    }
                    queue.Enqueue(nextTableAdd);
                    allTables.Add(nextTableAdd);
                }

                // Branch: Exclude the next item
                Table nextTableIgnore = new Table(currentTable.Level + 1, currentTable.Value, currentTable.Weight);
                nextTableIgnore.Decisions.AddRange(currentTable.Decisions);
                nextTableIgnore.Decisions.Add(0);// Exclude item

                queue.Enqueue(nextTableIgnore);
                allTables.Add(nextTableIgnore);
            }

            // Display all tables
            Console.WriteLine("All Tables:");
            foreach (var table in allTables)
            {
                DisplayTable(table, items);
            }

            // Display the optimal solution
            Console.WriteLine("\nOptimal Solution:");
            Console.WriteLine($"Max Value: {maxValue}");

            if (optimalTable != null)
            {
                Console.WriteLine("\nOptimal Table:");
                DisplayTable(optimalTable, items);
            }

            Console.ReadKey();

            return maxValue;
        }

        private static void DisplayTable(Table node, List<ObjVariables> items)
        {
            Console.WriteLine($"Branch at level {node.Level}:");
            for (int i = 0; i < items.Count; i++)
            {
                string decision = i <= node.Level ? (node.Decisions[i] == 1 ? "1" : "0") : " ";
                Console.WriteLine($"x{i + 1} = {decision}");
            }
            Console.WriteLine($"Current Value: {node.Value}, Current Weight: {node.Weight}");
            Console.WriteLine("");
        }
    }
}
