using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPR381_Project
{
    internal class Menu
    {
        public void Run()
        {
            Console.WriteLine("---------------------------\n" +
                "Welcome to the the ALGO app !\n" +
                "---------------------------"
                );

            Console.WriteLine("Press 1 for the simplex algorithm");
            Console.WriteLine("Press 0 to exit");

            int input = int.Parse(Console.ReadLine());

            bool menu = true;

            while (menu)
            {
                switch (input)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Lets go!");
                        menu = false;
                        break;

                    case 0:
                        Console.Clear();
                        Console.WriteLine("Thank you for using our application");
                        menu = false;
                        break;
                }
            }

        }
    }
}
