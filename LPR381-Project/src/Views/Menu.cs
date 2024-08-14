using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LPR381_Project.src.Utils;
using LPR381_Project.src.Models;
using LPR381_Project.src.Models.Tester;

namespace LPR381_Project
{
    internal class Menu
    {
        public void Run()
        {
            Console.WriteLine("---------------------------\n" +
                "Welcome to the our ALGO app !\n" +
                "---------------------------"
                );

            Console.WriteLine("Press 1 for the simplex algorithm");
            Console.WriteLine("Press 2 for Knapsack");
            Console.WriteLine("Press 0 to exit");

            int input = int.Parse(Console.ReadLine());

            bool menu = true;

            while (menu)
            {
                switch (input)
                {
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Lets go!");
                        FileParser reader = new FileParser("Test.txt");
                        reader.ReadFile();
                        menu = false;
                        break;

                    case 1:
                        Console.Clear();
                        SimplexAlgo test = new SimplexAlgo();
                        test.Simplex();
                        menu = false;
                        break;

                    case 2:
                        Console.Clear();
                        KnapOrginal knapOriginal = new KnapOrginal();
                        knapOriginal.KnapSackRound(40);
                        menu = false;
                    break;

                    default:
                        Console.WriteLine("Something went wrong");
                    break;
                }
            }

        }
    }
}
