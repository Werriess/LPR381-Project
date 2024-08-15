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

            Console.WriteLine("Press 1 for the Simplex Algorithm");
            Console.WriteLine("Press 2 to Branch and Bound");
            Console.WriteLine("Press 3 to Solve Cutting Plane");
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
                        menu = false;
                        break;

                    case 1:
                        Console.Clear();
                        string inputFilePath = "lp_model.txt";
                        string outputFilePath = "canonical_lp_model.txt";

                        FileParser.TakeInputAndSaveToFile(inputFilePath);

                        double[,] canonicalForm = FileParser.ConvertToCanonicalForm(inputFilePath, outputFilePath);

                        SimplexAlgo test = new SimplexAlgo();

                        for(int i = 0; i < canonicalForm.GetLength(0); i++)
                        {
                            for(int j = 0; j < canonicalForm.GetLength(1); j++)
                            {
                                Console.Write(canonicalForm[i, j] + "\t");
                            }
                            Console.WriteLine();
                        }

                        test.Simplex(canonicalForm);
                        Console.WriteLine("\nPress 0 to go back");
                        int inputSS = int.Parse(Console.ReadLine());

                        if (inputSS == 0)
                        {
                            Console.Clear();
                            Run();
                        }
                        else
                        {
                            menu = false;
                            Console.WriteLine("Invalid input");
                        }
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
                        int vCount = FileParser.GetNumberOfVariables("lp_model.txt");
                        BranchAndBound b = new BranchAndBound();
                        b.SolveBB(dataT.BranchAndBound(dat), vCount, 15);

                        Console.WriteLine("\nPress 0 to go back");
                        int inputBB = int.Parse(Console.ReadLine());

                        if(inputBB == 0)
                        {
                            Console.Clear();
                            Run();
                        }
                        else
                        {
                            menu = false;
                            Console.WriteLine("Invalid input");
                        }
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
                        int vCountTwo = FileParser.GetNumberOfVariables("lp_model.txt");
                        double[,] cuttingPlaneResult = plane.SolveCuttingPlane(sAlgo.BranchAndBound(dataNew), vCountTwo);
                        sAlgo.Simplex(cuttingPlaneResult);
                        Console.WriteLine("\nPress 0 to go back");
                        int inputCP = int.Parse(Console.ReadLine());

                        if (inputCP == 0)
                        {
                            Console.Clear();
                            Run();
                        }
                        else
                        {
                            menu = false;
                            Console.WriteLine("Invalid input");
                        }
                        break;

                    case 4:
                        Console.Clear();
                        KnapOrginal kp = new KnapOrginal();
                        kp.KnapSackRound(40);

                        Console.WriteLine("\nPress 0 to go back");
                        int inputKP = int.Parse(Console.ReadLine());

                        if (inputKP == 0)
                        {
                            Console.Clear();
                            Run();
                        }
                        else
                        {
                            menu = false;
                            Console.WriteLine("Invalid input");
                        }
                    break;

                    default:
                        Console.WriteLine("Something went wrong");
                    break;
                }
            }

        }
    }
}
