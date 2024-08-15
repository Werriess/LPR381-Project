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
            Console.WriteLine("Press 2 to Branch and Bound");
            Console.WriteLine("Press 3 to solve cutting plane");
            Console.WriteLine("Press 4 for Knapsack");
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
                        double[,] data = {
                                            { -2, -3, -3, -5, -2, -4, 0, 0, 0, 0, 0, 0, 0, 0 },
                                            { 11, 8, 6, 14, 10, 10, 1, 0, 0, 0, 0, 0, 0,40 },
                                            { 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1 },
                                            { 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 },
                                            { 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1 },
                                            { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1 },
                                            { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1 },
                                            { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 1 },
            };
                        test.Simplex(data);
                        menu = false;
                        break;

                    case 2:
                        Console.Clear();
                        SimplexAlgo dataT = new SimplexAlgo();
                        double[,] dat = {
                                            { -2, -3, -3, -5, -2, -4, 0, 0, 0, 0, 0, 0, 0, 0 },
                                            { 11, 8, 6, 14, 10, 10, 1, 0, 0, 0, 0, 0, 0,40 },
                                            { 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1 },
                                            { 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 },
                                            { 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1 },
                                            { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1 },
                                            { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1 },
                                            { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 1 },
            };

                        BranchAndBound b = new BranchAndBound();
                        b.SolveBB(dataT.BranchAndBound(dat), 6, 15);
                        menu = false;
                        break;

                    case 3:
                        Console.Clear();
                        CuttingPlane plane = new CuttingPlane();
                        SimplexAlgo sAlgo = new SimplexAlgo();
                        double[,] dataNew = {
                                            { -2, -3, -3, -5, -2, -4, 0, 0, 0, 0, 0, 0, 0, 0 },
                                            { 11, 8, 6, 14, 10, 10, 1, 0, 0, 0, 0, 0, 0,40 },
                                            { 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1 },
                                            { 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 },
                                            { 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1 },
                                            { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1 },
                                            { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1 },
                                            { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 1 },
            };
                        double[,] cuttingPlaneResult = plane.SolveCuttingPlane(sAlgo.BranchAndBound(dataNew), 6);
                        sAlgo.Simplex(cuttingPlaneResult);
                        menu = false;
                        break;

                    case 4:
                        Console.Clear();
                        KnapOrginal kp = new KnapOrginal();
                        kp.KnapSackRound(40);
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
